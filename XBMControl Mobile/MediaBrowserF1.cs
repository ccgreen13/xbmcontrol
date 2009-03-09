using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XBMControl
{
    public partial class MediaBrowserF1 : Form
    {
        MainForm parent;
        TreeNode tNode;
        bool artistDirectorySelected = false;
        bool albumDirectorySelected = false;
        bool rightClick = false;

        public MediaBrowserF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();

            this.PopulateDirectoryBrowser();
            this.Owner = parent;

        }

        private void cbShareType_MouseHover(object sender, EventArgs e)
        {

        }

        private void cbShareType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ShowSongs()
        {
#if false
            if (!rightClick && ActiveTreeView().SelectedNode != null)
            {
                if (!ActiveTreeView().SelectedNode.IsExpanded && ActiveTab() != tabAlbums)
                {
                    if (ActiveTab() == tabShares)
                        ExpandSharedDirectory();
                    else if (ActiveTab() == tabArtists && artistDirectorySelected)
                        ExpandArtistDirectory();
                }

                if (ActiveTreeView().SelectedNode.Nodes.Count == 0 || ActiveTab() == tabShares)
                    PopulateSongBrowser();
            }
#else
            if (ActiveTreeView().SelectedNode.Nodes.Count == 0)
                PopulateSongBrowser();
#endif
        }

        private void SetTreeViewSelection(object sender, KeyEventArgs e)
        {
#if false
            ActiveTreeView().SelectedNode = ActiveTreeView().GetNodeAt(e.X, e.Y);
            rightClick = (e.Button == MouseButtons.Right) ? true : false;

            if (ActiveTreeView().SelectedNode == null)
            {
                albumDirectorySelected = false;
                artistDirectorySelected = false;
            }
            else if (ActiveTreeView().SelectedNode.Parent != null && ActiveTab() == tabArtists)
            {
                albumDirectorySelected = true;
                artistDirectorySelected = false;
            }
            else if (ActiveTab() == tabArtists || ActiveTab() == tabAlbums)
            {
                albumDirectorySelected = false;
                artistDirectorySelected = true;
            }
#else
            //rightClick = (e.Button == MouseButtons.Right) ? true : false;
            albumDirectorySelected = false;
            artistDirectorySelected = false;

#endif
        }

        private void PlaySelectedFiles(object sender, MouseEventArgs e)
        {

        }

        private TreeView ActiveTreeView()
        {
            TreeView activeTreeView = null;
#if false
            string tabName = tcMediaBrowser.SelectedTab.Name.ToString();

            if (tabName == "tabShares")
                activeTreeView = tvMediaShares;
            else if (tabName == "tabArtists")
                activeTreeView = tvArtists;
            else if (tabName == "tabAlbums")
                activeTreeView = tvAlbums;
            else if (tabName == "tabSongs")
                activeTreeView = null;
            else if (tabName == "tabVideos")
                activeTreeView = null;
#else
            activeTreeView = tvMediaShares;
#endif
            return activeTreeView;
        }

#if false
        private ListView ActiveListView()
        {
            ListView activeListView = null;
#if false
            string tabName = tcMediaBrowser.SelectedTab.Name.ToString();

            if (tabName == "tabShares")
                activeListView = lvDirectorySongs;
            else if (tabName == "tabArtists")
                activeListView = lvArtistSongs;
            else if (tabName == "tabAlbums")
                activeListView = lvAlbumSongs;
            else if (tabName == "tabSongs")
                activeListView = lvSongs;
            else if (tabName == "tabVideos")
                activeListView = lvVideos;
#else
            activeListView = lvDirectorySongs;
#endif
            return activeListView;
        }
#endif
        private void PopulateDirectoryBrowser(string searchString)
        {
            ActiveTreeView().Nodes.Clear();
            //ActiveListView().Items.Clear();

            string[] aDirectories = null;
            string[] aDirectoryIds = null;

            if (parent.XBMC.Status.IsConnected() == false)
                return;
#if false
            if (this.ActiveTab() == tabShares)
            {
                aDirectories = parent.XBMC.Media.GetShares(cbShareType.Text);
                aDirectoryIds = parent.XBMC.Media.GetShares(cbShareType.Text, true);
            }
            else if (this.ActiveTab() == tabArtists)
            {
                aDirectories = parent.XBMC.Database.GetArtists(searchString);
                aDirectoryIds = parent.XBMC.Database.GetArtistIds(searchString);
            }
            else if (this.ActiveTab() == tabAlbums || (this.ActiveTab() == tabArtists && artistDirectorySelected))
            {
                aDirectories = parent.XBMC.Database.GetAlbums(searchString);
                aDirectoryIds = parent.XBMC.Database.GetAlbumIds(searchString);
            }
#else
            aDirectories = parent.XBMC.Media.GetShares("music");
            aDirectoryIds = parent.XBMC.Media.GetShares("music", true);
#endif
            if (aDirectories != null)
            {
                for (int x = 0; x < aDirectories.Length; x++)
                {
                    tNode = new TreeNode();
                   // tNode.Name = aDirectories[x];
                    tNode.Text = aDirectories[x];
                    if (aDirectoryIds != null) tNode.Tag = (object)aDirectoryIds[x];
                    ActiveTreeView().Nodes.Add(tNode);
                    ActiveTreeView().Nodes[x].ImageIndex = 0;
                }
            }
        }

        private void PopulateDirectoryBrowser()
        {
            this.PopulateDirectoryBrowser(null);
        }

        private void ExpandSharedDirectory()
        {
            this.TestConnectivity();
            //ActiveListView().Items.Clear();

            if (ActiveTreeView().SelectedNode.GetNodeCount(false) == 0)
            {
                string[] aDirectoryContentPaths = parent.XBMC.Media.GetDirectoryContentPaths((string)ActiveTreeView().SelectedNode.Tag, "/");
                string[] aDirectoryContentNames = parent.XBMC.Media.GetDirectoryContentNames((string)ActiveTreeView().SelectedNode.Tag, "/");

                if (aDirectoryContentPaths != null)
                {
                    for (int x = 0; x < aDirectoryContentPaths.Length; x++)
                    {
                        if (aDirectoryContentPaths[x] != null && aDirectoryContentPaths[x] != "")
                        {
                            tNode = new TreeNode();
                            //tNode.Name = aDirectoryContentNames[x];
                            tNode.Text = aDirectoryContentNames[x];
                            tNode.Tag = (object)aDirectoryContentPaths[x];
                            ActiveTreeView().SelectedNode.Nodes.Add(tNode);
                            ActiveTreeView().SelectedNode.Nodes[x].ImageIndex = 0;
                        }
                    }

                    ActiveTreeView().SelectedNode.Expand();
                }
            }
        }

        private void PopulateSongBrowser()
        {
            this.TestConnectivity();

            string[] aTitles = null;
            string[] aPaths = null;
            ListViewItem tempItem = null;

            //ActiveListView().Items.Clear();

#if false
            if (ActiveTab() == tabShares)
            {
                aTitles = parent.XBMC.Media.GetDirectoryContentNames(ActiveTreeView().SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
                aPaths = parent.XBMC.Media.GetDirectoryContentPaths(ActiveTreeView().SelectedNode.ToolTipText, "[" + cbShareType.Text + "]");
            }
            else if (ActiveTab() == tabSongs)
            {
                aTitles = parent.XBMC.Database.GetSearchSongTitles(tbSearchSong.Text);
                aPaths = parent.XBMC.Database.GetSearchSongPaths(tbSearchSong.Text);
            }
            else if ((ActiveTab() == tabArtists && albumDirectorySelected) || ActiveTab() == tabAlbums)
                aTitles = parent.XBMC.Database.GetTitlesByAlbumId(ActiveTreeView().SelectedNode.ToolTipText);
            else
                aTitles = parent.XBMC.Database.GetTitlesByArtistId(ActiveTreeView().SelectedNode.ToolTipText);
#else
            aTitles = parent.XBMC.Media.GetDirectoryContentNames((string)ActiveTreeView().SelectedNode.Tag, "[music]");
            aPaths = parent.XBMC.Media.GetDirectoryContentPaths((string)ActiveTreeView().SelectedNode.Tag, "[music]");
#endif

#if false
            if (aTitles != null)
            {
                for (int x = 0; x < aTitles.Length; x++)
                {
                    tempItem = new ListViewItem(aTitles[x]);
                    ActiveListView().Items.Add(tempItem);
                    ActiveListView().Items[x].ImageIndex = 1;
                    if (aPaths != null) ActiveListView().Items[x].Tag = aPaths[x];
                }
            }
#endif
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
            parent.XBMC.Playlist.AddDirectoryContent((string)tvMediaShares.SelectedNode.Tag, "music", recursive);
            if (play) parent.XBMC.Playlist.PlaySong(0);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            AddDirectoryContentToPlaylist(false, true, true);
            MessageBox.Show("Added to Playlist");
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MediaBrowserF1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                ExpandSharedDirectory();
                //ShowSongs();
            }
        }

        private void tvMediaShares_AfterSelect(object sender, TreeViewEventArgs e)
        {
           // ExpandSharedDirectory();
        }

    }
}