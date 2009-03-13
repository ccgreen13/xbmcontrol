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
        private Int16[,] arrayName;
        
        private Int16[,] buttons240x240 = new Int16[6,4] {
        // 240x320
            {003, 150, 32, 32},   // Volume down   
            {041, 150, 32, 32},   // Volume up
            {079, 150, 32, 32},   // Rewind
            {117, 150, 32, 32},   // Stop
            {155, 150, 32, 32},   // Play/Pause
            {194, 150, 32, 32}    // Foward
        };

        private Int16[,] buttons320x320 = new Int16[6, 4] {
        // 240x320
            {220, 180, 32, 32},   // Volume down   
            {263, 180, 32, 32},   // Volume up
            {  3, 200, 48, 48},   // Rewind
            { 57, 200, 48, 48},   // Stop
            {111, 200, 48, 48},   // Play/Pause
            {165, 200, 48, 48}    // Foward
        };

        private Int16[,] buttons480x480 = new Int16[6, 4] {
        // 480x480
            {306, 265, 64, 64},   // Volume down   
            {376, 265, 64, 64},   // Volume up
            {008, 300, 64, 64},   // Rewind
            {078, 300, 64, 64},   // Stop
            {148, 300, 64, 64},   // Play/Pause
            {218, 300, 64, 64}    // Foward
        };

        private Int16[,] buttons240x320 = new Int16[6, 4] {
        // 240x320
            {155, 160, 32, 32},   // Volume down   
            {198, 160, 32, 32},   // Volume up
            {  3, 220, 48, 48},   // Rewind
            { 57, 220, 48, 48},   // Stop
            {125, 220, 48, 48},   // Play/Pause
            {189, 220, 48, 48}    // Foward
        };

        private Int16[,] buttons640x480 = new Int16[6, 4] {
        // 640x480
            {274, 270, 96, 96},   // Volume down   
            {376, 270, 96, 96},   // Volume up
            {010, 400, 96, 96},   // Rewind
            {112, 400, 96, 96},   // Stop
            {214, 400, 96, 96},   // Play/Pause
            {316, 400, 96, 96}    // Foward
        };

        public MainForm()
        {
            InitializeComponent();
            XBMC = new XBMC_Communicator();
            XBMC.SetIp("");
            XBMC.SetConnectionTimeout(3000);
            XBMC.SetCredentials("", "");
            Initialize();

            if (Screen.PrimaryScreen.Bounds.Height == 240 && Screen.PrimaryScreen.Bounds.Width == 240)
                arrayName = buttons240x240;
            else if (Screen.PrimaryScreen.Bounds.Height == 320 && Screen.PrimaryScreen.Bounds.Width == 320)
                arrayName = buttons320x320;
            else if (Screen.PrimaryScreen.Bounds.Height == 480 && Screen.PrimaryScreen.Bounds.Width == 480)
                arrayName = buttons480x480;
            else if (Screen.PrimaryScreen.Bounds.Height == 640 && Screen.PrimaryScreen.Bounds.Width == 480)
                arrayName = buttons640x480;
            else
                arrayName = buttons240x320;

            this.Text += " " + Properties.Resources.version;

            buttonVolumeDown = new ImageButton();
            buttonVolumeDown.Image = XBMControl.Properties.Resources.vol_down;
            buttonVolumeDown.Location = new Point(arrayName[0, 0], arrayName[0, 1]);
            buttonVolumeDown.Size = new Size(arrayName[0, 2], arrayName[0, 3]);
            //Hook up into click event
            buttonVolumeDown.Click += new EventHandler(bVolDown_Click);
            this.Controls.Add(buttonVolumeDown);

            buttonVolumeUp = new ImageButton();
            buttonVolumeUp.Image = XBMControl.Properties.Resources.vol_up;
            buttonVolumeUp.Location = new Point(arrayName[1, 0], arrayName[1, 1]);
            buttonVolumeUp.Size = new Size(arrayName[1, 2], arrayName[1, 3]);
            //Hook up into click event
            buttonVolumeUp.Click += new EventHandler(bVolUp_Click);
            this.Controls.Add(buttonVolumeUp);

            buttonRewind = new ImageButton();
            buttonRewind.Image = XBMControl.Properties.Resources.rewind;
            buttonRewind.Location = new Point(arrayName[2, 0], arrayName[2, 1]);
            buttonRewind.Size = new Size(arrayName[2, 2], arrayName[2, 3]);
            //Hook up into click event
            buttonRewind.Click += new EventHandler(buttonRewind_Click);
            this.Controls.Add(buttonRewind);

            buttonStop = new ImageButton();
            buttonStop.Image = XBMControl.Properties.Resources.stop;
            buttonStop.Location = new Point(arrayName[3, 0], arrayName[3, 1]);
            buttonStop.Size = new Size(arrayName[3, 2], arrayName[3, 3]);
            //Hook up into click event
            buttonStop.Click += new EventHandler(buttonStop_Click);
            this.Controls.Add(buttonStop);

            buttonPausePlay = new ImageButton();
            buttonPausePlay.Image = XBMControl.Properties.Resources.play_pause;
            buttonPausePlay.Location = new Point(arrayName[4, 0], arrayName[4, 1]);
            buttonPausePlay.Size = new Size(arrayName[4, 2], arrayName[4, 3]);
            //Hook up into click event
            buttonPausePlay.Click += new EventHandler(buttonPausePlay_Click);
            this.Controls.Add(buttonPausePlay);

            buttonForward = new ImageButton();
            buttonForward.Image = XBMControl.Properties.Resources.forward;
            buttonForward.Location = new Point(arrayName[5, 0], arrayName[5, 1]);
            buttonForward.Size = new Size(arrayName[5, 2], arrayName[5, 3]);
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
            }
            else if (this.XBMC.Status.IsNewMediaPlaying())
            {
                string year = (this.XBMC.NowPlaying.Get("year") == null) ? "" : " [" + this.XBMC.NowPlaying.Get("year") + "]";
                lBitrate.Text = this.XBMC.NowPlaying.Get("bitrate");
                lSamplerate.Text = this.XBMC.NowPlaying.Get("samplerate");
                string genre = (this.XBMC.NowPlaying.Get("genre") == null) ? "" : " [" + this.XBMC.NowPlaying.Get("genre") + "]";
                string artistLable = (this.XBMC.NowPlaying.Get("artist") == "" || this.XBMC.NowPlaying.Get("artist") == null) ? "" : this.XBMC.NowPlaying.Get("artist") + " - ";
                lArtistSong.Text = artistLable + this.XBMC.NowPlaying.Get("title");
                tbVolume.Value = XBMC.Status.GetVolume(true);

                if (XBMC.Playlist.getPlayListSize() == 0)
                    XBMC.Playlist.Get(false, true);
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
                }
            }
            else
            {
                updateTimer.Interval = updateTimerDisconnected;
                updateTimer.Enabled = true;

                lConnected.Text = "Unconnected";
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