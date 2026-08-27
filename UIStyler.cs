using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using POS.DesignSystem.Components;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS
{
    /// <summary>
    /// Centralized UI Styler utility that provides standard design system styling,
    /// button variations, card borders, and DataGridView setup across all forms.
    /// </summary>
    public static class UIStyler
    {
        #region Form & Container Theme Application

        /// <summary>
        /// Applies the complete design system font, RTL configuration, and basic styling to a Form.
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;

            form.BackColor = UIColors.AppBackground;
            form.RightToLeft = RightToLeft.Yes;
            form.RightToLeftLayout = true;
            FontManager.ApplyCairoFont(form);
        }

        #endregion

        #region Button Styling

        /// <summary>
        /// Adjusts button width and height automatically based on its text and font metrics.
        /// </summary>
        public static void AutoFitButton(Button btn, int minWidth = 90, int minHeight = UISpacing.ButtonHeightDefault, int horizontalPadding = 24)
        {
            if (btn == null) return;

            try
            {
                if (btn.Dock == DockStyle.None && !string.IsNullOrEmpty(btn.Text))
                {
                    Font f = btn.Font ?? UITypography.Button;
                    Size measured = TextRenderer.MeasureText(btn.Text, f);
                    int desiredWidth = Math.Max(minWidth, measured.Width + horizontalPadding);
                    int desiredHeight = Math.Max(minHeight, measured.Height + 12);

                    if (btn.Width < desiredWidth)
                        btn.Width = desiredWidth;
                    if (btn.Height < desiredHeight)
                        btn.Height = desiredHeight;
                }
            }
            catch { }
        }

        /// <summary>
        /// Enables native WinForms AutoSize for a button with safe growth settings and padding.
        /// </summary>
        public static void EnableAutoSize(Button btn, int horizontalPadding = 16, int verticalPadding = 6)
        {
            if (btn == null) return;
            btn.AutoSize = true;
            btn.AutoSizeMode = AutoSizeMode.GrowOnly;
            btn.Padding = new Padding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        }

        /// <summary>
        /// Styles a button as a primary action button (Solid Blue/Sky with white text).
        /// </summary>
        public static void StylePrimaryButton(Button btn, string text = null, bool autoFit = true)
        {
            if (btn == null) return;
            if (text != null) btn.Text = text;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = UIColors.Primary;
            btn.ForeColor = UIColors.TextLight;
            btn.Font = UITypography.Button;
            btn.Cursor = Cursors.Hand;
            btn.UseCompatibleTextRendering = false;
            btn.RightToLeft = RightToLeft.No;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(0);

            if (autoFit) AutoFitButton(btn);
        }

        /// <summary>
        /// Styles a button as a secondary action button (Light slate background with dark border).
        /// </summary>
        public static void StyleSecondaryButton(Button btn, string text = null, bool autoFit = true)
        {
            if (btn == null) return;
            if (text != null) btn.Text = text;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = UIColors.BorderDark;
            btn.BackColor = UIColors.SurfaceAlt;
            btn.ForeColor = UIColors.TextSecondary;
            btn.Font = UITypography.Button;
            btn.Cursor = Cursors.Hand;
            btn.UseCompatibleTextRendering = false;
            btn.RightToLeft = RightToLeft.No;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(0);

            if (autoFit) AutoFitButton(btn);
        }

        /// <summary>
        /// Styles a button as a success action button (Solid Green with white text).
        /// </summary>
        public static void StyleSuccessButton(Button btn, string text = null, bool autoFit = true)
        {
            if (btn == null) return;
            if (text != null) btn.Text = text;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = UIColors.Success;
            btn.ForeColor = UIColors.TextLight;
            btn.Font = UITypography.Button;
            btn.Cursor = Cursors.Hand;
            btn.UseCompatibleTextRendering = false;
            btn.RightToLeft = RightToLeft.No;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(0);

            if (autoFit) AutoFitButton(btn);
        }

        /// <summary>
        /// Styles a button as a danger / delete action button (Solid Red with white text).
        /// </summary>
        public static void StyleDangerButton(Button btn, string text = null, bool autoFit = true)
        {
            if (btn == null) return;
            if (text != null) btn.Text = text;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = UIColors.Danger;
            btn.ForeColor = UIColors.TextLight;
            btn.Font = UITypography.Button;
            btn.Cursor = Cursors.Hand;
            btn.UseCompatibleTextRendering = false;
            btn.RightToLeft = RightToLeft.No;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(0);

            if (autoFit) AutoFitButton(btn);
        }

        /// <summary>
        /// Styles a button as an active/inactive filter pill tab.
        /// </summary>
        public static void SetFilterButtonActive(Button btn, bool isActive)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.UseCompatibleTextRendering = false;
            btn.Cursor = Cursors.Hand;
            btn.RightToLeft = RightToLeft.No;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(0);

            if (isActive)
            {
                btn.BackColor = UIColors.Primary;
                btn.ForeColor = UIColors.TextLight;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = UITypography.Button;
            }
            else
            {
                btn.BackColor = UIColors.SurfaceAlt;
                btn.ForeColor = UIColors.TextSecondary;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = UIColors.BorderDark;
                btn.Font = UITypography.Subtitle;
            }

            AutoFitButton(btn);
        }

        #endregion

        #region DataGridView Modern Styling

        /// <summary>
        /// Applies standard modern DataGridView styling (Cairo font, row heights, alternating colors, selection styles, double buffering).
        /// </summary>
        public static void StyleDataGrid(DataGridView dgv)
        {
            if (dgv == null) return;

            // Enable double buffering via reflection to eliminate flickering
            try
            {
                typeof(DataGridView).InvokeMember(
                    "DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null,
                    dgv,
                    new object[] { true });
            }
            catch { }

            dgv.BackgroundColor = UIColors.Surface;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = UIColors.SurfaceAlt;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.RightToLeft = RightToLeft.Yes;

            // Column Header Style
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = UISpacing.GridHeaderHeight;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = UIColors.SurfaceAlt;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = UIColors.TextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.Font = UITypography.GridHeader;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = UIColors.SurfaceAlt;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = UIColors.TextPrimary;

            // Row Template & Cell Styles
            dgv.RowTemplate.Height = UISpacing.GridRowHeight;
            dgv.DefaultCellStyle.BackColor = UIColors.Surface;
            dgv.DefaultCellStyle.ForeColor = UIColors.TextPrimary;
            dgv.DefaultCellStyle.Font = UITypography.GridCell;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.SelectionBackColor = UIColors.PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = UIColors.PrimaryDark;
            dgv.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Styling
            dgv.AlternatingRowsDefaultCellStyle.BackColor = UIColors.AppBackground;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = UIColors.TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.Font = UITypography.GridCell;
            dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = UIColors.PrimaryLight;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = UIColors.PrimaryDark;
            dgv.AlternatingRowsDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
        }

        #endregion

        #region Card & Panel Styling

        /// <summary>
        /// Styles a panel as a modern card container with a clean white background and subtle anti-aliased border.
        /// </summary>
        public static void StyleCardPanel(Panel panel)
        {
            if (panel == null) return;

            panel.BackColor = UIColors.Surface;
            panel.Paint -= OnCardPanelPaint;
            panel.Paint += OnCardPanelPaint;
        }

        private static void OnCardPanelPaint(object sender, PaintEventArgs e)
        {
            if (sender is Panel p)
            {
                GraphicsHelper.ConfigureHighQuality(e.Graphics);
                using (var pen = new Pen(UIColors.Border, 1f))
                {
                    var rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
                    GraphicsHelper.DrawRoundedRectangle(e.Graphics, pen, rect, UISpacing.RadiusMedium);
                }
            }
        }

        /// <summary>
        /// Styles a KPI Summary Card with title, large numeric value, subtitle, and accent color.
        /// </summary>
        public static void StyleKpiCard(Panel card, Label lblTitle, Label lblVal, Label lblSub, Color accentColor)
        {
            if (card != null)
            {
                card.BackColor = UIColors.Surface;
                card.Paint -= OnCardPanelPaint;
                card.Paint += OnCardPanelPaint;
            }

            if (lblTitle != null)
            {
                lblTitle.ForeColor = UIColors.TextMuted;
                lblTitle.Font = UITypography.CardTitle;
                lblTitle.UseCompatibleTextRendering = false;
            }

            if (lblVal != null)
            {
                lblVal.ForeColor = accentColor;
                lblVal.Font = UITypography.KpiNumberMedium;
                lblVal.UseCompatibleTextRendering = false;
            }

            if (lblSub != null)
            {
                lblSub.ForeColor = UIColors.TextSecondary;
                lblSub.Font = UITypography.Caption;
                lblSub.UseCompatibleTextRendering = false;
            }
        }

        #endregion

        #region Inputs & TextBoxes

        /// <summary>
        /// Styles a TextBox for search or input with clean typography and padding.
        /// </summary>
        public static void StyleTextBox(TextBox txt)
        {
            if (txt == null) return;

            txt.Font = UITypography.Input;
            txt.BackColor = UIColors.Surface;
            txt.ForeColor = UIColors.TextPrimary;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// Styles a ComboBox with design system typography and colors.
        /// </summary>
        public static void StyleComboBox(ComboBox cmb)
        {
            if (cmb == null) return;

            cmb.Font = UITypography.Input;
            cmb.BackColor = UIColors.Surface;
            cmb.ForeColor = UIColors.TextPrimary;
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.RightToLeft = RightToLeft.Yes;
        }

        #endregion
    }
}
