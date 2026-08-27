using System.Drawing;

namespace POS.DesignSystem.Tokens
{
    /// <summary>
    /// Design Tokens: Standard Color Palette
    /// </summary>
    public static class UIColors
    {
        #region Primary & Brand Palette
        // Primary Brand Colors (Modern Sky / Indigo Blue)
        public static readonly Color Primary = Color.FromArgb(14, 165, 233);       // #0EA5E9
        public static readonly Color PrimaryHover = Color.FromArgb(2, 132, 199);   // #0284C7
        public static readonly Color PrimaryActive = Color.FromArgb(3, 105, 161);  // #0369A1
        public static readonly Color PrimaryDark = Color.FromArgb(3, 105, 161);    // #0369A1
        public static readonly Color PrimaryLight = Color.FromArgb(224, 242, 254); // #E0F2FE
        #endregion

        #region Secondary & Slate Palette
        public static readonly Color Secondary = Color.FromArgb(71, 85, 105);      // #475569
        public static readonly Color SecondaryHover = Color.FromArgb(51, 65, 85);  // #334155
        public static readonly Color SecondaryActive = Color.FromArgb(30, 41, 59); // #1E293B
        public static readonly Color SecondaryLight = Color.FromArgb(241, 245, 249);// #F1F5F9
        #endregion

        #region Surface & Neutral Backgrounds
        public static readonly Color AppBackground = Color.FromArgb(248, 250, 252);// #F8FAFC
        public static readonly Color Surface = Color.FromArgb(255, 255, 255);       // #FFFFFF
        public static readonly Color SurfaceAlt = Color.FromArgb(241, 245, 249);   // #F1F5F9
        public static readonly Color SurfaceHover = Color.FromArgb(243, 244, 246); // #F3F4F6
        #endregion

        #region Borders & Dividers
        public static readonly Color Border = Color.FromArgb(226, 232, 240);       // #E2E8F0
        public static readonly Color BorderDark = Color.FromArgb(203, 213, 225);   // #CBD5E1
        public static readonly Color BorderFocus = Color.FromArgb(14, 165, 233);  // #0EA5E9
        #endregion

        #region Typography
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);     // #0F172A
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);  // #475569
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);    // #64748B
        public static readonly Color TextLight = Color.FromArgb(255, 255, 255);    // #FFFFFF
        public static readonly Color TextPlaceholder = Color.FromArgb(148, 163, 184);// #94A3B8
        #endregion

        #region Semantic Status Colors
        // Success
        public static readonly Color Success = Color.FromArgb(22, 163, 74);        // #16A34A
        public static readonly Color SuccessHover = Color.FromArgb(21, 128, 61);   // #15803D
        public static readonly Color SuccessLight = Color.FromArgb(220, 252, 231); // #DCFCE7
        public static readonly Color SuccessDark = Color.FromArgb(20, 83, 45);     // #14532D

        // Danger / Error
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);         // #DC2626
        public static readonly Color DangerHover = Color.FromArgb(185, 28, 28);    // #B91C1C
        public static readonly Color DangerLight = Color.FromArgb(254, 226, 226);  // #FEE2E2
        public static readonly Color DangerDark = Color.FromArgb(127, 29, 29);     // #7F1D1D

        // Warning
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);        // #D97706
        public static readonly Color WarningHover = Color.FromArgb(180, 83, 9);    // #B45309
        public static readonly Color WarningLight = Color.FromArgb(254, 243, 199); // #FEF3C7
        public static readonly Color WarningDark = Color.FromArgb(120, 53, 15);    // #78350F

        // Info
        public static readonly Color Info = Color.FromArgb(37, 99, 235);           // #2563EB
        public static readonly Color InfoHover = Color.FromArgb(29, 78, 216);      // #1D4ED8
        public static readonly Color InfoLight = Color.FromArgb(219, 234, 254);    // #DBEAFE
        public static readonly Color InfoDark = Color.FromArgb(30, 64, 175);       // #1E40AF

        // Purple / Accent
        public static readonly Color Purple = Color.FromArgb(124, 58, 237);        // #7C3AED
        public static readonly Color PurpleHover = Color.FromArgb(109, 40, 217);   // #6D28D9
        public static readonly Color PurpleLight = Color.FromArgb(243, 232, 255);  // #F3E8FF
        public static readonly Color PurpleDark = Color.FromArgb(91, 33, 182);     // #5B21B6
        #endregion

        #region Sidebar & Navigation
        public static readonly Color SidebarBackground = Color.FromArgb(15, 23, 42); // #0F172A
        public static readonly Color SidebarActiveItem = Color.FromArgb(30, 41, 59); // #1E293B
        public static readonly Color SidebarHoverItem = Color.FromArgb(24, 33, 47);  // #18212F
        public static readonly Color SidebarText = Color.FromArgb(148, 163, 184);   // #94A3B8
        public static readonly Color SidebarActiveText = Color.White;
        #endregion
    }
}
