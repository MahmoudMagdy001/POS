using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class ProductsForm : Form
    {
        private int _selectedProductId = 0;
        private DataTable _productsTable;
        private Timer _searchDebounceTimer;
        private bool _isLoading = false;

        public ProductsForm()
        {
            InitializeComponent();

            _searchDebounceTimer = new Timer();
            _searchDebounceTimer.Interval = 250;
            _searchDebounceTimer.Tick += OnSearchDebounceTick;
        }

        private async void ProductsForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblEditorTitle.Font = FontManager.GetBold(11.5f);
            UIStyler.StylePrimaryButton(btnSaveProduct, "💾 حفظ بيانات المنتج");
            UIStyler.StyleSecondaryButton(btnNewProduct, "➕ صنف جديد (تفريغ الحقول)");
            UIStyler.StyleSecondaryButton(btnGenBarcode, "باركود");
            UIStyler.StyleSecondaryButton(btnManageCategories, "الأقسام");
            UIStyler.StyleDangerButton(btnDeleteProduct, "🗑️ حذف الصنف المحدد");
            UIStyler.StyleSecondaryButton(btnRefresh, "🔄 تحديث");
            UIStyler.StyleDataGrid(dgvProducts);

            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        public async void RefreshData()
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await DbHelper.GetAllCategoriesAsync();

                // Dropdown in Editor
                cmbCategory.DataSource = null;
                cmbCategory.DataSource = new System.Collections.Generic.List<CategoryModel>(categories);
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";

                // Dropdown in Filter Bar
                var filterCats = new System.Collections.Generic.List<CategoryModel>(categories);
                filterCats.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "جميع الأقسام" });
                cmbCategoryFilter.DataSource = null;
                cmbCategoryFilter.DataSource = filterCats;
                cmbCategoryFilter.DisplayMember = "CategoryName";
                cmbCategoryFilter.ValueMember = "CategoryId";
            }
            catch { }
        }

        private async Task LoadProductsAsync()
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                string search = txtSearch.Text.Trim();
                int? catId = null;
                if (cmbCategoryFilter.SelectedValue != null && int.TryParse(cmbCategoryFilter.SelectedValue.ToString(), out int id) && id > 0)
                {
                    catId = id;
                }
                bool lowStock = chkLowStockOnly.Checked;

                _productsTable = await DbHelper.GetAllProductsDataTableAsync(search, catId, lowStock);
                dgvProducts.DataSource = _productsTable;
                FormatProductsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل المنتجات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void FormatProductsGrid()
        {
            if (dgvProducts.Columns.Count == 0) return;

            dgvProducts.ScrollBars = ScrollBars.Both;
            dgvProducts.ColumnHeadersHeight = 48;
            dgvProducts.RowTemplate.Height = 40;
            dgvProducts.EnableHeadersVisualStyles = false;

            if (dgvProducts.Columns["ProductId"] != null)
                dgvProducts.Columns["ProductId"].Visible = false;
            if (dgvProducts.Columns["CategoryId"] != null)
                dgvProducts.Columns["CategoryId"].Visible = false;
            if (dgvProducts.Columns["CreatedAt"] != null)
                dgvProducts.Columns["CreatedAt"].Visible = false;
            if (dgvProducts.Columns["IsLowStock"] != null)
                dgvProducts.Columns["IsLowStock"].Visible = false;

            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "اسم المنتج";
                dgvProducts.Columns["ProductName"].FillWeight = 175; // 35%
                dgvProducts.Columns["ProductName"].MinimumWidth = 160;
            }
            if (dgvProducts.Columns["Barcode"] != null)
            {
                dgvProducts.Columns["Barcode"].HeaderText = "الباركود";
                dgvProducts.Columns["Barcode"].FillWeight = 75; // 15%
                dgvProducts.Columns["Barcode"].MinimumWidth = 100;
            }
            if (dgvProducts.Columns["CategoryName"] != null)
            {
                dgvProducts.Columns["CategoryName"].HeaderText = "القسم";
                dgvProducts.Columns["CategoryName"].FillWeight = 70; // 14%
                dgvProducts.Columns["CategoryName"].MinimumWidth = 90;
            }
            if (dgvProducts.Columns["BuyPrice"] != null)
            {
                dgvProducts.Columns["BuyPrice"].HeaderText = "سعر الشراء";
                dgvProducts.Columns["BuyPrice"].FillWeight = 60; // 12%
                dgvProducts.Columns["BuyPrice"].MinimumWidth = 85;
                dgvProducts.Columns["BuyPrice"].DefaultCellStyle.Format = "N2";
                dgvProducts.Columns["BuyPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns["SellPrice"] != null)
            {
                dgvProducts.Columns["SellPrice"].HeaderText = "سعر البيع";
                dgvProducts.Columns["SellPrice"].FillWeight = 60; // 12%
                dgvProducts.Columns["SellPrice"].MinimumWidth = 85;
                dgvProducts.Columns["SellPrice"].DefaultCellStyle.Format = "N2";
                dgvProducts.Columns["SellPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText = "الكمية بالمخزن";
                dgvProducts.Columns["StockQuantity"].FillWeight = 60; // 12%
                dgvProducts.Columns["StockQuantity"].MinimumWidth = 95;
                dgvProducts.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvProducts.Columns["MinStockAlert"] != null)
            {
                dgvProducts.Columns["MinStockAlert"].HeaderText = "حد التنبيه";
                dgvProducts.Columns["MinStockAlert"].FillWeight = 50; // 10%
                dgvProducts.Columns["MinStockAlert"].MinimumWidth = 80;
                dgvProducts.Columns["MinStockAlert"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                if (row.Cells["IsLowStock"] != null && row.Cells["IsLowStock"].Value != DBNull.Value)
                {
                    int isLow = Convert.ToInt32(row.Cells["IsLowStock"].Value);
                    if (isLow == 1)
                    {
                        row.DefaultCellStyle.BackColor = POS.DesignSystem.Tokens.UIColors.DangerLight;
                        row.DefaultCellStyle.ForeColor = POS.DesignSystem.Tokens.UIColors.DangerHover;
                        row.DefaultCellStyle.SelectionBackColor = POS.DesignSystem.Tokens.UIColors.DangerLight;
                        row.DefaultCellStyle.SelectionForeColor = POS.DesignSystem.Tokens.UIColors.DangerDark;
                    }
                }
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var row = dgvProducts.SelectedRows[0];
                _selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();

                if (row.Cells["CategoryId"].Value != DBNull.Value)
                {
                    cmbCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
                }
                else
                {
                    if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                }

                numBuyPrice.Value = Convert.ToDecimal(row.Cells["BuyPrice"].Value);
                numSellPrice.Value = Convert.ToDecimal(row.Cells["SellPrice"].Value);
                numStockQuantity.Value = Convert.ToDecimal(row.Cells["StockQuantity"].Value);
                numMinStockAlert.Value = Convert.ToDecimal(row.Cells["MinStockAlert"].Value);

                btnDeleteProduct.Enabled = true;
                lblEditorTitle.Text = $"📝 تعديل الصنف #{_selectedProductId}";
            }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }

        private void ClearEditor()
        {
            _selectedProductId = 0;
            txtBarcode.Clear();
            txtProductName.Clear();
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            numBuyPrice.Value = 0;
            numSellPrice.Value = 0;
            numStockQuantity.Value = 0;
            numMinStockAlert.Value = 5;
            btnDeleteProduct.Enabled = false;
            lblEditorTitle.Text = "📝 إضافة صنف جديد";
            dgvProducts.ClearSelection();
            txtBarcode.Focus();
        }

        private void btnGenBarcode_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = DbHelper.GenerateUniqueBarcode();
            txtProductName.Focus();
        }

        private async void btnSaveProduct_Click(object sender, EventArgs e)
        {
            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                MessageBox.Show("يرجى إدخال الباركود أو الضغط على زر توليد باركود تلقائي.", "حقل مطلوب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم المنتج.", "حقل مطلوب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (numSellPrice.Value < 0 || numBuyPrice.Value < 0)
            {
                MessageBox.Show("الأسعار لا يمكن أن تكون أرقاماً سالبة.", "قيمة غير مقبولة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? catId = null;
            if (cmbCategory.SelectedValue != null && int.TryParse(cmbCategory.SelectedValue.ToString(), out int cId) && cId > 0)
            {
                catId = cId;
            }

            ProductModel product = new ProductModel
            {
                ProductId = _selectedProductId,
                Barcode = barcode,
                ProductName = name,
                CategoryId = catId,
                BuyPrice = numBuyPrice.Value,
                SellPrice = numSellPrice.Value,
                StockQuantity = (int)numStockQuantity.Value,
                MinStockAlert = (int)numMinStockAlert.Value
            };

            var res = DbHelper.SaveProduct(product);
            if (res.Success)
            {
                MessageBox.Show(res.Message, "تم الحفظ بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsAsync();
                ClearEditor();
            }
            else
            {
                MessageBox.Show(res.Message, "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (_selectedProductId <= 0) return;

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من رغبتك في حذف المنتج '{txtProductName.Text}' نهائياً من قاعدة البيانات؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                var res = DbHelper.DeleteProduct(_selectedProductId);
                if (res.Success)
                {
                    MessageBox.Show(res.Message, "تم الحذف بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                    ClearEditor();
                }
                else
                {
                    MessageBox.Show(res.Message, "تعذر الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnManageCategories_Click(object sender, EventArgs e)
        {
            using (Form catForm = new Form())
            {
                catForm.Text = "إدارة وتصنيف الأقسام";
                catForm.Size = new Size(450, 480);
                catForm.StartPosition = FormStartPosition.CenterScreen;
                catForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                catForm.MaximizeBox = false;
                catForm.MinimizeBox = false;
                catForm.RightToLeft = RightToLeft.Yes;
                catForm.RightToLeftLayout = true;
                catForm.Font = FontManager.GetRegular(9f);
                catForm.BackColor = Color.White;

                Label lblCat = new Label { Text = "اسم القسم الجديد:", Location = new Point(20, 20), AutoSize = true, Font = FontManager.GetBold(9f) };
                TextBox txtCat = new TextBox { Location = new Point(20, 45), Size = new Size(270, 25), Font = FontManager.GetRegular(10f) };
                Button btnAdd = new Button { Text = "➕ إضافة", Location = new Point(300, 43), Size = new Size(110, 29), BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = FontManager.GetBold(9f), Cursor = Cursors.Hand };
                btnAdd.FlatAppearance.BorderSize = 0;

                ListBox lstCats = new ListBox { Location = new Point(20, 85), Size = new Size(390, 290), Font = FontManager.GetRegular(10f) };
                Button btnDel = new Button { Text = "🗑️ حذف القسم المحدد", Location = new Point(20, 390), Size = new Size(390, 35), BackColor = Color.FromArgb(254, 242, 242), ForeColor = Color.FromArgb(220, 38, 38), FlatStyle = FlatStyle.Flat, Font = FontManager.GetBold(9f), Cursor = Cursors.Hand };
                btnDel.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);

                Action refreshCats = () =>
                {
                    var cats = DbHelper.GetAllCategories();
                    lstCats.DataSource = null;
                    lstCats.DataSource = cats;
                    lstCats.DisplayMember = "CategoryName";
                    lstCats.ValueMember = "CategoryId";
                };

                btnAdd.Click += (s, ev) =>
                {
                    if (!string.IsNullOrWhiteSpace(txtCat.Text))
                    {
                        var res = DbHelper.SaveCategory(txtCat.Text.Trim());
                        if (res.Success)
                        {
                            txtCat.Clear();
                            refreshCats();
                        }
                        else
                        {
                            MessageBox.Show(res.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                };

                btnDel.Click += (s, ev) =>
                {
                    if (lstCats.SelectedItem is CategoryModel selCat)
                    {
                        var res = DbHelper.DeleteCategory(selCat.CategoryId);
                        if (res.Success)
                        {
                            refreshCats();
                        }
                        else
                        {
                            MessageBox.Show(res.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };

                btnAdd.TextAlign = ContentAlignment.MiddleCenter;
                btnDel.TextAlign = ContentAlignment.MiddleCenter;

                catForm.Controls.Add(lblCat);
                catForm.Controls.Add(txtCat);
                catForm.Controls.Add(btnAdd);
                catForm.Controls.Add(lstCats);
                catForm.Controls.Add(btnDel);

                refreshCats();
                FontManager.ApplyCairoFont(catForm);
                catForm.ShowDialog(this.FindForm() ?? this);

                await LoadCategoriesAsync();
                await LoadProductsAsync();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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

        private async void chkLowStockOnly_CheckedChanged(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            chkLowStockOnly.Checked = false;
            if (cmbCategoryFilter.Items.Count > 0) cmbCategoryFilter.SelectedIndex = 0;
            await LoadProductsAsync();
        }
    }
}
