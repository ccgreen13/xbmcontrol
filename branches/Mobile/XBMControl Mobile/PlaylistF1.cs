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
    public partial class PlaylistF1 : Form
    {
        private MainForm parent;
        private int previouslySelectedItem = 0;

        public PlaylistF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();

            if (parent.XBMC.Status.IsConnected())
            {
                parent.XBMC.Status.Refresh();
                parent.XBMC.Playlist.Set("");
                this.PopulatePlaylist();
                this.UpdatePlaylistSelection();
                this.timerUpdateSelection.Enabled = true;
            }
            this.Owner = parent;
        }

        internal void PopulatePlaylist()
        {
            if (lvPlaylist.SelectedIndices.Count > 0)
                previouslySelectedItem = lvPlaylist.SelectedIndices[0];
            else
                previouslySelectedItem = 0;

            lvPlaylist.BeginUpdate();
            lvPlaylist.Items.Clear();

            if (parent.XBMC.Status.IsConnected())
            {    
                string[] aPlaylistEntries = parent.XBMC.Playlist.Get(true, true);

                if (aPlaylistEntries != null)
                {
                    for (int x = 0; x < aPlaylistEntries.Length; x++)
                    {
                        if (aPlaylistEntries[x] != "")
                        {
                            ListViewItem tempViewItem = new ListViewItem(x + ". " + aPlaylistEntries[x]);
                            tempViewItem.ImageIndex = 0;
                            tempViewItem.Selected = false;
                            lvPlaylist.Items.Add(tempViewItem);
                        }
                    }
                }
                if (lvPlaylist.Items.Count > 0)
                    lvPlaylist.Items[previouslySelectedItem].Selected = true;
            }
            lvPlaylist.EndUpdate();
        }

        internal void PlaySelectedEntry()
        {
            if (lvPlaylist.SelectedIndices.Count == 0)
            {
                parent.XBMC.Playlist.PlaySong(0);
                this.RefreshPlaylist();
                return;
            }

            if (parent.XBMC.Status.IsConnected() && lvPlaylist.SelectedIndices[0] != -1)
            {
                parent.XBMC.Playlist.PlaySong(lvPlaylist.SelectedIndices[0]);
                this.RefreshPlaylist();
            }
        }

        private void timerUpdateSelection_Tick(object sender, EventArgs e)
        {
            if (parent.XBMC.Status.IsConnected())
            {
                //if (!lbPlaylist.Focused)
                {
                    //PopulatePlaylist();
                    if (!parent.XBMC.Status.IsNotPlaying()) UpdatePlaylistSelection();
                }
            }
            else
                lvPlaylist.Items.Clear();
        }

        internal void UpdatePlaylistSelection()
        {
            int tempCount;

            if (parent.XBMC.Status.IsConnected())
            {
                int currentSongNo = Convert.ToInt32(parent.XBMC.NowPlaying.Get("songno", true));

                if (lvPlaylist.Items.Count > 0 && currentSongNo < lvPlaylist.Items.Count)
                {
                    for (tempCount = 0; tempCount < lvPlaylist.Items.Count; tempCount++)
                    {
                        if (!parent.XBMC.Status.IsNotPlaying())
                        {
                            if (tempCount == currentSongNo)
                                lvPlaylist.Items[tempCount].ImageIndex = 1;
                            else
                                lvPlaylist.Items[tempCount].ImageIndex = 0;
                        }
                    }
                }
            }
            else
                lvPlaylist.Items.Clear();
        }

        internal void RefreshPlaylist(object sender, EventArgs e)
        {
            //this.PopulatePlaylist();
            this.UpdatePlaylistSelection();
        }

        internal void RefreshPlaylist()
        {
            this.RefreshPlaylist(null, null);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            timerUpdateSelection.Enabled = false;
            this.Close();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //this.PlaySelectedEntry();
        }

        private void lvPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPlaylist.FocusedItem != null)
                previouslySelectedItem = lvPlaylist.FocusedItem.Index;
            else
                previouslySelectedItem = 0;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.PlaySelectedEntry();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (parent.XBMC.Status.IsConnected())
            {
                parent.XBMC.Playlist.Clear();
                PopulatePlaylist();
            }
            else
                lvPlaylist.Items.Clear();

        }

        private string GetSelectedPlaylistEntry()
        {
            if (parent.XBMC.Status.IsConnected())
            {
                string[] aPlaylistEntry = parent.XBMC.Request("GetPlaylistSong(" + lvPlaylist.SelectedIndices[0] + ")");
                return (aPlaylistEntry != null) ? aPlaylistEntry[0] : null;
            }
            else
            {
                lvPlaylist.Items.Clear();
                return null;
            }
        }

        private void RemoveSelected()
        {
            RemoveSelected(null, null);
        }

        private void RemoveSelected(object sender, EventArgs e)
        {
            if (parent.XBMC.Status.IsConnected())
            {
                string selectedEntry = GetSelectedPlaylistEntry();
                if (selectedEntry != null)
                {
                    parent.XBMC.Playlist.Remove(lvPlaylist.SelectedIndices[0]);
                    PopulatePlaylist();
                }
            }
            else
                lvPlaylist.Items.Clear();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            this.RemoveSelected();
            this.PopulatePlaylist();
            this.UpdatePlaylistSelection();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            this.PopulatePlaylist();
            this.UpdatePlaylistSelection();
        }

    }
}