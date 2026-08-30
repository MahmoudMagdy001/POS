namespace POS
{
    partial class POSForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBarcodeTop = new System.Windows.Forms.Panel();
            this.tlpBarcodeHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblBarcodeTitle = new System.Windows.Forms.Label();
            this.txtBarcodeScan = new System.Windows.Forms.TextBox();
            this.lblBarcodeHint = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProductsCatalog = new System.Windows.Forms.Panel();
            this.dgvProductsCatalog = new System.Windows.Forms.DataGridView();
            this.pnlProductSearch = new System.Windows.Forms.Panel();
            this.tlpSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblCategoryFilter = new System.Windows.Forms.Label();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.pnlCartSummary = new System.Windows.Forms.Panel();
            this.tlpCartActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.tlpPaymentGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblSubtotalVal = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblCashPaid = new System.Windows.Forms.Label();
            this.numCashPaid = new System.Windows.Forms.NumericUpDown();
            this.lblChangeDue = new System.Windows.Forms.Label();
            this.lblChangeDueVal = new System.Windows.Forms.Label();
            this.lblVat = new System.Windows.Forms.Label();
            this.lblVatVal = new System.Windows.Forms.Label();
            this.pnlFinalTotal = new System.Windows.Forms.Panel();
            this.lblFinalTotalVal = new System.Windows.Forms.Label();
            this.lblFinalTotalTitle = new System.Windows.Forms.Label();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.pnlShiftBanner = new System.Windows.Forms.Panel();
            this.btnShiftBannerAction = new System.Windows.Forms.Button();
            this.lblShiftBannerText = new System.Windows.Forms.Label();
            this.lblShiftBannerIcon = new System.Windows.Forms.Label();
            this.pnlBarcodeTop.SuspendLayout();
            this.tlpBarcodeHeader.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlProductsCatalog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsCatalog)).BeginInit();
            this.pnlProductSearch.SuspendLayout();
            this.tlpSearch.SuspendLayout();
            this.pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartSummary.SuspendLayout();
            this.tlpCartActions.SuspendLayout();
            this.pnlPaymentDetails.SuspendLayout();
            this.tlpPaymentGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaid)).BeginInit();
            this.pnlFinalTotal.SuspendLayout();
            this.pnlShiftBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBarcodeTop
            // 
            this.pnlBarcodeTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlBarcodeTop.Controls.Add(this.tlpBarcodeHeader);
            this.pnlBarcodeTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarcodeTop.Location = new System.Drawing.Point(14, 58);
            this.pnlBarcodeTop.Name = "pnlBarcodeTop";
            this.pnlBarcodeTop.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlBarcodeTop.Size = new System.Drawing.Size(1172, 80);
            this.pnlBarcodeTop.TabIndex = 0;
            // 
            // tlpBarcodeHeader
            // 
            this.tlpBarcodeHeader.ColumnCount = 3;
            this.tlpBarcodeHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tlpBarcodeHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tlpBarcodeHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBarcodeHeader.Controls.Add(this.lblBarcodeTitle, 0, 0);
            this.tlpBarcodeHeader.Controls.Add(this.txtBarcodeScan, 1, 0);
            this.tlpBarcodeHeader.Controls.Add(this.lblBarcodeHint, 2, 0);
            this.tlpBarcodeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBarcodeHeader.Location = new System.Drawing.Point(12, 8);
            this.tlpBarcodeHeader.Name = "tlpBarcodeHeader";
            this.tlpBarcodeHeader.RowCount = 1;
            this.tlpBarcodeHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBarcodeHeader.Size = new System.Drawing.Size(1148, 64);
            this.tlpBarcodeHeader.TabIndex = 0;
            // 
            // lblBarcodeTitle
            // 
            this.lblBarcodeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarcodeTitle.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblBarcodeTitle.ForeColor = System.Drawing.Color.White;
            this.lblBarcodeTitle.Location = new System.Drawing.Point(926, 0);
            this.lblBarcodeTitle.Name = "lblBarcodeTitle";
            this.lblBarcodeTitle.Size = new System.Drawing.Size(219, 64);
            this.lblBarcodeTitle.TabIndex = 0;
            this.lblBarcodeTitle.Text = "قارئ الباركود السريع (F2):";
            this.lblBarcodeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBarcodeTitle.Click += new System.EventHandler(this.lblBarcodeTitle_Click);
            // 
            // txtBarcodeScan
            // 
            this.txtBarcodeScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcodeScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtBarcodeScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcodeScan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtBarcodeScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.txtBarcodeScan.Location = new System.Drawing.Point(586, 16);
            this.txtBarcodeScan.Name = "txtBarcodeScan";
            this.txtBarcodeScan.Size = new System.Drawing.Size(334, 32);
            this.txtBarcodeScan.TabIndex = 1;
            this.txtBarcodeScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcodeScan_KeyDown);
            // 
            // lblBarcodeHint
            // 
            this.lblBarcodeHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarcodeHint.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblBarcodeHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBarcodeHint.Location = new System.Drawing.Point(3, 0);
            this.lblBarcodeHint.Name = "lblBarcodeHint";
            this.lblBarcodeHint.Size = new System.Drawing.Size(577, 64);
            this.lblBarcodeHint.TabIndex = 2;
            this.lblBarcodeHint.Text = "امسح باركود المنتج واضغط Enter لإضافته مباشرة إلى سلة المبيعات";
            this.lblBarcodeHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMain.Controls.Add(this.pnlProductsCatalog, 0, 0);
            this.tlpMain.Controls.Add(this.pnlCart, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(14, 138);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1172, 568);
            this.tlpMain.TabIndex = 1;
            // 
            // pnlProductsCatalog
            // 
            this.pnlProductsCatalog.BackColor = System.Drawing.Color.White;
            this.pnlProductsCatalog.Controls.Add(this.dgvProductsCatalog);
            this.pnlProductsCatalog.Controls.Add(this.pnlProductSearch);
            this.pnlProductsCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductsCatalog.Location = new System.Drawing.Point(645, 13);
            this.pnlProductsCatalog.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlProductsCatalog.Name = "pnlProductsCatalog";
            this.pnlProductsCatalog.Padding = new System.Windows.Forms.Padding(12);
            this.pnlProductsCatalog.Size = new System.Drawing.Size(524, 552);
            this.pnlProductsCatalog.TabIndex = 0;
            // 
            // dgvProductsCatalog
            // 
            this.dgvProductsCatalog.AllowUserToAddRows = false;
            this.dgvProductsCatalog.AllowUserToDeleteRows = false;
            this.dgvProductsCatalog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductsCatalog.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductsCatalog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductsCatalog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProductsCatalog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductsCatalog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvProductsCatalog.ColumnHeadersHeight = 38;
            this.dgvProductsCatalog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductsCatalog.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvProductsCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductsCatalog.EnableHeadersVisualStyles = false;
            this.dgvProductsCatalog.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvProductsCatalog.Location = new System.Drawing.Point(12, 70);
            this.dgvProductsCatalog.MultiSelect = false;
            this.dgvProductsCatalog.Name = "dgvProductsCatalog";
            this.dgvProductsCatalog.ReadOnly = true;
            this.dgvProductsCatalog.RowHeadersVisible = false;
            this.dgvProductsCatalog.RowHeadersWidth = 51;
            this.dgvProductsCatalog.RowTemplate.Height = 36;
            this.dgvProductsCatalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductsCatalog.Size = new System.Drawing.Size(500, 470);
            this.dgvProductsCatalog.TabIndex = 1;
            this.dgvProductsCatalog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductsCatalog_CellContentClick);
            this.dgvProductsCatalog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductsCatalog_CellDoubleClick);
            // 
            // pnlProductSearch
            // 
            this.pnlProductSearch.Controls.Add(this.tlpSearch);
            this.pnlProductSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductSearch.Location = new System.Drawing.Point(12, 12);
            this.pnlProductSearch.Name = "pnlProductSearch";
            this.pnlProductSearch.Size = new System.Drawing.Size(500, 58);
            this.pnlProductSearch.TabIndex = 0;
            // 
            // tlpSearch
            // 
            this.tlpSearch.ColumnCount = 2;
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpSearch.Controls.Add(this.lblSearchProduct, 0, 0);
            this.tlpSearch.Controls.Add(this.txtSearchProduct, 0, 1);
            this.tlpSearch.Controls.Add(this.lblCategoryFilter, 1, 0);
            this.tlpSearch.Controls.Add(this.cmbCategoryFilter, 1, 1);
            this.tlpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSearch.Location = new System.Drawing.Point(0, 0);
            this.tlpSearch.Name = "tlpSearch";
            this.tlpSearch.RowCount = 2;
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSearch.Size = new System.Drawing.Size(500, 58);
            this.tlpSearch.TabIndex = 0;
            // 
            // lblSearchProduct
            // 
            this.lblSearchProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchProduct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearchProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSearchProduct.Location = new System.Drawing.Point(178, 0);
            this.lblSearchProduct.Name = "lblSearchProduct";
            this.lblSearchProduct.Size = new System.Drawing.Size(319, 22);
            this.lblSearchProduct.TabIndex = 0;
            this.lblSearchProduct.Text = "بحث بالاسم أو الباركود:";
            this.lblSearchProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchProduct.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearchProduct.Location = new System.Drawing.Point(178, 26);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(319, 28);
            this.txtSearchProduct.TabIndex = 1;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // lblCategoryFilter
            // 
            this.lblCategoryFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategoryFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategoryFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCategoryFilter.Location = new System.Drawing.Point(3, 0);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Size = new System.Drawing.Size(169, 22);
            this.lblCategoryFilter.TabIndex = 2;
            this.lblCategoryFilter.Text = "القسم:";
            this.lblCategoryFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(3, 25);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(169, 29);
            this.cmbCategoryFilter.TabIndex = 3;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // pnlCart
            // 
            this.pnlCart.BackColor = System.Drawing.Color.White;
            this.pnlCart.Controls.Add(this.dgvCart);
            this.pnlCart.Controls.Add(this.pnlCartSummary);
            this.pnlCart.Controls.Add(this.lblCartTitle);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCart.Location = new System.Drawing.Point(3, 13);
            this.pnlCart.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCart.Size = new System.Drawing.Size(642, 552);
            this.pnlCart.TabIndex = 1;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCart.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCart.ColumnHeadersHeight = 38;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvCart.Location = new System.Drawing.Point(12, 44);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 36;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(618, 248);
            this.dgvCart.TabIndex = 1;
            this.dgvCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellContentClick);
            this.dgvCart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellValueChanged);
            // 
            // pnlCartSummary
            // 
            this.pnlCartSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlCartSummary.Controls.Add(this.tlpCartActions);
            this.pnlCartSummary.Controls.Add(this.pnlPaymentDetails);
            this.pnlCartSummary.Controls.Add(this.pnlFinalTotal);
            this.pnlCartSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartSummary.Location = new System.Drawing.Point(12, 292);
            this.pnlCartSummary.Name = "pnlCartSummary";
            this.pnlCartSummary.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCartSummary.Size = new System.Drawing.Size(618, 248);
            this.pnlCartSummary.TabIndex = 2;
            // 
            // tlpCartActions
            // 
            this.tlpCartActions.ColumnCount = 2;
            this.tlpCartActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tlpCartActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpCartActions.Controls.Add(this.btnCheckout, 0, 0);
            this.tlpCartActions.Controls.Add(this.btnClearCart, 1, 0);
            this.tlpCartActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpCartActions.Location = new System.Drawing.Point(12, 186);
            this.tlpCartActions.Name = "tlpCartActions";
            this.tlpCartActions.RowCount = 1;
            this.tlpCartActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCartActions.Size = new System.Drawing.Size(594, 50);
            this.tlpCartActions.TabIndex = 2;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(170, 3);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCheckout.Size = new System.Drawing.Size(421, 44);
            this.btnCheckout.TabIndex = 0;
            this.btnCheckout.Text = "إتمام البيع وطباعة الفاتورة (F5)";
            this.btnCheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearCart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClearCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnClearCart.Location = new System.Drawing.Point(3, 3);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClearCart.Size = new System.Drawing.Size(161, 44);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "تفريغ السلة (F4)";
            this.btnClearCart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.Controls.Add(this.tlpPaymentGrid);
            this.pnlPaymentDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(12, 74);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlPaymentDetails.Size = new System.Drawing.Size(594, 106);
            this.pnlPaymentDetails.TabIndex = 1;
            // 
            // tlpPaymentGrid
            // 
            this.tlpPaymentGrid.ColumnCount = 6;
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpPaymentGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tlpPaymentGrid.Controls.Add(this.lblSubtotal, 0, 0);
            this.tlpPaymentGrid.Controls.Add(this.lblSubtotalVal, 1, 0);
            this.tlpPaymentGrid.Controls.Add(this.lblDiscount, 2, 0);
            this.tlpPaymentGrid.Controls.Add(this.numDiscount, 3, 0);
            this.tlpPaymentGrid.Controls.Add(this.lblPaymentMethod, 4, 0);
            this.tlpPaymentGrid.Controls.Add(this.cmbPaymentMethod, 5, 0);
            this.tlpPaymentGrid.Controls.Add(this.lblCashPaid, 0, 1);
            this.tlpPaymentGrid.Controls.Add(this.numCashPaid, 1, 1);
            this.tlpPaymentGrid.Controls.Add(this.lblChangeDue, 2, 1);
            this.tlpPaymentGrid.Controls.Add(this.lblChangeDueVal, 3, 1);
            this.tlpPaymentGrid.Controls.Add(this.lblVat, 4, 1);
            this.tlpPaymentGrid.Controls.Add(this.lblVatVal, 5, 1);
            this.tlpPaymentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPaymentGrid.Location = new System.Drawing.Point(0, 6);
            this.tlpPaymentGrid.Name = "tlpPaymentGrid";
            this.tlpPaymentGrid.RowCount = 2;
            this.tlpPaymentGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPaymentGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPaymentGrid.Size = new System.Drawing.Size(594, 94);
            this.tlpPaymentGrid.TabIndex = 0;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSubtotal.Location = new System.Drawing.Point(491, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(100, 47);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "المجموع قبل:";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotalVal
            // 
            this.lblSubtotalVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotalVal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSubtotalVal.Location = new System.Drawing.Point(385, 0);
            this.lblSubtotalVal.Name = "lblSubtotalVal";
            this.lblSubtotalVal.Size = new System.Drawing.Size(100, 47);
            this.lblSubtotalVal.TabIndex = 1;
            this.lblSubtotalVal.Text = "0.00 ج.م";
            this.lblSubtotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiscount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiscount.Location = new System.Drawing.Point(296, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(83, 47);
            this.lblDiscount.TabIndex = 2;
            this.lblDiscount.Text = "الخصم (ج.م):";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numDiscount
            // 
            this.numDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numDiscount.DecimalPlaces = 2;
            this.numDiscount.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.numDiscount.Location = new System.Drawing.Point(190, 9);
            this.numDiscount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(100, 28);
            this.numDiscount.TabIndex = 3;
            this.numDiscount.ValueChanged += new System.EventHandler(this.numDiscount_ValueChanged);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(101, 0);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(83, 47);
            this.lblPaymentMethod.TabIndex = 4;
            this.lblPaymentMethod.Text = "طريقة الدفع:";
            this.lblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Items.AddRange(new object[] {
            "نقدي",
            "بطاقة بنكية",
            "محفظة إلكترونية"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(3, 10);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(92, 27);
            this.cmbPaymentMethod.TabIndex = 5;
            // 
            // lblCashPaid
            // 
            this.lblCashPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCashPaid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCashPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCashPaid.Location = new System.Drawing.Point(491, 47);
            this.lblCashPaid.Name = "lblCashPaid";
            this.lblCashPaid.Size = new System.Drawing.Size(100, 47);
            this.lblCashPaid.TabIndex = 6;
            this.lblCashPaid.Text = "المبلغ المدفوع:";
            this.lblCashPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numCashPaid
            // 
            this.numCashPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numCashPaid.DecimalPlaces = 2;
            this.numCashPaid.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.numCashPaid.Location = new System.Drawing.Point(385, 56);
            this.numCashPaid.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCashPaid.Name = "numCashPaid";
            this.numCashPaid.Size = new System.Drawing.Size(100, 29);
            this.numCashPaid.TabIndex = 7;
            this.numCashPaid.ValueChanged += new System.EventHandler(this.numCashPaid_ValueChanged);
            // 
            // lblChangeDue
            // 
            this.lblChangeDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangeDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblChangeDue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChangeDue.Location = new System.Drawing.Point(296, 47);
            this.lblChangeDue.Name = "lblChangeDue";
            this.lblChangeDue.Size = new System.Drawing.Size(83, 47);
            this.lblChangeDue.TabIndex = 8;
            this.lblChangeDue.Text = "الباقي للعميل:";
            this.lblChangeDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChangeDueVal
            // 
            this.lblChangeDueVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangeDueVal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblChangeDueVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblChangeDueVal.Location = new System.Drawing.Point(190, 47);
            this.lblChangeDueVal.Name = "lblChangeDueVal";
            this.lblChangeDueVal.Size = new System.Drawing.Size(100, 47);
            this.lblChangeDueVal.TabIndex = 9;
            this.lblChangeDueVal.Text = "0.00 ج.م";
            this.lblChangeDueVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVat
            // 
            this.lblVat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblVat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVat.Location = new System.Drawing.Point(101, 47);
            this.lblVat.Name = "lblVat";
            this.lblVat.Size = new System.Drawing.Size(83, 47);
            this.lblVat.TabIndex = 10;
            this.lblVat.Text = "الضريبة (0%):";
            this.lblVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVatVal
            // 
            this.lblVatVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVatVal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblVatVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblVatVal.Location = new System.Drawing.Point(3, 47);
            this.lblVatVal.Name = "lblVatVal";
            this.lblVatVal.Size = new System.Drawing.Size(92, 47);
            this.lblVatVal.TabIndex = 11;
            this.lblVatVal.Text = "0.00 ج.م";
            this.lblVatVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFinalTotal
            // 
            this.pnlFinalTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlFinalTotal.Controls.Add(this.lblFinalTotalVal);
            this.pnlFinalTotal.Controls.Add(this.lblFinalTotalTitle);
            this.pnlFinalTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFinalTotal.Location = new System.Drawing.Point(12, 12);
            this.pnlFinalTotal.Name = "pnlFinalTotal";
            this.pnlFinalTotal.Padding = new System.Windows.Forms.Padding(15, 6, 15, 6);
            this.pnlFinalTotal.Size = new System.Drawing.Size(594, 62);
            this.pnlFinalTotal.TabIndex = 0;
            // 
            // lblFinalTotalVal
            // 
            this.lblFinalTotalVal.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFinalTotalVal.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblFinalTotalVal.Location = new System.Drawing.Point(15, 6);
            this.lblFinalTotalVal.Name = "lblFinalTotalVal";
            this.lblFinalTotalVal.Size = new System.Drawing.Size(280, 50);
            this.lblFinalTotalVal.TabIndex = 1;
            this.lblFinalTotalVal.Text = "0.00 ج.م";
            this.lblFinalTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinalTotalTitle
            // 
            this.lblFinalTotalTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFinalTotalTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalTitle.ForeColor = System.Drawing.Color.White;
            this.lblFinalTotalTitle.Location = new System.Drawing.Point(279, 6);
            this.lblFinalTotalTitle.Name = "lblFinalTotalTitle";
            this.lblFinalTotalTitle.Size = new System.Drawing.Size(300, 50);
            this.lblFinalTotalTitle.TabIndex = 0;
            this.lblFinalTotalTitle.Text = "الإجمالي النهائي المستحق:";
            this.lblFinalTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCartTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCartTitle.Location = new System.Drawing.Point(12, 12);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(618, 32);
            this.lblCartTitle.TabIndex = 0;
            this.lblCartTitle.Text = "سلة مشتريات العميل الحالية";
            this.lblCartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlShiftBanner
            // 
            this.pnlShiftBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlShiftBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShiftBanner.Controls.Add(this.btnShiftBannerAction);
            this.pnlShiftBanner.Controls.Add(this.lblShiftBannerText);
            this.pnlShiftBanner.Controls.Add(this.lblShiftBannerIcon);
            this.pnlShiftBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlShiftBanner.Location = new System.Drawing.Point(14, 14);
            this.pnlShiftBanner.Name = "pnlShiftBanner";
            this.pnlShiftBanner.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlShiftBanner.Size = new System.Drawing.Size(1172, 44);
            this.pnlShiftBanner.TabIndex = 2;
            // 
            // btnShiftBannerAction
            // 
            this.btnShiftBannerAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnShiftBannerAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShiftBannerAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnShiftBannerAction.FlatAppearance.BorderSize = 0;
            this.btnShiftBannerAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftBannerAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnShiftBannerAction.ForeColor = System.Drawing.Color.White;
            this.btnShiftBannerAction.Location = new System.Drawing.Point(12, 4);
            this.btnShiftBannerAction.Name = "btnShiftBannerAction";
            this.btnShiftBannerAction.Size = new System.Drawing.Size(180, 34);
            this.btnShiftBannerAction.TabIndex = 2;
            this.btnShiftBannerAction.Text = "بدء وردية العمل الآن";
            this.btnShiftBannerAction.UseVisualStyleBackColor = false;
            this.btnShiftBannerAction.Click += new System.EventHandler(this.btnShiftBannerAction_Click);
            // 
            // lblShiftBannerText
            // 
            this.lblShiftBannerText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblShiftBannerText.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblShiftBannerText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblShiftBannerText.Location = new System.Drawing.Point(12, 4);
            this.lblShiftBannerText.Name = "lblShiftBannerText";
            this.lblShiftBannerText.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblShiftBannerText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblShiftBannerText.Size = new System.Drawing.Size(1118, 34);
            this.lblShiftBannerText.TabIndex = 1;
            this.lblShiftBannerText.Text = "تنبيه: لا توجد وردية عمل مفتوحة حالياً. يجب بدء وردية العمل أولاً للبدء في استخدا" +
    "م نقطة البيع.";
            this.lblShiftBannerText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShiftBannerIcon
            // 
            this.lblShiftBannerIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShiftBannerIcon.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblShiftBannerIcon.Location = new System.Drawing.Point(1130, 4);
            this.lblShiftBannerIcon.Name = "lblShiftBannerIcon";
            this.lblShiftBannerIcon.Size = new System.Drawing.Size(28, 34);
            this.lblShiftBannerIcon.TabIndex = 0;
            this.lblShiftBannerIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // POSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.pnlBarcodeTop);
            this.Controls.Add(this.pnlShiftBanner);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "POSForm";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "نقطة البيع (POS)";
            this.Load += new System.EventHandler(this.POSForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSForm_KeyDown);
            this.pnlBarcodeTop.ResumeLayout(false);
            this.tlpBarcodeHeader.ResumeLayout(false);
            this.tlpBarcodeHeader.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.pnlProductsCatalog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsCatalog)).EndInit();
            this.pnlProductSearch.ResumeLayout(false);
            this.tlpSearch.ResumeLayout(false);
            this.tlpSearch.PerformLayout();
            this.pnlCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartSummary.ResumeLayout(false);
            this.tlpCartActions.ResumeLayout(false);
            this.pnlPaymentDetails.ResumeLayout(false);
            this.tlpPaymentGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaid)).EndInit();
            this.pnlFinalTotal.ResumeLayout(false);
            this.pnlShiftBanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlShiftBanner;
        private System.Windows.Forms.Label lblShiftBannerIcon;
        private System.Windows.Forms.Label lblShiftBannerText;
        private System.Windows.Forms.Button btnShiftBannerAction;
        private System.Windows.Forms.Panel pnlBarcodeTop;
        private System.Windows.Forms.TableLayoutPanel tlpBarcodeHeader;
        private System.Windows.Forms.Label lblBarcodeTitle;
        private System.Windows.Forms.TextBox txtBarcodeScan;
        private System.Windows.Forms.Label lblBarcodeHint;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlProductsCatalog;
        private System.Windows.Forms.DataGridView dgvProductsCatalog;
        private System.Windows.Forms.Panel pnlProductSearch;
        private System.Windows.Forms.TableLayoutPanel tlpSearch;
        private System.Windows.Forms.Label lblSearchProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblCategoryFilter;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.Panel pnlCart;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlCartSummary;
        private System.Windows.Forms.Panel pnlFinalTotal;
        private System.Windows.Forms.Label lblFinalTotalTitle;
        private System.Windows.Forms.Label lblFinalTotalVal;
        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.TableLayoutPanel tlpPaymentGrid;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblSubtotalVal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblCashPaid;
        private System.Windows.Forms.NumericUpDown numCashPaid;
        private System.Windows.Forms.Label lblChangeDue;
        private System.Windows.Forms.Label lblChangeDueVal;
        private System.Windows.Forms.Label lblVat;
        private System.Windows.Forms.Label lblVatVal;
        private System.Windows.Forms.TableLayoutPanel tlpCartActions;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
    }
}