using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XBMC;
using XBMControl;
using XBMControl.Properties;

namespace XBMControl
{
    public partial class MainForm : Form
    {
        internal XBMC_Communicator XBMC;
        private ConfigurationF1 configForm;
        private MediaBrowserF1 ShareBrowser;
        private PlaylistF1 Playlist;
        private NavigatorF1 Navigator;
        private bool resetToDefault = false;
        private int updateTimerConnected = 800;
        private int updateTimerDisconnected = 5000;
        private int updateTimerPowerSaver = 30000;
        private ImageButton buttonVolumeDown;
        private ImageButton buttonVolumeUp;
        private ImageButton buttonRewind;
        private ImageButton buttonStop;
        private ImageButton buttonPausePlay;
        private ImageButton buttonForward;
        
        public MainForm()
        {
            InitializeComponent();
            XBMC = new XBMC_Communicator();
            XBMC.SetIp("");
            XBMC.SetConnectionTimeout(3000);
            XBMC.SetCredentials("", "");
            Initialize();

            buttonVolumeDown = new ImageButton();
            buttonVolumeDown.Image = XBMControl.Properties.Resources.vol_down32x32;
            buttonVolumeDown.Location = new Point(155, 160);
            buttonVolumeDown.Size = new Size(32, 32);
            //Hook up into click event
            buttonVolumeDown.Click += new EventHandler(bVolDown_Click);
            this.Controls.Add(buttonVolumeDown);

            buttonVolumeUp = new ImageButton();
            buttonVolumeUp.Image = XBMControl.Properties.Resources.vol_up32x32;
            buttonVolumeUp.Location = new Point(198, 160);
            buttonVolumeUp.Size = new Size(32, 32);
            //Hook up into click event
            buttonVolumeUp.Click += new EventHandler(bVolUp_Click);
            this.Controls.Add(buttonVolumeUp);

            buttonRewind = new ImageButton();
            buttonRewind.Image = XBMControl.Properties.Resources.rewind;
            buttonRewind.Location = new Point(3, 220);
            buttonRewind.Size = new Size(48, 48);
            //Hook up into click event
            buttonRewind.Click += new EventHandler(buttonRewind_Click);
            this.Controls.Add(buttonRewind);

            buttonStop = new ImageButton();
            buttonStop.Image = XBMControl.Properties.Resources.stop;
            buttonStop.Location = new Point(57, 220);
            buttonStop.Size = new Size(48, 48);
            //Hook up into click event
            buttonStop.Click += new EventHandler(buttonStop_Click);
            this.Controls.Add(buttonStop);

            buttonPausePlay = new ImageButton();
            buttonPausePlay.Image = XBMControl.Properties.Resources.play_pause;
            buttonPausePlay.Location = new Point(125, 220);
            buttonPausePlay.Size = new Size(48, 48);
            //Hook up into click event
            buttonPausePlay.Click += new EventHandler(buttonPausePlay_Click);
            this.Controls.Add(buttonPausePlay);

            buttonForward = new ImageButton();
            buttonForward.Image = XBMControl.Properties.Resources.forward;
            buttonForward.Location = new Point(189, 220);
            buttonForward.Size = new Size(48, 48);
            //Hook up into click event
            buttonForward.Click += new EventHandler(buttonForward_Click);
            this.Controls.Add(buttonForward);
        }

        private void Initialize()
        {
            updateTimer.Enabled = true;
            if (XBMC.Status.IsConnected())
            {
                if (!XBMC.Status.WebServerEnabled())
                {
                    MessageBox.Show("Web Server Disabled");
                    this.Dispose();
                }
            }
            else
            {
                if (XBMC.GetIp() == "")
                    MessageBox.Show("IP Not configured");
                else
                    MessageBox.Show("Unable to connect");
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            this.ShareBrowser = new MediaBrowserF1(this);
            this.ShareBrowser.Show();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Playlist = new PlaylistF1(this);
            this.Playlist.Show();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            this.Navigator = new NavigatorF1(this);
            this.Navigator.Show();
       }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            configForm = new ConfigurationF1(this);
            configForm.Show();

        }

        private void SetNowPlayingTimePlayed(bool resetToDefault)
        {
            lTimePlayed.Text = (resetToDefault) ? "00:00" : this.XBMC.NowPlaying.Get("time");
        }

