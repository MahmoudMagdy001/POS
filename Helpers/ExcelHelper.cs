using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace POS
{
    public static class ExcelHelper
    {
        #region Export Products to Real Native XLSX / CSV

        /// <summary>
        /// Exports products to a native OpenXML Excel Workbook (.xlsx) or CSV with UTF-8 BOM.
        /// Opens in Microsoft Excel instantly with 0 warnings or popups.
        /// </summary>
        public static void ExportProducts(string filePath, DataTable dt, string sheetName = "المنتجات والمخزون")
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext == ".csv")
            {
                ExportToCsv(filePath, dt);
            }
            else
            {
                ExportToNativeXlsx(filePath, dt, sheetName);
            }
        }

        private static void ExportToCsv(string filePath, DataTable dt)
        {
            var sb = new StringBuilder();

            // Row 1: Headers
            sb.AppendLine("\"الباركود\",\"اسم المنتج\",\"القسم\",\"سعر الشراء\",\"سعر البيع\",\"الكمية بالمخزن\",\"حد التنبيه\"");

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string barcode = EscapeCsv(row["Barcode"]?.ToString());
                    string name = EscapeCsv(row["ProductName"]?.ToString());
                    string category = EscapeCsv(row["CategoryName"]?.ToString());
                    string buyPrice = Convert.ToDecimal(row["BuyPrice"] != DBNull.Value ? row["BuyPrice"] : 0).ToString("F2", CultureInfo.InvariantCulture);
                    string sellPrice = Convert.ToDecimal(row["SellPrice"] != DBNull.Value ? row["SellPrice"] : 0).ToString("F2", CultureInfo.InvariantCulture);
                    string qty = Convert.ToInt32(row["StockQuantity"] != DBNull.Value ? row["StockQuantity"] : 0).ToString();
                    string minAlert = Convert.ToInt32(row["MinStockAlert"] != DBNull.Value ? row["MinStockAlert"] : 0).ToString();

                    sb.AppendLine($"\"{barcode}\",\"{name}\",\"{category}\",{buyPrice},{sellPrice},{qty},{minAlert}");
                }
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private static string EscapeCsv(string val)
        {
            if (string.IsNullOrEmpty(val)) return "";
            return val.Replace("\"", "\"\"");
        }

        /// <summary>
        /// Creates a genuine native OpenXML .xlsx file using standard System.IO.Compression.
        /// Zero external dependencies, 100% compatible with all versions of Microsoft Excel.
        /// </summary>
        private static void ExportToNativeXlsx(string filePath, DataTable dt, string sheetName)
        {
            if (File.Exists(filePath))
            {
                try { File.Delete(filePath); } catch { }
            }

            using (var zipStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true, Encoding.UTF8))
            {
                // 1. [Content_Types].xml
                AddZipEntry(archive, "[Content_Types].xml", @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>
  <Override PartName=""/xl/worksheets/sheet1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>
  <Override PartName=""/xl/styles.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml""/>
</Types>");

                // 2. _rels/.rels
                AddZipEntry(archive, "_rels/.rels", @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>
</Relationships>");

                // 3. xl/_rels/workbook.xml.rels
                AddZipEntry(archive, "xl/_rels/workbook.xml.rels", @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>
  <Relationship Id=""rId2"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""styles.xml""/>
</Relationships>");

                // 4. xl/workbook.xml
                string safeSheetName = EscapeXml(string.IsNullOrWhiteSpace(sheetName) ? "المنتجات" : sheetName);
                AddZipEntry(archive, "xl/workbook.xml", $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
  <sheets>
    <sheet name=""{safeSheetName}"" sheetId=""1"" r:id=""rId1""/>
  </sheets>
</workbook>");

                // 5. xl/styles.xml
                AddZipEntry(archive, "xl/styles.xml", @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<styleSheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
  <numFmts count=""2"">
    <numFmt numFmtId=""164"" formatCode=""#,##0.00""/>
    <numFmt numFmtId=""165"" formatCode=""#,##0""/>
  </numFmts>
  <fonts count=""2"">
    <font><sz val=""10.5""/><name val=""Segoe UI""/><family val=""2""/></font>
    <font><b/><sz val=""11""/><color rgb=""FFFFFFFF""/><name val=""Segoe UI""/><family val=""2""/></font>
  </fonts>
  <fills count=""3"">
    <fill><patternFill patternType=""none""/></fill>
    <fill><patternFill patternType=""gray125""/></fill>
    <fill><patternFill patternType=""solid""><fgColor rgb=""FF1E293B""/></patternFill></fill>
  </fills>
  <borders count=""2"">
    <border><left/><right/><top/><bottom/><diagonal/></border>
    <border><left/><right/><top/><bottom style=""thin""><color rgb=""FFCBD5E1""/></bottom><diagonal/></border>
  </borders>
  <cellStyleXfs count=""1"">
    <xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0""/>
  </cellStyleXfs>
  <cellXfs count=""5"">
    <xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0""/>
    <xf numFmtId=""0"" fontId=""1"" fillId=""2"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1"" applyAlignment=""1"">
      <alignment horizontal=""center"" vertical=""center""/>
    </xf>
    <xf numFmtId=""164"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0"" applyNumberFormat=""1"" applyAlignment=""1"">
      <alignment horizontal=""center"" vertical=""center""/>
    </xf>
    <xf numFmtId=""165"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0"" applyNumberFormat=""1"" applyAlignment=""1"">
      <alignment horizontal=""center"" vertical=""center""/>
    </xf>
    <xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0"" applyAlignment=""1"">
      <alignment horizontal=""right"" vertical=""center""/>
    </xf>
  </cellXfs>
</styleSheet>");

                // 6. xl/worksheets/sheet1.xml
                var sbSheet = new StringBuilder();
                sbSheet.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>");
                sbSheet.AppendLine(@"<worksheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">");
                sbSheet.AppendLine(@"  <sheetViews>");
                sbSheet.AppendLine(@"    <sheetView rightToLeft=""1"" tabSelected=""1"" workbookViewId=""0""/>");
                sbSheet.AppendLine(@"  </sheetViews>");
                sbSheet.AppendLine(@"  <sheetFormatPr defaultRowHeight=""22""/>");
                sbSheet.AppendLine(@"  <cols>");
                sbSheet.AppendLine(@"    <col min=""1"" max=""1"" width=""18"" customWidth=""1""/>"); // Barcode
                sbSheet.AppendLine(@"    <col min=""2"" max=""2"" width=""32"" customWidth=""1""/>"); // Name
                sbSheet.AppendLine(@"    <col min=""3"" max=""3"" width=""20"" customWidth=""1""/>"); // Category
                sbSheet.AppendLine(@"    <col min=""4"" max=""4"" width=""14"" customWidth=""1""/>"); // BuyPrice
                sbSheet.AppendLine(@"    <col min=""5"" max=""5"" width=""14"" customWidth=""1""/>"); // SellPrice
                sbSheet.AppendLine(@"    <col min=""6"" max=""6"" width=""12"" customWidth=""1""/>"); // Stock
                sbSheet.AppendLine(@"    <col min=""7"" max=""7"" width=""12"" customWidth=""1""/>"); // MinAlert
                sbSheet.AppendLine(@"  </cols>");
                sbSheet.AppendLine(@"  <sheetData>");

                // Row 1: Headers
                sbSheet.AppendLine(@"    <row r=""1"" ht=""28"" customHeight=""1"">");
                sbSheet.AppendLine(@"      <c r=""A1"" s=""1"" t=""inlineStr""><is><t>الباركود</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""B1"" s=""1"" t=""inlineStr""><is><t>اسم المنتج</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""C1"" s=""1"" t=""inlineStr""><is><t>القسم</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""D1"" s=""1"" t=""inlineStr""><is><t>سعر الشراء</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""E1"" s=""1"" t=""inlineStr""><is><t>سعر البيع</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""F1"" s=""1"" t=""inlineStr""><is><t>الكمية بالمخزن</t></is></c>");
                sbSheet.AppendLine(@"      <c r=""G1"" s=""1"" t=""inlineStr""><is><t>حد التنبيه</t></is></c>");
                sbSheet.AppendLine(@"    </row>");

                if (dt != null)
                {
                    int rowIdx = 2;
                    foreach (DataRow row in dt.Rows)
                    {
                        string barcode = row["Barcode"]?.ToString() ?? "";
                        string name = row["ProductName"]?.ToString() ?? "";
                        string category = row["CategoryName"]?.ToString() ?? "";
                        decimal buyPrice = Convert.ToDecimal(row["BuyPrice"] != DBNull.Value ? row["BuyPrice"] : 0);
                        decimal sellPrice = Convert.ToDecimal(row["SellPrice"] != DBNull.Value ? row["SellPrice"] : 0);
                        int qty = Convert.ToInt32(row["StockQuantity"] != DBNull.Value ? row["StockQuantity"] : 0);
                        int minAlert = Convert.ToInt32(row["MinStockAlert"] != DBNull.Value ? row["MinStockAlert"] : 0);

                        sbSheet.AppendLine($@"    <row r=""{rowIdx}"" ht=""22"" customHeight=""1"">");
                        sbSheet.AppendLine($@"      <c r=""A{rowIdx}"" t=""inlineStr""><is><t>{EscapeXml(barcode)}</t></is></c>");
                        sbSheet.AppendLine($@"      <c r=""B{rowIdx}"" s=""4"" t=""inlineStr""><is><t>{EscapeXml(name)}</t></is></c>");
                        sbSheet.AppendLine($@"      <c r=""C{rowIdx}"" t=""inlineStr""><is><t>{EscapeXml(category)}</t></is></c>");
                        sbSheet.AppendLine($@"      <c r=""D{rowIdx}"" s=""2""><v>{buyPrice.ToString("F2", CultureInfo.InvariantCulture)}</v></c>");
                        sbSheet.AppendLine($@"      <c r=""E{rowIdx}"" s=""2""><v>{sellPrice.ToString("F2", CultureInfo.InvariantCulture)}</v></c>");
                        sbSheet.AppendLine($@"      <c r=""F{rowIdx}"" s=""3""><v>{qty}</v></c>");
                        sbSheet.AppendLine($@"      <c r=""G{rowIdx}"" s=""3""><v>{minAlert}</v></c>");
                        sbSheet.AppendLine(@"    </row>");

                        rowIdx++;
                    }
                }

                sbSheet.AppendLine(@"  </sheetData>");
                sbSheet.AppendLine(@"</worksheet>");

                AddZipEntry(archive, "xl/worksheets/sheet1.xml", sbSheet.ToString());
            }
        }

        private static void AddZipEntry(ZipArchive archive, string entryName, string content)
        {
            var entry = archive.CreateEntry(entryName, CompressionLevel.Fastest);
            using (var stream = entry.Open())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(content);
            }
        }

        private static string EscapeXml(string val)
        {
            if (string.IsNullOrEmpty(val)) return "";
            return val.Replace("&", "&amp;")
                      .Replace("<", "&lt;")
                      .Replace(">", "&gt;")
                      .Replace("\"", "&quot;")
                      .Replace("'", "&apos;");
        }

        /// <summary>
        /// Generates a blank/sample template Excel XLSX file for bulk import.
        /// </summary>
        public static void GenerateSampleTemplate(string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("BuyPrice", typeof(decimal));
            dt.Columns.Add("SellPrice", typeof(decimal));
            dt.Columns.Add("StockQuantity", typeof(int));
            dt.Columns.Add("MinStockAlert", typeof(int));

            dt.Rows.Add("622100100001", "حليب جهينة كامل الدسم 1 لتر", "ألبان ومنتجات الأجبان", 35.00m, 44.00m, 80, 15);
            dt.Rows.Add("622100200001", "مياه معدنية نستله 1.5 لتر", "مياه ومشروبات وعصائر", 7.50m, 12.00m, 120, 20);
            dt.Rows.Add("", "شيبسي عائلي طماطم (باركود تلقائي)", "سناكس ومقرمشات وشيبسي", 11.50m, 15.00m, 60, 10);

            ExportProducts(filePath, dt, "نموذج استيراد المنتجات");
        }

        #endregion

        #region Import Products from Excel / CSV

        public class ImportResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public int TotalRead { get; set; }
            public int AddedCount { get; set; }
            public int UpdatedCount { get; set; }
            public int SkippedCount { get; set; }
            public List<string> Warnings { get; set; } = new List<string>();
        }

        public class ImportedRow
        {
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public decimal BuyPrice { get; set; }
            public decimal SellPrice { get; set; }
            public int StockQuantity { get; set; }
            public int MinStockAlert { get; set; }
            public int LineNumber { get; set; }
        }

        public static ImportResult ImportProducts(string filePath, bool updateExisting = true)
        {
            var result = new ImportResult();

            if (!File.Exists(filePath))
            {
                result.Success = false;
                result.Message = "الملف المحدد غير موجود.";
                return result;
            }

            try
            {
                List<ImportedRow> rows = ReadRowsFromFile(filePath);
                result.TotalRead = rows.Count;

                if (rows.Count == 0)
                {
                    result.Success = false;
                    result.Message = "لم يتم العثور على أي صفوف أو بيانات منتجات صالحة في الملف.\nيرجى التأكد من أن الملف يحتوي على أعمدة (اسم المنتج، سعر البيع أو الباركود).";
                    return result;
                }

                // Pre-load all existing categories into dictionary
                var catDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var c in DbHelper.GetAllCategories())
                {
                    if (!string.IsNullOrWhiteSpace(c.CategoryName))
                    {
                        catDict[c.CategoryName.Trim()] = c.CategoryId;
                    }
                }

                // Pre-load all existing products into dictionary (1 single query instead of N queries)
                var existingProducts = DbHelper.GetAllProductsDictionary();
                var usedBarcodes = new HashSet<string>(existingProducts.Keys, StringComparer.OrdinalIgnoreCase);

                var toInsert = new List<ProductModel>();
                var toUpdate = new List<ProductModel>();

                // In-memory barcode generation counter seed
                long barcodeSeed = DateTime.Now.Ticks % 1000000000L;

                foreach (var r in rows)
                {
                    string pName = r.ProductName?.Trim();
                    if (string.IsNullOrWhiteSpace(pName))
                    {
                        result.SkippedCount++;
                        continue;
                    }

                    // Strict protection against header/summary or binary artifacts
                    if (pName.Equals("الباركود", StringComparison.OrdinalIgnoreCase) ||
                        pName.Equals("اسم المنتج", StringComparison.OrdinalIgnoreCase) ||
                        pName.Equals("ProductName", StringComparison.OrdinalIgnoreCase) ||
                        pName.Equals("Barcode", StringComparison.OrdinalIgnoreCase) ||
                        pName.StartsWith("إجمالي", StringComparison.OrdinalIgnoreCase) ||
                        pName.StartsWith("المجموع", StringComparison.OrdinalIgnoreCase) ||
                        pName.StartsWith("تقرير", StringComparison.OrdinalIgnoreCase) ||
                        pName.StartsWith("Total", StringComparison.OrdinalIgnoreCase) ||
                        pName.Contains("PK") || pName.Contains("\0"))
                    {
                        result.SkippedCount++;
                        continue;
                    }

                    int? categoryId = null;
                    if (!string.IsNullOrWhiteSpace(r.CategoryName))
                    {
                        string cName = r.CategoryName.Trim();
                        if (catDict.TryGetValue(cName, out int existingCatId))
                        {
                            categoryId = existingCatId;
                        }
                        else
                        {
                            var catRes = DbHelper.SaveCategory(cName);
                            if (catRes.Success)
                            {
                                int newCatId = DbHelper.GetAllCategories().FirstOrDefault(x => x.CategoryName.Equals(cName, StringComparison.OrdinalIgnoreCase))?.CategoryId ?? 0;
                                if (newCatId > 0)
                                {
                                    catDict[cName] = newCatId;
                                    categoryId = newCatId;
                                }
                            }
                        }
                    }

                    string barcode = r.Barcode?.Trim();
                    if (string.IsNullOrWhiteSpace(barcode))
                    {
                        do
                        {
                            barcodeSeed++;
                            barcode = "622" + barcodeSeed.ToString("D9");
                        } while (usedBarcodes.Contains(barcode));
                        usedBarcodes.Add(barcode);
                    }

                    if (existingProducts.TryGetValue(barcode, out ProductModel existingProduct))
                    {
                        if (updateExisting)
                        {
                            existingProduct.ProductName = pName;
                            if (categoryId.HasValue) existingProduct.CategoryId = categoryId;
                            existingProduct.BuyPrice = r.BuyPrice;
                            existingProduct.SellPrice = r.SellPrice;
                            existingProduct.StockQuantity = r.StockQuantity;
                            existingProduct.MinStockAlert = r.MinStockAlert > 0 ? r.MinStockAlert : 5;

                            toUpdate.Add(existingProduct);
                        }
                        else
                        {
                            result.SkippedCount++;
                            result.Warnings.Add($"السطر {r.LineNumber}: تم تخطي المنتج '{pName}' لأن الباركود '{barcode}' مسجل مسبقاً.");
                        }
                    }
                    else
                    {
                        var newProd = new ProductModel
                        {
                            ProductId = 0,
                            Barcode = barcode,
                            ProductName = pName,
                            CategoryId = categoryId,
                            BuyPrice = r.BuyPrice,
                            SellPrice = r.SellPrice,
                            StockQuantity = r.StockQuantity,
                            MinStockAlert = r.MinStockAlert > 0 ? r.MinStockAlert : 5
                        };

                        toInsert.Add(newProd);
                        existingProducts[barcode] = newProd;
                        usedBarcodes.Add(barcode);
                    }
                }

                // Execute high-performance bulk batch insert & update
                var bulkResult = DbHelper.BulkSaveProducts(toInsert, toUpdate);
                result.AddedCount = bulkResult.InsertedCount;
                result.UpdatedCount = bulkResult.UpdatedCount;
                if (bulkResult.Errors != null && bulkResult.Errors.Count > 0)
                {
                    result.Warnings.AddRange(bulkResult.Errors);
                }

                result.Success = true;
                result.Message = $"اكتمل الاستيراد بنجاح!\n\n• تم إضافة: {result.AddedCount} صنف جديد\n• تم تحديث: {result.UpdatedCount} صنف مسجل\n• تم تخطي: {result.SkippedCount} صف";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "حدث خطأ أثناء قراءة واستيراد الملف: " + ex.Message;
                return result;
            }
        }

        private static List<ImportedRow> ReadRowsFromFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            if (IsZipFile(filePath) || ext == ".xlsx")
            {
                return ReadXlsxFile(filePath);
            }

            if (ext == ".csv" || ext == ".txt")
            {
                return ReadCsvFile(filePath);
            }

            // Fallback for XML Spreadsheet (.xml, .xls)
            var xmlRows = ReadXmlSpreadsheet(filePath);
            if (xmlRows.Count > 0) return xmlRows;

            return ReadCsvFile(filePath);
        }

        private static bool IsZipFile(string filePath)
        {
            try
            {
                byte[] buffer = new byte[4];
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Read(buffer, 0, 4) < 4) return false;
                }
                // ZIP magic header 'PK\x03\x04' or 'PK\x05\x06'
                return buffer[0] == 0x50 && buffer[1] == 0x4B;
            }
            catch
            {
                return false;
            }
        }

        private static List<ImportedRow> ReadXlsxFile(string filePath)
        {
            var list = new List<ImportedRow>();

            using (var zip = ZipFile.OpenRead(filePath))
            {
                // 1. Read Shared Strings (if workbook uses sharedStrings)
                var sharedStrings = new List<string>();
                var stringsEntry = zip.GetEntry("xl/sharedStrings.xml");
                if (stringsEntry != null)
                {
                    using (var s = stringsEntry.Open())
                    {
                        var doc = new XmlDocument();
                        doc.Load(s);
                        foreach (XmlNode si in doc.GetElementsByTagName("si"))
                        {
                            var sb = new StringBuilder();
                            foreach (XmlNode t in si.ChildNodes)
                            {
                                if (t.LocalName == "t") sb.Append(t.InnerText);
                                else if (t.LocalName == "r")
                                {
                                    foreach (XmlNode rt in t.ChildNodes)
                                    {
                                        if (rt.LocalName == "t") sb.Append(rt.InnerText);
                                    }
                                }
                            }
                            sharedStrings.Add(sb.ToString());
                        }
                    }
                }

                // 2. Locate worksheet
                var sheetEntry = zip.GetEntry("xl/worksheets/sheet1.xml") ?? zip.Entries.FirstOrDefault(e => e.FullName.StartsWith("xl/worksheets/sheet", StringComparison.OrdinalIgnoreCase));
                if (sheetEntry == null) return list;

                using (var s = sheetEntry.Open())
                {
                    var doc = new XmlDocument();
                    doc.Load(s);

                    var rowNodes = doc.GetElementsByTagName("row");
                    int headerLineIdx = -1;
                    Dictionary<string, int> colMap = null;

                    for (int i = 0; i < rowNodes.Count; i++)
                    {
                        var parts = ExtractXlsxRowValues(rowNodes[i], sharedStrings);
                        var map = MapColumns(parts);
                        if (IsValidHeaderMap(map))
                        {
                            headerLineIdx = i;
                            colMap = map;
                            break;
                        }
                    }

                    if (colMap == null)
                    {
                        // Fallback default order if no header row recognized
                        colMap = new Dictionary<string, int>
                        {
                            ["barcode"] = 0,
                            ["name"] = 1,
                            ["category"] = 2,
                            ["buyprice"] = 3,
                            ["sellprice"] = 4,
                            ["stock"] = 5,
                            ["minalert"] = 6
                        };
                        headerLineIdx = -1;
                    }

                    for (int i = headerLineIdx + 1; i < rowNodes.Count; i++)
                    {
                        var parts = ExtractXlsxRowValues(rowNodes[i], sharedStrings);
                        if (parts.Count == 0 || parts.All(string.IsNullOrWhiteSpace)) continue;

                        var row = CreateRowFromMap(parts, colMap, i + 1);
                        if (row != null && !string.IsNullOrWhiteSpace(row.ProductName))
                        {
                            list.Add(row);
                        }
                    }
                }
            }

            return list;
        }

        private static List<string> ExtractXlsxRowValues(XmlNode rowNode, List<string> sharedStrings)
        {
            var dict = new SortedDictionary<int, string>();
            int maxCol = 0;

            foreach (XmlNode c in rowNode.ChildNodes)
            {
                if (c.LocalName != "c") continue;

                string rAttr = c.Attributes?["r"]?.Value;
                int colIdx = ParseColumnIndex(rAttr);

                string tAttr = c.Attributes?["t"]?.Value;
                string cellValue = "";

                if (tAttr == "inlineStr")
                {
                    foreach (XmlNode child in c.ChildNodes)
                    {
                        if (child.LocalName == "is")
                        {
                            foreach (XmlNode isChild in child.ChildNodes)
                            {
                                if (isChild.LocalName == "t") cellValue = isChild.InnerText;
                            }
                        }
                        else if (child.LocalName == "t")
                        {
                            cellValue = child.InnerText;
                        }
                    }
                }
                else
                {
                    string rawVal = "";
                    foreach (XmlNode child in c.ChildNodes)
                    {
                        if (child.LocalName == "v") rawVal = child.InnerText;
                    }

                    if (tAttr == "s" && int.TryParse(rawVal, out int strIdx) && strIdx >= 0 && strIdx < sharedStrings.Count)
                    {
                        cellValue = sharedStrings[strIdx];
                    }
                    else
                    {
                        cellValue = rawVal;
                    }
                }

                dict[colIdx] = cellValue.Trim();
                if (colIdx > maxCol) maxCol = colIdx;
            }

            var result = new List<string>();
            for (int i = 0; i <= maxCol; i++)
            {
                result.Add(dict.ContainsKey(i) ? dict[i] : "");
            }

            return result;
        }

        private static int ParseColumnIndex(string cellRef)
        {
            if (string.IsNullOrEmpty(cellRef)) return 0;
            string colPart = Regex.Match(cellRef, "^[A-Za-z]+").Value.ToUpperInvariant();
            int col = 0;
            foreach (char ch in colPart)
            {
                col = (col * 26) + (ch - 'A' + 1);
            }
            return Math.Max(0, col - 1);
        }

        private static List<ImportedRow> ReadCsvFile(string filePath)
        {
            var list = new List<ImportedRow>();
            string[] lines = File.ReadAllLines(filePath, DetectEncoding(filePath));
            if (lines.Length == 0) return list;

            int headerLineIdx = -1;
            char delimiter = ',';
            Dictionary<string, int> colMap = null;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                int commaCount = CountOccurrences(line, ',');
                int semicolonCount = CountOccurrences(line, ';');
                int tabCount = CountOccurrences(line, '\t');

                char bestDelim = ',';
                if (semicolonCount > commaCount && semicolonCount > tabCount) bestDelim = ';';
                else if (tabCount > commaCount && tabCount > semicolonCount) bestDelim = '\t';

                var parts = ParseCsvLine(line, bestDelim);
                var map = MapColumns(parts);
                if (IsValidHeaderMap(map))
                {
                    headerLineIdx = i;
                    delimiter = bestDelim;
                    colMap = map;
                    break;
                }
            }

            if (headerLineIdx == -1 || colMap == null)
            {
                colMap = new Dictionary<string, int>
                {
                    ["barcode"] = 0,
                    ["name"] = 1,
                    ["category"] = 2,
                    ["buyprice"] = 3,
                    ["sellprice"] = 4,
                    ["stock"] = 5,
                    ["minalert"] = 6
                };
                headerLineIdx = -1;
            }

            for (int i = headerLineIdx + 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var parts = ParseCsvLine(line, delimiter);
                if (parts.Count == 0 || parts.All(string.IsNullOrWhiteSpace)) continue;

                var row = CreateRowFromMap(parts, colMap, i + 1);
                if (row != null && !string.IsNullOrWhiteSpace(row.ProductName))
                {
                    list.Add(row);
                }
            }

            return list;
        }

        private static List<ImportedRow> ReadXmlSpreadsheet(string filePath)
        {
            var list = new List<ImportedRow>();
            try
            {
                var doc = new XmlDocument();
                doc.Load(filePath);

                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

                var rows = doc.SelectNodes("//ss:Table/ss:Row", nsmgr);
                if (rows == null || rows.Count == 0) return list;

                int headerLineIdx = -1;
                Dictionary<string, int> colMap = null;

                for (int i = 0; i < rows.Count; i++)
                {
                    var rowNode = rows[i];
                    var cells = rowNode.SelectNodes("ss:Cell", nsmgr);
                    var parts = new List<string>();

                    foreach (XmlNode c in cells)
                    {
                        var data = c.SelectSingleNode("ss:Data", nsmgr);
                        parts.Add(data?.InnerText?.Trim() ?? "");
                    }

                    var map = MapColumns(parts);
                    if (IsValidHeaderMap(map))
                    {
                        headerLineIdx = i;
                        colMap = map;
                        break;
                    }
                }

                if (colMap == null) return list;

                for (int i = headerLineIdx + 1; i < rows.Count; i++)
                {
                    var rowNode = rows[i];
                    var cells = rowNode.SelectNodes("ss:Cell", nsmgr);
                    var parts = new List<string>();

                    int currentCellIdx = 0;
                    foreach (XmlNode c in cells)
                    {
                        if (c.Attributes?["ss:Index"] != null && int.TryParse(c.Attributes["ss:Index"].Value, out int explicitIdx))
                        {
                            while (currentCellIdx < explicitIdx - 1)
                            {
                                parts.Add("");
                                currentCellIdx++;
                            }
                        }

                        var data = c.SelectSingleNode("ss:Data", nsmgr);
                        parts.Add(data?.InnerText?.Trim() ?? "");
                        currentCellIdx++;
                    }

                    var row = CreateRowFromMap(parts, colMap, i + 1);
                    if (row != null && !string.IsNullOrWhiteSpace(row.ProductName))
                    {
                        list.Add(row);
                    }
                }
            }
            catch { }

            return list;
        }

        private static bool IsValidHeaderMap(Dictionary<string, int> map)
        {
            if (map == null || map.Count == 0) return false;
            if (map.ContainsKey("name") && (map.ContainsKey("barcode") || map.ContainsKey("sellprice") || map.ContainsKey("buyprice") || map.ContainsKey("category")))
            {
                return true;
            }
            return map.Count >= 3;
        }

        private static Dictionary<string, int> MapColumns(List<string> headers)
        {
            var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < headers.Count; i++)
            {
                string raw = headers[i].Trim();
                if (string.IsNullOrWhiteSpace(raw)) continue;

                if (raw.Contains("تقرير") || raw.Contains("إجمالي") || raw.Contains("المجموع") || raw.Contains("total") || raw.Contains("summary"))
                {
                    continue;
                }

                string h = raw.ToLowerInvariant().Replace(" ", "").Replace("_", "").Replace("-", "");

                if (h == "الباركود" || h == "باركود" || h == "barcode" || h == "code" || h == "كود" || h == "upc" || h == "ean" || h.Contains("باركود") || h.Contains("barcode"))
                {
                    if (!map.ContainsKey("barcode")) map["barcode"] = i;
                }
                else if (h == "اسمالمنتج" || h == "اسمالصنف" || h == "الاسم" || h == "اسم" || h == "productname" || h == "product" || h == "itemname" || h == "name" || h.Contains("اسمالمنتج") || h.Contains("اسمالصنف") || h.Contains("productname") || h.Contains("itemname"))
                {
                    if (!map.ContainsKey("name")) map["name"] = i;
                }
                else if (h == "القسم" || h == "التصنيف" || h == "الفئة" || h == "المجموعة" || h == "category" || h == "categoryname" || h.Contains("قسم") || h.Contains("تصنيف") || h.Contains("category"))
                {
                    if (!map.ContainsKey("category")) map["category"] = i;
                }
                else if (h == "سعرالشراء" || h == "سعروالتكلفة" || h == "التكلفة" || h == "سعرالتكلفة" || h == "شراء" || h == "buyprice" || h == "cost" || h == "purchaseprice" || h.Contains("شراء") || h.Contains("تكلفة") || h.Contains("buyprice") || h.Contains("cost"))
                {
                    if (!map.ContainsKey("buyprice")) map["buyprice"] = i;
                }
                else if (h == "سعرالبيع" || h == "سعرالجمهور" || h == "السعر" || h == "سعر" || h == "بيع" || h == "sellprice" || h == "price" || h.Contains("بيع") || h.Contains("جمهور") || h.Contains("sellprice") || h.Contains("price"))
                {
                    if (!map.ContainsKey("sellprice")) map["sellprice"] = i;
                }
                else if (h == "الكميةبالمخزن" || h == "الكمية" || h == "الرصيد" || h == "المخزون" || h == "stockquantity" || h == "quantity" || h == "stock" || h == "qty" || h.Contains("كمية") || h.Contains("مخزن") || h.Contains("رصيد") || h.Contains("stock") || h.Contains("qty"))
                {
                    if (!map.ContainsKey("stock")) map["stock"] = i;
                }
                else if (h == "حدالتنبيه" || h == "أدنىمخزون" || h == "الحدالأدنى" || h == "تنبيه" || h == "minstockalert" || h == "minalert" || h == "alert" || h.Contains("تنبيه") || h.Contains("alert"))
                {
                    if (!map.ContainsKey("minalert")) map["minalert"] = i;
                }
            }

            return map;
        }

        private static ImportedRow CreateRowFromMap(List<string> parts, Dictionary<string, int> map, int lineNum)
        {
            string GetVal(string key)
            {
                if (map.TryGetValue(key, out int idx) && idx >= 0 && idx < parts.Count)
                {
                    return parts[idx]?.Trim() ?? "";
                }
                return "";
            }

            string name = GetVal("name");
            if (string.IsNullOrWhiteSpace(name)) return null;

            if (name == "الباركود" || name == "اسم المنتج" || name == "ProductName" || name == "Barcode" ||
                name.StartsWith("إجمالي") || name.StartsWith("المجموع") || name.StartsWith("تقرير") || name.StartsWith("Total") ||
                name.Contains("PK") || name.Contains("\0"))
            {
                return null;
            }

            string barcode = GetVal("barcode");
            string category = GetVal("category");

            decimal.TryParse(CleanNumericString(GetVal("buyprice")), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal buyPrice);
            decimal.TryParse(CleanNumericString(GetVal("sellprice")), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sellPrice);
            int.TryParse(CleanNumericString(GetVal("stock")), out int stock);
            int.TryParse(CleanNumericString(GetVal("minalert")), out int minAlert);

            if (minAlert <= 0) minAlert = 5;

            return new ImportedRow
            {
                LineNumber = lineNum,
                Barcode = barcode,
                ProductName = name,
                CategoryName = category,
                BuyPrice = buyPrice,
                SellPrice = sellPrice,
                StockQuantity = stock,
                MinStockAlert = minAlert
            };
        }

        private static string CleanNumericString(string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return "0";
            return val.Replace("ج.م", "").Replace("EGP", "").Replace("$", "").Replace(",", "").Trim();
        }

        private static List<string> ParseCsvLine(string line, char delimiter)
        {
            var list = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        sb.Append('\"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == delimiter && !inQuotes)
                {
                    list.Add(sb.ToString().Trim());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            list.Add(sb.ToString().Trim());
            return list;
        }

        private static int CountOccurrences(string text, char c)
        {
            int count = 0;
            foreach (char ch in text) if (ch == c) count++;
            return count;
        }

        private static Encoding DetectEncoding(string filePath)
        {
            byte[] bom = new byte[4];
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode;
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode;

            return Encoding.UTF8;
        }

        #endregion
    }
}

