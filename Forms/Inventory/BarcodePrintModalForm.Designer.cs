namespace POS
{
    partial class BarcodePrintModalForm
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

        private void InitializeComponent()
        {
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeftSettings = new System.Windows.Forms.Panel();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.lblPresetSize = new System.Windows.Forms.Label();
            this.cmbPresetSize = new System.Windows.Forms.ComboBox();
            this.pnlCustomDimensions = new System.Windows.Forms.Panel();
            this.lblCustomW = new System.Windows.Forms.Label();
            this.numCustomW = new System.Windows.Forms.NumericUpDown();
            this.lblCustomH = new System.Windows.Forms.Label();
            this.numCustomH = new System.Windows.Forms.NumericUpDown();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.chkShowStoreName = new System.Windows.Forms.CheckBox();
            this.chkShowProductName = new System.Windows.Forms.CheckBox();
            this.chkShowPrice = new System.Windows.Forms.CheckBox();
            this.chkShowBarcodeText = new System.Windows.Forms.CheckBox();
            this.lblCopies = new System.Windows.Forms.Label();
            this.numCopies = new System.Windows.Forms.NumericUpDown();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.pnlRightPreview = new System.Windows.Forms.Panel();
            this.lblPreviewTitle = new System.Windows.Forms.Label();
            this.pnlPreviewWrapper = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.pnlBottomBar = new System.Windows.Forms.Panel();
            this.btnPrintDirect = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnExportImage = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTopHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeftSettings.SuspendLayout();
            this.pnlCustomDimensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).BeginInit();
            this.pnlRightPreview.SuspendLayout();
            this.pnlPreviewWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.pnlBottomBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.White;
            this.pnlTopHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlTopHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlTopHeader.Size = new System.Drawing.Size(860, 68);
            this.pnlTopHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(20, 36);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(350, 17);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "تخصيص وطباعة ملصقات الباركود والأسعار على طابعات الملصقات الحرارية أو ورق A4";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(185, 19);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "طباعة باركود المنتجات";
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.pnlMain.Controls.Add(this.pnlLeftSettings, 0, 0);
            this.pnlMain.Controls.Add(this.pnlRightPreview, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 68);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(860, 480);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlLeftSettings
            // 
            this.pnlLeftSettings.AutoScroll = true;
            this.pnlLeftSettings.BackColor = System.Drawing.Color.White;
            this.pnlLeftSettings.Controls.Add(this.lblProduct);
            this.pnlLeftSettings.Controls.Add(this.cmbProducts);
            this.pnlLeftSettings.Controls.Add(this.lblPresetSize);
            this.pnlLeftSettings.Controls.Add(this.cmbPresetSize);
            this.pnlLeftSettings.Controls.Add(this.pnlCustomDimensions);
            this.pnlLeftSettings.Controls.Add(this.lblStoreName);
            this.pnlLeftSettings.Controls.Add(this.txtStoreName);
            this.pnlLeftSettings.Controls.Add(this.chkShowStoreName);
            this.pnlLeftSettings.Controls.Add(this.chkShowProductName);
            this.pnlLeftSettings.Controls.Add(this.chkShowPrice);
            this.pnlLeftSettings.Controls.Add(this.chkShowBarcodeText);
            this.pnlLeftSettings.Controls.Add(this.lblCopies);
            this.pnlLeftSettings.Controls.Add(this.numCopies);
            this.pnlLeftSettings.Controls.Add(this.lblPrinter);
            this.pnlLeftSettings.Controls.Add(this.cmbPrinters);
            this.pnlLeftSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftSettings.Location = new System.Drawing.Point(402, 11);
            this.pnlLeftSettings.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.pnlLeftSettings.Name = "pnlLeftSettings";
            this.pnlLeftSettings.Padding = new System.Windows.Forms.Padding(14);
            this.pnlLeftSettings.Size = new System.Drawing.Size(443, 458);
            this.pnlLeftSettings.TabIndex = 0;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProduct.Location = new System.Drawing.Point(14, 12);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(89, 14);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "الصنف المحدد:";
            // 
            // cmbProducts
            // 
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(14, 30);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(410, 24);
            this.cmbProducts.TabIndex = 1;
            this.cmbProducts.SelectedIndexChanged += new System.EventHandler(this.cmbProducts_SelectedIndexChanged);
            // 
            // lblPresetSize
            // 
            this.lblPresetSize.AutoSize = true;
            this.lblPresetSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPresetSize.Location = new System.Drawing.Point(14, 62);
            this.lblPresetSize.Name = "lblPresetSize";
            this.lblPresetSize.Size = new System.Drawing.Size(127, 14);
            this.lblPresetSize.TabIndex = 2;
            this.lblPresetSize.Text = "مقاس ونوع الملصق:";
            // 
            // cmbPresetSize
            // 
            this.cmbPresetSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresetSize.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbPresetSize.FormattingEnabled = true;
            this.cmbPresetSize.Location = new System.Drawing.Point(14, 80);
            this.cmbPresetSize.Name = "cmbPresetSize";
            this.cmbPresetSize.Size = new System.Drawing.Size(410, 24);
            this.cmbPresetSize.TabIndex = 3;
            this.cmbPresetSize.SelectedIndexChanged += new System.EventHandler(this.cmbPresetSize_SelectedIndexChanged);
            // 
            // pnlCustomDimensions
            // 
            this.pnlCustomDimensions.Controls.Add(this.lblCustomW);
            this.pnlCustomDimensions.Controls.Add(this.numCustomW);
            this.pnlCustomDimensions.Controls.Add(this.lblCustomH);
            this.pnlCustomDimensions.Controls.Add(this.numCustomH);
            this.pnlCustomDimensions.Location = new System.Drawing.Point(14, 110);
            this.pnlCustomDimensions.Name = "pnlCustomDimensions";
            this.pnlCustomDimensions.Size = new System.Drawing.Size(410, 32);
            this.pnlCustomDimensions.TabIndex = 4;
            this.pnlCustomDimensions.Visible = false;
            // 
            // lblCustomW
            // 
            this.lblCustomW.AutoSize = true;
            this.lblCustomW.Location = new System.Drawing.Point(315, 7);
            this.lblCustomW.Name = "lblCustomW";
            this.lblCustomW.Size = new System.Drawing.Size(88, 14);
            this.lblCustomW.TabIndex = 0;
            this.lblCustomW.Text = "العرض (مم):";
            // 
            // numCustomW
            // 
            this.numCustomW.Location = new System.Drawing.Point(220, 5);
            this.numCustomW.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numCustomW.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            this.numCustomW.Name = "numCustomW";
            this.numCustomW.Size = new System.Drawing.Size(85, 22);
            this.numCustomW.TabIndex = 1;
            this.numCustomW.Value = new decimal(new int[] { 38, 0, 0, 0 });
            this.numCustomW.ValueChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // lblCustomH
            // 
            this.lblCustomH.AutoSize = true;
            this.lblCustomH.Location = new System.Drawing.Point(105, 7);
            this.lblCustomH.Name = "lblCustomH";
            this.lblCustomH.Size = new System.Drawing.Size(92, 14);
            this.lblCustomH.TabIndex = 2;
            this.lblCustomH.Text = "الارتفاع (مم):";
            // 
            // numCustomH
            // 
            this.numCustomH.Location = new System.Drawing.Point(10, 5);
            this.numCustomH.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numCustomH.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numCustomH.Name = "numCustomH";
            this.numCustomH.Size = new System.Drawing.Size(85, 22);
            this.numCustomH.TabIndex = 3;
            this.numCustomH.Value = new decimal(new int[] { 25, 0, 0, 0 });
            this.numCustomH.ValueChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStoreName.Location = new System.Drawing.Point(14, 148);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(126, 14);
            this.lblStoreName.TabIndex = 5;
            this.lblStoreName.Text = "اسم المتجر / المحل:";
            // 
            // txtStoreName
            // 
            this.txtStoreName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtStoreName.Location = new System.Drawing.Point(14, 166);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(410, 23);
            this.txtStoreName.TabIndex = 6;
            this.txtStoreName.TextChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // chkShowStoreName
            // 
            this.chkShowStoreName.AutoSize = true;
            this.chkShowStoreName.Checked = true;
            this.chkShowStoreName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStoreName.Location = new System.Drawing.Point(230, 202);
            this.chkShowStoreName.Name = "chkShowStoreName";
            this.chkShowStoreName.Size = new System.Drawing.Size(130, 18);
            this.chkShowStoreName.TabIndex = 7;
            this.chkShowStoreName.Text = "إظهار اسم المتجر";
            this.chkShowStoreName.UseVisualStyleBackColor = true;
            this.chkShowStoreName.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // chkShowProductName
            // 
            this.chkShowProductName.AutoSize = true;
            this.chkShowProductName.Checked = true;
            this.chkShowProductName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowProductName.Location = new System.Drawing.Point(50, 202);
            this.chkShowProductName.Name = "chkShowProductName";
            this.chkShowProductName.Size = new System.Drawing.Size(125, 18);
            this.chkShowProductName.TabIndex = 8;
            this.chkShowProductName.Text = "إظهار اسم المنتج";
            this.chkShowProductName.UseVisualStyleBackColor = true;
            this.chkShowProductName.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // chkShowPrice
            // 
            this.chkShowPrice.AutoSize = true;
            this.chkShowPrice.Checked = true;
            this.chkShowPrice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPrice.Location = new System.Drawing.Point(230, 230);
            this.chkShowPrice.Name = "chkShowPrice";
            this.chkShowPrice.Size = new System.Drawing.Size(134, 18);
            this.chkShowPrice.TabIndex = 9;
            this.chkShowPrice.Text = "إظهار السعر والعملة";
            this.chkShowPrice.UseVisualStyleBackColor = true;
            this.chkShowPrice.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // chkShowBarcodeText
            // 
            this.chkShowBarcodeText.AutoSize = true;
            this.chkShowBarcodeText.Checked = true;
            this.chkShowBarcodeText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBarcodeText.Location = new System.Drawing.Point(50, 230);
            this.chkShowBarcodeText.Name = "chkShowBarcodeText";
            this.chkShowBarcodeText.Size = new System.Drawing.Size(132, 18);
            this.chkShowBarcodeText.TabIndex = 10;
            this.chkShowBarcodeText.Text = "إظهار رقم الباركود";
            this.chkShowBarcodeText.UseVisualStyleBackColor = true;
            this.chkShowBarcodeText.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCopies.Location = new System.Drawing.Point(14, 266);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(155, 14);
            this.lblCopies.TabIndex = 11;
            this.lblCopies.Text = "عدد الملصقات للطباعة:";
            // 
            // numCopies
            // 
            this.numCopies.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.numCopies.Location = new System.Drawing.Point(14, 284);
            this.numCopies.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.numCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCopies.Name = "numCopies";
            this.numCopies.Size = new System.Drawing.Size(410, 23);
            this.numCopies.TabIndex = 12;
            this.numCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrinter.Location = new System.Drawing.Point(14, 318);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(91, 14);
            this.lblPrinter.TabIndex = 13;
            this.lblPrinter.Text = "طابعة الباركود:";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(14, 336);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(410, 24);
            this.cmbPrinters.TabIndex = 14;
            // 
            // pnlRightPreview
            // 
            this.pnlRightPreview.BackColor = System.Drawing.Color.White;
            this.pnlRightPreview.Controls.Add(this.lblPreviewTitle);
            this.pnlRightPreview.Controls.Add(this.pnlPreviewWrapper);
            this.pnlRightPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightPreview.Location = new System.Drawing.Point(15, 11);
            this.pnlRightPreview.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.pnlRightPreview.Name = "pnlRightPreview";
            this.pnlRightPreview.Padding = new System.Windows.Forms.Padding(14);
            this.pnlRightPreview.Size = new System.Drawing.Size(378, 458);
            this.pnlRightPreview.TabIndex = 1;
            // 
            // lblPreviewTitle
            // 
            this.lblPreviewTitle.AutoSize = true;
            this.lblPreviewTitle.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPreviewTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPreviewTitle.Location = new System.Drawing.Point(14, 14);
            this.lblPreviewTitle.Name = "lblPreviewTitle";
            this.lblPreviewTitle.Size = new System.Drawing.Size(161, 16);
            this.lblPreviewTitle.TabIndex = 0;
            this.lblPreviewTitle.Text = "معاينة مباشرة للملصق:";
            // 
            // pnlPreviewWrapper
            // 
            this.pnlPreviewWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreviewWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlPreviewWrapper.Controls.Add(this.pbPreview);
            this.pnlPreviewWrapper.Location = new System.Drawing.Point(14, 40);
            this.pnlPreviewWrapper.Name = "pnlPreviewWrapper";
            this.pnlPreviewWrapper.Padding = new System.Windows.Forms.Padding(10);
            this.pnlPreviewWrapper.Size = new System.Drawing.Size(350, 400);
            this.pnlPreviewWrapper.TabIndex = 1;
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.White;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(10, 10);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(330, 380);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // pnlBottomBar
            // 
            this.pnlBottomBar.BackColor = System.Drawing.Color.White;
            this.pnlBottomBar.Controls.Add(this.btnPrintDirect);
            this.pnlBottomBar.Controls.Add(this.btnPrintPreview);
            this.pnlBottomBar.Controls.Add(this.btnExportImage);
            this.pnlBottomBar.Controls.Add(this.btnClose);
            this.pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomBar.Location = new System.Drawing.Point(0, 548);
            this.pnlBottomBar.Name = "pnlBottomBar";
            this.pnlBottomBar.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlBottomBar.Size = new System.Drawing.Size(860, 56);
            this.pnlBottomBar.TabIndex = 2;
            // 
            // btnPrintDirect
            // 
            this.btnPrintDirect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPrintDirect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintDirect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDirect.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrintDirect.ForeColor = System.Drawing.Color.White;
            this.btnPrintDirect.Location = new System.Drawing.Point(700, 10);
            this.btnPrintDirect.Name = "btnPrintDirect";
            this.btnPrintDirect.Size = new System.Drawing.Size(146, 36);
            this.btnPrintDirect.TabIndex = 0;
            this.btnPrintDirect.Text = "طباعة الآن";
            this.btnPrintDirect.UseVisualStyleBackColor = false;
            this.btnPrintDirect.Click += new System.EventHandler(this.btnPrintDirect_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPrintPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrintPreview.ForeColor = System.Drawing.Color.White;
            this.btnPrintPreview.Location = new System.Drawing.Point(544, 10);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(146, 36);
            this.btnPrintPreview.TabIndex = 1;
            this.btnPrintPreview.Text = "معاينة الطباعة";
            this.btnPrintPreview.UseVisualStyleBackColor = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnExportImage
            // 
            this.btnExportImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnExportImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportImage.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnExportImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnExportImage.Location = new System.Drawing.Point(388, 10);
            this.btnExportImage.Name = "btnExportImage";
            this.btnExportImage.Size = new System.Drawing.Size(146, 36);
            this.btnExportImage.TabIndex = 2;
            this.btnExportImage.Text = "تصدير كصورة PNG";
            this.btnExportImage.UseVisualStyleBackColor = false;
            this.btnExportImage.Click += new System.EventHandler(this.btnExportImage_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnClose.Location = new System.Drawing.Point(14, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "إغلاق";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BarcodePrintModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(860, 604);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottomBar);
            this.Controls.Add(this.pnlTopHeader);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BarcodePrintModalForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "طباعة باركود المنتجات";
            this.Load += new System.EventHandler(this.BarcodePrintModalForm_Load);
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeftSettings.ResumeLayout(false);
            this.pnlLeftSettings.PerformLayout();
            this.pnlCustomDimensions.ResumeLayout(false);
            this.pnlCustomDimensions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).EndInit();
            this.pnlRightPreview.ResumeLayout(false);
            this.pnlRightPreview.PerformLayout();
            this.pnlPreviewWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.pnlBottomBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlLeftSettings;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Label lblPresetSize;
        private System.Windows.Forms.ComboBox cmbPresetSize;
        private System.Windows.Forms.Panel pnlCustomDimensions;
        private System.Windows.Forms.Label lblCustomW;
        private System.Windows.Forms.NumericUpDown numCustomW;
        private System.Windows.Forms.Label lblCustomH;
        private System.Windows.Forms.NumericUpDown numCustomH;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.CheckBox chkShowStoreName;
        private System.Windows.Forms.CheckBox chkShowProductName;
        private System.Windows.Forms.CheckBox chkShowPrice;
        private System.Windows.Forms.CheckBox chkShowBarcodeText;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.NumericUpDown numCopies;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Panel pnlRightPreview;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Panel pnlPreviewWrapper;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.Button btnPrintDirect;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnExportImage;
        private System.Windows.Forms.Button btnClose;
    }
}
