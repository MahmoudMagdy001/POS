using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace POS
{
    public static class ReceiptPrinter
    {
        // تحميل خط Cairo من مجلد fonts بجانب الـ exe
        private static readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private static FontFamily _cairoFamily;

        static ReceiptPrinter()
        {
            try
            {
                string fontsDir = Path.Combine(Application.StartupPath, "fonts");
                string regularPath = Path.Combine(fontsDir, "Cairo-Regular.ttf");
                string boldPath = Path.Combine(fontsDir, "Cairo-Bold.ttf");

                if (File.Exists(regularPath)) _fonts.AddFontFile(regularPath);
                if (File.Exists(boldPath)) _fonts.AddFontFile(boldPath);

                foreach (var family in _fonts.Families)
                {
                    if (family.Name.IndexOf("Cairo", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _cairoFamily = family;
                        break;
                    }
                }
            }
            catch
            {
                _cairoFamily = null; // هيرجع لـ Tahoma تلقائي لو فشل التحميل
            }
        }

        private static FontFamily ArabicFamily => _cairoFamily ?? new FontFamily("Tahoma");

        // عرض قابل للطباعة آمن لورق 80مم (بالمئة من البوصة)
        private const int PaperWidthHundredths = 302;
        private const int SideMargin = 6;

        // الدقة القياسية لطابعات الكاشير الحرارية (نقطة/بوصة)
        private const float ThermalDpi = 203f;

        // ═══════════════════════════════════════════════
        // الطباعة الحقيقية على طابعة فعلية
        // ═══════════════════════════════════════════════
        public static void PrintReceipt(SaleModel sale, List<CartItemModel> items, bool previewFirst = true)
        {
            if (sale == null || items == null) return;

            int pageWidth = PaperWidthHundredths - (SideMargin * 2);
            int startX = SideMargin;
            int estimatedHeight = CalculateContentHeight(sale, items, pageWidth);

            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Thermal80mm", PaperWidthHundredths, estimatedHeight);
            printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printDoc.OriginAtMargins = false;

            printDoc.PrintPage += (sender, e) =>
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                DrawReceiptContent(e.Graphics, sale, items, startX, pageWidth);
                e.HasMorePages = false;
            };

            if (previewFirst)
            {
                using (PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 450,
                    Height = 650,
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = $"معاينة فاتورة رقم #{sale.SaleId:D5}"
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

        // ═══════════════════════════════════════════════
        // تصدير الفاتورة كصورة PNG بمقاس 80مم حقيقي (من غير أي طابعة)
        // ═══════════════════════════════════════════════
        // ═══════════════════════════════════════════════
        // تصدير الفاتورة كصورة PNG بمقاس 80مم حقيقي — بتتحفظ أوتوماتيك على الـ Desktop
        // ═══════════════════════════════════════════════
        public static string PreviewReceiptAsImage(SaleModel sale, List<CartItemModel> items, bool openAfterSave = true)
        {
            if (sale == null || items == null) return null;

            int pageWidth = PaperWidthHundredths - (SideMargin * 2);
            int startX = SideMargin;
            int estimatedHeight = CalculateContentHeight(sale, items, pageWidth);

            int pxWidth = (int)Math.Ceiling(PaperWidthHundredths / 100f * ThermalDpi);
            int pxHeight = (int)Math.Ceiling(estimatedHeight / 100f * ThermalDpi);

            using (Bitmap bmp = new Bitmap(pxWidth, pxHeight))
            {
                bmp.SetResolution(ThermalDpi, ThermalDpi);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    g.PageUnit = GraphicsUnit.Display;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                    DrawReceiptContent(g, sale, items, startX, pageWidth);
                }

                // حفظ أوتوماتيك على سطح المكتب
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"Receipt_{sale.SaleId:D5}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
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

        // ═══════════════════════════════════════════════
        // دالة الرسم المشتركة — نفس المنطق للطباعة وللصورة
        // ═══════════════════════════════════════════════
        private static void DrawReceiptContent(Graphics g, SaleModel sale, List<CartItemModel> items, int startX, int pageWidth)
        {
            using (Font titleFont = new Font(ArabicFamily, 12, FontStyle.Bold))
            using (Font headerFont = new Font(ArabicFamily, 8.5f, FontStyle.Regular))
            using (Font boldFont = new Font(ArabicFamily, 8.5f, FontStyle.Bold))
            using (Font smallFont = new Font(ArabicFamily, 8, FontStyle.Regular))
            using (Font largeBoldFont = new Font(ArabicFamily, 11, FontStyle.Bold))
            using (Pen dashedPen = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                Color textColor = Color.Black;

                TextFormatFlags flagsCenter = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsRight = TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsCenterWrapped = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsRightWrapped = TextFormatFlags.Right | TextFormatFlags.WordBreak | TextFormatFlags.RightToLeft;

                int y = 6;

                // 1. رأس الفاتورة
                TextRenderer.DrawText(g, "نظام نقاط البيع والسوبرماركت", titleFont,
                    new Rectangle(startX, y, pageWidth, 28), textColor, flagsCenter);
                y += 28;

                TextRenderer.DrawText(g, "فاتورة مبيعات ضريبية مبسطة", headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsCenter);
                y += 18;

                TextRenderer.DrawText(g, "هاتف: 01001234567 • القاهرة، مصر", smallFont,
                    new Rectangle(startX, y, pageWidth, 16), textColor, flagsCenter);
                y += 20;

                g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                y += 8;

                // 2. بيانات الفاتورة
                string dateStr = sale.SaleDate.ToString("yyyy-MM-dd HH:mm:ss");

                TextRenderer.DrawText(g, $"رقم الفاتورة: #{sale.SaleId:D5}", boldFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 18;

                TextRenderer.DrawText(g, $"التاريخ: {dateStr}", smallFont,
                    new Rectangle(startX, y, pageWidth, 16), textColor, flagsRight);
                y += 16;

                TextRenderer.DrawText(g, $"الكاشير: {sale.CashierName}", smallFont,
                    new Rectangle(startX, y, pageWidth, 16), textColor, flagsRight);
                y += 16;

                TextRenderer.DrawText(g, $"طريقة الدفع: {sale.PaymentMethod}", smallFont,
                    new Rectangle(startX, y, pageWidth, 16), textColor, flagsRight);
                y += 20;

                g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                y += 8;

                // 3. ترويسة جدول الأصناف
                int colQtyW = 34;
                int colPriceW = 50;
                int colTotalW = 58;
                int colProdW = pageWidth - colQtyW - colPriceW - colTotalW;

                int curX = startX + pageWidth - colProdW;
                TextRenderer.DrawText(g, "الصنف", boldFont,
                    new Rectangle(curX, y, colProdW, 18), textColor, flagsRight);

                curX -= colQtyW;
                TextRenderer.DrawText(g, "الكمية", boldFont,
                    new Rectangle(curX, y, colQtyW, 18), textColor, flagsCenter);

                curX -= colPriceW;
                TextRenderer.DrawText(g, "السعر", boldFont,
                    new Rectangle(curX, y, colPriceW, 18), textColor, flagsRight);

                curX -= colTotalW;
                TextRenderer.DrawText(g, "الإجمالي", boldFont,
                    new Rectangle(curX, y, colTotalW, 18), textColor, flagsRight);
                y += 20;

                g.DrawLine(Pens.Black, startX, y, startX + pageWidth, y);
                y += 6;

                // 4. صفوف الأصناف — بارتفاع مرن لو اسم الصنف طويل
                foreach (var item in items)
                {
                    int rowHeight = MeasureRowHeight(g, item.ProductName, smallFont, colProdW);

                    curX = startX + pageWidth - colProdW;
                    TextRenderer.DrawText(g, item.ProductName, smallFont,
                        new Rectangle(curX, y, colProdW, rowHeight), textColor, flagsRightWrapped);

                    curX -= colQtyW;
                    TextRenderer.DrawText(g, item.Quantity.ToString(), smallFont,
                        new Rectangle(curX, y, colQtyW, rowHeight), textColor, flagsCenter);

                    curX -= colPriceW;
                    TextRenderer.DrawText(g, $"{item.UnitPrice:N2}", smallFont,
                        new Rectangle(curX, y, colPriceW, rowHeight), textColor, flagsRight);

                    curX -= colTotalW;
                    TextRenderer.DrawText(g, $"{item.LineTotal:N2}", boldFont,
                        new Rectangle(curX, y, colTotalW, rowHeight), textColor, flagsRight);

                    y += rowHeight;
                }

                y += 6;
                g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                y += 8;

                // 5. الحسابات والإجماليات
                TextRenderer.DrawText(g, $"المجموع: {sale.TotalAmount:N2} ج.م", headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 18;

                if (sale.Discount > 0)
                {
                    TextRenderer.DrawText(g, $"الخصم: -{sale.Discount:N2} ج.م", headerFont,
                        new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                    y += 18;
                }

                if (sale.TotalRefunded > 0)
                {
                    TextRenderer.DrawText(g, $"المسترد (مرتجع): -{sale.TotalRefunded:N2} ج.م", headerFont,
                        new Rectangle(startX, y, pageWidth, 18), Color.FromArgb(220, 38, 38), flagsRight);
                    y += 18;
                }

                g.DrawLine(Pens.Black, startX, y, startX + pageWidth, y);
                y += 6;

                string finalLabel = sale.TotalRefunded > 0 ? $"صافي المستحق: {sale.NetFinalAmount:N2} ج.م" : $"الإجمالي المستحق: {sale.FinalAmount:N2} ج.م";
                TextRenderer.DrawText(g, finalLabel, largeBoldFont,
                    new Rectangle(startX, y, pageWidth, 24), textColor, flagsRight);
                y += 24;

                TextRenderer.DrawText(g, $"المدفوع: {sale.PaidAmount:N2} ج.م", headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 18;

                TextRenderer.DrawText(g, $"الباقي: {sale.ChangeAmount:N2} ج.م", boldFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 24;

                g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                y += 12;

                // 6. تذييل الفاتورة
                TextRenderer.DrawText(g, "شكراً لزيارتكم ونتمنى لكم يوماً سعيداً!", boldFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsCenter);
                y += 18;

                string footerText = "الأسعار تشمل ضريبة القيمة المضافة • البضاعة المباعة ترد وتستبدل خلال 14 يوماً بالفاتورة";
                int footerHeight = MeasureRowHeight(g, footerText, smallFont, pageWidth);

                TextRenderer.DrawText(g, footerText, smallFont,
                    new Rectangle(startX, y, pageWidth, footerHeight), textColor, flagsCenterWrapped);
            }
        }

        // بيحسب ارتفاع الصف حسب عدد الأسطر اللي النص هياخدها لو طويل
        private static int MeasureRowHeight(Graphics g, string text, Font font, int width)
        {
            SizeF size = g.MeasureString(text, font, width);
            int lines = Math.Max(1, (int)Math.Ceiling(size.Height / font.GetHeight(g)));
            return Math.Max(18, lines * 16);
        }

        // بيحسب الطول الكلي المتوقع للفاتورة قبل الطباعة/التصدير
        private static int CalculateContentHeight(SaleModel sale, List<CartItemModel> items, int pageWidth)
        {
            int height = 6;
            height += 28 + 18 + 20;
            height += 8;
            height += 18 + 16 + 16 + 20;
            height += 8;
            height += 20 + 6;

            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font smallFont = new Font(ArabicFamily, 8))
            {
                int colProdW = pageWidth - 34 - 50 - 58;
                foreach (var item in items)
                    height += MeasureRowHeight(g, item.ProductName, smallFont, colProdW);
            }

            height += 6 + 8;
            height += 18;
            if (sale.Discount > 0) height += 18;
            height += 6;
            height += 24 + 18 + 24;
            height += 12;
            height += 18;
            height += 40;
            height += 20;

            return Math.Max(height, 300);
        }
    }
}