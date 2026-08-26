using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            FontManager.ApplyCairoFont(this);
            lblSignInTitle.Font = FontManager.GetBold(18f);
            lblLeftTitle.Font = FontManager.GetBold(18f);
            btnLogin.Font = FontManager.GetBold(11f);
            lblStatus.Text = string.Empty;
            
            // تهيئة جداول قاعدة البيانات والبيانات الأولية باللغة العربية
            await Task.Run(() =>
            {
                DbHelper.InitializeDatabase();
            });
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                SetStatus("يرجى إدخال اسم المستخدم.", isError: true);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                SetStatus("يرجى إدخال كلمة المرور.", isError: true);
                txtPassword.Focus();
                return;
            }

            SetControlsEnabled(false);
            SetStatus("جاري تسجيل الدخول والتحقق، يرجى الانتظار...", isError: false);

            var authResult = await Task.Run(() => DbHelper.Authenticate(username, password));

            SetControlsEnabled(true);

            if (authResult.Success)
            {
                SetStatus("تم تسجيل الدخول بنجاح!", isError: false);

                MainForm mainForm = new MainForm(authResult.User);
                this.Hide();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                SetStatus(authResult.Message, isError: true);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        private void SetStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? Color.FromArgb(220, 38, 38) : Color.FromArgb(22, 163, 74);
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnLogin.Enabled = enabled;
            chkShowPassword.Enabled = enabled;
            btnExit.Enabled = enabled;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
