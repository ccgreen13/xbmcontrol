namespace XBMControl.XBMC.Library
{
    partial class Form1
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
            this.pbFullSizeImage.Location = new System.Drawing.Point(27, 26);
            this.pbFullSizeImage.Name = "pbFullSizeImage";
            this.pbFullSizeImage.Size = new System.Drawing.Size(100, 50);
            this.pbFullSizeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFullSizeImage.TabIndex = 0;
            this.pbFullSizeImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.pbFullSizeImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbFullSizeImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFullSizeImage;
    }
}