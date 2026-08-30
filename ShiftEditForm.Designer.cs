namespace POS
{
    partial class ShiftEditForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblEmployeeVal = new System.Windows.Forms.Label();
            this.lblClockIn = new System.Windows.Forms.Label();
            this.dtpClockIn = new System.Windows.Forms.DateTimePicker();
            this.lblClockOut = new System.Windows.Forms.Label();
            this.dtpClockOut = new System.Windows.Forms.DateTimePicker();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(440, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "تعديل بيانات الوردية";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmployee
            // 
            this.lblEmployee.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEmployee.Location = new System.Drawing.Point(350, 60);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(110, 25);
            this.lblEmployee.TabIndex = 1;
            this.lblEmployee.Text = "الموظف:";
            this.lblEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmployeeVal
            // 
            this.lblEmployeeVal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblEmployeeVal.Location = new System.Drawing.Point(30, 60);
            this.lblEmployeeVal.Name = "lblEmployeeVal";
            this.lblEmployeeVal.Size = new System.Drawing.Size(310, 25);
            this.lblEmployeeVal.TabIndex = 2;
            this.lblEmployeeVal.Text = "";
            this.lblEmployeeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClockIn
            // 
            this.lblClockIn.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblClockIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblClockIn.Location = new System.Drawing.Point(350, 100);
            this.lblClockIn.Name = "lblClockIn";
            this.lblClockIn.Size = new System.Drawing.Size(110, 25);
            this.lblClockIn.TabIndex = 3;
            this.lblClockIn.Text = "وقت الحضور:";
            this.lblClockIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpClockIn
            // 
            this.dtpClockIn.CustomFormat = "yyyy-MM-dd  hh:mm tt";
            this.dtpClockIn.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpClockIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpClockIn.Location = new System.Drawing.Point(30, 98);
            this.dtpClockIn.Name = "dtpClockIn";
            this.dtpClockIn.Size = new System.Drawing.Size(310, 27);
            this.dtpClockIn.TabIndex = 4;
            // 
            // lblClockOut
            // 
            this.lblClockOut.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblClockOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblClockOut.Location = new System.Drawing.Point(350, 140);
            this.lblClockOut.Name = "lblClockOut";
            this.lblClockOut.Size = new System.Drawing.Size(110, 25);
            this.lblClockOut.TabIndex = 5;
            this.lblClockOut.Text = "وقت الانصراف:";
            this.lblClockOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpClockOut
            // 
            this.dtpClockOut.CustomFormat = "yyyy-MM-dd  hh:mm tt";
            this.dtpClockOut.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpClockOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpClockOut.Location = new System.Drawing.Point(30, 138);
            this.dtpClockOut.Name = "dtpClockOut";
            this.dtpClockOut.ShowCheckBox = true;
            this.dtpClockOut.Size = new System.Drawing.Size(310, 27);
            this.dtpClockOut.TabIndex = 6;
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNotes.Location = new System.Drawing.Point(350, 180);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(110, 25);
            this.lblNotes.TabIndex = 7;
            this.lblNotes.Text = "ملاحظات:";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtNotes.Location = new System.Drawing.Point(30, 178);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(310, 65);
            this.txtNotes.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(210, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "حفظ التعديلات";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(30, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ShiftEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(480, 315);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.dtpClockOut);
            this.Controls.Add(this.lblClockOut);
            this.Controls.Add(this.dtpClockIn);
            this.Controls.Add(this.lblClockIn);
            this.Controls.Add(this.lblEmployeeVal);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShiftEditForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تعديل سجل الوردية";
            this.Load += new System.EventHandler(this.ShiftEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblEmployeeVal;
        private System.Windows.Forms.Label lblClockIn;
        private System.Windows.Forms.DateTimePicker dtpClockIn;
        private System.Windows.Forms.Label lblClockOut;
        private System.Windows.Forms.DateTimePicker dtpClockOut;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
