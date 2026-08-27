namespace POS
{
    partial class ProductsForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.tlpEditor = new System.Windows.Forms.TableLayoutPanel();
            this.lblEditorTitle = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.pnlBarcodeRow = new System.Windows.Forms.Panel();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnGenBarcode = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.pnlCategoryRow = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnManageCategories = new System.Windows.Forms.Button();
            this.lblBuyPrice = new System.Windows.Forms.Label();
            this.numBuyPrice = new System.Windows.Forms.NumericUpDown();
            this.lblSellPrice = new System.Windows.Forms.Label();
            this.numSellPrice = new System.Windows.Forms.NumericUpDown();
            this.lblStockQuantity = new System.Windows.Forms.Label();
            this.numStockQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblMinStockAlert = new System.Windows.Forms.Label();
            this.numMinStockAlert = new System.Windows.Forms.NumericUpDown();
            this.btnNewProduct = new System.Windows.Forms.Button();
            this.btnSaveProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.tlpFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblCategoryFilter = new System.Windows.Forms.Label();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.chkLowStockOnly = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.tlpEditor.SuspendLayout();
            this.pnlBarcodeRow.SuspendLayout();
            this.pnlCategoryRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStockAlert)).BeginInit();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.tlpFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlEditor, 0, 0);
            this.tlpMain.Controls.Add(this.pnlList, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(14, 14);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1172, 692);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlEditor
            // 
            this.pnlEditor.AutoScroll = true;
            this.pnlEditor.BackColor = System.Drawing.Color.White;
            this.pnlEditor.Controls.Add(this.tlpEditor);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(752, 3);
            this.pnlEditor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new System.Windows.Forms.Padding(14);
            this.pnlEditor.Size = new System.Drawing.Size(417, 686);
            this.pnlEditor.TabIndex = 0;
            // 
            // tlpEditor
            // 
            this.tlpEditor.ColumnCount = 1;
            this.tlpEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditor.Controls.Add(this.lblEditorTitle, 0, 0);
            this.tlpEditor.Controls.Add(this.lblBarcode, 0, 1);
            this.tlpEditor.Controls.Add(this.pnlBarcodeRow, 0, 2);
            this.tlpEditor.Controls.Add(this.lblProductName, 0, 3);
            this.tlpEditor.Controls.Add(this.txtProductName, 0, 4);
            this.tlpEditor.Controls.Add(this.lblCategory, 0, 5);
            this.tlpEditor.Controls.Add(this.pnlCategoryRow, 0, 6);
            this.tlpEditor.Controls.Add(this.lblBuyPrice, 0, 7);
            this.tlpEditor.Controls.Add(this.numBuyPrice, 0, 8);
            this.tlpEditor.Controls.Add(this.lblSellPrice, 0, 9);
            this.tlpEditor.Controls.Add(this.numSellPrice, 0, 10);
            this.tlpEditor.Controls.Add(this.lblStockQuantity, 0, 11);
            this.tlpEditor.Controls.Add(this.numStockQuantity, 0, 12);
            this.tlpEditor.Controls.Add(this.lblMinStockAlert, 0, 13);
            this.tlpEditor.Controls.Add(this.numMinStockAlert, 0, 14);
            this.tlpEditor.Controls.Add(this.btnNewProduct, 0, 15);
            this.tlpEditor.Controls.Add(this.btnSaveProduct, 0, 16);
            this.tlpEditor.Controls.Add(this.btnDeleteProduct, 0, 17);
            this.tlpEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpEditor.Location = new System.Drawing.Point(14, 14);
            this.tlpEditor.Name = "tlpEditor";
            this.tlpEditor.RowCount = 18;
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEditor.Size = new System.Drawing.Size(389, 500);
            this.tlpEditor.TabIndex = 0;
            // 
            // lblEditorTitle
            // 
            this.lblEditorTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditorTitle.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblEditorTitle.Location = new System.Drawing.Point(3, 0);
            this.lblEditorTitle.Name = "lblEditorTitle";
            this.lblEditorTitle.Size = new System.Drawing.Size(383, 32);
            this.lblEditorTitle.TabIndex = 0;
            this.lblEditorTitle.Text = "📝 بيانات الصنف / المنتج";
            this.lblEditorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBarcode
            // 
            this.lblBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBarcode.Location = new System.Drawing.Point(3, 32);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(383, 18);
            this.lblBarcode.TabIndex = 1;
            this.lblBarcode.Text = "الباركود:";
            this.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBarcodeRow
            // 
            this.pnlBarcodeRow.Controls.Add(this.txtBarcode);
            this.pnlBarcodeRow.Controls.Add(this.btnGenBarcode);
            this.pnlBarcodeRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBarcodeRow.Location = new System.Drawing.Point(0, 50);
            this.pnlBarcodeRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.pnlBarcodeRow.Name = "pnlBarcodeRow";
            this.pnlBarcodeRow.Size = new System.Drawing.Size(389, 26);
            this.pnlBarcodeRow.TabIndex = 2;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBarcode.Location = new System.Drawing.Point(120, 2);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(266, 28);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.Text = "d";
            // 
            // btnGenBarcode
            // 
            this.btnGenBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnGenBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenBarcode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnGenBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenBarcode.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnGenBarcode.Location = new System.Drawing.Point(3, 1);
            this.btnGenBarcode.Name = "btnGenBarcode";
            this.btnGenBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGenBarcode.Size = new System.Drawing.Size(77, 26);
            this.btnGenBarcode.TabIndex = 1;
            this.btnGenBarcode.Text = "باركود";
            this.btnGenBarcode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenBarcode.UseVisualStyleBackColor = false;
            this.btnGenBarcode.Click += new System.EventHandler(this.btnGenBarcode_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblProductName.Location = new System.Drawing.Point(3, 82);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(383, 18);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "اسم المنتج:";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProductName
            // 
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProductName.Location = new System.Drawing.Point(3, 103);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(383, 28);
            this.txtProductName.TabIndex = 4;
            // 
            // lblCategory
            // 
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCategory.Location = new System.Drawing.Point(3, 132);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(383, 18);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "القسم:";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCategoryRow
            // 
            this.pnlCategoryRow.Controls.Add(this.cmbCategory);
            this.pnlCategoryRow.Controls.Add(this.btnManageCategories);
            this.pnlCategoryRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryRow.Location = new System.Drawing.Point(0, 150);
            this.pnlCategoryRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.pnlCategoryRow.Name = "pnlCategoryRow";
            this.pnlCategoryRow.Size = new System.Drawing.Size(389, 26);
            this.pnlCategoryRow.TabIndex = 6;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(120, 2);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(266, 29);
            this.cmbCategory.TabIndex = 0;
            // 
            // btnManageCategories
            // 
            this.btnManageCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnManageCategories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageCategories.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnManageCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCategories.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnManageCategories.Location = new System.Drawing.Point(3, 1);
            this.btnManageCategories.Name = "btnManageCategories";
            this.btnManageCategories.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnManageCategories.Size = new System.Drawing.Size(77, 26);
            this.btnManageCategories.TabIndex = 1;
            this.btnManageCategories.Text = "الأقسام";
            this.btnManageCategories.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnManageCategories.UseVisualStyleBackColor = false;
            this.btnManageCategories.Click += new System.EventHandler(this.btnManageCategories_Click);
            // 
            // lblBuyPrice
            // 
            this.lblBuyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuyPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBuyPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBuyPrice.Location = new System.Drawing.Point(3, 182);
            this.lblBuyPrice.Name = "lblBuyPrice";
            this.lblBuyPrice.Size = new System.Drawing.Size(383, 18);
            this.lblBuyPrice.TabIndex = 7;
            this.lblBuyPrice.Text = "سعر الشراء (التكلفة):";
            this.lblBuyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numBuyPrice
            // 
            this.numBuyPrice.DecimalPlaces = 2;
            this.numBuyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBuyPrice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.numBuyPrice.Location = new System.Drawing.Point(3, 203);
            this.numBuyPrice.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.numBuyPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numBuyPrice.Name = "numBuyPrice";
            this.numBuyPrice.Size = new System.Drawing.Size(383, 28);
            this.numBuyPrice.TabIndex = 8;
            // 
            // lblSellPrice
            // 
            this.lblSellPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSellPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSellPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSellPrice.Location = new System.Drawing.Point(3, 232);
            this.lblSellPrice.Name = "lblSellPrice";
            this.lblSellPrice.Size = new System.Drawing.Size(383, 18);
            this.lblSellPrice.TabIndex = 9;
            this.lblSellPrice.Text = "سعر البيع للجمهور:";
            this.lblSellPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numSellPrice
            // 
            this.numSellPrice.DecimalPlaces = 2;
            this.numSellPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSellPrice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.numSellPrice.Location = new System.Drawing.Point(3, 253);
            this.numSellPrice.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.numSellPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numSellPrice.Name = "numSellPrice";
            this.numSellPrice.Size = new System.Drawing.Size(383, 28);
            this.numSellPrice.TabIndex = 10;
            // 
            // lblStockQuantity
            // 
            this.lblStockQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStockQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStockQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStockQuantity.Location = new System.Drawing.Point(3, 282);
            this.lblStockQuantity.Name = "lblStockQuantity";
            this.lblStockQuantity.Size = new System.Drawing.Size(383, 18);
            this.lblStockQuantity.TabIndex = 11;
            this.lblStockQuantity.Text = "الكمية المتاحة بالمخزن:";
            this.lblStockQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numStockQuantity
            // 
            this.numStockQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numStockQuantity.Font = new System.Drawing.Font("Tahoma", 10F);
            this.numStockQuantity.Location = new System.Drawing.Point(3, 303);
            this.numStockQuantity.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.numStockQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numStockQuantity.Name = "numStockQuantity";
            this.numStockQuantity.Size = new System.Drawing.Size(383, 28);
            this.numStockQuantity.TabIndex = 12;
            // 
            // lblMinStockAlert
            // 
            this.lblMinStockAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMinStockAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblMinStockAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMinStockAlert.Location = new System.Drawing.Point(3, 332);
            this.lblMinStockAlert.Name = "lblMinStockAlert";
            this.lblMinStockAlert.Size = new System.Drawing.Size(383, 18);
            this.lblMinStockAlert.TabIndex = 13;
            this.lblMinStockAlert.Text = "حد التنبيه (أدنى مخزون):";
            this.lblMinStockAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numMinStockAlert
            // 
            this.numMinStockAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMinStockAlert.Font = new System.Drawing.Font("Tahoma", 10F);
            this.numMinStockAlert.Location = new System.Drawing.Point(3, 353);
            this.numMinStockAlert.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.numMinStockAlert.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinStockAlert.Name = "numMinStockAlert";
            this.numMinStockAlert.Size = new System.Drawing.Size(383, 28);
            this.numMinStockAlert.TabIndex = 14;
            this.numMinStockAlert.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnNewProduct
            // 
            this.btnNewProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNewProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnNewProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewProduct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNewProduct.Location = new System.Drawing.Point(3, 385);
            this.btnNewProduct.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnNewProduct.Name = "btnNewProduct";
            this.btnNewProduct.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNewProduct.Size = new System.Drawing.Size(383, 32);
            this.btnNewProduct.TabIndex = 15;
            this.btnNewProduct.Text = "➕ صنف جديد (تفريغ الحقول)";
            this.btnNewProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewProduct.UseVisualStyleBackColor = false;
            this.btnNewProduct.Click += new System.EventHandler(this.btnNewProduct_Click);
            // 
            // btnSaveProduct
            // 
            this.btnSaveProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveProduct.FlatAppearance.BorderSize = 0;
            this.btnSaveProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProduct.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveProduct.ForeColor = System.Drawing.Color.White;
            this.btnSaveProduct.Location = new System.Drawing.Point(3, 425);
            this.btnSaveProduct.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnSaveProduct.Name = "btnSaveProduct";
            this.btnSaveProduct.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSaveProduct.Size = new System.Drawing.Size(383, 34);
            this.btnSaveProduct.TabIndex = 16;
            this.btnSaveProduct.Text = "💾 حفظ بيانات المنتج";
            this.btnSaveProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveProduct.UseVisualStyleBackColor = false;
            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDeleteProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteProduct.Enabled = false;
            this.btnDeleteProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProduct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeleteProduct.Location = new System.Drawing.Point(3, 467);
            this.btnDeleteProduct.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDeleteProduct.Size = new System.Drawing.Size(383, 30);
            this.btnDeleteProduct.TabIndex = 17;
            this.btnDeleteProduct.Text = "🗑️ حذف الصنف المحدد";
            this.btnDeleteProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteProduct.UseVisualStyleBackColor = false;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // pnlList
            // 
            this.pnlList.BackColor = System.Drawing.Color.White;
            this.pnlList.Controls.Add(this.dgvProducts);
            this.pnlList.Controls.Add(this.pnlFilters);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(3, 3);
            this.pnlList.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlList.Size = new System.Drawing.Size(749, 686);
            this.pnlList.TabIndex = 1;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProducts.ColumnHeadersHeight = 48;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProducts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvProducts.Location = new System.Drawing.Point(14, 76);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.RowTemplate.Height = 40;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(721, 596);
            this.dgvProducts.TabIndex = 1;
            this.dgvProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProducts_CellFormatting);
            this.dgvProducts.SelectionChanged += new System.EventHandler(this.dgvProducts_SelectionChanged);
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.tlpFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(14, 14);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(721, 62);
            this.pnlFilters.TabIndex = 0;
            // 
            // tlpFilters
            // 
            this.tlpFilters.ColumnCount = 4;
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpFilters.Controls.Add(this.lblSearch, 0, 0);
            this.tlpFilters.Controls.Add(this.txtSearch, 0, 1);
            this.tlpFilters.Controls.Add(this.lblCategoryFilter, 1, 0);
            this.tlpFilters.Controls.Add(this.cmbCategoryFilter, 1, 1);
            this.tlpFilters.Controls.Add(this.chkLowStockOnly, 2, 1);
            this.tlpFilters.Controls.Add(this.btnRefresh, 3, 1);
            this.tlpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilters.Location = new System.Drawing.Point(0, 0);
            this.tlpFilters.Name = "tlpFilters";
            this.tlpFilters.RowCount = 2;
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilters.Size = new System.Drawing.Size(721, 62);
            this.tlpFilters.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSearch.Location = new System.Drawing.Point(422, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(296, 22);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 بحث بالاسم أو الباركود:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearch.Location = new System.Drawing.Point(422, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(296, 28);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblCategoryFilter
            // 
            this.lblCategoryFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategoryFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategoryFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCategoryFilter.Location = new System.Drawing.Point(221, 0);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Size = new System.Drawing.Size(195, 22);
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
            this.cmbCategoryFilter.Location = new System.Drawing.Point(221, 27);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(195, 29);
            this.cmbCategoryFilter.TabIndex = 3;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // chkLowStockOnly
            // 
            this.chkLowStockOnly.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkLowStockOnly.AutoSize = true;
            this.chkLowStockOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkLowStockOnly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkLowStockOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.chkLowStockOnly.Location = new System.Drawing.Point(77, 31);
            this.chkLowStockOnly.Name = "chkLowStockOnly";
            this.chkLowStockOnly.Size = new System.Drawing.Size(138, 22);
            this.chkLowStockOnly.TabIndex = 4;
            this.chkLowStockOnly.Text = "⚠️ عرض النواقص فقط";
            this.chkLowStockOnly.UseVisualStyleBackColor = true;
            this.chkLowStockOnly.CheckedChanged += new System.EventHandler(this.chkLowStockOnly_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(3, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRefresh.Size = new System.Drawing.Size(68, 30);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 تحديث";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductsForm";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "إدارة المنتجات والمخزون";
            this.Load += new System.EventHandler(this.ProductsForm_Load);
            this.tlpMain.ResumeLayout(false);
            this.pnlEditor.ResumeLayout(false);
            this.tlpEditor.ResumeLayout(false);
            this.tlpEditor.PerformLayout();
            this.pnlBarcodeRow.ResumeLayout(false);
            this.pnlBarcodeRow.PerformLayout();
            this.pnlCategoryRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBuyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStockAlert)).EndInit();
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.tlpFilters.ResumeLayout(false);
            this.tlpFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.TableLayoutPanel tlpEditor;
        private System.Windows.Forms.Label lblEditorTitle;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Panel pnlBarcodeRow;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnGenBarcode;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Panel pnlCategoryRow;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnManageCategories;
        private System.Windows.Forms.Label lblBuyPrice;
        private System.Windows.Forms.NumericUpDown numBuyPrice;
        private System.Windows.Forms.Label lblSellPrice;
        private System.Windows.Forms.NumericUpDown numSellPrice;
        private System.Windows.Forms.Label lblStockQuantity;
        private System.Windows.Forms.NumericUpDown numStockQuantity;
        private System.Windows.Forms.Label lblMinStockAlert;
        private System.Windows.Forms.NumericUpDown numMinStockAlert;
        private System.Windows.Forms.Button btnNewProduct;
        private System.Windows.Forms.Button btnSaveProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.TableLayoutPanel tlpFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategoryFilter;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.CheckBox chkLowStockOnly;
        private System.Windows.Forms.Button btnRefresh;
    }
}
