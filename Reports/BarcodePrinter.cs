using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace POS
{
    public static class BarcodePrinter
    {
        private static readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private static FontFamily _cairoRegularFamily;
        private static FontFamily _cairoBoldFamily;

        static BarcodePrinter()
        {
            try
            {
                string fontsDir = Path.Combine(Application.StartupPath, "fonts");
                string regularPath = Path.Combine(fontsDir, "Cairo-Regular.ttf");
                string boldPath = Path.Combine(fontsDir, "Cairo-Bold.ttf");

                if (File.Exists(regularPath)) _fonts.AddFontFile(regularPath);
                if (File.Exists(boldPath)) _fonts.AddFontFile(boldPath);

                if (_fonts.Families.Length >= 1) _cairoRegularFamily = _fonts.Families[0];
                if (_fonts.Families.Length >= 2) _cairoBoldFamily = _fonts.Families[1];
                else _cairoBoldFamily = _cairoRegularFamily;
            }
            catch
            {
                _cairoRegularFamily = null;
                _cairoBoldFamily = null;
            }
        }

        private static Font CreateFont(float size, bool bold)
        {
            FontFamily family = bold ? _cairoBoldFamily : _cairoRegularFamily;
            if (family != null)
            {
                if (family.IsStyleAvailable(bold ? FontStyle.Bold : FontStyle.Regular))
                    return new Font(family, size, bold ? FontStyle.Bold : FontStyle.Regular);
            }
            return new Font("Tahoma", size, bold ? FontStyle.Bold : FontStyle.Regular);
        }

        /// <summary>
        /// Renders a single product barcode label inside the specified rectangle on any Graphics canvas.
        /// </summary>
        public static void DrawSingleLabel(Graphics g, ProductModel product, BarcodePrintOptions options, RectangleF rect)
        {
            if (g == null || product == null || options == null)
                return;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Background & Border (white background, thin subtle border if needed)
            using (Brush bgBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(bgBrush, rect);
            }

            float paddingX = rect.Width * 0.04f;
            float paddingY = rect.Height * 0.04f;
            float contentWidth = rect.Width - (paddingX * 2);
            float currentY = rect.Y + paddingY;
            float availableHeight = rect.Height - (paddingY * 2);

            Color textColor = Color.Black;

            // 1. Store Name (Header)
            if (options.ShowStoreName)
            {
                string storeName = !string.IsNullOrWhiteSpace(options.StoreName) ? options.StoreName : "متجر تجريبي";
                float storeFontSize = Math.Max(6.5f, Math.Min(9.5f, rect.Height * 0.085f));
                using (Font storeFont = CreateFont(storeFontSize, bold: true))
                using (Brush brush = new SolidBrush(textColor))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.DirectionRightToLeft
                })
                {
                    float h = Math.Max(12f, rect.Height * 0.12f);
                    RectangleF storeRect = new RectangleF(rect.X + paddingX, currentY, contentWidth, h);
                    g.DrawString(storeName, storeFont, brush, storeRect, sf);
                    currentY += h + 2f;
                }
            }

            // 2. Product Name
            if (options.ShowProductName)
            {
                string prodName = product.ProductName ?? "";
                float prodFontSize = Math.Max(7f, Math.Min(10f, rect.Height * 0.09f));
                using (Font prodFont = CreateFont(prodFontSize, bold: true))
                using (Brush brush = new SolidBrush(textColor))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                })
                {
                    float h = Math.Max(14f, rect.Height * 0.14f);
                    RectangleF prodRect = new RectangleF(rect.X + paddingX, currentY, contentWidth, h);
                    g.DrawString(prodName, prodFont, brush, prodRect, sf);
                    currentY += h + 2f;
                }
            }

            // Calculate bottom price height
            float priceHeight = 0;
            if (options.ShowPrice)
            {
                priceHeight = Math.Max(16f, rect.Height * 0.18f);
            }

            // 3. Barcode graphic in the middle
            float remainingHeight = (rect.Y + rect.Height - paddingY) - currentY - priceHeight;
            if (remainingHeight > 15f)
            {
                float barcodeAreaHeight = remainingHeight;
                RectangleF barcodeRect = new RectangleF(rect.X + paddingX, currentY, contentWidth, barcodeAreaHeight);
                
                string barcodeText = string.IsNullOrWhiteSpace(product.Barcode) ? "000000000000" : product.Barcode;
                float barcodeTextFontSize = Math.Max(6f, Math.Min(8.5f, rect.Height * 0.075f));
                
                using (Font monoFont = new Font("Consolas", barcodeTextFontSize, FontStyle.Bold))
                {
                    BarcodeGenerator.DrawBarcode(g, barcodeText, barcodeRect, options.ShowBarcodeText, monoFont, Color.Black, textColor);
                }
                currentY += barcodeAreaHeight;
            }

            // 4. Price & Currency at the bottom
            if (options.ShowPrice)
            {
                string currency = !string.IsNullOrWhiteSpace(options.CurrencySymbol) ? options.CurrencySymbol : "ج.م";
                string priceText = $"{product.SellPrice:N2} {currency}";
                
                float priceFontSize = Math.Max(8f, Math.Min(12f, rect.Height * 0.12f));
                using (Font priceFont = CreateFont(priceFontSize, bold: true))
                using (Brush brush = new SolidBrush(textColor))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                })
                {
                    RectangleF priceRect = new RectangleF(rect.X + paddingX, currentY, contentWidth, priceHeight);
                    g.DrawString(priceText, priceFont, brush, priceRect, sf);
                }
            }
        }

        /// <summary>
        /// Prints barcode labels to a physical or thermal printer according to specified options.
        /// </summary>
        public static void PrintLabels(ProductModel product, BarcodePrintOptions options, bool previewFirst = false)
        {
            if (product == null || options == null) return;

            PrintDocument printDoc = new PrintDocument();
            if (!string.IsNullOrWhiteSpace(options.PrinterName))
            {
                printDoc.PrinterSettings.PrinterName = options.PrinterName;
            }

            int totalCopies = Math.Max(1, options.Copies);
            int printedCount = 0;

            if (options.PrintMode == BarcodePrintMode.ThermalRoll)
            {
                // Single label per page mode
                int w = options.WidthInHundredths;
                int h = options.HeightInHundredths;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("BarcodeLabel", w, h);
                printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printDoc.OriginAtMargins = false;

                printDoc.PrintPage += (sender, e) =>
                {
                    RectangleF labelRect = new RectangleF(0, 0, e.PageBounds.Width, e.PageBounds.Height);
                    DrawSingleLabel(e.Graphics, product, options, labelRect);
                    
                    printedCount++;
                    e.HasMorePages = printedCount < totalCopies;
                };
            }
            else
            {
                // A4 Sheet multi-label grid mode
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 hundredths of inch
                printDoc.DefaultPageSettings.Margins = new Margins(20, 20, 20, 20);
                printDoc.OriginAtMargins = false;

                int cols = Math.Max(1, options.SheetColumns);
                int rows = Math.Max(1, options.SheetRows);
                int labelsPerPage = cols * rows;

                printDoc.PrintPage += (sender, e) =>
                {
                    float marginX = 20f;
                    float marginY = 20f;
                    float availableWidth = e.PageBounds.Width - (marginX * 2);
                    float availableHeight = e.PageBounds.Height - (marginY * 2);

                    float labelW = availableWidth / cols;
                    float labelH = availableHeight / rows;

                    using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 1) { DashStyle = DashStyle.Dot })
                    {
                        for (int r = 0; r < rows; r++)
                        {
                            for (int c = 0; c < cols; c++)
                            {
                                if (printedCount >= totalCopies)
                                    break;

                                float x = marginX + (c * labelW);
                                float y = marginY + (r * labelH);
                                RectangleF labelRect = new RectangleF(x + 4, y + 4, labelW - 8, labelH - 8);

                                DrawSingleLabel(e.Graphics, product, options, labelRect);
                                e.Graphics.DrawRectangle(borderPen, x, y, labelW, labelH);

                                printedCount++;
                            }
                            if (printedCount >= totalCopies) break;
                        }
                    }

                    e.HasMorePages = printedCount < totalCopies;
                };
            }

            if (previewFirst)
            {
                using (PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 600,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = $"معاينة طباعة باركود: {product.ProductName}"
                })
                {
                    previewDialog.ShowDialog();
                }
            }
            else
            {
                printDoc.Print();
            }
        }

        /// <summary>
        /// Generates a live bitmap preview of the sticker for the UI.
        /// </summary>
        public static Bitmap GenerateLabelPreviewBitmap(ProductModel product, BarcodePrintOptions options, int previewWidth, int previewHeight)
        {
            if (previewWidth <= 20) previewWidth = 300;
            if (previewHeight <= 20) previewHeight = 200;

            Bitmap bmp = new Bitmap(previewWidth, previewHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                RectangleF rect = new RectangleF(6, 6, previewWidth - 12, previewHeight - 12);
                DrawSingleLabel(g, product, options, rect);

                // Draw dashed outline to represent label bounds
                using (Pen borderPen = new Pen(Color.FromArgb(203, 213, 225), 1.5f) { DashStyle = DashStyle.Dash })
                {
                    g.DrawRectangle(borderPen, 2, 2, previewWidth - 4, previewHeight - 4);
                }
            }
            return bmp;
        }

        /// <summary>
        /// Saves a high-resolution PNG image of the label onto Desktop and optionally opens it.
        /// </summary>
        public static string PreviewLabelAsImage(ProductModel product, BarcodePrintOptions options, bool openAfterSave = true)
        {
            if (product == null || options == null) return null;

            const float dpi = 300f;
            int pxWidth = (int)Math.Ceiling((options.LabelWidthMm / 25.4f) * dpi);
            int pxHeight = (int)Math.Ceiling((options.LabelHeightMm / 25.4f) * dpi);

            using (Bitmap bmp = new Bitmap(pxWidth, pxHeight))
            {
                bmp.SetResolution(dpi, dpi);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    RectangleF rect = new RectangleF(0, 0, pxWidth, pxHeight);
                    DrawSingleLabel(g, product, options, rect);
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string cleanBarcode = !string.IsNullOrWhiteSpace(product.Barcode) ? product.Barcode : product.ProductId.ToString();
                string fileName = $"Barcode_{cleanBarcode}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string fullPath = Path.Combine(desktopPath, fileName);

                bmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                if (openAfterSave)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fullPath)
                    {
                        UseShellExecute = true
                    });
                }

                return fullPath;
            }
        }
    }
}
