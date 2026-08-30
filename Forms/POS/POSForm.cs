using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class POSForm : Form
    {
        private readonly UserModel _currentUser;
        private List<CartItemModel> _cartItems = new List<CartItemModel>();
        private DataTable _cartTable;
        private SystemSettingsModel _sysSettings;
        private Timer _searchDebounceTimer;
        private bool _isCheckingOut = false;
        private ShiftModel _activeShift = null;

        public bool HasActiveShift => _activeShift != null;

        public POSForm(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            _searchDebounceTimer = new Timer();
            _searchDebounceTimer.Interval = 250;
            _searchDebounceTimer.Tick += OnSearchDebounceTick;
        }

        private async void POSForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblFinalTotalVal.Font = FontManager.GetBold(18f);
            lblFinalTotalTitle.Font = FontManager.GetBold(11.5f);
            UIStyler.StyleSuccessButton(btnCheckout, "إتمام وطباعة الفاتورة [F12]");
            UIStyler.StyleDangerButton(btnClearCart, "تفريغ السلة");
            UIStyler.StyleDataGrid(dgvProductsCatalog);
            UIStyler.StyleDataGrid(dgvCart);
            InitCartTable();
            cmbPaymentMethod.SelectedIndex = 0; // نقدي

            LoadSettings();
            UpdateShiftStatus();
            await LoadCategoriesAsync();
            await LoadProductsAsync();
            if (HasActiveShift || (_currentUser != null && _currentUser.IsAdmin))
            {
                txtBarcodeScan.Focus();
            }
        }

        public async void RefreshData()
        {
            LoadSettings();
            UpdateShiftStatus();
            await LoadCategoriesAsync();
            await LoadProductsAsync();
            CalculateTotals();
            if (HasActiveShift || (_currentUser != null && _currentUser.IsAdmin))
            {
                txtBarcodeScan.Focus();
            }
        }

        public void UpdateShiftStatus()
        {
            if (_currentUser == null) return;

            try
            {
                _activeShift = DbHelper.GetActiveShift(_currentUser.UserId);

                if (_currentUser.IsAdmin)
                {
                    // مدير النظام يمتلك صلاحية كاملة دائماً
                    txtBarcodeScan.Enabled = true;
                    btnCheckout.Enabled = true;
                    btnClearCart.Enabled = true;
                    dgvProductsCatalog.Enabled = true;

                    if (_activeShift != null)
                    {
                        TimeSpan elapsed = DateTime.Now - _activeShift.ClockInTime;
                        pnlShiftBanner.BackColor = Color.FromArgb(240, 253, 244);
                        pnlShiftBanner.ForeColor = Color.FromArgb(22, 101, 52);
                        lblShiftBannerIcon.Text = "";
                        lblShiftBannerText.Text = $"الوردية مفتوحة للمدير: {_currentUser.FullName}  |  وقت الحضور: {_activeShift.ClockInTime:hh:mm tt}  (المدة: {(int)elapsed.TotalHours}:{elapsed.Minutes:D2} ساعة)";
                        lblShiftBannerText.ForeColor = Color.FromArgb(22, 101, 52);
                        UIStyler.StyleDangerButton(btnShiftBannerAction, "إنهاء الوردية");
                    }
                    else
                    {
                        pnlShiftBanner.BackColor = Color.FromArgb(241, 245, 249);
                        pnlShiftBanner.ForeColor = Color.FromArgb(71, 85, 105);
                        lblShiftBannerIcon.Text = "";
                        lblShiftBannerText.Text = $"حساب مدير النظام ({_currentUser.FullName}) - الصلاحيات كاملة وشاشة البيع مفعلة دائماً بدون تقييد.";
                        lblShiftBannerText.ForeColor = Color.FromArgb(71, 85, 105);
                        UIStyler.StyleSecondaryButton(btnShiftBannerAction, "بدء وردية (اختياري)");
                    }
                    return;
                }

                // بقية المستخدمين (كاشير، موظف مبيعات...)
                if (_activeShift != null)
                {
                    TimeSpan elapsed = DateTime.Now - _activeShift.ClockInTime;
                    pnlShiftBanner.BackColor = Color.FromArgb(240, 253, 244);
                    pnlShiftBanner.ForeColor = Color.FromArgb(22, 101, 52);
                    lblShiftBannerIcon.Text = "";
                    lblShiftBannerText.Text = $"الوردية مفتوحة للموظف: {_currentUser.FullName}  |  وقت الحضور: {_activeShift.ClockInTime:hh:mm tt}  (المدة: {(int)elapsed.TotalHours}:{elapsed.Minutes:D2} ساعة)";
                    lblShiftBannerText.ForeColor = Color.FromArgb(22, 101, 52);
                    UIStyler.StyleDangerButton(btnShiftBannerAction, "إنهاء الوردية");

                    txtBarcodeScan.Enabled = true;
                    btnCheckout.Enabled = true;
                    btnClearCart.Enabled = true;
                    dgvProductsCatalog.Enabled = true;
                }
                else
                {
                    pnlShiftBanner.BackColor = Color.FromArgb(254, 242, 242);
                    pnlShiftBanner.ForeColor = Color.FromArgb(153, 27, 27);
                    lblShiftBannerIcon.Text = "";
                    lblShiftBannerText.Text = "تنبيه: يجب تسجيل بدء وردية العمل أولاً للتمكن من استخدام نقطة البيع (POS) وإجراء عمليات البيع والباركود.";
                    lblShiftBannerText.ForeColor = Color.FromArgb(153, 27, 27);
                    UIStyler.StyleSuccessButton(btnShiftBannerAction, "بدء وردية العمل الآن");

                    txtBarcodeScan.Enabled = false;
                    btnCheckout.Enabled = false;
                    btnClearCart.Enabled = false;
                }
            }
            catch
            {
                _activeShift = null;
            }
        }

        public bool EnsureActiveShift(bool promptIfMissing = true)
        {
            // استثناء مدير النظام من أي قيد
            if (_currentUser != null && _currentUser.IsAdmin)
            {
                return true;
            }

            UpdateShiftStatus();
            if (HasActiveShift) return true;

            if (promptIfMissing)
            {
                using (var startForm = new StartShiftModalForm(_currentUser))
                {
                    if (startForm.ShowDialog(this) == DialogResult.OK)
                    {
                        UpdateShiftStatus();
                        txtBarcodeScan.Focus();
                        return HasActiveShift;
                    }
                }
            }

            return false;
        }

        private void btnShiftBannerAction_Click(object sender, EventArgs e)
        {
            if (!HasActiveShift)
            {
                using (var startForm = new StartShiftModalForm(_currentUser))
                {
                    if (startForm.ShowDialog(this) == DialogResult.OK)
                    {
                        UpdateShiftStatus();
                        txtBarcodeScan.Focus();
                    }
                }
            }
            else
            {
                var confirmResult = MessageBox.Show(
                    $"هل أنت متأكد من رغبتك في تسجيل انصراف وإنهاء الوردية الحالية للموظف '{_currentUser?.FullName}'؟\nسيتم إغلاق الوردية الحالية وحساب إجمالي الساعات.",
                    "تأكيد إنهاء الوردية",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    var result = DbHelper.ClockOut(_currentUser.UserId);
                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "تم تسجيل الانصراف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCart();
                        UpdateShiftStatus();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void LoadSettings()
        {
            try
            {
                _sysSettings = DbHelper.GetSystemSettings();
                if (_sysSettings != null && lblVat != null)
                {
                    lblVat.Text = $"الضريبة ({_sysSettings.VatRate:0.##}%):";
                }
            }
            catch { }
        }

        #region Cashier & POS Sales Functions

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await DbHelper.GetAllCategoriesAsync();
                categories.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "جميع الأقسام" });

                cmbCategoryFilter.DataSource = null;
                cmbCategoryFilter.DataSource = categories;
                cmbCategoryFilter.DisplayMember = "CategoryName";
                cmbCategoryFilter.ValueMember = "CategoryId";
            }
            catch { }
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                string search = txtSearchProduct.Text.Trim();
                int? catId = null;
                if (cmbCategoryFilter.SelectedValue != null && int.TryParse(cmbCategoryFilter.SelectedValue.ToString(), out int id) && id > 0)
                {
                    catId = id;
                }

                DataTable dt = await DbHelper.GetAllProductsDataTableAsync(search, catId, false);
                dgvProductsCatalog.DataSource = dt;
                FormatCatalogGrid();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading catalog: " + ex.Message);
            }
        }

        private void FormatCatalogGrid()
        {
            if (dgvProductsCatalog.Columns.Count == 0) return;

            dgvProductsCatalog.ScrollBars = ScrollBars.Both;
            dgvProductsCatalog.ColumnHeadersHeight = 40;
            dgvProductsCatalog.RowTemplate.Height = 36;
            dgvProductsCatalog.EnableHeadersVisualStyles = false;

            dgvProductsCatalog.HideColumn("ProductId");
            dgvProductsCatalog.HideColumn("CategoryId");
            dgvProductsCatalog.HideColumn("BuyPrice");
            dgvProductsCatalog.HideColumn("MinStockAlert");
            dgvProductsCatalog.HideColumn("CreatedAt");
            dgvProductsCatalog.HideColumn("IsLowStock");

            dgvProductsCatalog.ConfigureCenterColumn("Barcode", "الباركود", fillWeight: 90, minWidth: 95);
            dgvProductsCatalog.ConfigureTextColumn("ProductName", "اسم المنتج", fillWeight: 165, minWidth: 135);
            dgvProductsCatalog.ConfigureTextColumn("CategoryName", "القسم", fillWeight: 85, minWidth: 85);
            dgvProductsCatalog.ConfigureCurrencyColumn("SellPrice", "السعر", fillWeight: 70, minWidth: 65);
            dgvProductsCatalog.ConfigureNumericColumn("StockQuantity", "المتاح", fillWeight: 55, minWidth: 55);
            dgvProductsCatalog.ConfigureButtonColumn("colAdd", "إضافة", "+", fillWeight: 45, minWidth: 48);
        }

        private void InitCartTable()
        {
            _cartTable = new DataTable();
            _cartTable.Columns.Add("ProductId", typeof(int));
            _cartTable.Columns.Add("Barcode", typeof(string));
            _cartTable.Columns.Add("ProductName", typeof(string));
            _cartTable.Columns.Add("UnitPrice", typeof(decimal));
            _cartTable.Columns.Add("Quantity", typeof(int));
            _cartTable.Columns.Add("LineTotal", typeof(decimal));
            _cartTable.Columns.Add("AvailableStock", typeof(int));

            dgvCart.DataSource = _cartTable;
            FormatCartGrid();
        }

        private void FormatCartGrid()
        {
            if (dgvCart.Columns.Count == 0) return;

            dgvCart.ScrollBars = ScrollBars.Both;
            dgvCart.ColumnHeadersHeight = 40;
            dgvCart.RowTemplate.Height = 36;
            dgvCart.EnableHeadersVisualStyles = false;

            dgvCart.HideColumn("ProductId");
            dgvCart.HideColumn("AvailableStock");

            var colB = dgvCart.ConfigureCenterColumn("Barcode", "الباركود", fillWeight: 95, minWidth: 95);
            if (colB != null) colB.ReadOnly = true;

            var colP = dgvCart.ConfigureTextColumn("ProductName", "اسم الصنف", fillWeight: 180, minWidth: 140);
            if (colP != null) colP.ReadOnly = true;

            var colU = dgvCart.ConfigureCurrencyColumn("UnitPrice", "السعر", fillWeight: 70, minWidth: 65);
            if (colU != null) colU.ReadOnly = true;

            dgvCart.ConfigureButtonColumn("colMinus", "-", "-", fillWeight: 35, minWidth: 35);

            var colQ = dgvCart.ConfigureNumericColumn("Quantity", "الكمية", fillWeight: 55, minWidth: 50);
            if (colQ != null) colQ.ReadOnly = false;

            dgvCart.ConfigureButtonColumn("colPlus", "+", "+", fillWeight: 35, minWidth: 35);

            var colT = dgvCart.ConfigureCurrencyColumn("LineTotal", "الإجمالي", fillWeight: 75, minWidth: 70);
            if (colT != null) colT.ReadOnly = true;

            dgvCart.ConfigureButtonColumn("colDelete", "حذف", "حذف", fillWeight: 45, minWidth: 45, textColor: Color.FromArgb(220, 38, 38));
        }

        private void AddProductToCart(ProductModel product, int quantityToAdd = 1)
        {
            if (product == null) return;

            if (product.StockQuantity <= 0)
            {
                MessageBox.Show($"عذراً، المنتج '{product.ProductName}' نفد من المخزون بالكامل (الرصيد: 0).", "نفاد المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingItem = _cartItems.Find(x => x.ProductId == product.ProductId);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantityToAdd > product.StockQuantity)
                {
                    MessageBox.Show($"لا يمكن إضافة المزيد. الكمية المتاحة في المخزن هي ({product.StockQuantity}) فقط.", "تنبيه المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingItem.Quantity += quantityToAdd;
            }
            else
            {
                if (quantityToAdd > product.StockQuantity)
                {
                    MessageBox.Show($"الكمية المطلوبة أكبر من المخزون المتاح ({product.StockQuantity}).", "تنبيه المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _cartItems.Add(new CartItemModel
                {
                    ProductId = product.ProductId,
                    Barcode = product.Barcode,
                    ProductName = product.ProductName,
                    UnitPrice = product.SellPrice,
                    Quantity = quantityToAdd,
                    AvailableStock = product.StockQuantity
                });
            }

            SyncCartTable();
            txtBarcodeScan.Clear();
            txtBarcodeScan.Focus();
        }

        private void SyncCartTable()
        {
            _cartTable.BeginLoadData();
            _cartTable.Rows.Clear();
            foreach (var item in _cartItems)
            {
                _cartTable.Rows.Add(
                    item.ProductId,
                    item.Barcode,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity,
                    item.LineTotal,
                    item.AvailableStock
                );
            }
            _cartTable.EndLoadData();

            CalculateTotals();
        }

        private bool _isUpdatingTotals = false;

        private void CalculateTotals(bool autoSyncCashPaid = true)
        {
            if (_isUpdatingTotals) return;

            try
            {
                _isUpdatingTotals = true;

                if (_sysSettings == null)
                {
                    _sysSettings = DbHelper.GetSystemSettings();
                }

                string curr = !string.IsNullOrWhiteSpace(_sysSettings?.CurrencySymbol) ? _sysSettings.CurrencySymbol : "ج.م";
                decimal vatRate = _sysSettings?.VatRate ?? 0.00m;

                decimal subtotal = 0;
                foreach (var item in _cartItems)
                {
                    subtotal += item.LineTotal;
                }

                decimal discount = numDiscount.Value;
                decimal taxableBase = Math.Max(0, subtotal - discount);
                decimal vatAmount = (vatRate > 0) ? Math.Round(taxableBase * (vatRate / 100m), 2) : 0.00m;
                decimal finalAmount = taxableBase + vatAmount;

                lblSubtotalVal.Text = $"{subtotal:N2} {curr}";
                if (lblVat != null)
                    lblVat.Text = $"الضريبة ({vatRate:0.##}%):";
                if (lblVatVal != null)
                    lblVatVal.Text = $"{vatAmount:N2} {curr}";
                lblFinalTotalVal.Text = $"{finalAmount:N2} {curr}";

                if (autoSyncCashPaid)
                {
                    numCashPaid.Value = finalAmount;
                }

                decimal change = Math.Max(0, numCashPaid.Value - finalAmount);
                lblChangeDueVal.Text = $"{change:N2} {curr}";
            }
            finally
            {
                _isUpdatingTotals = false;
            }
        }

        private async void txtBarcodeScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!EnsureActiveShift(true)) return;

                string barcode = txtBarcodeScan.Text.Trim();
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    ProductModel product = await DbHelper.GetProductByBarcodeAsync(barcode);
                    if (product != null)
                    {
                        AddProductToCart(product, 1);
                    }
                    else
                    {
                        MessageBox.Show($"لم يتم العثور على أي منتج بالباركود: '{barcode}'", "صنف غير مسجل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarcodeScan.SelectAll();
                    }
                }
            }
        }

        private void dgvProductsCatalog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductsCatalog.Columns[e.ColumnIndex].Name == "colAdd")
            {
                if (!EnsureActiveShift(true)) return;
                AddSelectedCatalogProduct(e.RowIndex);
            }
        }

        private void dgvProductsCatalog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!EnsureActiveShift(true)) return;
                AddSelectedCatalogProduct(e.RowIndex);
            }
        }

        private void AddSelectedCatalogProduct(int rowIndex)
        {
            if (!EnsureActiveShift(true)) return;
            if (rowIndex < 0 || rowIndex >= dgvProductsCatalog.Rows.Count) return;

            var row = dgvProductsCatalog.Rows[rowIndex];
            int prodId = Convert.ToInt32(row.Cells["ProductId"].Value);
            string barcode = row.Cells["Barcode"].Value?.ToString() ?? "";
            string pName = row.Cells["ProductName"].Value?.ToString() ?? "";
            decimal sellPrice = Convert.ToDecimal(row.Cells["SellPrice"].Value);
            int stockQty = Convert.ToInt32(row.Cells["StockQuantity"].Value);

            ProductModel product = new ProductModel
            {
                ProductId = prodId,
                Barcode = barcode,
                ProductName = pName,
                SellPrice = sellPrice,
                StockQuantity = stockQty
            };

            AddProductToCart(product, 1);
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _cartItems.Count) return;

            string colName = dgvCart.Columns[e.ColumnIndex].Name;
            var item = _cartItems[e.RowIndex];

            if (colName == "colPlus")
            {
                if (item.Quantity + 1 > item.AvailableStock)
                {
                    MessageBox.Show($"الكمية المتاحة في المخزن هي ({item.AvailableStock}) فقط.", "تنبيه المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                item.Quantity++;
                SyncCartTable();
            }
            else if (colName == "colMinus")
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    SyncCartTable();
                }
                else
                {
                    _cartItems.RemoveAt(e.RowIndex);
                    SyncCartTable();
                }
            }
            else if (colName == "colDelete")
            {
                _cartItems.RemoveAt(e.RowIndex);
                SyncCartTable();
            }
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _cartItems.Count && dgvCart.Columns[e.ColumnIndex].Name == "Quantity")
            {
                var row = dgvCart.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int newQty))
                {
                    var item = _cartItems[e.RowIndex];
                    if (newQty <= 0)
                    {
                        _cartItems.RemoveAt(e.RowIndex);
                    }
                    else if (newQty > item.AvailableStock)
                    {
                        MessageBox.Show($"الكمية المتاحة في المخزن هي ({item.AvailableStock}) فقط.", "تنبيه المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        item.Quantity = item.AvailableStock;
                    }
                    else
                    {
                        item.Quantity = newQty;
                    }
                    SyncCartTable();
                }
            }
        }

        private void numDiscount_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotals(autoSyncCashPaid: true);
        }

        private void numCashPaid_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotals(autoSyncCashPaid: false);
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void OnSearchDebounceTick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            await LoadProductsAsync();
        }

        private async void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0) return;

            var res = MessageBox.Show("هل أنت متأكد من تفريغ سلة المشتريات بالكامل؟", "تفريغ السلة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                ClearCart();
            }
        }

        private void ClearCart()
        {
            _cartItems.Clear();
            SyncCartTable();
            numDiscount.Value = 0;
            numCashPaid.Value = 0;
            txtBarcodeScan.Focus();
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (!EnsureActiveShift(true)) return;
            await ProcessCheckoutAsync();
        }

        private async Task ProcessCheckoutAsync()
        {
            if (_isCheckingOut) return;
            if (!EnsureActiveShift(true)) return;

            if (_cartItems.Count == 0)
            {
                MessageBox.Show("سلة المشتريات فارغة! يرجى مسح أو اختيار منتجات أولاً.", "سلة فارغة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcodeScan.Focus();
                return;
            }

            if (_sysSettings == null)
            {
                _sysSettings = DbHelper.GetSystemSettings();
            }

            string curr = !string.IsNullOrWhiteSpace(_sysSettings?.CurrencySymbol) ? _sysSettings.CurrencySymbol : "ج.م";
            decimal vatRate = _sysSettings?.VatRate ?? 0.00m;

            decimal subtotal = 0;
            foreach (var item in _cartItems) subtotal += item.LineTotal;

            decimal discount = numDiscount.Value;
            decimal taxableBase = Math.Max(0, subtotal - discount);
            decimal vatAmount = (vatRate > 0) ? Math.Round(taxableBase * (vatRate / 100m), 2) : 0.00m;
            decimal finalAmount = taxableBase + vatAmount;
            decimal paidAmount = numCashPaid.Value;

            if (paidAmount < finalAmount)
            {
                MessageBox.Show($"المبلغ المدفوع ({paidAmount:N2} {curr}) أقل من إجمالي الفاتورة المطلوب ({finalAmount:N2} {curr}).", "المبلغ المدفوع غير كافٍ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCashPaid.Focus();
                return;
            }

            decimal change = paidAmount - finalAmount;
            string paymentMethod = cmbPaymentMethod.SelectedItem?.ToString() ?? "نقدي";

            SaleModel sale = new SaleModel
            {
                UserId = _currentUser?.UserId,
                CashierName = _currentUser?.FullName ?? "مدير النظام",
                SaleDate = DateTime.Now,
                TotalAmount = subtotal,
                Discount = discount,
                TaxAmount = vatAmount,
                FinalAmount = finalAmount,
                PaidAmount = paidAmount,
                ChangeAmount = change,
                PaymentMethod = paymentMethod
            };

            try
            {
                _isCheckingOut = true;
                btnCheckout.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await DbHelper.ProcessSaleTransactionAsync(sale, _cartItems);

                if (result.Success)
                {
                    sale.SaleId = result.SaleId;
                    var sysSettings = _sysSettings ?? DbHelper.GetSystemSettings();

                    string msg = $"تم إتمام الفاتورة بنجاح!\n\nرقم الفاتورة: #{result.SaleId:D5}\nالمجموع: {subtotal:N2} {curr}";
                    if (discount > 0) msg += $"\nالخصم: {discount:N2} {curr}";
                    if (vatAmount > 0) msg += $"\nالضريبة ({vatRate:0.##}%): {vatAmount:N2} {curr}";
                    msg += $"\nالإجمالي النهائي: {finalAmount:N2} {curr}\nالمدفوع: {paidAmount:N2} {curr}\nالمتبقي للعميل: {change:N2} {curr}";

                    MessageBox.Show(msg, "عملية بيع ناجحة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // معاينة وطباعة الفاتورة الحرارية 80 مم
                    if (sysSettings.AutoPrintOnSale)
                    {
                        ReceiptPrinter.PrintReceipt(sale, new List<CartItemModel>(_cartItems), previewFirst: sysSettings.EnablePrintPreview);
                    }
                    else
                    {
                        var printConfirm = MessageBox.Show("هل ترغب في طباعة إيصال الفاتورة الحراري (80mm)؟", "طباعة الفاتورة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (printConfirm == DialogResult.Yes)
                        {
                            ReceiptPrinter.PrintReceipt(sale, new List<CartItemModel>(_cartItems), previewFirst: sysSettings.EnablePrintPreview);
                        }
                    }

                    ClearCart();
                    await LoadProductsAsync(); // تحديث أرقام المخزون في الكتالوج
                }
                else
                {
                    MessageBox.Show(result.Message, "فشل عملية البيع", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isCheckingOut = false;
                btnCheckout.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void POSForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 || e.KeyCode == Keys.F5)
            {
                e.SuppressKeyPress = true;
                if (!EnsureActiveShift(true)) return;
                await ProcessCheckoutAsync();
            }
            else if (e.KeyCode == Keys.F4)
            {
                e.SuppressKeyPress = true;
                btnClearCart_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                if (!EnsureActiveShift(true)) return;
                txtBarcodeScan.Focus();
                txtBarcodeScan.SelectAll();
            }
        }

        #endregion

        private void lblBarcodeTitle_Click(object sender, EventArgs e)
        {
        }
    }
}
