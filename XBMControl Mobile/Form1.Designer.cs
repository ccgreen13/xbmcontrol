namespace XBMControl
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.updateTimer = new System.Windows.Forms.Timer();
            this.lTimePlayed = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lSamplerate = new System.Windows.Forms.Label();
            this.lSamplerateTitle = new System.Windows.Forms.Label();
            this.lBitrate = new System.Windows.Forms.Label();
            this.lBitrateTitle = new System.Windows.Forms.Label();
            this.lConnected = new System.Windows.Forms.Label();
            this.tbProgress = new System.Windows.Forms.TrackBar();
            this.lbPlaylistSize = new System.Windows.Forms.Label();
            this.lArtistSong = new System.Windows.Forms.Label();
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
            this.menuItem1.Text = "Menu";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Playlist";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Media Browser";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Navigator";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "Settings";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // lTimePlayed
            // 
            this.lTimePlayed.BackColor = System.Drawing.Color.DarkGray;
            this.lTimePlayed.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold);
            this.lTimePlayed.ForeColor = System.Drawing.Color.Black;
            this.lTimePlayed.Location = new System.Drawing.Point(3, 0);
            this.lTimePlayed.Name = "lTimePlayed";
            this.lTimePlayed.Size = new System.Drawing.Size(136, 66);
            this.lTimePlayed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbVolume
            // 
            this.tbVolume.Enabled = false;
            this.tbVolume.LargeChange = 2;
            this.tbVolume.Location = new System.Drawing.Point(147, 114);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(77, 18);
            this.tbVolume.TabIndex = 23;
            this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // lSamplerate
            // 
            this.lSamplerate.BackColor = System.Drawing.Color.DarkGray;
            this.lSamplerate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lSamplerate.ForeColor = System.Drawing.Color.Black;
            this.lSamplerate.Location = new System.Drawing.Point(160, 40);
            this.lSamplerate.Name = "lSamplerate";
            this.lSamplerate.Size = new System.Drawing.Size(25, 16);
            this.lSamplerate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lSamplerateTitle
            // 
            this.lSamplerateTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lSamplerateTitle.ForeColor = System.Drawing.Color.Black;
            this.lSamplerateTitle.Location = new System.Drawing.Point(194, 40);
            this.lSamplerateTitle.Name = "lSamplerateTitle";
            this.lSamplerateTitle.Size = new System.Drawing.Size(30, 16);
            this.lSamplerateTitle.Text = "khz";
            // 
            // lBitrate
            // 
            this.lBitrate.BackColor = System.Drawing.Color.DarkGray;
            this.lBitrate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lBitrate.ForeColor = System.Drawing.Color.Black;
            this.lBitrate.Location = new System.Drawing.Point(160, 20);
            this.lBitrate.Name = "lBitrate";
            this.lBitrate.Size = new System.Drawing.Size(25, 20);
            this.lBitrate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lBitrateTitle
            // 
            this.lBitrateTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lBitrateTitle.ForeColor = System.Drawing.Color.Black;
            this.lBitrateTitle.Location = new System.Drawing.Point(191, 20);
            this.lBitrateTitle.Name = "lBitrateTitle";
            this.lBitrateTitle.Size = new System.Drawing.Size(33, 20);
            this.lBitrateTitle.Text = "kbps";
            // 
            // lConnected
            // 
            this.lConnected.BackColor = System.Drawing.Color.DarkGray;
            this.lConnected.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lConnected.ForeColor = System.Drawing.Color.Black;
            this.lConnected.Location = new System.Drawing.Point(160, 0);
            this.lConnected.Name = "lConnected";
            this.lConnected.Size = new System.Drawing.Size(64, 19);
            // 
            // tbProgress
            // 
            this.tbProgress.Enabled = false;
            this.tbProgress.Location = new System.Drawing.Point(3, 114);
            this.tbProgress.Maximum = 100;
            this.tbProgress.Minimum = 1;
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(78, 18);
            this.tbProgress.TabIndex = 20;
            this.tbProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbProgress.Value = 1;
            // 
            // lbPlaylistSize
            // 
            this.lbPlaylistSize.BackColor = System.Drawing.Color.Silver;
            this.lbPlaylistSize.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbPlaylistSize.ForeColor = System.Drawing.Color.Black;
            this.lbPlaylistSize.Location = new System.Drawing.Point(5, 135);
            this.lbPlaylistSize.Name = "lbPlaylistSize";
            this.lbPlaylistSize.Size = new System.Drawing.Size(126, 14);
            this.lbPlaylistSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lArtistSong
            // 
            this.lArtistSong.BackColor = System.Drawing.Color.DarkGray;
            this.lArtistSong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lArtistSong.ForeColor = System.Drawing.Color.Black;
            this.lArtistSong.Location = new System.Drawing.Point(3, 66);
            this.lArtistSong.Name = "lArtistSong";
            this.lArtistSong.Size = new System.Drawing.Size(221, 46);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lbPlaylistSize);
            this.Controls.Add(this.lConnected);
            this.Controls.Add(this.lSamplerate);
            this.Controls.Add(this.lSamplerateTitle);
            this.Controls.Add(this.lBitrate);
            this.Controls.Add(this.lBitrateTitle);
            this.Controls.Add(this.lArtistSong);
            this.Controls.Add(this.tbVolume);
            this.Controls.Add(this.tbProgress);
            this.Controls.Add(this.lTimePlayed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "XBMControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.Label lTimePlayed;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lSamplerate;
        private System.Windows.Forms.Label lSamplerateTitle;
        private System.Windows.Forms.Label lBitrate;
        private System.Windows.Forms.Label lBitrateTitle;
        private System.Windows.Forms.Label lConnected;
        private System.Windows.Forms.TrackBar tbProgress;
        private System.Windows.Forms.Label lbPlaylistSize;
        private System.Windows.Forms.Label lArtistSong;
    }
}

