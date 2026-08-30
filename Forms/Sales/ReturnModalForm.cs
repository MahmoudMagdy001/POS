using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class ReturnModalForm : Form
    {
        private readonly int _saleId;
        private readonly UserModel _currentUser;
        private SaleModel _sale;
        private List<ReturnItemModel> _items = new List<ReturnItemModel>();

        public ReturnModalForm(int saleId, UserModel currentUser = null)
        {
            _saleId = saleId;
            _currentUser = currentUser;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
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

        private void ReturnModalForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.CenterFormOnScreen(this);
            UIStyler.StyleDangerButton(btnConfirmReturn, "تأكيد عملية الإرجاع");
            UIStyler.StyleSecondaryButton(btnReturnAll, "إرجاع الكل");
            UIStyler.StyleSecondaryButton(btnResetAll, "تصفير الكل");
            UIStyler.StyleSecondaryButton(btnCancel, "إلغاء");
            UIStyler.StyleDataGrid(dgvReturnItems);

            // Enable grid cell editing
            dgvReturnItems.ReadOnly = false;
            dgvReturnItems.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvReturnItems.EditMode = DataGridViewEditMode.EditOnEnter;

            LoadSaleData();
        }

        private void LoadSaleData()
        {
            _sale = DbHelper.GetSaleById(_saleId);
            if (_sale == null)
            {
                MessageBox.Show("تعذر تحميل بيانات الفاتورة المحددة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            lblSaleId.Text = $"فاتورة رقم: #{_sale.SaleId:D5}";
            lblSaleDate.Text = $"التاريخ: {_sale.SaleDate:yyyy-MM-dd HH:mm}";
            lblCashier.Text = $"الكاشير: {_sale.CashierName}";
            lblSaleTotal.Text = $"صافي الفاتورة: {_sale.NetFinalAmount:N2} ج.م";

            _items = DbHelper.GetSaleDetailsForReturn(_saleId);
            SetupGridColumns();
            PopulateGrid();
            CalculateTotalRefund();
        }

        private void SetupGridColumns()
        {
            dgvReturnItems.ScrollBars = ScrollBars.Both;
            dgvReturnItems.Columns.Clear();
            dgvReturnItems.AutoGenerateColumns = false;

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBarcode",
                HeaderText = "الباركود",
                ReadOnly = true,
                FillWeight = 85,
                MinimumWidth = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "اسم الصنف",
                ReadOnly = true,
                FillWeight = 160,
                MinimumWidth = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                HeaderText = "سعر الوحدة",
                ReadOnly = true,
                FillWeight = 70,
                MinimumWidth = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "N2" }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoldQty",
                HeaderText = "المباع",
                ReadOnly = true,
                FillWeight = 55,
                MinimumWidth = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrevReturned",
                HeaderText = "مرتجع سابقاً",
                ReadOnly = true,
                FillWeight = 65,
                MinimumWidth = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = POS.DesignSystem.Tokens.UIColors.TextMuted }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAvailableToReturn",
                HeaderText = "المتاح",
                ReadOnly = true,
                FillWeight = 60,
                MinimumWidth = 65,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = FontManager.GetBold(9.5f), ForeColor = POS.DesignSystem.Tokens.UIColors.Success }
            });

            // Button - (Minus)
            var colBtnDec = new DataGridViewButtonColumn
            {
                Name = "colBtnDec",
                HeaderText = "-",
                Text = "-",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 42,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(9.5f),
                    ForeColor = Color.FromArgb(185, 28, 28)
                }
            };
            dgvReturnItems.Columns.Add(colBtnDec);

            // Editable Return Qty
            var colReturnQty = new DataGridViewTextBoxColumn
            {
                Name = "colReturnQty",
                HeaderText = "كمية الإرجاع",
                ReadOnly = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 85,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(11f),
                    BackColor = Color.FromArgb(254, 243, 199),
                    ForeColor = Color.FromArgb(146, 64, 14),
                    SelectionBackColor = Color.FromArgb(253, 230, 138),
                    SelectionForeColor = Color.FromArgb(146, 64, 14)
                }
            };
            dgvReturnItems.Columns.Add(colReturnQty);

            // Button + (Plus)
            var colBtnInc = new DataGridViewButtonColumn
            {
                Name = "colBtnInc",
                HeaderText = "+",
                Text = "+",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 42,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(9.5f),
                    ForeColor = Color.FromArgb(22, 163, 74)
                }
            };
            dgvReturnItems.Columns.Add(colBtnInc);

            // Button All for this row
            var colBtnAll = new DataGridViewButtonColumn
            {
                Name = "colBtnAll",
                HeaderText = "إرجاع",
                Text = "الكل",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 55,
                Resizable = DataGridViewTriState.False,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(8.5f),
                    ForeColor = Color.FromArgb(30, 64, 175)
                }
            };
            dgvReturnItems.Columns.Add(colBtnAll);

            // Refund Amount
            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRefundAmount",
                HeaderText = "المسترد (ج.م)",
                ReadOnly = true,
                FillWeight = 85,
                MinimumWidth = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(9.5f),
                    ForeColor = POS.DesignSystem.Tokens.UIColors.Danger,
                    Format = "N2"
                }
            });

            // Wire event handlers
            dgvReturnItems.CellContentClick -= DgvReturnItems_CellContentClick;
            dgvReturnItems.CellContentClick += DgvReturnItems_CellContentClick;
            dgvReturnItems.EditingControlShowing -= DgvReturnItems_EditingControlShowing;
            dgvReturnItems.EditingControlShowing += DgvReturnItems_EditingControlShowing;
        }

        private void PopulateGrid()
        {
            dgvReturnItems.Rows.Clear();
            foreach (var item in _items)
            {
                int rowIndex = dgvReturnItems.Rows.Add(
                    item.Barcode,
                    item.ProductName,
                    item.UnitPrice,
                    item.OriginalQuantity,
                    item.AlreadyReturnedQuantity,
                    item.AvailableToReturn,
                    "-",
                    item.ReturnQuantity,
                    "+",
                    "الكل",
                    item.RefundAmount
                );

                dgvReturnItems.Rows[rowIndex].Tag = item;

                // Disable row editing if no available quantity left to return
                if (item.AvailableToReturn <= 0)
                {
                    dgvReturnItems.Rows[rowIndex].Cells["colReturnQty"].ReadOnly = true;
                    dgvReturnItems.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                    dgvReturnItems.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
                }
            }
        }

        private void DgvReturnItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvReturnItems.CurrentCell != null && dgvReturnItems.Columns[dgvReturnItems.CurrentCell.ColumnIndex].Name == "colReturnQty")
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= ReturnQtyTextBox_KeyPress;
                    tb.KeyPress += ReturnQtyTextBox_KeyPress;
                    tb.SelectAll();
                }
            }
        }

        private void ReturnQtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DgvReturnItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvReturnItems.Rows.Count) return;

            var row = dgvReturnItems.Rows[e.RowIndex];
            var item = row.Tag as ReturnItemModel;
            if (item == null || item.AvailableToReturn <= 0) return;

            string colName = dgvReturnItems.Columns[e.ColumnIndex].Name;

            if (colName == "colBtnInc")
            {
                if (item.ReturnQuantity < item.AvailableToReturn)
                {
                    SetItemReturnQuantity(row, item, item.ReturnQuantity + 1);
                }
            }
            else if (colName == "colBtnDec")
            {
                if (item.ReturnQuantity > 0)
                {
                    SetItemReturnQuantity(row, item, item.ReturnQuantity - 1);
                }
            }
            else if (colName == "colBtnAll")
            {
                SetItemReturnQuantity(row, item, item.AvailableToReturn);
            }
        }

        private void SetItemReturnQuantity(DataGridViewRow row, ReturnItemModel item, int newQty)
        {
            if (newQty < 0) newQty = 0;
            if (newQty > item.AvailableToReturn) newQty = item.AvailableToReturn;

            item.ReturnQuantity = newQty;
            row.Cells["colReturnQty"].Value = item.ReturnQuantity;
            row.Cells["colRefundAmount"].Value = item.RefundAmount;

            UpdateRowHighlight(row, item);
            CalculateTotalRefund();
        }

        private void UpdateRowHighlight(DataGridViewRow row, ReturnItemModel item)
        {
            if (item.ReturnQuantity > 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242);
            }
            else if (item.AvailableToReturn <= 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dgvReturnItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvReturnItems.Rows.Count) return;

            var row = dgvReturnItems.Rows[e.RowIndex];
            var item = row.Tag as ReturnItemModel;
            if (item == null) return;

            var cellVal = row.Cells["colReturnQty"].Value;
            int inputQty = 0;
            if (cellVal != null && int.TryParse(cellVal.ToString().Trim(), out int parsed))
            {
                inputQty = parsed;
            }

            if (inputQty < 0) inputQty = 0;
            if (inputQty > item.AvailableToReturn)
            {
                MessageBox.Show($"لا يمكن إرجاع كمية أكبر من المتبقي المتاح ({item.AvailableToReturn}) للصنف: {item.ProductName}", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                inputQty = item.AvailableToReturn;
            }

            SetItemReturnQuantity(row, item, inputQty);
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvReturnItems.Rows)
            {
                var item = row.Tag as ReturnItemModel;
                if (item != null && item.AvailableToReturn > 0)
                {
                    SetItemReturnQuantity(row, item, item.AvailableToReturn);
                }
            }
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvReturnItems.Rows)
            {
                var item = row.Tag as ReturnItemModel;
                if (item != null)
                {
                    SetItemReturnQuantity(row, item, 0);
                }
            }
        }

        private void CalculateTotalRefund()
        {
            decimal total = 0;
            int totalReturnCount = 0;
            foreach (var item in _items)
            {
                if (item.ReturnQuantity > 0)
                {
                    total += item.RefundAmount;
                    totalReturnCount += item.ReturnQuantity;
                }
            }

            lblTotalRefundVal.Text = $"{total:N2} ج.م";
            btnConfirmReturn.Enabled = totalReturnCount > 0;
        }

        private void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            // Commit any current cell edit in progress
            dgvReturnItems.EndEdit();

            var returnItems = _items.FindAll(x => x.ReturnQuantity > 0);
            if (returnItems.Count == 0)
            {
                MessageBox.Show("يرجى تحديد كمية إرجاع لصنف واحد على الأقل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalRefund = 0;
            int totalUnits = 0;
            foreach (var it in returnItems)
            {
                totalRefund += it.RefundAmount;
                totalUnits += it.ReturnQuantity;
            }

            string reason = txtReason.Text.Trim();
            if (string.IsNullOrWhiteSpace(reason))
                reason = "مرتجع من العميل";

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من إرجاع ({totalUnits}) قطعة من الفاتورة رقم #{_sale.SaleId:D5}؟\n\n" +
                $"إجمالي المبلغ المسترد للعميل: {totalRefund:N2} ج.م\n" +
                $"سيتم إعادة الأصناف تلقائياً إلى رصيد المخزن.",
                "تأكيد إرجاع المبيعات",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            var result = DbHelper.ProcessSaleReturnTransaction(_saleId, _currentUser?.UserId, reason, returnItems);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "خطأ في الإرجاع", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

