using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;
using XBMControl.Language;
using XBMControl.Properties;

namespace XBMControl
{
    public partial class MediaBrowserF1 : Form
    {
        XBMCcomm XBMC;
        TreeNode sharesNode;

        public MediaBrowserF1()
        {
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();
            this.PopulateShareBrowser();
        }

        public void PopulateShareBrowser()
        {
            string[] aShares     = XBMC.GetMediaShares(cbShareType.Text);
            string[] aSharePaths = XBMC.GetMediaShares(cbShareType.Text, true);
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
            if (tvMediaShares.SelectedNode.GetNodeCount(false) == 0)
            {
                string[] aDirectoryContentPaths = XBMC.GetDirectoryContentPaths(tvMediaShares.SelectedNode.ToolTipText, "/");
                string[] aDirectoryContentNames = XBMC.GetDirectoryContentNames(tvMediaShares.SelectedNode.ToolTipText, "/");

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
            lvDirectoryContent.Items.Clear();

            string[] aDirectoryContentPaths = XBMC.GetDirectoryContentPaths(tvMediaShares.SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
            string[] aDirectoryContentNames = XBMC.GetDirectoryContentNames(tvMediaShares.SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
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

        private void AddDirectoryContentToPlaylist(bool enqueue, bool play, bool recursive)
        {
            if(!enqueue) XBMC.ClearPlayList();

            XBMC.AddDirectoryContentToPlaylist(tvMediaShares.SelectedNode.ToolTipText, cbShareType.Text, recursive);

            if (play)
            {
                string[] aDirectoryContentPaths = XBMC.GetDirectoryContentPaths(tvMediaShares.SelectedNode.ToolTipText);

                if (aDirectoryContentPaths != null)
                    XBMC.PlayFile(aDirectoryContentPaths[1]);
            }
        }

        private void AddFilesToPlaylist()
        {
            foreach (ListViewItem item in lvDirectoryContent.SelectedItems)
            {
                XBMC.AddFilesToPlaylist(item.ToolTipText);
            }
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
            XBMC.PlayMedia(lvDirectoryContent.Items[lvDirectoryContent.FocusedItem.Index].ToolTipText);
        }

        private void MediaBrowserF1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void tvMediaShares_MouseDown(object sender, MouseEventArgs e)
        {
            tvMediaShares.SelectedNode = tvMediaShares.GetNodeAt(e.X, e.Y);

            if (e.Button != MouseButtons.Right && tvMediaShares.SelectedNode != null)
            {
                if (tvMediaShares.SelectedNode.IsExpanded)
                    tvMediaShares.SelectedNode.Collapse();
                else
                    this.ExpandSharedDirectory();
            }
        }

        private void tsiCollapseAll_Click(object sender, EventArgs e)
        {
            tvMediaShares.CollapseAll();
        }

        //START Playlis controls
        private void tsiPlayFolder_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(false, true, false);
        }

        private void tsiEnqueueFolder_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(true, false, false);
        }

        private void tsiPlayRecursive_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(false, true, true);
        }

        private void tsiEnqueueRecursive_Click(object sender, EventArgs e)
        {
            this.AddDirectoryContentToPlaylist(true, false, true);
        }

        private void tsiEnqueueFiles_Click(object sender, EventArgs e)
        {
            this.AddFilesToPlaylist();
        }

        //END Playlist controls
    }
}
