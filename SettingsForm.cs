using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POS
{
    public partial class SettingsForm : Form
    {
        private readonly UserModel _currentUser;
        private SystemSettingsModel _currentSettings;

        public SettingsForm(UserModel currentUser = null)
        {
            _currentUser = currentUser;
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UIStyler.ApplyTheme(this);
            UIStyler.StylePrimaryButton(btnSave, "💾 حفظ وتطبيق الإعدادات");
            UIStyler.StyleSecondaryButton(btnResetDefaults, "🔄 استعادة الافتراضي");
            UIStyler.StyleSecondaryButton(btnTestReceipt, "🖨️ فحص طباعة إيصال");
            UIStyler.StyleSecondaryButton(btnBackupDb, "📦 أخذ نسخة احتياطية الآن");
            UIStyler.StyleSecondaryButton(btnRestoreDb, "📥 استرجاع نسخة سابقة");
            UIStyler.StyleDangerButton(btnClearHistory, "⚠️ تفريغ كافة سجلات المبيعات والمشتريات");
            VerifyAdminAccess();
            LoadSettings();
        }

        private bool IsAdminUser()
        {
            if (_currentUser == null) return false;
            return string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase) ||
                   _currentUser.Role == "مدير";
        }

        private void VerifyAdminAccess()
        {
            if (!IsAdminUser())
            {
                pnlMain.Enabled = false;
                flpActions.Enabled = false;
                lblStatus.ForeColor = POS.DesignSystem.Tokens.UIColors.Danger;
                lblStatus.Text = "⛔ وصول مقيد: هذه الشاشة مخصصة فقط لمدير النظام.";
                MessageBox.Show(
                    "عذراً، هذه الشاشة مخصصة لمدير النظام فقط. لا تملك الصلاحية الكافية لتعديل إعدادات النظام.",
                    "تنبيه أمني",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        public void RefreshData()
        {
            if (IsAdminUser())
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            try
            {
                _currentSettings = DbHelper.GetSystemSettings();

                txtStoreName.Text = _currentSettings.StoreName;
                txtStoreSubtitle.Text = _currentSettings.StoreSubtitle;
                txtStorePhone.Text = _currentSettings.StorePhone;
                txtStoreAddress.Text = _currentSettings.StoreAddress;
                txtTaxNumber.Text = _currentSettings.TaxNumber;

                txtReceiptHeader.Text = _currentSettings.ReceiptHeader;
                txtReceiptFooter.Text = _currentSettings.ReceiptFooter;
                txtCurrencySymbol.Text = _currentSettings.CurrencySymbol;
                nudVatRate.Value = Math.Max(0, Math.Min(100, _currentSettings.VatRate));
                chkEnablePrintPreview.Checked = _currentSettings.EnablePrintPreview;
                chkAutoPrintOnSale.Checked = _currentSettings.AutoPrintOnSale;

                nudDefaultMinStock.Value = Math.Max(1, _currentSettings.DefaultMinStock);
                chkAllowNegativeStock.Checked = _currentSettings.AllowNegativeStock;

                lblStatus.Text = "";
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = POS.DesignSystem.Tokens.UIColors.Danger;
                lblStatus.Text = "فشل تحميل الإعدادات: " + ex.Message;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsAdminUser())
            {
                VerifyAdminAccess();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                MessageBox.Show("يرجى إدخال اسم المتجر / المنشأة.", "حقل مطلوب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabSettings.SelectedTab = tabStoreInfo;
                txtStoreName.Focus();
                return;
            }

            try
            {
                var newSettings = new SystemSettingsModel
                {
                    StoreName = txtStoreName.Text.Trim(),
                    StoreSubtitle = txtStoreSubtitle.Text.Trim(),
                    StorePhone = txtStorePhone.Text.Trim(),
                    StoreAddress = txtStoreAddress.Text.Trim(),
                    TaxNumber = txtTaxNumber.Text.Trim(),
                    ReceiptHeader = txtReceiptHeader.Text.Trim(),
                    ReceiptFooter = txtReceiptFooter.Text.Trim(),
                    CurrencySymbol = string.IsNullOrWhiteSpace(txtCurrencySymbol.Text) ? "ج.م" : txtCurrencySymbol.Text.Trim(),
                    VatRate = nudVatRate.Value,
                    EnablePrintPreview = chkEnablePrintPreview.Checked,
                    AutoPrintOnSale = chkAutoPrintOnSale.Checked,
                    DefaultMinStock = (int)nudDefaultMinStock.Value,
                    AllowNegativeStock = chkAllowNegativeStock.Checked
                };

                var result = DbHelper.SaveSystemSettings(newSettings);
                if (result.Success)
                {
                    _currentSettings = newSettings;
                    lblStatus.ForeColor = POS.DesignSystem.Tokens.UIColors.Success;
                    lblStatus.Text = "✔ تم حفظ كافة الإعدادات بنجاح!";
                    MessageBox.Show("تم حفظ وتطبيق كافة إعدادات النظام بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.ForeColor = POS.DesignSystem.Tokens.UIColors.Danger;
                    lblStatus.Text = "❌ " + result.Message;
                    MessageBox.Show(result.Message, "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الإعدادات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            if (!IsAdminUser()) return;

            var confirm = MessageBox.Show(
                "هل أنت متأكد من رغبتك في إعادة ضبط جميع الحقول إلى القيم الافتراضية للنظام؟\n(لن يتم حفظ التغييرات في قاعدة البيانات حتى تضغط على 'حفظ التغييرات')",
                "تأكيد استعادة الافتراضي",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var defaults = new SystemSettingsModel();
                txtStoreName.Text = defaults.StoreName;
                txtStoreSubtitle.Text = defaults.StoreSubtitle;
                txtStorePhone.Text = defaults.StorePhone;
                txtStoreAddress.Text = defaults.StoreAddress;
                txtTaxNumber.Text = defaults.TaxNumber;

                txtReceiptHeader.Text = defaults.ReceiptHeader;
                txtReceiptFooter.Text = defaults.ReceiptFooter;
                txtCurrencySymbol.Text = defaults.CurrencySymbol;
                nudVatRate.Value = defaults.VatRate;
                chkEnablePrintPreview.Checked = defaults.EnablePrintPreview;
                chkAutoPrintOnSale.Checked = defaults.AutoPrintOnSale;

                nudDefaultMinStock.Value = defaults.DefaultMinStock;
                chkAllowNegativeStock.Checked = defaults.AllowNegativeStock;

                lblStatus.ForeColor = Color.FromArgb(217, 119, 6);
                lblStatus.Text = "تمت استعادة القيم الافتراضية بالواجهة. اضغط 'حفظ التغييرات' لاعتمادها.";
            }
        }

        private void btnTestReceipt_Click(object sender, EventArgs e)
        {
            // إنشاء فاتورة تجريبية لمعاينة تصميم الفاتورة بالإعدادات الحالية
            SaleModel dummySale = new SaleModel
            {
                SaleId = 99999,
                SaleDate = DateTime.Now,
                CashierName = _currentUser?.FullName ?? "مدير النظام التجريبي",
                TotalAmount = 75.00m,
                Discount = 5.00m,
                FinalAmount = 70.00m,
                PaidAmount = 100.00m,
                ChangeAmount = 30.00m,
                PaymentMethod = "نقدي"
            };

            List<CartItemModel> dummyItems = new List<CartItemModel>
            {
                new CartItemModel { ProductId = 1, ProductName = "عصير فواكه طبيعي 1 لتر", Quantity = 2, UnitPrice = 25.00m },
                new CartItemModel { ProductId = 2, ProductName = "بسكويت ويفر بالشوكولاتة", Quantity = 1, UnitPrice = 25.00m }
            };

            ReceiptPrinter.PrintReceipt(dummySale, dummyItems, previewFirst: true);
        }

        private void btnBackupDb_Click(object sender, EventArgs e)
        {
            if (!IsAdminUser()) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                sfd.Filter = "SQL Backup Files (*.bak)|*.bak";
                sfd.FileName = $"POS_DB_Backup_{timestamp}.bak";
                sfd.Title = "تحديد مسار حفظ النسخة الاحتياطية";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        var res = DbHelper.BackupDatabase(sfd.FileName);
                        Cursor = Cursors.Default;

                        if (res.Success)
                        {
                            MessageBox.Show(res.Message, "تم النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(res.Message, "فشل النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("خطأ أثناء النسخ الاحتياطي: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRestoreDb_Click(object sender, EventArgs e)
        {
            if (!IsAdminUser()) return;

            var warn = MessageBox.Show(
                "تحذير هام: استعادة قاعدة البيانات ستؤدي إلى استبدال كافة البيانات الحالية بالبيانات الموجودة في ملف النسخة الاحتياطية.\n\nهل أنت متأكد من رغبتك في المتابعة؟",
                "تحذير استعادة قاعدة البيانات",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (warn != DialogResult.Yes) return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "SQL Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                ofd.Title = "اختر ملف النسخة الاحتياطية لاستعادتها";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        var res = DbHelper.RestoreDatabase(ofd.FileName);
                        Cursor = Cursors.Default;

                        if (res.Success)
                        {
                            MessageBox.Show(res.Message + "\n\nيُفضل إعادة تشغيل النظام لتحديث كافة الواجهات.", "تمت الاستعادة بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadSettings();
                        }
                        else
                        {
                            MessageBox.Show(res.Message, "فشل الاستعادة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("خطأ أثناء استعادة قاعدة البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (!IsAdminUser()) return;

            var confirm1 = MessageBox.Show(
                "⚠️ تحذير شديد الخطورة:\n\nسيتم حذف جميع فواتير المبيعات، وتفاصيلها، والمرتجعات، وفواتير المشتريات بالكامل بشكل نهائي، وتصفير عداد الفواتير ليبدأ من رقم 1.\n(لن يتم حذف الأصناف أو الأقسام أو المستخدمين).\n\nهل ترغب في المتابعة؟",
                "تأكيد تصفير المعاملات",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm1 != DialogResult.Yes) return;

            string password = PromptPassword("يرجى إدخال كلمة مرور المشرف (Admin) للتأكيد النهائي:");
            if (string.IsNullOrEmpty(password)) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                var res = DbHelper.ClearTransactionHistory(_currentUser?.Username ?? "admin", password);
                Cursor = Cursors.Default;

                if (res.Success)
                {
                    MessageBox.Show(res.Message, "تم التصفير بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(res.Message, "فشل الإجراء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("خطأ أثناء تصفير السجلات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PromptPassword(string promptText)
        {
            Form prompt = new Form()
            {
                Width = 420,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "تأكيد كلمة المرور",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                RightToLeft = RightToLeft.Yes,
                RightToLeftLayout = true,
                BackColor = Color.White
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Width = 360, Text = promptText, Font = FontManager.GetBold(9.5f) };
            TextBox textBox = new TextBox() { Left = 20, Top = 55, Width = 360, UseSystemPasswordChar = true, Font = new Font("Tahoma", 10.5f) };
            Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 90, Top = 100, DialogResult = DialogResult.OK, Height = 34, BackColor = Color.FromArgb(220, 38, 38), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button cancel = new Button() { Text = "إلغاء", Left = 100, Width = 90, Top = 100, DialogResult = DialogResult.Cancel, Height = 34, BackColor = Color.FromArgb(241, 245, 249), FlatStyle = FlatStyle.Flat };

            confirmation.FlatAppearance.BorderSize = 0;
            cancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            FontManager.ApplyCairoFont(prompt);

            return prompt.ShowDialog(this.FindForm() ?? this) == DialogResult.OK ? textBox.Text : "";
        }
    }
}
