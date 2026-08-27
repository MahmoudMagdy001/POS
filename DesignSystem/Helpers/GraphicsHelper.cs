using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace POS.DesignSystem.Helpers
{
    /// <summary>
    /// Graphics rendering helper for drawing anti-aliased rounded shapes, borders, and shadows.
    /// </summary>
    public static class GraphicsHelper
    {
        public static GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (rect.Width <= 0 || rect.Height <= 0) return path;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            Rectangle arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top Left
            path.AddArc(arc, 180, 90);

            // Top Right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom Right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom Left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void ConfigureHighQuality(Graphics g)
        {
            if (g == null) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

        public static void FillRoundedRectangle(Graphics g, Brush brush, Rectangle rect, int radius)
        {
            if (g == null || brush == null || rect.Width <= 0 || rect.Height <= 0) return;
            using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(Graphics g, Pen pen, Rectangle rect, int radius)
        {
            if (g == null || pen == null || rect.Width <= 0 || rect.Height <= 0) return;
            using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
            {
                g.DrawPath(pen, path);
            }
        }
    }
}
