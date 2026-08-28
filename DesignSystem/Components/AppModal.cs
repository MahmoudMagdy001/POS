using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using POS.DesignSystem.Helpers;
using POS.DesignSystem.Tokens;

namespace POS.DesignSystem.Components
{
    /// <summary>
    /// Standardized Design System Base Modal Dialog Form.
    /// Provides consistent header, footer actions, smooth drag movement, and rounded border.
    /// </summary>
    public class AppModal : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private Panel _headerPanel;
        private Label _lblModalTitle;
        private Label _btnClose;
        private Panel _bodyPanel;
        private Panel _footerPanel;

        public AppModal()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = UIColors.AppBackground;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            Size = new Size(500, 400);

            InitializeComponents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CenterToScreen();
        }

        private void InitializeComponents()
        {
            // Header Panel
            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = UIColors.Surface,
                Padding = new Padding(16, 0, 16, 0)
            };
            _headerPanel.MouseDown += HeaderPanel_MouseDown;

            _lblModalTitle = new Label
            {
                Text = "عنوان النافذة",
                Font = UITypography.SectionHeader,
                ForeColor = UIColors.TextPrimary,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            _lblModalTitle.MouseDown += HeaderPanel_MouseDown;

            _btnClose = new Label
            {
                Text = "✕",
                Font = new Font("Arial", 11f, FontStyle.Bold),
                ForeColor = UIColors.TextMuted,
                Dock = DockStyle.Left, // In RTL, Left is the close side
                Width = 32,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            _btnClose.Click += (s, e) => Close();
            _btnClose.MouseEnter += (s, e) => _btnClose.ForeColor = UIColors.Danger;
            _btnClose.MouseLeave += (s, e) => _btnClose.ForeColor = UIColors.TextMuted;

            _headerPanel.Controls.Add(_lblModalTitle);
            _headerPanel.Controls.Add(_btnClose);

            // Footer Panel
            _footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = UIColors.SurfaceAlt,
                Padding = new Padding(16, 12, 16, 12)
            };

            // Body Panel
            _bodyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIColors.AppBackground,
                Padding = new Padding(16)
            };

            Controls.Add(_bodyPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);
        }

        private void HeaderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        [Category("Design System")]
        [DefaultValue("عنوان النافذة")]
        public string ModalTitle
        {
            get => _lblModalTitle.Text;
            set => _lblModalTitle.Text = value;
        }

        [Category("Design System")]
        public Panel Body => _bodyPanel;

        [Category("Design System")]
        public Panel Footer => _footerPanel;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw modal outer border
            using (Pen pen = new Pen(UIColors.BorderDark, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}
