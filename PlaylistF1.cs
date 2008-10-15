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
        private bool isDragging = false;
        private int clickOffsetX, clickOffsetY;

        public PlaylistF1()
        {
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();
            PopulatePlaylist();
            timerUpdateSelection.Enabled = true;
            XBMC.GetXbmcProperties();
            UpdatePlaylistSelection();
        }

        private void PopulatePlaylist()
        {
            lbPlaylist.Items.Clear();
            string[] aPlaylistEntries = XBMC.GetPlaylist(true);

            if (aPlaylistEntries != null)
            {
                for (int x = 1; x < aPlaylistEntries.Length; x++)
                    lbPlaylist.Items.Add(x + ". " + aPlaylistEntries[x]);
            }
        }

        private void PlaySelected(object sender, EventArgs e)
        {
            string selectedEntry = GetSelectedPlaylistEntry();

            if (selectedEntry != null)
                XBMC.PlayFile(selectedEntry);
        }

        private void RemoveSelected(object sender, EventArgs e)
        {
            string selectedEntry = GetSelectedPlaylistEntry();

            if (selectedEntry != null)
            {
                XBMC.Request("RemoveFromPlaylist(" + lbPlaylist.SelectedIndex + ")");
                PopulatePlaylist();
            }
        }

        private void pbClearPlaylist_Click(object sender, EventArgs e)
        {
            XBMC.Request("ClearPlayList(GetCurrentPlaylist)");
            PopulatePlaylist();
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
            string[] aPlaylistEntry = XBMC.Request("GetPlaylistSong(" + lbPlaylist.SelectedIndex + ")");
            return (aPlaylistEntry != null) ? aPlaylistEntry[1] : null;
        }

        private void UpdatePlaylistSelection()
        {
            int currentPlaylistSong = Convert.ToInt32(XBMC.GetNowPlayingInfo("songno"));
            lbPlaylist.SelectedIndex = currentPlaylistSong;
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
            XBMC.GetXbmcProperties();
            if(!lbPlaylist.Focused && !XBMC.IsNotPlaying() && XBMC.IsConnected())
                UpdatePlaylistSelection();
        }
    }
}
