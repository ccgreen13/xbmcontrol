using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        XBMCcomm XBMC;
        static string XBMCip        = "10.1.1.156:80";
        string XBMCurl              = XBMCip + "/xbmcCmds/xbmcHttp?";
        public string[,] maNowPlayingInfo = new string[50,2];

        public MainForm()
        {
            XBMC = new XBMCcomm();
            XBMC.SetIp(XBMCip);
            InitializeComponent();
            Initialize();
        }
       
        private void Initialize()
        {
            if (XBMC.IsConnected())
            {
                UpdateApp();
                UpdateAppLong();
                timerShort.Enabled = true;
                timerLong.Enabled  = true;
            }
            else
                MessageBox.Show("Could not connect to XBMC. Please check your ip configuration.");
        }

        private void UpdateApp()
        {
            bool defaults = (XBMC.IsPlaying()) ? false : true;

            SetProgressPosition();
            SetVolumePosition();
            SetNowPlayingTimePlayed(defaults);
        }


        private void UpdateAppLong()
        {
            bool defaults = (XBMC.IsPlaying()) ? false : true;

            ShowMediaTypeImage(defaults);
            SetNowPlayingBitrate(defaults);
            SetNowPlayingSamplerate(defaults);
            SetNowPlayingSongInfo(defaults);
            SetNowPlayingThumbnail(defaults);
        }

        private void SetProgressPosition()
        {
            string[] aCurProgress = XBMC.Request("GetPercentage");
            int newProgressPosition = (aCurProgress.Length > 1 && aCurProgress[1] != "Error") ? Convert.ToInt32(aCurProgress[1]) : 1;
            if (newProgressPosition > 0 && newProgressPosition < 101)
                tbProgress.Value = newProgressPosition;
        }

        private void SetProgress()
        {
            XBMC.Request("SeekPercentage", Convert.ToString(tbProgress.Value));    
        }

        private void SetVolumePosition()
        {
            string[] aCurVolume = XBMC.Request("GetVolume");
            int newVolumePosition = (aCurVolume.Length > 1 && aCurVolume[1] != "Error") ? Convert.ToInt32(aCurVolume[1]) : 0 ;
            tbVolume.Value = newVolumePosition;
        }

        private void SetVolume()
        {
            XBMC.Request("SetVolume", Convert.ToString(tbVolume.Value));
        }

        private void SetNowPlayingTimePlayed(bool defaults)
        {
            lTimePlayed.Text = (defaults)? "00:00" : XBMC.GetNowPlayingInfo("time");
        }

        private void SetNowPlayingBitrate(bool defaults)
        {
            lBitrate.Text = (defaults) ? "" : XBMC.GetNowPlayingInfo("bitrate");
        }

        private void SetNowPlayingSamplerate(bool defaults)
        {
            lSamplerate.Text = (defaults) ? "" : XBMC.GetNowPlayingInfo("samplerate");
        }

        private void SetNowPlayingThumbnail(bool defaults)
        {
            MemoryStream thumbNailStream = XBMC.GetThumbnail();
            pbThumbnail.Image = (defaults || thumbNailStream == null) ? Properties.Resources.XBMClogo : new Bitmap(thumbNailStream);
        }

        private void SetNowPlayingSongInfo(bool defaults)
        {
            if (defaults)
            {
                lArtistSong.Text = "No music playing";
                lArtist.Text     = "";
                lTitle.Text      = "";
                lDuration.Text   = "";
                lAlbum.Text      = "";
            }
            else
            {
                string year      = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " (" + XBMC.GetNowPlayingInfo("year") + ")";
                lArtistSong.Text = XBMC.GetNowPlayingInfo("artist") + " - " + XBMC.GetNowPlayingInfo("title");
                lArtist.Text     = XBMC.GetNowPlayingInfo("artist");
                lTitle.Text      = XBMC.GetNowPlayingInfo("title");
                lDuration.Text   = XBMC.GetNowPlayingInfo("duration");
                lAlbum.Text      = XBMC.GetNowPlayingInfo("album") + year;
            }
        }

        private void ShowMediaTypeImage(bool defaults)
        {
            string mediaType = XBMC.GetNowPlayingInfo("type");
            string mediaFile = XBMC.GetNowPlayingInfo("filename");

            pbLastFM.Visible    = (mediaFile.Substring(0, 6) == "lastfm") ? true : false;
            pbMediaType.Visible = (defaults) ? false : true;

            if (mediaType == "Audio")
                pbMediaType.Image = Properties.Resources.audio_cd_32x32;
            else if (mediaType == "Video")
                pbMediaType.Image = Properties.Resources.video_32x32;
            else if (mediaType == "Picture")
                pbMediaType.Image = Properties.Resources.pictures_32x32;
        }


//------------------START EVENTS-------------------------

        private void timerLong_Tick(object sender, EventArgs e)
        {
            UpdateAppLong();
        }

        private void timerShort_Tick(object sender, EventArgs e)
        {
            UpdateApp();
        }

        private void tbProgress_MouseUp(object sender, MouseEventArgs e)
        {
            SetProgress();
            timerLong.Enabled = true;
        }

        private void tbProgress_MouseDown(object sender, MouseEventArgs e)
        {
            timerLong.Enabled = false;
        }

        private void tbVolume_ValueChanged(object sender, EventArgs e)
        {
            SetVolume();
        }

        private void bPrevious_Click(object sender, EventArgs e)
        {
            XBMC.Request("PlayPrev");
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            XBMC.Request("PlayNext");
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            XBMC.Request("Stop");
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            XBMC.Request("Pause");
        }

        private void bMute_Click(object sender, EventArgs e)
        {
            XBMC.Request("Mute");
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingInfo("playstatus") == "Paused")
                XBMC.Request("Pause");
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }
    }
}
