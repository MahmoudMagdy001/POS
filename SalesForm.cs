using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class SalesForm : Form
    {
        private readonly UserModel _currentUser;
        private Timer _searchDebounceTimer;
        private int _currentSelectionSequence = 0;
        private bool _isLoadingList = false;

        public SalesForm(UserModel currentUser = null)
        {
            _currentUser = currentUser;
            InitializeComponent();

            _searchDebounceTimer = new Timer();
            _searchDebounceTimer.Interval = 250;
            _searchDebounceTimer.Tick += OnSearchDebounceTick;
        }

        private async void SalesForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblCardRevenueVal.Font = FontManager.GetBold(15f);
            lblCardCountVal.Font = FontManager.GetBold(15f);
            lblCardItemsVal.Font = FontManager.GetBold(15f);
            UIStyler.StyleSecondaryButton(btnRefresh, "🔄 تحديث");
            UIStyler.StylePrimaryButton(btnPrintReceipt, "🖨️ طباعة إيصال");
            UIStyler.StyleSecondaryButton(btnExportImage, "🖼️ تصدير كصورة");
            UIStyler.StyleDangerButton(btnReturn, "↩️ إرجاع أصناف");
            UIStyler.StyleDataGrid(dgvSalesList);
            UIStyler.StyleDataGrid(dgvSaleDetails);
            cmbPeriod.SelectedIndex = 0; // اليوم
            await LoadSalesListAsync();
        }

        public async void RefreshData()
        {
            await LoadSalesListAsync();
        }

        private async Task LoadSalesListAsync()
        {
            if (_isLoadingList) return;

            try
            {
                _isLoadingList = true;
                btnRefresh.Enabled = false;

                string period = cmbPeriod.SelectedItem?.ToString() ?? "اليوم";
                string search = txtSearch.Text.Trim();

                DataTable dt = await DbHelper.GetAllSalesDataTableAsync(period, null, null, search);
                dgvSalesList.DataSource = dt;
                FormatSalesGrid();

                // حساب مؤشرات الأداء والملخص الصافي
                decimal totalRevenue = 0;
                int totalInvoices = dt.Rows.Count;
                int totalItems = 0;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["NetFinalAmount"] != DBNull.Value)
                        totalRevenue += Convert.ToDecimal(r["NetFinalAmount"]);
                    else if (r["FinalAmount"] != DBNull.Value)
                        totalRevenue += Convert.ToDecimal(r["FinalAmount"]);

                    if (r["ItemsCount"] != DBNull.Value)
                        totalItems += Convert.ToInt32(r["ItemsCount"]);
                }

                lblCardRevenueVal.Text = $"{totalRevenue:N2} ج.م";
                lblCardCountVal.Text = $"{totalInvoices} فاتورة";
                lblCardItemsVal.Text = $"{totalItems} بند / صنف";

                if (dt.Rows.Count == 0)
                {
                    dgvSaleDetails.DataSource = null;
                    lblSaleDetailsTitle.Text = "📦 تفاصيل وأصناف الفاتورة المحددة (لا توجد بيانات)";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in LoadSalesList: " + ex.Message);
            }
            finally
            {
                _isLoadingList = false;
                btnRefresh.Enabled = true;
            }
        }

        private void FormatSalesGrid()
        {
            if (dgvSalesList.Columns.Count == 0) return;

            dgvSalesList.ColumnHeadersHeight = 44;
            dgvSalesList.RowTemplate.Height = 38;
            dgvSalesList.EnableHeadersVisualStyles = false;

            if (dgvSalesList.Columns["SaleId"] != null)
            {
                dgvSalesList.Columns["SaleId"].HeaderText = "رقم الفاتورة";
                dgvSalesList.Columns["SaleId"].FillWeight = 65;
                dgvSalesList.Columns["SaleId"].DefaultCellStyle.Format = "D5";
                dgvSalesList.Columns["SaleId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSalesList.Columns["SaleDate"] != null)
            {
                dgvSalesList.Columns["SaleDate"].HeaderText = "تاريخ ووقت البيع";
                dgvSalesList.Columns["SaleDate"].FillWeight = 110;
                dgvSalesList.Columns["SaleDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }
            if (dgvSalesList.Columns["CashierName"] != null)
            {
                dgvSalesList.Columns["CashierName"].HeaderText = "الكاشير";
                dgvSalesList.Columns["CashierName"].FillWeight = 90;
            }
            if (dgvSalesList.Columns["TotalAmount"] != null)
            {
                dgvSalesList.Columns["TotalAmount"].Visible = false;
            }
            if (dgvSalesList.Columns["Discount"] != null)
            {
                dgvSalesList.Columns["Discount"].Visible = false;
            }
            if (dgvSalesList.Columns["TaxAmount"] != null)
            {
                dgvSalesList.Columns["TaxAmount"].Visible = false;
            }
            if (dgvSalesList.Columns["FinalAmount"] != null)
            {
                dgvSalesList.Columns["FinalAmount"].HeaderText = "المبلغ الأصلي";
                dgvSalesList.Columns["FinalAmount"].FillWeight = 75;
                dgvSalesList.Columns["FinalAmount"].DefaultCellStyle.Format = "N2";
                dgvSalesList.Columns["FinalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSalesList.Columns["TotalRefunded"] != null)
            {
                dgvSalesList.Columns["TotalRefunded"].HeaderText = "المسترد";
                dgvSalesList.Columns["TotalRefunded"].FillWeight = 65;
                dgvSalesList.Columns["TotalRefunded"].DefaultCellStyle.Format = "N2";
                dgvSalesList.Columns["TotalRefunded"].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                dgvSalesList.Columns["TotalRefunded"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSalesList.Columns["NetFinalAmount"] != null)
            {
                dgvSalesList.Columns["NetFinalAmount"].HeaderText = "الصافي المستحق";
                dgvSalesList.Columns["NetFinalAmount"].FillWeight = 85;
                dgvSalesList.Columns["NetFinalAmount"].DefaultCellStyle.Format = "N2";
                dgvSalesList.Columns["NetFinalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesList.Columns["NetFinalAmount"].DefaultCellStyle.Font = FontManager.GetBold(9.5f);
                dgvSalesList.Columns["NetFinalAmount"].DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
            }
            if (dgvSalesList.Columns["ReturnStatus"] != null)
            {
                dgvSalesList.Columns["ReturnStatus"].HeaderText = "حالة الفاتورة";
                dgvSalesList.Columns["ReturnStatus"].FillWeight = 80;
                dgvSalesList.Columns["ReturnStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSalesList.Columns["ReturnStatus"].DefaultCellStyle.Font = FontManager.GetBold(9f);
            }
            if (dgvSalesList.Columns["PaidAmount"] != null)
            {
                dgvSalesList.Columns["PaidAmount"].Visible = false;
            }
            if (dgvSalesList.Columns["ChangeAmount"] != null)
            {
                dgvSalesList.Columns["ChangeAmount"].Visible = false;
            }
            if (dgvSalesList.Columns["PaymentMethod"] != null)
            {
                dgvSalesList.Columns["PaymentMethod"].HeaderText = "الدفع";
                dgvSalesList.Columns["PaymentMethod"].FillWeight = 60;
                dgvSalesList.Columns["PaymentMethod"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSalesList.Columns["ItemsCount"] != null)
            {
                dgvSalesList.Columns["ItemsCount"].HeaderText = "الأصناف";
                dgvSalesList.Columns["ItemsCount"].FillWeight = 50;
                dgvSalesList.Columns["ItemsCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvSalesList.CellFormatting -= DgvSalesList_CellFormatting;
            dgvSalesList.CellFormatting += DgvSalesList_CellFormatting;
        }

        private void DgvSalesList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvSalesList.Columns[e.ColumnIndex].Name;
            if (colName == "ReturnStatus" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "مرتجع بالكامل")
                {
                    e.CellStyle.ForeColor = POS.DesignSystem.Tokens.UIColors.Danger;
                    e.CellStyle.BackColor = POS.DesignSystem.Tokens.UIColors.DangerLight;
                }
                else if (status == "مرتجع جزئي")
                {
                    e.CellStyle.ForeColor = POS.DesignSystem.Tokens.UIColors.Warning;
                    e.CellStyle.BackColor = POS.DesignSystem.Tokens.UIColors.WarningLight;
                }
                else
                {
                    e.CellStyle.ForeColor = POS.DesignSystem.Tokens.UIColors.Success;
                }
            }
        }

        private async void dgvSalesList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSalesList.SelectedRows.Count > 0)
            {
                int seq = ++_currentSelectionSequence;
                var cellVal = dgvSalesList.SelectedRows[0].Cells["SaleId"].Value;
                if (cellVal != null && int.TryParse(cellVal.ToString(), out int saleId))
                {
                    await LoadSaleDetailsAsync(saleId, seq);
                }
            }
        }

        private async Task LoadSaleDetailsAsync(int saleId, int sequence)
        {
            try
            {
                DataTable dt = await DbHelper.GetSaleDetailsDataTableAsync(saleId);
                if (sequence != _currentSelectionSequence) return; // Discard stale response

                dgvSaleDetails.DataSource = dt;
                FormatSaleDetailsGrid();
                lblSaleDetailsTitle.Text = $"📦 تفاصيل وأصناف الفاتورة #{saleId:D5}";
            }
            catch { }
        }

        private void FormatSaleDetailsGrid()
        {
            if (dgvSaleDetails.Columns.Count == 0) return;

            dgvSaleDetails.ColumnHeadersHeight = 44;
            dgvSaleDetails.RowTemplate.Height = 38;
            dgvSaleDetails.EnableHeadersVisualStyles = false;

            if (dgvSaleDetails.Columns["DetailId"] != null)
                dgvSaleDetails.Columns["DetailId"].Visible = false;
            if (dgvSaleDetails.Columns["SaleId"] != null)
                dgvSaleDetails.Columns["SaleId"].Visible = false;
            if (dgvSaleDetails.Columns["ProductId"] != null)
                dgvSaleDetails.Columns["ProductId"].Visible = false;

            if (dgvSaleDetails.Columns["Barcode"] != null)
            {
                dgvSaleDetails.Columns["Barcode"].HeaderText = "الباركود";
                dgvSaleDetails.Columns["Barcode"].FillWeight = 75;
            }
            if (dgvSaleDetails.Columns["ProductName"] != null)
            {
                dgvSaleDetails.Columns["ProductName"].HeaderText = "اسم الصنف";
                dgvSaleDetails.Columns["ProductName"].FillWeight = 160;
            }
            if (dgvSaleDetails.Columns["UnitPrice"] != null)
            {
                dgvSaleDetails.Columns["UnitPrice"].HeaderText = "سعر الوحدة";
                dgvSaleDetails.Columns["UnitPrice"].FillWeight = 65;
                dgvSaleDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                dgvSaleDetails.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSaleDetails.Columns["Quantity"] != null)
            {
                dgvSaleDetails.Columns["Quantity"].HeaderText = "المباع";
                dgvSaleDetails.Columns["Quantity"].FillWeight = 50;
                dgvSaleDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSaleDetails.Columns["ReturnedQuantity"] != null)
            {
                dgvSaleDetails.Columns["ReturnedQuantity"].HeaderText = "المرتجع";
                dgvSaleDetails.Columns["ReturnedQuantity"].FillWeight = 50;
                dgvSaleDetails.Columns["ReturnedQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSaleDetails.Columns["ReturnedQuantity"].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
            }
            if (dgvSaleDetails.Columns["ActiveQuantity"] != null)
            {
                dgvSaleDetails.Columns["ActiveQuantity"].HeaderText = "الصافي";
                dgvSaleDetails.Columns["ActiveQuantity"].FillWeight = 50;
                dgvSaleDetails.Columns["ActiveQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSaleDetails.Columns["ActiveQuantity"].DefaultCellStyle.Font = FontManager.GetBold(9f);
                dgvSaleDetails.Columns["ActiveQuantity"].DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
            }
            if (dgvSaleDetails.Columns["LineTotal"] != null)
            {
                dgvSaleDetails.Columns["LineTotal"].HeaderText = "الإجمالي (ج.م)";
                dgvSaleDetails.Columns["LineTotal"].FillWeight = 75;
                dgvSaleDetails.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
                dgvSaleDetails.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private async void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadSalesListAsync();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void OnSearchDebounceTick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            await LoadSalesListAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadSalesListAsync();
        }

        private async void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvSalesList.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار فاتورة من القائمة أولاً لإجراء عملية الإرجاع.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int saleId = Convert.ToInt32(dgvSalesList.SelectedRows[0].Cells["SaleId"].Value);
            using (ReturnModalForm returnForm = new ReturnModalForm(saleId, _currentUser))
            {
                if (returnForm.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadSalesListAsync();
                }
            }
        }

        private (SaleModel Sale, List<CartItemModel> Items) GetSelectedSale()
        {
            if (dgvSalesList.SelectedRows.Count == 0) return (null, null);

            int saleId = Convert.ToInt32(dgvSalesList.SelectedRows[0].Cells["SaleId"].Value);
            SaleModel sale = DbHelper.GetSaleById(saleId);
            if (sale == null) return (null, null);

            DataTable dt = DbHelper.GetSaleDetailsDataTable(saleId);
            List<CartItemModel> items = new List<CartItemModel>();
            foreach (DataRow row in dt.Rows)
            {
                items.Add(new CartItemModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"]?.ToString() ?? "",
                    ProductName = row["ProductName"]?.ToString() ?? "",
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Quantity = Convert.ToInt32(row["Quantity"])
                });
            }

            return (sale, items);
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            var (sale, items) = GetSelectedSale();
            if (sale == null || items == null || items.Count == 0)
            {
                MessageBox.Show("يرجى اختيار فاتورة من القائمة أولاً لمعاينتها أو طباعتها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReceiptPrinter.PrintReceipt(sale, items, previewFirst: true);
        }

        private void btnExportImage_Click(object sender, EventArgs e)
        {
            var (sale, items) = GetSelectedSale();
            if (sale == null || items == null || items.Count == 0)
            {
                MessageBox.Show("يرجى اختيار فاتورة من القائمة أولاً لتصديرها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string savedPath = ReceiptPrinter.PreviewReceiptAsImage(sale, items, openAfterSave: true);
            if (!string.IsNullOrEmpty(savedPath))
            {
                MessageBox.Show($"تم تصدير وحفظ صورة الإيصال بنجاح على سطح المكتب:\n{savedPath}", "تم الحفظ بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
