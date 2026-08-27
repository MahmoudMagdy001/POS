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
            lblFinalTotalVal.Font = FontManager.GetBold(20f);
            lblFinalTotalTitle.Font = FontManager.GetBold(12f);
            UIStyler.StyleSuccessButton(btnCheckout, "💳 إتمام وطباعة الفاتورة [F12]");
            UIStyler.StyleDangerButton(btnClearCart, "🗑️ تفريغ السلة");
            UIStyler.StyleDataGrid(dgvProductsCatalog);
            UIStyler.StyleDataGrid(dgvCart);
            InitCartTable();
            cmbPaymentMethod.SelectedIndex = 0; // نقدي

            LoadSettings();
            await LoadCategoriesAsync();
            await LoadProductsAsync();
            txtBarcodeScan.Focus();
        }

        public async void RefreshData()
        {
            LoadSettings();
            await LoadCategoriesAsync();
            await LoadProductsAsync();
            CalculateTotals();
            txtBarcodeScan.Focus();
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

            dgvProductsCatalog.ColumnHeadersHeight = 48;
            dgvProductsCatalog.RowTemplate.Height = 40;
            dgvProductsCatalog.EnableHeadersVisualStyles = false;

            if (dgvProductsCatalog.Columns["ProductId"] != null)
                dgvProductsCatalog.Columns["ProductId"].Visible = false;
            if (dgvProductsCatalog.Columns["CategoryId"] != null)
                dgvProductsCatalog.Columns["CategoryId"].Visible = false;
            if (dgvProductsCatalog.Columns["BuyPrice"] != null)
                dgvProductsCatalog.Columns["BuyPrice"].Visible = false;
            if (dgvProductsCatalog.Columns["MinStockAlert"] != null)
                dgvProductsCatalog.Columns["MinStockAlert"].Visible = false;
            if (dgvProductsCatalog.Columns["CreatedAt"] != null)
                dgvProductsCatalog.Columns["CreatedAt"].Visible = false;
            if (dgvProductsCatalog.Columns["IsLowStock"] != null)
                dgvProductsCatalog.Columns["IsLowStock"].Visible = false;

            if (dgvProductsCatalog.Columns["Barcode"] != null)
            {
                dgvProductsCatalog.Columns["Barcode"].HeaderText = "الباركود";
                dgvProductsCatalog.Columns["Barcode"].FillWeight = 85;
            }
            if (dgvProductsCatalog.Columns["ProductName"] != null)
            {
                dgvProductsCatalog.Columns["ProductName"].HeaderText = "اسم المنتج";
                dgvProductsCatalog.Columns["ProductName"].FillWeight = 160;
            }
            if (dgvProductsCatalog.Columns["CategoryName"] != null)
            {
                dgvProductsCatalog.Columns["CategoryName"].HeaderText = "القسم";
                dgvProductsCatalog.Columns["CategoryName"].FillWeight = 90;
            }
            if (dgvProductsCatalog.Columns["SellPrice"] != null)
            {
                dgvProductsCatalog.Columns["SellPrice"].HeaderText = "السعر";
                dgvProductsCatalog.Columns["SellPrice"].FillWeight = 75;
                dgvProductsCatalog.Columns["SellPrice"].DefaultCellStyle.Format = "N2";
                dgvProductsCatalog.Columns["SellPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProductsCatalog.Columns["StockQuantity"] != null)
            {
                dgvProductsCatalog.Columns["StockQuantity"].HeaderText = "المتاح";
                dgvProductsCatalog.Columns["StockQuantity"].FillWeight = 60;
                dgvProductsCatalog.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvProductsCatalog.Columns["colAdd"] == null)
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "colAdd";
                btnCol.HeaderText = "إضافة";
                btnCol.Text = "➕";
                btnCol.UseColumnTextForButtonValue = true;
                btnCol.FillWeight = 50;
                btnCol.FlatStyle = FlatStyle.Flat;
                dgvProductsCatalog.Columns.Add(btnCol);
            }
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

            dgvCart.ColumnHeadersHeight = 48;
            dgvCart.RowTemplate.Height = 40;
            dgvCart.EnableHeadersVisualStyles = false;

            if (dgvCart.Columns["ProductId"] != null)
                dgvCart.Columns["ProductId"].Visible = false;
            if (dgvCart.Columns["AvailableStock"] != null)
                dgvCart.Columns["AvailableStock"].Visible = false;

            if (dgvCart.Columns["Barcode"] != null)
            {
                dgvCart.Columns["Barcode"].HeaderText = "الباركود";
                dgvCart.Columns["Barcode"].ReadOnly = true;
                dgvCart.Columns["Barcode"].FillWeight = 85;
            }
            if (dgvCart.Columns["ProductName"] != null)
            {
                dgvCart.Columns["ProductName"].HeaderText = "اسم الصنف";
                dgvCart.Columns["ProductName"].ReadOnly = true;
                dgvCart.Columns["ProductName"].FillWeight = 160;
            }
            if (dgvCart.Columns["UnitPrice"] != null)
            {
                dgvCart.Columns["UnitPrice"].HeaderText = "السعر";
                dgvCart.Columns["UnitPrice"].ReadOnly = true;
                dgvCart.Columns["UnitPrice"].FillWeight = 75;
                dgvCart.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCart.Columns["colMinus"] == null)
            {
                DataGridViewButtonColumn btnMinus = new DataGridViewButtonColumn();
                btnMinus.Name = "colMinus";
                btnMinus.HeaderText = "-";
                btnMinus.Text = "➖";
                btnMinus.UseColumnTextForButtonValue = true;
                btnMinus.FillWeight = 40;
                btnMinus.FlatStyle = FlatStyle.Flat;
                dgvCart.Columns.Add(btnMinus);
            }

            if (dgvCart.Columns["Quantity"] != null)
            {
                dgvCart.Columns["Quantity"].HeaderText = "الكمية";
                dgvCart.Columns["Quantity"].ReadOnly = false;
                dgvCart.Columns["Quantity"].FillWeight = 60;
                dgvCart.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvCart.Columns["colPlus"] == null)
            {
                DataGridViewButtonColumn btnPlus = new DataGridViewButtonColumn();
                btnPlus.Name = "colPlus";
                btnPlus.HeaderText = "+";
                btnPlus.Text = "➕";
                btnPlus.UseColumnTextForButtonValue = true;
                btnPlus.FillWeight = 40;
                btnPlus.FlatStyle = FlatStyle.Flat;
                dgvCart.Columns.Add(btnPlus);
            }

            if (dgvCart.Columns["LineTotal"] != null)
            {
                dgvCart.Columns["LineTotal"].HeaderText = "الإجمالي";
                dgvCart.Columns["LineTotal"].ReadOnly = true;
                dgvCart.Columns["LineTotal"].FillWeight = 80;
                dgvCart.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCart.Columns["colDelete"] == null)
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "colDelete";
                btnDelete.HeaderText = "حذف";
                btnDelete.Text = "❌";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FillWeight = 45;
                btnDelete.FlatStyle = FlatStyle.Flat;
                dgvCart.Columns.Add(btnDelete);
            }
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

        private void CalculateTotals()
        {
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

            if (numCashPaid.Value == 0 || numCashPaid.Value < finalAmount)
            {
                numCashPaid.Value = finalAmount;
            }

            decimal change = Math.Max(0, numCashPaid.Value - finalAmount);
            lblChangeDueVal.Text = $"{change:N2} {curr}";
        }

        private async void txtBarcodeScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
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
                AddSelectedCatalogProduct(e.RowIndex);
            }
        }

        private void dgvProductsCatalog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AddSelectedCatalogProduct(e.RowIndex);
            }
        }

        private void AddSelectedCatalogProduct(int rowIndex)
        {
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
            CalculateTotals();
        }

        private void numCashPaid_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotals();
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
            await ProcessCheckoutAsync();
        }

        private async Task ProcessCheckoutAsync()
        {
            if (_isCheckingOut) return;

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
