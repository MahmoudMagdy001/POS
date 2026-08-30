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
            UIStyler.StyleSecondaryButton(btnRefresh, "تحديث");
            UIStyler.StylePrimaryButton(btnPrintReceipt, "طباعة إيصال");
            UIStyler.StyleSecondaryButton(btnExportImage, "تصدير كصورة");
            UIStyler.StyleDangerButton(btnReturn, "إرجاع أصناف");
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
                    lblSaleDetailsTitle.Text = "تفاصيل وأصناف الفاتورة المحددة (لا توجد بيانات)";
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

            dgvSalesList.ScrollBars = ScrollBars.Both;
            dgvSalesList.ColumnHeadersHeight = 44;
            dgvSalesList.RowTemplate.Height = 38;
            dgvSalesList.EnableHeadersVisualStyles = false;

            dgvSalesList.HideColumn("TotalAmount");
            dgvSalesList.HideColumn("Discount");
            dgvSalesList.HideColumn("TaxAmount");
            dgvSalesList.HideColumn("PaidAmount");
            dgvSalesList.HideColumn("ChangeAmount");

            dgvSalesList.ConfigureIdColumn("SaleId", "رقم الفاتورة", fillWeight: 65, minWidth: 85);
            dgvSalesList.ConfigureDateColumn("SaleDate", "تاريخ ووقت البيع", fillWeight: 110, minWidth: 140);
            dgvSalesList.ConfigureTextColumn("CashierName", "الكاشير", fillWeight: 90, minWidth: 110);
            dgvSalesList.ConfigureCurrencyColumn("FinalAmount", "المبلغ الأصلي", fillWeight: 75, minWidth: 95);
            
            var colRef = dgvSalesList.ConfigureCurrencyColumn("TotalRefunded", "المسترد", fillWeight: 65, minWidth: 85);
            if (colRef != null) colRef.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);

            var colNet = dgvSalesList.ConfigureCurrencyColumn("NetFinalAmount", "الصافي المستحق", fillWeight: 85, minWidth: 110);
            if (colNet != null)
            {
                colNet.DefaultCellStyle.Font = FontManager.GetBold(9.5f);
                colNet.DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
            }

            var colStat = dgvSalesList.ConfigureCenterColumn("ReturnStatus", "حالة الفاتورة", fillWeight: 80, minWidth: 105);
            if (colStat != null) colStat.DefaultCellStyle.Font = FontManager.GetBold(9f);

            dgvSalesList.ConfigureCenterColumn("PaymentMethod", "الدفع", fillWeight: 60, minWidth: 75);
            dgvSalesList.ConfigureNumericColumn("ItemsCount", "الأصناف", fillWeight: 50, minWidth: 65);

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
                lblSaleDetailsTitle.Text = $"تفاصيل وأصناف الفاتورة #{saleId:D5}";
            }
            catch { }
        }

        private void FormatSaleDetailsGrid()
        {
            if (dgvSaleDetails.Columns.Count == 0) return;

            dgvSaleDetails.ScrollBars = ScrollBars.Both;
            dgvSaleDetails.ColumnHeadersHeight = 44;
            dgvSaleDetails.RowTemplate.Height = 38;
            dgvSaleDetails.EnableHeadersVisualStyles = false;

            dgvSaleDetails.HideColumn("DetailId");
            dgvSaleDetails.HideColumn("SaleId");
            dgvSaleDetails.HideColumn("ProductId");

            dgvSaleDetails.ConfigureCenterColumn("Barcode", "الباركود", fillWeight: 75, minWidth: 90);
            dgvSaleDetails.ConfigureTextColumn("ProductName", "اسم الصنف", fillWeight: 160, minWidth: 150);
            dgvSaleDetails.ConfigureCurrencyColumn("UnitPrice", "سعر الوحدة", fillWeight: 65, minWidth: 85);
            dgvSaleDetails.ConfigureNumericColumn("Quantity", "المباع", fillWeight: 50, minWidth: 60);

            var colRet = dgvSaleDetails.ConfigureNumericColumn("ReturnedQuantity", "المرتجع", fillWeight: 50, minWidth: 65);
            if (colRet != null) colRet.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);

            var colAct = dgvSaleDetails.ConfigureNumericColumn("ActiveQuantity", "الصافي", fillWeight: 50, minWidth: 65);
            if (colAct != null)
            {
                colAct.DefaultCellStyle.Font = FontManager.GetBold(9f);
                colAct.DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
            }

            dgvSaleDetails.ConfigureCurrencyColumn("LineTotal", "الإجمالي (ج.م)", fillWeight: 75, minWidth: 95);
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
                returnForm.StartPosition = FormStartPosition.CenterScreen;
                if (returnForm.ShowDialog(this.FindForm() ?? this) == DialogResult.OK)
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