        private void GetNowPlayingSongInfo(bool resetToDefault)
        {
            if (!this.XBMC.Status.IsPlaying() && !this.XBMC.Status.IsPaused())
            {
                lArtistSong.Text = "No media playing";
                lBitrate.Text = "";
                lSamplerate.Text = "";
                //lArtist.Text = "";
                //lTitle.Text = "";
                //lAlbum.Text = "";
            }
            else if (this.XBMC.Status.IsNewMediaPlaying())
            {
                //if (this.Playlist != null) this.Playlist.RefreshPlaylist();
                //Image coverArt = this.XBMC.NowPlaying.GetCoverArt();
                //pbThumbnail.Image = (coverArt == null) ? Resources.XBMClogo : coverArt;
                string year = (this.XBMC.NowPlaying.Get("year") == null) ? "" : " [" + this.XBMC.NowPlaying.Get("year") + "]";
                lBitrate.Text = this.XBMC.NowPlaying.Get("bitrate");
                lSamplerate.Text = this.XBMC.NowPlaying.Get("samplerate");
                string genre = (this.XBMC.NowPlaying.Get("genre") == null) ? "" : " [" + this.XBMC.NowPlaying.Get("genre") + "]";
                string artistLable = (this.XBMC.NowPlaying.Get("artist") == "" || this.XBMC.NowPlaying.Get("artist") == null) ? "" : this.XBMC.NowPlaying.Get("artist") + " - ";
                lArtistSong.Text = artistLable + this.XBMC.NowPlaying.Get("title");
                //lArtist.Text = this.XBMC.NowPlaying.Get("artist") + genre;
                //lTitle.Text = this.XBMC.NowPlaying.Get("title") + " [" + this.XBMC.NowPlaying.Get("duration") + "]";
                //lAlbum.Text = this.XBMC.NowPlaying.Get("album") + year;
                tbVolume.Value = XBMC.Status.GetVolume(true);

                if (XBMC.Playlist.getPlayListSize() == 0)
                    XBMC.Playlist.Get(false, true);
                //pLastFmButtons.Visible = (this.XBMC.Status.LastFmEnabled()) ? true : false;
            }
        }

        internal void UpdateData()
        {
            resetToDefault = (!XBMC.Status.IsConnected()) ? true : false;

            if (XBMC.Status.IsConnected())
            {
                lConnected.Text = "Connected";

                XBMC.Status.Refresh();

                if (!XBMC.Status.IsNotPlaying())
                {
                    if (XBMC.getPowerSaver() != true)
                        updateTimer.Interval = updateTimerConnected;
                    else
                        updateTimer.Interval = updateTimerPowerSaver;
                    updateTimer.Enabled = true;

                    tbProgress.Value = XBMC.Status.GetProgress();
                    tbVolume.Value = XBMC.Status.GetVolume();
                    SetNowPlayingTimePlayed(resetToDefault);
                    GetNowPlayingSongInfo(resetToDefault);
                    lConnected.Text = "Connected";
                    lbPlaylistSize.Text = Convert.ToString(XBMC.Playlist.getPlayListSize()) + " entries in playlist";
                    //ShowNowPlayingInfo(resetToDefault);
                    //ShowPlayStatusInfo();
                    //SetMediaTypeImage();

                    //Set control button states
                    //bOpen.BackgroundImage = (shareBrowserOpened) ? Resources.button_open_click : Resources.button_open;
                    //bPause.BackgroundImage = (XBMC.Status.IsPaused()) ? Resources.button_pause_click : Resources.button_pause;
                    //bPlay.BackgroundImage = (XBMC.Status.IsPlaying()) ? Resources.button_play_click : Resources.button_play;
                    //bStop.BackgroundImage = (XBMC.Status.IsNotPlaying()) ? Resources.button_stop_click : Resources.button_stop;
                    //bMute.BackgroundImage = (XBMC.Status.IsMuted()) ? Resources.button_mute_click : Resources.button_mute;
                    //bLastFmHate.Visible = (XBMC.Status.IsPlaying("lastfm")) ? true : false;
                    //bLastFmLove.Visible = (XBMC.NowPlaying.GetMediaType() == "Audio") ? true : false;
                    //bPlaylist.BackgroundImage = (Settings.Default.playlistOpened && Playlist != null) ? Resources.button_playlist_click : Resources.button_playlist;
                    //bNavigator.BackgroundImage = (Settings.Default.NavigatorOpened && Navigator != null) ? Resources.button_remote_click : Resources.button_remote;
                }
            }
            else
            {
                updateTimer.Interval = updateTimerDisconnected;
                updateTimer.Enabled = true;

                lConnected.Text = "Unconnected";

                //SetControlsEnabled(false);
                //this.ShowConnectionInfo();
                //ShowNowPlayingInfo(resetToDefault);
                //SetNowPlayingTimePlayed(resetToDefault);
                //SetMediaTypeImage();
                //bPause.BackgroundImage = Resources.button_pause;
                //bPlay.BackgroundImage = Resources.button_play;
                //bMute.BackgroundImage = Resources.button_mute;

            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void bVolDown_Click(object sender, EventArgs e)
        {
            int tempVolume;

            tempVolume = XBMC.Status.GetVolume();

            tempVolume -= 10;
            if (tempVolume < 0)
                tempVolume = 0;

            if (XBMC.Status.IsConnected())
            {
                XBMC.Controls.SetVolume(tempVolume);
            }

        }

        private void bVolUp_Click(object sender, EventArgs e)
        {
            int tempVolume;

            tempVolume = XBMC.Status.GetVolume();

            tempVolume += 10;
            if (tempVolume > 99)
                tempVolume = 99;

            if (XBMC.Status.IsConnected())
            {
                XBMC.Controls.SetVolume(tempVolume);
            }
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            XBMC.Controls.Stop();
        }

        private void buttonPausePlay_Click(object sender, EventArgs e)
        {
            if (XBMC.Status.IsNotPlaying())
            {
                if (XBMC.Playlist.getPlayListSize() > 0)
                    XBMC.Playlist.PlaySong(0);
            }
            else
            {
                XBMC.Controls.Play();
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            XBMC.Controls.Next();
        }

        private void buttonRewind_Click(object sender, EventArgs e)
        {
            XBMC.Controls.Previous();
        }
    }
}