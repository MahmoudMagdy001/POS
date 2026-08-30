using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace POS
{
    /// <summary>
    /// Professional standard Code 128 Barcode generator and renderer.
    /// Provides vector-sharp GDI+ drawing, checksum calculation, and Bitmap generation.
    /// </summary>
    public static class BarcodeGenerator
    {
        // Standard Code 128 Patterns (0 to 106)
        // Each pattern has 11 modules (Stop has 13 modules)
        private static readonly string[] Code128Patterns = new string[]
        {
            "11011001100", // 0
            "11001101100", // 1
            "11001100110", // 2
            "10010011000", // 3
            "10010001100", // 4
            "10001001100", // 5
            "10011001000", // 6
            "10011000100", // 7
            "10001100100", // 8
            "11001001000", // 9
            "11001000100", // 10
            "11000100100", // 11
            "10110011100", // 12
            "10011011100", // 13
            "10011001110", // 14
            "10111001100", // 15
            "10011101100", // 16
            "10011100110", // 17
            "11001110010", // 18
            "11001011100", // 19
            "11001001110", // 20
            "11011100100", // 21
            "11001110100", // 22
            "11101101110", // 23
            "11101001100", // 24
            "11100101100", // 25
            "11100100110", // 26
            "11101100100", // 27
            "11100110100", // 28
            "11100110010", // 29
            "11011011000", // 30
            "11011000110", // 31
            "11000110110", // 32
            "10100011000", // 33
            "10001011000", // 34
            "10001000110", // 35
            "10110001000", // 36
            "10001101000", // 37
            "10001100010", // 38
            "11010001000", // 39
            "11000101000", // 40
            "11000100010", // 41
            "10110111000", // 42
            "10110001110", // 43
            "10001101110", // 44
            "10111011000", // 45
            "10111000110", // 46
            "10001110110", // 47
            "11101110110", // 48
            "11010001110", // 49
            "11000101110", // 50
            "11011101000", // 51
            "11011100010", // 52
            "11011101110", // 53
            "11101011000", // 54
            "11101000110", // 55
            "11100010110", // 56
            "11101101000", // 57
            "11101100010", // 58
            "11100011010", // 59
            "11101111010", // 60
            "11001000010", // 61
            "11110001010", // 62
            "10100110000", // 63
            "10100001100", // 64
            "10010110000", // 65
            "10010000110", // 66
            "10000101100", // 67
            "10000100110", // 68
            "10110010000", // 69
            "10110000100", // 70
            "10011010000", // 71
            "10011000010", // 72
            "10000110100", // 73
            "10000110010", // 74
            "11000010010", // 75
            "11001010000", // 76
            "11110111010", // 77
            "11000010100", // 78
            "10001111010", // 79
            "10100111100", // 80
            "10010111100", // 81
            "10010011110", // 82
            "10111100100", // 83
            "10011110100", // 84
            "10011110010", // 85
            "11110100100", // 86
            "11110010100", // 87
            "11110010010", // 88
            "11011011110", // 89
            "11011110110", // 90
            "11110110110", // 91
            "10101111000", // 92
            "10100011110", // 93
            "10001011110", // 94
            "10111101000", // 95
            "10111100010", // 96
            "11110101000", // 97
            "11110100010", // 98
            "10111011110", // 99
            "10111101110", // 100
            "11101011110", // 101
            "11110101110", // 102
            "11010000100", // 103: Start A
            "11010010000", // 104: Start B
            "11010011100", // 105: Start C
            "1100011101011" // 106: Stop (13 modules)
        };

        private const int StartBCode = 104;
        private const int StopCode = 106;

        /// <summary>
        /// Generates the binary module string ("1" for black bar, "0" for white space) for a given text using Code 128B.
        /// </summary>
        public static string EncodeCode128B(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Sanitize text to printable ASCII (32-126)
            List<int> charCodes = new List<int>();
            foreach (char c in text)
            {
                int code = (int)c;
                if (code >= 32 && code <= 126)
                {
                    charCodes.Add(code - 32);
                }
                else
                {
                    // Replace unprintable characters with space
                    charCodes.Add(0);
                }
            }

            if (charCodes.Count == 0)
                return string.Empty;

            // Calculate checksum Modulo 103
            int checksum = StartBCode;
            for (int i = 0; i < charCodes.Count; i++)
            {
                checksum += charCodes[i] * (i + 1);
            }
            checksum %= 103;

            // Build binary pattern string
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            // Quiet Zone at start (10 modules)
            sb.Append("0000000000");

            // Start B
            sb.Append(Code128Patterns[StartBCode]);

            // Data characters
            foreach (int code in charCodes)
            {
                sb.Append(Code128Patterns[code]);
            }

            // Checksum
            sb.Append(Code128Patterns[checksum]);

            // Stop pattern
            sb.Append(Code128Patterns[StopCode]);

            // Quiet Zone at end (10 modules)
            sb.Append("0000000000");

            return sb.ToString();
        }

        /// <summary>
        /// Draws the barcode directly onto a GDI+ Graphics surface within the specified bounding rectangle.
        /// </summary>
        public static void DrawBarcode(Graphics g, string code, RectangleF bounds, bool drawText = false, Font textFont = null, Color? barColor = null, Color? textColor = null)
        {
            if (g == null || bounds.Width <= 0 || bounds.Height <= 0)
                return;

            string binary = EncodeCode128B(code);
            if (string.IsNullOrEmpty(binary))
                return;

            Color bColor = barColor ?? Color.Black;
            Color tColor = textColor ?? Color.Black;

            float barcodeHeight = bounds.Height;
            float textHeight = 0;

            if (drawText && !string.IsNullOrEmpty(code))
            {
                Font font = textFont ?? new Font("Consolas", 8.5f, FontStyle.Bold);
                SizeF textSize = g.MeasureString(code, font);
                textHeight = Math.Min(textSize.Height + 2, bounds.Height * 0.35f);
                barcodeHeight = bounds.Height - textHeight;
            }

            // Calculate module width
            int totalModules = binary.Length;
            float moduleWidth = bounds.Width / totalModules;
            float currentX = bounds.X;

            using (Brush brush = new SolidBrush(bColor))
            {
                int i = 0;
                while (i < binary.Length)
                {
                    if (binary[i] == '1')
                    {
                        // Count contiguous 1s for accurate anti-aliased single bar drawing
                        int barSpan = 0;
                        while (i + barSpan < binary.Length && binary[i + barSpan] == '1')
                        {
                            barSpan++;
                        }

                        float barWidth = barSpan * moduleWidth;
                        g.FillRectangle(brush, currentX, bounds.Y, barWidth, barcodeHeight);

                        currentX += barWidth;
                        i += barSpan;
                    }
                    else
                    {
                        currentX += moduleWidth;
                        i++;
                    }
                }
            }

            // Draw human-readable text below barcode if requested
            if (drawText && !string.IsNullOrEmpty(code) && textHeight > 0)
            {
                Font font = textFont ?? new Font("Consolas", 8.5f, FontStyle.Bold);
                using (Brush textBrush = new SolidBrush(tColor))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                {
                    RectangleF textRect = new RectangleF(bounds.X, bounds.Y + barcodeHeight, bounds.Width, textHeight);
                    g.DrawString(code, font, textBrush, textRect, sf);
                }
            }
        }

        /// <summary>
        /// Generates a standalone Bitmap containing the barcode.
        /// </summary>
        public static Bitmap GenerateBarcodeBitmap(string code, int width, int height, bool drawText = true, Font textFont = null)
        {
            if (width <= 10) width = 200;
            if (height <= 10) height = 80;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                DrawBarcode(g, code, new RectangleF(4, 4, width - 8, height - 8), drawText, textFont);
            }
            return bmp;
        }
    }
}
