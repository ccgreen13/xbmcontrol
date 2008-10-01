﻿namespace XBMControl
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timerLong = new System.Windows.Forms.Timer(this.components);
            this.timerShort = new System.Windows.Forms.Timer(this.components);
            this.ilMediaType = new System.Windows.Forms.ImageList(this.components);
            this.pControls = new System.Windows.Forms.Panel();
            this.bNext = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            this.bPlay = new System.Windows.Forms.Button();
            this.bPrevious = new System.Windows.Forms.Button();
            this.bMute = new System.Windows.Forms.Button();
            this.tbProgress = new System.Windows.Forms.TrackBar();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.MainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsControls = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPause = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsStop = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNext = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsShow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pMusicInfo = new System.Windows.Forms.Panel();
            this.lDurationTitle = new System.Windows.Forms.Label();
            this.lDuration = new System.Windows.Forms.Label();
            this.pThumbnail = new System.Windows.Forms.Panel();
            this.pbLastFM = new System.Windows.Forms.PictureBox();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            this.lTitle = new System.Windows.Forms.Label();
            this.lAlbumTitle = new System.Windows.Forms.Label();
            this.lArtist = new System.Windows.Forms.Label();
            this.lTitleTitle = new System.Windows.Forms.Label();
            this.lAlbum = new System.Windows.Forms.Label();
            this.lArtistTitle = new System.Windows.Forms.Label();
            this.pNowPlayingInfo = new System.Windows.Forms.Panel();
            this.lArtistSong = new System.Windows.Forms.Label();
            this.pbMediaType = new System.Windows.Forms.PictureBox();
            this.lSamplerate = new System.Windows.Forms.Label();
            this.lSamplerateTitle = new System.Windows.Forms.Label();
            this.lBitrate = new System.Windows.Forms.Label();
            this.lBitrateTitle = new System.Windows.Forms.Label();
            this.lTimePlayed = new System.Windows.Forms.Label();
            this.pControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.MainContextMenu.SuspendLayout();
            this.pMusicInfo.SuspendLayout();
            this.pThumbnail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLastFM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.pNowPlayingInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMediaType)).BeginInit();
            this.SuspendLayout();
            // 
            // timerLong
            // 
            this.timerLong.Interval = 5000;
            this.timerLong.Tick += new System.EventHandler(this.timerLong_Tick);
            // 
            // timerShort
            // 
            this.timerShort.Interval = 1000;
            this.timerShort.Tick += new System.EventHandler(this.timerShort_Tick);
            // 
            // ilMediaType
            // 
            this.ilMediaType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMediaType.ImageStream")));
            this.ilMediaType.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMediaType.Images.SetKeyName(0, "video-32x32.png");
            this.ilMediaType.Images.SetKeyName(1, "audio-cd-32x32.png");
            this.ilMediaType.Images.SetKeyName(2, "pictures-32x32.png");
            // 
            // pControls
            // 
            this.pControls.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pControls.Controls.Add(this.bNext);
            this.pControls.Controls.Add(this.bStop);
            this.pControls.Controls.Add(this.bPause);
            this.pControls.Controls.Add(this.bPlay);
            this.pControls.Controls.Add(this.bPrevious);
            this.pControls.Controls.Add(this.bMute);
            this.pControls.Controls.Add(this.tbProgress);
            this.pControls.Controls.Add(this.tbVolume);
            this.pControls.Location = new System.Drawing.Point(0, 131);
            this.pControls.Name = "pControls";
            this.pControls.Size = new System.Drawing.Size(347, 65);
            this.pControls.TabIndex = 16;
            // 
            // bNext
            // 
            this.bNext.BackgroundImage = global::XBMControl.Properties.Resources.next2_32x32;
            this.bNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.bNext.FlatAppearance.BorderSize = 0;
            this.bNext.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.bNext.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.bNext.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNext.ForeColor = System.Drawing.SystemColors.Control;
            this.bNext.Location = new System.Drawing.Point(162, 25);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(32, 32);
            this.bNext.TabIndex = 16;
            this.bNext.UseVisualStyleBackColor = true;
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // bStop
            // 
            this.bStop.BackgroundImage = global::XBMControl.Properties.Resources.stop2_32x32;
            this.bStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bStop.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.bStop.FlatAppearance.BorderSize = 0;
            this.bStop.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.bStop.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.bStop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.ForeColor = System.Drawing.SystemColors.Control;
            this.bStop.Location = new System.Drawing.Point(124, 25);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(32, 32);
            this.bStop.TabIndex = 15;
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bPause
            // 
            this.bPause.BackgroundImage = global::XBMControl.Properties.Resources.pause2_32x32;
            this.bPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bPause.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.bPause.FlatAppearance.BorderSize = 0;
            this.bPause.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPause.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPause.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPause.ForeColor = System.Drawing.SystemColors.Control;
            this.bPause.Location = new System.Drawing.Point(86, 25);
            this.bPause.Name = "bPause";
            this.bPause.Size = new System.Drawing.Size(32, 32);
            this.bPause.TabIndex = 14;
            this.bPause.UseVisualStyleBackColor = true;
            this.bPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // bPlay
            // 
            this.bPlay.BackgroundImage = global::XBMControl.Properties.Resources.play2_32x32;
            this.bPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bPlay.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.bPlay.FlatAppearance.BorderSize = 0;
            this.bPlay.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPlay.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPlay.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlay.ForeColor = System.Drawing.SystemColors.Control;
            this.bPlay.Location = new System.Drawing.Point(48, 25);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(32, 32);
            this.bPlay.TabIndex = 13;
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // bPrevious
            // 
            this.bPrevious.BackgroundImage = global::XBMControl.Properties.Resources.previous_32x32;
            this.bPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bPrevious.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.bPrevious.FlatAppearance.BorderSize = 0;
            this.bPrevious.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.bPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPrevious.ForeColor = System.Drawing.SystemColors.Control;
            this.bPrevious.Location = new System.Drawing.Point(12, 25);
            this.bPrevious.Name = "bPrevious";
            this.bPrevious.Size = new System.Drawing.Size(32, 32);
            this.bPrevious.TabIndex = 12;
            this.bPrevious.UseVisualStyleBackColor = true;
            this.bPrevious.Click += new System.EventHandler(this.bPrevious_Click);
            // 
            // bMute
            // 
            this.bMute.BackgroundImage = global::XBMControl.Properties.Resources.mute_16x16;
            this.bMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bMute.FlatAppearance.BorderSize = 0;
            this.bMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMute.Location = new System.Drawing.Point(316, 20);
            this.bMute.Name = "bMute";
            this.bMute.Size = new System.Drawing.Size(16, 16);
            this.bMute.TabIndex = 11;
            this.bMute.UseVisualStyleBackColor = true;
            this.bMute.Click += new System.EventHandler(this.bMute_Click);
            // 
            // tbProgress
            // 
            this.tbProgress.AutoSize = false;
            this.tbProgress.LargeChange = 2;
            this.tbProgress.Location = new System.Drawing.Point(0, 0);
            this.tbProgress.Maximum = 100;
            this.tbProgress.MaximumSize = new System.Drawing.Size(350, 22);
            this.tbProgress.Minimum = 1;
            this.tbProgress.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(347, 22);
            this.tbProgress.TabIndex = 10;
            this.tbProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbProgress.Value = 1;
            this.tbProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbProgress_MouseDown);
            this.tbProgress.MouseHover += new System.EventHandler(this.tbProgress_MouseHover);
            this.tbProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbProgress_MouseUp);
            // 
            // tbVolume
            // 
            this.tbVolume.AutoSize = false;
            this.tbVolume.Location = new System.Drawing.Point(215, 20);
            this.tbVolume.Maximum = 100;
            this.tbVolume.MaximumSize = new System.Drawing.Size(200, 21);
            this.tbVolume.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(103, 20);
            this.tbVolume.TabIndex = 9;
            this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
            this.tbVolume.MouseHover += new System.EventHandler(this.tbVolume_MouseHover);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Click to open the XBMController";
            this.notifyIcon1.BalloonTipTitle = "XBMController";
            this.notifyIcon1.ContextMenuStrip = this.MainContextMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "XBMControl";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsControls,
            this.toolStripSeparator2,
            this.cmsShow,
            this.cmsHide,
            this.toolStripSeparator1,
            this.cmsConfigure,
            this.toolStripSeparator3,
            this.cmsExit});
            this.MainContextMenu.Name = "cmsNotifyIcon";
            this.MainContextMenu.Size = new System.Drawing.Size(153, 154);
            // 
            // cmsControls
            // 
            this.cmsControls.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsPrevious,
            this.cmsPlay,
            this.cmsPause,
            this.cmsStop,
            this.cmsNext,
            this.cmsMute});
            this.cmsControls.Image = global::XBMControl.Properties.Resources.folder_16x16;
            this.cmsControls.Name = "cmsControls";
            this.cmsControls.Size = new System.Drawing.Size(152, 22);
            // 
            // cmsPrevious
            // 
            this.cmsPrevious.Image = global::XBMControl.Properties.Resources.previous_16x16;
            this.cmsPrevious.Name = "cmsPrevious";
            this.cmsPrevious.Size = new System.Drawing.Size(152, 22);
            this.cmsPrevious.Click += new System.EventHandler(this.bPrevious_Click);
            // 
            // cmsPlay
            // 
            this.cmsPlay.Image = global::XBMControl.Properties.Resources.play2_16x16;
            this.cmsPlay.Name = "cmsPlay";
            this.cmsPlay.Size = new System.Drawing.Size(152, 22);
            this.cmsPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // cmsPause
            // 
            this.cmsPause.Image = global::XBMControl.Properties.Resources.pause_16x16;
            this.cmsPause.Name = "cmsPause";
            this.cmsPause.Size = new System.Drawing.Size(152, 22);
            this.cmsPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // cmsStop
            // 
            this.cmsStop.Image = global::XBMControl.Properties.Resources.stop_16x16;
            this.cmsStop.Name = "cmsStop";
            this.cmsStop.Size = new System.Drawing.Size(152, 22);
            this.cmsStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // cmsNext
            // 
            this.cmsNext.Image = global::XBMControl.Properties.Resources.next_16x16;
            this.cmsNext.Name = "cmsNext";
            this.cmsNext.Size = new System.Drawing.Size(152, 22);
            this.cmsNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // cmsMute
            // 
            this.cmsMute.Image = global::XBMControl.Properties.Resources.mute_16x16;
            this.cmsMute.Name = "cmsMute";
            this.cmsMute.Size = new System.Drawing.Size(152, 22);
            this.cmsMute.Click += new System.EventHandler(this.bMute_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsShow
            // 
            this.cmsShow.Image = global::XBMControl.Properties.Resources.show_16x16;
            this.cmsShow.Name = "cmsShow";
            this.cmsShow.Size = new System.Drawing.Size(152, 22);
            this.cmsShow.Click += new System.EventHandler(this.cmsNotifyShow_Click);
            // 
            // cmsHide
            // 
            this.cmsHide.Image = global::XBMControl.Properties.Resources.hide_16x16;
            this.cmsHide.Name = "cmsHide";
            this.cmsHide.Size = new System.Drawing.Size(152, 22);
            this.cmsHide.Click += new System.EventHandler(this.cmsNotifyHide_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsConfigure
            // 
            this.cmsConfigure.Image = global::XBMControl.Properties.Resources.configure_16x16;
            this.cmsConfigure.Name = "cmsConfigure";
            this.cmsConfigure.Size = new System.Drawing.Size(152, 22);
            this.cmsConfigure.Click += new System.EventHandler(this.cmsConfigure_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsExit
            // 
            this.cmsExit.Image = global::XBMControl.Properties.Resources.exit_16x16;
            this.cmsExit.Name = "cmsExit";
            this.cmsExit.Size = new System.Drawing.Size(152, 22);
            this.cmsExit.Click += new System.EventHandler(this.cmsNotifyExit_Click);
            // 
            // pMusicInfo
            // 
            this.pMusicInfo.Controls.Add(this.lDurationTitle);
            this.pMusicInfo.Controls.Add(this.lDuration);
            this.pMusicInfo.Controls.Add(this.pThumbnail);
            this.pMusicInfo.Controls.Add(this.lTitle);
            this.pMusicInfo.Controls.Add(this.lAlbumTitle);
            this.pMusicInfo.Controls.Add(this.lArtist);
            this.pMusicInfo.Controls.Add(this.lTitleTitle);
            this.pMusicInfo.Controls.Add(this.lAlbum);
            this.pMusicInfo.Controls.Add(this.lArtistTitle);
            this.pMusicInfo.Controls.Add(this.pNowPlayingInfo);
            this.pMusicInfo.Location = new System.Drawing.Point(0, 1);
            this.pMusicInfo.Name = "pMusicInfo";
            this.pMusicInfo.Size = new System.Drawing.Size(347, 130);
            this.pMusicInfo.TabIndex = 17;
            // 
            // lDurationTitle
            // 
            this.lDurationTitle.ForeColor = System.Drawing.Color.Silver;
            this.lDurationTitle.Location = new System.Drawing.Point(3, 100);
            this.lDurationTitle.Name = "lDurationTitle";
            this.lDurationTitle.Size = new System.Drawing.Size(44, 13);
            this.lDurationTitle.TabIndex = 30;
            this.lDurationTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lDuration
            // 
            this.lDuration.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDuration.ForeColor = System.Drawing.Color.Gray;
            this.lDuration.Location = new System.Drawing.Point(44, 100);
            this.lDuration.Name = "lDuration";
            this.lDuration.Size = new System.Drawing.Size(170, 13);
            this.lDuration.TabIndex = 31;
            // 
            // pThumbnail
            // 
            this.pThumbnail.Controls.Add(this.pbLastFM);
            this.pThumbnail.Controls.Add(this.pbThumbnail);
            this.pThumbnail.Location = new System.Drawing.Point(215, 0);
            this.pThumbnail.Name = "pThumbnail";
            this.pThumbnail.Size = new System.Drawing.Size(129, 130);
            this.pThumbnail.TabIndex = 29;
            // 
            // pbLastFM
            // 
            this.pbLastFM.Image = global::XBMControl.Properties.Resources.lastfm1;
            this.pbLastFM.Location = new System.Drawing.Point(81, 104);
            this.pbLastFM.Name = "pbLastFM";
            this.pbLastFM.Size = new System.Drawing.Size(39, 15);
            this.pbLastFM.TabIndex = 12;
            this.pbLastFM.TabStop = false;
            this.pbLastFM.Visible = false;
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.BackgroundImage = global::XBMControl.Properties.Resources.XBMClogo;
            this.pbThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbThumbnail.ErrorImage = global::XBMControl.Properties.Resources.XBMClogo;
            this.pbThumbnail.InitialImage = global::XBMControl.Properties.Resources.XBMClogo;
            this.pbThumbnail.Location = new System.Drawing.Point(5, 4);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(120, 120);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThumbnail.TabIndex = 11;
            this.pbThumbnail.TabStop = false;
            // 
            // lTitle
            // 
            this.lTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.Gray;
            this.lTitle.Location = new System.Drawing.Point(44, 87);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(170, 13);
            this.lTitle.TabIndex = 26;
            // 
            // lAlbumTitle
            // 
            this.lAlbumTitle.ForeColor = System.Drawing.Color.Silver;
            this.lAlbumTitle.Location = new System.Drawing.Point(3, 113);
            this.lAlbumTitle.Name = "lAlbumTitle";
            this.lAlbumTitle.Size = new System.Drawing.Size(44, 13);
            this.lAlbumTitle.TabIndex = 23;
            this.lAlbumTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lArtist
            // 
            this.lArtist.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lArtist.ForeColor = System.Drawing.Color.Gray;
            this.lArtist.Location = new System.Drawing.Point(44, 74);
            this.lArtist.Name = "lArtist";
            this.lArtist.Size = new System.Drawing.Size(170, 13);
            this.lArtist.TabIndex = 25;
            // 
            // lTitleTitle
            // 
            this.lTitleTitle.ForeColor = System.Drawing.Color.Silver;
            this.lTitleTitle.Location = new System.Drawing.Point(3, 87);
            this.lTitleTitle.Name = "lTitleTitle";
            this.lTitleTitle.Size = new System.Drawing.Size(44, 13);
            this.lTitleTitle.TabIndex = 22;
            this.lTitleTitle.Tag = "";
            this.lTitleTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lAlbum
            // 
            this.lAlbum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAlbum.ForeColor = System.Drawing.Color.Gray;
            this.lAlbum.Location = new System.Drawing.Point(44, 113);
            this.lAlbum.Name = "lAlbum";
            this.lAlbum.Size = new System.Drawing.Size(170, 13);
            this.lAlbum.TabIndex = 24;
            // 
            // lArtistTitle
            // 
            this.lArtistTitle.ForeColor = System.Drawing.Color.Silver;
            this.lArtistTitle.Location = new System.Drawing.Point(3, 74);
            this.lArtistTitle.Name = "lArtistTitle";
            this.lArtistTitle.Size = new System.Drawing.Size(44, 13);
            this.lArtistTitle.TabIndex = 21;
            this.lArtistTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pNowPlayingInfo
            // 
            this.pNowPlayingInfo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pNowPlayingInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNowPlayingInfo.Controls.Add(this.lArtistSong);
            this.pNowPlayingInfo.Controls.Add(this.pbMediaType);
            this.pNowPlayingInfo.Controls.Add(this.lSamplerate);
            this.pNowPlayingInfo.Controls.Add(this.lSamplerateTitle);
            this.pNowPlayingInfo.Controls.Add(this.lBitrate);
            this.pNowPlayingInfo.Controls.Add(this.lBitrateTitle);
            this.pNowPlayingInfo.Controls.Add(this.lTimePlayed);
            this.pNowPlayingInfo.Location = new System.Drawing.Point(4, 4);
            this.pNowPlayingInfo.Name = "pNowPlayingInfo";
            this.pNowPlayingInfo.Size = new System.Drawing.Size(210, 67);
            this.pNowPlayingInfo.TabIndex = 20;
            // 
            // lArtistSong
            // 
            this.lArtistSong.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lArtistSong.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lArtistSong.ForeColor = System.Drawing.Color.Gold;
            this.lArtistSong.Location = new System.Drawing.Point(-2, 49);
            this.lArtistSong.Margin = new System.Windows.Forms.Padding(0);
            this.lArtistSong.Name = "lArtistSong";
            this.lArtistSong.Size = new System.Drawing.Size(211, 16);
            this.lArtistSong.TabIndex = 7;
            this.lArtistSong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbMediaType
            // 
            this.pbMediaType.Image = global::XBMControl.Properties.Resources.audio_cd_32x32;
            this.pbMediaType.Location = new System.Drawing.Point(176, 1);
            this.pbMediaType.Name = "pbMediaType";
            this.pbMediaType.Size = new System.Drawing.Size(32, 32);
            this.pbMediaType.TabIndex = 6;
            this.pbMediaType.TabStop = false;
            this.pbMediaType.Visible = false;
            // 
            // lSamplerate
            // 
            this.lSamplerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lSamplerate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSamplerate.ForeColor = System.Drawing.Color.Snow;
            this.lSamplerate.Location = new System.Drawing.Point(124, 2);
            this.lSamplerate.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lSamplerate.Name = "lSamplerate";
            this.lSamplerate.Size = new System.Drawing.Size(27, 13);
            this.lSamplerate.TabIndex = 4;
            this.lSamplerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lSamplerateTitle
            // 
            this.lSamplerateTitle.AutoSize = true;
            this.lSamplerateTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSamplerateTitle.ForeColor = System.Drawing.Color.Gold;
            this.lSamplerateTitle.Location = new System.Drawing.Point(148, 15);
            this.lSamplerateTitle.Name = "lSamplerateTitle";
            this.lSamplerateTitle.Size = new System.Drawing.Size(23, 13);
            this.lSamplerateTitle.TabIndex = 3;
            this.lSamplerateTitle.Text = "khz";
            // 
            // lBitrate
            // 
            this.lBitrate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBitrate.ForeColor = System.Drawing.Color.Snow;
            this.lBitrate.Location = new System.Drawing.Point(116, 15);
            this.lBitrate.Margin = new System.Windows.Forms.Padding(0);
            this.lBitrate.Name = "lBitrate";
            this.lBitrate.Size = new System.Drawing.Size(35, 13);
            this.lBitrate.TabIndex = 2;
            this.lBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lBitrateTitle
            // 
            this.lBitrateTitle.AutoSize = true;
            this.lBitrateTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBitrateTitle.ForeColor = System.Drawing.Color.Gold;
            this.lBitrateTitle.Location = new System.Drawing.Point(148, 2);
            this.lBitrateTitle.Name = "lBitrateTitle";
            this.lBitrateTitle.Size = new System.Drawing.Size(29, 13);
            this.lBitrateTitle.TabIndex = 1;
            this.lBitrateTitle.Text = "kbps";
            // 
            // lTimePlayed
            // 
            this.lTimePlayed.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lTimePlayed.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTimePlayed.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lTimePlayed.Location = new System.Drawing.Point(-3, 0);
            this.lTimePlayed.Name = "lTimePlayed";
            this.lTimePlayed.Size = new System.Drawing.Size(135, 48);
            this.lTimePlayed.TabIndex = 0;
            this.lTimePlayed.Text = "00:00";
            // 
            // MainForm
            // 
            this.AccessibleName = "MainForm";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 196);
            this.ContextMenuStrip = this.MainContextMenu;
            this.Controls.Add(this.pMusicInfo);
            this.Controls.Add(this.pControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XBMControl";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.pControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.MainContextMenu.ResumeLayout(false);
            this.pMusicInfo.ResumeLayout(false);
            this.pThumbnail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLastFM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.pNowPlayingInfo.ResumeLayout(false);
            this.pNowPlayingInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMediaType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerLong;
        private System.Windows.Forms.Timer timerShort;
        private System.Windows.Forms.ImageList ilMediaType;
        private System.Windows.Forms.Panel pControls;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bPause;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bPrevious;
        private System.Windows.Forms.Button bMute;
        private System.Windows.Forms.TrackBar tbProgress;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel pMusicInfo;
        private System.Windows.Forms.Panel pThumbnail;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Label lAlbumTitle;
        private System.Windows.Forms.Label lArtist;
        private System.Windows.Forms.Label lTitleTitle;
        private System.Windows.Forms.Label lAlbum;
        private System.Windows.Forms.Label lArtistTitle;
        private System.Windows.Forms.Panel pNowPlayingInfo;
        private System.Windows.Forms.PictureBox pbMediaType;
        private System.Windows.Forms.Label lSamplerate;
        private System.Windows.Forms.Label lSamplerateTitle;
        private System.Windows.Forms.Label lBitrate;
        private System.Windows.Forms.Label lBitrateTitle;
        private System.Windows.Forms.Label lTimePlayed;
        private System.Windows.Forms.PictureBox pbLastFM;
        private System.Windows.Forms.Label lDurationTitle;
        private System.Windows.Forms.Label lDuration;
        private System.Windows.Forms.Label lArtistSong;
        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.ContextMenuStrip MainContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmsExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmsShow;
        private System.Windows.Forms.ToolStripMenuItem cmsHide;
        private System.Windows.Forms.ToolStripMenuItem cmsConfigure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmsControls;
        private System.Windows.Forms.ToolStripMenuItem cmsPrevious;
        private System.Windows.Forms.ToolStripMenuItem cmsPlay;
        private System.Windows.Forms.ToolStripMenuItem cmsPause;
        private System.Windows.Forms.ToolStripMenuItem cmsStop;
        private System.Windows.Forms.ToolStripMenuItem cmsNext;
        private System.Windows.Forms.ToolStripMenuItem cmsMute;
    }
}
