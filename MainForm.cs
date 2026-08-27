using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace POS
{
    public partial class MainForm : Form
    {
        private readonly UserModel _currentUser;
        private Form _activeChildForm = null;

        private DashboardForm _dashboardForm;
        private POSForm _posForm;
        private SalesForm _salesForm;
        private ProductsForm _productsForm;
        private PurchasesForm _purchasesForm;
        private UsersForm _usersForm;
        private SettingsForm _settingsForm;

        public MainForm(UserModel user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.StyleDangerButton(btnLogout, "🚪 خروج");
            SetupUserInfo();
            InitializeChildForms();
            ShowView("Dashboard");
            UpdateClock();
        }

        private void SetupUserInfo()
        {
            if (_currentUser != null)
            {
                lblUserName.Text = _currentUser.FullName;
                bool isAdmin = string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير";
                lblUserRole.Text = isAdmin ? "مدير النظام" : "كاشير الصالة";

                string initials = "م";
                if (!string.IsNullOrWhiteSpace(_currentUser.FullName))
                {
                    string[] parts = _currentUser.FullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 1 && parts[0].Length >= 1)
                        initials = parts[0][0].ToString();
                }
                lblUserAvatar.Text = initials;

                btnNavUsers.Visible = isAdmin;
                btnNavSettings.Visible = isAdmin;

                try
                {
                    var sysSettings = DbHelper.GetSystemSettings();
                    if (!string.IsNullOrWhiteSpace(sysSettings.StoreName))
                        lblAppBrand.Text = "🛒 " + sysSettings.StoreName;
                    if (!string.IsNullOrWhiteSpace(sysSettings.StoreSubtitle))
                        lblAppSubtitle.Text = sysSettings.StoreSubtitle;
                }
                catch { }
            }
        }

        private void InitializeChildForms()
        {
            _dashboardForm = new DashboardForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _posForm = new POSForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _salesForm = new SalesForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _productsForm = new ProductsForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _purchasesForm = new PurchasesForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _usersForm = new UsersForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _settingsForm = new SettingsForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
        }

        private void ShowView(string viewName)
        {
            Form targetForm = null;
            Button activeButton = null;
            string sectionTitle = "";
            bool isAdmin = _currentUser != null && (string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير");

            switch (viewName)
            {
                case "Dashboard":
                    targetForm = _dashboardForm;
                    activeButton = btnNavDashboard;
                    sectionTitle = "لوحة التحكم العامة ومتابعة الأداء";
                    _dashboardForm?.RefreshData();
                    break;
                case "POS":
                    targetForm = _posForm;
                    activeButton = btnNavPOS;
                    sectionTitle = "نقطة البيع - شاشة الكاشير السريع (POS)";
                    _posForm?.RefreshData();
                    break;
                case "Sales":
                    targetForm = _salesForm;
                    activeButton = btnNavSales;
                    sectionTitle = "سجل فواتير وتقارير المبيعات العامة";
                    _salesForm?.RefreshData();
                    break;
                case "Products":
                    targetForm = _productsForm;
                    activeButton = btnNavProducts;
                    sectionTitle = "إدارة المنتجات والمخزون والتسعير";
                    _productsForm?.RefreshData();
                    break;
                case "Purchases":
                    targetForm = _purchasesForm;
                    activeButton = btnNavPurchases;
                    sectionTitle = "إدارة فواتير المشتريات والموردين";
                    _purchasesForm?.RefreshData();
                    break;
                case "Users":
                    if (!isAdmin)
                    {
                        MessageBox.Show("عذراً، هذه الشاشة مخصصة لمدير النظام فقط.", "صلاحية مقيدة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    targetForm = _usersForm;
                    activeButton = btnNavUsers;
                    sectionTitle = "إدارة المستخدمين وصلاحيات الموظفين";
                    _usersForm?.RefreshData();
                    break;
                case "Settings":
                    if (!isAdmin)
                    {
                        MessageBox.Show("عذراً، هذه الشاشة مخصصة لمدير النظام فقط.", "صلاحية مقيدة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    targetForm = _settingsForm;
                    activeButton = btnNavSettings;
                    sectionTitle = "إعدادات النظام العامة والتحكم بالصلاحيات والنسخ الاحتياطي";
                    _settingsForm?.RefreshData();
                    break;
            }

            if (targetForm == null) return;

            ResetNavButtons();
            if (activeButton != null)
            {
                activeButton.BackColor = POS.DesignSystem.Tokens.UIColors.SidebarActiveItem;
                activeButton.ForeColor = POS.DesignSystem.Tokens.UIColors.SidebarActiveText;
            }

            lblCurrentSectionTitle.Text = sectionTitle;

            if (_activeChildForm != targetForm)
            {
                pnlMainContent.SuspendLayout();
                pnlMainContent.Controls.Clear();
                pnlMainContent.Controls.Add(targetForm);
                targetForm.Show();
                _activeChildForm = targetForm;
                pnlMainContent.ResumeLayout();
            }
        }

        private void ResetNavButtons()
        {
            Button[] buttons = { btnNavDashboard, btnNavPOS, btnNavSales, btnNavProducts, btnNavPurchases, btnNavUsers, btnNavSettings };
            foreach (var b in buttons)
            {
                b.BackColor = Color.Transparent;
                b.ForeColor = POS.DesignSystem.Tokens.UIColors.SidebarText;
            }
        }

        private void btnNav_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked == null) return;

            if (clicked == btnNavDashboard) ShowView("Dashboard");
            else if (clicked == btnNavPOS) ShowView("POS");
            else if (clicked == btnNavSales) ShowView("Sales");
            else if (clicked == btnNavProducts) ShowView("Products");
            else if (clicked == btnNavPurchases) ShowView("Purchases");
            else if (clicked == btnNavUsers) ShowView("Users");
            else if (clicked == btnNavSettings) ShowView("Settings");
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            try
            {
                var arCulture = new CultureInfo("ar-EG");
                lblCurrentTime.Text = DateTime.Now.ToString("dddd، dd MMMM yyyy  -  hh:mm:ss tt", arCulture);
            }
            catch
            {
                lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "هل أنت متأكد من رغبتك في تسجيل الخروج من النظام؟",
                "تأكيد تسجيل الخروج",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
