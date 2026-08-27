using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    public enum ButtonVariant
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Outline,
        Ghost
    }

    /// <summary>
    /// Standardized Design System Button control with custom variant styling,
    /// rounded corners, smooth hover effects, Cairo typography, and RTL support.
    /// </summary>
    [DefaultEvent("Click")]
    public class AppButton : Button
    {
        private ButtonVariant _variant = ButtonVariant.Primary;
        private int _borderRadius = UISpacing.RadiusMedium;
        private int _borderSize = 0;
        private Color _borderColor = Color.Transparent;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private Image _customIcon = null;
        private int _iconSpacing = 8;

        public AppButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Size = new Size(120, UISpacing.ButtonHeightDefault);
            Font = UITypography.Button;
            UseCompatibleTextRendering = false;
            UpdateColors();
        }

        [Category("Design System")]
        [DefaultValue(ButtonVariant.Primary)]
        public ButtonVariant Variant
        {
            get => _variant;
            set
            {
                _variant = value;
                UpdateColors();
                Invalidate();
            }
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

        [Category("Design System")]
        [DefaultValue(null)]
        public Image CustomIcon
        {
            get => _customIcon;
            set
            {
                _customIcon = value;
                Invalidate();
            }
        }

        [Category("Design System")]
        [DefaultValue(8)]
        public int IconSpacing
        {
            get => _iconSpacing;
            set
            {
                _iconSpacing = value;
                Invalidate();
            }
        }

        private void UpdateColors()
        {
            switch (_variant)
            {
                case ButtonVariant.Primary:
                    BackColor = _isPressed ? UIColors.PrimaryActive : (_isHovered ? UIColors.PrimaryHover : UIColors.Primary);
                    ForeColor = UIColors.TextLight;
                    _borderSize = 0;
                    break;

                case ButtonVariant.Secondary:
                    BackColor = _isPressed ? UIColors.SecondaryActive : (_isHovered ? UIColors.SecondaryHover : UIColors.SurfaceAlt);
                    ForeColor = (_isHovered || _isPressed) ? UIColors.TextLight : UIColors.TextSecondary;
                    _borderSize = 1;
                    _borderColor = UIColors.BorderDark;
                    break;

                case ButtonVariant.Success:
                    BackColor = _isPressed ? UIColors.SuccessDark : (_isHovered ? UIColors.SuccessHover : UIColors.Success);
                    ForeColor = UIColors.TextLight;
                    _borderSize = 0;
                    break;

                case ButtonVariant.Danger:
                    BackColor = _isPressed ? UIColors.DangerDark : (_isHovered ? UIColors.DangerHover : UIColors.Danger);
                    ForeColor = UIColors.TextLight;
                    _borderSize = 0;
                    break;

                case ButtonVariant.Warning:
                    BackColor = _isPressed ? UIColors.WarningDark : (_isHovered ? UIColors.WarningHover : UIColors.Warning);
                    ForeColor = UIColors.TextLight;
                    _borderSize = 0;
                    break;

                case ButtonVariant.Info:
                    BackColor = _isPressed ? UIColors.InfoDark : (_isHovered ? UIColors.InfoHover : UIColors.Info);
                    ForeColor = UIColors.TextLight;
                    _borderSize = 0;
                    break;

                case ButtonVariant.Outline:
                    BackColor = _isPressed ? UIColors.PrimaryLight : (_isHovered ? UIColors.SurfaceHover : UIColors.Surface);
                    ForeColor = UIColors.Primary;
                    _borderSize = 1;
                    _borderColor = UIColors.Primary;
                    break;

                case ButtonVariant.Ghost:
                    BackColor = _isPressed ? UIColors.SurfaceAlt : (_isHovered ? UIColors.SurfaceHover : Color.Transparent);
                    ForeColor = UIColors.TextSecondary;
                    _borderSize = 0;
                    break;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            UpdateColors();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            UpdateColors();
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                _isPressed = true;
                UpdateColors();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isPressed = false;
            UpdateColors();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            GraphicsHelper.ConfigureHighQuality(g);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // 1. Draw Background
            using (SolidBrush bgBrush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, bgBrush, rect, _borderRadius);
            }

            // 2. Draw Border if configured
            if (_borderSize > 0 && _borderColor != Color.Transparent)
            {
                using (Pen pen = new Pen(_borderColor, _borderSize))
                {
                    pen.Alignment = PenAlignment.Inset;
                    GraphicsHelper.DrawRoundedRectangle(g, pen, rect, _borderRadius);
                }
            }

            // 3. Draw Icon & Text
            Font textFont = Font ?? UITypography.Button;
            Size textSize = TextRenderer.MeasureText(Text, textFont);
            int iconWidth = _customIcon?.Width ?? 0;
            int totalContentWidth = textSize.Width + (iconWidth > 0 ? iconWidth + _iconSpacing : 0);

            int startX = (Width - totalContentWidth) / 2;
            int startY = (Height - textSize.Height) / 2;

            if (_customIcon != null)
            {
                int iconY = (Height - _customIcon.Height) / 2;
                if (RightToLeft == RightToLeft.Yes)
                {
                    // RTL: Icon on Right, Text on Left
                    int iconX = startX + textSize.Width + _iconSpacing;
                    g.DrawImage(_customIcon, iconX, iconY);
                    TextRenderer.DrawText(g, Text, textFont, new Point(startX, startY), ForeColor, TextFormatFlags.SingleLine);
                }
                else
                {
                    // LTR: Icon on Left, Text on Right
                    g.DrawImage(_customIcon, startX, iconY);
                    TextRenderer.DrawText(g, Text, textFont, new Point(startX + iconWidth + _iconSpacing, startY), ForeColor, TextFormatFlags.SingleLine);
                }
            }
            else
            {
                TextRenderer.DrawText(g, Text, textFont, ClientRectangle, ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            }
        }
    }
}
