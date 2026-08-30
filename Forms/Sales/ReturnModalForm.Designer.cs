namespace POS
{
    partial class ReturnModalForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlSaleInfo = new System.Windows.Forms.Panel();
            this.flpBatchButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReturnAll = new System.Windows.Forms.Button();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.flpSaleDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSaleId = new System.Windows.Forms.Label();
            this.lblSaleDate = new System.Windows.Forms.Label();
            this.lblCashier = new System.Windows.Forms.Label();
            this.lblSaleTotal = new System.Windows.Forms.Label();
            this.dgvReturnItems = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomTop = new System.Windows.Forms.Panel();
            this.pnlRefundBadge = new System.Windows.Forms.Panel();
            this.lblTotalRefundVal = new System.Windows.Forms.Label();
            this.lblTotalRefundTitle = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.lblReturnHint = new System.Windows.Forms.Label();
            this.btnConfirmReturn = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlSaleInfo.SuspendLayout();
            this.flpBatchButtons.SuspendLayout();
            this.flpSaleDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlBottomTop.SuspendLayout();
            this.pnlRefundBadge.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1060, 64);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(18, 34);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(1024, 20);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "حدد كمية الأصناف المراد إرجاعها إلى المخزن مع استرداد قيمتها للعميل";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 10);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(1024, 24);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "عمل إرجاع مبيعات (مرتجع أصناف / فاتورة)";
            // 
            // pnlSaleInfo
            // 
            this.pnlSaleInfo.BackColor = System.Drawing.Color.White;
            this.pnlSaleInfo.Controls.Add(this.flpSaleDetails);
            this.pnlSaleInfo.Controls.Add(this.flpBatchButtons);
            this.pnlSaleInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSaleInfo.Location = new System.Drawing.Point(0, 64);
            this.pnlSaleInfo.Name = "pnlSaleInfo";
            this.pnlSaleInfo.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlSaleInfo.Size = new System.Drawing.Size(1060, 52);
            this.pnlSaleInfo.TabIndex = 1;
            // 
            // flpBatchButtons
            // 
            this.flpBatchButtons.AutoSize = true;
            this.flpBatchButtons.Controls.Add(this.btnReturnAll);
            this.flpBatchButtons.Controls.Add(this.btnResetAll);
            this.flpBatchButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpBatchButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpBatchButtons.Location = new System.Drawing.Point(14, 8);
            this.flpBatchButtons.Name = "flpBatchButtons";
            this.flpBatchButtons.Size = new System.Drawing.Size(260, 36);
            this.flpBatchButtons.TabIndex = 6;
            this.flpBatchButtons.WrapContents = false;
            // 
            // btnReturnAll
            // 
            this.btnReturnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnReturnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturnAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnReturnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnAll.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnReturnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnReturnAll.Location = new System.Drawing.Point(3, 2);
            this.btnReturnAll.Margin = new System.Windows.Forms.Padding(3, 2, 4, 2);
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.Size = new System.Drawing.Size(120, 32);
            this.btnReturnAll.TabIndex = 4;
            this.btnReturnAll.Text = "إرجاع الكل";
            this.btnReturnAll.UseVisualStyleBackColor = false;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // btnResetAll
            // 
            this.btnResetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnResetAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnResetAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetAll.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnResetAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnResetAll.Location = new System.Drawing.Point(130, 2);
            this.btnResetAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(110, 32);
            this.btnResetAll.TabIndex = 5;
            this.btnResetAll.Text = "تصفير الكل";
            this.btnResetAll.UseVisualStyleBackColor = false;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // flpSaleDetails
            // 
            this.flpSaleDetails.AutoScroll = true;
            this.flpSaleDetails.Controls.Add(this.lblSaleId);
            this.flpSaleDetails.Controls.Add(this.lblSaleDate);
            this.flpSaleDetails.Controls.Add(this.lblCashier);
            this.flpSaleDetails.Controls.Add(this.lblSaleTotal);
            this.flpSaleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSaleDetails.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpSaleDetails.Location = new System.Drawing.Point(274, 8);
            this.flpSaleDetails.Name = "flpSaleDetails";
            this.flpSaleDetails.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flpSaleDetails.Size = new System.Drawing.Size(772, 36);
            this.flpSaleDetails.TabIndex = 7;
            this.flpSaleDetails.WrapContents = false;
            // 
            // lblSaleId
            // 
            this.lblSaleId.AutoSize = true;
            this.lblSaleId.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSaleId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSaleId.Location = new System.Drawing.Point(645, 4);
            this.lblSaleId.Margin = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.lblSaleId.Name = "lblSaleId";
            this.lblSaleId.Size = new System.Drawing.Size(123, 19);
            this.lblSaleId.TabIndex = 0;
            this.lblSaleId.Text = "فاتورة: #00000";
            // 
            // lblSaleDate
            // 
            this.lblSaleDate.AutoSize = true;
            this.lblSaleDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSaleDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSaleDate.Location = new System.Drawing.Point(475, 4);
            this.lblSaleDate.Margin = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.lblSaleDate.Name = "lblSaleDate";
            this.lblSaleDate.Size = new System.Drawing.Size(154, 18);
            this.lblSaleDate.TabIndex = 2;
            this.lblSaleDate.Text = "التاريخ: 2026-00-00 00:00";
            // 
            // lblCashier
            // 
            this.lblCashier.AutoSize = true;
            this.lblCashier.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCashier.Location = new System.Drawing.Point(375, 4);
            this.lblCashier.Margin = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(84, 18);
            this.lblCashier.TabIndex = 3;
            this.lblCashier.Text = "الكاشير: -";
            // 
            // lblSaleTotal
            // 
            this.lblSaleTotal.AutoSize = true;
            this.lblSaleTotal.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSaleTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblSaleTotal.Location = new System.Drawing.Point(210, 4);
            this.lblSaleTotal.Margin = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.lblSaleTotal.Name = "lblSaleTotal";
            this.lblSaleTotal.Size = new System.Drawing.Size(149, 19);
            this.lblSaleTotal.TabIndex = 1;
            this.lblSaleTotal.Text = "صافي الفاتورة: 0.00 ج.م";
            // 
            // dgvReturnItems
            // 
            this.dgvReturnItems.AllowUserToAddRows = false;
            this.dgvReturnItems.AllowUserToDeleteRows = false;
            this.dgvReturnItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReturnItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvReturnItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReturnItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReturnItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReturnItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReturnItems.ColumnHeadersHeight = 44;
            this.dgvReturnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReturnItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReturnItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReturnItems.EnableHeadersVisualStyles = false;
            this.dgvReturnItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvReturnItems.Location = new System.Drawing.Point(0, 116);
            this.dgvReturnItems.MultiSelect = false;
            this.dgvReturnItems.Name = "dgvReturnItems";
            this.dgvReturnItems.RowHeadersVisible = false;
            this.dgvReturnItems.RowHeadersWidth = 51;
            this.dgvReturnItems.RowTemplate.Height = 40;
            this.dgvReturnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReturnItems.Size = new System.Drawing.Size(1060, 374);
            this.dgvReturnItems.TabIndex = 2;
            this.dgvReturnItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnItems_CellEndEdit);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.pnlBottomButtons);
            this.pnlBottom.Controls.Add(this.pnlBottomTop);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 490);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlBottom.Size = new System.Drawing.Size(1060, 110);
            this.pnlBottom.TabIndex = 3;
            // 
            // pnlBottomTop
            // 
            this.pnlBottomTop.Controls.Add(this.pnlRefundBadge);
            this.pnlBottomTop.Controls.Add(this.txtReason);
            this.pnlBottomTop.Controls.Add(this.lblReason);
            this.pnlBottomTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottomTop.Location = new System.Drawing.Point(16, 8);
            this.pnlBottomTop.Name = "pnlBottomTop";
            this.pnlBottomTop.Size = new System.Drawing.Size(1028, 42);
            this.pnlBottomTop.TabIndex = 0;
            // 
            // pnlRefundBadge
            // 
            this.pnlRefundBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlRefundBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlRefundBadge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRefundBadge.Controls.Add(this.lblTotalRefundVal);
            this.pnlRefundBadge.Controls.Add(this.lblTotalRefundTitle);
            this.pnlRefundBadge.Location = new System.Drawing.Point(0, 2);
            this.pnlRefundBadge.Name = "pnlRefundBadge";
            this.pnlRefundBadge.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.pnlRefundBadge.Size = new System.Drawing.Size(350, 36);
            this.pnlRefundBadge.TabIndex = 4;
            // 
            // lblTotalRefundVal
            // 
            this.lblTotalRefundVal.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalRefundVal.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTotalRefundVal.Location = new System.Drawing.Point(8, 2);
            this.lblTotalRefundVal.Name = "lblTotalRefundVal";
            this.lblTotalRefundVal.Size = new System.Drawing.Size(160, 30);
            this.lblTotalRefundVal.TabIndex = 3;
            this.lblTotalRefundVal.Text = "0.00 ج.م";
            this.lblTotalRefundVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalRefundTitle
            // 
            this.lblTotalRefundTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalRefundTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTotalRefundTitle.Location = new System.Drawing.Point(170, 2);
            this.lblTotalRefundTitle.Name = "lblTotalRefundTitle";
            this.lblTotalRefundTitle.Size = new System.Drawing.Size(170, 30);
            this.lblTotalRefundTitle.TabIndex = 2;
            this.lblTotalRefundTitle.Text = "إجمالي المبلغ المسترد:";
            this.lblTotalRefundTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReason
            // 
            this.txtReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReason.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtReason.Location = new System.Drawing.Point(370, 6);
            this.txtReason.MaxLength = 200;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(560, 27);
            this.txtReason.TabIndex = 1;
            // 
            // lblReason
            // 
            this.lblReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReason.Location = new System.Drawing.Point(936, 10);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(92, 18);
            this.lblReason.TabIndex = 0;
            this.lblReason.Text = "سبب الإرجاع:";
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Controls.Add(this.lblReturnHint);
            this.pnlBottomButtons.Controls.Add(this.btnConfirmReturn);
            this.pnlBottomButtons.Controls.Add(this.btnCancel);
            this.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomButtons.Location = new System.Drawing.Point(16, 54);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(1028, 48);
            this.pnlBottomButtons.TabIndex = 1;
            // 
            // lblReturnHint
            // 
            this.lblReturnHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReturnHint.AutoSize = true;
            this.lblReturnHint.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblReturnHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReturnHint.Location = new System.Drawing.Point(620, 14);
            this.lblReturnHint.Name = "lblReturnHint";
            this.lblReturnHint.Size = new System.Drawing.Size(405, 18);
            this.lblReturnHint.TabIndex = 6;
            this.lblReturnHint.Text = "ملاحظة: سيتم إعادة الأصناف المرتجعة تلقائياً إلى رصيد المخزن.";
            // 
            // btnConfirmReturn
            // 
            this.btnConfirmReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnConfirmReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmReturn.FlatAppearance.BorderSize = 0;
            this.btnConfirmReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmReturn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmReturn.ForeColor = System.Drawing.Color.White;
            this.btnConfirmReturn.Location = new System.Drawing.Point(125, 4);
            this.btnConfirmReturn.Name = "btnConfirmReturn";
            this.btnConfirmReturn.Size = new System.Drawing.Size(225, 40);
            this.btnConfirmReturn.TabIndex = 4;
            this.btnConfirmReturn.Text = "تأكيد إرجاع الأصناف";
            this.btnConfirmReturn.UseVisualStyleBackColor = false;
            this.btnConfirmReturn.Click += new System.EventHandler(this.btnConfirmReturn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(0, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // ReturnModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1060, 600);
            this.Controls.Add(this.dgvReturnItems);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlSaleInfo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(980, 560);
            this.Name = "ReturnModalForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "عملية إرجاع مبيعات";
            this.Load += new System.EventHandler(this.ReturnModalForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSaleInfo.ResumeLayout(false);
            this.pnlSaleInfo.PerformLayout();
            this.flpBatchButtons.ResumeLayout(false);
            this.flpSaleDetails.ResumeLayout(false);
            this.flpSaleDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottomTop.ResumeLayout(false);
            this.pnlBottomTop.PerformLayout();
            this.pnlRefundBadge.ResumeLayout(false);
            this.pnlBottomButtons.ResumeLayout(false);
            this.pnlBottomButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlSaleInfo;
        private System.Windows.Forms.FlowLayoutPanel flpSaleDetails;
        private System.Windows.Forms.Label lblSaleId;
        private System.Windows.Forms.Label lblSaleTotal;
        private System.Windows.Forms.Label lblSaleDate;
        private System.Windows.Forms.Label lblCashier;
        private System.Windows.Forms.FlowLayoutPanel flpBatchButtons;
        private System.Windows.Forms.Button btnReturnAll;
        private System.Windows.Forms.Button btnResetAll;
        private System.Windows.Forms.DataGridView dgvReturnItems;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomTop;
        private System.Windows.Forms.Panel pnlRefundBadge;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblTotalRefundTitle;
        private System.Windows.Forms.Label lblTotalRefundVal;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Label lblReturnHint;
        private System.Windows.Forms.Button btnConfirmReturn;
        private System.Windows.Forms.Button btnCancel;
    }
}
