using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace POS
{
    /// <summary>
    /// Centralized UI Styler utility that provides standard styling functions,
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

            form.BackColor = UITheme.AppBackground;
            form.RightToLeft = RightToLeft.Yes;
            form.RightToLeftLayout = true;
            FontManager.ApplyCairoFont(form);
        }

        #endregion

        #region Button Styling

        /// <summary>
        /// Adjusts button width and height automatically based on its text and font metrics.
        /// </summary>
        public static void AutoFitButton(Button btn, int minWidth = 90, int minHeight = 36, int horizontalPadding = 24)
        {
            if (btn == null) return;

            try
            {
                if (btn.Dock == DockStyle.None && !string.IsNullOrEmpty(btn.Text))
                {
                    Font f = btn.Font ?? UITheme.ButtonFont;
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
            btn.BackColor = UITheme.Primary;
            btn.ForeColor = UITheme.TextLight;
            btn.Font = UITheme.ButtonFont;
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
            btn.FlatAppearance.BorderColor = UITheme.BorderDark;
            btn.BackColor = UITheme.SurfaceAlt;
            btn.ForeColor = UITheme.TextSecondary;
            btn.Font = UITheme.ButtonFont;
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
            btn.BackColor = UITheme.Success;
            btn.ForeColor = UITheme.TextLight;
            btn.Font = UITheme.ButtonFont;
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
            btn.BackColor = UITheme.Danger;
            btn.ForeColor = UITheme.TextLight;
            btn.Font = UITheme.ButtonFont;
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
                btn.BackColor = UITheme.Primary;
                btn.ForeColor = UITheme.TextLight;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = UITheme.ButtonFont;
            }
            else
            {
                btn.BackColor = UITheme.SurfaceAlt;
                btn.ForeColor = UITheme.TextSecondary;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = UITheme.BorderDark;
                btn.Font = UITheme.SubtitleFont;
            }

            AutoFitButton(btn);
        }

        #endregion

        #region DataGridView Modern Styling

        /// <summary>
        /// Applies standard modern DataGridView styling (Cairo font, row heights, alternating colors, selection styles).
        /// </summary>
        public static void StyleDataGrid(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.BackgroundColor = UITheme.Surface;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = UITheme.SurfaceAlt;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            // Column Header Style
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = UITheme.GridHeaderHeight;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = UITheme.AppBackground;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = UITheme.TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Font = UITheme.GridHeaderFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = UITheme.AppBackground;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = UITheme.TextSecondary;

            // Row Template & Cell Styles
            dgv.RowTemplate.Height = UITheme.GridRowHeight;
            dgv.DefaultCellStyle.BackColor = UITheme.Surface;
            dgv.DefaultCellStyle.ForeColor = UITheme.TextPrimary;
            dgv.DefaultCellStyle.Font = UITheme.GridCellFont;
            dgv.DefaultCellStyle.SelectionBackColor = UITheme.SurfaceAlt;
            dgv.DefaultCellStyle.SelectionForeColor = UITheme.TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

            // Alternating Row Styling
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 255);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = UITheme.TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = UITheme.SurfaceAlt;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = UITheme.TextPrimary;
        }

        #endregion

        #region Card & Panel Styling

        /// <summary>
        /// Styles a panel as a modern card container with a clean white background and subtle border.
        /// </summary>
        public static void StyleCardPanel(Panel panel)
        {
            if (panel == null) return;

            panel.BackColor = UITheme.Surface;
            panel.Paint -= OnCardPanelPaint;
            panel.Paint += OnCardPanelPaint;
        }

        private static void OnCardPanelPaint(object sender, PaintEventArgs e)
        {
            if (sender is Panel p)
            {
                using (var pen = new Pen(UITheme.Border, 1f))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    var rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
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
                card.BackColor = UITheme.Surface;
                card.Paint -= OnCardPanelPaint;
                card.Paint += OnCardPanelPaint;
            }

            if (lblTitle != null)
            {
                lblTitle.ForeColor = UITheme.TextMuted;
                lblTitle.Font = UITheme.SubtitleFont;
                lblTitle.UseCompatibleTextRendering = false;
            }

            if (lblVal != null)
            {
                lblVal.ForeColor = accentColor;
                lblVal.Font = UITheme.KpiNumberFont;
                lblVal.UseCompatibleTextRendering = false;
            }

            if (lblSub != null)
            {
                lblSub.ForeColor = UITheme.TextMuted;
                lblSub.Font = UITheme.CaptionFont;
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

            txt.Font = UITheme.BodyFont;
            txt.BackColor = UITheme.Surface;
            txt.ForeColor = UITheme.TextPrimary;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion
    }
}
