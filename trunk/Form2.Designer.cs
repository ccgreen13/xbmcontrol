namespace XBMControl
{
    partial class VolumeControlF1
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
            this.gbVolume = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.gbVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbVolume
            // 
            this.gbVolume.Controls.Add(this.trackBar1);
            this.gbVolume.Location = new System.Drawing.Point(3, 1);
            this.gbVolume.Name = "gbVolume";
            this.gbVolume.Size = new System.Drawing.Size(79, 178);
            this.gbVolume.TabIndex = 0;
            this.gbVolume.TabStop = false;
            this.gbVolume.Text = "groupBox1";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(18, 24);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(42, 142);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // VolumeControlF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(84, 179);
            this.ControlBox = false;
            this.Controls.Add(this.gbVolume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VolumeControlF1";
            this.Text = "Form2";
            this.gbVolume.ResumeLayout(false);
            this.gbVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbVolume;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}