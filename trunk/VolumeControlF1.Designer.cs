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
            this.tbVolumeSysTray = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeSysTray)).BeginInit();
            this.SuspendLayout();
            // 
            // tbVolumeSysTray
            // 
            this.tbVolumeSysTray.Location = new System.Drawing.Point(2, 2);
            this.tbVolumeSysTray.Maximum = 100;
            this.tbVolumeSysTray.Name = "tbVolumeSysTray";
            this.tbVolumeSysTray.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbVolumeSysTray.Size = new System.Drawing.Size(42, 134);
            this.tbVolumeSysTray.TabIndex = 2;
            this.tbVolumeSysTray.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbVolumeSysTray.MouseLeave += new System.EventHandler(this.tbVolumeSysTray_MouseLeave);
            this.tbVolumeSysTray.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            this.tbVolumeSysTray.LostFocus += new System.EventHandler(this.tbVolumeSysTray_LostFocus);
            this.tbVolumeSysTray.MouseHover += new System.EventHandler(this.tbVolumeSysTray_MouseHover);
            // 
            // VolumeControlF1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(47, 139);
            this.ControlBox = false;
            this.Controls.Add(this.tbVolumeSysTray);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VolumeControlF1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.MouseLeave += new System.EventHandler(this.VolumeControlF1_MouseLeave);
            this.MouseHover += new System.EventHandler(this.VolumeControlF1_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeSysTray)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbVolumeSysTray;

    }
}