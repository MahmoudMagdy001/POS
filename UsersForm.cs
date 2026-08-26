using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class UsersForm : Form
    {
        private readonly UserModel _currentUser;
        private DataTable _usersTable;

        public UsersForm(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblTitle.Font = FontManager.GetBold(16f);
            lblSubtitle.Font = FontManager.GetRegular(9f);
            UIStyler.StylePrimaryButton(btnCreateUser, "➕ إضافة مستخدم جديد");
            UIStyler.StyleSecondaryButton(btnEditUser, "✏️ تعديل");
            UIStyler.StyleSecondaryButton(btnToggleStatus, "🔄 تفعيل / تعطيل");
            UIStyler.StyleDangerButton(btnDeleteUser, "🗑️ حذف");
            UIStyler.StyleSecondaryButton(btnRefresh, "🔄 تحديث");
            UIStyler.StyleDataGrid(dgvUsers);
            LoadUsers();
        }

        public void RefreshData()
        {
            LoadUsers(txtSearch.Text.Trim());
        }

        private void LoadUsers(string searchTerm = "")
        {
            try
            {
                _usersTable = DbHelper.GetAllUsers(searchTerm);
                dgvUsers.DataSource = _usersTable;
                FormatUsersGrid();
                UpdateActionButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل قائمة المستخدمين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatUsersGrid()
        {
            if (dgvUsers.Columns.Count == 0) return;

            dgvUsers.ColumnHeadersHeight = 48;
            dgvUsers.RowTemplate.Height = 40;
            dgvUsers.EnableHeadersVisualStyles = false;

            if (dgvUsers.Columns["UserId"] != null)
                dgvUsers.Columns["UserId"].Visible = false;

            if (dgvUsers.Columns["FullName"] != null)
            {
                dgvUsers.Columns["FullName"].HeaderText = "الاسم الكامل";
                dgvUsers.Columns["FullName"].FillWeight = 150; // 30%
            }
            if (dgvUsers.Columns["Username"] != null)
            {
                dgvUsers.Columns["Username"].HeaderText = "اسم المستخدم";
                dgvUsers.Columns["Username"].FillWeight = 100; // 20%
            }
            if (dgvUsers.Columns["Role"] != null)
            {
                dgvUsers.Columns["Role"].HeaderText = "الدور / الصلاحية";
                dgvUsers.Columns["Role"].FillWeight = 75; // 15%
                dgvUsers.Columns["Role"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvUsers.Columns["IsActive"] != null)
            {
                dgvUsers.Columns["IsActive"].HeaderText = "الحالة";
                dgvUsers.Columns["IsActive"].FillWeight = 40; // ~5-8%
                dgvUsers.Columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvUsers.Columns["CreatedAt"] != null)
            {
                dgvUsers.Columns["CreatedAt"].HeaderText = "تاريخ الإنشاء";
                dgvUsers.Columns["CreatedAt"].FillWeight = 75; // 15%
                dgvUsers.Columns["CreatedAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvUsers.Columns["CreatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvUsers.Columns["LastLogin"] != null)
            {
                dgvUsers.Columns["LastLogin"].HeaderText = "آخر تسجيل دخول";
                dgvUsers.Columns["LastLogin"].FillWeight = 75; // 15%
                dgvUsers.Columns["LastLogin"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvUsers.Columns["LastLogin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            using (UserModalForm modal = new UserModalForm())
            {
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    LoadUsers(txtSearch.Text.Trim());
                }
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            EditSelectedUser();
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedUser();
            }
        }

        private void EditSelectedUser()
        {
            UserModel selectedUser = GetSelectedUser();
            if (selectedUser == null)
            {
                MessageBox.Show("يرجى تحديد مستخدم من القائمة للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (UserModalForm modal = new UserModalForm(selectedUser))
            {
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    LoadUsers(txtSearch.Text.Trim());
                }
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
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
                    LoadUsers(txtSearch.Text.Trim());
                }
                else
                {
                    MessageBox.Show(result.Message, "تعذر التعديل", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
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
                    LoadUsers(txtSearch.Text.Trim());
                }
                else
                {
                    MessageBox.Show(result.Message, "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadUsers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUsers(txtSearch.Text.Trim());
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtonsState();
        }
    }
}
