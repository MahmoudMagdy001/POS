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
        }

        private void ReturnModalForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.StyleDangerButton(btnConfirmReturn, "↩️ تأكيد عملية الإرجاع");
            UIStyler.StyleSecondaryButton(btnReturnAll, "إرجاع الكل");
            UIStyler.StyleSecondaryButton(btnCancel, "إلغاء");
            UIStyler.StyleDataGrid(dgvReturnItems);
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
            dgvReturnItems.Columns.Clear();
            dgvReturnItems.AutoGenerateColumns = false;

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBarcode",
                HeaderText = "الباركود",
                ReadOnly = true,
                FillWeight = 85
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "اسم الصنف",
                ReadOnly = true,
                FillWeight = 160
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                HeaderText = "سعر الوحدة",
                ReadOnly = true,
                FillWeight = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoldQty",
                HeaderText = "الكمية المباعة",
                ReadOnly = true,
                FillWeight = 65,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrevReturned",
                HeaderText = "مرتجع سابقاً",
                ReadOnly = true,
                FillWeight = 65,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(100, 116, 139) }
            });

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAvailableToReturn",
                HeaderText = "المتاح للإرجاع",
                ReadOnly = true,
                FillWeight = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = FontManager.GetBold(9.5f), ForeColor = Color.FromArgb(22, 163, 74) }
            });

            // Editable Return Qty
            var colReturnQty = new DataGridViewTextBoxColumn
            {
                Name = "colReturnQty",
                HeaderText = "كمية الإرجاع",
                ReadOnly = false,
                FillWeight = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = FontManager.GetBold(10f),
                    BackColor = Color.FromArgb(254, 249, 195), // Light Yellow Highlight
                    ForeColor = Color.FromArgb(161, 98, 7),
                    SelectionBackColor = Color.FromArgb(253, 230, 138),
                    SelectionForeColor = Color.FromArgb(113, 63, 18)
                }
            };
            dgvReturnItems.Columns.Add(colReturnQty);

            dgvReturnItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRefundAmount",
                HeaderText = "المسترد (ج.م)",
                ReadOnly = true,
                FillWeight = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = FontManager.GetBold(9.5f),
                    ForeColor = Color.FromArgb(220, 38, 38),
                    Format = "N2"
                }
            });
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
                    item.ReturnQuantity,
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

            item.ReturnQuantity = inputQty;
            row.Cells["colReturnQty"].Value = inputQty;
            row.Cells["colRefundAmount"].Value = item.RefundAmount;

            CalculateTotalRefund();
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvReturnItems.Rows)
            {
                var item = row.Tag as ReturnItemModel;
                if (item != null && item.AvailableToReturn > 0)
                {
                    item.ReturnQuantity = item.AvailableToReturn;
                    row.Cells["colReturnQty"].Value = item.ReturnQuantity;
                    row.Cells["colRefundAmount"].Value = item.RefundAmount;
                }
            }
            CalculateTotalRefund();
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
