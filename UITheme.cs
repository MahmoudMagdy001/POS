using System;
using System.Drawing;

namespace POS
{
    /// <summary>
    /// Centralized Design System tokens containing standard colors, typography,
    /// spacing, and visual styles for consistent UI across the entire application.
    /// </summary>
    public static class UITheme
    {
        #region Colors - Brand & Core Palette

        // Primary Brand Colors (Modern Sky / Indigo Blue)
        public static readonly Color Primary = Color.FromArgb(14, 165, 233);       // #0EA5E9
        public static readonly Color PrimaryHover = Color.FromArgb(2, 132, 199);   // #0284C7
        public static readonly Color PrimaryDark = Color.FromArgb(3, 105, 161);    // #0369A1
        public static readonly Color PrimaryLight = Color.FromArgb(224, 242, 254); // #E0F2FE

        // Secondary / Slate
        public static readonly Color Secondary = Color.FromArgb(71, 85, 105);      // #475569
        public static readonly Color SecondaryHover = Color.FromArgb(51, 65, 85);  // #334155
        public static readonly Color SecondaryLight = Color.FromArgb(241, 245, 249);// #F1F5F9

        // Neutral / Backgrounds
        public static readonly Color AppBackground = Color.FromArgb(248, 250, 252);// #F8FAFC
        public static readonly Color Surface = Color.FromArgb(255, 255, 255);       // #FFFFFF
        public static readonly Color SurfaceAlt = Color.FromArgb(241, 245, 249);   // #F1F5F9

        // Borders & Dividers
        public static readonly Color Border = Color.FromArgb(226, 232, 240);       // #E2E8F0
        public static readonly Color BorderDark = Color.FromArgb(203, 213, 225);   // #CBD5E1

        // Typography Colors
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);     // #0F172A
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);  // #475569
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);    // #64748B
        public static readonly Color TextLight = Color.FromArgb(255, 255, 255);    // #FFFFFF

        // Semantic Status Colors
        public static readonly Color Success = Color.FromArgb(22, 163, 74);        // #16A34A
        public static readonly Color SuccessLight = Color.FromArgb(220, 252, 231); // #DCFCE7
        public static readonly Color SuccessDark = Color.FromArgb(20, 83, 45);     // #14532D

        public static readonly Color Danger = Color.FromArgb(220, 38, 38);         // #DC2626
        public static readonly Color DangerLight = Color.FromArgb(254, 226, 226);  // #FEE2E2
        public static readonly Color DangerDark = Color.FromArgb(127, 29, 29);     // #7F1D1D

        public static readonly Color Warning = Color.FromArgb(217, 119, 6);        // #D97706
        public static readonly Color WarningLight = Color.FromArgb(254, 243, 199); // #FEF3C7
        public static readonly Color WarningDark = Color.FromArgb(120, 53, 15);    // #78350F

        public static readonly Color Info = Color.FromArgb(37, 99, 235);           // #2563EB
        public static readonly Color InfoLight = Color.FromArgb(219, 234, 254);    // #DBEAFE

        public static readonly Color Purple = Color.FromArgb(124, 58, 237);        // #7C3AED
        public static readonly Color PurpleLight = Color.FromArgb(243, 232, 255);  // #F3E8FF

        // Sidebar Navigation
        public static readonly Color SidebarBackground = Color.FromArgb(15, 23, 42); // #0F172A
        public static readonly Color SidebarActiveItem = Color.FromArgb(30, 41, 59); // #1E293B
        public static readonly Color SidebarText = Color.FromArgb(148, 163, 184);   // #94A3B8
        public static readonly Color SidebarActiveText = Color.White;

        #endregion

        #region Typography Fonts (Cairo Embedded Integration)

        public static Font AppTitleFont => FontManager.GetBold(16f);
        public static Font SectionHeaderFont => FontManager.GetBold(14f);
        public static Font CardTitleFont => FontManager.GetBold(11f);
        public static Font SubtitleFont => FontManager.GetRegular(9.5f);

        public static Font BodyFont => FontManager.GetRegular(9.5f);
        public static Font BodyBoldFont => FontManager.GetBold(9.5f);
        public static Font CaptionFont => FontManager.GetRegular(8.5f);

        public static Font ButtonFont => FontManager.GetBold(10f);
        public static Font ButtonLargeFont => FontManager.GetBold(11.5f);

        public static Font KpiNumberFont => FontManager.GetBold(22f);
        public static Font KpiNumberMediumFont => FontManager.GetBold(18f);

        public static Font GridHeaderFont => FontManager.GetBold(9.5f);
        public static Font GridCellFont => FontManager.GetRegular(9.5f);
        public static Font GridCellBoldFont => FontManager.GetBold(9.5f);

        #endregion

        #region Metrics & Dimensions

        public const int GridHeaderHeight = 48;
        public const int GridRowHeight = 40;
        public const int DefaultButtonHeight = 38;
        public const int SearchBoxHeight = 36;
        public const int CardPadding = 12;
        public const int CornerRadius = 8;

        #endregion
    }
}
