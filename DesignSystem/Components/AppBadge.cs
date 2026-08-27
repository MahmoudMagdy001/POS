using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    public enum BadgeVariant
    {
        Success,
        Danger,
        Warning,
        Info,
        Neutral,
        Purple
    }

    /// <summary>
    /// Standardized Design System Badge/Tag pill for statuses (Paid, Pending, Low Stock, etc.).
    /// </summary>
    public class AppBadge : Control
    {
        private BadgeVariant _variant = BadgeVariant.Neutral;
        private bool _showDot = true;

        public AppBadge()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(80, 24);
            Font = UITypography.Badge;
            Text = "حالة";
        }

        [Category("Design System")]
        [DefaultValue(BadgeVariant.Neutral)]
        public BadgeVariant Variant
        {
            get => _variant;
            set { _variant = value; Invalidate(); }
        }

        [Category("Design System")]
        [DefaultValue(true)]
        public bool ShowDot
        {
            get => _showDot;
            set { _showDot = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsHelper.ConfigureHighQuality(g);

            Color bgColor;
            Color textColor;
            Color dotColor;

            switch (_variant)
            {
                case BadgeVariant.Success:
                    bgColor = UIColors.SuccessLight;
                    textColor = UIColors.SuccessDark;
                    dotColor = UIColors.Success;
                    break;
                case BadgeVariant.Danger:
                    bgColor = UIColors.DangerLight;
                    textColor = UIColors.DangerDark;
                    dotColor = UIColors.Danger;
                    break;
                case BadgeVariant.Warning:
                    bgColor = UIColors.WarningLight;
                    textColor = UIColors.WarningDark;
                    dotColor = UIColors.Warning;
                    break;
                case BadgeVariant.Info:
                    bgColor = UIColors.InfoLight;
                    textColor = UIColors.InfoDark;
                    dotColor = UIColors.Info;
                    break;
                case BadgeVariant.Purple:
                    bgColor = UIColors.PurpleLight;
                    textColor = UIColors.PurpleDark;
                    dotColor = UIColors.Purple;
                    break;
                default:
                    bgColor = UIColors.SurfaceAlt;
                    textColor = UIColors.TextSecondary;
                    dotColor = UIColors.TextMuted;
                    break;
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int pillRadius = Height / 2;

            // Draw Pill Background
            using (SolidBrush brush = new SolidBrush(bgColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, brush, rect, pillRadius);
            }

            // Text Layout & Dot
            Font f = Font ?? UITypography.Badge;
            Size textSize = TextRenderer.MeasureText(Text, f);
            int dotSize = 6;
            int dotSpacing = 6;
            int totalContentWidth = textSize.Width + (_showDot ? dotSize + dotSpacing : 0);
            int startX = (Width - totalContentWidth) / 2;
            int textY = (Height - textSize.Height) / 2;

            if (_showDot)
            {
                int dotY = (Height - dotSize) / 2;
                if (RightToLeft == RightToLeft.Yes)
                {
                    // RTL: Dot on Right, Text on Left
                    int dotX = startX + textSize.Width + dotSpacing;
                    using (SolidBrush dotBrush = new SolidBrush(dotColor))
                    {
                        g.FillEllipse(dotBrush, dotX, dotY, dotSize, dotSize);
                    }
                    TextRenderer.DrawText(g, Text, f, new Point(startX, textY), textColor, TextFormatFlags.SingleLine);
                }
                else
                {
                    // LTR: Dot on Left, Text on Right
                    using (SolidBrush dotBrush = new SolidBrush(dotColor))
                    {
                        g.FillEllipse(dotBrush, startX, dotY, dotSize, dotSize);
                    }
                    TextRenderer.DrawText(g, Text, f, new Point(startX + dotSize + dotSpacing, textY), textColor, TextFormatFlags.SingleLine);
                }
            }
            else
            {
                TextRenderer.DrawText(g, Text, f, ClientRectangle, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            }
        }
    }
}
