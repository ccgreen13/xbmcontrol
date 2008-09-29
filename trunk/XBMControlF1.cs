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
        static string XBMCip         = "10.1.1.156";
        string XBMCurl               = XBMCip + "/xbmcCmds/xbmcHttp?";
        string[,] maNowPlayingInfo   = new string[50,2];
        string mediaCurrentlyPlaying = null;
        bool pausedMessageShowed     = false;
        bool resetToDefault          = false;

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
                UpdateData();
                UpdateDataLong();
                timerShort.Enabled = true;
                timerLong.Enabled  = true;
            }
            else
                notifyIcon1.ShowBalloonTip(2000, "XBMControl", "Could not connect to XBMC with ip " + XBMCip, ToolTipIcon.Info);
        }

        private void UpdateData()
        {
            resetToDefault = (XBMC.IsPlaying()) ? false : true;
            SetProgressPosition();
            SetVolumePosition();
            SetNowPlayingTimePlayed(resetToDefault);
        }

        private void UpdateDataLong()
        {
            ShowNowPlayingBalloonTip();
            ShowPlayStausBalloonTip();
            ShowMediaTypeImage(resetToDefault);
            SetNowPlayingBitrate(resetToDefault);
            SetNowPlayingSamplerate(resetToDefault);
            SetNowPlayingSongInfo(resetToDefault);
            SetNowPlayingThumbnail(resetToDefault);
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

        private void SetNowPlayingTimePlayed(bool resetToDefault)
        {
            lTimePlayed.Text = (resetToDefault)? "00:00" : XBMC.GetNowPlayingInfo("time");
        }

        private void SetNowPlayingBitrate(bool resetToDefault)
        {
            lBitrate.Text = (resetToDefault) ? "" : XBMC.GetNowPlayingInfo("bitrate");
        }

        private void SetNowPlayingSamplerate(bool resetToDefault)
        {
            lSamplerate.Text = (resetToDefault) ? "" : XBMC.GetNowPlayingInfo("samplerate");
        }

        private void SetNowPlayingThumbnail(bool resetToDefault)
        {
            MemoryStream thumbNailStream = XBMC.GetThumbnail();
            pbThumbnail.Image = (resetToDefault || thumbNailStream == null) ? Properties.Resources.XBMClogo : new Bitmap(thumbNailStream);
        }

        private void SetNowPlayingSongInfo(bool resetToDefault)
        {
            if (resetToDefault)
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

        private void ShowMediaTypeImage(bool resetToDefault)
        {
            string mediaType = XBMC.GetNowPlayingInfo("type");
            string mediaFile = XBMC.GetNowPlayingInfo("filename");

            pbLastFM.Visible    = (mediaFile.Substring(0, 6) == "lastfm") ? true : false;
            pbMediaType.Visible = (resetToDefault) ? false : true;

            if (mediaType == "Audio")
                pbMediaType.Image = Properties.Resources.audio_cd_32x32;
            else if (mediaType == "Video")
                pbMediaType.Image = Properties.Resources.video_32x32;
            else if (mediaType == "Picture")
                pbMediaType.Image = Properties.Resources.pictures_32x32;
        }

        private void ShowNowPlayingBalloonTip()
        {
            if (resetToDefault)
                notifyIcon1.ShowBalloonTip(2000, "XBMControl", "Nothing playing...", ToolTipIcon.Info);
            else if (mediaCurrentlyPlaying != XBMC.GetNowPlayingInfo("filename") && XBMC.GetNowPlayingInfo("type") == "Audio")
            {
                mediaCurrentlyPlaying = XBMC.GetNowPlayingInfo("filename");
                string lastFM = (mediaCurrentlyPlaying.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";
                notifyIcon1.ShowBalloonTip(2000, "XBMControl : Now playing " + lastFM, XBMC.GetNowPlayingInfo("artist") + " - " + XBMC.GetNowPlayingInfo("title"), ToolTipIcon.Info);
            }
        }

        private void ShowPlayStausBalloonTip()
        {
            if (XBMC.GetNowPlayingInfo("playstatus") == "Paused" && !pausedMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, "XBMControl", "Playback has been paused.", ToolTipIcon.Info);
                pausedMessageShowed = true;
            }
            else if (XBMC.GetNowPlayingInfo("playstatus") == "Playing")
                pausedMessageShowed = false;
        }

//------------------START EVENTS-------------------------

        private void timerLong_Tick(object sender, EventArgs e)
        {
            UpdateDataLong();
        }

        private void timerShort_Tick(object sender, EventArgs e)
        {
            UpdateData();
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
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void tbVolume_MouseHover(object sender, EventArgs e)
        {
            tbVolume.Focus();
        }

        private void tbProgress_MouseHover(object sender, EventArgs e)
        {
            tbProgress.Focus();
        }

        private void cmsNotifyExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmsNotifyPrevious_Click(object sender, EventArgs e)
        {
            XBMC.Request("PlayPrev");
        }

        private void cmsNotifyPlay_Click(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingInfo("playstatus") == "Paused")
                XBMC.Request("Pause");
        }

        private void cmsNotifyPause_Click(object sender, EventArgs e)
        {
            XBMC.Request("Pause");
        }

        private void cmsNotifyStop_Click(object sender, EventArgs e)
        {
            XBMC.Request("Stop");
        }

        private void cmsNotifyNext_Click(object sender, EventArgs e)
        {
            XBMC.Request("PlayNext");
        }

        private void cmsNotifyMute_Click(object sender, EventArgs e)
        {
            XBMC.Request("Mute");
        }

        private void cmsNotifyShow_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
        }

        private void cmsNotifyHide_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                this.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
