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
        TreeNode tNode;

        public MediaBrowserF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
            this.PopulateDirectoryBrowser();
            this.Owner = parent;

            cbShareType.SelectedIndex = 0;
        }

        private TreeView ActiveTreeView()
        {
            TreeView activeTreeView = null;
            string tabName = tcMediaBrowser.SelectedTab.Name.ToString();

            if (tabName == "tabShares")
                activeTreeView = tvMediaShares;
            else if (tabName == "tabArtists")
                activeTreeView = tvArtists;
            else if (tabName == "tabAlbums")
                activeTreeView = tvAlbums;

            return activeTreeView;
        }

        private ListView ActiveListView()
        {
            ListView activeListView = null;
            string tabName = tcMediaBrowser.SelectedTab.Name.ToString();

            if (tabName == "tabShares")
                activeListView = lvDirectorySongs;
            else if (tabName == "tabArtists")
                activeListView = lvArtistSongs;
            else if (tabName == "tabAlbums")
                activeListView = lvAlbumSongs;

            return activeListView;
        }

        private string ActiveTab()
        {
            return tcMediaBrowser.SelectedTab.Name.ToString();
        }

        private void PopulateDirectoryBrowser(string searchString)
        {
            ActiveTreeView().Nodes.Clear();
            ActiveListView().Items.Clear();

            string[] aDirectories = null;
            string[] aDirectoryIds = null;

            if (this.ActiveTab() == "tabShares")
            {
                aDirectories = parent.XBMC.Media.GetShares(cbShareType.Text);
                aDirectoryIds = parent.XBMC.Media.GetShares(cbShareType.Text, true);
            }
            else if (this.ActiveTab() == "tabArtists")
            {
                aDirectories = parent.XBMC.Database.GetArtists(searchString);
                aDirectoryIds = parent.XBMC.Database.GetArtistIds(searchString);
            }
            else if (this.ActiveTab() == "tabAlbums" || (this.ActiveTab() == "tabArtists" && ActiveTreeView().SelectedNode.Parent == null))
            {
                aDirectories = parent.XBMC.Database.GetAlbums(searchString);
                aDirectoryIds = parent.XBMC.Database.GetAlbumIds(searchString);
            }

            if (aDirectories != null && aDirectoryIds != null)
            {
                if (aDirectories.Length > 1 && aDirectoryIds.Length > 1)
                {
                    //MessageBox.Show(aDirectoryIds.Length.ToString() + " - " + aDirectories.Length.ToString());
                    for (int x = 1; x < aDirectories.Length; x++)
                    {
                        tNode = new TreeNode();
                        tNode.Name = aDirectories[x];
                        tNode.Text = aDirectories[x];
                        tNode.ToolTipText = aDirectoryIds[x];
                        ActiveTreeView().Nodes.Add(tNode);
                        ActiveTreeView().Nodes[x - 1].ImageIndex = 0;
                    }
                }
            }
        }

        private void PopulateDirectoryBrowser()
        { 
            this.PopulateDirectoryBrowser(null);
        }

        private void PopulateSongBrowser()
        {
            ActiveListView().Items.Clear();
            this.TestConnectivity();

            string parentDirectoryName = null;
            string directoryName = null;
            string[] aTitles = null;
            string[] aPaths = null;

            if (ActiveTreeView().SelectedNode.Parent != null)
                parentDirectoryName = ActiveTreeView().SelectedNode.Parent.Text.ToString();

            directoryName = ActiveTreeView().SelectedNode.Text.ToString();

            if (this.ActiveTab() == "tabShares")
            {
                aTitles = parent.XBMC.Media.GetDirectoryContentNames(ActiveTreeView().SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
                aPaths = parent.XBMC.Media.GetDirectoryContentPaths(ActiveTreeView().SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
            }
            else if ((this.ActiveTab() == "tabArtists" && ActiveTreeView().SelectedNode.Parent != null) || this.ActiveTab() == "tabAlbums")
            {
                aTitles = parent.XBMC.Database.GetTitlesByAlbumId(ActiveTreeView().SelectedNode.ToolTipText);
                aPaths = parent.XBMC.Database.GetPathsByAlbumId(ActiveTreeView().SelectedNode.ToolTipText);
            }
            else
            {
                aTitles = parent.XBMC.Database.GetTitlesByArtistId(ActiveTreeView().SelectedNode.ToolTipText);
                aPaths = parent.XBMC.Database.GetPathsByArtistId(ActiveTreeView().SelectedNode.ToolTipText);
            }

            if (aTitles != null)
            {
                if (aTitles.Length > 1)
                {
                    for (int x = 1; x < aTitles.Length; x++)
                    {
                        if (aTitles[x] != null)
                        {
                            ActiveListView().Items.Add(aTitles[x]);
                            ActiveListView().Items[x - 1].ImageIndex = 1;
                            ActiveListView().Items[x - 1].ToolTipText = aPaths[x];
                        }
                    }
                }
            }
        }

        private void ExpandSharedDirectory()
        {
            this.TestConnectivity();
            ActiveListView().Items.Clear();

            if (ActiveTreeView().SelectedNode.GetNodeCount(false) == 0)
            {
                string[] aDirectoryContentPaths = parent.XBMC.Media.GetDirectoryContentPaths(ActiveTreeView().SelectedNode.ToolTipText, "/");
                string[] aDirectoryContentNames = parent.XBMC.Media.GetDirectoryContentNames(ActiveTreeView().SelectedNode.ToolTipText, "/");

                if (aDirectoryContentPaths != null)
                {
                    for (int x = 1; x < aDirectoryContentPaths.Length; x++)
                    {
                        if (aDirectoryContentPaths[x] != null)
                        {
                            tNode = new TreeNode();
                            tNode.Name = aDirectoryContentNames[x];
                            tNode.Text = aDirectoryContentNames[x];
                            tNode.ToolTipText = aDirectoryContentPaths[x];
                            ActiveTreeView().SelectedNode.Nodes.Add(tNode);
                            ActiveTreeView().SelectedNode.Nodes[x - 1].ImageIndex = 0;
                        }
                    }

                    ActiveTreeView().SelectedNode.Expand();
                }
            }
        }

        private void ExpandArtistDirectory(string artist)
        {
            this.TestConnectivity();
            ActiveListView().Items.Clear();

            if (ActiveTreeView().SelectedNode.GetNodeCount(false) == 0)
            {
                string[] aAlbums = parent.XBMC.Database.GetAlbumsByArtistId(ActiveTreeView().SelectedNode.ToolTipText);
                string[] albumIds = parent.XBMC.Database.GetAlbumIdsByArtistId(ActiveTreeView().SelectedNode.ToolTipText);

                if (aAlbums != null)
                {
                    for (int x = 1; x < aAlbums.Length; x++)
                    {
                        if (aAlbums.Length > 1)
                        {
                            tNode = new TreeNode();
                            tNode.Name = aAlbums[x];
                            tNode.Text = aAlbums[x];
                            tNode.ToolTipText = albumIds[x];
                            ActiveTreeView().SelectedNode.Nodes.Add(tNode);
                            ActiveTreeView().SelectedNode.Nodes[x - 1].ImageIndex = 0;

                            ActiveTreeView().SelectedNode.Expand();
                        }
                    }
                }
            }
        }

        private void TestConnectivity()
        {
            if (!parent.XBMC.Status.IsConnected())
                this.Dispose();
        }

        private void AddDirectoryContentToPlaylist(bool play, bool enqueue, bool recursive)
        {
            this.TestConnectivity();
            if (play) parent.XBMC.Playlist.Clear();
            parent.XBMC.Playlist.AddDirectoryContent(tvMediaShares.SelectedNode.ToolTipText, cbShareType.Text, recursive);
            if (play) parent.XBMC.Playlist.PlaySong(0);
            if (Settings.Default.playlistOpened) parent.Playlist.RefreshPlaylist();
        }

        private void AddFilesToPlaylist(bool play)
        {
            this.TestConnectivity();

            if (play) parent.XBMC.Playlist.Clear();

            foreach (ListViewItem item in ActiveListView().SelectedItems)
                parent.XBMC.Playlist.AddFilesToPlaylist(item.ToolTipText);

            if (play) parent.XBMC.Playlist.PlaySong(0);
            if (Settings.Default.playlistOpened) parent.Playlist.RefreshPlaylist();
        }

        private void AddFilesToPlaylist()
        {
            this.AddFilesToPlaylist(false);
        }

        private void cbShareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateDirectoryBrowser();
        }

        private void tvMediaShares_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PopulateSongBrowser();
        }

        private void lvDirectoryContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            parent.XBMC.Controls.PlayMedia(lvDirectorySongs.Items[lvDirectorySongs.FocusedItem.Index].ToolTipText);
        }

        private void MediaBrowserF1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.shareBrowserOpened = false;
            this.Dispose();
        }

        private void tvMediaShares_MouseDown(object sender, MouseEventArgs e)
        {
            ActiveTreeView().SelectedNode = ActiveTreeView().GetNodeAt(e.X, e.Y);

            if (e.Button != MouseButtons.Right && ActiveTreeView().SelectedNode != null)
            {
                if (!ActiveTreeView().SelectedNode.IsExpanded)
                {
                    this.ExpandSharedDirectory();
                    this.PopulateSongBrowser();
                }
            }
        }

        private void tvArtists_MouseUp(object sender, MouseEventArgs e)
        {
            ActiveTreeView().SelectedNode = ActiveTreeView().GetNodeAt(e.X, e.Y);

            if (e.Button != MouseButtons.Right && ActiveTreeView().SelectedNode != null )
            {
                if (!ActiveTreeView().SelectedNode.IsExpanded)
                {
                    if (ActiveTreeView().SelectedNode.Parent == null)
                        this.ExpandArtistDirectory(ActiveTreeView().SelectedNode.Text.ToString());
                    else
                        this.PopulateSongBrowser();
                }
            }
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
            parent.Playlist.RefreshPlaylist();
        }

        private void tsiPlayFiles_Click(object sender, EventArgs e)
        {
            this.AddFilesToPlaylist(true);
            parent.Playlist.RefreshPlaylist();
        }

        private void tcMediaBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateDirectoryBrowser();

            if (this.ActiveTab() == "tabArtists")
                tbSearchArtist.Focus();
        }
        //END Playlist controls

        //START HOVER FOCUS

        private void tvMediaShares_MouseHover(object sender, EventArgs e)
        {
            tvMediaShares.Focus();
        }

        private void cbShareType_MouseHover(object sender, EventArgs e)
        {
            cbShareType.Focus();
        }

        private void tvArtists_MouseHover(object sender, EventArgs e)
        {
            tvArtists.Focus();
        }

        private void tvAlbums_MouseHover(object sender, EventArgs e)
        {
            tvAlbums.Focus();
        }

        private void tbSearchArtist_TextChanged(object sender, EventArgs e)
        {
            string searchString = (tbSearchArtist.Text == "") ? null : tbSearchArtist.Text;
            this.PopulateDirectoryBrowser(searchString);
        }

        private void tbSearchAlbum_TextChanged(object sender, EventArgs e)
        {
            string searchString = (tbSearchAlbum.Text == "") ? null : tbSearchAlbum.Text;
            this.PopulateDirectoryBrowser(searchString);
        }

        //END HOVER FOCUS
    }
}
