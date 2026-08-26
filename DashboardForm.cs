using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class DashboardForm : Form
    {
        private string _activeFilter = "اليوم";

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            FontManager.ApplyCairoFont(this);
            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                // 1. KPIs
                var kpis = DbHelper.GetDashboardKPIs(_activeFilter);

                // 1.1 إجمالي المبيعات (البيع)
                lblKpiSalesVal.Text = $"{kpis.TotalSalesRevenue:N2} ج.م";
                lblKpiSalesSub.Text = $"عدد الفواتير: {kpis.TotalTransactionsCount} فاتورة";

                // 1.2 إجمالي المشتريات (الشراء)
                lblKpiPurchasesVal.Text = $"{kpis.TotalPurchasesAmount:N2} ج.م";
                lblKpiPurchasesSub.Text = $"تكلفة المباع (COGS): {kpis.CostOfGoodsSold:N2} ج.م";

                // 1.3 صافي الأرباح والمكسب
                string sign = kpis.NetProfit >= 0 ? "+" : "";
                lblKpiProfitVal.Text = $"{sign}{kpis.NetProfit:N2} ج.م";
                lblKpiProfitVal.ForeColor = kpis.NetProfit >= 0 ? Color.FromArgb(16, 185, 129) : Color.FromArgb(220, 38, 38);
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
                DataTable dtTop = DbHelper.GetTopSellingProducts(5, _activeFilter);
                dgvTopProducts.DataSource = dtTop;
                FormatTopProductsGrid();

                // 3. Recent Sales Transactions
                DataTable dtRecent = DbHelper.GetRecentTransactions(10);
                dgvRecentSales.DataSource = dtRecent;
                FormatRecentSalesGrid();

                // 4. Low Stock Critical Alerts
                DataTable dtLowStock = DbHelper.GetUrgentLowStockProducts(10);
                dgvLowStock.DataSource = dtLowStock;
                FormatLowStockGrid();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Dashboard refresh error: " + ex.Message);
            }
        }

        private void FormatTopProductsGrid()
        {
            if (dgvTopProducts.Columns.Count == 0) return;

            dgvTopProducts.ColumnHeadersHeight = 48;
            dgvTopProducts.RowTemplate.Height = 40;
            dgvTopProducts.EnableHeadersVisualStyles = false;

            if (dgvTopProducts.Columns["ProductName"] != null)
            {
                dgvTopProducts.Columns["ProductName"].HeaderText = "اسم المنتج";
                dgvTopProducts.Columns["ProductName"].FillWeight = 175;
            }
            if (dgvTopProducts.Columns["Barcode"] != null)
            {
                dgvTopProducts.Columns["Barcode"].HeaderText = "الباركود";
                dgvTopProducts.Columns["Barcode"].FillWeight = 75;
            }
            if (dgvTopProducts.Columns["CategoryName"] != null)
            {
                dgvTopProducts.Columns["CategoryName"].HeaderText = "القسم";
                dgvTopProducts.Columns["CategoryName"].FillWeight = 75;
            }
            if (dgvTopProducts.Columns["UnitsSold"] != null)
            {
                dgvTopProducts.Columns["UnitsSold"].HeaderText = "الكمية المباعة";
                dgvTopProducts.Columns["UnitsSold"].FillWeight = 75;
                dgvTopProducts.Columns["UnitsSold"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvTopProducts.Columns["TotalRevenue"] != null)
            {
                dgvTopProducts.Columns["TotalRevenue"].HeaderText = "إجمالي الإيراد";
                dgvTopProducts.Columns["TotalRevenue"].FillWeight = 100;
                dgvTopProducts.Columns["TotalRevenue"].DefaultCellStyle.Format = "N2";
                dgvTopProducts.Columns["TotalRevenue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FormatRecentSalesGrid()
        {
            if (dgvRecentSales.Columns.Count == 0) return;

            dgvRecentSales.ColumnHeadersHeight = 48;
            dgvRecentSales.RowTemplate.Height = 40;
            dgvRecentSales.EnableHeadersVisualStyles = false;

            if (dgvRecentSales.Columns["SaleId"] != null)
            {
                dgvRecentSales.Columns["SaleId"].HeaderText = "رقم الفاتورة";
                dgvRecentSales.Columns["SaleId"].FillWeight = 60;
                dgvRecentSales.Columns["SaleId"].DefaultCellStyle.Format = "D5";
            }
            if (dgvRecentSales.Columns["SaleDate"] != null)
            {
                dgvRecentSales.Columns["SaleDate"].HeaderText = "التاريخ والوقت";
                dgvRecentSales.Columns["SaleDate"].FillWeight = 110;
                dgvRecentSales.Columns["SaleDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }
            if (dgvRecentSales.Columns["Cashier"] != null)
            {
                dgvRecentSales.Columns["Cashier"].HeaderText = "الكاشير";
                dgvRecentSales.Columns["Cashier"].FillWeight = 90;
            }
            if (dgvRecentSales.Columns["FinalAmount"] != null)
            {
                dgvRecentSales.Columns["FinalAmount"].HeaderText = "الإجمالي (ج.م)";
                dgvRecentSales.Columns["FinalAmount"].FillWeight = 90;
                dgvRecentSales.Columns["FinalAmount"].DefaultCellStyle.Format = "N2";
                dgvRecentSales.Columns["FinalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvRecentSales.Columns["PaymentMethod"] != null)
            {
                dgvRecentSales.Columns["PaymentMethod"].HeaderText = "الدفع";
                dgvRecentSales.Columns["PaymentMethod"].FillWeight = 70;
            }
            if (dgvRecentSales.Columns["ItemsCount"] != null)
            {
                dgvRecentSales.Columns["ItemsCount"].HeaderText = "عدد الأصناف";
                dgvRecentSales.Columns["ItemsCount"].FillWeight = 80;
                dgvRecentSales.Columns["ItemsCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void FormatLowStockGrid()
        {
            if (dgvLowStock.Columns.Count == 0) return;

            dgvLowStock.ColumnHeadersHeight = 48;
            dgvLowStock.RowTemplate.Height = 40;
            dgvLowStock.EnableHeadersVisualStyles = false;

            if (dgvLowStock.Columns["ProductId"] != null)
                dgvLowStock.Columns["ProductId"].Visible = false;

            if (dgvLowStock.Columns["ProductName"] != null)
            {
                dgvLowStock.Columns["ProductName"].HeaderText = "اسم المنتج";
                dgvLowStock.Columns["ProductName"].FillWeight = 175;
            }
            if (dgvLowStock.Columns["Barcode"] != null)
            {
                dgvLowStock.Columns["Barcode"].HeaderText = "الباركود";
                dgvLowStock.Columns["Barcode"].FillWeight = 75;
            }
            if (dgvLowStock.Columns["CategoryName"] != null)
            {
                dgvLowStock.Columns["CategoryName"].HeaderText = "القسم";
                dgvLowStock.Columns["CategoryName"].FillWeight = 70;
            }
            if (dgvLowStock.Columns["StockQuantity"] != null)
            {
                dgvLowStock.Columns["StockQuantity"].HeaderText = "الكمية الحالية";
                dgvLowStock.Columns["StockQuantity"].FillWeight = 60;
                dgvLowStock.Columns["StockQuantity"].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                dgvLowStock.Columns["StockQuantity"].DefaultCellStyle.Font = FontManager.GetBold(9.5f);
                dgvLowStock.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvLowStock.Columns["MinStockAlert"] != null)
            {
                dgvLowStock.Columns["MinStockAlert"].HeaderText = "حد الطلب (الأدنى)";
                dgvLowStock.Columns["MinStockAlert"].FillWeight = 60;
                dgvLowStock.Columns["MinStockAlert"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvLowStock.Columns["BuyPrice"] != null)
            {
                dgvLowStock.Columns["BuyPrice"].HeaderText = "سعر الشراء";
                dgvLowStock.Columns["BuyPrice"].FillWeight = 60;
                dgvLowStock.Columns["BuyPrice"].DefaultCellStyle.Format = "N2";
                dgvLowStock.Columns["BuyPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvLowStock.Columns["SellPrice"] != null)
            {
                dgvLowStock.Columns["SellPrice"].HeaderText = "سعر البيع";
                dgvLowStock.Columns["SellPrice"].FillWeight = 60;
                dgvLowStock.Columns["SellPrice"].DefaultCellStyle.Format = "N2";
                dgvLowStock.Columns["SellPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            btnFilterToday.BackColor = Color.FromArgb(241, 245, 249);
            btnFilterToday.ForeColor = Color.FromArgb(51, 65, 85);
            btnFilterWeek.BackColor = Color.FromArgb(241, 245, 249);
            btnFilterWeek.ForeColor = Color.FromArgb(51, 65, 85);
            btnFilterMonth.BackColor = Color.FromArgb(241, 245, 249);
            btnFilterMonth.ForeColor = Color.FromArgb(51, 65, 85);
            btnFilterAll.BackColor = Color.FromArgb(241, 245, 249);
            btnFilterAll.ForeColor = Color.FromArgb(51, 65, 85);

            btn.BackColor = Color.FromArgb(14, 165, 233);
            btn.ForeColor = Color.White;

            if (btn == btnFilterToday) _activeFilter = "اليوم";
            else if (btn == btnFilterWeek) _activeFilter = "الأسبوع";
            else if (btn == btnFilterMonth) _activeFilter = "الشهر";
            else if (btn == btnFilterAll) _activeFilter = "الكل";

            RefreshData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void lblKpiSalesVal_Click(object sender, EventArgs e)
        {

        }
    }
}
