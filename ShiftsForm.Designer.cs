namespace POS
{
    partial class ShiftsForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClockIn = new System.Windows.Forms.Button();
            this.btnClockOut = new System.Windows.Forms.Button();
            this.btnEditShift = new System.Windows.Forms.Button();
            this.btnDeleteShift = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFilterUser = new System.Windows.Forms.Label();
            this.cmbUserFilter = new System.Windows.Forms.ComboBox();
            this.lblFilterFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFilterTo = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.tabShifts = new System.Windows.Forms.TabControl();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.dgvSummary = new System.Windows.Forms.DataGridView();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.lblShiftStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.tabShifts.SuspendLayout();
            this.tabDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.tabSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).BeginInit();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(20, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(402, 18);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "تسجيل ومتابعة حضور وانصراف الموظفين وحساب ساعات العمل";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(473, 33);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⏰ نظام الورديات والحضور والانصراف";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.btnClockIn);
            this.pnlToolbar.Controls.Add(this.btnClockOut);
            this.pnlToolbar.Controls.Add(this.btnEditShift);
            this.pnlToolbar.Controls.Add(this.btnDeleteShift);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 70);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(1000, 50);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnClockIn
            // 
            this.btnClockIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClockIn.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClockIn.Location = new System.Drawing.Point(19, 11);
            this.btnClockIn.Name = "btnClockIn";
            this.btnClockIn.Size = new System.Drawing.Size(140, 32);
            this.btnClockIn.TabIndex = 0;
            this.btnClockIn.Text = "🟢 تسجيل حضور";
            this.btnClockIn.UseVisualStyleBackColor = true;
            this.btnClockIn.Click += new System.EventHandler(this.btnClockIn_Click);
            // 
            // btnClockOut
            // 
            this.btnClockOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClockOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClockOut.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClockOut.Location = new System.Drawing.Point(165, 11);
            this.btnClockOut.Name = "btnClockOut";
            this.btnClockOut.Size = new System.Drawing.Size(140, 32);
            this.btnClockOut.TabIndex = 1;
            this.btnClockOut.Text = "🔴 تسجيل انصراف";
            this.btnClockOut.UseVisualStyleBackColor = true;
            this.btnClockOut.Click += new System.EventHandler(this.btnClockOut_Click);
            // 
            // btnEditShift
            // 
            this.btnEditShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditShift.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnEditShift.Location = new System.Drawing.Point(311, 11);
            this.btnEditShift.Name = "btnEditShift";
            this.btnEditShift.Size = new System.Drawing.Size(100, 32);
            this.btnEditShift.TabIndex = 2;
            this.btnEditShift.Text = "✏️ تعديل";
            this.btnEditShift.UseVisualStyleBackColor = true;
            this.btnEditShift.Click += new System.EventHandler(this.btnEditShift_Click);
            // 
            // btnDeleteShift
            // 
            this.btnDeleteShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteShift.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDeleteShift.Location = new System.Drawing.Point(417, 11);
            this.btnDeleteShift.Name = "btnDeleteShift";
            this.btnDeleteShift.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteShift.TabIndex = 3;
            this.btnDeleteShift.Text = "🗑️ حذف";
            this.btnDeleteShift.UseVisualStyleBackColor = true;
            this.btnDeleteShift.Click += new System.EventHandler(this.btnDeleteShift_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(523, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 تحديث";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFilters.Controls.Add(this.cmbUserFilter);
            this.pnlFilters.Controls.Add(this.lblFilterUser);
            this.pnlFilters.Controls.Add(this.lblFilterFrom);
            this.pnlFilters.Controls.Add(this.dtpDateFrom);
            this.pnlFilters.Controls.Add(this.lblFilterTo);
            this.pnlFilters.Controls.Add(this.dtpDateTo);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.btnApplyFilter);
            this.pnlFilters.Controls.Add(this.btnClearFilter);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlFilters.Location = new System.Drawing.Point(0, 120);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.pnlFilters.Size = new System.Drawing.Size(1000, 55);
            this.pnlFilters.TabIndex = 2;
            // 
            // lblFilterUser
            // 
            this.lblFilterUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterUser.Location = new System.Drawing.Point(213, 15);
            this.lblFilterUser.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.lblFilterUser.Name = "lblFilterUser";
            this.lblFilterUser.Size = new System.Drawing.Size(93, 28);
            this.lblFilterUser.TabIndex = 0;
            this.lblFilterUser.Text = "الموظف:";
            this.lblFilterUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbUserFilter
            // 
            this.cmbUserFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserFilter.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.cmbUserFilter.Location = new System.Drawing.Point(19, 14);
            this.cmbUserFilter.Margin = new System.Windows.Forms.Padding(6, 2, 3, 0);
            this.cmbUserFilter.Name = "cmbUserFilter";
            this.cmbUserFilter.Size = new System.Drawing.Size(185, 27);
            this.cmbUserFilter.TabIndex = 1;
            // 
            // lblFilterFrom
            // 
            this.lblFilterFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterFrom.Location = new System.Drawing.Point(312, 15);
            this.lblFilterFrom.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.lblFilterFrom.Name = "lblFilterFrom";
            this.lblFilterFrom.Size = new System.Drawing.Size(28, 28);
            this.lblFilterFrom.TabIndex = 2;
            this.lblFilterFrom.Text = "من:";
            this.lblFilterFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(343, 14);
            this.dtpDateFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(120, 26);
            this.dtpDateFrom.TabIndex = 3;
            // 
            // lblFilterTo
            // 
            this.lblFilterTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterTo.Location = new System.Drawing.Point(472, 15);
            this.lblFilterTo.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.lblFilterTo.Name = "lblFilterTo";
            this.lblFilterTo.Size = new System.Drawing.Size(28, 28);
            this.lblFilterTo.TabIndex = 4;
            this.lblFilterTo.Text = "إلى:";
            this.lblFilterTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(503, 14);
            this.dtpDateTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(120, 26);
            this.dtpDateTo.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(632, 14);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6, 2, 6, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 27);
            this.txtSearch.TabIndex = 6;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilter.Location = new System.Drawing.Point(801, 12);
            this.btnApplyFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(80, 30);
            this.btnApplyFilter.TabIndex = 7;
            this.btnApplyFilter.Text = "🔍 فلتر";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearFilter.Location = new System.Drawing.Point(19, 43);
            this.btnClearFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(100, 30);
            this.btnClearFilter.TabIndex = 8;
            this.btnClearFilter.Text = "✖ مسح الفلتر";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // tabShifts
            // 
            this.tabShifts.Controls.Add(this.tabDetails);
            this.tabShifts.Controls.Add(this.tabSummary);
            this.tabShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabShifts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.tabShifts.Location = new System.Drawing.Point(0, 175);
            this.tabShifts.Name = "tabShifts";
            this.tabShifts.RightToLeftLayout = true;
            this.tabShifts.SelectedIndex = 0;
            this.tabShifts.Size = new System.Drawing.Size(1000, 390);
            this.tabShifts.TabIndex = 3;
            this.tabShifts.SelectedIndexChanged += new System.EventHandler(this.tabShifts_SelectedIndexChanged);
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.dgvShifts);
            this.tabDetails.Location = new System.Drawing.Point(4, 30);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Windows.Forms.Padding(8);
            this.tabDetails.Size = new System.Drawing.Size(992, 356);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.Text = "📋 سجل الورديات التفصيلي";
            this.tabDetails.UseVisualStyleBackColor = true;
            // 
            // dgvShifts
            // 
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AllowUserToResizeRows = false;
            this.dgvShifts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShifts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvShifts.ColumnHeadersHeight = 29;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShifts.Location = new System.Drawing.Point(8, 8);
            this.dgvShifts.MultiSelect = false;
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.ReadOnly = true;
            this.dgvShifts.RowHeadersVisible = false;
            this.dgvShifts.RowHeadersWidth = 51;
            this.dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShifts.Size = new System.Drawing.Size(976, 340);
            this.dgvShifts.TabIndex = 0;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.dgvSummary);
            this.tabSummary.Location = new System.Drawing.Point(4, 30);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(8);
            this.tabSummary.Size = new System.Drawing.Size(992, 356);
            this.tabSummary.TabIndex = 1;
            this.tabSummary.Text = "📊 ملخص ساعات العمل";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // dgvSummary
            // 
            this.dgvSummary.AllowUserToAddRows = false;
            this.dgvSummary.AllowUserToDeleteRows = false;
            this.dgvSummary.AllowUserToResizeRows = false;
            this.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgvSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSummary.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSummary.ColumnHeadersHeight = 29;
            this.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSummary.Location = new System.Drawing.Point(8, 8);
            this.dgvSummary.MultiSelect = false;
            this.dgvSummary.Name = "dgvSummary";
            this.dgvSummary.ReadOnly = true;
            this.dgvSummary.RowHeadersVisible = false;
            this.dgvSummary.RowHeadersWidth = 51;
            this.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSummary.Size = new System.Drawing.Size(976, 340);
            this.dgvSummary.TabIndex = 0;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.White;
            this.pnlStatus.Controls.Add(this.lblTotalRecords);
            this.pnlStatus.Controls.Add(this.lblShiftStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 565);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(20, 6, 20, 6);
            this.pnlStatus.Size = new System.Drawing.Size(1000, 35);
            this.pnlStatus.TabIndex = 4;
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalRecords.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTotalRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblTotalRecords.Location = new System.Drawing.Point(20, 6);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(250, 23);
            this.lblTotalRecords.TabIndex = 1;
            this.lblTotalRecords.Text = "إجمالي السجلات: 0";
            this.lblTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShiftStatus
            // 
            this.lblShiftStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShiftStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblShiftStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblShiftStatus.Location = new System.Drawing.Point(500, 6);
            this.lblShiftStatus.Name = "lblShiftStatus";
            this.lblShiftStatus.Size = new System.Drawing.Size(480, 23);
            this.lblShiftStatus.TabIndex = 0;
            this.lblShiftStatus.Text = "لا توجد وردية مفتوحة حالياً";
            this.lblShiftStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ShiftsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabShifts);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "ShiftsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "نظام الورديات والحضور والانصراف";
            this.Load += new System.EventHandler(this.ShiftsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.tabShifts.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.tabSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlToolbar;
        private System.Windows.Forms.Button btnClockIn;
        private System.Windows.Forms.Button btnClockOut;
        private System.Windows.Forms.Button btnEditShift;
        private System.Windows.Forms.Button btnDeleteShift;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel pnlFilters;
        private System.Windows.Forms.Label lblFilterUser;
        private System.Windows.Forms.ComboBox cmbUserFilter;
        private System.Windows.Forms.Label lblFilterFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblFilterTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.TabControl tabShifts;
        private System.Windows.Forms.TabPage tabDetails;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblShiftStatus;
        private System.Windows.Forms.Label lblTotalRecords;
    }
}
