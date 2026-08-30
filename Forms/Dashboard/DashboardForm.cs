using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class DashboardForm : Form
    {
        private string _activeFilter = "اليوم";
        private bool _isLoading = false;

        public DashboardForm()
        {
            InitializeComponent();
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.SetFilterButtonActive(btnFilterToday, true);
            UIStyler.SetFilterButtonActive(btnFilterWeek, false);
            UIStyler.SetFilterButtonActive(btnFilterMonth, false);
            UIStyler.SetFilterButtonActive(btnFilterAll, false);
            UIStyler.StyleSecondaryButton(btnRefresh);
            UIStyler.StyleDataGrid(dgvTopProducts);
            UIStyler.StyleDataGrid(dgvRecentSales);
            UIStyler.StyleDataGrid(dgvLowStock);
            await RefreshDataAsync();
        }

        public async void RefreshData()
        {
            await RefreshDataAsync();
        }

        public async Task RefreshDataAsync()
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                btnRefresh.Enabled = false;

                // Execute all 4 analytical queries concurrently in parallel
                var kpisTask = DbHelper.GetDashboardKPIsAsync(_activeFilter);
                var topTask = DbHelper.GetTopSellingProductsAsync(5, _activeFilter);
                var recentTask = DbHelper.GetRecentTransactionsAsync(10);
                var lowStockTask = DbHelper.GetUrgentLowStockProductsAsync(10);

                await Task.WhenAll(kpisTask, topTask, recentTask, lowStockTask);

                var kpis = await kpisTask;
                DataTable dtTop = await topTask;
                DataTable dtRecent = await recentTask;
                DataTable dtLowStock = await lowStockTask;

                // 1. KPIs
                // 1.1 إجمالي المبيعات (البيع)
                lblKpiSalesVal.Text = $"{kpis.TotalSalesRevenue:N2} ج.م";
                lblKpiSalesSub.Text = $"عدد الفواتير: {kpis.TotalTransactionsCount} فاتورة";

                // 1.2 إجمالي المشتريات (الشراء)
                lblKpiPurchasesVal.Text = $"{kpis.TotalPurchasesAmount:N2} ج.م";
                lblKpiPurchasesSub.Text = $"تكلفة المباع (COGS): {kpis.CostOfGoodsSold:N2} ج.م";

                // 1.3 صافي الأرباح والمكسب
                string sign = kpis.NetProfit >= 0 ? "+" : "";
                lblKpiProfitVal.Text = $"{sign}{kpis.NetProfit:N2} ج.م";
                lblKpiProfitVal.ForeColor = kpis.NetProfit >= 0 ? POS.DesignSystem.Tokens.UIColors.Success : POS.DesignSystem.Tokens.UIColors.Danger;
                lblKpiProfitSub.Text = $"هامش الربح: {kpis.ProfitMarginPct:N1}% من إجمالي المبيعات";

                // 1.4 بضاعة وقيمة المخزون
                lblKpiProductsVal.Text = $"{kpis.TotalProductsInStock:N0} قطعة";
                lblKpiProductsSub.Text = $"قيمة التكلفة: {kpis.InventoryCostValue:N2} ج.م | {kpis.LowStockItemsCount} نواقص";

                // Apply prominent KPI font styling
                lblKpiSalesVal.Font = FontManager.GetBold(14.5f);
                lblKpiPurchasesVal.Font = FontManager.GetBold(14.5f);
                lblKpiProfitVal.Font = FontManager.GetBold(14.5f);
                lblKpiProductsVal.Font = FontManager.GetBold(14.5f);

                lblKpiSalesTitle.Font = FontManager.GetBold(9.5f);
                lblKpiPurchasesTitle.Font = FontManager.GetBold(9.5f);
                lblKpiProfitTitle.Font = FontManager.GetBold(9.5f);
                lblKpiProductsTitle.Font = FontManager.GetBold(9.5f);

                lblKpiSalesSub.Font = FontManager.GetRegular(8.5f);
                lblKpiPurchasesSub.Font = FontManager.GetRegular(8.5f);
                lblKpiProfitSub.Font = FontManager.GetRegular(8.5f);
                lblKpiProductsSub.Font = FontManager.GetRegular(8.5f);

                // 2. Top Selling Products
                dgvTopProducts.DataSource = dtTop;
                FormatTopProductsGrid();

                // 3. Recent Sales Transactions
                dgvRecentSales.DataSource = dtRecent;
                FormatRecentSalesGrid();

                // 4. Low Stock Critical Alerts
                dgvLowStock.DataSource = dtLowStock;
                FormatLowStockGrid();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Dashboard refresh error: " + ex.Message);
            }
            finally
            {
                _isLoading = false;
                btnRefresh.Enabled = true;
            }
        }

        private void FormatTopProductsGrid()
        {
            if (dgvTopProducts.Columns.Count == 0) return;

            dgvTopProducts.ScrollBars = ScrollBars.Both;
            dgvTopProducts.ColumnHeadersHeight = UITheme.GridHeaderHeight;
            dgvTopProducts.RowTemplate.Height = UITheme.GridRowHeight;
            dgvTopProducts.EnableHeadersVisualStyles = false;

            dgvTopProducts.ConfigureTextColumn("ProductName", "اسم المنتج", fillWeight: 175, minWidth: 140);
            dgvTopProducts.ConfigureCenterColumn("Barcode", "الباركود", fillWeight: 75, minWidth: 90);
            dgvTopProducts.ConfigureTextColumn("CategoryName", "القسم", fillWeight: 75, minWidth: 85);
            dgvTopProducts.ConfigureNumericColumn("UnitsSold", "الكمية المباعة", fillWeight: 75, minWidth: 85);
            dgvTopProducts.ConfigureCurrencyColumn("TotalRevenue", "إجمالي الإيراد", fillWeight: 100, minWidth: 95);
        }

        private void FormatRecentSalesGrid()
        {
            if (dgvRecentSales.Columns.Count == 0) return;

            dgvRecentSales.ScrollBars = ScrollBars.Both;
            dgvRecentSales.ColumnHeadersHeight = UITheme.GridHeaderHeight;
            dgvRecentSales.RowTemplate.Height = UITheme.GridRowHeight;
            dgvRecentSales.EnableHeadersVisualStyles = false;

            dgvRecentSales.ConfigureIdColumn("SaleId", "رقم الفاتورة", fillWeight: 60, minWidth: 80);
            dgvRecentSales.ConfigureDateColumn("SaleDate", "التاريخ والوقت", fillWeight: 110, minWidth: 130);
            dgvRecentSales.ConfigureTextColumn("Cashier", "الكاشير", fillWeight: 90, minWidth: 100);
            dgvRecentSales.ConfigureCurrencyColumn("FinalAmount", "الإجمالي (ج.م)", fillWeight: 90, minWidth: 95);
            dgvRecentSales.ConfigureCenterColumn("PaymentMethod", "الدفع", fillWeight: 70, minWidth: 75);
            dgvRecentSales.ConfigureNumericColumn("ItemsCount", "عدد الأصناف", fillWeight: 80, minWidth: 80);
        }

        private void FormatLowStockGrid()
        {
            if (dgvLowStock.Columns.Count == 0) return;

            dgvLowStock.ScrollBars = ScrollBars.Both;
            dgvLowStock.ColumnHeadersHeight = UITheme.GridHeaderHeight;
            dgvLowStock.RowTemplate.Height = UITheme.GridRowHeight;
            dgvLowStock.EnableHeadersVisualStyles = false;

            dgvLowStock.HideColumn("ProductId");

            dgvLowStock.ConfigureTextColumn("ProductName", "اسم المنتج", fillWeight: 175, minWidth: 140);
            dgvLowStock.ConfigureCenterColumn("Barcode", "الباركود", fillWeight: 75, minWidth: 90);
            dgvLowStock.ConfigureTextColumn("CategoryName", "القسم", fillWeight: 70, minWidth: 85);

            var colStock = dgvLowStock.ConfigureNumericColumn("StockQuantity", "الكمية الحالية", fillWeight: 60, minWidth: 85);
            if (colStock != null)
            {
                colStock.DefaultCellStyle.ForeColor = POS.DesignSystem.Tokens.UIColors.Danger;
                colStock.DefaultCellStyle.Font = FontManager.GetBold(9.5f);
            }

            dgvLowStock.ConfigureNumericColumn("MinStockAlert", "حد الطلب (الأدنى)", fillWeight: 60, minWidth: 95);
            dgvLowStock.ConfigureCurrencyColumn("BuyPrice", "سعر الشراء", fillWeight: 60, minWidth: 85);
            dgvLowStock.ConfigureCurrencyColumn("SellPrice", "سعر البيع", fillWeight: 60, minWidth: 85);
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            UIStyler.SetFilterButtonActive(btnFilterToday, btn == btnFilterToday);
            UIStyler.SetFilterButtonActive(btnFilterWeek, btn == btnFilterWeek);
            UIStyler.SetFilterButtonActive(btnFilterMonth, btn == btnFilterMonth);
            UIStyler.SetFilterButtonActive(btnFilterAll, btn == btnFilterAll);

            if (btn == btnFilterToday) _activeFilter = "اليوم";
            else if (btn == btnFilterWeek) _activeFilter = "الأسبوع";
            else if (btn == btnFilterMonth) _activeFilter = "الشهر";
            else if (btn == btnFilterAll) _activeFilter = "الكل";

            await RefreshDataAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshDataAsync();
        }

        private void lblKpiSalesVal_Click(object sender, EventArgs e)
        {
        }

        private void flpFilters_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
