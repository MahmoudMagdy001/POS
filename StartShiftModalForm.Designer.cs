namespace POS
{
    partial class StartShiftModalForm
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
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblCashierNameVal = new System.Windows.Forms.Label();
            this.lblCashierName = new System.Windows.Forms.Label();
            this.lblStartTimeVal = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblStartingCash = new System.Windows.Forms.Label();
            this.numStartingCash = new System.Windows.Forms.NumericUpDown();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlInfoCard = new System.Windows.Forms.Panel();
            this.lblInfoIcon = new System.Windows.Forms.Label();
            this.lblInfoNotice = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStartShift = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartingCash)).BeginInit();
            this.pnlInfoCard.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.pnlHeader.Size = new System.Drawing.Size(520, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(85, 46);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(410, 18);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "يرجى تأكيد تسجيل الحضور وبدء الوردية للبدء في استخدام الكاشير";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(232, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(263, 27);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "تسجيل بدء وردية عمل";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlInfoCard);
            this.pnlBody.Controls.Add(this.lblCashierName);
            this.pnlBody.Controls.Add(this.lblCashierNameVal);
            this.pnlBody.Controls.Add(this.lblStartTime);
            this.pnlBody.Controls.Add(this.lblStartTimeVal);
            this.pnlBody.Controls.Add(this.lblStartingCash);
            this.pnlBody.Controls.Add(this.numStartingCash);
            this.pnlBody.Controls.Add(this.lblNotes);
            this.pnlBody.Controls.Add(this.txtNotes);
            this.pnlBody.Controls.Add(this.lblStatus);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 80);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24);
            this.pnlBody.Size = new System.Drawing.Size(520, 360);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlInfoCard
            // 
            this.pnlInfoCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlInfoCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoCard.Controls.Add(this.lblInfoIcon);
            this.pnlInfoCard.Controls.Add(this.lblInfoNotice);
            this.pnlInfoCard.Location = new System.Drawing.Point(24, 16);
            this.pnlInfoCard.Name = "pnlInfoCard";
            this.pnlInfoCard.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlInfoCard.Size = new System.Drawing.Size(472, 54);
            this.pnlInfoCard.TabIndex = 0;
            // 
            // lblInfoIcon
            // 
            this.lblInfoIcon.AutoSize = true;
            this.lblInfoIcon.Font = new System.Drawing.Font("Tahoma", 14F);
            this.lblInfoIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblInfoIcon.Location = new System.Drawing.Point(426, 10);
            this.lblInfoIcon.Name = "lblInfoIcon";
            this.lblInfoIcon.Size = new System.Drawing.Size(0, 29);
            this.lblInfoIcon.TabIndex = 0;
            this.lblInfoIcon.Text = "";
            // 
            // lblInfoNotice
            // 
            this.lblInfoNotice.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblInfoNotice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblInfoNotice.Location = new System.Drawing.Point(10, 8);
            this.lblInfoNotice.Name = "lblInfoNotice";
            this.lblInfoNotice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblInfoNotice.Size = new System.Drawing.Size(410, 36);
            this.lblInfoNotice.TabIndex = 1;
            this.lblInfoNotice.Text = "يجب فتح الوردية أولاً لتمكين عمليات البيع والباركود وإصدار الفواتير باسمك.";
            this.lblInfoNotice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCashierName
            // 
            this.lblCashierName.AutoSize = true;
            this.lblCashierName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCashierName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCashierName.Location = new System.Drawing.Point(395, 88);
            this.lblCashierName.Name = "lblCashierName";
            this.lblCashierName.Size = new System.Drawing.Size(104, 18);
            this.lblCashierName.TabIndex = 1;
            this.lblCashierName.Text = "اسم الكاشير:";
            // 
            // lblCashierNameVal
            // 
            this.lblCashierNameVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblCashierNameVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCashierNameVal.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashierNameVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCashierNameVal.Location = new System.Drawing.Point(24, 82);
            this.lblCashierNameVal.Name = "lblCashierNameVal";
            this.lblCashierNameVal.Padding = new System.Windows.Forms.Padding(6);
            this.lblCashierNameVal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCashierNameVal.Size = new System.Drawing.Size(360, 32);
            this.lblCashierNameVal.TabIndex = 2;
            this.lblCashierNameVal.Text = "أحمد محمد علي";
            this.lblCashierNameVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStartTime.Location = new System.Drawing.Point(407, 132);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(92, 18);
            this.lblStartTime.TabIndex = 3;
            this.lblStartTime.Text = "وقت الحضور:";
            // 
            // lblStartTimeVal
            // 
            this.lblStartTimeVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblStartTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStartTimeVal.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblStartTimeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStartTimeVal.Location = new System.Drawing.Point(24, 126);
            this.lblStartTimeVal.Name = "lblStartTimeVal";
            this.lblStartTimeVal.Padding = new System.Windows.Forms.Padding(6);
            this.lblStartTimeVal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStartTimeVal.Size = new System.Drawing.Size(360, 32);
            this.lblStartTimeVal.TabIndex = 4;
            this.lblStartTimeVal.Text = "2026-08-30  10:20 ص";
            this.lblStartTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartingCash
            // 
            this.lblStartingCash.AutoSize = true;
            this.lblStartingCash.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartingCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStartingCash.Location = new System.Drawing.Point(390, 178);
            this.lblStartingCash.Name = "lblStartingCash";
            this.lblStartingCash.Size = new System.Drawing.Size(109, 18);
            this.lblStartingCash.TabIndex = 5;
            this.lblStartingCash.Text = "عهدة الافتتاح:";
            // 
            // numStartingCash
            // 
            this.numStartingCash.DecimalPlaces = 2;
            this.numStartingCash.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.numStartingCash.Location = new System.Drawing.Point(24, 173);
            this.numStartingCash.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numStartingCash.Name = "numStartingCash";
            this.numStartingCash.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numStartingCash.Size = new System.Drawing.Size(360, 30);
            this.numStartingCash.TabIndex = 6;
            this.numStartingCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNotes.Location = new System.Drawing.Point(426, 224);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(73, 18);
            this.lblNotes.TabIndex = 7;
            this.lblNotes.Text = "ملاحظات:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtNotes.Location = new System.Drawing.Point(24, 219);
            this.txtNotes.MaxLength = 300;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNotes.Size = new System.Drawing.Size(360, 60);
            this.txtNotes.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatus.Location = new System.Drawing.Point(24, 290);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStatus.Size = new System.Drawing.Size(472, 22);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnStartShift);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 440);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.pnlFooter.Size = new System.Drawing.Size(520, 66);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(24, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStartShift
            // 
            this.btnStartShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnStartShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartShift.FlatAppearance.BorderSize = 0;
            this.btnStartShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartShift.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartShift.ForeColor = System.Drawing.Color.White;
            this.btnStartShift.Location = new System.Drawing.Point(160, 13);
            this.btnStartShift.Name = "btnStartShift";
            this.btnStartShift.Size = new System.Drawing.Size(336, 38);
            this.btnStartShift.TabIndex = 0;
            this.btnStartShift.Text = "بدء الوردية وتفعيل الكاشير";
            this.btnStartShift.UseVisualStyleBackColor = false;
            this.btnStartShift.Click += new System.EventHandler(this.btnStartShift_Click);
            // 
            // StartShiftModalForm
            // 
            this.AcceptButton = this.btnStartShift;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(520, 506);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartShiftModalForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل بدء وردية العمل";
            this.Load += new System.EventHandler(this.StartShiftModalForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartingCash)).EndInit();
            this.pnlInfoCard.ResumeLayout(false);
            this.pnlInfoCard.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlInfoCard;
        private System.Windows.Forms.Label lblInfoIcon;
        private System.Windows.Forms.Label lblInfoNotice;
        private System.Windows.Forms.Label lblCashierName;
        private System.Windows.Forms.Label lblCashierNameVal;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblStartTimeVal;
        private System.Windows.Forms.Label lblStartingCash;
        private System.Windows.Forms.NumericUpDown numStartingCash;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnStartShift;
    }
}
