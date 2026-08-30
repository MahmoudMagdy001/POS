using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace POS
{
    public partial class MainForm : Form
    {
        private static readonly CultureInfo _arCulture = new CultureInfo("ar-EG");

        private readonly UserModel _currentUser;
        private Form _activeChildForm = null;

        private DashboardForm _dashboardForm;
        private POSForm _posForm;
        private SalesForm _salesForm;
        private ProductsForm _productsForm;
        private PurchasesForm _purchasesForm;
        private ShiftsForm _shiftsForm;
        private UsersForm _usersForm;
        private SettingsForm _settingsForm;

        public MainForm(UserModel user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CheckShiftOnStartup();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.StyleDangerButton(btnLogout, "تسجيل خروج");
            lblCurrentTime.AutoSize = true;
            SetupUserInfo();
            InitializeChildForms();

            bool isAdmin = _currentUser != null && (string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير");
            if (isAdmin)
            {
                ShowView("Dashboard");
            }
            else
            {
                ShowView("POS");
            }

            UpdateShiftBadge();
            UpdateClock();
        }

        private void CheckShiftOnStartup()
        {
            if (_currentUser == null) return;

            // استثناء مدير النظام من الإلزام ببدء الوردية عند الدخول
            if (_currentUser.IsAdmin)
            {
                UpdateShiftBadge();
                return;
            }

            var activeShift = DbHelper.GetActiveShift(_currentUser.UserId);
            if (activeShift == null)
            {
                using (var startShiftForm = new StartShiftModalForm(_currentUser))
                {
                    if (startShiftForm.ShowDialog(this) == DialogResult.OK)
                    {
                        _posForm?.RefreshData();
                        _shiftsForm?.RefreshData();
                        UpdateShiftBadge();
                    }
                }
            }
            else
            {
                UpdateShiftBadge();
            }
        }

        public void UpdateShiftBadge()
        {
            if (_currentUser == null || btnTopShift == null) return;

            try
            {
                var activeShift = DbHelper.GetActiveShift(_currentUser.UserId);
                if (activeShift != null)
                {
                    btnTopShift.Text = "الوردية نشطة";
                    btnTopShift.BackColor = Color.FromArgb(240, 253, 244);
                    btnTopShift.ForeColor = Color.FromArgb(22, 101, 52);
                    btnTopShift.FlatAppearance.BorderColor = Color.FromArgb(187, 247, 208);
                }
                else if (_currentUser.IsAdmin)
                {
                    btnTopShift.Text = "وضع المدير";
                    btnTopShift.BackColor = Color.FromArgb(241, 245, 249);
                    btnTopShift.ForeColor = Color.FromArgb(71, 85, 105);
                    btnTopShift.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                }
                else
                {
                    btnTopShift.Text = "بدء الوردية";
                    btnTopShift.BackColor = Color.FromArgb(254, 242, 242);
                    btnTopShift.ForeColor = Color.FromArgb(220, 38, 38);
                    btnTopShift.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
                }
            }
            catch { }
        }

        private void btnTopShift_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;

            var activeShift = DbHelper.GetActiveShift(_currentUser.UserId);
            if (activeShift == null)
            {
                using (var startShiftForm = new StartShiftModalForm(_currentUser))
                {
                    if (startShiftForm.ShowDialog(this) == DialogResult.OK)
                    {
                        _posForm?.RefreshData();
                        _shiftsForm?.RefreshData();
                        UpdateShiftBadge();
                    }
                }
            }
            else
            {
                var confirmResult = MessageBox.Show(
                    $"هل تريد تسجيل انصراف وإنهاء الوردية الحالية للموظف '{_currentUser.FullName}' الآن؟",
                    "تأكيد إنهاء الوردية",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    var res = DbHelper.ClockOut(_currentUser.UserId);
                    if (res.Success)
                    {
                        MessageBox.Show(res.Message, "تسجيل انصراف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateShiftBadge();
                        _posForm?.RefreshData();
                        _shiftsForm?.RefreshData();
                    }
                    else
                    {
                        MessageBox.Show(res.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
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

                UpdateStoreBrand();
            }
        }

        public void UpdateStoreBrand()
        {
            try
            {
                var sysSettings = DbHelper.GetSystemSettings();
                if (sysSettings != null)
                {
                    string storeName = !string.IsNullOrWhiteSpace(sysSettings.StoreName) 
                        ? sysSettings.StoreName 
                        : "كاشير ونقاط بيع";

                    lblAppBrand.Text = storeName;

                    if (lblAppSubtitle != null && !string.IsNullOrWhiteSpace(sysSettings.StoreSubtitle))
                    {
                        lblAppSubtitle.Text = sysSettings.StoreSubtitle;
                    }

                    this.Text = $"{storeName} - إدارة المبيعات ونقاط البيع";
                }
            }
            catch { }
        }

        private void InitializeChildForms()
        {
            _dashboardForm = new DashboardForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _posForm = new POSForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _salesForm = new SalesForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _productsForm = new ProductsForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _purchasesForm = new PurchasesForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            _shiftsForm = new ShiftsForm(_currentUser) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
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
                case "Shifts":
                    targetForm = _shiftsForm;
                    activeButton = btnNavShifts;
                    sectionTitle = "نظام الورديات والحضور والانصراف وحساب ساعات العمل";
                    _shiftsForm?.RefreshData();
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

            UpdateShiftBadge();
        }

        private void ResetNavButtons()
        {
            Button[] buttons = { btnNavDashboard, btnNavPOS, btnNavSales, btnNavProducts, btnNavPurchases, btnNavShifts, btnNavUsers, btnNavSettings };
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
            else if (clicked == btnNavShifts) ShowView("Shifts");
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
                lblCurrentTime.Text = DateTime.Now.ToString("dddd، dd MMMM yyyy  -  hh:mm:ss tt", _arCulture);
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
