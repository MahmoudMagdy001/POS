using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class ShiftEditForm : Form
    {
        private readonly int _shiftId;

        public ShiftEditForm(int shiftId, string employeeName, DateTime clockIn, DateTime? clockOut, string notes)
        {
            InitializeComponent();
            _shiftId = shiftId;
            lblEmployeeVal.Text = employeeName;
            dtpClockIn.Value = clockIn;
            if (clockOut.HasValue)
            {
                dtpClockOut.Checked = true;
                dtpClockOut.Value = clockOut.Value;
            }
            else
            {
                dtpClockOut.Checked = false;
            }
            txtNotes.Text = notes ?? "";
        }

        private void ShiftEditForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblTitle.Font = FontManager.GetBold(14f);
            UIStyler.StylePrimaryButton(btnSave, "💾 حفظ التعديلات");
            UIStyler.StyleSecondaryButton(btnCancel, "إلغاء");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime clockIn = dtpClockIn.Value;
            DateTime? clockOut = dtpClockOut.Checked ? (DateTime?)dtpClockOut.Value : null;
            string notes = txtNotes.Text.Trim();

            var result = DbHelper.UpdateShift(_shiftId, clockIn, clockOut, notes);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
