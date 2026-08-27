using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class PurchasesForm : Form
    {
        private List<PurchaseDetailModel> _purchaseItems = new List<PurchaseDetailModel>();
        private DataTable _purchaseItemsTable;
        private List<ProductModel> _allProducts = new List<ProductModel>();
        private int _currentHistorySequence = 0;
        private bool _isLoadingHistory = false;

        public PurchasesForm()
        {
            InitializeComponent();
        }

        private async void PurchasesForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.StylePrimaryButton(btnSavePurchase, "💾 حفظ فاتورة المشتريات");
            UIStyler.StyleSecondaryButton(btnResetPurchase, "🔄 تفريغ");
            UIStyler.StyleSecondaryButton(btnQuickAddProduct, "➕ جديد");
            UIStyler.StyleSecondaryButton(btnQuickAddSupplier, "➕ مورد");
            UIStyler.StyleDataGrid(dgvPurchaseItems);
            UIStyler.StyleDataGrid(dgvPurchasesHistory);
            UIStyler.StyleDataGrid(dgvPurchaseHistoryDetails);
            InitPurchaseItemsTable();

            await LoadSuppliersAsync();
            await LoadProductDropdownAsync();
            await LoadPurchasesHistoryAsync();
        }

        public async void RefreshData()
        {
            await LoadSuppliersAsync();
            await LoadProductDropdownAsync();
            await LoadPurchasesHistoryAsync();
        }

        private void InitPurchaseItemsTable()
        {
            _purchaseItemsTable = new DataTable();
            _purchaseItemsTable.Columns.Add("ProductId", typeof(int));
            _purchaseItemsTable.Columns.Add("Barcode", typeof(string));
            _purchaseItemsTable.Columns.Add("ProductName", typeof(string));
            _purchaseItemsTable.Columns.Add("UnitPrice", typeof(decimal));
            _purchaseItemsTable.Columns.Add("Quantity", typeof(int));
            _purchaseItemsTable.Columns.Add("LineTotal", typeof(decimal));

            dgvPurchaseItems.DataSource = _purchaseItemsTable;
            FormatPurchaseItemsGrid();
        }

        private void FormatPurchaseItemsGrid()
        {
            if (dgvPurchaseItems.Columns.Count == 0) return;

            dgvPurchaseItems.ColumnHeadersHeight = 48;
            dgvPurchaseItems.RowTemplate.Height = 40;
            dgvPurchaseItems.EnableHeadersVisualStyles = false;

            if (dgvPurchaseItems.Columns["ProductId"] != null)
                dgvPurchaseItems.Columns["ProductId"].Visible = false;

            if (dgvPurchaseItems.Columns["Barcode"] != null)
            {
                dgvPurchaseItems.Columns["Barcode"].HeaderText = "الباركود";
                dgvPurchaseItems.Columns["Barcode"].FillWeight = 75; // 15%
            }
            if (dgvPurchaseItems.Columns["ProductName"] != null)
            {
                dgvPurchaseItems.Columns["ProductName"].HeaderText = "اسم الصنف";
                dgvPurchaseItems.Columns["ProductName"].FillWeight = 175; // 35%
            }
            if (dgvPurchaseItems.Columns["UnitPrice"] != null)
            {
                dgvPurchaseItems.Columns["UnitPrice"].HeaderText = "سعر الشراء للوحدة";
                dgvPurchaseItems.Columns["UnitPrice"].FillWeight = 75; // 15%
                dgvPurchaseItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                dgvPurchaseItems.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvPurchaseItems.Columns["Quantity"] != null)
            {
                dgvPurchaseItems.Columns["Quantity"].HeaderText = "الكمية المشتراة";
                dgvPurchaseItems.Columns["Quantity"].FillWeight = 60; // 12%
                dgvPurchaseItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvPurchaseItems.Columns["LineTotal"] != null)
            {
                dgvPurchaseItems.Columns["LineTotal"].HeaderText = "الإجمالي (ج.م)";
                dgvPurchaseItems.Columns["LineTotal"].FillWeight = 75; // 15%
                dgvPurchaseItems.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
                dgvPurchaseItems.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvPurchaseItems.Columns["colDelete"] == null)
            {
                DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
                btnDel.Name = "colDelete";
                btnDel.HeaderText = "إجراء";
                btnDel.Text = "❌ حذف";
                btnDel.UseColumnTextForButtonValue = true;
                btnDel.FillWeight = 40; // 8%
                btnDel.FlatStyle = FlatStyle.Flat;
                btnDel.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                btnDel.DefaultCellStyle.SelectionForeColor = Color.FromArgb(220, 38, 38);
                dgvPurchaseItems.Columns.Add(btnDel);
            }
        }

        private async Task LoadSuppliersAsync()
        {
            try
            {
                var suppliers = await Task.Run(() => DbHelper.GetAllSuppliersList());
                suppliers.Insert(0, new SupplierModel { SupplierId = 0, SupplierName = "مورد عام / نقدي (بدون تسجيل مورد)" });

                cmbSupplier.DataSource = null;
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierId";
            }
            catch { }
        }

        private async Task LoadProductDropdownAsync()
        {
            try
            {
                DataTable dt = await DbHelper.GetAllProductsDataTableAsync();
                _allProducts.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    _allProducts.Add(new ProductModel
                    {
                        ProductId = Convert.ToInt32(r["ProductId"]),
                        Barcode = r["Barcode"].ToString(),
                        ProductName = r["ProductName"].ToString(),
                        BuyPrice = Convert.ToDecimal(r["BuyPrice"]),
                        SellPrice = Convert.ToDecimal(r["SellPrice"]),
                        StockQuantity = Convert.ToInt32(r["StockQuantity"])
                    });
                }

                cmbSelectProduct.DataSource = null;
                cmbSelectProduct.DataSource = new List<ProductModel>(_allProducts);
                cmbSelectProduct.DisplayMember = "ProductName";
                cmbSelectProduct.ValueMember = "ProductId";
            }
            catch { }
        }

        private void cmbSelectProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectProduct.SelectedItem is ProductModel prod)
            {
                numUnitCost.Value = prod.BuyPrice;
                numPurchaseQty.Value = 1;
            }
        }

        private void btnQuickAddProduct_Click(object sender, EventArgs e)
        {
            if (cmbSelectProduct.SelectedItem is ProductModel prod)
            {
                decimal unitCost = numUnitCost.Value;
                int qty = (int)numPurchaseQty.Value;

                if (qty <= 0)
                {
                    MessageBox.Show("يرجى إدخال كمية أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existing = _purchaseItems.Find(x => x.ProductId == prod.ProductId);
                if (existing != null)
                {
                    existing.Quantity += qty;
                    existing.UnitPrice = unitCost;
                    existing.LineTotal = existing.Quantity * existing.UnitPrice;
                }
                else
                {
                    _purchaseItems.Add(new PurchaseDetailModel
                    {
                        ProductId = prod.ProductId,
                        Barcode = prod.Barcode,
                        ProductName = prod.ProductName,
                        UnitPrice = unitCost,
                        Quantity = qty,
                        LineTotal = qty * unitCost
                    });
                }

                SyncPurchaseItems();
            }
        }

        private void SyncPurchaseItems()
        {
            _purchaseItemsTable.BeginLoadData();
            _purchaseItemsTable.Rows.Clear();
            decimal total = 0;

            foreach (var item in _purchaseItems)
            {
                _purchaseItemsTable.Rows.Add(
                    item.ProductId,
                    item.Barcode,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity,
                    item.LineTotal
                );
                total += item.LineTotal;
            }
            _purchaseItemsTable.EndLoadData();

            lblTotalPurchaseVal.Text = $"{total:N2} ج.م";
        }

        private void dgvPurchaseItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _purchaseItems.Count && dgvPurchaseItems.Columns[e.ColumnIndex].Name == "colDelete")
            {
                _purchaseItems.RemoveAt(e.RowIndex);
                SyncPurchaseItems();
            }
        }

        private async void btnSavePurchase_Click(object sender, EventArgs e)
        {
            if (_purchaseItems.Count == 0)
            {
                MessageBox.Show("فاتورة المشتريات فارغة! يرجى إضافة أصناف للفاتورة أولاً.", "فاتورة فارغة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = 0;
            foreach (var item in _purchaseItems) total += item.LineTotal;

            int? supplierId = null;
            if (cmbSupplier.SelectedValue != null && int.TryParse(cmbSupplier.SelectedValue.ToString(), out int sId) && sId > 0)
            {
                supplierId = sId;
            }

            PurchaseModel purchase = new PurchaseModel
            {
                SupplierId = supplierId,
                PurchaseDate = dtpPurchaseDate.Value,
                TotalAmount = total,
                Notes = txtPurchaseNotes.Text.Trim()
            };

            bool updateBuyPrice = chkUpdateBuyPrice.Checked;
            var result = await Task.Run(() => DbHelper.ProcessPurchaseTransaction(purchase, _purchaseItems, updateBuyPrice));

            if (result.Success)
            {
                MessageBox.Show($"تم حفظ فاتورة المشتريات #{result.PurchaseId:D5} وتحديث أرصدة المخزون بنجاح!", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetNewPurchaseForm();
                await LoadPurchasesHistoryAsync();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPurchase_Click(object sender, EventArgs e)
        {
            if (_purchaseItems.Count > 0)
            {
                var confirm = MessageBox.Show("هل أنت متأكد من تفريغ بنود فاتورة المشتريات الحالية؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    ResetNewPurchaseForm();
                }
            }
        }

        private async void ResetNewPurchaseForm()
        {
            _purchaseItems.Clear();
            SyncPurchaseItems();
            txtPurchaseNotes.Clear();
            dtpPurchaseDate.Value = DateTime.Now;
            if (cmbSupplier.Items.Count > 0) cmbSupplier.SelectedIndex = 0;
            await LoadProductDropdownAsync();
        }

        private async void btnQuickAddSupplier_Click(object sender, EventArgs e)
        {
            using (Form modal = new Form())
            {
                modal.Text = "إضافة مورد جديد";
                modal.Size = new Size(420, 320);
                modal.StartPosition = FormStartPosition.CenterParent;
                modal.FormBorderStyle = FormBorderStyle.FixedDialog;
                modal.MaximizeBox = false;
                modal.MinimizeBox = false;
                modal.RightToLeft = RightToLeft.Yes;
                modal.RightToLeftLayout = true;
                modal.Font = FontManager.GetRegular(9f);
                modal.BackColor = Color.White;

                Label lblName = new Label { Text = "اسم المورد / الشركة:", Location = new Point(20, 20), AutoSize = true, Font = FontManager.GetBold(9f) };
                TextBox txtName = new TextBox { Location = new Point(20, 45), Size = new Size(360, 25), Font = FontManager.GetRegular(10f) };

                Label lblPhone = new Label { Text = "رقم الهاتف / المحمول:", Location = new Point(20, 80), AutoSize = true, Font = FontManager.GetBold(9f) };
                TextBox txtPhone = new TextBox { Location = new Point(20, 105), Size = new Size(360, 25), Font = FontManager.GetRegular(10f) };

                Label lblAddress = new Label { Text = "العنوان:", Location = new Point(20, 140), AutoSize = true, Font = FontManager.GetBold(9f) };
                TextBox txtAddress = new TextBox { Location = new Point(20, 165), Size = new Size(360, 25), Font = FontManager.GetRegular(10f) };

                Button btnSave = new Button { Text = "💾 حفظ المورد", Location = new Point(20, 215), Size = new Size(360, 42) };
                UIStyler.StylePrimaryButton(btnSave, "💾 حفظ المورد", false);

                btnSave.Click += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("يرجى إدخال اسم المورد.", "حقل مطلوب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Focus();
                        return;
                    }

                    var res = DbHelper.SaveSupplier(new SupplierModel
                    {
                        SupplierName = txtName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim()
                    });

                    if (res.Success)
                    {
                        MessageBox.Show(res.Message, "تمت الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        modal.DialogResult = DialogResult.OK;
                        modal.Close();
                    }
                    else
                    {
                        MessageBox.Show(res.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnSave.TextAlign = ContentAlignment.MiddleCenter;

                modal.Controls.Add(lblName);
                modal.Controls.Add(txtName);
                modal.Controls.Add(lblPhone);
                modal.Controls.Add(txtPhone);
                modal.Controls.Add(lblAddress);
                modal.Controls.Add(txtAddress);
                modal.Controls.Add(btnSave);

                FontManager.ApplyCairoFont(modal);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadSuppliersAsync();
                }
            }
        }

        #region History Tab

        private async void tabPurchases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPurchases.SelectedTab == tabHistory)
            {
                await LoadPurchasesHistoryAsync();
            }
        }

        private async Task LoadPurchasesHistoryAsync()
        {
            if (_isLoadingHistory) return;

            try
            {
                _isLoadingHistory = true;
                DataTable dt = await DbHelper.GetAllPurchasesDataTableAsync();
                dgvPurchasesHistory.DataSource = dt;
                FormatHistoryGrid();
            }
            catch { }
            finally
            {
                _isLoadingHistory = false;
            }
        }

        private void FormatHistoryGrid()
        {
            if (dgvPurchasesHistory.Columns.Count == 0) return;

            dgvPurchasesHistory.ColumnHeadersHeight = 48;
            dgvPurchasesHistory.RowTemplate.Height = 40;
            dgvPurchasesHistory.EnableHeadersVisualStyles = false;

            if (dgvPurchasesHistory.Columns["PurchaseId"] != null)
            {
                dgvPurchasesHistory.Columns["PurchaseId"].HeaderText = "رقم الفاتورة";
                dgvPurchasesHistory.Columns["PurchaseId"].FillWeight = 70; // 14%
                dgvPurchasesHistory.Columns["PurchaseId"].DefaultCellStyle.Format = "D5";
            }
            if (dgvPurchasesHistory.Columns["PurchaseDate"] != null)
            {
                dgvPurchasesHistory.Columns["PurchaseDate"].HeaderText = "تاريخ الشراء";
                dgvPurchasesHistory.Columns["PurchaseDate"].FillWeight = 100; // 20%
                dgvPurchasesHistory.Columns["PurchaseDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            if (dgvPurchasesHistory.Columns["SupplierName"] != null)
            {
                dgvPurchasesHistory.Columns["SupplierName"].HeaderText = "المورد";
                dgvPurchasesHistory.Columns["SupplierName"].FillWeight = 140; // 28%
            }
            if (dgvPurchasesHistory.Columns["TotalAmount"] != null)
            {
                dgvPurchasesHistory.Columns["TotalAmount"].HeaderText = "الإجمالي (ج.م)";
                dgvPurchasesHistory.Columns["TotalAmount"].FillWeight = 100; // 20%
                dgvPurchasesHistory.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
                dgvPurchasesHistory.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvPurchasesHistory.Columns["ItemsCount"] != null)
            {
                dgvPurchasesHistory.Columns["ItemsCount"].HeaderText = "عدد الأصناف";
                dgvPurchasesHistory.Columns["ItemsCount"].FillWeight = 90; // 18%
                dgvPurchasesHistory.Columns["ItemsCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvPurchasesHistory.Columns["Notes"] != null)
            {
                dgvPurchasesHistory.Columns["Notes"].HeaderText = "ملاحظات";
                dgvPurchasesHistory.Columns["Notes"].FillWeight = 90;
            }
        }

        private async void dgvPurchasesHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPurchasesHistory.SelectedRows.Count > 0)
            {
                int seq = ++_currentHistorySequence;
                var val = dgvPurchasesHistory.SelectedRows[0].Cells["PurchaseId"].Value;
                if (val != null && int.TryParse(val.ToString(), out int purchaseId))
                {
                    await LoadPurchaseHistoryDetailsAsync(purchaseId, seq);
                }
            }
        }

        private async Task LoadPurchaseHistoryDetailsAsync(int purchaseId, int sequence)
        {
            try
            {
                DataTable dt = await DbHelper.GetPurchaseDetailsDataTableAsync(purchaseId);
                if (sequence != _currentHistorySequence) return;

                dgvPurchaseHistoryDetails.DataSource = dt;
                FormatHistoryDetailsGrid();
                lblHistoryDetailsTitle.Text = $"📦 تفاصيل وأصناف الفاتورة #{purchaseId:D5}";
            }
            catch { }
        }

        private void FormatHistoryDetailsGrid()
        {
            if (dgvPurchaseHistoryDetails.Columns.Count == 0) return;

            dgvPurchaseHistoryDetails.ColumnHeadersHeight = 48;
            dgvPurchaseHistoryDetails.RowTemplate.Height = 40;
            dgvPurchaseHistoryDetails.EnableHeadersVisualStyles = false;

            if (dgvPurchaseHistoryDetails.Columns["DetailId"] != null)
                dgvPurchaseHistoryDetails.Columns["DetailId"].Visible = false;
            if (dgvPurchaseHistoryDetails.Columns["PurchaseId"] != null)
                dgvPurchaseHistoryDetails.Columns["PurchaseId"].Visible = false;
            if (dgvPurchaseHistoryDetails.Columns["ProductId"] != null)
                dgvPurchaseHistoryDetails.Columns["ProductId"].Visible = false;

            if (dgvPurchaseHistoryDetails.Columns["Barcode"] != null)
            {
                dgvPurchaseHistoryDetails.Columns["Barcode"].HeaderText = "الباركود";
                dgvPurchaseHistoryDetails.Columns["Barcode"].FillWeight = 80; // 16%
            }
            if (dgvPurchaseHistoryDetails.Columns["ProductName"] != null)
            {
                dgvPurchaseHistoryDetails.Columns["ProductName"].HeaderText = "اسم الصنف";
                dgvPurchaseHistoryDetails.Columns["ProductName"].FillWeight = 200; // 40%
            }
            if (dgvPurchaseHistoryDetails.Columns["UnitPrice"] != null)
            {
                dgvPurchaseHistoryDetails.Columns["UnitPrice"].HeaderText = "سعر الوحدة";
                dgvPurchaseHistoryDetails.Columns["UnitPrice"].FillWeight = 80; // 16%
                dgvPurchaseHistoryDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                dgvPurchaseHistoryDetails.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvPurchaseHistoryDetails.Columns["Quantity"] != null)
            {
                dgvPurchaseHistoryDetails.Columns["Quantity"].HeaderText = "الكمية";
                dgvPurchaseHistoryDetails.Columns["Quantity"].FillWeight = 60; // 12%
                dgvPurchaseHistoryDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvPurchaseHistoryDetails.Columns["LineTotal"] != null)
            {
                dgvPurchaseHistoryDetails.Columns["LineTotal"].HeaderText = "الإجمالي (ج.م)";
                dgvPurchaseHistoryDetails.Columns["LineTotal"].FillWeight = 80; // 16%
                dgvPurchaseHistoryDetails.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
                dgvPurchaseHistoryDetails.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        #endregion
    }
}
