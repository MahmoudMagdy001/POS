using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System KPI Stat Card for dashboards and summary overviews.
    /// Displays Title, Formatted Metric Value, Subtitle/Trend, and an Icon container.
    /// </summary>
    public class AppKpiCard : UserControl
    {
        private string _title = "إجمالي المبيعات";
        private string _value = "0.00 ج.م";
        private string _subtitle = "مقارنة بالشهر السابق";
        private Color _accentColor = UIColors.Primary;
        private Image _icon = null;
        private int _borderRadius = UISpacing.RadiusMedium;

        public AppKpiCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(220, UISpacing.KpiCardHeight);
            BackColor = UIColors.Surface;
            RightToLeft = RightToLeft.Yes;
        }

        [Category("Design System")]
        [DefaultValue("العنوان")]
        public string Title
        {
            get => _title;
            set { _title = value; Invalidate(); }
        }

        [Category("Design System")]
        [DefaultValue("0.00")]
        public string Value
        {
            get => _value;
            set { _value = value; Invalidate(); }
        }

        [Category("Design System")]
        [DefaultValue("")]
        public string Subtitle
        {
            get => _subtitle;
            set { _subtitle = value; Invalidate(); }
        }

        [Category("Design System")]
        public Color AccentColor
        {
            get => _accentColor;
            set { _accentColor = value; Invalidate(); }
        }

        [Category("Design System")]
        [DefaultValue(null)]
        public Image Icon
        {
            get => _icon;
            set { _icon = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsHelper.ConfigureHighQuality(g);

            Rectangle cardRect = new Rectangle(0, 0, Width - 1, Height - 1);

            // 1. Card Background
            using (SolidBrush bgBrush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, bgBrush, cardRect, _borderRadius);
            }

            // 2. Card Border
            using (Pen borderPen = new Pen(UIColors.Border, 1))
            {
                GraphicsHelper.DrawRoundedRectangle(g, borderPen, cardRect, _borderRadius);
            }

            // 3. Right Accent Indicator Strip (in RTL)
            int stripWidth = 4;
            int stripHeight = Height - 24;
            Rectangle stripRect = new Rectangle(Width - 10, 12, stripWidth, stripHeight);
            using (SolidBrush stripBrush = new SolidBrush(_accentColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, stripBrush, stripRect, 2);
            }

            // 4. Layout coordinates for text (RTL aware)
            int textRight = Width - 24;
            int textLeft = 16;
            int textWidth = textRight - textLeft;

            // Draw Title
            Font titleFont = UITypography.CardTitle;
            Rectangle titleRect = new Rectangle(textLeft, 14, textWidth, 20);
            TextRenderer.DrawText(g, _title, titleFont, titleRect, UIColors.TextMuted,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);

            // Draw Value
            Font valueFont = UITypography.KpiNumberMedium;
            Rectangle valueRect = new Rectangle(textLeft, 36, textWidth, 32);
            TextRenderer.DrawText(g, _value, valueFont, valueRect, UIColors.TextPrimary,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);

            // Draw Subtitle / Trend
            if (!string.IsNullOrEmpty(_subtitle))
            {
                Font subFont = UITypography.Caption;
                Rectangle subRect = new Rectangle(textLeft, 70, textWidth, 18);
                TextRenderer.DrawText(g, _subtitle, subFont, subRect, UIColors.TextSecondary,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            }

            // 5. Draw Icon Container (Top Left in RTL)
            if (_icon != null)
            {
                int iconBoxSize = 36;
                Rectangle iconBoxRect = new Rectangle(14, 14, iconBoxSize, iconBoxSize);
                using (SolidBrush iconBgBrush = new SolidBrush(Color.FromArgb(30, _accentColor)))
                {
                    GraphicsHelper.FillRoundedRectangle(g, iconBgBrush, iconBoxRect, 8);
                }

                int iconX = iconBoxRect.X + (iconBoxSize - _icon.Width) / 2;
                int iconY = iconBoxRect.Y + (iconBoxSize - _icon.Height) / 2;
                g.DrawImage(_icon, iconX, iconY);
            }
        }
    }
}
