using System.Drawing;
using POS.DesignSystem.Tokens;

namespace POS
{
    /// <summary>
    /// Centralized Design System tokens and facade for colors, typography,
    /// and spacing across the entire application.
    /// </summary>
    public static class UITheme
    {
        #region Colors - Brand & Core Palette
        public static Color Primary => UIColors.Primary;
        public static Color PrimaryHover => UIColors.PrimaryHover;
        public static Color PrimaryDark => UIColors.PrimaryActive;
        public static Color PrimaryLight => UIColors.PrimaryLight;

        public static Color Secondary => UIColors.Secondary;
        public static Color SecondaryHover => UIColors.SecondaryHover;
        public static Color SecondaryLight => UIColors.SecondaryLight;

        public static Color AppBackground => UIColors.AppBackground;
        public static Color Surface => UIColors.Surface;
        public static Color SurfaceAlt => UIColors.SurfaceAlt;

        public static Color Border => UIColors.Border;
        public static Color BorderDark => UIColors.BorderDark;

        public static Color TextPrimary => UIColors.TextPrimary;
        public static Color TextSecondary => UIColors.TextSecondary;
        public static Color TextMuted => UIColors.TextMuted;
        public static Color TextLight => UIColors.TextLight;

        public static Color Success => UIColors.Success;
        public static Color SuccessLight => UIColors.SuccessLight;
        public static Color SuccessDark => UIColors.SuccessDark;

        public static Color Danger => UIColors.Danger;
        public static Color DangerLight => UIColors.DangerLight;
        public static Color DangerDark => UIColors.DangerDark;

        public static Color Warning => UIColors.Warning;
        public static Color WarningLight => UIColors.WarningLight;
        public static Color WarningDark => UIColors.WarningDark;

        public static Color Info => UIColors.Info;
        public static Color InfoLight => UIColors.InfoLight;

        public static Color Purple => UIColors.Purple;
        public static Color PurpleLight => UIColors.PurpleLight;

        public static Color SidebarBackground => UIColors.SidebarBackground;
        public static Color SidebarActiveItem => UIColors.SidebarActiveItem;
        public static Color SidebarText => UIColors.SidebarText;
        public static Color SidebarActiveText => UIColors.SidebarActiveText;
        #endregion

        #region Typography Fonts
        public static Font AppTitleFont => UITypography.AppTitle;
        public static Font SectionHeaderFont => UITypography.SectionHeader;
        public static Font CardTitleFont => UITypography.CardTitle;
        public static Font SubtitleFont => UITypography.Subtitle;

        public static Font BodyFont => UITypography.Body;
        public static Font BodyBoldFont => UITypography.BodyBold;
        public static Font CaptionFont => UITypography.Caption;

        public static Font ButtonFont => UITypography.Button;
        public static Font ButtonLargeFont => UITypography.ButtonLarge;

        public static Font KpiNumberFont => UITypography.KpiNumberLarge;
        public static Font KpiNumberMediumFont => UITypography.KpiNumberMedium;

        public static Font GridHeaderFont => UITypography.GridHeader;
        public static Font GridCellFont => UITypography.GridCell;
        public static Font GridCellBoldFont => UITypography.GridCellBold;
        #endregion

        #region Metrics & Dimensions
        public const int GridHeaderHeight = UISpacing.GridHeaderHeight;
        public const int GridRowHeight = UISpacing.GridRowHeight;
        public const int DefaultButtonHeight = UISpacing.ButtonHeightDefault;
        public const int SearchBoxHeight = UISpacing.SearchBoxHeight;
        public const int CardPadding = UISpacing.SpaceLG;
        public const int CornerRadius = UISpacing.RadiusMedium;
        #endregion
    }
}
