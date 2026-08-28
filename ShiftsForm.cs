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
            UIStyler.StyleSuccessButton(btnClockIn, "🟢 تسجيل حضور");
            UIStyler.StyleDangerButton(btnClockOut, "🔴 تسجيل انصراف");
            UIStyler.StyleSecondaryButton(btnEditShift, "✏️ تعديل");
            UIStyler.StyleDangerButton(btnDeleteShift, "🗑️ حذف");
            UIStyler.StyleSecondaryButton(btnRefresh, "🔄 تحديث");
            UIStyler.StylePrimaryButton(btnApplyFilter, "🔍 فلتر");
            UIStyler.StyleSecondaryButton(btnClearFilter, "✖ مسح الفلتر");
            UIStyler.StyleDataGrid(dgvShifts);
            UIStyler.StyleDataGrid(dgvSummary);

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
            dgvShifts.ColumnHeadersHeight = 48;
            dgvShifts.RowTemplate.Height = 40;
            dgvShifts.EnableHeadersVisualStyles = false;

            if (dgvShifts.Columns["ShiftId"] != null)
            {
                dgvShifts.Columns["ShiftId"].HeaderText = "#";
                dgvShifts.Columns["ShiftId"].FillWeight = 40;
                dgvShifts.Columns["ShiftId"].MinimumWidth = 50;
                dgvShifts.Columns["ShiftId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvShifts.Columns["UserId"] != null)
                dgvShifts.Columns["UserId"].Visible = false;
            if (dgvShifts.Columns["FullName"] != null)
            {
                dgvShifts.Columns["FullName"].HeaderText = "اسم الموظف";
                dgvShifts.Columns["FullName"].FillWeight = 130;
                dgvShifts.Columns["FullName"].MinimumWidth = 130;
            }
            if (dgvShifts.Columns["Username"] != null)
            {
                dgvShifts.Columns["Username"].HeaderText = "المستخدم";
                dgvShifts.Columns["Username"].FillWeight = 80;
                dgvShifts.Columns["Username"].MinimumWidth = 90;
            }
            if (dgvShifts.Columns["ClockInTime"] != null)
            {
                dgvShifts.Columns["ClockInTime"].HeaderText = "وقت الحضور";
                dgvShifts.Columns["ClockInTime"].FillWeight = 120;
                dgvShifts.Columns["ClockInTime"].MinimumWidth = 140;
                dgvShifts.Columns["ClockInTime"].DefaultCellStyle.Format = "yyyy-MM-dd  hh:mm tt";
            }
            if (dgvShifts.Columns["ClockOutTime"] != null)
            {
                dgvShifts.Columns["ClockOutTime"].HeaderText = "وقت الانصراف";
                dgvShifts.Columns["ClockOutTime"].FillWeight = 120;
                dgvShifts.Columns["ClockOutTime"].MinimumWidth = 140;
                dgvShifts.Columns["ClockOutTime"].DefaultCellStyle.Format = "yyyy-MM-dd  hh:mm tt";
            }
            if (dgvShifts.Columns["Duration"] != null)
            {
                dgvShifts.Columns["Duration"].HeaderText = "المدة";
                dgvShifts.Columns["Duration"].FillWeight = 70;
                dgvShifts.Columns["Duration"].MinimumWidth = 90;
                dgvShifts.Columns["Duration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvShifts.Columns["TotalHours"] != null)
            {
                dgvShifts.Columns["TotalHours"].HeaderText = "الساعات";
                dgvShifts.Columns["TotalHours"].FillWeight = 60;
                dgvShifts.Columns["TotalHours"].MinimumWidth = 70;
                dgvShifts.Columns["TotalHours"].DefaultCellStyle.Format = "N2";
                dgvShifts.Columns["TotalHours"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvShifts.Columns["Notes"] != null)
            {
                dgvShifts.Columns["Notes"].HeaderText = "ملاحظات";
                dgvShifts.Columns["Notes"].FillWeight = 100;
                dgvShifts.Columns["Notes"].MinimumWidth = 100;
            }

            // Highlight open shifts in green
            foreach (DataGridViewRow row in dgvShifts.Rows)
            {
                if (row.Cells["ClockOutTime"] != null && row.Cells["ClockOutTime"].Value == DBNull.Value)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 253, 244);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                }
            }
        }

        private void FormatSummaryGrid()
        {
            if (dgvSummary.Columns.Count == 0) return;

            dgvSummary.ScrollBars = ScrollBars.Both;
            dgvSummary.ColumnHeadersHeight = 48;
            dgvSummary.RowTemplate.Height = 40;
            dgvSummary.EnableHeadersVisualStyles = false;

            if (dgvSummary.Columns["UserId"] != null)
                dgvSummary.Columns["UserId"].Visible = false;
            if (dgvSummary.Columns["FullName"] != null)
            {
                dgvSummary.Columns["FullName"].HeaderText = "اسم الموظف";
                dgvSummary.Columns["FullName"].FillWeight = 150;
                dgvSummary.Columns["FullName"].MinimumWidth = 140;
            }
            if (dgvSummary.Columns["TotalShifts"] != null)
            {
                dgvSummary.Columns["TotalShifts"].HeaderText = "عدد الورديات";
                dgvSummary.Columns["TotalShifts"].FillWeight = 80;
                dgvSummary.Columns["TotalShifts"].MinimumWidth = 90;
                dgvSummary.Columns["TotalShifts"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSummary.Columns["TotalHours"] != null)
            {
                dgvSummary.Columns["TotalHours"].HeaderText = "إجمالي الساعات";
                dgvSummary.Columns["TotalHours"].FillWeight = 100;
                dgvSummary.Columns["TotalHours"].MinimumWidth = 100;
                dgvSummary.Columns["TotalHours"].DefaultCellStyle.Format = "N2";
                dgvSummary.Columns["TotalHours"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSummary.Columns["AvgHoursPerShift"] != null)
            {
                dgvSummary.Columns["AvgHoursPerShift"].HeaderText = "متوسط ساعات/وردية";
                dgvSummary.Columns["AvgHoursPerShift"].FillWeight = 100;
                dgvSummary.Columns["AvgHoursPerShift"].MinimumWidth = 110;
                dgvSummary.Columns["AvgHoursPerShift"].DefaultCellStyle.Format = "N2";
                dgvSummary.Columns["AvgHoursPerShift"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSummary.Columns["LastClockIn"] != null)
            {
                dgvSummary.Columns["LastClockIn"].HeaderText = "آخر حضور";
                dgvSummary.Columns["LastClockIn"].FillWeight = 120;
                dgvSummary.Columns["LastClockIn"].MinimumWidth = 140;
                dgvSummary.Columns["LastClockIn"].DefaultCellStyle.Format = "yyyy-MM-dd  hh:mm tt";
            }
        }

        private void UpdateShiftStatus()
        {
            if (_currentUser == null) return;
            var activeShift = DbHelper.GetActiveShift(_currentUser.UserId);
            if (activeShift != null)
            {
                TimeSpan elapsed = DateTime.Now - activeShift.ClockInTime;
                lblShiftStatus.Text = $"⏰ وردية مفتوحة منذ: {activeShift.ClockInTime:hh:mm tt} ({(int)elapsed.TotalHours}:{elapsed.Minutes:D2} ساعة)";
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
            var result = DbHelper.ClockIn(_currentUser.UserId);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "تسجيل حضور", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadShiftsAsync();
                UpdateShiftStatus();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
