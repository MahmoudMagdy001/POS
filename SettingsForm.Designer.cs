namespace POS
{
    partial class SettingsForm
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
            this.pnlTopBanner = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBottomBar = new System.Windows.Forms.Panel();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            this.btnTestReceipt = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabStoreInfo = new System.Windows.Forms.TabPage();
            this.pnlStoreInfo = new System.Windows.Forms.Panel();
            this.tlpStoreInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.lblStoreSubtitle = new System.Windows.Forms.Label();
            this.txtStoreSubtitle = new System.Windows.Forms.TextBox();
            this.lblStorePhone = new System.Windows.Forms.Label();
            this.txtStorePhone = new System.Windows.Forms.TextBox();
            this.lblStoreAddress = new System.Windows.Forms.Label();
            this.txtStoreAddress = new System.Windows.Forms.TextBox();
            this.lblTaxNumber = new System.Windows.Forms.Label();
            this.txtTaxNumber = new System.Windows.Forms.TextBox();
            this.tabPrinting = new System.Windows.Forms.TabPage();
            this.pnlPrinting = new System.Windows.Forms.Panel();
            this.tlpPrinting = new System.Windows.Forms.TableLayoutPanel();
            this.lblReceiptHeader = new System.Windows.Forms.Label();
            this.txtReceiptHeader = new System.Windows.Forms.TextBox();
            this.lblReceiptFooter = new System.Windows.Forms.Label();
            this.txtReceiptFooter = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtCurrencySymbol = new System.Windows.Forms.TextBox();
            this.lblVatRate = new System.Windows.Forms.Label();
            this.nudVatRate = new System.Windows.Forms.NumericUpDown();
            this.chkEnablePrintPreview = new System.Windows.Forms.CheckBox();
            this.chkAutoPrintOnSale = new System.Windows.Forms.CheckBox();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.pnlInventory = new System.Windows.Forms.Panel();
            this.tlpInventory = new System.Windows.Forms.TableLayoutPanel();
            this.lblDefaultMinStock = new System.Windows.Forms.Label();
            this.nudDefaultMinStock = new System.Windows.Forms.NumericUpDown();
            this.chkAllowNegativeStock = new System.Windows.Forms.CheckBox();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.pnlDatabase = new System.Windows.Forms.Panel();
            this.tlpDatabase = new System.Windows.Forms.TableLayoutPanel();
            this.lblDbInfoTitle = new System.Windows.Forms.Label();
            this.lblDbInfoVal = new System.Windows.Forms.Label();
            this.btnBackupDb = new System.Windows.Forms.Button();
            this.btnRestoreDb = new System.Windows.Forms.Button();
            this.pnlDangerZone = new System.Windows.Forms.Panel();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.lblDangerDesc = new System.Windows.Forms.Label();
            this.lblDangerTitle = new System.Windows.Forms.Label();
            this.pnlTopBanner.SuspendLayout();
            this.pnlBottomBar.SuspendLayout();
            this.flpActions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabStoreInfo.SuspendLayout();
            this.pnlStoreInfo.SuspendLayout();
            this.tlpStoreInfo.SuspendLayout();
            this.tabPrinting.SuspendLayout();
            this.pnlPrinting.SuspendLayout();
            this.tlpPrinting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVatRate)).BeginInit();
            this.tabInventory.SuspendLayout();
            this.pnlInventory.SuspendLayout();
            this.tlpInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultMinStock)).BeginInit();
            this.tabDatabase.SuspendLayout();
            this.pnlDatabase.SuspendLayout();
            this.tlpDatabase.SuspendLayout();
            this.pnlDangerZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBanner
            // 
            this.pnlTopBanner.BackColor = System.Drawing.Color.White;
            this.pnlTopBanner.Controls.Add(this.lblSubtitle);
            this.pnlTopBanner.Controls.Add(this.lblTitle);
            this.pnlTopBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBanner.Location = new System.Drawing.Point(14, 14);
            this.pnlTopBanner.Name = "pnlTopBanner";
            this.pnlTopBanner.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.pnlTopBanner.Size = new System.Drawing.Size(1092, 80);
            this.pnlTopBanner.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubtitle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(18, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(1056, 22);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "تخصيص بيانات المنشأة، سياسات الفواتير والطباعة، التنبيهات، وإدارة النسخ الاحتياط" +
    "ي (متاح لمدير النظام فقط)";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1056, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚙️ إعدادات النظام العامة والتحكم";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBottomBar
            // 
            this.pnlBottomBar.BackColor = System.Drawing.Color.White;
            this.pnlBottomBar.Controls.Add(this.flpActions);
            this.pnlBottomBar.Controls.Add(this.lblStatus);
            this.pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomBar.Location = new System.Drawing.Point(14, 656);
            this.pnlBottomBar.Name = "pnlBottomBar";
            this.pnlBottomBar.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.pnlBottomBar.Size = new System.Drawing.Size(1092, 60);
            this.pnlBottomBar.TabIndex = 1;
            // 
            // flpActions
            // 
            this.flpActions.Controls.Add(this.btnSave);
            this.flpActions.Controls.Add(this.btnResetDefaults);
            this.flpActions.Controls.Add(this.btnTestReceipt);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpActions.Location = new System.Drawing.Point(18, 12);
            this.flpActions.Name = "flpActions";
            this.flpActions.Size = new System.Drawing.Size(550, 36);
            this.flpActions.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(390, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0);
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSave.Size = new System.Drawing.Size(160, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 حفظ التغييرات";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnResetDefaults
            // 
            this.btnResetDefaults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnResetDefaults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetDefaults.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnResetDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetDefaults.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetDefaults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnResetDefaults.Location = new System.Drawing.Point(210, 0);
            this.btnResetDefaults.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Padding = new System.Windows.Forms.Padding(0);
            this.btnResetDefaults.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnResetDefaults.Size = new System.Drawing.Size(170, 36);
            this.btnResetDefaults.TabIndex = 1;
            this.btnResetDefaults.Text = "🔄 استعادة الافتراضي";
            this.btnResetDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnResetDefaults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetDefaults.UseVisualStyleBackColor = false;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            // 
            // btnTestReceipt
            // 
            this.btnTestReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTestReceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestReceipt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTestReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestReceipt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTestReceipt.Location = new System.Drawing.Point(40, 0);
            this.btnTestReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.btnTestReceipt.Name = "btnTestReceipt";
            this.btnTestReceipt.Padding = new System.Windows.Forms.Padding(0);
            this.btnTestReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTestReceipt.Size = new System.Drawing.Size(160, 36);
            this.btnTestReceipt.TabIndex = 2;
            this.btnTestReceipt.Text = "معاينة إيصال";
            this.btnTestReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTestReceipt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTestReceipt.UseVisualStyleBackColor = false;
            this.btnTestReceipt.Click += new System.EventHandler(this.btnTestReceipt_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblStatus.Location = new System.Drawing.Point(600, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(474, 36);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabSettings);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(14, 94);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlMain.Size = new System.Drawing.Size(1092, 562);
            this.pnlMain.TabIndex = 2;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabStoreInfo);
            this.tabSettings.Controls.Add(this.tabPrinting);
            this.tabSettings.Controls.Add(this.tabInventory);
            this.tabSettings.Controls.Add(this.tabDatabase);
            this.tabSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSettings.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.tabSettings.ItemSize = new System.Drawing.Size(260, 34);
            this.tabSettings.Location = new System.Drawing.Point(0, 10);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Drawing.Point(20, 6);
            this.tabSettings.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabSettings.RightToLeftLayout = true;
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(1092, 542);
            this.tabSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSettings.TabIndex = 0;
            // 
            // tabStoreInfo
            // 
            this.tabStoreInfo.BackColor = System.Drawing.Color.White;
            this.tabStoreInfo.Controls.Add(this.pnlStoreInfo);
            this.tabStoreInfo.Location = new System.Drawing.Point(4, 38);
            this.tabStoreInfo.Name = "tabStoreInfo";
            this.tabStoreInfo.Padding = new System.Windows.Forms.Padding(20);
            this.tabStoreInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabStoreInfo.Size = new System.Drawing.Size(1084, 500);
            this.tabStoreInfo.TabIndex = 0;
            this.tabStoreInfo.Text = "🏢  بيانات المنشأة والمتجر";
            // 
            // pnlStoreInfo
            // 
            this.pnlStoreInfo.AutoScroll = true;
            this.pnlStoreInfo.Controls.Add(this.tlpStoreInfo);
            this.pnlStoreInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStoreInfo.Location = new System.Drawing.Point(20, 20);
            this.pnlStoreInfo.Name = "pnlStoreInfo";
            this.pnlStoreInfo.Size = new System.Drawing.Size(1044, 456);
            this.pnlStoreInfo.TabIndex = 0;
            // 
            // tlpStoreInfo
            // 
            this.tlpStoreInfo.ColumnCount = 2;
            this.tlpStoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpStoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStoreInfo.Controls.Add(this.lblStoreName, 0, 0);
            this.tlpStoreInfo.Controls.Add(this.txtStoreName, 1, 0);
            this.tlpStoreInfo.Controls.Add(this.lblStoreSubtitle, 0, 1);
            this.tlpStoreInfo.Controls.Add(this.txtStoreSubtitle, 1, 1);
            this.tlpStoreInfo.Controls.Add(this.lblStorePhone, 0, 2);
            this.tlpStoreInfo.Controls.Add(this.txtStorePhone, 1, 2);
            this.tlpStoreInfo.Controls.Add(this.lblStoreAddress, 0, 3);
            this.tlpStoreInfo.Controls.Add(this.txtStoreAddress, 1, 3);
            this.tlpStoreInfo.Controls.Add(this.lblTaxNumber, 0, 4);
            this.tlpStoreInfo.Controls.Add(this.txtTaxNumber, 1, 4);
            this.tlpStoreInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpStoreInfo.Location = new System.Drawing.Point(0, 0);
            this.tlpStoreInfo.Name = "tlpStoreInfo";
            this.tlpStoreInfo.RowCount = 5;
            this.tlpStoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpStoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpStoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpStoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpStoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpStoreInfo.Size = new System.Drawing.Size(1044, 275);
            this.tlpStoreInfo.TabIndex = 0;
            // 
            // lblStoreName
            // 
            this.lblStoreName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStoreName.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStoreName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStoreName.Location = new System.Drawing.Point(827, 0);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(214, 55);
            this.lblStoreName.TabIndex = 0;
            this.lblStoreName.Text = "اسم المتجر / المحل:";
            this.lblStoreName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStoreName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreName.Location = new System.Drawing.Point(3, 12);
            this.txtStoreName.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(818, 28);
            this.txtStoreName.TabIndex = 1;
            // 
            // lblStoreSubtitle
            // 
            this.lblStoreSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStoreSubtitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStoreSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStoreSubtitle.Location = new System.Drawing.Point(827, 55);
            this.lblStoreSubtitle.Name = "lblStoreSubtitle";
            this.lblStoreSubtitle.Size = new System.Drawing.Size(214, 55);
            this.lblStoreSubtitle.TabIndex = 2;
            this.lblStoreSubtitle.Text = "وصف النشاط / الشعار:";
            this.lblStoreSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStoreSubtitle
            // 
            this.txtStoreSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStoreSubtitle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreSubtitle.Location = new System.Drawing.Point(3, 67);
            this.txtStoreSubtitle.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtStoreSubtitle.Name = "txtStoreSubtitle";
            this.txtStoreSubtitle.Size = new System.Drawing.Size(818, 28);
            this.txtStoreSubtitle.TabIndex = 3;
            // 
            // lblStorePhone
            // 
            this.lblStorePhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStorePhone.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStorePhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStorePhone.Location = new System.Drawing.Point(827, 110);
            this.lblStorePhone.Name = "lblStorePhone";
            this.lblStorePhone.Size = new System.Drawing.Size(214, 55);
            this.lblStorePhone.TabIndex = 4;
            this.lblStorePhone.Text = "رقم الهاتف / خدمة العملاء:";
            this.lblStorePhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStorePhone
            // 
            this.txtStorePhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStorePhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStorePhone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStorePhone.Location = new System.Drawing.Point(3, 122);
            this.txtStorePhone.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtStorePhone.Name = "txtStorePhone";
            this.txtStorePhone.Size = new System.Drawing.Size(818, 28);
            this.txtStorePhone.TabIndex = 5;
            // 
            // lblStoreAddress
            // 
            this.lblStoreAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStoreAddress.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStoreAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStoreAddress.Location = new System.Drawing.Point(827, 165);
            this.lblStoreAddress.Name = "lblStoreAddress";
            this.lblStoreAddress.Size = new System.Drawing.Size(214, 55);
            this.lblStoreAddress.TabIndex = 6;
            this.lblStoreAddress.Text = "العنوان والمقر:";
            this.lblStoreAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStoreAddress
            // 
            this.txtStoreAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStoreAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreAddress.Location = new System.Drawing.Point(3, 177);
            this.txtStoreAddress.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtStoreAddress.Name = "txtStoreAddress";
            this.txtStoreAddress.Size = new System.Drawing.Size(818, 28);
            this.txtStoreAddress.TabIndex = 7;
            // 
            // lblTaxNumber
            // 
            this.lblTaxNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaxNumber.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTaxNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTaxNumber.Location = new System.Drawing.Point(827, 220);
            this.lblTaxNumber.Name = "lblTaxNumber";
            this.lblTaxNumber.Size = new System.Drawing.Size(214, 55);
            this.lblTaxNumber.TabIndex = 8;
            this.lblTaxNumber.Text = "الرقم الضريبي / السجل:";
            this.lblTaxNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTaxNumber
            // 
            this.txtTaxNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTaxNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTaxNumber.Location = new System.Drawing.Point(3, 232);
            this.txtTaxNumber.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.Size = new System.Drawing.Size(818, 28);
            this.txtTaxNumber.TabIndex = 9;
            // 
            // tabPrinting
            // 
            this.tabPrinting.BackColor = System.Drawing.Color.White;
            this.tabPrinting.Controls.Add(this.pnlPrinting);
            this.tabPrinting.Location = new System.Drawing.Point(4, 38);
            this.tabPrinting.Name = "tabPrinting";
            this.tabPrinting.Padding = new System.Windows.Forms.Padding(20);
            this.tabPrinting.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabPrinting.Size = new System.Drawing.Size(1084, 500);
            this.tabPrinting.TabIndex = 1;
            this.tabPrinting.Text = "🧾  سياسات الفواتير والطباعة";
            // 
            // pnlPrinting
            // 
            this.pnlPrinting.AutoScroll = true;
            this.pnlPrinting.Controls.Add(this.tlpPrinting);
            this.pnlPrinting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrinting.Location = new System.Drawing.Point(20, 20);
            this.pnlPrinting.Name = "pnlPrinting";
            this.pnlPrinting.Size = new System.Drawing.Size(1044, 456);
            this.pnlPrinting.TabIndex = 0;
            // 
            // tlpPrinting
            // 
            this.tlpPrinting.ColumnCount = 2;
            this.tlpPrinting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlpPrinting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrinting.Controls.Add(this.lblReceiptHeader, 0, 0);
            this.tlpPrinting.Controls.Add(this.txtReceiptHeader, 1, 0);
            this.tlpPrinting.Controls.Add(this.lblReceiptFooter, 0, 1);
            this.tlpPrinting.Controls.Add(this.txtReceiptFooter, 1, 1);
            this.tlpPrinting.Controls.Add(this.lblCurrency, 0, 2);
            this.tlpPrinting.Controls.Add(this.txtCurrencySymbol, 1, 2);
            this.tlpPrinting.Controls.Add(this.lblVatRate, 0, 3);
            this.tlpPrinting.Controls.Add(this.nudVatRate, 1, 3);
            this.tlpPrinting.Controls.Add(this.chkEnablePrintPreview, 1, 4);
            this.tlpPrinting.Controls.Add(this.chkAutoPrintOnSale, 1, 5);
            this.tlpPrinting.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpPrinting.Location = new System.Drawing.Point(0, 0);
            this.tlpPrinting.Name = "tlpPrinting";
            this.tlpPrinting.RowCount = 6;
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpPrinting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpPrinting.Size = new System.Drawing.Size(1044, 325);
            this.tlpPrinting.TabIndex = 0;
            // 
            // lblReceiptHeader
            // 
            this.lblReceiptHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptHeader.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblReceiptHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblReceiptHeader.Location = new System.Drawing.Point(817, 0);
            this.lblReceiptHeader.Name = "lblReceiptHeader";
            this.lblReceiptHeader.Size = new System.Drawing.Size(224, 55);
            this.lblReceiptHeader.TabIndex = 0;
            this.lblReceiptHeader.Text = "ترويسة الفاتورة:";
            this.lblReceiptHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReceiptHeader
            // 
            this.txtReceiptHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceiptHeader.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtReceiptHeader.Location = new System.Drawing.Point(3, 12);
            this.txtReceiptHeader.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtReceiptHeader.Name = "txtReceiptHeader";
            this.txtReceiptHeader.Size = new System.Drawing.Size(808, 28);
            this.txtReceiptHeader.TabIndex = 1;
            // 
            // lblReceiptFooter
            // 
            this.lblReceiptFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptFooter.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblReceiptFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblReceiptFooter.Location = new System.Drawing.Point(817, 55);
            this.lblReceiptFooter.Name = "lblReceiptFooter";
            this.lblReceiptFooter.Size = new System.Drawing.Size(224, 70);
            this.lblReceiptFooter.TabIndex = 2;
            this.lblReceiptFooter.Text = "تذييل الفاتورة وملاحظة الاستبدال:";
            this.lblReceiptFooter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReceiptFooter
            // 
            this.txtReceiptFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceiptFooter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtReceiptFooter.Location = new System.Drawing.Point(3, 67);
            this.txtReceiptFooter.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtReceiptFooter.Multiline = true;
            this.txtReceiptFooter.Name = "txtReceiptFooter";
            this.txtReceiptFooter.Size = new System.Drawing.Size(808, 48);
            this.txtReceiptFooter.TabIndex = 3;
            // 
            // lblCurrency
            // 
            this.lblCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCurrency.Location = new System.Drawing.Point(817, 125);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(224, 55);
            this.lblCurrency.TabIndex = 4;
            this.lblCurrency.Text = "رمز العملة المستخدم:";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCurrencySymbol
            // 
            this.txtCurrencySymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrencySymbol.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCurrencySymbol.Location = new System.Drawing.Point(611, 137);
            this.txtCurrencySymbol.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.txtCurrencySymbol.Name = "txtCurrencySymbol";
            this.txtCurrencySymbol.Size = new System.Drawing.Size(200, 28);
            this.txtCurrencySymbol.TabIndex = 5;
            // 
            // lblVatRate
            // 
            this.lblVatRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVatRate.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVatRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblVatRate.Location = new System.Drawing.Point(817, 180);
            this.lblVatRate.Name = "lblVatRate";
            this.lblVatRate.Size = new System.Drawing.Size(224, 55);
            this.lblVatRate.TabIndex = 6;
            this.lblVatRate.Text = "نسبة الضريبة (VAT %):";
            this.lblVatRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudVatRate
            // 
            this.nudVatRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudVatRate.DecimalPlaces = 2;
            this.nudVatRate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.nudVatRate.Location = new System.Drawing.Point(611, 192);
            this.nudVatRate.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.nudVatRate.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudVatRate.Name = "nudVatRate";
            this.nudVatRate.Size = new System.Drawing.Size(200, 28);
            this.nudVatRate.TabIndex = 7;
            // 
            // chkEnablePrintPreview
            // 
            this.chkEnablePrintPreview.AutoSize = true;
            this.chkEnablePrintPreview.Checked = true;
            this.chkEnablePrintPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnablePrintPreview.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkEnablePrintPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkEnablePrintPreview.Location = new System.Drawing.Point(544, 245);
            this.chkEnablePrintPreview.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chkEnablePrintPreview.Name = "chkEnablePrintPreview";
            this.chkEnablePrintPreview.Size = new System.Drawing.Size(267, 25);
            this.chkEnablePrintPreview.TabIndex = 8;
            this.chkEnablePrintPreview.Text = "عرض معاينة الإيصال قبل إرساله للطابعة";
            this.chkEnablePrintPreview.UseVisualStyleBackColor = true;
            // 
            // chkAutoPrintOnSale
            // 
            this.chkAutoPrintOnSale.AutoSize = true;
            this.chkAutoPrintOnSale.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkAutoPrintOnSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkAutoPrintOnSale.Location = new System.Drawing.Point(524, 290);
            this.chkAutoPrintOnSale.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chkAutoPrintOnSale.Name = "chkAutoPrintOnSale";
            this.chkAutoPrintOnSale.Size = new System.Drawing.Size(287, 25);
            this.chkAutoPrintOnSale.TabIndex = 9;
            this.chkAutoPrintOnSale.Text = "طباعة الفاتورة آلياً فور إتمام عملية البيع (POS)";
            this.chkAutoPrintOnSale.UseVisualStyleBackColor = true;
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.White;
            this.tabInventory.Controls.Add(this.pnlInventory);
            this.tabInventory.Location = new System.Drawing.Point(4, 38);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(20);
            this.tabInventory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabInventory.Size = new System.Drawing.Size(1084, 500);
            this.tabInventory.TabIndex = 2;
            this.tabInventory.Text = "📦  المخزون والعمليات";
            // 
            // pnlInventory
            // 
            this.pnlInventory.AutoScroll = true;
            this.pnlInventory.Controls.Add(this.tlpInventory);
            this.pnlInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInventory.Location = new System.Drawing.Point(20, 20);
            this.pnlInventory.Name = "pnlInventory";
            this.pnlInventory.Size = new System.Drawing.Size(1044, 460);
            this.pnlInventory.TabIndex = 0;
            // 
            // tlpInventory
            // 
            this.tlpInventory.ColumnCount = 2;
            this.tlpInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tlpInventory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInventory.Controls.Add(this.lblDefaultMinStock, 0, 0);
            this.tlpInventory.Controls.Add(this.nudDefaultMinStock, 1, 0);
            this.tlpInventory.Controls.Add(this.chkAllowNegativeStock, 1, 1);
            this.tlpInventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInventory.Location = new System.Drawing.Point(0, 0);
            this.tlpInventory.Name = "tlpInventory";
            this.tlpInventory.RowCount = 2;
            this.tlpInventory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpInventory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpInventory.Size = new System.Drawing.Size(1044, 120);
            this.tlpInventory.TabIndex = 0;
            // 
            // lblDefaultMinStock
            // 
            this.lblDefaultMinStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefaultMinStock.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDefaultMinStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDefaultMinStock.Location = new System.Drawing.Point(787, 0);
            this.lblDefaultMinStock.Name = "lblDefaultMinStock";
            this.lblDefaultMinStock.Size = new System.Drawing.Size(254, 60);
            this.lblDefaultMinStock.TabIndex = 0;
            this.lblDefaultMinStock.Text = "الحد الأدنى الافتراضي لتنبيه النواقص:";
            this.lblDefaultMinStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudDefaultMinStock
            // 
            this.nudDefaultMinStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDefaultMinStock.Font = new System.Drawing.Font("Tahoma", 10F);
            this.nudDefaultMinStock.Location = new System.Drawing.Point(581, 15);
            this.nudDefaultMinStock.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.nudDefaultMinStock.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDefaultMinStock.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDefaultMinStock.Name = "nudDefaultMinStock";
            this.nudDefaultMinStock.Size = new System.Drawing.Size(200, 28);
            this.nudDefaultMinStock.TabIndex = 1;
            this.nudDefaultMinStock.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkAllowNegativeStock
            // 
            this.chkAllowNegativeStock.AutoSize = true;
            this.chkAllowNegativeStock.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkAllowNegativeStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkAllowNegativeStock.Location = new System.Drawing.Point(472, 75);
            this.chkAllowNegativeStock.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.chkAllowNegativeStock.Name = "chkAllowNegativeStock";
            this.chkAllowNegativeStock.Size = new System.Drawing.Size(309, 25);
            this.chkAllowNegativeStock.TabIndex = 2;
            this.chkAllowNegativeStock.Text = "السماح بإتمام عمليات البيع عند نفاد المخزون (بالسالب)";
            this.chkAllowNegativeStock.UseVisualStyleBackColor = true;
            // 
            // tabDatabase
            // 
            this.tabDatabase.BackColor = System.Drawing.Color.White;
            this.tabDatabase.Controls.Add(this.pnlDatabase);
            this.tabDatabase.Location = new System.Drawing.Point(4, 38);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(20);
            this.tabDatabase.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabDatabase.Size = new System.Drawing.Size(1084, 500);
            this.tabDatabase.TabIndex = 3;
            this.tabDatabase.Text = "💾  النسخ الاحتياطي والصيانة";
            // 
            // pnlDatabase
            // 
            this.pnlDatabase.AutoScroll = true;
            this.pnlDatabase.Controls.Add(this.tlpDatabase);
            this.pnlDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatabase.Location = new System.Drawing.Point(20, 20);
            this.pnlDatabase.Name = "pnlDatabase";
            this.pnlDatabase.Size = new System.Drawing.Size(1044, 456);
            this.pnlDatabase.TabIndex = 0;
            // 
            // tlpDatabase
            // 
            this.tlpDatabase.ColumnCount = 2;
            this.tlpDatabase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDatabase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDatabase.Controls.Add(this.lblDbInfoTitle, 0, 0);
            this.tlpDatabase.Controls.Add(this.lblDbInfoVal, 0, 1);
            this.tlpDatabase.Controls.Add(this.btnBackupDb, 0, 2);
            this.tlpDatabase.Controls.Add(this.btnRestoreDb, 1, 2);
            this.tlpDatabase.Controls.Add(this.pnlDangerZone, 0, 3);
            this.tlpDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDatabase.Location = new System.Drawing.Point(0, 0);
            this.tlpDatabase.Name = "tlpDatabase";
            this.tlpDatabase.RowCount = 4;
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpDatabase.Size = new System.Drawing.Size(1044, 280);
            this.tlpDatabase.TabIndex = 0;
            // 
            // lblDbInfoTitle
            // 
            this.tlpDatabase.SetColumnSpan(this.lblDbInfoTitle, 2);
            this.lblDbInfoTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDbInfoTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDbInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDbInfoTitle.Location = new System.Drawing.Point(3, 0);
            this.lblDbInfoTitle.Name = "lblDbInfoTitle";
            this.lblDbInfoTitle.Size = new System.Drawing.Size(1038, 35);
            this.lblDbInfoTitle.TabIndex = 0;
            this.lblDbInfoTitle.Text = "📁 معلومات قاعدة البيانات الحالية:";
            this.lblDbInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDbInfoVal
            // 
            this.tlpDatabase.SetColumnSpan(this.lblDbInfoVal, 2);
            this.lblDbInfoVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDbInfoVal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblDbInfoVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDbInfoVal.Location = new System.Drawing.Point(3, 35);
            this.lblDbInfoVal.Name = "lblDbInfoVal";
            this.lblDbInfoVal.Size = new System.Drawing.Size(1038, 45);
            this.lblDbInfoVal.TabIndex = 1;
            this.lblDbInfoVal.Text = "قاعدة البيانات: Microsoft SQL Server (LocalDB) • الاسم: POS_DB • حالة الاتصال: مت" +
    "صل وجاهز";
            this.lblDbInfoVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBackupDb
            // 
            this.btnBackupDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnBackupDb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackupDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBackupDb.FlatAppearance.BorderSize = 0;
            this.btnBackupDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupDb.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBackupDb.ForeColor = System.Drawing.Color.White;
            this.btnBackupDb.Location = new System.Drawing.Point(525, 83);
            this.btnBackupDb.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.btnBackupDb.Name = "btnBackupDb";
            this.btnBackupDb.Padding = new System.Windows.Forms.Padding(0);
            this.btnBackupDb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBackupDb.Size = new System.Drawing.Size(509, 47);
            this.btnBackupDb.TabIndex = 2;
            this.btnBackupDb.Text = "💾 أخذ نسخة احتياطية من قاعدة البيانات (.bak)";
            this.btnBackupDb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBackupDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackupDb.UseVisualStyleBackColor = false;
            this.btnBackupDb.Click += new System.EventHandler(this.btnBackupDb_Click);
            // 
            // btnRestoreDb
            // 
            this.btnRestoreDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRestoreDb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestoreDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestoreDb.FlatAppearance.BorderSize = 0;
            this.btnRestoreDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreDb.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRestoreDb.ForeColor = System.Drawing.Color.White;
            this.btnRestoreDb.Location = new System.Drawing.Point(10, 83);
            this.btnRestoreDb.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.btnRestoreDb.Name = "btnRestoreDb";
            this.btnRestoreDb.Padding = new System.Windows.Forms.Padding(0);
            this.btnRestoreDb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRestoreDb.Size = new System.Drawing.Size(509, 47);
            this.btnRestoreDb.TabIndex = 3;
            this.btnRestoreDb.Text = "🔄 استعادة قاعدة بيانات من نسخة احتياطية (.bak)";
            this.btnRestoreDb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRestoreDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestoreDb.UseVisualStyleBackColor = false;
            this.btnRestoreDb.Click += new System.EventHandler(this.btnRestoreDb_Click);
            // 
            // pnlDangerZone
            // 
            this.pnlDangerZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.tlpDatabase.SetColumnSpan(this.pnlDangerZone, 2);
            this.pnlDangerZone.Controls.Add(this.btnClearHistory);
            this.pnlDangerZone.Controls.Add(this.lblDangerDesc);
            this.pnlDangerZone.Controls.Add(this.lblDangerTitle);
            this.pnlDangerZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDangerZone.Location = new System.Drawing.Point(3, 150);
            this.pnlDangerZone.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnlDangerZone.Name = "pnlDangerZone";
            this.pnlDangerZone.Padding = new System.Windows.Forms.Padding(15);
            this.pnlDangerZone.Size = new System.Drawing.Size(1038, 127);
            this.pnlDangerZone.TabIndex = 4;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnClearHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearHistory.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClearHistory.FlatAppearance.BorderSize = 0;
            this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearHistory.ForeColor = System.Drawing.Color.White;
            this.btnClearHistory.Location = new System.Drawing.Point(15, 75);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Padding = new System.Windows.Forms.Padding(0);
            this.btnClearHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClearHistory.Size = new System.Drawing.Size(260, 37);
            this.btnClearHistory.TabIndex = 2;
            this.btnClearHistory.Text = "🗑️ تصفير المعاملات وبدء تشغيل جديد";
            this.btnClearHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClearHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearHistory.UseVisualStyleBackColor = false;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // lblDangerDesc
            // 
            this.lblDangerDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDangerDesc.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblDangerDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblDangerDesc.Location = new System.Drawing.Point(15, 40);
            this.lblDangerDesc.Name = "lblDangerDesc";
            this.lblDangerDesc.Size = new System.Drawing.Size(1008, 35);
            this.lblDangerDesc.TabIndex = 1;
            this.lblDangerDesc.Text = "يقوم هذا الإجراء بحذف كافة سجلات المبيعات والمشتريات والمرتجعات التجريبية مع الإ" +
    "بقاء على المنتجات والأقسام والمستخدمين.";
            this.lblDangerDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDangerTitle
            // 
            this.lblDangerTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDangerTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDangerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblDangerTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDangerTitle.Name = "lblDangerTitle";
            this.lblDangerTitle.Size = new System.Drawing.Size(1008, 25);
            this.lblDangerTitle.TabIndex = 0;
            this.lblDangerTitle.Text = "⚠️ منطقة الإجراءات المتقدمة والحساسة:";
            this.lblDangerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1120, 730);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottomBar);
            this.Controls.Add(this.pnlTopBanner);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(14);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "إعدادات النظام";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.pnlTopBanner.ResumeLayout(false);
            this.pnlBottomBar.ResumeLayout(false);
            this.flpActions.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabStoreInfo.ResumeLayout(false);
            this.pnlStoreInfo.ResumeLayout(false);
            this.tlpStoreInfo.ResumeLayout(false);
            this.tlpStoreInfo.PerformLayout();
            this.tabPrinting.ResumeLayout(false);
            this.pnlPrinting.ResumeLayout(false);
            this.tlpPrinting.ResumeLayout(false);
            this.tlpPrinting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVatRate)).EndInit();
            this.tabInventory.ResumeLayout(false);
            this.pnlInventory.ResumeLayout(false);
            this.tlpInventory.ResumeLayout(false);
            this.tlpInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultMinStock)).EndInit();
            this.tabDatabase.ResumeLayout(false);
            this.pnlDatabase.ResumeLayout(false);
            this.tlpDatabase.ResumeLayout(false);
            this.pnlDangerZone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBanner;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnResetDefaults;
        private System.Windows.Forms.Button btnTestReceipt;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabStoreInfo;
        private System.Windows.Forms.Panel pnlStoreInfo;
        private System.Windows.Forms.TableLayoutPanel tlpStoreInfo;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.Label lblStoreSubtitle;
        private System.Windows.Forms.TextBox txtStoreSubtitle;
        private System.Windows.Forms.Label lblStorePhone;
        private System.Windows.Forms.TextBox txtStorePhone;
        private System.Windows.Forms.Label lblStoreAddress;
        private System.Windows.Forms.TextBox txtStoreAddress;
        private System.Windows.Forms.Label lblTaxNumber;
        private System.Windows.Forms.TextBox txtTaxNumber;
        private System.Windows.Forms.TabPage tabPrinting;
        private System.Windows.Forms.Panel pnlPrinting;
        private System.Windows.Forms.TableLayoutPanel tlpPrinting;
        private System.Windows.Forms.Label lblReceiptHeader;
        private System.Windows.Forms.TextBox txtReceiptHeader;
        private System.Windows.Forms.Label lblReceiptFooter;
        private System.Windows.Forms.TextBox txtReceiptFooter;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.TextBox txtCurrencySymbol;
        private System.Windows.Forms.Label lblVatRate;
        private System.Windows.Forms.NumericUpDown nudVatRate;
        private System.Windows.Forms.CheckBox chkEnablePrintPreview;
        private System.Windows.Forms.CheckBox chkAutoPrintOnSale;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.Panel pnlInventory;
        private System.Windows.Forms.TableLayoutPanel tlpInventory;
        private System.Windows.Forms.Label lblDefaultMinStock;
        private System.Windows.Forms.NumericUpDown nudDefaultMinStock;
        private System.Windows.Forms.CheckBox chkAllowNegativeStock;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.Panel pnlDatabase;
        private System.Windows.Forms.TableLayoutPanel tlpDatabase;
        private System.Windows.Forms.Label lblDbInfoTitle;
        private System.Windows.Forms.Label lblDbInfoVal;
        private System.Windows.Forms.Button btnBackupDb;
        private System.Windows.Forms.Button btnRestoreDb;
        private System.Windows.Forms.Panel pnlDangerZone;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Label lblDangerDesc;
        private System.Windows.Forms.Label lblDangerTitle;
    }
}
