namespace XBMControl
{
    partial class MediaBrowserF1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaBrowserF1));
            this.cmsFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiPlayRecursive = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiEnqueueRecursive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ilFiletypes = new System.Windows.Forms.ImageList(this.components);
            this.cmsSongs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiPlayFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiEnqueueFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMediaBrowser = new System.Windows.Forms.TabControl();
            this.tabShares = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbShareType = new System.Windows.Forms.ComboBox();
            this.tvMediaShares = new System.Windows.Forms.TreeView();
            this.lvDirectorySongs = new System.Windows.Forms.ListView();
            this.tabArtists = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvArtists = new System.Windows.Forms.TreeView();
            this.lvArtistSongs = new System.Windows.Forms.ListView();
            this.tabAlbums = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tvAlbums = new System.Windows.Forms.TreeView();
            this.lvAlbumSongs = new System.Windows.Forms.ListView();
            this.Search = new System.Windows.Forms.TabPage();
            this.tbSearchLibrary = new System.Windows.Forms.TextBox();
            this.tbSearchArtist = new System.Windows.Forms.TextBox();
            this.tbSearchAlbum = new System.Windows.Forms.TextBox();
            this.cmsFolder.SuspendLayout();
            this.cmsSongs.SuspendLayout();
            this.tcMediaBrowser.SuspendLayout();
            this.tabShares.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabArtists.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabAlbums.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.Search.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsFolder
            // 
            this.cmsFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiPlayRecursive,
            this.tsiEnqueueRecursive,
            this.toolStripSeparator1,
            this.tsiCollapseAll});
            this.cmsFolder.Name = "cmsFolder";
            this.cmsFolder.Size = new System.Drawing.Size(175, 76);
            // 
            // tsiPlayRecursive
            // 
            this.tsiPlayRecursive.Name = "tsiPlayRecursive";
            this.tsiPlayRecursive.Size = new System.Drawing.Size(174, 22);
            this.tsiPlayRecursive.Text = "Play";
            this.tsiPlayRecursive.Click += new System.EventHandler(this.tsiPlayRecursive_Click);
            // 
            // tsiEnqueueRecursive
            // 
            this.tsiEnqueueRecursive.Name = "tsiEnqueueRecursive";
            this.tsiEnqueueRecursive.Size = new System.Drawing.Size(174, 22);
            this.tsiEnqueueRecursive.Text = "Enqueue";
            this.tsiEnqueueRecursive.Click += new System.EventHandler(this.tsiEnqueueRecursive_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // tsiCollapseAll
            // 
            this.tsiCollapseAll.Name = "tsiCollapseAll";
            this.tsiCollapseAll.Size = new System.Drawing.Size(174, 22);
            this.tsiCollapseAll.Text = "Collapse all folders";
            this.tsiCollapseAll.Click += new System.EventHandler(this.tsiCollapseAll_Click);
            // 
            // ilFiletypes
            // 
            this.ilFiletypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFiletypes.ImageStream")));
            this.ilFiletypes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilFiletypes.Images.SetKeyName(0, "folder2.png");
            this.ilFiletypes.Images.SetKeyName(1, "music.png");
            this.ilFiletypes.Images.SetKeyName(2, "picture.png");
            this.ilFiletypes.Images.SetKeyName(3, "video.png");
            this.ilFiletypes.Images.SetKeyName(4, "file.png");
            // 
            // cmsSongs
            // 
            this.cmsSongs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiPlayFiles,
            this.tsiEnqueueFiles});
            this.cmsSongs.Name = "cmsFiles";
            this.cmsSongs.Size = new System.Drawing.Size(128, 48);
            // 
            // tsiPlayFiles
            // 
            this.tsiPlayFiles.Name = "tsiPlayFiles";
            this.tsiPlayFiles.Size = new System.Drawing.Size(127, 22);
            this.tsiPlayFiles.Text = "Play";
            this.tsiPlayFiles.Click += new System.EventHandler(this.tsiPlayFiles_Click);
            // 
            // tsiEnqueueFiles
            // 
            this.tsiEnqueueFiles.Name = "tsiEnqueueFiles";
            this.tsiEnqueueFiles.Size = new System.Drawing.Size(127, 22);
            this.tsiEnqueueFiles.Text = "Enqueue";
            this.tsiEnqueueFiles.Click += new System.EventHandler(this.tsiEnqueueFiles_Click);
            // 
            // tcMediaBrowser
            // 
            this.tcMediaBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMediaBrowser.Controls.Add(this.tabShares);
            this.tcMediaBrowser.Controls.Add(this.tabArtists);
            this.tcMediaBrowser.Controls.Add(this.tabAlbums);
            this.tcMediaBrowser.Controls.Add(this.Search);
            this.tcMediaBrowser.Location = new System.Drawing.Point(0, 1);
            this.tcMediaBrowser.Name = "tcMediaBrowser";
            this.tcMediaBrowser.SelectedIndex = 0;
            this.tcMediaBrowser.Size = new System.Drawing.Size(525, 450);
            this.tcMediaBrowser.TabIndex = 2;
            this.tcMediaBrowser.SelectedIndexChanged += new System.EventHandler(this.tcMediaBrowser_SelectedIndexChanged);
            // 
            // tabShares
            // 
            this.tabShares.Controls.Add(this.splitContainer1);
            this.tabShares.Location = new System.Drawing.Point(4, 22);
            this.tabShares.Name = "tabShares";
            this.tabShares.Padding = new System.Windows.Forms.Padding(3);
            this.tabShares.Size = new System.Drawing.Size(517, 424);
            this.tabShares.TabIndex = 0;
            this.tabShares.Text = "Shares";
            this.tabShares.ToolTipText = "Browse configured media shares (file mode)";
            this.tabShares.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbShareType);
            this.splitContainer1.Panel1.Controls.Add(this.tvMediaShares);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvDirectorySongs);
            this.splitContainer1.Size = new System.Drawing.Size(514, 418);
            this.splitContainer1.SplitterDistance = 162;
            this.splitContainer1.TabIndex = 6;
            // 
            // cbShareType
            // 
            this.cbShareType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShareType.FormattingEnabled = true;
            this.cbShareType.Items.AddRange(new object[] {
            "music",
            "video",
            "pictures",
            "files"});
            this.cbShareType.Location = new System.Drawing.Point(0, 3);
            this.cbShareType.Name = "cbShareType";
            this.cbShareType.Size = new System.Drawing.Size(162, 21);
            this.cbShareType.TabIndex = 6;
            this.cbShareType.MouseHover += new System.EventHandler(this.cbShareType_MouseHover);
            this.cbShareType.SelectedIndexChanged += new System.EventHandler(this.cbShareType_SelectedIndexChanged);
            // 
            // tvMediaShares
            // 
            this.tvMediaShares.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMediaShares.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvMediaShares.ContextMenuStrip = this.cmsFolder;
            this.tvMediaShares.ImageIndex = 0;
            this.tvMediaShares.ImageList = this.ilFiletypes;
            this.tvMediaShares.ItemHeight = 16;
            this.tvMediaShares.Location = new System.Drawing.Point(0, 29);
            this.tvMediaShares.Name = "tvMediaShares";
            this.tvMediaShares.SelectedImageIndex = 0;
            this.tvMediaShares.Size = new System.Drawing.Size(162, 389);
            this.tvMediaShares.TabIndex = 1;
            this.tvMediaShares.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvMediaShares_MouseDown);
            this.tvMediaShares.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMediaShares_AfterSelect);
            this.tvMediaShares.MouseHover += new System.EventHandler(this.tvMediaShares_MouseHover);
            // 
            // lvDirectorySongs
            // 
            this.lvDirectorySongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDirectorySongs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDirectorySongs.ContextMenuStrip = this.cmsSongs;
            this.lvDirectorySongs.Location = new System.Drawing.Point(0, 0);
            this.lvDirectorySongs.Name = "lvDirectorySongs";
            this.lvDirectorySongs.Size = new System.Drawing.Size(348, 418);
            this.lvDirectorySongs.SmallImageList = this.ilFiletypes;
            this.lvDirectorySongs.TabIndex = 0;
            this.lvDirectorySongs.UseCompatibleStateImageBehavior = false;
            this.lvDirectorySongs.View = System.Windows.Forms.View.List;
            this.lvDirectorySongs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDirectoryContent_MouseDoubleClick);
            // 
            // tabArtists
            // 
            this.tabArtists.Controls.Add(this.splitContainer2);
            this.tabArtists.Location = new System.Drawing.Point(4, 22);
            this.tabArtists.Name = "tabArtists";
            this.tabArtists.Padding = new System.Windows.Forms.Padding(3);
            this.tabArtists.Size = new System.Drawing.Size(517, 424);
            this.tabArtists.TabIndex = 1;
            this.tabArtists.Text = "Arists";
            this.tabArtists.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(1, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbSearchArtist);
            this.splitContainer2.Panel1.Controls.Add(this.tvArtists);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvArtistSongs);
            this.splitContainer2.Size = new System.Drawing.Size(514, 418);
            this.splitContainer2.SplitterDistance = 162;
            this.splitContainer2.TabIndex = 7;
            // 
            // tvArtists
            // 
            this.tvArtists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvArtists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvArtists.ContextMenuStrip = this.cmsFolder;
            this.tvArtists.ImageIndex = 0;
            this.tvArtists.ImageList = this.ilFiletypes;
            this.tvArtists.ItemHeight = 16;
            this.tvArtists.Location = new System.Drawing.Point(0, 24);
            this.tvArtists.Name = "tvArtists";
            this.tvArtists.SelectedImageIndex = 0;
            this.tvArtists.Size = new System.Drawing.Size(162, 394);
            this.tvArtists.TabIndex = 1;
            this.tvArtists.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvArtists_MouseUp);
            this.tvArtists.MouseHover += new System.EventHandler(this.tvArtists_MouseHover);
            // 
            // lvArtistSongs
            // 
            this.lvArtistSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvArtistSongs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvArtistSongs.ContextMenuStrip = this.cmsSongs;
            this.lvArtistSongs.Location = new System.Drawing.Point(0, 0);
            this.lvArtistSongs.Name = "lvArtistSongs";
            this.lvArtistSongs.Size = new System.Drawing.Size(347, 418);
            this.lvArtistSongs.SmallImageList = this.ilFiletypes;
            this.lvArtistSongs.TabIndex = 0;
            this.lvArtistSongs.UseCompatibleStateImageBehavior = false;
            this.lvArtistSongs.View = System.Windows.Forms.View.List;
            // 
            // tabAlbums
            // 
            this.tabAlbums.Controls.Add(this.splitContainer3);
            this.tabAlbums.Location = new System.Drawing.Point(4, 22);
            this.tabAlbums.Name = "tabAlbums";
            this.tabAlbums.Size = new System.Drawing.Size(517, 424);
            this.tabAlbums.TabIndex = 2;
            this.tabAlbums.Text = "Albums";
            this.tabAlbums.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(1, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tbSearchAlbum);
            this.splitContainer3.Panel1.Controls.Add(this.tvAlbums);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvAlbumSongs);
            this.splitContainer3.Size = new System.Drawing.Size(514, 418);
            this.splitContainer3.SplitterDistance = 162;
            this.splitContainer3.TabIndex = 8;
            // 
            // tvAlbums
            // 
            this.tvAlbums.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvAlbums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvAlbums.ContextMenuStrip = this.cmsFolder;
            this.tvAlbums.ImageIndex = 0;
            this.tvAlbums.ImageList = this.ilFiletypes;
            this.tvAlbums.ItemHeight = 16;
            this.tvAlbums.Location = new System.Drawing.Point(0, 24);
            this.tvAlbums.Name = "tvAlbums";
            this.tvAlbums.SelectedImageIndex = 0;
            this.tvAlbums.Size = new System.Drawing.Size(162, 394);
            this.tvAlbums.TabIndex = 1;
            this.tvAlbums.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvArtists_MouseUp);
            this.tvAlbums.MouseHover += new System.EventHandler(this.tvAlbums_MouseHover);
            // 
            // lvAlbumSongs
            // 
            this.lvAlbumSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAlbumSongs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvAlbumSongs.ContextMenuStrip = this.cmsSongs;
            this.lvAlbumSongs.Location = new System.Drawing.Point(0, 0);
            this.lvAlbumSongs.Name = "lvAlbumSongs";
            this.lvAlbumSongs.Size = new System.Drawing.Size(347, 418);
            this.lvAlbumSongs.SmallImageList = this.ilFiletypes;
            this.lvAlbumSongs.TabIndex = 0;
            this.lvAlbumSongs.UseCompatibleStateImageBehavior = false;
            this.lvAlbumSongs.View = System.Windows.Forms.View.List;
            // 
            // Search
            // 
            this.Search.Controls.Add(this.tbSearchLibrary);
            this.Search.Location = new System.Drawing.Point(4, 22);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(517, 424);
            this.Search.TabIndex = 3;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            // 
            // tbSearchLibrary
            // 
            this.tbSearchLibrary.Location = new System.Drawing.Point(8, 14);
            this.tbSearchLibrary.Name = "tbSearchLibrary";
            this.tbSearchLibrary.Size = new System.Drawing.Size(298, 21);
            this.tbSearchLibrary.TabIndex = 9;
            this.tbSearchLibrary.Visible = false;
            // 
            // tbSearchArtist
            // 
            this.tbSearchArtist.Location = new System.Drawing.Point(0, 0);
            this.tbSearchArtist.Name = "tbSearchArtist";
            this.tbSearchArtist.Size = new System.Drawing.Size(162, 21);
            this.tbSearchArtist.TabIndex = 13;
            this.tbSearchArtist.TextChanged += new System.EventHandler(this.tbSearchArtist_TextChanged);
            // 
            // tbSearchAlbum
            // 
            this.tbSearchAlbum.Location = new System.Drawing.Point(0, 0);
            this.tbSearchAlbum.Name = "tbSearchAlbum";
            this.tbSearchAlbum.Size = new System.Drawing.Size(162, 21);
            this.tbSearchAlbum.TabIndex = 14;
            this.tbSearchAlbum.TextChanged += new System.EventHandler(this.tbSearchAlbum_TextChanged);
            // 
            // MediaBrowserF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 450);
            this.Controls.Add(this.tcMediaBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MediaBrowserF1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Browser";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediaBrowserF1_FormClosing);
            this.cmsFolder.ResumeLayout(false);
            this.cmsSongs.ResumeLayout(false);
            this.tcMediaBrowser.ResumeLayout(false);
            this.tabShares.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabArtists.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabAlbums.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.Search.ResumeLayout(false);
            this.Search.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ilFiletypes;
        private System.Windows.Forms.ContextMenuStrip cmsFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsiCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem tsiPlayRecursive;
        private System.Windows.Forms.ToolStripMenuItem tsiEnqueueRecursive;
        private System.Windows.Forms.ContextMenuStrip cmsSongs;
        private System.Windows.Forms.ToolStripMenuItem tsiEnqueueFiles;
        private System.Windows.Forms.ToolStripMenuItem tsiPlayFiles;
        private System.Windows.Forms.TabControl tcMediaBrowser;
        private System.Windows.Forms.TabPage tabShares;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvMediaShares;
        private System.Windows.Forms.ListView lvDirectorySongs;
        private System.Windows.Forms.TabPage tabArtists;
        private System.Windows.Forms.TabPage tabAlbums;
        private System.Windows.Forms.TabPage Search;
        private System.Windows.Forms.TextBox tbSearchLibrary;
        private System.Windows.Forms.ComboBox cbShareType;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvArtists;
        private System.Windows.Forms.ListView lvArtistSongs;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView tvAlbums;
        private System.Windows.Forms.ListView lvAlbumSongs;
        private System.Windows.Forms.TextBox tbSearchArtist;
        private System.Windows.Forms.TextBox tbSearchAlbum;
    }
}