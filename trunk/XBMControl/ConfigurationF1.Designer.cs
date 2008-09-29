namespace XBMControl
{
    partial class ConfigurationF1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationF1));
            this.bCancel = new System.Windows.Forms.Button();
            this.bConfirm = new System.Windows.Forms.Button();
            this.bApply = new System.Windows.Forms.Button();
            this.ip = new System.Windows.Forms.TextBox();
            this.lIpTitle = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.lUsernameTitle = new System.Windows.Forms.Label();
            this.lPasswordTitle = new System.Windows.Forms.Label();
            this.cbShowInTray = new System.Windows.Forms.CheckBox();
            this.cbShowNowPlayingBalloonTip = new System.Windows.Forms.CheckBox();
            this.cbShowPlayStatusBalloonTip = new System.Windows.Forms.CheckBox();
            this.cbShowInTaskbar = new System.Windows.Forms.CheckBox();
            this.cbShowConnectionStatusBalloonTip = new System.Windows.Forms.CheckBox();
            this.lLanguageTitle = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(382, 139);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(57, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bConfirm
            // 
            this.bConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bConfirm.Location = new System.Drawing.Point(318, 139);
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.Size = new System.Drawing.Size(58, 23);
            this.bConfirm.TabIndex = 1;
            this.bConfirm.Text = "OK";
            this.bConfirm.UseVisualStyleBackColor = true;
            this.bConfirm.Click += new System.EventHandler(this.bConfirm_Click);
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bApply.Enabled = false;
            this.bApply.Location = new System.Drawing.Point(256, 139);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(56, 23);
            this.bApply.TabIndex = 2;
            this.bApply.Text = "Apply";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // ip
            // 
            this.ip.AcceptsReturn = true;
            this.ip.Location = new System.Drawing.Point(87, 39);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(119, 21);
            this.ip.TabIndex = 5;
            this.ip.TextChanged += new System.EventHandler(this.tbIp_TextChanged);
            // 
            // lIpTitle
            // 
            this.lIpTitle.Location = new System.Drawing.Point(3, 42);
            this.lIpTitle.Name = "lIpTitle";
            this.lIpTitle.Size = new System.Drawing.Size(80, 18);
            this.lIpTitle.TabIndex = 8;
            this.lIpTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // username
            // 
            this.username.Enabled = false;
            this.username.Location = new System.Drawing.Point(87, 66);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(119, 21);
            this.username.TabIndex = 9;
            this.username.TextChanged += new System.EventHandler(this.tbUsername_TextChanged);
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(87, 93);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(119, 21);
            this.password.TabIndex = 10;
            this.password.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // lUsernameTitle
            // 
            this.lUsernameTitle.Location = new System.Drawing.Point(3, 69);
            this.lUsernameTitle.Name = "lUsernameTitle";
            this.lUsernameTitle.Size = new System.Drawing.Size(80, 18);
            this.lUsernameTitle.TabIndex = 11;
            this.lUsernameTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lPasswordTitle
            // 
            this.lPasswordTitle.Location = new System.Drawing.Point(3, 96);
            this.lPasswordTitle.Name = "lPasswordTitle";
            this.lPasswordTitle.Size = new System.Drawing.Size(80, 18);
            this.lPasswordTitle.TabIndex = 12;
            this.lPasswordTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbShowInTray
            // 
            this.cbShowInTray.AutoSize = true;
            this.cbShowInTray.Location = new System.Drawing.Point(224, 11);
            this.cbShowInTray.Name = "cbShowInTray";
            this.cbShowInTray.Size = new System.Drawing.Size(13, 12);
            this.cbShowInTray.TabIndex = 13;
            this.cbShowInTray.UseVisualStyleBackColor = true;
            this.cbShowInTray.Click += new System.EventHandler(this.cbShowInTray_Click);
            // 
            // cbShowNowPlayingBalloonTip
            // 
            this.cbShowNowPlayingBalloonTip.AutoSize = true;
            this.cbShowNowPlayingBalloonTip.Enabled = false;
            this.cbShowNowPlayingBalloonTip.Location = new System.Drawing.Point(224, 57);
            this.cbShowNowPlayingBalloonTip.Name = "cbShowNowPlayingBalloonTip";
            this.cbShowNowPlayingBalloonTip.Size = new System.Drawing.Size(13, 12);
            this.cbShowNowPlayingBalloonTip.TabIndex = 14;
            this.cbShowNowPlayingBalloonTip.UseVisualStyleBackColor = true;
            this.cbShowNowPlayingBalloonTip.Click += new System.EventHandler(this.cbShowNowPlayingBalloonTip_Click);
            // 
            // cbShowPlayStatusBalloonTip
            // 
            this.cbShowPlayStatusBalloonTip.AutoSize = true;
            this.cbShowPlayStatusBalloonTip.Enabled = false;
            this.cbShowPlayStatusBalloonTip.Location = new System.Drawing.Point(224, 80);
            this.cbShowPlayStatusBalloonTip.Name = "cbShowPlayStatusBalloonTip";
            this.cbShowPlayStatusBalloonTip.Size = new System.Drawing.Size(13, 12);
            this.cbShowPlayStatusBalloonTip.TabIndex = 15;
            this.cbShowPlayStatusBalloonTip.UseVisualStyleBackColor = true;
            this.cbShowPlayStatusBalloonTip.Click += new System.EventHandler(this.cbShowPlayStatusBalloonTips_Click);
            // 
            // cbShowInTaskbar
            // 
            this.cbShowInTaskbar.AutoSize = true;
            this.cbShowInTaskbar.Enabled = false;
            this.cbShowInTaskbar.Location = new System.Drawing.Point(224, 34);
            this.cbShowInTaskbar.Name = "cbShowInTaskbar";
            this.cbShowInTaskbar.Size = new System.Drawing.Size(13, 12);
            this.cbShowInTaskbar.TabIndex = 16;
            this.cbShowInTaskbar.UseVisualStyleBackColor = true;
            this.cbShowInTaskbar.Click += new System.EventHandler(this.cbMinimizeToTray_Click);
            // 
            // cbShowConnectionStatusBalloonTip
            // 
            this.cbShowConnectionStatusBalloonTip.AutoSize = true;
            this.cbShowConnectionStatusBalloonTip.Enabled = false;
            this.cbShowConnectionStatusBalloonTip.Location = new System.Drawing.Point(224, 103);
            this.cbShowConnectionStatusBalloonTip.Name = "cbShowConnectionStatusBalloonTip";
            this.cbShowConnectionStatusBalloonTip.Size = new System.Drawing.Size(13, 12);
            this.cbShowConnectionStatusBalloonTip.TabIndex = 17;
            this.cbShowConnectionStatusBalloonTip.UseVisualStyleBackColor = true;
            // 
            // lLanguageTitle
            // 
            this.lLanguageTitle.Location = new System.Drawing.Point(3, 15);
            this.lLanguageTitle.Name = "lLanguageTitle";
            this.lLanguageTitle.Size = new System.Drawing.Size(80, 18);
            this.lLanguageTitle.TabIndex = 18;
            this.lLanguageTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            "english",
            "dutch"});
            this.cbLanguage.Location = new System.Drawing.Point(87, 12);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 19;
            this.cbLanguage.TextChanged += new System.EventHandler(this.cbLanguage_TextChanged);
            // 
            // ConfigurationF1
            // 
            this.AcceptButton = this.bConfirm;
            this.AccessibleName = "";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.PropertyPage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(449, 174);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.lLanguageTitle);
            this.Controls.Add(this.cbShowConnectionStatusBalloonTip);
            this.Controls.Add(this.cbShowInTaskbar);
            this.Controls.Add(this.cbShowPlayStatusBalloonTip);
            this.Controls.Add(this.cbShowNowPlayingBalloonTip);
            this.Controls.Add(this.cbShowInTray);
            this.Controls.Add(this.lPasswordTitle);
            this.Controls.Add(this.lUsernameTitle);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.lIpTitle);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.bApply);
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.bCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationF1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XBMControl Configuration";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigurationF1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bConfirm;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.Label lIpTitle;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label lUsernameTitle;
        private System.Windows.Forms.Label lPasswordTitle;
        private System.Windows.Forms.CheckBox cbShowInTray;
        private System.Windows.Forms.CheckBox cbShowNowPlayingBalloonTip;
        private System.Windows.Forms.CheckBox cbShowPlayStatusBalloonTip;
        private System.Windows.Forms.CheckBox cbShowInTaskbar;
        private System.Windows.Forms.CheckBox cbShowConnectionStatusBalloonTip;
        private System.Windows.Forms.Label lLanguageTitle;
        private System.Windows.Forms.ComboBox cbLanguage;
    }
}