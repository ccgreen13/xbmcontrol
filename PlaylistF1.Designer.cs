namespace XBMControl
{
    partial class PlaylistF1
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
            this.lbPlaylist = new System.Windows.Forms.ListBox();
            this.cmsPlaylist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsPlayFromSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolbar = new System.Windows.Forms.Panel();
            this.lMainTitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbMinimize = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pbClearPlaylist = new System.Windows.Forms.PictureBox();
            this.timerUpdateSelection = new System.Windows.Forms.Timer(this.components);
            this.cmsPlaylist.SuspendLayout();
            this.pToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearPlaylist)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPlaylist
            // 
            this.lbPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlaylist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(242)))));
            this.lbPlaylist.ContextMenuStrip = this.cmsPlaylist;
            this.lbPlaylist.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlaylist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.lbPlaylist.FormattingEnabled = true;
            this.lbPlaylist.Location = new System.Drawing.Point(1, 21);
            this.lbPlaylist.Name = "lbPlaylist";
            this.lbPlaylist.Size = new System.Drawing.Size(265, 82);
            this.lbPlaylist.TabIndex = 1;
            this.lbPlaylist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbPlaylist_MouseDoubleClick);
            this.lbPlaylist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPlaylist_MouseDown);
            // 
            // cmsPlaylist
            // 
            this.cmsPlaylist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsPlayFromSelection,
            this.cmsRemoveItem});
            this.cmsPlaylist.Name = "contextMenuStrip1";
            this.cmsPlaylist.Size = new System.Drawing.Size(125, 48);
            // 
            // cmsPlayFromSelection
            // 
            this.cmsPlayFromSelection.Name = "cmsPlayFromSelection";
            this.cmsPlayFromSelection.Size = new System.Drawing.Size(124, 22);
            this.cmsPlayFromSelection.Text = "Play";
            this.cmsPlayFromSelection.Click += new System.EventHandler(this.PlaySelected);
            // 
            // cmsRemoveItem
            // 
            this.cmsRemoveItem.Name = "cmsRemoveItem";
            this.cmsRemoveItem.Size = new System.Drawing.Size(124, 22);
            this.cmsRemoveItem.Text = "Remove";
            this.cmsRemoveItem.Click += new System.EventHandler(this.RemoveSelected);
            // 
            // pToolbar
            // 
            this.pToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pToolbar.BackColor = System.Drawing.Color.Transparent;
            this.pToolbar.BackgroundImage = global::XBMControl.Properties.Resources.toolbar1;
            this.pToolbar.Controls.Add(this.lMainTitle);
            this.pToolbar.Controls.Add(this.pictureBox2);
            this.pToolbar.Controls.Add(this.pbMinimize);
            this.pToolbar.Controls.Add(this.pbClose);
            this.pToolbar.Location = new System.Drawing.Point(2, 1);
            this.pToolbar.Name = "pToolbar";
            this.pToolbar.Size = new System.Drawing.Size(264, 20);
            this.pToolbar.TabIndex = 2;
            this.pToolbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseMove);
            this.pToolbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseDown);
            this.pToolbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseUp);
            // 
            // lMainTitle
            // 
            this.lMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lMainTitle.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lMainTitle.ForeColor = System.Drawing.Color.Silver;
            this.lMainTitle.Location = new System.Drawing.Point(26, 4);
            this.lMainTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lMainTitle.Name = "lMainTitle";
            this.lMainTitle.Size = new System.Drawing.Size(212, 13);
            this.lMainTitle.TabIndex = 6;
            this.lMainTitle.Text = "Playlist";
            this.lMainTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseMove);
            this.lMainTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseDown);
            this.lMainTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pToolbar_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::XBMControl.Properties.Resources.XBMC;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(4, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pbMinimize
            // 
            this.pbMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinimize.BackColor = System.Drawing.Color.Transparent;
            this.pbMinimize.BackgroundImage = global::XBMControl.Properties.Resources.minimize1;
            this.pbMinimize.Location = new System.Drawing.Point(244, 4);
            this.pbMinimize.Name = "pbMinimize";
            this.pbMinimize.Size = new System.Drawing.Size(9, 9);
            this.pbMinimize.TabIndex = 4;
            this.pbMinimize.TabStop = false;
            // 
            // pbClose
            // 
            this.pbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::XBMControl.Properties.Resources.close1;
            this.pbClose.Location = new System.Drawing.Point(251, 4);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(9, 9);
            this.pbClose.TabIndex = 3;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // pbClearPlaylist
            // 
            this.pbClearPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbClearPlaylist.BackgroundImage = global::XBMControl.Properties.Resources.button_clear;
            this.pbClearPlaylist.Location = new System.Drawing.Point(3, 109);
            this.pbClearPlaylist.Name = "pbClearPlaylist";
            this.pbClearPlaylist.Size = new System.Drawing.Size(16, 16);
            this.pbClearPlaylist.TabIndex = 3;
            this.pbClearPlaylist.TabStop = false;
            this.pbClearPlaylist.Click += new System.EventHandler(this.pbClearPlaylist_Click);
            // 
            // timerUpdateSelection
            // 
            this.timerUpdateSelection.Interval = 5000;
            this.timerUpdateSelection.Tick += new System.EventHandler(this.timerUpdateSelection_Tick);
            // 
            // PlaylistF1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(242)))));
            this.BackgroundImage = global::XBMControl.Properties.Resources.MainFormBackground2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(268, 130);
            this.Controls.Add(this.pToolbar);
            this.Controls.Add(this.lbPlaylist);
            this.Controls.Add(this.pbClearPlaylist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PlaylistF1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form4";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlaylistF1_FormClosed);
            this.cmsPlaylist.ResumeLayout(false);
            this.pToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearPlaylist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbPlaylist;
        private System.Windows.Forms.Panel pToolbar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pbMinimize;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lMainTitle;
        private System.Windows.Forms.ContextMenuStrip cmsPlaylist;
        private System.Windows.Forms.ToolStripMenuItem cmsPlayFromSelection;
        private System.Windows.Forms.ToolStripMenuItem cmsRemoveItem;
        private System.Windows.Forms.PictureBox pbClearPlaylist;
        private System.Windows.Forms.Timer timerUpdateSelection;

    }
}