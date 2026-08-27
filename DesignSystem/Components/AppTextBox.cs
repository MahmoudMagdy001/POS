using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System Rounded TextBox with placeholder support,
    /// focus highlight border, and consistent Cairo font.
    /// </summary>
    [DefaultEvent("TextChanged")]
    public class AppTextBox : UserControl
    {
        private TextBox _innerTextBox;
        private string _placeholderText = "";
        private bool _isFocused = false;
        private int _borderRadius = UISpacing.RadiusMedium;
        private Color _borderColor = UIColors.Border;
        private Color _borderFocusColor = UIColors.BorderFocus;

        public new event EventHandler TextChanged;

        public AppTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            _innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = UIColors.Surface,
                ForeColor = UIColors.TextPrimary,
                Font = UITypography.Input,
                Dock = DockStyle.None,
                Location = new Point(10, 8)
            };

            _innerTextBox.Enter += InnerTextBox_Enter;
            _innerTextBox.Leave += InnerTextBox_Leave;
            _innerTextBox.TextChanged += InnerTextBox_TextChanged;

            Controls.Add(_innerTextBox);

            BackColor = UIColors.Surface;
            Size = new Size(200, UISpacing.InputHeightDefault);
            Padding = new Padding(10, 8, 10, 8);
            AdjustInnerTextBox();
        }

        [Category("Design System")]
        public override string Text
        {
            get => _innerTextBox.Text;
            set
            {
                _innerTextBox.Text = value;
                Invalidate();
            }
        }

        [Category("Design System")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                Invalidate();
            }
        }

        [Category("Design System")]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get => _innerTextBox.UseSystemPasswordChar;
            set => _innerTextBox.UseSystemPasswordChar = value;
        }

        [Category("Design System")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _innerTextBox.ReadOnly;
            set => _innerTextBox.ReadOnly = value;
        }

        [Category("Design System")]
        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get => _innerTextBox.TextAlign;
            set => _innerTextBox.TextAlign = value;
        }

        [Category("Design System")]
        [DefaultValue(8)]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        public TextBox InnerTextBox => _innerTextBox;

        private void InnerTextBox_Enter(object sender, EventArgs e)
        {
            _isFocused = true;
            Invalidate();
        }

        private void InnerTextBox_Leave(object sender, EventArgs e)
        {
            _isFocused = false;
            Invalidate();
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustInnerTextBox();
        }

        private void AdjustInnerTextBox()
        {
            if (_innerTextBox == null) return;
            _innerTextBox.Location = new Point(10, (Height - _innerTextBox.Height) / 2);
            _innerTextBox.Width = Width - 20;
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
            Color currentBorder = _isFocused ? _borderFocusColor : _borderColor;
            int borderWidth = _isFocused ? 2 : 1;
            using (Pen pen = new Pen(currentBorder, borderWidth))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                GraphicsHelper.DrawRoundedRectangle(g, pen, rect, _borderRadius);
            }

            // Draw Placeholder if empty and not focused
            if (string.IsNullOrEmpty(_innerTextBox.Text) && !string.IsNullOrEmpty(_placeholderText) && !_isFocused)
            {
                Rectangle placeholderRect = new Rectangle(_innerTextBox.Left, _innerTextBox.Top, _innerTextBox.Width, _innerTextBox.Height);
                TextRenderer.DrawText(g, _placeholderText, _innerTextBox.Font, placeholderRect, UIColors.TextPlaceholder,
                    (RightToLeft == RightToLeft.Yes ? TextFormatFlags.Right : TextFormatFlags.Left) | TextFormatFlags.VerticalCenter);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _innerTextBox.Focus();
        }
    }
}
