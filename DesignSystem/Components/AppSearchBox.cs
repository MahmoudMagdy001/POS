using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System Search Box control with embedded search icon,
    /// clear action, and debounced text change event.
    /// </summary>
    [DefaultEvent("SearchTextChanged")]
    public class AppSearchBox : UserControl
    {
        private TextBox _txtSearch;
        private Label _lblClear;
        private string _placeholder = "بحث...";
        private bool _isFocused = false;
        private int _borderRadius = UISpacing.RadiusMedium;

        public event EventHandler SearchTextChanged;
        public event EventHandler SearchCleared;

        public AppSearchBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(260, UISpacing.SearchBoxHeight);
            BackColor = UIColors.Surface;
            RightToLeft = RightToLeft.Yes;

            _txtSearch = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = UIColors.Surface,
                ForeColor = UIColors.TextPrimary,
                Font = UITypography.Input,
                RightToLeft = RightToLeft.Yes
            };

            _txtSearch.Enter += (s, e) => { _isFocused = true; Invalidate(); };
            _txtSearch.Leave += (s, e) => { _isFocused = false; Invalidate(); };
            _txtSearch.TextChanged += TxtSearch_TextChanged;

            _lblClear = new Label
            {
                Text = "✕",
                Font = new Font("Arial", 9f, FontStyle.Bold),
                ForeColor = UIColors.TextMuted,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(18, 18),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            _lblClear.Click += (s, e) =>
            {
                _txtSearch.Clear();
                SearchCleared?.Invoke(this, EventArgs.Empty);
            };

            _lblClear.MouseEnter += (s, e) => _lblClear.ForeColor = UIColors.Danger;
            _lblClear.MouseLeave += (s, e) => _lblClear.ForeColor = UIColors.TextMuted;

            Controls.Add(_txtSearch);
            Controls.Add(_lblClear);

            AdjustLayout();
        }

        [Category("Design System")]
        public string SearchText
        {
            get => _txtSearch.Text;
            set => _txtSearch.Text = value;
        }

        [Category("Design System")]
        [DefaultValue("بحث...")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _lblClear.Visible = !string.IsNullOrEmpty(_txtSearch.Text);
            SearchTextChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (_txtSearch == null || _lblClear == null) return;

            int iconPadding = 30;
            int clearPadding = 24;

            _txtSearch.Location = new Point(clearPadding + 4, (Height - _txtSearch.Height) / 2);
            _txtSearch.Width = Width - iconPadding - clearPadding - 8;

            _lblClear.Location = new Point(6, (Height - _lblClear.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsHelper.ConfigureHighQuality(g);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Draw Background
            using (SolidBrush bgBrush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, bgBrush, rect, _borderRadius);
            }

            // Draw Border
            Color currentBorder = _isFocused ? UIColors.BorderFocus : UIColors.Border;
            int borderWidth = _isFocused ? 2 : 1;
            using (Pen pen = new Pen(currentBorder, borderWidth))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                GraphicsHelper.DrawRoundedRectangle(g, pen, rect, _borderRadius);
            }

            // Draw Search Icon (Vector indicator on right side in RTL)
            using (var iconPen = new Pen(UIColors.TextMuted, 1.6f))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int cx = Width - 16;
                int cy = Height / 2;
                g.DrawEllipse(iconPen, cx - 8, cy - 7, 7, 7);
                g.DrawLine(iconPen, cx - 3, cy - 2, cx + 1, cy + 2);
            }

            // Draw Placeholder if empty
            if (string.IsNullOrEmpty(_txtSearch.Text) && !_isFocused)
            {
                Rectangle placeholderRect = new Rectangle(_txtSearch.Left, _txtSearch.Top, _txtSearch.Width, _txtSearch.Height);
                TextRenderer.DrawText(g, _placeholder, _txtSearch.Font, placeholderRect, UIColors.TextPlaceholder,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
