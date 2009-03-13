namespace XBMControl
{
    partial class MediaBrowserF1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuPlay = new System.Windows.Forms.MenuItem();
            this.menuEnqueue = new System.Windows.Forms.MenuItem();
            this.tvMediaShares = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuPlay);
            this.mainMenu1.MenuItems.Add(this.menuEnqueue);
            // 
            // menuPlay
            // 
            this.menuPlay.Text = "Enqueue";
            this.menuPlay.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuEnqueue
            // 
            this.menuEnqueue.Text = "Main Menu";
            this.menuEnqueue.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // tvMediaShares
            // 
            this.tvMediaShares.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMediaShares.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tvMediaShares.ForeColor = System.Drawing.Color.Black;
            this.tvMediaShares.Location = new System.Drawing.Point(0, 0);
            this.tvMediaShares.Name = "tvMediaShares";
            this.tvMediaShares.Size = new System.Drawing.Size(240, 268);
            this.tvMediaShares.TabIndex = 8;
            this.tvMediaShares.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMediaShares_AfterSelect);
            // 
            // MediaBrowserF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tvMediaShares);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "MediaBrowserF1";
            this.Text = "XBMControl";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MediaBrowserF1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMediaShares;
        private System.Windows.Forms.MenuItem menuPlay;
        private System.Windows.Forms.MenuItem menuEnqueue;
    }
}