using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class UsersForm : Form
    {
        private readonly UserModel _currentUser;
        private DataTable _usersTable;
        private Timer _searchDebounceTimer;
        private bool _isLoading = false;

        public UsersForm(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            _searchDebounceTimer = new Timer();
            _searchDebounceTimer.Interval = 250;
            _searchDebounceTimer.Tick += OnSearchDebounceTick;
        }

        private async void UsersForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblTitle.Font = FontManager.GetBold(16f);
            lblSubtitle.Font = FontManager.GetRegular(9f);
            UIStyler.StylePrimaryButton(btnCreateUser, "إضافة مستخدم جديد");
            UIStyler.StyleSecondaryButton(btnEditUser, "تعديل");
            UIStyler.StyleSecondaryButton(btnToggleStatus, "تفعيل / تعطيل");
            UIStyler.StyleDangerButton(btnDeleteUser, "حذف");
            UIStyler.StyleSecondaryButton(btnRefresh, "تحديث");
            UIStyler.StyleDataGrid(dgvUsers);

            await LoadUsersAsync();
        }

        public async void RefreshData()
        {
            await LoadUsersAsync(txtSearch.Text.Trim());
        }

        private async Task LoadUsersAsync(string searchTerm = "")
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                _usersTable = await DbHelper.GetAllUsersAsync(searchTerm);
                dgvUsers.DataSource = _usersTable;
                FormatUsersGrid();
                UpdateActionButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل قائمة المستخدمين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void FormatUsersGrid()
        {
            if (dgvUsers.Columns.Count == 0) return;

            dgvUsers.ScrollBars = ScrollBars.Both;
            dgvUsers.ColumnHeadersHeight = 44;
            dgvUsers.RowTemplate.Height = 38;
            dgvUsers.EnableHeadersVisualStyles = false;

            dgvUsers.HideColumn("UserId");

            dgvUsers.ConfigureTextColumn("FullName", "الاسم الكامل", fillWeight: 150, minWidth: 140);
            dgvUsers.ConfigureTextColumn("Username", "اسم المستخدم", fillWeight: 100, minWidth: 110);
            dgvUsers.ConfigureCenterColumn("Role", "الدور / الصلاحية", fillWeight: 75, minWidth: 100);
            dgvUsers.ConfigureCenterColumn("IsActive", "الحالة", fillWeight: 50, minWidth: 70);
            dgvUsers.ConfigureDateColumn("CreatedAt", "تاريخ الإنشاء", fillWeight: 75, minWidth: 125);
            dgvUsers.ConfigureDateColumn("LastLogin", "آخر تسجيل دخول", fillWeight: 75, minWidth: 125);
        }

        private void UpdateActionButtonsState()
        {
            bool hasSelection = dgvUsers.SelectedRows.Count > 0;
            btnEditUser.Enabled = hasSelection;
            btnToggleStatus.Enabled = hasSelection;
            btnDeleteUser.Enabled = hasSelection;
        }

        private UserModel GetSelectedUser()
        {
            if (dgvUsers.SelectedRows.Count == 0) return null;

            DataGridViewRow row = dgvUsers.SelectedRows[0];
            return new UserModel
            {
                UserId = Convert.ToInt32(row.Cells["UserId"].Value),
                Username = row.Cells["Username"].Value?.ToString(),
                FullName = row.Cells["FullName"].Value?.ToString(),
                Role = row.Cells["Role"].Value?.ToString(),
                IsActive = Convert.ToBoolean(row.Cells["IsActive"].Value)
            };
        }

        private async void btnCreateUser_Click(object sender, EventArgs e)
        {
            using (UserModalForm modal = new UserModalForm())
            {
                modal.StartPosition = FormStartPosition.CenterScreen;
                if (modal.ShowDialog(this.FindForm() ?? this) == DialogResult.OK)
                {
                    await LoadUsersAsync(txtSearch.Text.Trim());
                }
            }
        }

        private async void btnEditUser_Click(object sender, EventArgs e)
        {
            await EditSelectedUserAsync();
        }

        private async void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                await EditSelectedUserAsync();
            }
        }

        private async Task EditSelectedUserAsync()
        {
            UserModel selectedUser = GetSelectedUser();
            if (selectedUser == null)
            {
                MessageBox.Show("يرجى تحديد مستخدم من القائمة للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (UserModalForm modal = new UserModalForm(selectedUser))
            {
                modal.StartPosition = FormStartPosition.CenterScreen;
                if (modal.ShowDialog(this.FindForm() ?? this) == DialogResult.OK)
                {
                    await LoadUsersAsync(txtSearch.Text.Trim());
                }
            }
        }

        private async void btnToggleStatus_Click(object sender, EventArgs e)
        {
            UserModel selectedUser = GetSelectedUser();
            if (selectedUser == null) return;

            bool newStatus = !selectedUser.IsActive;
            string actionText = newStatus ? "تنشيط" : "تعطيل";

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من رغبتك في {actionText} حساب المستخدم '{selectedUser.Username}' ({selectedUser.FullName})؟",
                "تأكيد تغيير الحالة",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = DbHelper.ToggleUserActive(selectedUser.UserId, _currentUser?.UserId ?? 0, newStatus);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "تم التحديث", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync(txtSearch.Text.Trim());
                }
                else
                {
                    MessageBox.Show(result.Message, "تعذر التعديل", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnDeleteUser_Click(object sender, EventArgs e)
        {
            UserModel selectedUser = GetSelectedUser();
            if (selectedUser == null) return;

            if (_currentUser != null && selectedUser.UserId == _currentUser.UserId)
            {
                MessageBox.Show("لا يمكنك حذف حسابك الحالي المسجل به الدخول.", "إجراء غير مسموح", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من رغبتك في حذف المستخدم '{selectedUser.Username}' نهائياً؟\nلا يمكن التراجع عن هذا الإجراء.",
                "تأكيد الحذف النهائي",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                var result = DbHelper.DeleteUser(selectedUser.UserId, _currentUser?.UserId ?? 0);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync(txtSearch.Text.Trim());
                }
                else
                {
                    MessageBox.Show(result.Message, "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadUsersAsync();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void OnSearchDebounceTick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            await LoadUsersAsync(txtSearch.Text.Trim());
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtonsState();
        }
    }
}
