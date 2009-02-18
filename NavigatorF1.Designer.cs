namespace XBMControl
{
    partial class NavigatorF1
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lMainTitle = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.bRight = new System.Windows.Forms.Button();
            this.bLeft = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.pToolbar = new System.Windows.Forms.Panel();
            this.bSelect = new System.Windows.Forms.Button();
            this.bUndo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.pToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::XBMControl.Properties.Resources.XBMC;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // lMainTitle
            // 
            this.lMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lMainTitle.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lMainTitle.ForeColor = System.Drawing.Color.Silver;
            this.lMainTitle.Location = new System.Drawing.Point(23, 0);
            this.lMainTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lMainTitle.Name = "lMainTitle";
            this.lMainTitle.Size = new System.Drawing.Size(182, 13);
            this.lMainTitle.TabIndex = 7;
            this.lMainTitle.Text = "Navigator";
            this.lMainTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseMove);
            this.lMainTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseDown);
            this.lMainTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseUp);
            // 
            // pbClose
            // 
            this.pbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::XBMControl.Properties.Resources.close1;
            this.pbClose.Location = new System.Drawing.Point(206, 4);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(9, 9);
            this.pbClose.TabIndex = 8;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // bRight
            // 
            this.bRight.BackgroundImage = global::XBMControl.Properties.Resources.button_back;
            this.bRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bRight.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRight.Location = new System.Drawing.Point(132, 52);
            this.bRight.Margin = new System.Windows.Forms.Padding(0);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(38, 43);
            this.bRight.TabIndex = 9;
            this.bRight.UseVisualStyleBackColor = true;
            this.bRight.Click += new System.EventHandler(this.bRight_Click);
            // 
            // bLeft
            // 
            this.bLeft.BackgroundImage = global::XBMControl.Properties.Resources.button_forward;
            this.bLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bLeft.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLeft.Location = new System.Drawing.Point(53, 52);
            this.bLeft.Name = "bLeft";
            this.bLeft.Size = new System.Drawing.Size(38, 43);
            this.bLeft.TabIndex = 10;
            this.bLeft.UseVisualStyleBackColor = true;
            this.bLeft.Click += new System.EventHandler(this.bLeft_Click);
            // 
            // bUp
            // 
            this.bUp.BackgroundImage = global::XBMControl.Properties.Resources.button_up;
            this.bUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUp.Location = new System.Drawing.Point(89, 17);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(43, 38);
            this.bUp.TabIndex = 11;
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.BackgroundImage = global::XBMControl.Properties.Resources.button_down;
            this.bDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDown.Location = new System.Drawing.Point(89, 92);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(43, 38);
            this.bDown.TabIndex = 12;
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // pToolbar
            // 
            this.pToolbar.Controls.Add(this.pictureBox2);
            this.pToolbar.Controls.Add(this.lMainTitle);
            this.pToolbar.Controls.Add(this.pbClose);
            this.pToolbar.Location = new System.Drawing.Point(1, 1);
            this.pToolbar.Name = "pToolbar";
            this.pToolbar.Size = new System.Drawing.Size(218, 19);
            this.pToolbar.TabIndex = 13;
            this.pToolbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseMove);
            this.pToolbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseDown);
            this.pToolbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseUp);
            // 
            // bSelect
            // 
            this.bSelect.BackgroundImage = global::XBMControl.Properties.Resources.button_select;
            this.bSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSelect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bSelect.FlatAppearance.BorderSize = 0;
            this.bSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSelect.Location = new System.Drawing.Point(89, 52);
            this.bSelect.Margin = new System.Windows.Forms.Padding(0);
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(43, 43);
            this.bSelect.TabIndex = 14;
            this.bSelect.UseVisualStyleBackColor = true;
            this.bSelect.Click += new System.EventHandler(this.bSelect_Click);
            // 
            // bUndo
            // 
            this.bUndo.BackgroundImage = global::XBMControl.Properties.Resources.button_undo;
            this.bUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bUndo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUndo.Location = new System.Drawing.Point(12, 44);
            this.bUndo.Name = "bUndo";
            this.bUndo.Size = new System.Drawing.Size(31, 50);
            this.bUndo.TabIndex = 15;
            this.bUndo.UseVisualStyleBackColor = true;
            this.bUndo.Click += new System.EventHandler(this.bUndo_Click_1);
            // 
            // NavigatorF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(242)))));
            this.BackgroundImage = global::XBMControl.Properties.Resources.MainFormBackground2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(220, 146);
            this.Controls.Add(this.bUndo);
            this.Controls.Add(this.bSelect);
            this.Controls.Add(this.pToolbar);
            this.Controls.Add(this.bDown);
            this.Controls.Add(this.bUp);
            this.Controls.Add(this.bLeft);
            this.Controls.Add(this.bRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NavigatorF1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Navigator";
            this.Load += new System.EventHandler(this.NavigatorF1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.pToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lMainTitle;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Button bRight;
        private System.Windows.Forms.Button bLeft;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.Panel pToolbar;
        private System.Windows.Forms.Button bSelect;
        private System.Windows.Forms.Button bUndo;
    }
}