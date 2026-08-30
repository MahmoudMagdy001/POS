using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System DataGridView with double-buffering, modern styling,
    /// Cairo typography, alternating row colors, and RTL optimization.
    /// </summary>
    public class AppGrid : DataGridView
    {
        public AppGrid()
        {
            EnableDoubleBuffering();
            ApplyDesignSystemStyles();
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                typeof(DataGridView).InvokeMember(
                    "DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null,
                    this,
                    new object[] { true });
            }
            catch { }
        }

        public void ApplyDesignSystemStyles()
        {
            // General Grid Setup
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BackgroundColor = UIColors.Surface;
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            GridColor = UIColors.Border;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = false;
            AutoGenerateColumns = false;
            RightToLeft = RightToLeft.Yes;
            ScrollBars = ScrollBars.Both;

            // Header Style
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersHeight = UISpacing.GridHeaderHeight;

            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UIColors.SurfaceAlt,
                ForeColor = UIColors.TextPrimary,
                Font = UITypography.GridHeader,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(6, 4, 6, 4),
                WrapMode = DataGridViewTriState.False
            };

            // Default Row Style
            RowTemplate.Height = UISpacing.GridRowHeight;
            DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UIColors.Surface,
                ForeColor = UIColors.TextPrimary,
                Font = UITypography.GridCell,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = UIColors.PrimaryLight,
                SelectionForeColor = UIColors.PrimaryDark,
                Padding = new Padding(6, 2, 6, 2)
            };

            // Alternating Row Style
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UIColors.AppBackground,
                ForeColor = UIColors.TextPrimary,
                Font = UITypography.GridCell,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = UIColors.PrimaryLight,
                SelectionForeColor = UIColors.PrimaryDark,
                Padding = new Padding(6, 2, 6, 2)
            };
        }

        public void HideColumn(string colName) => GridExtensions.HideColumn(this, colName);
        public DataGridViewColumn ConfigureTextColumn(string colName, string headerText, int fillWeight = 100, int minWidth = 100) => GridExtensions.ConfigureTextColumn(this, colName, headerText, fillWeight, minWidth);
        public DataGridViewColumn ConfigureCenterColumn(string colName, string headerText, int fillWeight = 80, int minWidth = 80, string format = null) => GridExtensions.ConfigureCenterColumn(this, colName, headerText, fillWeight, minWidth, format);
        public DataGridViewColumn ConfigureNumericColumn(string colName, string headerText, int fillWeight = 60, int minWidth = 75, string format = "N0") => GridExtensions.ConfigureNumericColumn(this, colName, headerText, fillWeight, minWidth, format);
        public DataGridViewColumn ConfigureCurrencyColumn(string colName, string headerText, int fillWeight = 75, int minWidth = 85, string format = "N2") => GridExtensions.ConfigureCurrencyColumn(this, colName, headerText, fillWeight, minWidth, format);
        public DataGridViewColumn ConfigureDateColumn(string colName, string headerText, int fillWeight = 100, int minWidth = 120, string format = "yyyy-MM-dd HH:mm") => GridExtensions.ConfigureDateColumn(this, colName, headerText, fillWeight, minWidth, format);
        public DataGridViewColumn ConfigureIdColumn(string colName, string headerText, int fillWeight = 65, int minWidth = 80, string format = "D5") => GridExtensions.ConfigureIdColumn(this, colName, headerText, fillWeight, minWidth, format);
        public DataGridViewButtonColumn ConfigureButtonColumn(string colName, string headerText, string buttonText, int fillWeight = 50, int minWidth = 70, Color? textColor = null) => GridExtensions.ConfigureButtonColumn(this, colName, headerText, buttonText, fillWeight, minWidth, textColor);

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            if (e.Column != null && e.Column.MinimumWidth < 50)
            {
                e.Column.MinimumWidth = 50;
            }
        }
    }
}
