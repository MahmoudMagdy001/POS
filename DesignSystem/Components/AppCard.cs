using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System Card container with rounded borders,
    /// surface background, customizable border colors, and optional accent top border.
    /// </summary>
    public class AppCard : Panel
    {
        private int _borderRadius = UISpacing.RadiusMedium;
        private int _borderWidth = 1;
        private Color _borderColor = UIColors.Border;
        private Color _accentColor = Color.Transparent;
        private int _accentHeight = 3;

        public AppCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = UIColors.Surface;
            Padding = new Padding(UISpacing.SpaceLG);
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
        [DefaultValue(1)]
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Design System")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        [Category("Design System")]
        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                Invalidate();
            }
        }

        [Category("Design System")]
        [DefaultValue(3)]
        public int AccentHeight
        {
            get => _accentHeight;
            set
            {
                _accentHeight = Math.Max(0, value);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsHelper.ConfigureHighQuality(g);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // 1. Draw Card Background
            using (SolidBrush bgBrush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, bgBrush, rect, _borderRadius);
            }

            // 2. Draw Accent Bar (optional)
            if (_accentColor != Color.Transparent && _accentHeight > 0)
            {
                Rectangle accentRect = new Rectangle(0, 0, Width, _accentHeight);
                using (SolidBrush accentBrush = new SolidBrush(_accentColor))
                {
                    g.FillRectangle(accentBrush, accentRect);
                }
            }

            // 3. Draw Outer Border
            if (_borderWidth > 0 && _borderColor != Color.Transparent)
            {
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    pen.Alignment = PenAlignment.Inset;
                    GraphicsHelper.DrawRoundedRectangle(g, pen, rect, _borderRadius);
                }
            }
        }
    }
}
