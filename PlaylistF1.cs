using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;
using XBMControl.Properties;

namespace XBMControl
{
    public partial class PlaylistF1 : Form
    {
        XBMCcomm XBMC;
        MediaBrowserF1 ShareBrowser;
        private bool isDragging = false;
        private int clickOffsetX, clickOffsetY;

        public PlaylistF1()
        {
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();

            if (XBMC.IsConnected())
            {
                XBMC.GetXbmcProperties();
                PopulatePlaylist();
                UpdatePlaylistSelection();
                timerUpdateSelection.Enabled = true;
            }
        }

        private void PopulatePlaylist()
        {
            lbPlaylist.Items.Clear();

            if(XBMC.IsConnected())
            {
                string surrentPlaylistType = (XBMC.GetNowPlayingMediaType() == "Video") ? "video" : "";
                XBMC.SetPlaylist(surrentPlaylistType);

                string[] aPlaylistEntries = XBMC.GetPlaylist(true);

                if (aPlaylistEntries != null)
                {
                    for (int x = 1; x < aPlaylistEntries.Length; x++)
                    {
                        if (aPlaylistEntries[x] != "")
                        {
                            lbPlaylist.Items.Add(x + ". " + aPlaylistEntries[x]);
                        }
                    }
                }
            }
        }

        private void PlaySelected(object sender, EventArgs e)
        {
            if(XBMC.IsConnected())
            {
                string selectedEntry = GetSelectedPlaylistEntry();

                if (selectedEntry != null)
                    XBMC.PlayFile(selectedEntry);
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void PlaySelected()
        {
            this.PlaySelected(null, null);
        }

        private void RemoveSelected(object sender, EventArgs e)
        {
            if (XBMC.IsConnected())
            {
                string selectedEntry = GetSelectedPlaylistEntry();
                if (selectedEntry != null)
                {
                    XBMC.Request("RemoveFromPlaylist(" + lbPlaylist.SelectedIndex + ")");
                    PopulatePlaylist();
                }
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void RemoveSelected()
        {
            RemoveSelected(null, null);
        }

        private void pbClearPlaylist_Click(object sender, EventArgs e)
        {
            if (XBMC.IsConnected())
            {
                XBMC.Request("ClearPlayList(GetCurrentPlaylist)");
                PopulatePlaylist();
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void lbPlaylist_MouseDown(object sender, MouseEventArgs e)
        {
            cmsPlaylist.Enabled = (e.Button == MouseButtons.Right && lbPlaylist.SelectedIndex == -1)? false : true ;
        }

        private void lbPlaylist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PlaySelected(sender, e);
        }

        private string GetSelectedPlaylistEntry()
        {
            if (XBMC.IsConnected())
            {
                string[] aPlaylistEntry = XBMC.Request("GetPlaylistSong(" + lbPlaylist.SelectedIndex + ")");
                return (aPlaylistEntry != null) ? aPlaylistEntry[1] : null;
            }
            else
            {
                lbPlaylist.Items.Clear();
                return null;
            }
        }

        private void UpdatePlaylistSelection()
        {
            if (XBMC.IsConnected())
            {
                int currentPlaylistSong = Convert.ToInt32(XBMC.GetNowPlayingInfo("songno"));
                if (currentPlaylistSong != 0)
                    lbPlaylist.SelectedIndex = currentPlaylistSong;
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //START FAKE DRAG DROP
        private void pToolbar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            clickOffsetX = e.X;
            clickOffsetY = e.Y;
        }

        private void pToolbar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pToolbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                this.Left = e.X + this.Left - clickOffsetX;
                this.Top = e.Y + this.Top - clickOffsetY;
            }
        }
        //END FAKE DRAG DROP

        private void PlaylistF1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void timerUpdateSelection_Tick(object sender, EventArgs e)
        {
            if (XBMC.IsConnected())
            {
                XBMC.GetXbmcProperties();
                if (!lbPlaylist.Focused)
                {
                    PopulatePlaylist();
                    if (!XBMC.IsNotPlaying()) UpdatePlaylistSelection();
                }
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void lbPlaylist_KeyUp(object sender, KeyEventArgs e)
        {
            if (XBMC.IsConnected())
            {
                if (e.KeyData.ToString() == "Delete")
                {
                    this.RemoveSelected();
                    this.PopulatePlaylist();
                }
                else if (e.KeyData.ToString() == "Return")
                    this.PlaySelected();
            }
            else
                lbPlaylist.Items.Clear();
        }

        private void pbBrowseShares_Click(object sender, EventArgs e)
        {
            ShareBrowser = new MediaBrowserF1();
            ShareBrowser.Show();
        }
    }
}
