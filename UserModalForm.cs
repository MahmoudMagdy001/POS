using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class UserModalForm : Form
    {
        private readonly UserModel _existingUser = null;
        private readonly bool _isEditMode = false;

        public UserModalForm()
        {
            InitializeComponent();
            _isEditMode = false;
            SetupForm();
        }

        public UserModalForm(UserModel userToEdit)
        {
            InitializeComponent();
            _existingUser = userToEdit;
            _isEditMode = true;
            SetupForm();
        }

        private void SetupForm()
        {
            UIStyler.ApplyTheme(this);
            lblHeaderTitle.Font = FontManager.GetBold(14f);
            UIStyler.StylePrimaryButton(btnSave);
            UIStyler.StyleSecondaryButton(btnCancel, "إلغاء");
            lblStatus.Text = string.Empty;

            cboRole.Items.Clear();
            cboRole.Items.Add(new ComboBoxItem("Admin", "مدير النظام (Admin)"));
            cboRole.Items.Add(new ComboBoxItem("Cashier", "كاشير الصالة (Cashier)"));

            if (_isEditMode && _existingUser != null)
            {
                lblHeaderTitle.Text = "تعديل بيانات المستخدم";
                lblHeaderSubtitle.Text = $"تعديل بيانات الحساب: {_existingUser.Username}";
                this.Text = "تعديل مستخدم";

                txtUsername.Text = _existingUser.Username;
                txtUsername.Enabled = false; // لا يمكن تغيير اسم المستخدم بعد إنشائه
                txtFullName.Text = _existingUser.FullName;

                // اختيار الدور
                int selectIdx = string.Equals(_existingUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                cboRole.SelectedIndex = selectIdx;

                chkIsActive.Checked = _existingUser.IsActive;

                lblPassword.Text = "كلمة المرور الجديدة (اختياري):";
                lblConfirmPassword.Text = "تأكيد كلمة المرور الجديدة:";
                btnSave.Text = "💾 حفظ التعديلات";
            }
            else
            {
                lblHeaderTitle.Text = "إضافة مستخدم جديد";
                lblHeaderSubtitle.Text = "يرجى تعبئة بيانات الحساب والصلاحيات المطلوبة";
                this.Text = "مستخدم جديد";

                cboRole.SelectedIndex = 1; // Cashier افتراضياً
                chkIsActive.Checked = true;
                btnSave.Text = "➕ إنشاء المستخدم";
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            bool isActive = chkIsActive.Checked;

            var selectedRoleItem = cboRole.SelectedItem as ComboBoxItem;
            string role = selectedRoleItem?.Value ?? "Cashier";

            if (!_isEditMode)
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    SetStatus("اسم المستخدم مطلوب.", isError: true);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    SetStatus("كلمة المرور مطلوبة للمستخدم الجديد.", isError: true);
                    txtPassword.Focus();
                    return;
                }

                if (password.Length < 4)
                {
                    SetStatus("كلمة المرور يجب أن لا تقل عن 4 خانات.", isError: true);
                    txtPassword.Focus();
                    return;
                }

                if (password != confirmPassword)
                {
                    SetStatus("كلمتا المرور غير متطابقتين.", isError: true);
                    txtConfirmPassword.Focus();
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (password.Length < 4)
                    {
                        SetStatus("كلمة المرور يجب أن لا تقل عن 4 خانات.", isError: true);
                        txtPassword.Focus();
                        return;
                    }

                    if (password != confirmPassword)
                    {
                        SetStatus("كلمتا المرور غير متطابقتين.", isError: true);
                        txtConfirmPassword.Focus();
                        return;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                SetStatus("الاسم الكامل مطلوب.", isError: true);
                txtFullName.Focus();
                return;
            }

            if (_isEditMode)
            {
                string newPass = string.IsNullOrWhiteSpace(password) ? null : password;
                var res = DbHelper.UpdateUser(_existingUser.UserId, fullName, role, isActive, newPass);
                if (res.Success)
                {
                    MessageBox.Show(res.Message, "تم التعديل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    SetStatus(res.Message, isError: true);
                }
            }
            else
            {
                var res = DbHelper.CreateUser(username, password, fullName, role, isActive);
                if (res.Success)
                {
                    MessageBox.Show(res.Message, "تم الإنشاء بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    SetStatus(res.Message, isError: true);
                }
            }
        }

        private void SetStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? POS.DesignSystem.Tokens.UIColors.Danger : POS.DesignSystem.Tokens.UIColors.Success;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class ComboBoxItem
        {
            public string Value { get; set; }
            public string Text { get; set; }

            public ComboBoxItem(string value, string text)
            {
                Value = value;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
