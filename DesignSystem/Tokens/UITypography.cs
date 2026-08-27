using System.Drawing;

namespace POS.DesignSystem.Tokens
{
    /// <summary>
    /// Design Tokens: Standard Cairo Typography & Fonts
    /// </summary>
    public static class UITypography
    {
        // Titles & Headings
        public static Font PageTitle => FontManager.GetBold(18f);
        public static Font AppTitle => FontManager.GetBold(16f);
        public static Font SectionHeader => FontManager.GetBold(14f);
        public static Font CardTitle => FontManager.GetBold(11f);
        public static Font Subtitle => FontManager.GetRegular(9.5f);

        // Body & Content
        public static Font Body => FontManager.GetRegular(9.5f);
        public static Font BodyMedium => FontManager.GetSemiBold(9.5f);
        public static Font BodyBold => FontManager.GetBold(9.5f);
        public static Font Caption => FontManager.GetRegular(8.5f);
        public static Font CaptionBold => FontManager.GetBold(8.5f);

        // Controls
        public static Font Button => FontManager.GetBold(10f);
        public static Font ButtonLarge => FontManager.GetBold(11.5f);
        public static Font Input => FontManager.GetRegular(9.5f);
        public static Font Badge => FontManager.GetBold(8.5f);

        // Metrics & KPI Numbers
        public static Font KpiNumberLarge => FontManager.GetBold(22f);
        public static Font KpiNumberMedium => FontManager.GetBold(18f);
        public static Font KpiNumberSmall => FontManager.GetBold(14f);

        // DataGrid
        public static Font GridHeader => FontManager.GetBold(9.5f);
        public static Font GridCell => FontManager.GetRegular(9.5f);
        public static Font GridCellBold => FontManager.GetBold(9.5f);
    }
}
