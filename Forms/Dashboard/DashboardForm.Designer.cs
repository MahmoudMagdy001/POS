namespace POS
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.flpFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFilterLabel = new System.Windows.Forms.Label();
            this.btnFilterToday = new System.Windows.Forms.Button();
            this.btnFilterWeek = new System.Windows.Forms.Button();
            this.btnFilterMonth = new System.Windows.Forms.Button();
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlTitles = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpKpis = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiProducts = new System.Windows.Forms.Panel();
            this.lblKpiProductsSub = new System.Windows.Forms.Label();
            this.lblKpiProductsVal = new System.Windows.Forms.Label();
            this.lblKpiProductsTitle = new System.Windows.Forms.Label();
            this.pnlKpiProfit = new System.Windows.Forms.Panel();
            this.lblKpiProfitSub = new System.Windows.Forms.Label();
            this.lblKpiProfitVal = new System.Windows.Forms.Label();
            this.lblKpiProfitTitle = new System.Windows.Forms.Label();
            this.pnlKpiPurchases = new System.Windows.Forms.Panel();
            this.lblKpiPurchasesSub = new System.Windows.Forms.Label();
            this.lblKpiPurchasesVal = new System.Windows.Forms.Label();
            this.lblKpiPurchasesTitle = new System.Windows.Forms.Label();
            this.pnlKpiSales = new System.Windows.Forms.Panel();
            this.lblKpiSalesSub = new System.Windows.Forms.Label();
            this.lblKpiSalesVal = new System.Windows.Forms.Label();
            this.lblKpiSalesTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardRecentSales = new System.Windows.Forms.Panel();
            this.dgvRecentSales = new System.Windows.Forms.DataGridView();
            this.lblCardRecentSalesTitle = new System.Windows.Forms.Label();
            this.pnlCardTopProducts = new System.Windows.Forms.Panel();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.lblCardTopProductsTitle = new System.Windows.Forms.Label();
            this.pnlCardLowStock = new System.Windows.Forms.Panel();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.lblCardLowStockTitle = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.flpFilters.SuspendLayout();
            this.pnlTitles.SuspendLayout();
            this.tlpKpis.SuspendLayout();
            this.pnlKpiProducts.SuspendLayout();
            this.pnlKpiProfit.SuspendLayout();
            this.pnlKpiPurchases.SuspendLayout();
            this.pnlKpiSales.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.pnlCardRecentSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).BeginInit();
            this.pnlCardTopProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.pnlCardLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.flpFilters);
            this.pnlTop.Controls.Add(this.pnlTitles);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(16, 16);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlTop.Size = new System.Drawing.Size(1168, 68);
            this.pnlTop.TabIndex = 0;
            // 
            // flpFilters
            // 
            this.flpFilters.Controls.Add(this.lblFilterLabel);
            this.flpFilters.Controls.Add(this.btnFilterToday);
            this.flpFilters.Controls.Add(this.btnFilterWeek);
            this.flpFilters.Controls.Add(this.btnFilterMonth);
            this.flpFilters.Controls.Add(this.btnFilterAll);
            this.flpFilters.Controls.Add(this.btnRefresh);
            this.flpFilters.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpFilters.Location = new System.Drawing.Point(-60, 8);
            this.flpFilters.Name = "flpFilters";
            this.flpFilters.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpFilters.Size = new System.Drawing.Size(700, 52);
            this.flpFilters.TabIndex = 1;
            this.flpFilters.WrapContents = false;
            this.flpFilters.Paint += new System.Windows.Forms.PaintEventHandler(this.flpFilters_Paint);
            // 
            // lblFilterLabel
            // 
            this.lblFilterLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFilterLabel.AutoSize = true;
            this.lblFilterLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblFilterLabel.Location = new System.Drawing.Point(592, 17);
            this.lblFilterLabel.Margin = new System.Windows.Forms.Padding(3, 0, 6, 0);
            this.lblFilterLabel.Name = "lblFilterLabel";
            this.lblFilterLabel.Size = new System.Drawing.Size(105, 18);
            this.lblFilterLabel.TabIndex = 0;
            this.lblFilterLabel.Text = "الفترة الزمنية:";
            // 
            // btnFilterToday
            // 
            this.btnFilterToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnFilterToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterToday.FlatAppearance.BorderSize = 0;
            this.btnFilterToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterToday.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterToday.ForeColor = System.Drawing.Color.White;
            this.btnFilterToday.Location = new System.Drawing.Point(508, 11);
            this.btnFilterToday.Name = "btnFilterToday";
            this.btnFilterToday.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFilterToday.Size = new System.Drawing.Size(75, 30);
            this.btnFilterToday.TabIndex = 1;
            this.btnFilterToday.Text = "اليوم";
            this.btnFilterToday.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilterToday.UseVisualStyleBackColor = false;
            this.btnFilterToday.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnFilterWeek
            // 
            this.btnFilterWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnFilterWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterWeek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnFilterWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterWeek.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnFilterWeek.Location = new System.Drawing.Point(417, 11);
            this.btnFilterWeek.Name = "btnFilterWeek";
            this.btnFilterWeek.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFilterWeek.Size = new System.Drawing.Size(85, 30);
            this.btnFilterWeek.TabIndex = 2;
            this.btnFilterWeek.Text = "الأسبوع";
            this.btnFilterWeek.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilterWeek.UseVisualStyleBackColor = false;
            this.btnFilterWeek.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnFilterMonth
            // 
            this.btnFilterMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnFilterMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnFilterMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterMonth.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnFilterMonth.Location = new System.Drawing.Point(326, 11);
            this.btnFilterMonth.Name = "btnFilterMonth";
            this.btnFilterMonth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFilterMonth.Size = new System.Drawing.Size(85, 30);
            this.btnFilterMonth.TabIndex = 3;
            this.btnFilterMonth.Text = "الشهر";
            this.btnFilterMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilterMonth.UseVisualStyleBackColor = false;
            this.btnFilterMonth.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnFilterAll
            // 
            this.btnFilterAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnFilterAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnFilterAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterAll.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnFilterAll.Location = new System.Drawing.Point(235, 11);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFilterAll.Size = new System.Drawing.Size(85, 30);
            this.btnFilterAll.TabIndex = 4;
            this.btnFilterAll.Text = "الكل";
            this.btnFilterAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilterAll.UseVisualStyleBackColor = false;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRefresh.Location = new System.Drawing.Point(134, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRefresh.Size = new System.Drawing.Size(95, 30);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "تحديث";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlTitles
            // 
            this.pnlTitles.Controls.Add(this.lblSubtitle);
            this.pnlTitles.Controls.Add(this.lblTitle);
            this.pnlTitles.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTitles.Location = new System.Drawing.Point(640, 8);
            this.pnlTitles.Name = "pnlTitles";
            this.pnlTitles.Size = new System.Drawing.Size(516, 52);
            this.pnlTitles.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubtitle.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 28);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubtitle.Size = new System.Drawing.Size(516, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "متابعة شاملة للمبيعات والمشتريات والأرباح المحققة وحركة المخزون";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size(516, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "لوحة التحكم العامة والأرباح";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlpKpis
            // 
            this.tlpKpis.ColumnCount = 4;
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpis.Controls.Add(this.pnlKpiProducts, 0, 0);
            this.tlpKpis.Controls.Add(this.pnlKpiProfit, 1, 0);
            this.tlpKpis.Controls.Add(this.pnlKpiPurchases, 2, 0);
            this.tlpKpis.Controls.Add(this.pnlKpiSales, 3, 0);
            this.tlpKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpKpis.Location = new System.Drawing.Point(16, 84);
            this.tlpKpis.Name = "tlpKpis";
            this.tlpKpis.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tlpKpis.RowCount = 1;
            this.tlpKpis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKpis.Size = new System.Drawing.Size(1168, 125);
            this.tlpKpis.TabIndex = 1;
            // 
            // pnlKpiProducts
            // 
            this.pnlKpiProducts.BackColor = System.Drawing.Color.White;
            this.pnlKpiProducts.Controls.Add(this.lblKpiProductsSub);
            this.pnlKpiProducts.Controls.Add(this.lblKpiProductsVal);
            this.pnlKpiProducts.Controls.Add(this.lblKpiProductsTitle);
            this.pnlKpiProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiProducts.Location = new System.Drawing.Point(881, 13);
            this.pnlKpiProducts.Margin = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.pnlKpiProducts.Name = "pnlKpiProducts";
            this.pnlKpiProducts.Padding = new System.Windows.Forms.Padding(10, 8, 10, 6);
            this.pnlKpiProducts.Size = new System.Drawing.Size(287, 99);
            this.pnlKpiProducts.TabIndex = 0;
            // 
            // lblKpiProductsSub
            // 
            this.lblKpiProductsSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProductsSub.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblKpiProductsSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiProductsSub.Location = new System.Drawing.Point(10, 68);
            this.lblKpiProductsSub.Name = "lblKpiProductsSub";
            this.lblKpiProductsSub.Size = new System.Drawing.Size(267, 22);
            this.lblKpiProductsSub.TabIndex = 2;
            this.lblKpiProductsSub.Text = "قيمة التكلفة: 0.00 ج.م";
            this.lblKpiProductsSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiProductsVal
            // 
            this.lblKpiProductsVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProductsVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblKpiProductsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblKpiProductsVal.Location = new System.Drawing.Point(10, 30);
            this.lblKpiProductsVal.Name = "lblKpiProductsVal";
            this.lblKpiProductsVal.Size = new System.Drawing.Size(267, 38);
            this.lblKpiProductsVal.TabIndex = 1;
            this.lblKpiProductsVal.Text = "0 قطعة";
            this.lblKpiProductsVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiProductsTitle
            // 
            this.lblKpiProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProductsTitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiProductsTitle.Location = new System.Drawing.Point(10, 8);
            this.lblKpiProductsTitle.Name = "lblKpiProductsTitle";
            this.lblKpiProductsTitle.Size = new System.Drawing.Size(267, 22);
            this.lblKpiProductsTitle.TabIndex = 0;
            this.lblKpiProductsTitle.Text = "رصيد وقيمة المخزون:";
            this.lblKpiProductsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpiProfit
            // 
            this.pnlKpiProfit.BackColor = System.Drawing.Color.White;
            this.pnlKpiProfit.Controls.Add(this.lblKpiProfitSub);
            this.pnlKpiProfit.Controls.Add(this.lblKpiProfitVal);
            this.pnlKpiProfit.Controls.Add(this.lblKpiProfitTitle);
            this.pnlKpiProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiProfit.Location = new System.Drawing.Point(589, 13);
            this.pnlKpiProfit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlKpiProfit.Name = "pnlKpiProfit";
            this.pnlKpiProfit.Padding = new System.Windows.Forms.Padding(10, 8, 10, 6);
            this.pnlKpiProfit.Size = new System.Drawing.Size(282, 99);
            this.pnlKpiProfit.TabIndex = 1;
            // 
            // lblKpiProfitSub
            // 
            this.lblKpiProfitSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProfitSub.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblKpiProfitSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiProfitSub.Location = new System.Drawing.Point(10, 68);
            this.lblKpiProfitSub.Name = "lblKpiProfitSub";
            this.lblKpiProfitSub.Size = new System.Drawing.Size(262, 22);
            this.lblKpiProfitSub.TabIndex = 2;
            this.lblKpiProfitSub.Text = "هامش الربح: 0.0%";
            this.lblKpiProfitSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiProfitVal
            // 
            this.lblKpiProfitVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProfitVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblKpiProfitVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblKpiProfitVal.Location = new System.Drawing.Point(10, 30);
            this.lblKpiProfitVal.Name = "lblKpiProfitVal";
            this.lblKpiProfitVal.Size = new System.Drawing.Size(262, 38);
            this.lblKpiProfitVal.TabIndex = 1;
            this.lblKpiProfitVal.Text = "+0.00 ج.م";
            this.lblKpiProfitVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiProfitTitle
            // 
            this.lblKpiProfitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiProfitTitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiProfitTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiProfitTitle.Location = new System.Drawing.Point(10, 8);
            this.lblKpiProfitTitle.Name = "lblKpiProfitTitle";
            this.lblKpiProfitTitle.Size = new System.Drawing.Size(262, 22);
            this.lblKpiProfitTitle.TabIndex = 0;
            this.lblKpiProfitTitle.Text = "صافي الربح والمكسب:";
            this.lblKpiProfitTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpiPurchases
            // 
            this.pnlKpiPurchases.BackColor = System.Drawing.Color.White;
            this.pnlKpiPurchases.Controls.Add(this.lblKpiPurchasesSub);
            this.pnlKpiPurchases.Controls.Add(this.lblKpiPurchasesVal);
            this.pnlKpiPurchases.Controls.Add(this.lblKpiPurchasesTitle);
            this.pnlKpiPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiPurchases.Location = new System.Drawing.Point(297, 13);
            this.pnlKpiPurchases.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlKpiPurchases.Name = "pnlKpiPurchases";
            this.pnlKpiPurchases.Padding = new System.Windows.Forms.Padding(10, 8, 10, 6);
            this.pnlKpiPurchases.Size = new System.Drawing.Size(282, 99);
            this.pnlKpiPurchases.TabIndex = 2;
            // 
            // lblKpiPurchasesSub
            // 
            this.lblKpiPurchasesSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiPurchasesSub.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblKpiPurchasesSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiPurchasesSub.Location = new System.Drawing.Point(10, 68);
            this.lblKpiPurchasesSub.Name = "lblKpiPurchasesSub";
            this.lblKpiPurchasesSub.Size = new System.Drawing.Size(262, 22);
            this.lblKpiPurchasesSub.TabIndex = 2;
            this.lblKpiPurchasesSub.Text = "تكلفة المباع: 0.00 ج.م";
            this.lblKpiPurchasesSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiPurchasesVal
            // 
            this.lblKpiPurchasesVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiPurchasesVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblKpiPurchasesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblKpiPurchasesVal.Location = new System.Drawing.Point(10, 30);
            this.lblKpiPurchasesVal.Name = "lblKpiPurchasesVal";
            this.lblKpiPurchasesVal.Size = new System.Drawing.Size(262, 38);
            this.lblKpiPurchasesVal.TabIndex = 1;
            this.lblKpiPurchasesVal.Text = "0.00 ج.م";
            this.lblKpiPurchasesVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiPurchasesTitle
            // 
            this.lblKpiPurchasesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiPurchasesTitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiPurchasesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiPurchasesTitle.Location = new System.Drawing.Point(10, 8);
            this.lblKpiPurchasesTitle.Name = "lblKpiPurchasesTitle";
            this.lblKpiPurchasesTitle.Size = new System.Drawing.Size(262, 22);
            this.lblKpiPurchasesTitle.TabIndex = 0;
            this.lblKpiPurchasesTitle.Text = "إجمالي المشتريات (الشراء):";
            this.lblKpiPurchasesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKpiSales
            // 
            this.pnlKpiSales.BackColor = System.Drawing.Color.White;
            this.pnlKpiSales.Controls.Add(this.lblKpiSalesSub);
            this.pnlKpiSales.Controls.Add(this.lblKpiSalesVal);
            this.pnlKpiSales.Controls.Add(this.lblKpiSalesTitle);
            this.pnlKpiSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiSales.Location = new System.Drawing.Point(0, 13);
            this.pnlKpiSales.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.pnlKpiSales.Name = "pnlKpiSales";
            this.pnlKpiSales.Padding = new System.Windows.Forms.Padding(10, 8, 10, 6);
            this.pnlKpiSales.Size = new System.Drawing.Size(287, 99);
            this.pnlKpiSales.TabIndex = 3;
            // 
            // lblKpiSalesSub
            // 
            this.lblKpiSalesSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiSalesSub.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblKpiSalesSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiSalesSub.Location = new System.Drawing.Point(10, 68);
            this.lblKpiSalesSub.Name = "lblKpiSalesSub";
            this.lblKpiSalesSub.Size = new System.Drawing.Size(267, 22);
            this.lblKpiSalesSub.TabIndex = 2;
            this.lblKpiSalesSub.Text = "عدد الفواتير: 0";
            this.lblKpiSalesSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiSalesVal
            // 
            this.lblKpiSalesVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiSalesVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblKpiSalesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblKpiSalesVal.Location = new System.Drawing.Point(10, 30);
            this.lblKpiSalesVal.Name = "lblKpiSalesVal";
            this.lblKpiSalesVal.Size = new System.Drawing.Size(267, 38);
            this.lblKpiSalesVal.TabIndex = 1;
            this.lblKpiSalesVal.Text = "0.00 ج.م";
            this.lblKpiSalesVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiSalesTitle
            // 
            this.lblKpiSalesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiSalesTitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpiSalesTitle.Location = new System.Drawing.Point(10, 8);
            this.lblKpiSalesTitle.Name = "lblKpiSalesTitle";
            this.lblKpiSalesTitle.Size = new System.Drawing.Size(267, 22);
            this.lblKpiSalesTitle.TabIndex = 0;
            this.lblKpiSalesTitle.Text = "إجمالي المبيعات (البيع):";
            this.lblKpiSalesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.pnlCardRecentSales, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.pnlCardTopProducts, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.pnlCardLowStock, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(16, 209);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1168, 495);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // pnlCardRecentSales
            // 
            this.pnlCardRecentSales.BackColor = System.Drawing.Color.White;
            this.pnlCardRecentSales.Controls.Add(this.dgvRecentSales);
            this.pnlCardRecentSales.Controls.Add(this.lblCardRecentSalesTitle);
            this.pnlCardRecentSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRecentSales.Location = new System.Drawing.Point(589, 0);
            this.pnlCardRecentSales.Margin = new System.Windows.Forms.Padding(0, 0, 5, 10);
            this.pnlCardRecentSales.Name = "pnlCardRecentSales";
            this.pnlCardRecentSales.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCardRecentSales.Size = new System.Drawing.Size(579, 247);
            this.pnlCardRecentSales.TabIndex = 0;
            // 
            // dgvRecentSales
            // 
            this.dgvRecentSales.AllowUserToAddRows = false;
            this.dgvRecentSales.AllowUserToDeleteRows = false;
            this.dgvRecentSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentSales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvRecentSales.ColumnHeadersHeight = 48;
            this.dgvRecentSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentSales.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvRecentSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentSales.EnableHeadersVisualStyles = false;
            this.dgvRecentSales.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvRecentSales.Location = new System.Drawing.Point(12, 40);
            this.dgvRecentSales.MultiSelect = false;
            this.dgvRecentSales.Name = "dgvRecentSales";
            this.dgvRecentSales.ReadOnly = true;
            this.dgvRecentSales.RowHeadersVisible = false;
            this.dgvRecentSales.RowHeadersWidth = 51;
            this.dgvRecentSales.RowTemplate.Height = 40;
            this.dgvRecentSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentSales.Size = new System.Drawing.Size(555, 195);
            this.dgvRecentSales.TabIndex = 1;
            // 
            // lblCardRecentSalesTitle
            // 
            this.lblCardRecentSalesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardRecentSalesTitle.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCardRecentSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCardRecentSalesTitle.Location = new System.Drawing.Point(12, 12);
            this.lblCardRecentSalesTitle.Name = "lblCardRecentSalesTitle";
            this.lblCardRecentSalesTitle.Size = new System.Drawing.Size(555, 28);
            this.lblCardRecentSalesTitle.TabIndex = 0;
            this.lblCardRecentSalesTitle.Text = "📋 أحدث المعاملات والفواتير الصادرة";
            this.lblCardRecentSalesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCardTopProducts
            // 
            this.pnlCardTopProducts.BackColor = System.Drawing.Color.White;
            this.pnlCardTopProducts.Controls.Add(this.dgvTopProducts);
            this.pnlCardTopProducts.Controls.Add(this.lblCardTopProductsTitle);
            this.pnlCardTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTopProducts.Location = new System.Drawing.Point(0, 0);
            this.pnlCardTopProducts.Margin = new System.Windows.Forms.Padding(5, 0, 0, 10);
            this.pnlCardTopProducts.Name = "pnlCardTopProducts";
            this.pnlCardTopProducts.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCardTopProducts.Size = new System.Drawing.Size(579, 247);
            this.pnlCardTopProducts.TabIndex = 1;
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTopProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvTopProducts.ColumnHeadersHeight = 48;
            this.dgvTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopProducts.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProducts.EnableHeadersVisualStyles = false;
            this.dgvTopProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvTopProducts.Location = new System.Drawing.Point(12, 40);
            this.dgvTopProducts.MultiSelect = false;
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.RowHeadersWidth = 51;
            this.dgvTopProducts.RowTemplate.Height = 40;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(555, 195);
            this.dgvTopProducts.TabIndex = 1;
            // 
            // lblCardTopProductsTitle
            // 
            this.lblCardTopProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardTopProductsTitle.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCardTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCardTopProductsTitle.Location = new System.Drawing.Point(12, 12);
            this.lblCardTopProductsTitle.Name = "lblCardTopProductsTitle";
            this.lblCardTopProductsTitle.Size = new System.Drawing.Size(555, 28);
            this.lblCardTopProductsTitle.TabIndex = 0;
            this.lblCardTopProductsTitle.Text = "🔥 الأصناف الأكثر مبيعاً والأعلى تحقيقاً للإيراد";
            this.lblCardTopProductsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCardLowStock
            // 
            this.pnlCardLowStock.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelMain.SetColumnSpan(this.pnlCardLowStock, 2);
            this.pnlCardLowStock.Controls.Add(this.dgvLowStock);
            this.pnlCardLowStock.Controls.Add(this.lblCardLowStockTitle);
            this.pnlCardLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardLowStock.Location = new System.Drawing.Point(0, 257);
            this.pnlCardLowStock.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCardLowStock.Name = "pnlCardLowStock";
            this.pnlCardLowStock.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCardLowStock.Size = new System.Drawing.Size(1168, 238);
            this.pnlCardLowStock.TabIndex = 2;
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AllowUserToDeleteRows = false;
            this.dgvLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvLowStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLowStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLowStock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle17.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLowStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvLowStock.ColumnHeadersHeight = 48;
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLowStock.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLowStock.EnableHeadersVisualStyles = false;
            this.dgvLowStock.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvLowStock.Location = new System.Drawing.Point(12, 40);
            this.dgvLowStock.MultiSelect = false;
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.RowHeadersVisible = false;
            this.dgvLowStock.RowHeadersWidth = 51;
            this.dgvLowStock.RowTemplate.Height = 40;
            this.dgvLowStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLowStock.Size = new System.Drawing.Size(1144, 186);
            this.dgvLowStock.TabIndex = 1;
            // 
            // lblCardLowStockTitle
            // 
            this.lblCardLowStockTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardLowStockTitle.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCardLowStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblCardLowStockTitle.Location = new System.Drawing.Point(12, 12);
            this.lblCardLowStockTitle.Name = "lblCardLowStockTitle";
            this.lblCardLowStockTitle.Size = new System.Drawing.Size(1144, 28);
            this.lblCardLowStockTitle.TabIndex = 0;
            this.lblCardLowStockTitle.Text = "تنبيهات النواقص ورواكد المخزون (تحت حد الأمان)";
            this.lblCardLowStockTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.tlpKpis);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DashboardForm";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "لوحة التحكم العامة";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.flpFilters.ResumeLayout(false);
            this.flpFilters.PerformLayout();
            this.pnlTitles.ResumeLayout(false);
            this.tlpKpis.ResumeLayout(false);
            this.pnlKpiProducts.ResumeLayout(false);
            this.pnlKpiProfit.ResumeLayout(false);
            this.pnlKpiPurchases.ResumeLayout(false);
            this.pnlKpiSales.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.pnlCardRecentSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).EndInit();
            this.pnlCardTopProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.pnlCardLowStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTitles;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flpFilters;
        private System.Windows.Forms.Label lblFilterLabel;
        private System.Windows.Forms.Button btnFilterToday;
        private System.Windows.Forms.Button btnFilterWeek;
        private System.Windows.Forms.Button btnFilterMonth;
        private System.Windows.Forms.Button btnFilterAll;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpKpis;
        private System.Windows.Forms.Panel pnlKpiSales;
        private System.Windows.Forms.Label lblKpiSalesTitle;
        private System.Windows.Forms.Label lblKpiSalesVal;
        private System.Windows.Forms.Label lblKpiSalesSub;
        private System.Windows.Forms.Panel pnlKpiPurchases;
        private System.Windows.Forms.Label lblKpiPurchasesTitle;
        private System.Windows.Forms.Label lblKpiPurchasesVal;
        private System.Windows.Forms.Label lblKpiPurchasesSub;
        private System.Windows.Forms.Panel pnlKpiProfit;
        private System.Windows.Forms.Label lblKpiProfitTitle;
        private System.Windows.Forms.Label lblKpiProfitVal;
        private System.Windows.Forms.Label lblKpiProfitSub;
        private System.Windows.Forms.Panel pnlKpiProducts;
        private System.Windows.Forms.Label lblKpiProductsTitle;
        private System.Windows.Forms.Label lblKpiProductsVal;
        private System.Windows.Forms.Label lblKpiProductsSub;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel pnlCardTopProducts;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Label lblCardTopProductsTitle;
        private System.Windows.Forms.Panel pnlCardRecentSales;
        private System.Windows.Forms.DataGridView dgvRecentSales;
        private System.Windows.Forms.Label lblCardRecentSalesTitle;
        private System.Windows.Forms.Panel pnlCardLowStock;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Label lblCardLowStockTitle;
    }
}
