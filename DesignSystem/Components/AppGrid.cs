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
                Padding = new Padding(4, 0, 4, 0),
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
                Padding = new Padding(2, 0, 2, 0)
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
                Padding = new Padding(2, 0, 2, 0)
            };
        }

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
