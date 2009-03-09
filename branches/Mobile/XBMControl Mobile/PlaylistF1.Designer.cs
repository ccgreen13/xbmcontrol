namespace XBMControl
{
    partial class PlaylistF1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaylistF1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.timerUpdateSelection = new System.Windows.Forms.Timer();
            this.lvPlaylist = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.imageListPlaylist = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItem6);
            this.menuItem1.Text = "Actions";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Play";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Remove";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Clear";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "Resync";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Main Menu";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // timerUpdateSelection
            // 
            this.timerUpdateSelection.Interval = 500;
            this.timerUpdateSelection.Tick += new System.EventHandler(this.timerUpdateSelection_Tick);
            // 
            // lvPlaylist
            // 
            this.lvPlaylist.BackColor = System.Drawing.Color.Silver;
            this.lvPlaylist.Columns.Add(this.columnHeader1);
            this.lvPlaylist.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lvPlaylist.ForeColor = System.Drawing.Color.Black;
            this.lvPlaylist.FullRowSelect = true;
            this.lvPlaylist.Location = new System.Drawing.Point(0, 3);
            this.lvPlaylist.Name = "lvPlaylist";
            this.lvPlaylist.Size = new System.Drawing.Size(240, 262);
            this.lvPlaylist.SmallImageList = this.imageListPlaylist;
            this.lvPlaylist.TabIndex = 1;
            this.lvPlaylist.View = System.Windows.Forms.View.SmallIcon;
            this.lvPlaylist.SelectedIndexChanged += new System.EventHandler(this.lvPlaylist_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "SongList";
            this.columnHeader1.Width = 240;
            this.imageListPlaylist.Images.Clear();
            this.imageListPlaylist.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageListPlaylist.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // PlaylistF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lvPlaylist);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PlaylistF1";
            this.Text = "XBMControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Timer timerUpdateSelection;
        private System.Windows.Forms.ListView lvPlaylist;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListPlaylist;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;

    }
}