using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class StartShiftModalForm : Form
    {
        private readonly UserModel _currentUser;
        private readonly SystemSettingsModel _sysSettings;

        public StartShiftModalForm(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                _sysSettings = DbHelper.GetSystemSettings();
            }
            catch { }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UIStyler.CenterFormOnScreen(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            UIStyler.CenterFormOnScreen(this);
        }

        private void StartShiftModalForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.CenterFormOnScreen(this);

            lblHeaderTitle.Font = FontManager.GetBold(14f);
            lblCashierNameVal.Font = FontManager.GetBold(10.5f);
            lblStartTimeVal.Font = FontManager.GetRegular(10f);

            UIStyler.StyleSuccessButton(btnStartShift, "بدء الوردية وتفعيل الكاشير");
            UIStyler.StyleSecondaryButton(btnCancel, "إلغاء");

            string roleText = _currentUser != null && (string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير")
                ? " (مدير)" : " (كاشير)";
            lblCashierNameVal.Text = (_currentUser?.FullName ?? "كاشير") + roleText;
            lblStartTimeVal.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm tt");
            lblStatus.Text = string.Empty;

            string curr = !string.IsNullOrWhiteSpace(_sysSettings?.CurrencySymbol) ? _sysSettings.CurrencySymbol : "ج.م";
            lblStartingCash.Text = $"عهدة الافتتاح ({curr}):";
            numStartingCash.Value = 0.00m;
            numStartingCash.Focus();
            numStartingCash.Select(0, numStartingCash.Text.Length);
        }

        private void btnStartShift_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                lblStatus.Text = "بيانات المستخدم غير صحيحة.";
                return;
            }

            // تحقق أولاً إذا كان لديه وردية مفتوحة بالفعل
            var existingShift = DbHelper.GetActiveShift(_currentUser.UserId);
            if (existingShift != null)
            {
                MessageBox.Show("لديك وردية عمل مفتوحة بالفعل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            decimal startCash = numStartingCash.Value;
            string userNotes = txtNotes.Text.Trim();
            string curr = !string.IsNullOrWhiteSpace(_sysSettings?.CurrencySymbol) ? _sysSettings.CurrencySymbol : "ج.م";

            string combinedNotes = $"العهدة الافتتاحية: {startCash:N2} {curr}";
            if (!string.IsNullOrWhiteSpace(userNotes))
            {
                combinedNotes += $" | ملاحظات: {userNotes}";
            }

            btnStartShift.Enabled = false;
            btnCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var result = DbHelper.ClockIn(_currentUser.UserId, combinedNotes);
                if (result.Success)
                {
                    MessageBox.Show($"تم بدء وردية العمل بنجاح!\nوقت الحضور: {DateTime.Now:hh:mm tt}\nتم تفعيل شاشة البيع (POS).", 
                                    "بداية الوردية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblStatus.Text = result.Message;
                    MessageBox.Show(result.Message, "تعذر بدء الوردية", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "خطأ: " + ex.Message;
                MessageBox.Show("حدث خطأ أثناء بدء الوردية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnStartShift.Enabled = true;
                btnCancel.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
