using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace POS
{
    public partial class BarcodePrintModalForm : Form
    {
        private readonly int _initialProductId;
        private ProductModel _selectedProduct;
        private List<ProductModel> _allProducts = new List<ProductModel>();
        private SystemSettingsModel _sysSettings;
        private bool _isInitializing = true;

        public BarcodePrintModalForm(int productId = 0)
        {
            _initialProductId = productId;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public BarcodePrintModalForm(ProductModel product) : this(product?.ProductId ?? 0)
        {
            _selectedProduct = product;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UIStyler.CenterFormOnScreen(this);
        }

        private void BarcodePrintModalForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.CenterFormOnScreen(this);

            lblHeaderTitle.Font = FontManager.GetBold(12f);
            lblHeaderSubtitle.Font = FontManager.GetRegular(9f);
            lblPreviewTitle.Font = FontManager.GetBold(9.5f);

            UIStyler.StyleSuccessButton(btnPrintDirect, "طباعة الآن");
            UIStyler.StylePrimaryButton(btnPrintPreview, "معاينة الطباعة");
            UIStyler.StyleSecondaryButton(btnExportImage, "تصدير كصورة PNG");
            UIStyler.StyleSecondaryButton(btnClose, "إغلاق");

            _sysSettings = DbHelper.GetSystemSettings() ?? new SystemSettingsModel();
            txtStoreName.Text = !string.IsNullOrWhiteSpace(_sysSettings.StoreName) ? _sysSettings.StoreName : "متجر نقاط البيع";

            LoadPrinters();
            LoadPresets();
            LoadProducts();

            _isInitializing = false;
            UpdatePreview();
        }

        private void LoadPrinters()
        {
            cmbPrinters.Items.Clear();
            string defaultPrinter = "";
            try
            {
                PrinterSettings settings = new PrinterSettings();
                defaultPrinter = settings.PrinterName;

                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    cmbPrinters.Items.Add(printer);
                }
            }
            catch { }

            if (cmbPrinters.Items.Count == 0)
            {
                cmbPrinters.Items.Add("الطابعة الافتراضية للويندوز");
            }

            if (!string.IsNullOrEmpty(defaultPrinter) && cmbPrinters.Items.Contains(defaultPrinter))
            {
                cmbPrinters.SelectedItem = defaultPrinter;
            }
            else if (cmbPrinters.Items.Count > 0)
            {
                cmbPrinters.SelectedIndex = 0;
            }
        }

        private void LoadPresets()
        {
            cmbPresetSize.Items.Clear();
            cmbPresetSize.Items.Add("ملصق حراري 38 × 25 مم (الافتراضي)");
            cmbPresetSize.Items.Add("ملصق حراري 40 × 30 مم");
            cmbPresetSize.Items.Add("ملصق حراري 50 × 25 مم");
            cmbPresetSize.Items.Add("ملصق حراري 50 × 30 مم");
            cmbPresetSize.Items.Add("ملصق حراري 60 × 40 مم");
            cmbPresetSize.Items.Add("ورق ملصقات A4 (3 أعمدة × 8 صفوف = 24 ملصق)");
            cmbPresetSize.Items.Add("ورق ملصقات A4 (4 أعمدة × 10 صفوف = 40 ملصق)");
            cmbPresetSize.Items.Add("مقاس مخصص (Custom)");
            cmbPresetSize.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            try
            {
                var dt = DbHelper.GetAllProductsDataTable("", null, false);
                _allProducts = new List<ProductModel>();

                int selectedIndex = 0;
                int idx = 0;

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    var p = new ProductModel
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        Barcode = row["Barcode"]?.ToString() ?? "",
                        ProductName = row["ProductName"]?.ToString() ?? "",
                        SellPrice = Convert.ToDecimal(row["SellPrice"]),
                        StockQuantity = Convert.ToInt32(row["StockQuantity"])
                    };
                    _allProducts.Add(p);

                    if (_initialProductId > 0 && p.ProductId == _initialProductId)
                    {
                        selectedIndex = idx;
                        _selectedProduct = p;
                    }
                    idx++;
                }

                cmbProducts.DataSource = null;
                cmbProducts.DisplayMember = "DisplayName";
                cmbProducts.ValueMember = "ProductId";

                var displayList = new List<object>();
                foreach (var p in _allProducts)
                {
                    displayList.Add(new
                    {
                        p.ProductId,
                        DisplayName = $"{p.ProductName} | باركود: {p.Barcode} | السعر: {p.SellPrice:N2}"
                    });
                }

                cmbProducts.DataSource = displayList;
                if (displayList.Count > 0)
                {
                    cmbProducts.SelectedIndex = selectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل قائمة المنتجات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BarcodePrintOptions GetCurrentOptions()
        {
            BarcodePrintOptions options = new BarcodePrintOptions
            {
                StoreName = txtStoreName.Text.Trim(),
                ShowStoreName = chkShowStoreName.Checked,
                ShowProductName = chkShowProductName.Checked,
                ShowPrice = chkShowPrice.Checked,
                ShowBarcodeText = chkShowBarcodeText.Checked,
                CurrencySymbol = !string.IsNullOrWhiteSpace(_sysSettings?.CurrencySymbol) ? _sysSettings.CurrencySymbol : "ج.م",
                Copies = (int)numCopies.Value,
                PrinterName = cmbPrinters.SelectedItem?.ToString() ?? ""
            };

            int presetIdx = cmbPresetSize.SelectedIndex;
            switch (presetIdx)
            {
                case 0: // 38 x 25
                    options.LabelWidthMm = 38f;
                    options.LabelHeightMm = 25f;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
                case 1: // 40 x 30
                    options.LabelWidthMm = 40f;
                    options.LabelHeightMm = 30f;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
                case 2: // 50 x 25
                    options.LabelWidthMm = 50f;
                    options.LabelHeightMm = 25f;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
                case 3: // 50 x 30
                    options.LabelWidthMm = 50f;
                    options.LabelHeightMm = 30f;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
                case 4: // 60 x 40
                    options.LabelWidthMm = 60f;
                    options.LabelHeightMm = 40f;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
                case 5: // A4 (3x8)
                    options.LabelWidthMm = 65f;
                    options.LabelHeightMm = 33f;
                    options.PrintMode = BarcodePrintMode.A4Sheet;
                    options.SheetColumns = 3;
                    options.SheetRows = 8;
                    break;
                case 6: // A4 (4x10)
                    options.LabelWidthMm = 48f;
                    options.LabelHeightMm = 26f;
                    options.PrintMode = BarcodePrintMode.A4Sheet;
                    options.SheetColumns = 4;
                    options.SheetRows = 10;
                    break;
                case 7: // Custom
                    options.LabelWidthMm = (float)numCustomW.Value;
                    options.LabelHeightMm = (float)numCustomH.Value;
                    options.PrintMode = BarcodePrintMode.ThermalRoll;
                    break;
            }

            return options;
        }

        private void UpdatePreview()
        {
            if (_isInitializing || _selectedProduct == null) return;

            try
            {
                var options = GetCurrentOptions();

                int containerW = pbPreview.ClientSize.Width > 0 ? pbPreview.ClientSize.Width : 320;
                int containerH = pbPreview.ClientSize.Height > 0 ? pbPreview.ClientSize.Height : 360;

                // Aspect ratio calculation to fit inside pbPreview
                float labelAspect = options.LabelWidthMm / options.LabelHeightMm;
                int drawW = containerW - 20;
                int drawH = (int)(drawW / labelAspect);

                if (drawH > containerH - 20)
                {
                    drawH = containerH - 20;
                    drawW = (int)(drawH * labelAspect);
                }

                drawW = Math.Max(120, drawW);
                drawH = Math.Max(80, drawH);

                var oldImg = pbPreview.Image;
                pbPreview.Image = BarcodePrinter.GenerateLabelPreviewBitmap(_selectedProduct, options, drawW, drawH);
                oldImg?.Dispose();
            }
            catch { }
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex >= 0 && cmbProducts.SelectedIndex < _allProducts.Count)
            {
                _selectedProduct = _allProducts[cmbProducts.SelectedIndex];
                UpdatePreview();
            }
        }

        private void cmbPresetSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = cmbPresetSize.SelectedIndex == 7;
            pnlCustomDimensions.Visible = isCustom;
            UpdatePreview();
        }

        private void OnOptionChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void btnPrintDirect_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("يرجى اختيار صنف أولاً للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var options = GetCurrentOptions();
                BarcodePrinter.PrintLabels(_selectedProduct, options, previewFirst: false);
                MessageBox.Show($"تم إرسال أمر طباعة {options.Copies} ملصق إلى الطابعة بنجاح.", "تمت الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء إرسال أمر الطباعة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("يرجى اختيار صنف أولاً للمعاينة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var options = GetCurrentOptions();
                BarcodePrinter.PrintLabels(_selectedProduct, options, previewFirst: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء فتح معاينة الطباعة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportImage_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("يرجى اختيار صنف أولاً للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var options = GetCurrentOptions();
                string savedPath = BarcodePrinter.PreviewLabelAsImage(_selectedProduct, options, openAfterSave: true);
                if (!string.IsNullOrEmpty(savedPath))
                {
                    MessageBox.Show($"تم حفظ صورة الملصق بنجاح على سطح المكتب:\n{savedPath}", "تم التصدير بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تصدير الصورة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
