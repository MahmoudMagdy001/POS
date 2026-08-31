using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class ShiftsForm : Form
    {
        private readonly UserModel _currentUser;
        private bool _isLoading = false;

        public ShiftsForm(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private async void ShiftsForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            lblTitle.Font = FontManager.GetBold(16f);
            lblSubtitle.Font = FontManager.GetRegular(9f);
            UIStyler.StyleSuccessButton(btnClockIn, "تسجيل حضور");
            UIStyler.StyleDangerButton(btnClockOut, "تسجيل انصراف");
            UIStyler.StyleSecondaryButton(btnEditShift, "تعديل");
            UIStyler.StyleDangerButton(btnDeleteShift, "حذف");
            UIStyler.StyleSecondaryButton(btnRefresh, "تحديث");
            UIStyler.StylePrimaryButton(btnApplyFilter, "فلتر");
            UIStyler.StyleSecondaryButton(btnClearFilter, "مسح الفلتر");
            UIStyler.StyleDataGrid(dgvShifts);
            UIStyler.StyleDataGrid(dgvSummary);
            dgvShifts.CellFormatting += dgvShifts_CellFormatting;

            bool isAdmin = _currentUser != null && (string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير");
            btnEditShift.Visible = isAdmin;
            btnDeleteShift.Visible = isAdmin;

            if (!isAdmin)
            {
                if (tabShifts.TabPages.Contains(tabSummary))
                {
                    tabShifts.TabPages.Remove(tabSummary);
                }
                lblFilterUser.Visible = false;
                cmbUserFilter.Visible = false;
            }

            dtpDateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateTo.Value = DateTime.Now;

            if (isAdmin)
            {
                await LoadUsersFilterAsync();
            }
            await LoadShiftsAsync();
            UpdateShiftStatus();
        }

        public async void RefreshData()
        {
            await LoadShiftsAsync();
            UpdateShiftStatus();
        }

        private async Task LoadUsersFilterAsync()
        {
            try
            {
                DataTable dt = await DbHelper.GetActiveUsersForShiftsAsync();
                DataRow allRow = dt.NewRow();
                allRow["UserId"] = 0;
                allRow["FullName"] = "-- جميع الموظفين --";
                allRow["Username"] = "";
                dt.Rows.InsertAt(allRow, 0);

                cmbUserFilter.DataSource = dt;
                cmbUserFilter.DisplayMember = "FullName";
                cmbUserFilter.ValueMember = "UserId";
                cmbUserFilter.SelectedIndex = 0;
            }
            catch { }
        }

        private async Task LoadShiftsAsync()
        {
            if (_isLoading) return;
            try
            {
                _isLoading = true;

                bool isAdmin = _currentUser != null && (string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) || _currentUser.Role == "مدير");

                int? userIdFilter = null;
                if (!isAdmin && _currentUser != null)
                {
                    userIdFilter = _currentUser.UserId;
                }
                else if (cmbUserFilter.SelectedValue != null && Convert.ToInt32(cmbUserFilter.SelectedValue) > 0)
                {
                    userIdFilter = Convert.ToInt32(cmbUserFilter.SelectedValue);
                }

                var shiftsTask = DbHelper.GetShiftsAsync(userIdFilter, dtpDateFrom.Value, dtpDateTo.Value, txtSearch.Text.Trim());
                Task<DataTable> summaryTask = null;

                if (isAdmin)
                {
                    summaryTask = DbHelper.GetShiftsSummaryAsync(dtpDateFrom.Value, dtpDateTo.Value);
                    await Task.WhenAll(shiftsTask, summaryTask);
                }
                else
                {
                    await shiftsTask;
                }

                dgvShifts.DataSource = await shiftsTask;
                FormatShiftsGrid();

                if (isAdmin && summaryTask != null)
                {
                    dgvSummary.DataSource = await summaryTask;
                    FormatSummaryGrid();
                }

                int totalRecords = dgvShifts.Rows.Count;
                lblTotalRecords.Text = $"إجمالي السجلات: {totalRecords}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل بيانات الورديات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void FormatShiftsGrid()
        {
            if (dgvShifts.Columns.Count == 0) return;

            dgvShifts.ScrollBars = ScrollBars.Both;
            dgvShifts.ColumnHeadersHeight = 44;
            dgvShifts.RowTemplate.Height = 38;
            dgvShifts.EnableHeadersVisualStyles = false;

            dgvShifts.HideColumn("UserId");

            dgvShifts.ConfigureIdColumn("ShiftId", "#", fillWeight: 40, minWidth: 50, format: null);
            dgvShifts.ConfigureTextColumn("FullName", "اسم الموظف", fillWeight: 130, minWidth: 130);
            dgvShifts.ConfigureTextColumn("Username", "المستخدم", fillWeight: 80, minWidth: 90);
            dgvShifts.ConfigureDateColumn("ClockInTime", "وقت الحضور", fillWeight: 120, minWidth: 140, format: "yyyy-MM-dd hh:mm tt");
            dgvShifts.ConfigureDateColumn("ClockOutTime", "وقت الانصراف", fillWeight: 120, minWidth: 140, format: "yyyy-MM-dd hh:mm tt");
            dgvShifts.ConfigureCenterColumn("Duration", "المدة", fillWeight: 70, minWidth: 90);
            dgvShifts.ConfigureNumericColumn("TotalHours", "الساعات", fillWeight: 60, minWidth: 70, format: "N2");
            dgvShifts.ConfigureTextColumn("Notes", "ملاحظات", fillWeight: 100, minWidth: 100);
        }

        private void dgvShifts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvShifts.Rows.Count) return;

            try
            {
                var row = dgvShifts.Rows[e.RowIndex];
                if (row.Cells["ClockOutTime"] != null && (row.Cells["ClockOutTime"].Value == null || row.Cells["ClockOutTime"].Value == DBNull.Value))
                {
                    e.CellStyle.BackColor = Color.FromArgb(240, 253, 244);
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                }
            }
            catch { }
        }

        private void FormatSummaryGrid()
        {
            if (dgvSummary.Columns.Count == 0) return;

            dgvSummary.ScrollBars = ScrollBars.Both;
            dgvSummary.ColumnHeadersHeight = 44;
            dgvSummary.RowTemplate.Height = 38;
            dgvSummary.EnableHeadersVisualStyles = false;

            dgvSummary.HideColumn("UserId");

            dgvSummary.ConfigureTextColumn("FullName", "اسم الموظف", fillWeight: 150, minWidth: 140);
            dgvSummary.ConfigureNumericColumn("TotalShifts", "عدد الورديات", fillWeight: 80, minWidth: 90, format: "N0");
            dgvSummary.ConfigureNumericColumn("TotalHours", "إجمالي الساعات", fillWeight: 100, minWidth: 100, format: "N2");
            dgvSummary.ConfigureNumericColumn("AvgHoursPerShift", "متوسط ساعات/وردية", fillWeight: 100, minWidth: 110, format: "N2");
            dgvSummary.ConfigureDateColumn("LastClockIn", "آخر تسجيل حضور", fillWeight: 120, minWidth: 140, format: "yyyy-MM-dd hh:mm tt");
        }

        private void UpdateShiftStatus()
        {
            if (_currentUser == null) return;
            var activeShift = DbHelper.GetActiveShift(_currentUser.UserId);
            if (activeShift != null)
            {
                TimeSpan elapsed = DateTime.Now - activeShift.ClockInTime;
                lblShiftStatus.Text = $"وردية مفتوحة منذ: {activeShift.ClockInTime:hh:mm tt} ({(int)elapsed.TotalHours}:{elapsed.Minutes:D2} ساعة)";
                lblShiftStatus.ForeColor = Color.FromArgb(22, 101, 52);
                btnClockIn.Enabled = false;
                btnClockOut.Enabled = true;
            }
            else
            {
                lblShiftStatus.Text = "لا توجد وردية مفتوحة حالياً";
                lblShiftStatus.ForeColor = Color.FromArgb(107, 114, 128);
                btnClockIn.Enabled = true;
                btnClockOut.Enabled = false;
            }
        }

        private async void btnClockIn_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;

            using (var startShiftModal = new StartShiftModalForm(_currentUser))
            {
                if (startShiftModal.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadShiftsAsync();
                    UpdateShiftStatus();
                }
            }
        }

        private async void btnClockOut_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;

            var confirmResult = MessageBox.Show(
                "هل تريد تسجيل انصرافك الآن؟",
                "تأكيد تسجيل الانصراف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            var result = DbHelper.ClockOut(_currentUser.UserId);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "تسجيل انصراف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadShiftsAsync();
                UpdateShiftStatus();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnEditShift_Click(object sender, EventArgs e)
        {
            if (dgvShifts.CurrentRow == null || dgvShifts.CurrentRow.Index < 0)
            {
                MessageBox.Show("يرجى تحديد سجل وردية للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int shiftId = Convert.ToInt32(dgvShifts.CurrentRow.Cells["ShiftId"].Value);
            string employeeName = dgvShifts.CurrentRow.Cells["FullName"].Value.ToString();
            DateTime clockIn = Convert.ToDateTime(dgvShifts.CurrentRow.Cells["ClockInTime"].Value);
            DateTime? clockOut = dgvShifts.CurrentRow.Cells["ClockOutTime"].Value != DBNull.Value
                ? (DateTime?)Convert.ToDateTime(dgvShifts.CurrentRow.Cells["ClockOutTime"].Value) : null;
            string notes = dgvShifts.CurrentRow.Cells["Notes"].Value != DBNull.Value
                ? dgvShifts.CurrentRow.Cells["Notes"].Value.ToString() : "";

            using (var editForm = new ShiftEditForm(shiftId, employeeName, clockIn, clockOut, notes))
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadShiftsAsync();
                    UpdateShiftStatus();
                }
            }
        }

        private async void btnDeleteShift_Click(object sender, EventArgs e)
        {
            if (dgvShifts.CurrentRow == null || dgvShifts.CurrentRow.Index < 0)
            {
                MessageBox.Show("يرجى تحديد سجل وردية للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int shiftId = Convert.ToInt32(dgvShifts.CurrentRow.Cells["ShiftId"].Value);
            string employeeName = dgvShifts.CurrentRow.Cells["FullName"].Value.ToString();

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من حذف وردية الموظف '{employeeName}'؟\nهذا الإجراء لا يمكن التراجع عنه.",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var result = DbHelper.DeleteShift(shiftId);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadShiftsAsync();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadShiftsAsync();
            UpdateShiftStatus();
        }

        private async void btnApplyFilter_Click(object sender, EventArgs e)
        {
            await LoadShiftsAsync();
        }

        private async void btnClearFilter_Click(object sender, EventArgs e)
        {
            if (cmbUserFilter.Items.Count > 0)
                cmbUserFilter.SelectedIndex = 0;
            dtpDateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateTo.Value = DateTime.Now;
            txtSearch.Text = "";
            await LoadShiftsAsync();
        }

        private async void tabShifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadShiftsAsync();
        }
    }
}
