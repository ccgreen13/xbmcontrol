using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMControl.Language;
using XBMControl.Properties;

namespace XBMControl
{
    public partial class MediaBrowserF1 : Form
    {
        MainForm parent;
        TreeNode sharesNode;

        public MediaBrowserF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
            this.PopulateShareBrowser();
        }

        public void PopulateShareBrowser()
        {
            this.TestConnectivity();

            string[] aShares     = parent.XBMC.GetMediaShares(cbShareType.Text);
            string[] aSharePaths = parent.XBMC.GetMediaShares(cbShareType.Text, true);
            tvMediaShares.Nodes.Clear();

            if (aShares != null)
            {
                for (int x = 1; x < aShares.Length; x++)
                {
                    if (aShares[x] != null)
                    {
                        sharesNode = new TreeNode();
                        sharesNode.Name = aShares[x];
                        sharesNode.Text = aShares[x];
                        sharesNode.ToolTipText = aSharePaths[x];
                        tvMediaShares.Nodes.Add(sharesNode);
                        tvMediaShares.Nodes[x - 1].ImageIndex = 0;
                    }
                }
            }

        }

        private void ExpandSharedDirectory()
        {
            this.TestConnectivity();

            if (tvMediaShares.SelectedNode.GetNodeCount(false) == 0)
            {
                string[] aDirectoryContentPaths = parent.XBMC.GetDirectoryContentPaths(tvMediaShares.SelectedNode.ToolTipText, "/");
                string[] aDirectoryContentNames = parent.XBMC.GetDirectoryContentNames(tvMediaShares.SelectedNode.ToolTipText, "/");

                if (aDirectoryContentPaths != null)
                {
                    for (int x = 1; x < aDirectoryContentPaths.Length; x++)
                    {
                        if (aDirectoryContentPaths[x] != null)
                        {
                            sharesNode = new TreeNode();
                            sharesNode.Name = aDirectoryContentNames[x];
                            sharesNode.Text = aDirectoryContentNames[x];
                            sharesNode.ToolTipText = aDirectoryContentPaths[x];
                            tvMediaShares.SelectedNode.Nodes.Add(sharesNode);
                            tvMediaShares.SelectedNode.Nodes[x - 1].ImageIndex = 0;
                        }
                    }
                }
            }
            tvMediaShares.SelectedNode.Expand();
        }

        private void PopulateFileBrowser()
        {
            this.TestConnectivity();

            lvDirectoryContent.Items.Clear();

            string[] aDirectoryContentPaths = parent.XBMC.GetDirectoryContentPaths(tvMediaShares.SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
            string[] aDirectoryContentNames = parent.XBMC.GetDirectoryContentNames(tvMediaShares.SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
            int imgIndex;

            if (cbShareType.Text == "music")
                imgIndex = 1;
            else if (cbShareType.Text == "pictures")
                imgIndex = 2;
            else if (cbShareType.Text == "video")
                imgIndex = 3;
            else
                imgIndex = 4;

            if (aDirectoryContentNames != null)
            {
                for (int x = 1; x < aDirectoryContentNames.Length; x++)
                {
                    if (aDirectoryContentNames[x] != null)
                    {
                        lvDirectoryContent.Items.Add(aDirectoryContentNames[x]);
                        lvDirectoryContent.Items[x - 1].ImageIndex = imgIndex;
                        lvDirectoryContent.Items[x - 1].ToolTipText = aDirectoryContentPaths[x];
                    }
                }
            }
        }

        private void TestConnectivity()
        {
            if (!parent.XBMC.IsConnected())
                this.Dispose();
        }

        private void AddDirectoryContentToPlaylist(bool play, bool enqueue, bool recursive)
        {
            this.TestConnectivity();
            if (play) parent.XBMC.ClearPlayList();
            parent.XBMC.AddDirectoryContentToPlaylist(tvMediaShares.SelectedNode.ToolTipText, cbShareType.Text, recursive);
            if (play) parent.XBMC.PlayPlaylistSong(0);
            if (Settings.Default.playlistOpened) parent.Playlist.RefreshPlaylist();
        }

        private void AddFilesToPlaylist(bool play)
        {
            this.TestConnectivity();
            if (play) parent.XBMC.ClearPlayList();

            foreach (ListViewItem item in lvDirectoryContent.SelectedItems)
                parent.XBMC.AddFilesToPlaylist(item.ToolTipText);

            if (play) parent.XBMC.PlayPlaylistSong(0);
            if (Settings.Default.playlistOpened) parent.Playlist.RefreshPlaylist();
        }

        private void AddFilesToPlaylist()
        {
            this.AddFilesToPlaylist(false);
        }

        private void cbShareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateShareBrowser();
        }

        private void tvMediaShares_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PopulateFileBrowser();
        }

        private void lvDirectoryContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            parent.XBMC.PlayMedia(lvDirectoryContent.Items[lvDirectoryContent.FocusedItem.Index].ToolTipText);
        }

        private void MediaBrowserF1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.shareBrowserOpened = false;
            this.Dispose();
        }

        private void tvMediaShares_MouseDown(object sender, MouseEventArgs e)
        {
            tvMediaShares.SelectedNode = tvMediaShares.GetNodeAt(e.X, e.Y);

            if (e.Button != MouseButtons.Right && tvMediaShares.SelectedNode != null)
                this.ExpandSharedDirectory();
        }

        private void tsiCollapseAll_Click(object sender, EventArgs e)
        {
            tvMediaShares.CollapseAll();
        }

        //START Playlis controls
        private void tsiPlayRecursive_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(true, false, true);
        }

        private void tsiEnqueueRecursive_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(false, true, true);
        }

        private void tsiEnqueueFiles_Click(object sender, EventArgs e)
        {
            this.AddFilesToPlaylist();
        }

        private void cmsFiles_Click(object sender, EventArgs e)
        {
            this.AddFilesToPlaylist();
            if (Settings.Default.playlistOpened)
                parent.Playlist.RefreshPlaylist();
        }

        private void tsiPlayFiles_Click(object sender, EventArgs e)
        {
            this.AddFilesToPlaylist(true);
        }
        //END Playlist controls
    }
}
