namespace POS
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlNavContainer = new System.Windows.Forms.Panel();
            this.btnNavSettings = new System.Windows.Forms.Button();
            this.btnNavUsers = new System.Windows.Forms.Button();
            this.btnNavSales = new System.Windows.Forms.Button();
            this.btnNavPurchases = new System.Windows.Forms.Button();
            this.btnNavProducts = new System.Windows.Forms.Button();
            this.btnNavPOS = new System.Windows.Forms.Button();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.pnlUserBadge = new System.Windows.Forms.Panel();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserAvatar = new System.Windows.Forms.Label();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblAppBrand = new System.Windows.Forms.Label();
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblCurrentSectionTitle = new System.Windows.Forms.Label();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlNavContainer.SuspendLayout();
            this.pnlUserBadge.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.pnlTopHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlSidebar.Controls.Add(this.pnlNavContainer);
            this.pnlSidebar.Controls.Add(this.pnlUserBadge);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(1000, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlSidebar.Size = new System.Drawing.Size(280, 780);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlNavContainer
            // 
            this.pnlNavContainer.Controls.Add(this.btnNavSettings);
            this.pnlNavContainer.Controls.Add(this.btnNavUsers);
            this.pnlNavContainer.Controls.Add(this.btnNavSales);
            this.pnlNavContainer.Controls.Add(this.btnNavPurchases);
            this.pnlNavContainer.Controls.Add(this.btnNavProducts);
            this.pnlNavContainer.Controls.Add(this.btnNavPOS);
            this.pnlNavContainer.Controls.Add(this.btnNavDashboard);
            this.pnlNavContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlNavContainer.Name = "pnlNavContainer";
            this.pnlNavContainer.Padding = new System.Windows.Forms.Padding(12, 15, 12, 15);
            this.pnlNavContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlNavContainer.Size = new System.Drawing.Size(280, 620);
            this.pnlNavContainer.TabIndex = 2;
            // 
            // btnNavSettings
            // 
            this.btnNavSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnNavSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavSettings.FlatAppearance.BorderSize = 0;
            this.btnNavSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavSettings.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavSettings.Location = new System.Drawing.Point(12, 303);
            this.btnNavSettings.Name = "btnNavSettings";
            this.btnNavSettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavSettings.Size = new System.Drawing.Size(256, 48);
            this.btnNavSettings.TabIndex = 6;
            this.btnNavSettings.Text = "⚙️  إعدادات النظام";
            this.btnNavSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavSettings.UseVisualStyleBackColor = false;
            this.btnNavSettings.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavUsers
            // 
            this.btnNavUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnNavUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavUsers.FlatAppearance.BorderSize = 0;
            this.btnNavUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUsers.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavUsers.Location = new System.Drawing.Point(12, 255);
            this.btnNavUsers.Name = "btnNavUsers";
            this.btnNavUsers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavUsers.Size = new System.Drawing.Size(256, 48);
            this.btnNavUsers.TabIndex = 5;
            this.btnNavUsers.Text = "👥  المستخدمين والموظفين";
            this.btnNavUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavUsers.UseVisualStyleBackColor = false;
            this.btnNavUsers.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavSales
            // 
            this.btnNavSales.BackColor = System.Drawing.Color.Transparent;
            this.btnNavSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavSales.FlatAppearance.BorderSize = 0;
            this.btnNavSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavSales.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavSales.Location = new System.Drawing.Point(12, 207);
            this.btnNavSales.Name = "btnNavSales";
            this.btnNavSales.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavSales.Size = new System.Drawing.Size(256, 48);
            this.btnNavSales.TabIndex = 4;
            this.btnNavSales.Text = "🧾  فواتير وسجل المبيعات";
            this.btnNavSales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavSales.UseVisualStyleBackColor = false;
            this.btnNavSales.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavPurchases
            // 
            this.btnNavPurchases.BackColor = System.Drawing.Color.Transparent;
            this.btnNavPurchases.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPurchases.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPurchases.FlatAppearance.BorderSize = 0;
            this.btnNavPurchases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPurchases.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavPurchases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavPurchases.Location = new System.Drawing.Point(12, 159);
            this.btnNavPurchases.Name = "btnNavPurchases";
            this.btnNavPurchases.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavPurchases.Size = new System.Drawing.Size(256, 48);
            this.btnNavPurchases.TabIndex = 3;
            this.btnNavPurchases.Text = "📥  فواتير المشتريات";
            this.btnNavPurchases.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavPurchases.UseVisualStyleBackColor = false;
            this.btnNavPurchases.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavProducts
            // 
            this.btnNavProducts.BackColor = System.Drawing.Color.Transparent;
            this.btnNavProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavProducts.FlatAppearance.BorderSize = 0;
            this.btnNavProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavProducts.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavProducts.Location = new System.Drawing.Point(12, 111);
            this.btnNavProducts.Name = "btnNavProducts";
            this.btnNavProducts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavProducts.Size = new System.Drawing.Size(256, 48);
            this.btnNavProducts.TabIndex = 2;
            this.btnNavProducts.Text = "📦  المنتجات والمخزون";
            this.btnNavProducts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavProducts.UseVisualStyleBackColor = false;
            this.btnNavProducts.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavPOS
            // 
            this.btnNavPOS.BackColor = System.Drawing.Color.Transparent;
            this.btnNavPOS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPOS.FlatAppearance.BorderSize = 0;
            this.btnNavPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPOS.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavPOS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavPOS.Location = new System.Drawing.Point(12, 63);
            this.btnNavPOS.Name = "btnNavPOS";
            this.btnNavPOS.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavPOS.Size = new System.Drawing.Size(256, 48);
            this.btnNavPOS.TabIndex = 1;
            this.btnNavPOS.Text = "🛒  شاشة الكاشير (POS)";
            this.btnNavPOS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavPOS.UseVisualStyleBackColor = false;
            this.btnNavPOS.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNavDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.FlatAppearance.BorderSize = 0;
            this.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDashboard.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnNavDashboard.ForeColor = System.Drawing.Color.White;
            this.btnNavDashboard.Location = new System.Drawing.Point(12, 15);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNavDashboard.Size = new System.Drawing.Size(256, 48);
            this.btnNavDashboard.TabIndex = 0;
            this.btnNavDashboard.Text = "📊  لوحة التحكم العامة";
            this.btnNavDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // pnlUserBadge
            // 
            this.pnlUserBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(30)))));
            this.pnlUserBadge.Controls.Add(this.lblUserRole);
            this.pnlUserBadge.Controls.Add(this.lblUserName);
            this.pnlUserBadge.Controls.Add(this.lblUserAvatar);
            this.pnlUserBadge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserBadge.Location = new System.Drawing.Point(0, 700);
            this.pnlUserBadge.Name = "pnlUserBadge";
            this.pnlUserBadge.Padding = new System.Windows.Forms.Padding(12);
            this.pnlUserBadge.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlUserBadge.Size = new System.Drawing.Size(280, 80);
            this.pnlUserBadge.TabIndex = 1;
            // 
            // lblUserRole
            // 
            this.lblUserRole.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblUserRole.Location = new System.Drawing.Point(15, 44);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUserRole.Size = new System.Drawing.Size(200, 22);
            this.lblUserRole.TabIndex = 2;
            this.lblUserRole.Text = "مدير النظام";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(15, 18);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUserName.Size = new System.Drawing.Size(200, 24);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "مدير النظام العام";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserAvatar
            // 
            this.lblUserAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblUserAvatar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserAvatar.ForeColor = System.Drawing.Color.White;
            this.lblUserAvatar.Location = new System.Drawing.Point(225, 20);
            this.lblUserAvatar.Name = "lblUserAvatar";
            this.lblUserAvatar.Size = new System.Drawing.Size(40, 40);
            this.lblUserAvatar.TabIndex = 0;
            this.lblUserAvatar.Text = "م";
            this.lblUserAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBrand
            // 
            this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(15)))), ((int)(((byte)(30)))));
            this.pnlBrand.Controls.Add(this.lblAppSubtitle);
            this.pnlBrand.Controls.Add(this.lblAppBrand);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.pnlBrand.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlBrand.Size = new System.Drawing.Size(280, 80);
            this.pnlBrand.TabIndex = 0;
            // 
            // lblAppSubtitle
            // 
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAppSubtitle.Location = new System.Drawing.Point(50, 50);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(197, 17);
            this.lblAppSubtitle.TabIndex = 1;
            this.lblAppSubtitle.Text = "إدارة المبيعات والمخازن والتحصيل";
            // 
            // lblAppBrand
            // 
            this.lblAppBrand.AutoSize = true;
            this.lblAppBrand.Font = new System.Drawing.Font("Tahoma", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblAppBrand.ForeColor = System.Drawing.Color.White;
            this.lblAppBrand.Location = new System.Drawing.Point(34, 16);
            this.lblAppBrand.Name = "lblAppBrand";
            this.lblAppBrand.Size = new System.Drawing.Size(221, 28);
            this.lblAppBrand.TabIndex = 0;
            this.lblAppBrand.Text = "🛒 كاشير ونقاط بيع";
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.White;
            this.pnlTopHeader.Controls.Add(this.lblCurrentTime);
            this.pnlTopHeader.Controls.Add(this.btnLogout);
            this.pnlTopHeader.Controls.Add(this.lblCurrentSectionTitle);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlTopHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlTopHeader.TabIndex = 1;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCurrentTime.Location = new System.Drawing.Point(217, 16);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(280, 28);
            this.lblCurrentTime.TabIndex = 2;
            this.lblCurrentTime.Text = "2026-08-25 16:00:00";
            this.lblCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLogout.Location = new System.Drawing.Point(20, 14);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(115, 32);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "🚪 خروج";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblCurrentSectionTitle
            // 
            this.lblCurrentSectionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentSectionTitle.AutoSize = true;
            this.lblCurrentSectionTitle.Font = new System.Drawing.Font("Tahoma", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblCurrentSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCurrentSectionTitle.Location = new System.Drawing.Point(790, 14);
            this.lblCurrentSectionTitle.Name = "lblCurrentSectionTitle";
            this.lblCurrentSectionTitle.Size = new System.Drawing.Size(198, 25);
            this.lblCurrentSectionTitle.TabIndex = 0;
            this.lblCurrentSectionTitle.Text = "لوحة التحكم العامة";
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 60);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1000, 720);
            this.pnlMainContent.TabIndex = 2;
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 780);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlTopHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نظام نقاط البيع وإدارة المخزون (POS)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlNavContainer.ResumeLayout(false);
            this.pnlUserBadge.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblAppBrand;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Panel pnlUserBadge;
        private System.Windows.Forms.Label lblUserAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Panel pnlNavContainer;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavPOS;
        private System.Windows.Forms.Button btnNavSales;
        private System.Windows.Forms.Button btnNavProducts;
        private System.Windows.Forms.Button btnNavPurchases;
        private System.Windows.Forms.Button btnNavUsers;
        private System.Windows.Forms.Button btnNavSettings;
        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblCurrentSectionTitle;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Timer timerClock;
    }
}
