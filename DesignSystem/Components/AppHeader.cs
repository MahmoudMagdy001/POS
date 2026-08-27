using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System Page Header with title, subtitle, and action controls container.
    /// </summary>
    public class AppHeader : UserControl
    {
        private Label _lblTitle;
        private Label _lblSubtitle;
        private FlowLayoutPanel _actionPanel;

        public AppHeader()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Dock = DockStyle.Top;
            Height = UISpacing.HeaderHeight;
            BackColor = UIColors.Surface;
            Padding = new Padding(UISpacing.SpaceLG, UISpacing.SpaceSM, UISpacing.SpaceLG, UISpacing.SpaceSM);
            RightToLeft = RightToLeft.Yes;

            _lblTitle = new Label
            {
                Text = "عنوان الصفحة",
                Font = UITypography.SectionHeader,
                ForeColor = UIColors.TextPrimary,
                AutoSize = true,
                Location = new Point(0, 4)
            };

            _lblSubtitle = new Label
            {
                Text = "وصف مختصر للوظيفة الحالية",
                Font = UITypography.Caption,
                ForeColor = UIColors.TextMuted,
                AutoSize = true,
                Location = new Point(0, 28)
            };

            _actionPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Left, // In RTL, Left is the opposing side
                BackColor = Color.Transparent,
                WrapContents = false
            };

            Panel textContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            textContainer.Controls.Add(_lblTitle);
            textContainer.Controls.Add(_lblSubtitle);

            Controls.Add(textContainer);
            Controls.Add(_actionPanel);
        }

        [Category("Design System")]
        [DefaultValue("عنوان الصفحة")]
        public string Title
        {
            get => _lblTitle.Text;
            set => _lblTitle.Text = value;
        }

        [Category("Design System")]
        [DefaultValue("وصف مختصر للوظيفة الحالية")]
        public string Subtitle
        {
            get => _lblSubtitle.Text;
            set
            {
                _lblSubtitle.Text = value;
                _lblSubtitle.Visible = !string.IsNullOrEmpty(value);
            }
        }

        [Category("Design System")]
        public FlowLayoutPanel Actions => _actionPanel;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw bottom divider line
            using (Pen pen = new Pen(UIColors.Border, 1))
            {
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);
            }
        }
    }
}
