namespace POS
{
    partial class PurchasesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPurchases = new System.Windows.Forms.TabControl();
            this.tabNewPurchase = new System.Windows.Forms.TabPage();
            this.pnlNewPurchaseContainer = new System.Windows.Forms.Panel();
            this.dgvPurchaseItems = new System.Windows.Forms.DataGridView();
            this.pnlPurchaseBottom = new System.Windows.Forms.Panel();
            this.tlpPurchaseBottom = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalDisplay = new System.Windows.Forms.Panel();
            this.lblTotalPurchaseVal = new System.Windows.Forms.Label();
            this.lblTotalPurchaseTitle = new System.Windows.Forms.Label();
            this.chkUpdateBuyPrice = new System.Windows.Forms.CheckBox();
            this.flpPurchaseActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSavePurchase = new System.Windows.Forms.Button();
            this.btnResetPurchase = new System.Windows.Forms.Button();
            this.pnlAddItem = new System.Windows.Forms.Panel();
            this.tlpAddItem = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectProduct = new System.Windows.Forms.Label();
            this.cmbSelectProduct = new System.Windows.Forms.ComboBox();
            this.lblUnitCost = new System.Windows.Forms.Label();
            this.numUnitCost = new System.Windows.Forms.NumericUpDown();
            this.lblPurchaseQty = new System.Windows.Forms.Label();
            this.numPurchaseQty = new System.Windows.Forms.NumericUpDown();
            this.btnQuickAddProduct = new System.Windows.Forms.Button();
            this.pnlSupplierHeader = new System.Windows.Forms.Panel();
            this.tlpSupplierHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.pnlSupplierSelect = new System.Windows.Forms.Panel();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.btnQuickAddSupplier = new System.Windows.Forms.Button();
            this.lblPurchaseDate = new System.Windows.Forms.Label();
            this.dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.lblPurchaseNotes = new System.Windows.Forms.Label();
            this.txtPurchaseNotes = new System.Windows.Forms.TextBox();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.splitHistory = new System.Windows.Forms.SplitContainer();
            this.pnlHistoryList = new System.Windows.Forms.Panel();
            this.dgvPurchasesHistory = new System.Windows.Forms.DataGridView();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.pnlHistoryDetails = new System.Windows.Forms.Panel();
            this.dgvPurchaseHistoryDetails = new System.Windows.Forms.DataGridView();
            this.lblHistoryDetailsTitle = new System.Windows.Forms.Label();
            this.tabPurchases.SuspendLayout();
            this.tabNewPurchase.SuspendLayout();
            this.pnlNewPurchaseContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).BeginInit();
            this.pnlPurchaseBottom.SuspendLayout();
            this.tlpPurchaseBottom.SuspendLayout();
            this.pnlTotalDisplay.SuspendLayout();
            this.flpPurchaseActions.SuspendLayout();
            this.pnlAddItem.SuspendLayout();
            this.tlpAddItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchaseQty)).BeginInit();
            this.pnlSupplierHeader.SuspendLayout();
            this.tlpSupplierHeader.SuspendLayout();
            this.pnlSupplierSelect.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitHistory)).BeginInit();
            this.splitHistory.Panel1.SuspendLayout();
            this.splitHistory.Panel2.SuspendLayout();
            this.splitHistory.SuspendLayout();
            this.pnlHistoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchasesHistory)).BeginInit();
            this.pnlHistoryDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistoryDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPurchases
            // 
            this.tabPurchases.Controls.Add(this.tabNewPurchase);
            this.tabPurchases.Controls.Add(this.tabHistory);
            this.tabPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPurchases.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.tabPurchases.ItemSize = new System.Drawing.Size(300, 32);
            this.tabPurchases.Location = new System.Drawing.Point(14, 14);
            this.tabPurchases.Name = "tabPurchases";
            this.tabPurchases.Padding = new System.Drawing.Point(20, 6);
            this.tabPurchases.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabPurchases.RightToLeftLayout = true;
            this.tabPurchases.SelectedIndex = 0;
            this.tabPurchases.Size = new System.Drawing.Size(1172, 692);
            this.tabPurchases.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPurchases.TabIndex = 0;
            this.tabPurchases.SelectedIndexChanged += new System.EventHandler(this.tabPurchases_SelectedIndexChanged);
            // 
            // tabNewPurchase
            // 
            this.tabNewPurchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabNewPurchase.Controls.Add(this.pnlNewPurchaseContainer);
            this.tabNewPurchase.Location = new System.Drawing.Point(4, 36);
            this.tabNewPurchase.Name = "tabNewPurchase";
            this.tabNewPurchase.Padding = new System.Windows.Forms.Padding(10);
            this.tabNewPurchase.Size = new System.Drawing.Size(1164, 652);
            this.tabNewPurchase.TabIndex = 0;
            this.tabNewPurchase.Text = "تسجيل فاتورة مشتريات واردة";
            // 
            // pnlNewPurchaseContainer
            // 
            this.pnlNewPurchaseContainer.BackColor = System.Drawing.Color.White;
            this.pnlNewPurchaseContainer.Controls.Add(this.dgvPurchaseItems);
            this.pnlNewPurchaseContainer.Controls.Add(this.pnlPurchaseBottom);
            this.pnlNewPurchaseContainer.Controls.Add(this.pnlAddItem);
            this.pnlNewPurchaseContainer.Controls.Add(this.pnlSupplierHeader);
            this.pnlNewPurchaseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNewPurchaseContainer.Location = new System.Drawing.Point(10, 10);
            this.pnlNewPurchaseContainer.Name = "pnlNewPurchaseContainer";
            this.pnlNewPurchaseContainer.Padding = new System.Windows.Forms.Padding(14);
            this.pnlNewPurchaseContainer.Size = new System.Drawing.Size(1144, 632);
            this.pnlNewPurchaseContainer.TabIndex = 0;
            // 
            // dgvPurchaseItems
            // 
            this.dgvPurchaseItems.AllowUserToAddRows = false;
            this.dgvPurchaseItems.AllowUserToDeleteRows = false;
            this.dgvPurchaseItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchaseItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPurchaseItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPurchaseItems.ColumnHeadersHeight = 48;
            this.dgvPurchaseItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseItems.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPurchaseItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseItems.EnableHeadersVisualStyles = false;
            this.dgvPurchaseItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvPurchaseItems.Location = new System.Drawing.Point(14, 150);
            this.dgvPurchaseItems.MultiSelect = false;
            this.dgvPurchaseItems.Name = "dgvPurchaseItems";
            this.dgvPurchaseItems.RowHeadersVisible = false;
            this.dgvPurchaseItems.RowHeadersWidth = 51;
            this.dgvPurchaseItems.RowTemplate.Height = 40;
            this.dgvPurchaseItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseItems.Size = new System.Drawing.Size(1116, 408);
            this.dgvPurchaseItems.TabIndex = 2;
            this.dgvPurchaseItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseItems_CellContentClick);
            // 
            // pnlPurchaseBottom
            // 
            this.pnlPurchaseBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlPurchaseBottom.Controls.Add(this.tlpPurchaseBottom);
            this.pnlPurchaseBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPurchaseBottom.Location = new System.Drawing.Point(14, 558);
            this.pnlPurchaseBottom.Name = "pnlPurchaseBottom";
            this.pnlPurchaseBottom.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlPurchaseBottom.Size = new System.Drawing.Size(1116, 60);
            this.pnlPurchaseBottom.TabIndex = 3;
            // 
            // tlpPurchaseBottom
            // 
            this.tlpPurchaseBottom.ColumnCount = 3;
            this.tlpPurchaseBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpPurchaseBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpPurchaseBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpPurchaseBottom.Controls.Add(this.pnlTotalDisplay, 0, 0);
            this.tlpPurchaseBottom.Controls.Add(this.chkUpdateBuyPrice, 1, 0);
            this.tlpPurchaseBottom.Controls.Add(this.flpPurchaseActions, 2, 0);
            this.tlpPurchaseBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPurchaseBottom.Location = new System.Drawing.Point(10, 8);
            this.tlpPurchaseBottom.Name = "tlpPurchaseBottom";
            this.tlpPurchaseBottom.RowCount = 1;
            this.tlpPurchaseBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPurchaseBottom.Size = new System.Drawing.Size(1096, 44);
            this.tlpPurchaseBottom.TabIndex = 0;
            // 
            // pnlTotalDisplay
            // 
            this.pnlTotalDisplay.Controls.Add(this.lblTotalPurchaseVal);
            this.pnlTotalDisplay.Controls.Add(this.lblTotalPurchaseTitle);
            this.pnlTotalDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalDisplay.Location = new System.Drawing.Point(713, 0);
            this.pnlTotalDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTotalDisplay.Name = "pnlTotalDisplay";
            this.pnlTotalDisplay.Size = new System.Drawing.Size(383, 44);
            this.pnlTotalDisplay.TabIndex = 0;
            // 
            // lblTotalPurchaseVal
            // 
            this.lblTotalPurchaseVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalPurchaseVal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalPurchaseVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTotalPurchaseVal.Location = new System.Drawing.Point(0, 0);
            this.lblTotalPurchaseVal.Name = "lblTotalPurchaseVal";
            this.lblTotalPurchaseVal.Size = new System.Drawing.Size(228, 44);
            this.lblTotalPurchaseVal.TabIndex = 1;
            this.lblTotalPurchaseVal.Text = "0.00 ج.م";
            this.lblTotalPurchaseVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalPurchaseTitle
            // 
            this.lblTotalPurchaseTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalPurchaseTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalPurchaseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalPurchaseTitle.Location = new System.Drawing.Point(228, 0);
            this.lblTotalPurchaseTitle.Name = "lblTotalPurchaseTitle";
            this.lblTotalPurchaseTitle.Size = new System.Drawing.Size(155, 44);
            this.lblTotalPurchaseTitle.TabIndex = 0;
            this.lblTotalPurchaseTitle.Text = "إجمالي فاتورة الشراء:";
            this.lblTotalPurchaseTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkUpdateBuyPrice
            // 
            this.chkUpdateBuyPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkUpdateBuyPrice.AutoSize = true;
            this.chkUpdateBuyPrice.Checked = true;
            this.chkUpdateBuyPrice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateBuyPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkUpdateBuyPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkUpdateBuyPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkUpdateBuyPrice.Location = new System.Drawing.Point(388, 11);
            this.chkUpdateBuyPrice.Name = "chkUpdateBuyPrice";
            this.chkUpdateBuyPrice.Size = new System.Drawing.Size(322, 22);
            this.chkUpdateBuyPrice.TabIndex = 1;
            this.chkUpdateBuyPrice.Text = "تحديث سعر الشراء تلقائياً في بيانات المنتجات";
            this.chkUpdateBuyPrice.UseVisualStyleBackColor = true;
            // 
            // flpPurchaseActions
            // 
            this.flpPurchaseActions.Controls.Add(this.btnSavePurchase);
            this.flpPurchaseActions.Controls.Add(this.btnResetPurchase);
            this.flpPurchaseActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPurchaseActions.Location = new System.Drawing.Point(0, 0);
            this.flpPurchaseActions.Margin = new System.Windows.Forms.Padding(0);
            this.flpPurchaseActions.Name = "flpPurchaseActions";
            this.flpPurchaseActions.Size = new System.Drawing.Size(385, 44);
            this.flpPurchaseActions.TabIndex = 2;
            this.flpPurchaseActions.WrapContents = false;
            // 
            // btnSavePurchase
            // 
            this.btnSavePurchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSavePurchase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSavePurchase.FlatAppearance.BorderSize = 0;
            this.btnSavePurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePurchase.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePurchase.ForeColor = System.Drawing.Color.White;
            this.btnSavePurchase.Location = new System.Drawing.Point(128, 3);
            this.btnSavePurchase.Name = "btnSavePurchase";
            this.btnSavePurchase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSavePurchase.Size = new System.Drawing.Size(254, 38);
            this.btnSavePurchase.TabIndex = 0;
            this.btnSavePurchase.Text = "حفظ الفاتورة وتحديث المخزون";
            this.btnSavePurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSavePurchase.UseVisualStyleBackColor = false;
            this.btnSavePurchase.Click += new System.EventHandler(this.btnSavePurchase_Click);
            // 
            // btnResetPurchase
            // 
            this.btnResetPurchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnResetPurchase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetPurchase.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnResetPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPurchase.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnResetPurchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnResetPurchase.Location = new System.Drawing.Point(4, 3);
            this.btnResetPurchase.Name = "btnResetPurchase";
            this.btnResetPurchase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnResetPurchase.Size = new System.Drawing.Size(118, 38);
            this.btnResetPurchase.TabIndex = 1;
            this.btnResetPurchase.Text = "إلغاء الفاتورة";
            this.btnResetPurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetPurchase.UseVisualStyleBackColor = false;
            this.btnResetPurchase.Click += new System.EventHandler(this.btnResetPurchase_Click);
            // 
            // pnlAddItem
            // 
            this.pnlAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlAddItem.Controls.Add(this.tlpAddItem);
            this.pnlAddItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddItem.Location = new System.Drawing.Point(14, 82);
            this.pnlAddItem.Name = "pnlAddItem";
            this.pnlAddItem.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlAddItem.Size = new System.Drawing.Size(1116, 68);
            this.pnlAddItem.TabIndex = 1;
            // 
            // tlpAddItem
            // 
            this.tlpAddItem.ColumnCount = 4;
            this.tlpAddItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAddItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAddItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpAddItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpAddItem.Controls.Add(this.lblSelectProduct, 0, 0);
            this.tlpAddItem.Controls.Add(this.cmbSelectProduct, 0, 1);
            this.tlpAddItem.Controls.Add(this.lblUnitCost, 1, 0);
            this.tlpAddItem.Controls.Add(this.numUnitCost, 1, 1);
            this.tlpAddItem.Controls.Add(this.lblPurchaseQty, 2, 0);
            this.tlpAddItem.Controls.Add(this.numPurchaseQty, 2, 1);
            this.tlpAddItem.Controls.Add(this.btnQuickAddProduct, 3, 1);
            this.tlpAddItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAddItem.Location = new System.Drawing.Point(12, 0);
            this.tlpAddItem.Name = "tlpAddItem";
            this.tlpAddItem.RowCount = 2;
            this.tlpAddItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpAddItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAddItem.Size = new System.Drawing.Size(1092, 68);
            this.tlpAddItem.TabIndex = 0;
            // 
            // lblSelectProduct
            // 
            this.lblSelectProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectProduct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectProduct.Location = new System.Drawing.Point(549, 0);
            this.lblSelectProduct.Name = "lblSelectProduct";
            this.lblSelectProduct.Size = new System.Drawing.Size(540, 22);
            this.lblSelectProduct.TabIndex = 0;
            this.lblSelectProduct.Text = "الصنف / المنتج:";
            this.lblSelectProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSelectProduct
            // 
            this.cmbSelectProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectProduct.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbSelectProduct.FormattingEnabled = true;
            this.cmbSelectProduct.Location = new System.Drawing.Point(549, 30);
            this.cmbSelectProduct.Name = "cmbSelectProduct";
            this.cmbSelectProduct.Size = new System.Drawing.Size(540, 29);
            this.cmbSelectProduct.TabIndex = 1;
            this.cmbSelectProduct.SelectedIndexChanged += new System.EventHandler(this.cmbSelectProduct_SelectedIndexChanged);
            // 
            // lblUnitCost
            // 
            this.lblUnitCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnitCost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnitCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblUnitCost.Location = new System.Drawing.Point(331, 0);
            this.lblUnitCost.Name = "lblUnitCost";
            this.lblUnitCost.Size = new System.Drawing.Size(212, 22);
            this.lblUnitCost.TabIndex = 2;
            this.lblUnitCost.Text = "سعر الشراء الوارد:";
            this.lblUnitCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numUnitCost
            // 
            this.numUnitCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numUnitCost.DecimalPlaces = 2;
            this.numUnitCost.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.numUnitCost.Location = new System.Drawing.Point(331, 31);
            this.numUnitCost.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numUnitCost.Name = "numUnitCost";
            this.numUnitCost.Size = new System.Drawing.Size(212, 28);
            this.numUnitCost.TabIndex = 3;
            // 
            // lblPurchaseQty
            // 
            this.lblPurchaseQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPurchaseQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPurchaseQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPurchaseQty.Location = new System.Drawing.Point(168, 0);
            this.lblPurchaseQty.Name = "lblPurchaseQty";
            this.lblPurchaseQty.Size = new System.Drawing.Size(157, 22);
            this.lblPurchaseQty.TabIndex = 4;
            this.lblPurchaseQty.Text = "الكمية الواردة:";
            this.lblPurchaseQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numPurchaseQty
            // 
            this.numPurchaseQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPurchaseQty.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.numPurchaseQty.Location = new System.Drawing.Point(168, 31);
            this.numPurchaseQty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numPurchaseQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPurchaseQty.Name = "numPurchaseQty";
            this.numPurchaseQty.Size = new System.Drawing.Size(157, 28);
            this.numPurchaseQty.TabIndex = 5;
            this.numPurchaseQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnQuickAddProduct
            // 
            this.btnQuickAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnQuickAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickAddProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuickAddProduct.FlatAppearance.BorderSize = 0;
            this.btnQuickAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAddProduct.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnQuickAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnQuickAddProduct.Location = new System.Drawing.Point(3, 25);
            this.btnQuickAddProduct.Name = "btnQuickAddProduct";
            this.btnQuickAddProduct.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnQuickAddProduct.Size = new System.Drawing.Size(159, 40);
            this.btnQuickAddProduct.TabIndex = 6;
            this.btnQuickAddProduct.Text = "إضافة للفاتورة";
            this.btnQuickAddProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuickAddProduct.UseVisualStyleBackColor = false;
            this.btnQuickAddProduct.Click += new System.EventHandler(this.btnQuickAddProduct_Click);
            // 
            // pnlSupplierHeader
            // 
            this.pnlSupplierHeader.Controls.Add(this.tlpSupplierHeader);
            this.pnlSupplierHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSupplierHeader.Location = new System.Drawing.Point(14, 14);
            this.pnlSupplierHeader.Name = "pnlSupplierHeader";
            this.pnlSupplierHeader.Size = new System.Drawing.Size(1116, 68);
            this.pnlSupplierHeader.TabIndex = 0;
            // 
            // tlpSupplierHeader
            // 
            this.tlpSupplierHeader.ColumnCount = 3;
            this.tlpSupplierHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tlpSupplierHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSupplierHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tlpSupplierHeader.Controls.Add(this.lblSupplier, 0, 0);
            this.tlpSupplierHeader.Controls.Add(this.pnlSupplierSelect, 0, 1);
            this.tlpSupplierHeader.Controls.Add(this.lblPurchaseDate, 1, 0);
            this.tlpSupplierHeader.Controls.Add(this.dtpPurchaseDate, 1, 1);
            this.tlpSupplierHeader.Controls.Add(this.lblPurchaseNotes, 2, 0);
            this.tlpSupplierHeader.Controls.Add(this.txtPurchaseNotes, 2, 1);
            this.tlpSupplierHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSupplierHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpSupplierHeader.Name = "tlpSupplierHeader";
            this.tlpSupplierHeader.RowCount = 2;
            this.tlpSupplierHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpSupplierHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSupplierHeader.Size = new System.Drawing.Size(1116, 68);
            this.tlpSupplierHeader.TabIndex = 0;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupplier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSupplier.Location = new System.Drawing.Point(651, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(462, 22);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "المورد / الشركة:";
            this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSupplierSelect
            // 
            this.pnlSupplierSelect.Controls.Add(this.cmbSupplier);
            this.pnlSupplierSelect.Controls.Add(this.btnQuickAddSupplier);
            this.pnlSupplierSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSupplierSelect.Location = new System.Drawing.Point(651, 25);
            this.pnlSupplierSelect.Name = "pnlSupplierSelect";
            this.pnlSupplierSelect.Size = new System.Drawing.Size(462, 40);
            this.pnlSupplierSelect.TabIndex = 1;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(186, 4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(276, 29);
            this.cmbSupplier.TabIndex = 0;
            // 
            // btnQuickAddSupplier
            // 
            this.btnQuickAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnQuickAddSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickAddSupplier.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnQuickAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAddSupplier.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnQuickAddSupplier.Location = new System.Drawing.Point(3, 3);
            this.btnQuickAddSupplier.Name = "btnQuickAddSupplier";
            this.btnQuickAddSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnQuickAddSupplier.Size = new System.Drawing.Size(177, 28);
            this.btnQuickAddSupplier.TabIndex = 1;
            this.btnQuickAddSupplier.Text = "اضافه مورد";
            this.btnQuickAddSupplier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuickAddSupplier.UseVisualStyleBackColor = false;
            this.btnQuickAddSupplier.Click += new System.EventHandler(this.btnQuickAddSupplier_Click);
            // 
            // lblPurchaseDate
            // 
            this.lblPurchaseDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPurchaseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPurchaseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPurchaseDate.Location = new System.Drawing.Point(428, 0);
            this.lblPurchaseDate.Name = "lblPurchaseDate";
            this.lblPurchaseDate.Size = new System.Drawing.Size(217, 22);
            this.lblPurchaseDate.TabIndex = 2;
            this.lblPurchaseDate.Text = "تاريخ الفاتورة:";
            this.lblPurchaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPurchaseDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseDate.Location = new System.Drawing.Point(428, 31);
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            this.dtpPurchaseDate.Size = new System.Drawing.Size(217, 28);
            this.dtpPurchaseDate.TabIndex = 3;
            // 
            // lblPurchaseNotes
            // 
            this.lblPurchaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPurchaseNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPurchaseNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPurchaseNotes.Location = new System.Drawing.Point(3, 0);
            this.lblPurchaseNotes.Name = "lblPurchaseNotes";
            this.lblPurchaseNotes.Size = new System.Drawing.Size(419, 22);
            this.lblPurchaseNotes.TabIndex = 4;
            this.lblPurchaseNotes.Text = "ملاحظات الفاتورة:";
            this.lblPurchaseNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPurchaseNotes
            // 
            this.txtPurchaseNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaseNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseNotes.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPurchaseNotes.Location = new System.Drawing.Point(3, 31);
            this.txtPurchaseNotes.Name = "txtPurchaseNotes";
            this.txtPurchaseNotes.Size = new System.Drawing.Size(419, 28);
            this.txtPurchaseNotes.TabIndex = 5;
            // 
            // tabHistory
            // 
            this.tabHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabHistory.Controls.Add(this.splitHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 36);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(10);
            this.tabHistory.Size = new System.Drawing.Size(1164, 652);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "سجل فواتير المشتريات السابقة";
            // 
            // splitHistory
            // 
            this.splitHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHistory.Location = new System.Drawing.Point(10, 10);
            this.splitHistory.Name = "splitHistory";
            // 
            // splitHistory.Panel1
            // 
            this.splitHistory.Panel1.Controls.Add(this.pnlHistoryList);
            this.splitHistory.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitHistory.Panel2
            // 
            this.splitHistory.Panel2.Controls.Add(this.pnlHistoryDetails);
            this.splitHistory.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitHistory.Size = new System.Drawing.Size(1144, 632);
            this.splitHistory.SplitterDistance = 572;
            this.splitHistory.TabIndex = 0;
            // 
            // pnlHistoryList
            // 
            this.pnlHistoryList.BackColor = System.Drawing.Color.White;
            this.pnlHistoryList.Controls.Add(this.dgvPurchasesHistory);
            this.pnlHistoryList.Controls.Add(this.lblHistoryTitle);
            this.pnlHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoryList.Location = new System.Drawing.Point(0, 0);
            this.pnlHistoryList.Name = "pnlHistoryList";
            this.pnlHistoryList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlHistoryList.Size = new System.Drawing.Size(572, 632);
            this.pnlHistoryList.TabIndex = 0;
            // 
            // dgvPurchasesHistory
            // 
            this.dgvPurchasesHistory.AllowUserToAddRows = false;
            this.dgvPurchasesHistory.AllowUserToDeleteRows = false;
            this.dgvPurchasesHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchasesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchasesHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchasesHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPurchasesHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchasesHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPurchasesHistory.ColumnHeadersHeight = 48;
            this.dgvPurchasesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPurchasesHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchasesHistory.EnableHeadersVisualStyles = false;
            this.dgvPurchasesHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvPurchasesHistory.Location = new System.Drawing.Point(14, 46);
            this.dgvPurchasesHistory.MultiSelect = false;
            this.dgvPurchasesHistory.Name = "dgvPurchasesHistory";
            this.dgvPurchasesHistory.ReadOnly = true;
            this.dgvPurchasesHistory.RowHeadersVisible = false;
            this.dgvPurchasesHistory.RowHeadersWidth = 51;
            this.dgvPurchasesHistory.RowTemplate.Height = 40;
            this.dgvPurchasesHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchasesHistory.Size = new System.Drawing.Size(544, 572);
            this.dgvPurchasesHistory.TabIndex = 1;
            this.dgvPurchasesHistory.SelectionChanged += new System.EventHandler(this.dgvPurchasesHistory_SelectionChanged);
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHistoryTitle.Location = new System.Drawing.Point(14, 14);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(544, 32);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "قائمة فواتير الشراء المسجلة";
            this.lblHistoryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHistoryDetails
            // 
            this.pnlHistoryDetails.BackColor = System.Drawing.Color.White;
            this.pnlHistoryDetails.Controls.Add(this.dgvPurchaseHistoryDetails);
            this.pnlHistoryDetails.Controls.Add(this.lblHistoryDetailsTitle);
            this.pnlHistoryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoryDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlHistoryDetails.Name = "pnlHistoryDetails";
            this.pnlHistoryDetails.Padding = new System.Windows.Forms.Padding(14);
            this.pnlHistoryDetails.Size = new System.Drawing.Size(568, 632);
            this.pnlHistoryDetails.TabIndex = 0;
            // 
            // dgvPurchaseHistoryDetails
            // 
            this.dgvPurchaseHistoryDetails.AllowUserToAddRows = false;
            this.dgvPurchaseHistoryDetails.AllowUserToDeleteRows = false;
            this.dgvPurchaseHistoryDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseHistoryDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseHistoryDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchaseHistoryDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPurchaseHistoryDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseHistoryDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPurchaseHistoryDetails.ColumnHeadersHeight = 48;
            this.dgvPurchaseHistoryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPurchaseHistoryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseHistoryDetails.EnableHeadersVisualStyles = false;
            this.dgvPurchaseHistoryDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvPurchaseHistoryDetails.Location = new System.Drawing.Point(14, 46);
            this.dgvPurchaseHistoryDetails.MultiSelect = false;
            this.dgvPurchaseHistoryDetails.Name = "dgvPurchaseHistoryDetails";
            this.dgvPurchaseHistoryDetails.ReadOnly = true;
            this.dgvPurchaseHistoryDetails.RowHeadersVisible = false;
            this.dgvPurchaseHistoryDetails.RowHeadersWidth = 51;
            this.dgvPurchaseHistoryDetails.RowTemplate.Height = 40;
            this.dgvPurchaseHistoryDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseHistoryDetails.Size = new System.Drawing.Size(540, 572);
            this.dgvPurchaseHistoryDetails.TabIndex = 1;
            // 
            // lblHistoryDetailsTitle
            // 
            this.lblHistoryDetailsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistoryDetailsTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHistoryDetailsTitle.Location = new System.Drawing.Point(14, 14);
            this.lblHistoryDetailsTitle.Name = "lblHistoryDetailsTitle";
            this.lblHistoryDetailsTitle.Size = new System.Drawing.Size(540, 32);
            this.lblHistoryDetailsTitle.TabIndex = 0;
            this.lblHistoryDetailsTitle.Text = "تفاصيل وأصناف الفاتورة المحددة";
            this.lblHistoryDetailsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PurchasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.tabPurchases);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchasesForm";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "فواتير المشتريات";
            this.Load += new System.EventHandler(this.PurchasesForm_Load);
            this.tabPurchases.ResumeLayout(false);
            this.tabNewPurchase.ResumeLayout(false);
            this.pnlNewPurchaseContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).EndInit();
            this.pnlPurchaseBottom.ResumeLayout(false);
            this.tlpPurchaseBottom.ResumeLayout(false);
            this.tlpPurchaseBottom.PerformLayout();
            this.pnlTotalDisplay.ResumeLayout(false);
            this.flpPurchaseActions.ResumeLayout(false);
            this.pnlAddItem.ResumeLayout(false);
            this.tlpAddItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchaseQty)).EndInit();
            this.pnlSupplierHeader.ResumeLayout(false);
            this.tlpSupplierHeader.ResumeLayout(false);
            this.tlpSupplierHeader.PerformLayout();
            this.pnlSupplierSelect.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.splitHistory.Panel1.ResumeLayout(false);
            this.splitHistory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHistory)).EndInit();
            this.splitHistory.ResumeLayout(false);
            this.pnlHistoryList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchasesHistory)).EndInit();
            this.pnlHistoryDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistoryDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPurchases;
        private System.Windows.Forms.TabPage tabNewPurchase;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Panel pnlNewPurchaseContainer;
        private System.Windows.Forms.Panel pnlSupplierHeader;
        private System.Windows.Forms.TableLayoutPanel tlpSupplierHeader;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Panel pnlSupplierSelect;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Button btnQuickAddSupplier;
        private System.Windows.Forms.Label lblPurchaseDate;
        private System.Windows.Forms.DateTimePicker dtpPurchaseDate;
        private System.Windows.Forms.Label lblPurchaseNotes;
        private System.Windows.Forms.TextBox txtPurchaseNotes;
        private System.Windows.Forms.Panel pnlAddItem;
        private System.Windows.Forms.TableLayoutPanel tlpAddItem;
        private System.Windows.Forms.Label lblSelectProduct;
        private System.Windows.Forms.ComboBox cmbSelectProduct;
        private System.Windows.Forms.Label lblUnitCost;
        private System.Windows.Forms.NumericUpDown numUnitCost;
        private System.Windows.Forms.Label lblPurchaseQty;
        private System.Windows.Forms.NumericUpDown numPurchaseQty;
        private System.Windows.Forms.Button btnQuickAddProduct;
        private System.Windows.Forms.DataGridView dgvPurchaseItems;
        private System.Windows.Forms.Panel pnlPurchaseBottom;
        private System.Windows.Forms.TableLayoutPanel tlpPurchaseBottom;
        private System.Windows.Forms.Panel pnlTotalDisplay;
        private System.Windows.Forms.Label lblTotalPurchaseTitle;
        private System.Windows.Forms.Label lblTotalPurchaseVal;
        private System.Windows.Forms.CheckBox chkUpdateBuyPrice;
        private System.Windows.Forms.FlowLayoutPanel flpPurchaseActions;
        private System.Windows.Forms.Button btnSavePurchase;
        private System.Windows.Forms.Button btnResetPurchase;
        private System.Windows.Forms.SplitContainer splitHistory;
        private System.Windows.Forms.Panel pnlHistoryList;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvPurchasesHistory;
        private System.Windows.Forms.Panel pnlHistoryDetails;
        private System.Windows.Forms.Label lblHistoryDetailsTitle;
        private System.Windows.Forms.DataGridView dgvPurchaseHistoryDetails;
    }
}
