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
            this.cbShareType = new System.Windows.Forms.ComboBox();
            this.cmsFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiPlayFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiEnqueueFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvMediaShares = new System.Windows.Forms.TreeView();
            this.lvDirectoryContent = new System.Windows.Forms.ListView();
            this.cmsFolder.SuspendLayout();
            this.cmsFiles.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.ilFiletypes.Images.SetKeyName(0, "folder.png");
            this.ilFiletypes.Images.SetKeyName(1, "music.png");
            this.ilFiletypes.Images.SetKeyName(2, "picture.png");
            this.ilFiletypes.Images.SetKeyName(3, "video.png");
            this.ilFiletypes.Images.SetKeyName(4, "file.png");
            // 
            // cbShareType
            // 
            this.cbShareType.FormattingEnabled = true;
            this.cbShareType.Items.AddRange(new object[] {
            "music",
            "video",
            "pictures",
            "files"});
            this.cbShareType.Location = new System.Drawing.Point(12, 8);
            this.cbShareType.Name = "cbShareType";
            this.cbShareType.Size = new System.Drawing.Size(94, 21);
            this.cbShareType.TabIndex = 1;
            this.cbShareType.Text = "music";
            this.cbShareType.SelectedIndexChanged += new System.EventHandler(this.cbShareType_SelectedIndexChanged);
            // 
            // cmsFiles
            // 
            this.cmsFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiPlayFiles,
            this.tsiEnqueueFiles});
            this.cmsFiles.Name = "cmsFiles";
            this.cmsFiles.Size = new System.Drawing.Size(128, 48);
            this.cmsFiles.Click += new System.EventHandler(this.cmsFiles_Click);
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
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvMediaShares);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvDirectoryContent);
            this.splitContainer1.Size = new System.Drawing.Size(482, 361);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 2;
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
            this.tvMediaShares.Location = new System.Drawing.Point(0, 0);
            this.tvMediaShares.Name = "tvMediaShares";
            this.tvMediaShares.SelectedImageIndex = 0;
            this.tvMediaShares.Size = new System.Drawing.Size(174, 361);
            this.tvMediaShares.TabIndex = 1;
            this.tvMediaShares.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMediaShares_AfterSelect);
            this.tvMediaShares.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMediaShares_MouseDown);
            // 
            // lvDirectoryContent
            // 
            this.lvDirectoryContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDirectoryContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDirectoryContent.ContextMenuStrip = this.cmsFiles;
            this.lvDirectoryContent.Location = new System.Drawing.Point(3, 0);
            this.lvDirectoryContent.Name = "lvDirectoryContent";
            this.lvDirectoryContent.Size = new System.Drawing.Size(298, 361);
            this.lvDirectoryContent.SmallImageList = this.ilFiletypes;
            this.lvDirectoryContent.TabIndex = 0;
            this.lvDirectoryContent.UseCompatibleStateImageBehavior = false;
            this.lvDirectoryContent.View = System.Windows.Forms.View.List;
            this.lvDirectoryContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDirectoryContent_MouseDoubleClick);
            // 
            // MediaBrowserF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 402);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cbShareType);
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
            this.cmsFiles.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbShareType;
        private System.Windows.Forms.ImageList ilFiletypes;
        private System.Windows.Forms.ContextMenuStrip cmsFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsiCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem tsiPlayRecursive;
        private System.Windows.Forms.ToolStripMenuItem tsiEnqueueRecursive;
        private System.Windows.Forms.ContextMenuStrip cmsFiles;
        private System.Windows.Forms.ToolStripMenuItem tsiEnqueueFiles;
        private System.Windows.Forms.ToolStripMenuItem tsiPlayFiles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvDirectoryContent;
        private System.Windows.Forms.TreeView tvMediaShares;
    }
}