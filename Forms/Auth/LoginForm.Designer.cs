namespace POS
{
    partial class LoginForm
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
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblLeftVersion = new System.Windows.Forms.Label();
            this.lblFeature3 = new System.Windows.Forms.Label();
            this.lblFeature2 = new System.Windows.Forms.Label();
            this.lblFeature1 = new System.Windows.Forms.Label();
            this.lblLeftSubTitle = new System.Windows.Forms.Label();
            this.lblLeftTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblSignInSub = new System.Windows.Forms.Label();
            this.lblSignInTitle = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlRight.Controls.Add(this.lblLeftVersion);
            this.pnlRight.Controls.Add(this.lblFeature3);
            this.pnlRight.Controls.Add(this.lblFeature2);
            this.pnlRight.Controls.Add(this.lblFeature1);
            this.pnlRight.Controls.Add(this.lblLeftSubTitle);
            this.pnlRight.Controls.Add(this.lblLeftTitle);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(540, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(25, 40, 25, 20);
            this.pnlRight.Size = new System.Drawing.Size(310, 485);
            this.pnlRight.TabIndex = 0;
            // 
            // lblLeftVersion
            // 
            this.lblLeftVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLeftVersion.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblLeftVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblLeftVersion.Location = new System.Drawing.Point(25, 440);
            this.lblLeftVersion.Name = "lblLeftVersion";
            this.lblLeftVersion.Size = new System.Drawing.Size(260, 25);
            this.lblLeftVersion.TabIndex = 5;
            this.lblLeftVersion.Text = "نظام نقاط البيع v1.0 • جميع الحقوق محفوظة";
            this.lblLeftVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFeature3
            // 
            this.lblFeature3.AutoSize = true;
            this.lblFeature3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblFeature3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblFeature3.Location = new System.Drawing.Point(30, 280);
            this.lblFeature3.Name = "lblFeature3";
            this.lblFeature3.Size = new System.Drawing.Size(256, 21);
            this.lblFeature3.TabIndex = 4;
            this.lblFeature3.Text = "حماية وصلاحيات مستخدمين";
            // 
            // lblFeature2
            // 
            this.lblFeature2.AutoSize = true;
            this.lblFeature2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblFeature2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblFeature2.Location = new System.Drawing.Point(30, 235);
            this.lblFeature2.Name = "lblFeature2";
            this.lblFeature2.Size = new System.Drawing.Size(258, 21);
            this.lblFeature2.TabIndex = 3;
            this.lblFeature2.Text = "جرد المخزون وتنبيه النواقص";
            // 
            // lblFeature1
            // 
            this.lblFeature1.AutoSize = true;
            this.lblFeature1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblFeature1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblFeature1.Location = new System.Drawing.Point(30, 190);
            this.lblFeature1.Name = "lblFeature1";
            this.lblFeature1.Size = new System.Drawing.Size(256, 21);
            this.lblFeature1.TabIndex = 2;
            this.lblFeature1.Text = "كاشير وفواتير فائقة السرعة";
            // 
            // lblLeftSubTitle
            // 
            this.lblLeftSubTitle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblLeftSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblLeftSubTitle.Location = new System.Drawing.Point(15, 100);
            this.lblLeftSubTitle.Name = "lblLeftSubTitle";
            this.lblLeftSubTitle.Size = new System.Drawing.Size(280, 42);
            this.lblLeftSubTitle.TabIndex = 1;
            this.lblLeftSubTitle.Text = "إدارة متكاملة للمبيعات والمخازن والفواتير";
            this.lblLeftSubTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLeftTitle
            // 
            this.lblLeftTitle.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblLeftTitle.ForeColor = System.Drawing.Color.White;
            this.lblLeftTitle.Location = new System.Drawing.Point(15, 30);
            this.lblLeftTitle.Name = "lblLeftTitle";
            this.lblLeftTitle.Size = new System.Drawing.Size(280, 65);
            this.lblLeftTitle.TabIndex = 0;
            this.lblLeftTitle.Text = "نظام نقاط البيع";
            this.lblLeftTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlLeft.Controls.Add(this.btnExit);
            this.pnlLeft.Controls.Add(this.lblStatus);
            this.pnlLeft.Controls.Add(this.chkShowPassword);
            this.pnlLeft.Controls.Add(this.btnLogin);
            this.pnlLeft.Controls.Add(this.txtPassword);
            this.pnlLeft.Controls.Add(this.lblPassword);
            this.pnlLeft.Controls.Add(this.txtUsername);
            this.pnlLeft.Controls.Add(this.lblUsername);
            this.pnlLeft.Controls.Add(this.lblSignInSub);
            this.pnlLeft.Controls.Add(this.lblSignInTitle);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(40, 40, 40, 20);
            this.pnlLeft.Size = new System.Drawing.Size(540, 485);
            this.pnlLeft.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnExit.Location = new System.Drawing.Point(40, 436);
            this.btnExit.Name = "btnExit";
            this.btnExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExit.Size = new System.Drawing.Size(460, 38);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "إغلاق التطبيق";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatus.Location = new System.Drawing.Point(40, 332);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(460, 32);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.chkShowPassword.Location = new System.Drawing.Point(375, 300);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(135, 22);
            this.chkShowPassword.TabIndex = 7;
            this.chkShowPassword.Text = "إظهار كلمة المرور";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(40, 376);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLogin.Size = new System.Drawing.Size(460, 48);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "🔓 تسجيل الدخول";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPassword.Location = new System.Drawing.Point(40, 256);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(460, 30);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPassword.Location = new System.Drawing.Point(425, 232);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 19);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "كلمة المرور:";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtUsername.Location = new System.Drawing.Point(40, 180);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(460, 30);
            this.txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblUsername.Location = new System.Drawing.Point(415, 156);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(130, 19);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "اسم المستخدم:";
            // 
            // lblSignInSub
            // 
            this.lblSignInSub.AutoSize = true;
            this.lblSignInSub.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblSignInSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSignInSub.Location = new System.Drawing.Point(218, 95);
            this.lblSignInSub.Name = "lblSignInSub";
            this.lblSignInSub.Size = new System.Drawing.Size(320, 19);
            this.lblSignInSub.TabIndex = 1;
            this.lblSignInSub.Text = "يرجى إدخال بيانات الاعتماد للمتابعة إلى النظام";
            // 
            // lblSignInTitle
            // 
            this.lblSignInTitle.AutoSize = true;
            this.lblSignInTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblSignInTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSignInTitle.Location = new System.Drawing.Point(249, 45);
            this.lblSignInTitle.Name = "lblSignInTitle";
            this.lblSignInTitle.Size = new System.Drawing.Size(287, 41);
            this.lblSignInTitle.TabIndex = 0;
            this.lblSignInTitle.Text = "أهلاً بك مجدداً 👋";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(850, 485);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نظام نقاط البيع - تسجيل الدخول";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblLeftTitle;
        private System.Windows.Forms.Label lblLeftSubTitle;
        private System.Windows.Forms.Label lblFeature3;
        private System.Windows.Forms.Label lblFeature2;
        private System.Windows.Forms.Label lblFeature1;
        private System.Windows.Forms.Label lblLeftVersion;
        private System.Windows.Forms.Label lblSignInTitle;
        private System.Windows.Forms.Label lblSignInSub;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnExit;
    }
}
