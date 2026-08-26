namespace POS
{
    partial class SalesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTopCards = new System.Windows.Forms.Panel();
            this.tlpCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardItems = new System.Windows.Forms.Panel();
            this.lblCardItemsVal = new System.Windows.Forms.Label();
            this.lblCardItemsTitle = new System.Windows.Forms.Label();
            this.pnlCardCount = new System.Windows.Forms.Panel();
            this.lblCardCountVal = new System.Windows.Forms.Label();
            this.lblCardCountTitle = new System.Windows.Forms.Label();
            this.pnlCardRevenue = new System.Windows.Forms.Panel();
            this.lblCardRevenueVal = new System.Windows.Forms.Label();
            this.lblCardRevenueTitle = new System.Windows.Forms.Label();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.splitSales = new System.Windows.Forms.SplitContainer();
            this.pnlSalesList = new System.Windows.Forms.Panel();
            this.dgvSalesList = new System.Windows.Forms.DataGridView();
            this.lblSalesListTitle = new System.Windows.Forms.Label();
            this.pnlSaleDetails = new System.Windows.Forms.Panel();
            this.dgvSaleDetails = new System.Windows.Forms.DataGridView();
            this.pnlDetailsBottom = new System.Windows.Forms.Panel();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.btnExportImage = new System.Windows.Forms.Button();
            this.lblSaleDetailsTitle = new System.Windows.Forms.Label();
            this.pnlTopCards.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.pnlCardItems.SuspendLayout();
            this.pnlCardCount.SuspendLayout();
            this.pnlCardRevenue.SuspendLayout();
            this.pnlFilterBar.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSales)).BeginInit();
            this.splitSales.Panel1.SuspendLayout();
            this.splitSales.Panel2.SuspendLayout();
            this.splitSales.SuspendLayout();
            this.pnlSalesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesList)).BeginInit();
            this.pnlSaleDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleDetails)).BeginInit();
            this.pnlDetailsBottom.SuspendLayout();
            this.flpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopCards
            // 
            this.pnlTopCards.Controls.Add(this.tlpCards);
            this.pnlTopCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCards.Location = new System.Drawing.Point(14, 14);
            this.pnlTopCards.Name = "pnlTopCards";
            this.pnlTopCards.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlTopCards.Size = new System.Drawing.Size(1172, 95);
            this.pnlTopCards.TabIndex = 0;
            // 
            // tlpCards
            // 
            this.tlpCards.ColumnCount = 3;
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCards.Controls.Add(this.pnlCardItems, 0, 0);
            this.tlpCards.Controls.Add(this.pnlCardCount, 1, 0);
            this.tlpCards.Controls.Add(this.pnlCardRevenue, 2, 0);
            this.tlpCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCards.Location = new System.Drawing.Point(0, 0);
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.RowCount = 1;
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCards.Size = new System.Drawing.Size(1172, 85);
            this.tlpCards.TabIndex = 0;
            // 
            // pnlCardItems
            // 
            this.pnlCardItems.BackColor = System.Drawing.Color.White;
            this.pnlCardItems.Controls.Add(this.lblCardItemsVal);
            this.pnlCardItems.Controls.Add(this.lblCardItemsTitle);
            this.pnlCardItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardItems.Location = new System.Drawing.Point(787, 0);
            this.pnlCardItems.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.pnlCardItems.Name = "pnlCardItems";
            this.pnlCardItems.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlCardItems.Size = new System.Drawing.Size(385, 85);
            this.pnlCardItems.TabIndex = 2;
            // 
            // lblCardItemsVal
            // 
            this.lblCardItemsVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardItemsVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblCardItemsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblCardItemsVal.Location = new System.Drawing.Point(12, 34);
            this.lblCardItemsVal.Name = "lblCardItemsVal";
            this.lblCardItemsVal.Size = new System.Drawing.Size(361, 41);
            this.lblCardItemsVal.TabIndex = 1;
            this.lblCardItemsVal.Text = "0 صنف";
            this.lblCardItemsVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardItemsTitle
            // 
            this.lblCardItemsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardItemsTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardItemsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardItemsTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardItemsTitle.Name = "lblCardItemsTitle";
            this.lblCardItemsTitle.Size = new System.Drawing.Size(361, 24);
            this.lblCardItemsTitle.TabIndex = 0;
            this.lblCardItemsTitle.Text = "📦 إجمالي الأصناف المباعة بالفترة:";
            this.lblCardItemsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCardCount
            // 
            this.pnlCardCount.BackColor = System.Drawing.Color.White;
            this.pnlCardCount.Controls.Add(this.lblCardCountVal);
            this.pnlCardCount.Controls.Add(this.lblCardCountTitle);
            this.pnlCardCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardCount.Location = new System.Drawing.Point(397, 0);
            this.pnlCardCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pnlCardCount.Name = "pnlCardCount";
            this.pnlCardCount.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlCardCount.Size = new System.Drawing.Size(380, 85);
            this.pnlCardCount.TabIndex = 1;
            // 
            // lblCardCountVal
            // 
            this.lblCardCountVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardCountVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblCardCountVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCardCountVal.Location = new System.Drawing.Point(12, 34);
            this.lblCardCountVal.Name = "lblCardCountVal";
            this.lblCardCountVal.Size = new System.Drawing.Size(356, 41);
            this.lblCardCountVal.TabIndex = 1;
            this.lblCardCountVal.Text = "0 فاتورة";
            this.lblCardCountVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardCountTitle
            // 
            this.lblCardCountTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardCountTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardCountTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardCountTitle.Name = "lblCardCountTitle";
            this.lblCardCountTitle.Size = new System.Drawing.Size(356, 24);
            this.lblCardCountTitle.TabIndex = 0;
            this.lblCardCountTitle.Text = "🧾 إجمالي عدد فواتير المبيعات:";
            this.lblCardCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCardRevenue
            // 
            this.pnlCardRevenue.BackColor = System.Drawing.Color.White;
            this.pnlCardRevenue.Controls.Add(this.lblCardRevenueVal);
            this.pnlCardRevenue.Controls.Add(this.lblCardRevenueTitle);
            this.pnlCardRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRevenue.Location = new System.Drawing.Point(0, 0);
            this.pnlCardRevenue.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlCardRevenue.Name = "pnlCardRevenue";
            this.pnlCardRevenue.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlCardRevenue.Size = new System.Drawing.Size(387, 85);
            this.pnlCardRevenue.TabIndex = 0;
            // 
            // lblCardRevenueVal
            // 
            this.lblCardRevenueVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardRevenueVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblCardRevenueVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblCardRevenueVal.Location = new System.Drawing.Point(12, 34);
            this.lblCardRevenueVal.Name = "lblCardRevenueVal";
            this.lblCardRevenueVal.Size = new System.Drawing.Size(363, 41);
            this.lblCardRevenueVal.TabIndex = 1;
            this.lblCardRevenueVal.Text = "0.00 ج.م";
            this.lblCardRevenueVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardRevenueTitle
            // 
            this.lblCardRevenueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardRevenueTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardRevenueTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardRevenueTitle.Name = "lblCardRevenueTitle";
            this.lblCardRevenueTitle.Size = new System.Drawing.Size(363, 24);
            this.lblCardRevenueTitle.TabIndex = 0;
            this.lblCardRevenueTitle.Text = "💵 إجمالي إيراد المبيعات المحصل:";
            this.lblCardRevenueTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilterBar
            // 
            this.pnlFilterBar.BackColor = System.Drawing.Color.White;
            this.pnlFilterBar.Controls.Add(this.tlpFilter);
            this.pnlFilterBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterBar.Location = new System.Drawing.Point(14, 109);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlFilterBar.Size = new System.Drawing.Size(1172, 54);
            this.pnlFilterBar.TabIndex = 1;
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 5;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpFilter.Controls.Add(this.btnRefresh, 4, 0);
            this.tlpFilter.Controls.Add(this.txtSearch, 3, 0);
            this.tlpFilter.Controls.Add(this.lblSearch, 2, 0);
            this.tlpFilter.Controls.Add(this.cmbPeriod, 1, 0);
            this.tlpFilter.Controls.Add(this.lblPeriod, 0, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(12, 8);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(1148, 38);
            this.tlpFilter.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRefresh.Location = new System.Drawing.Point(0, 3);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 32);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 تحديث";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(130, 5);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(623, 27);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSearch.Location = new System.Drawing.Point(761, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(94, 38);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "بحث سريع:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.BackColor = System.Drawing.Color.White;
            this.cmbPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriod.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Items.AddRange(new object[] {
            "اليوم",
            "هذا الأسبوع",
            "هذا الشهر",
            "كل الفترات"});
            this.cmbPeriod.Location = new System.Drawing.Point(863, 5);
            this.cmbPeriod.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(170, 27);
            this.cmbPeriod.TabIndex = 1;
            this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbPeriod_SelectedIndexChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPeriod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPeriod.Location = new System.Drawing.Point(1041, 0);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(104, 38);
            this.lblPeriod.TabIndex = 0;
            this.lblPeriod.Text = "الفترة الزمنية:";
            this.lblPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitSales
            // 
            this.splitSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSales.Location = new System.Drawing.Point(14, 163);
            this.splitSales.Name = "splitSales";
            // 
            // splitSales.Panel1
            // 
            this.splitSales.Panel1.Controls.Add(this.pnlSalesList);
            this.splitSales.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitSales.Panel2
            // 
            this.splitSales.Panel2.Controls.Add(this.pnlSaleDetails);
            this.splitSales.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitSales.Size = new System.Drawing.Size(1172, 543);
            this.splitSales.SplitterDistance = 590;
            this.splitSales.TabIndex = 2;
            // 
            // pnlSalesList
            // 
            this.pnlSalesList.BackColor = System.Drawing.Color.White;
            this.pnlSalesList.Controls.Add(this.dgvSalesList);
            this.pnlSalesList.Controls.Add(this.lblSalesListTitle);
            this.pnlSalesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSalesList.Location = new System.Drawing.Point(0, 0);
            this.pnlSalesList.Name = "pnlSalesList";
            this.pnlSalesList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlSalesList.Size = new System.Drawing.Size(590, 543);
            this.pnlSalesList.TabIndex = 0;
            // 
            // dgvSalesList
            // 
            this.dgvSalesList.AllowUserToAddRows = false;
            this.dgvSalesList.AllowUserToDeleteRows = false;
            this.dgvSalesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalesList.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSalesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSalesList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSalesList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSalesList.ColumnHeadersHeight = 44;
            this.dgvSalesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSalesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalesList.EnableHeadersVisualStyles = false;
            this.dgvSalesList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvSalesList.Location = new System.Drawing.Point(14, 46);
            this.dgvSalesList.MultiSelect = false;
            this.dgvSalesList.Name = "dgvSalesList";
            this.dgvSalesList.ReadOnly = true;
            this.dgvSalesList.RowHeadersVisible = false;
            this.dgvSalesList.RowHeadersWidth = 51;
            this.dgvSalesList.RowTemplate.Height = 38;
            this.dgvSalesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSalesList.Size = new System.Drawing.Size(562, 483);
            this.dgvSalesList.TabIndex = 1;
            this.dgvSalesList.SelectionChanged += new System.EventHandler(this.dgvSalesList_SelectionChanged);
            // 
            // lblSalesListTitle
            // 
            this.lblSalesListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSalesListTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblSalesListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSalesListTitle.Location = new System.Drawing.Point(14, 14);
            this.lblSalesListTitle.Name = "lblSalesListTitle";
            this.lblSalesListTitle.Size = new System.Drawing.Size(562, 32);
            this.lblSalesListTitle.TabIndex = 0;
            this.lblSalesListTitle.Text = "📋 قائمة فواتير المبيعات المسجلة";
            this.lblSalesListTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSaleDetails
            // 
            this.pnlSaleDetails.BackColor = System.Drawing.Color.White;
            this.pnlSaleDetails.Controls.Add(this.dgvSaleDetails);
            this.pnlSaleDetails.Controls.Add(this.pnlDetailsBottom);
            this.pnlSaleDetails.Controls.Add(this.lblSaleDetailsTitle);
            this.pnlSaleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaleDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlSaleDetails.Name = "pnlSaleDetails";
            this.pnlSaleDetails.Padding = new System.Windows.Forms.Padding(14);
            this.pnlSaleDetails.Size = new System.Drawing.Size(578, 543);
            this.pnlSaleDetails.TabIndex = 0;
            // 
            // dgvSaleDetails
            // 
            this.dgvSaleDetails.AllowUserToAddRows = false;
            this.dgvSaleDetails.AllowUserToDeleteRows = false;
            this.dgvSaleDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvSaleDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSaleDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSaleDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSaleDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSaleDetails.ColumnHeadersHeight = 44;
            this.dgvSaleDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSaleDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSaleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSaleDetails.EnableHeadersVisualStyles = false;
            this.dgvSaleDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvSaleDetails.Location = new System.Drawing.Point(14, 46);
            this.dgvSaleDetails.MultiSelect = false;
            this.dgvSaleDetails.Name = "dgvSaleDetails";
            this.dgvSaleDetails.ReadOnly = true;
            this.dgvSaleDetails.RowHeadersVisible = false;
            this.dgvSaleDetails.RowHeadersWidth = 51;
            this.dgvSaleDetails.RowTemplate.Height = 38;
            this.dgvSaleDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaleDetails.Size = new System.Drawing.Size(550, 433);
            this.dgvSaleDetails.TabIndex = 1;
            // 
            // pnlDetailsBottom
            // 
            this.pnlDetailsBottom.Controls.Add(this.flpActions);
            this.pnlDetailsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetailsBottom.Location = new System.Drawing.Point(14, 479);
            this.pnlDetailsBottom.Name = "pnlDetailsBottom";
            this.pnlDetailsBottom.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlDetailsBottom.Size = new System.Drawing.Size(550, 50);
            this.pnlDetailsBottom.TabIndex = 2;
            // 
            // flpActions
            // 
            this.flpActions.Controls.Add(this.btnReturn);
            this.flpActions.Controls.Add(this.btnPrintReceipt);
            this.flpActions.Controls.Add(this.btnExportImage);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActions.Location = new System.Drawing.Point(0, 8);
            this.flpActions.Name = "flpActions";
            this.flpActions.Size = new System.Drawing.Size(550, 42);
            this.flpActions.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(377, 3);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(170, 36);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "🔄 عمل إرجاع / مرتجع";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPrintReceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintReceipt.FlatAppearance.BorderSize = 0;
            this.btnPrintReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrintReceipt.ForeColor = System.Drawing.Color.White;
            this.btnPrintReceipt.Location = new System.Drawing.Point(197, 3);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(174, 36);
            this.btnPrintReceipt.TabIndex = 0;
            this.btnPrintReceipt.Text = "🖨️ طباعة إيصال الفاتورة";
            this.btnPrintReceipt.UseVisualStyleBackColor = false;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // btnExportImage
            // 
            this.btnExportImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnExportImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnExportImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportImage.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnExportImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnExportImage.Location = new System.Drawing.Point(27, 3);
            this.btnExportImage.Name = "btnExportImage";
            this.btnExportImage.Size = new System.Drawing.Size(164, 36);
            this.btnExportImage.TabIndex = 1;
            this.btnExportImage.Text = "🖼️ تصدير كصورة PNG";
            this.btnExportImage.UseVisualStyleBackColor = false;
            this.btnExportImage.Click += new System.EventHandler(this.btnExportImage_Click);
            // 
            // lblSaleDetailsTitle
            // 
            this.lblSaleDetailsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSaleDetailsTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblSaleDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSaleDetailsTitle.Location = new System.Drawing.Point(14, 14);
            this.lblSaleDetailsTitle.Name = "lblSaleDetailsTitle";
            this.lblSaleDetailsTitle.Size = new System.Drawing.Size(550, 32);
            this.lblSaleDetailsTitle.TabIndex = 0;
            this.lblSaleDetailsTitle.Text = "📦 تفاصيل وأصناف الفاتورة المحددة";
            this.lblSaleDetailsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.splitSales);
            this.Controls.Add(this.pnlFilterBar);
            this.Controls.Add(this.pnlTopCards);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesForm";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "إدارة وسجل المبيعات";
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.pnlTopCards.ResumeLayout(false);
            this.tlpCards.ResumeLayout(false);
            this.pnlCardItems.ResumeLayout(false);
            this.pnlCardCount.ResumeLayout(false);
            this.pnlCardRevenue.ResumeLayout(false);
            this.pnlFilterBar.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.tlpFilter.PerformLayout();
            this.splitSales.Panel1.ResumeLayout(false);
            this.splitSales.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSales)).EndInit();
            this.splitSales.ResumeLayout(false);
            this.pnlSalesList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesList)).EndInit();
            this.pnlSaleDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleDetails)).EndInit();
            this.pnlDetailsBottom.ResumeLayout(false);
            this.flpActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopCards;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Label lblCardRevenueTitle;
        private System.Windows.Forms.Label lblCardRevenueVal;
        private System.Windows.Forms.Panel pnlCardCount;
        private System.Windows.Forms.Label lblCardCountTitle;
        private System.Windows.Forms.Label lblCardCountVal;
        private System.Windows.Forms.Panel pnlCardItems;
        private System.Windows.Forms.Label lblCardItemsTitle;
        private System.Windows.Forms.Label lblCardItemsVal;
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cmbPeriod;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer splitSales;
        private System.Windows.Forms.Panel pnlSalesList;
        private System.Windows.Forms.Label lblSalesListTitle;
        private System.Windows.Forms.DataGridView dgvSalesList;
        private System.Windows.Forms.Panel pnlSaleDetails;
        private System.Windows.Forms.Label lblSaleDetailsTitle;
        private System.Windows.Forms.DataGridView dgvSaleDetails;
        private System.Windows.Forms.Panel pnlDetailsBottom;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.Button btnExportImage;
    }
}
