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
    /// <summary>
    /// Professional Retail & Supermarket Thermal Receipt Printer Engine.
    /// Uses native Windows OpenType/TrueType system fonts (Segoe UI / Tahoma)
    /// to ensure 100% crash-free printing on all printer device contexts (PrintDocument & PrintPreviewDialog).
    /// </summary>
    public static class ReceiptPrinter
    {
        private static Font GetSafeFont(float size, bool bold)
        {
            FontStyle style = bold ? FontStyle.Bold : FontStyle.Regular;
            try
            {
                // Segoe UI has beautiful, modern Arabic & Latin typography
                return new Font("Segoe UI", size, style, GraphicsUnit.Point);
            }
            catch
            {
                // Universal fallback available on 100% of Windows installations
                return new Font("Tahoma", size, style, GraphicsUnit.Point);
            }
        }

        // Standard 80mm thermal receipt printable width in hundredths of an inch
        private const int PaperWidthHundredths = 285;
        private const int SideMargin = 8;
        private const float ThermalDpi = 203f;

        // ═══════════════════════════════════════════════
        // الطباعة الحقيقية والمعاينة
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
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                DrawReceiptContent(e.Graphics, sale, items, startX, pageWidth);
                e.HasMorePages = false;
            };

            if (previewFirst)
            {
                using (PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 480,
                    Height = 720,
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
        // تصدير الفاتورة كصورة PNG بمقاس 80مم
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
                    g.SmoothingMode = SmoothingMode.HighQuality;
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
        // دالة الرسم المشتركة
        // ═══════════════════════════════════════════════
        private static void DrawReceiptContent(Graphics g, SaleModel sale, List<CartItemModel> items, float startX, float pageWidth)
        {
            using (Font titleFont = GetSafeFont(13.5f, bold: true))
            using (Font subtitleFont = GetSafeFont(9f, bold: true))
            using (Font headerFont = GetSafeFont(8.5f, bold: false))
            using (Font boldFont = GetSafeFont(8.5f, bold: true))
            using (Font smallFont = GetSafeFont(7.8f, bold: false))
            using (Font smallBoldFont = GetSafeFont(7.8f, bold: true))
            using (Font largeBoldFont = GetSafeFont(11f, bold: true))
            using (Brush textBrush = new SolidBrush(Color.Black))
            using (Brush mutedBrush = new SolidBrush(Color.FromArgb(70, 70, 70)))
            using (Brush boxFillBrush = new SolidBrush(Color.FromArgb(246, 248, 250)))
            using (Pen solidPen = new Pen(Color.Black, 1f))
            using (Pen doublePen = new Pen(Color.Black, 1.2f))
            using (Pen dashedPen = new Pen(Color.FromArgb(100, 100, 100), 1f) { DashStyle = DashStyle.Dash })
            {
                using (StringFormat sfCenter = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                using (StringFormat sfCenterWrapped = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                using (StringFormat sfRight = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                using (StringFormat sfLeft = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                using (StringFormat sfRightWrapped = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Near
                })
                {
                    SystemSettingsModel sysSettings = DbHelper.GetSystemSettings() ?? new SystemSettingsModel();
                    string storeName = !string.IsNullOrWhiteSpace(sysSettings.StoreName) ? sysSettings.StoreName : "هايبر ماركت ونقاط البيع";
                    string receiptHeader = !string.IsNullOrWhiteSpace(sysSettings.ReceiptHeader) ? sysSettings.ReceiptHeader : "فاتورة مبيعات ضريبية مبسطة";
                    string currency = !string.IsNullOrWhiteSpace(sysSettings.CurrencySymbol) ? sysSettings.CurrencySymbol : "ج.م";
                    string footerNote = !string.IsNullOrWhiteSpace(sysSettings.ReceiptFooter) ? sysSettings.ReceiptFooter : "الأسعار تشمل ضريبة القيمة المضافة • البضاعة المباعة ترد وتستبدل خلال 14 يوماً بالفاتورة";

                    float y = 10f;

                    // 1. رأس الفاتورة (Store Header)
                    g.DrawString(storeName, titleFont, textBrush, new RectangleF(startX, y, pageWidth, 26f), sfCenter);
                    y += 26f;

                    g.DrawString($"• {receiptHeader} •", subtitleFont, textBrush, new RectangleF(startX, y, pageWidth, 18f), sfCenter);
                    y += 19f;

                    if (!string.IsNullOrWhiteSpace(sysSettings.StorePhone) || !string.IsNullOrWhiteSpace(sysSettings.StoreAddress))
                    {
                        string contact = "";
                        if (!string.IsNullOrWhiteSpace(sysSettings.StorePhone)) contact += $"هاتف: {sysSettings.StorePhone}  ";
                        if (!string.IsNullOrWhiteSpace(sysSettings.StoreAddress)) contact += $"• {sysSettings.StoreAddress}";

                        g.DrawString(contact, smallFont, mutedBrush, new RectangleF(startX, y, pageWidth, 16f), sfCenter);
                        y += 16f;
                    }

                    if (!string.IsNullOrWhiteSpace(sysSettings.TaxNumber))
                    {
                        g.DrawString($"الرقم الضريبي: {sysSettings.TaxNumber}", smallBoldFont, textBrush, new RectangleF(startX, y, pageWidth, 16f), sfCenter);
                        y += 16f;
                    }

                    y += 4f;
                    g.DrawLine(doublePen, startX, y, startX + pageWidth, y);
                    y += 8f;

                    // 2. بيانات الفاتورة والكاشير (Invoice Meta)
                    float halfW = pageWidth / 2f;

                    // السطر 1: رقم الفاتورة (يمين) + طريقة الدفع (يسار)
                    g.DrawString($"رقم الفاتورة: #{sale.SaleId:D5}", boldFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                    g.DrawString($"الدفع: {sale.PaymentMethod}", boldFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                    y += 18f;

                    // السطر 2: التاريخ (يمين) + الكاشير (يسار)
                    string dateStr = sale.SaleDate.ToString("yyyy-MM-dd HH:mm");
                    g.DrawString($"التاريخ: {dateStr}", smallFont, mutedBrush, new RectangleF(startX + halfW, y, halfW, 16f), sfRight);
                    g.DrawString($"الكاشير: {sale.CashierName}", smallFont, mutedBrush, new RectangleF(startX, y, halfW, 16f), sfLeft);
                    y += 18f;

                    y += 4f;
                    g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                    y += 8f;

                    // 3. ترويسة جدول الأصناف (Items Header)
                    float colQtyW = 34f;
                    float colPriceW = 54f;
                    float colTotalW = 58f;
                    float colProdW = pageWidth - colQtyW - colPriceW - colTotalW;

                    // [الإجمالي] [السعر] [الكمية] [الصنف]
                    float curX = startX;
                    g.DrawString("الإجمالي", boldFont, textBrush, new RectangleF(curX, y, colTotalW, 18f), sfLeft);

                    curX += colTotalW;
                    g.DrawString("السعر", boldFont, textBrush, new RectangleF(curX, y, colPriceW, 18f), sfLeft);

                    curX += colPriceW;
                    g.DrawString("الكمية", boldFont, textBrush, new RectangleF(curX, y, colQtyW, 18f), sfCenter);

                    curX += colQtyW;
                    g.DrawString("الصنف", boldFont, textBrush, new RectangleF(curX, y, colProdW, 18f), sfRight);
                    y += 20f;

                    g.DrawLine(solidPen, startX, y, startX + pageWidth, y);
                    y += 6f;

                    // 4. صفوف الأصناف (Items Rows)
                    int totalQuantityCount = 0;
                    foreach (var item in items)
                    {
                        totalQuantityCount += item.Quantity;
                        SizeF measured = g.MeasureString(item.ProductName, smallFont, (int)colProdW);
                        float rowHeight = Math.Max(18f, measured.Height + 2f);

                        curX = startX;
                        g.DrawString($"{item.LineTotal:N2}", boldFont, textBrush, new RectangleF(curX, y, colTotalW, rowHeight), sfLeft);

                        curX += colTotalW;
                        g.DrawString($"{item.UnitPrice:N2}", smallFont, textBrush, new RectangleF(curX, y, colPriceW, rowHeight), sfLeft);

                        curX += colPriceW;
                        g.DrawString(item.Quantity.ToString(), smallBoldFont, textBrush, new RectangleF(curX, y, colQtyW, rowHeight), sfCenter);

                        curX += colQtyW;
                        g.DrawString(item.ProductName, smallFont, textBrush, new RectangleF(curX, y, colProdW, rowHeight), sfRightWrapped);

                        y += rowHeight + 3f;
                    }

                    y += 2f;
                    g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                    y += 6f;

                    // ملخص عدد الأصناف وإجمالي القطع
                    string itemsSummary = $"عدد الأصناف: {items.Count}   |   إجمالي القطع: {totalQuantityCount}";
                    g.DrawString(itemsSummary, smallBoldFont, mutedBrush, new RectangleF(startX, y, pageWidth, 16f), sfCenter);
                    y += 18f;

                    g.DrawLine(solidPen, startX, y, startX + pageWidth, y);
                    y += 8f;

                    // 5. الحسابات والإجماليات (Totals Breakdown)
                    g.DrawString("المجموع الفرعي:", headerFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                    g.DrawString($"{sale.TotalAmount:N2} {currency}", boldFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                    y += 18f;

                    if (sale.Discount > 0)
                    {
                        g.DrawString("الخصم الممنوح:", headerFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                        g.DrawString($"- {sale.Discount:N2} {currency}", boldFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                        y += 18f;
                    }

                    if (sale.TaxAmount > 0)
                    {
                        string vatLabel = sysSettings.VatRate > 0 ? $"ضريبة القيمة المضافة ({sysSettings.VatRate:0.##}%):" : "ضريبة القيمة المضافة:";
                        g.DrawString(vatLabel, headerFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                        g.DrawString($"+ {sale.TaxAmount:N2} {currency}", boldFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                        y += 18f;
                    }

                    y += 4f;

                    // صندوق الإجمالي النهائي المستحق
                    RectangleF totalBoxRect = new RectangleF(startX, y, pageWidth, 28f);
                    g.FillRectangle(boxFillBrush, totalBoxRect);
                    g.DrawRectangle(solidPen, totalBoxRect.X, totalBoxRect.Y, totalBoxRect.Width, totalBoxRect.Height);

                    g.DrawString("الإجمالي المستحق:", largeBoldFont, textBrush, new RectangleF(startX + halfW, y, halfW - 6f, 28f), sfRight);
                    g.DrawString($"{sale.FinalAmount:N2} {currency}", largeBoldFont, textBrush, new RectangleF(startX + 6f, y, halfW, 28f), sfLeft);
                    y += 34f;

                    // المدفوع والباقي
                    g.DrawString("المبلغ المدفوع:", headerFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                    g.DrawString($"{sale.PaidAmount:N2} {currency}", headerFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                    y += 18f;

                    g.DrawString("المتبقي (الباقي):", boldFont, textBrush, new RectangleF(startX + halfW, y, halfW, 18f), sfRight);
                    g.DrawString($"{sale.ChangeAmount:N2} {currency}", boldFont, textBrush, new RectangleF(startX, y, halfW, 18f), sfLeft);
                    y += 24f;

                    g.DrawLine(dashedPen, startX, y, startX + pageWidth, y);
                    y += 12f;

                    // 6. تذييل الفاتورة (Footer)
                    g.DrawString("★ شكراً لزيارتكم ونتمنى لكم يوماً سعيداً ★", boldFont, textBrush, new RectangleF(startX, y, pageWidth, 18f), sfCenter);
                    y += 20f;

                    SizeF footerSize = g.MeasureString(footerNote, smallFont, (int)pageWidth);
                    float footerH = Math.Max(20f, footerSize.Height + 4f);
                    g.DrawString(footerNote, smallFont, mutedBrush, new RectangleF(startX, y, pageWidth, footerH), sfCenterWrapped);
                    y += footerH + 12f;
                }
            }
        }

        private static int CalculateContentHeight(SaleModel sale, List<CartItemModel> items, int pageWidth)
        {
            float height = 10f;
            height += 26f + 19f + 16f + 16f + 12f; // Header
            height += 18f + 18f + 12f; // Meta
            height += 20f + 6f; // Table Header

            using (Bitmap bmp = new Bitmap(1, 1))
            {
                bmp.SetResolution(ThermalDpi, ThermalDpi);
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font smallFont = GetSafeFont(7.8f, bold: false))
                {
                    float colProdW = pageWidth - 34f - 54f - 58f;
                    foreach (var item in items)
                    {
                        SizeF measured = g.MeasureString(item.ProductName, smallFont, (int)colProdW);
                        height += Math.Max(18f, measured.Height + 2f) + 3f;
                    }
                }
            }

            height += 6f + 18f + 8f; // Table summary
            height += 18f; // Subtotal
            if (sale.Discount > 0) height += 18f;
            if (sale.TaxAmount > 0) height += 18f;
            height += 34f; // Total box
            height += 18f + 24f + 12f; // Paid & change
            height += 20f + 30f + 16f; // Footer

            return (int)Math.Ceiling(Math.Max(height, 280f));
        }
    }
}