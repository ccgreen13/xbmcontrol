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
using XBMControl.Properties;
using XBMControl.Language;

namespace XBMControl
{
    public partial class MainForm : Form
    {
        XBMCcomm XBMC;
        XBMCLanguage Language;
        string[,] maNowPlayingInfo   = new string[50,2];
        string mediaCurrentlyPlaying = null;
        bool pausedMessageShowed     = false;
        bool notPlayingMessageShowed = false;
        bool resetToDefault          = false;
        bool configFormOpened        = false;
        bool connectionLost          = false;
        bool initiallyConnected      = false;

        public MainForm()
        {
            XBMC     = new XBMCcomm();
            Language = new XBMCLanguage();
            InitializeComponent();
            ApplyApplicationSettings();
            Initialize();
        }

        private void Initialize()
        {
            if (Settings.Default.Ip == "")
                ShowConfigurationForm();
            else if (!XBMC.IsConnected())
            {
                if (Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("notConnected"), ToolTipIcon.Info);
                else
                    MessageBox.Show(Language.GetString("notConnected"));
                timerLong.Enabled = true;
                this.Enabled = false;
            }
            else
            {
                XBMC.Request("SetResponseFormat");
                initiallyConnected = true;
                UpdateData();
                UpdateDataLong();
                timerShort.Enabled = true;
                timerLong.Enabled = true;
            }
        }

        private void ApplyApplicationSettings()
        {
            Language.SetModule("mainform");
            Language.SetLanguage(Settings.Default.Language);
            SetLanguageStrings();
            notifyIcon1.Visible = Settings.Default.ShowInSystemTray;
            this.Visible        = Settings.Default.ShowInTaskbar;
        }

        private void SetLanguageStrings()
        {
            lArtistTitle.Text   = Language.GetString("labelArtist");
            lTitleTitle.Text    = Language.GetString("labelTitle");
            lDurationTitle.Text = Language.GetString("labelDuration");
            lAlbumTitle.Text    = Language.GetString("labelAlbum");
        }

        private void UpdateData()
        {
            if(!connectionLost)
            {
                resetToDefault = (XBMC.IsPlaying()) ? false : true;
                SetProgressPosition();
                SetVolumePosition();
                SetNowPlayingTimePlayed(resetToDefault);
            }
        }

        public void UpdateDataLong()
        {
            if (XBMC.IsConnected())
            {
                if (timerShort.Enabled == false) timerShort.Enabled = true;
                if(this.Enabled == false) this.Enabled = true;
                if (initiallyConnected && connectionLost && Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("connectionReastablished"), ToolTipIcon.Info);
                if (connectionLost) connectionLost = false;
                if (Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingBalloonTip();
                if (Settings.Default.ShowPlayStausBalloonTips) ShowPlayStausBalloonTip();
                ShowMediaTypeImage(resetToDefault);
                SetNowPlayingBitrate(resetToDefault);
                SetNowPlayingSamplerate(resetToDefault);
                SetNowPlayingSongInfo(resetToDefault);
                SetNowPlayingThumbnail(resetToDefault);
            }
            else
            {
                if (!connectionLost && initiallyConnected && Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("connectionLost"), ToolTipIcon.Info);
                connectionLost = true;
                this.Enabled = false;
            }
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
                lArtistSong.Text = Language.GetString("nothingPlaying");
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

        public void SetConfigFormClosed(object sender, EventArgs e)
        {
            configFormOpened = false;
            this.Enabled = true;
            if (Settings.Default.Ip == "")
            {
                if (Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(3000, "XBMControl", Language.GetString("invalidIp"), ToolTipIcon.Info);
                else
                    MessageBox.Show(Language.GetString("invalidIp"));
            }
            ApplyApplicationSettings();
            Initialize();
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
            if (resetToDefault && !notPlayingMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("nothingPlaying"), ToolTipIcon.Info);
                notPlayingMessageShowed = true;
            }
            else if (mediaCurrentlyPlaying != XBMC.GetNowPlayingInfo("filename") && XBMC.GetNowPlayingInfo("type") == "Audio")
            {
                notPlayingMessageShowed = false;
                mediaCurrentlyPlaying = XBMC.GetNowPlayingInfo("filename");
                string lastFM = (mediaCurrentlyPlaying.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";
                notifyIcon1.ShowBalloonTip(2000, "XBMControl : " + Language.GetString("nowPlaying") + lastFM, XBMC.GetNowPlayingInfo("artist") + " ~ " + XBMC.GetNowPlayingInfo("title"), ToolTipIcon.Info);
            }
        }

        private void ShowPlayStausBalloonTip()
        {
            if (XBMC.GetNowPlayingInfo("playstatus") == "Paused" && !pausedMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("paused"), ToolTipIcon.Info);
                pausedMessageShowed = true;
            }
            else if (XBMC.GetNowPlayingInfo("playstatus") == "Playing")
                pausedMessageShowed = false;
        }

        private void ShowConfigurationForm()
        {
            if (!configFormOpened)
            {
                configFormOpened = true;
                ConfigurationF1 ConfigForm = new ConfigurationF1();
                ConfigForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SetConfigFormClosed);
                ConfigForm.Show();
                this.Enabled = false;
            }
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
                this.Visible = Settings.Default.ShowInTaskbar;
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
            this.Visible = Settings.Default.ShowInTaskbar;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                this.Visible = Settings.Default.ShowInTaskbar;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = Settings.Default.ShowInTaskbar;
        }

        private void cmsConfigure_Click(object sender, EventArgs e)
        {
            ShowConfigurationForm();    
        }
    }
}
