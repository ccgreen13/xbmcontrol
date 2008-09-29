namespace WindowsFormsApplication1
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
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(136, 109);
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
            this.bConfirm.Location = new System.Drawing.Point(72, 109);
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
            this.bApply.Location = new System.Drawing.Point(10, 109);
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
            this.ip.Location = new System.Drawing.Point(70, 10);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(119, 21);
            this.ip.TabIndex = 5;
            this.ip.TextChanged += new System.EventHandler(this.tbIp_TextChanged);
            // 
            // lIpTitle
            // 
            this.lIpTitle.AutoSize = true;
            this.lIpTitle.Location = new System.Drawing.Point(8, 13);
            this.lIpTitle.Name = "lIpTitle";
            this.lIpTitle.Size = new System.Drawing.Size(56, 13);
            this.lIpTitle.TabIndex = 8;
            this.lIpTitle.Text = "ip address";
            // 
            // username
            // 
            this.username.Enabled = false;
            this.username.Location = new System.Drawing.Point(70, 37);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(119, 21);
            this.username.TabIndex = 9;
            this.username.TextChanged += new System.EventHandler(this.tbUsername_TextChanged);
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(70, 64);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(119, 21);
            this.password.TabIndex = 10;
            this.password.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // lUsernameTitle
            // 
            this.lUsernameTitle.AutoSize = true;
            this.lUsernameTitle.Location = new System.Drawing.Point(10, 40);
            this.lUsernameTitle.Name = "lUsernameTitle";
            this.lUsernameTitle.Size = new System.Drawing.Size(54, 13);
            this.lUsernameTitle.TabIndex = 11;
            this.lUsernameTitle.Text = "username";
            // 
            // lPasswordTitle
            // 
            this.lPasswordTitle.AutoSize = true;
            this.lPasswordTitle.Location = new System.Drawing.Point(11, 67);
            this.lPasswordTitle.Name = "lPasswordTitle";
            this.lPasswordTitle.Size = new System.Drawing.Size(53, 13);
            this.lPasswordTitle.TabIndex = 12;
            this.lPasswordTitle.Text = "password";
            // 
            // ConfigurationF1
            // 
            this.AcceptButton = this.bConfirm;
            this.AccessibleName = "";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.PropertyPage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(203, 144);
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
    }
}