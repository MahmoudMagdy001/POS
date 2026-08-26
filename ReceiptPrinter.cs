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
        // تحميل خط Cairo من مجلد fonts بجانب الـ exe — كل وزن كعائلة منفصلة
        private static readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private static FontFamily _cairoRegularFamily;
        private static FontFamily _cairoBoldFamily;

        static ReceiptPrinter()
        {
            try
            {
                string fontsDir = Path.Combine(Application.StartupPath, "fonts");
                string regularPath = Path.Combine(fontsDir, "Cairo-Regular.ttf");
                string boldPath = Path.Combine(fontsDir, "Cairo-Bold.ttf");

                if (File.Exists(regularPath))
                {
                    _fonts.AddFontFile(regularPath);
                }
                if (File.Exists(boldPath))
                {
                    _fonts.AddFontFile(boldPath);
                }

                // كل عائلة بتتحدد بترتيب تحميلها، مش بالاسم، عشان بعض نسخ Cairo
                // بتسجل الاتنين بنفس اسم العائلة "Cairo"
                if (_fonts.Families.Length >= 1) _cairoRegularFamily = _fonts.Families[0];
                if (_fonts.Families.Length >= 2) _cairoBoldFamily = _fonts.Families[1];
                else _cairoBoldFamily = _cairoRegularFamily; // مفيش ملف Bold منفصل، استخدم نفس العائلة
            }
            catch
            {
                _cairoRegularFamily = null;
                _cairoBoldFamily = null;
            }
        }

        // بيرجع الخط الصح حسب الوزن المطلوب فعلياً — من غير ما يطلب Bold صناعي من GDI+
        private static Font CreateFont(float size, bool bold)
        {
            FontFamily family = bold ? _cairoBoldFamily : _cairoRegularFamily;

            if (family != null)
            {
                // نطلب دايماً Regular من العائلة نفسها، لأن الوزن (Bold/Regular)
                // ده جاي من اختيار ملف الخط مش من الـ FontStyle
                if (family.IsStyleAvailable(FontStyle.Regular))
                    return new Font(family, size, FontStyle.Regular);

                // لو العائلة مش بتدعم Regular لأي سبب، اطلب أي style متاح فعلياً
                if (family.IsStyleAvailable(FontStyle.Bold))
                    return new Font(family, size, FontStyle.Bold);
            }

            // فallback نهائي: Tahoma بتدعم الوزنين بشكل طبيعي وسليم
            return new Font("Tahoma", size, bold ? FontStyle.Bold : FontStyle.Regular);
        }

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
            using (Font titleFont = CreateFont(12, bold: true))
            using (Font headerFont = CreateFont(8.5f, bold: false))
            using (Font boldFont = CreateFont(8.5f, bold: true))
            using (Font smallFont = CreateFont(8, bold: false))
            using (Font largeBoldFont = CreateFont(11, bold: true))
            using (Pen dashedPen = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                Color textColor = Color.Black;

                TextFormatFlags flagsCenter = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsRight = TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsCenterWrapped = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak | TextFormatFlags.RightToLeft;
                TextFormatFlags flagsRightWrapped = TextFormatFlags.Right | TextFormatFlags.WordBreak | TextFormatFlags.RightToLeft;

                SystemSettingsModel sysSettings = DbHelper.GetSystemSettings();
                string storeName = !string.IsNullOrWhiteSpace(sysSettings.StoreName) ? sysSettings.StoreName : "نظام نقاط البيع والسوبرماركت";
                string receiptHeader = !string.IsNullOrWhiteSpace(sysSettings.ReceiptHeader) ? sysSettings.ReceiptHeader : "فاتورة مبيعات ضريبية مبسطة";
                string storeContact = $"هاتف: {sysSettings.StorePhone} • {sysSettings.StoreAddress}" + (string.IsNullOrWhiteSpace(sysSettings.TaxNumber) ? "" : $" • س.ت: {sysSettings.TaxNumber}");
                string currency = !string.IsNullOrWhiteSpace(sysSettings.CurrencySymbol) ? sysSettings.CurrencySymbol : "ج.م";
                string footerNote = !string.IsNullOrWhiteSpace(sysSettings.ReceiptFooter) ? sysSettings.ReceiptFooter : "الأسعار تشمل ضريبة القيمة المضافة • البضاعة المباعة ترد وتستبدل خلال 14 يوماً بالفاتورة";

                int y = 6;

                // 1. رأس الفاتورة
                TextRenderer.DrawText(g, storeName, titleFont,
                    new Rectangle(startX, y, pageWidth, 28), textColor, flagsCenter);
                y += 28;

                TextRenderer.DrawText(g, receiptHeader, headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsCenter);
                y += 18;

                TextRenderer.DrawText(g, storeContact, smallFont,
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
                TextRenderer.DrawText(g, $"المجموع: {sale.TotalAmount:N2} {currency}", headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 18;

                if (sale.Discount > 0)
                {
                    TextRenderer.DrawText(g, $"الخصم: -{sale.Discount:N2} {currency}", headerFont,
                        new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                    y += 18;
                }

                g.DrawLine(Pens.Black, startX, y, startX + pageWidth, y);
                y += 6;

                TextRenderer.DrawText(g, $"الإجمالي المستحق: {sale.FinalAmount:N2} {currency}", largeBoldFont,
                    new Rectangle(startX, y, pageWidth, 24), textColor, flagsRight);
                y += 24;

                TextRenderer.DrawText(g, $"المدفوع: {sale.PaidAmount:N2} {currency}", headerFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 18;

                TextRenderer.DrawText(g, $"الباقي: {sale.ChangeAmount:N2} {currency}", boldFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsRight);
                y += 24;

                g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                y += 12;

                // 6. تذييل الفاتورة
                TextRenderer.DrawText(g, "شكراً لزيارتكم ونتمنى لكم يوماً سعيداً!", boldFont,
                    new Rectangle(startX, y, pageWidth, 18), textColor, flagsCenter);
                y += 18;

                int footerHeight = MeasureRowHeight(g, footerNote, smallFont, pageWidth);

                TextRenderer.DrawText(g, footerNote, smallFont,
                    new Rectangle(startX, y, pageWidth, footerHeight), textColor, flagsCenterWrapped);
            }
        }

        // بيحسب ارتفاع الصف بنفس محرك القياس اللي بيرسم بيه TextRenderer.DrawText فعلياً
        private static int MeasureRowHeight(Graphics g, string text, Font font, int width)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.RightToLeft;
            Size proposed = new Size(width, int.MaxValue);
            Size actual = TextRenderer.MeasureText(g, text, font, proposed, flags);
            return Math.Max(18, actual.Height);
        }

        // بيحسب الطول الكلي المتوقع للفاتورة، بنفس دقة (DPI) الصورة/الطباعة الفعلية
        private static int CalculateContentHeight(SaleModel sale, List<CartItemModel> items, int pageWidth)
        {
            int height = 6;
            height += 28 + 18 + 20;
            height += 8;
            height += 18 + 16 + 16 + 20;
            height += 8;
            height += 20 + 6;

            using (Bitmap bmp = new Bitmap(1, 1))
            {
                bmp.SetResolution(ThermalDpi, ThermalDpi);
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font smallFont = CreateFont(8, bold: false))
                {
                    g.PageUnit = GraphicsUnit.Display;

                    int colProdW = pageWidth - 34 - 50 - 58;
                    foreach (var item in items)
                        height += MeasureRowHeight(g, item.ProductName, smallFont, colProdW);
                }
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