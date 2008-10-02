namespace XBMControl
{
    partial class FullSizeImageF1
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
            this.pbFullSizeImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFullSizeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFullSizeImage
            // 
            this.pbFullSizeImage.ErrorImage = global::XBMControl.Properties.Resources.XBMClogo;
            this.pbFullSizeImage.InitialImage = global::XBMControl.Properties.Resources.XBMClogo;
            this.pbFullSizeImage.Location = new System.Drawing.Point(0, 0);
            this.pbFullSizeImage.Name = "pbFullSizeImage";
            this.pbFullSizeImage.Size = new System.Drawing.Size(2, 2);
            this.pbFullSizeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFullSizeImage.TabIndex = 0;
            this.pbFullSizeImage.TabStop = false;
            this.pbFullSizeImage.MouseLeave += new System.EventHandler(this.pbFullSizeImage_MouseLeave);
            this.pbFullSizeImage.Click += new System.EventHandler(this.pbFullSizeImage_Click);
            this.pbFullSizeImage.MouseEnter += new System.EventHandler(this.pbFullSizeImage_MouseEnter);
            // 
            // FullSizeImageF1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(0, 0);
            this.ControlBox = false;
            this.Controls.Add(this.pbFullSizeImage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FullSizeImageF1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FullSizeImageF1_FormClosed);
            this.Click += new System.EventHandler(this.pbFullSizeImage_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbFullSizeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFullSizeImage;
    }
}