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
            if (Properties.Settings.Default.Ip == "")
                ShowConfigurationForm();
            else if (!XBMC.IsConnected())
            {
                if (Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, "XBMControl", Language.GetString("mainform/connection/none"), ToolTipIcon.Info);
                else
                    MessageBox.Show(Language.GetString("mainform/connection/none"));
                timerLong.Enabled = true;
                this.Enabled = false;
            }
            else
            {
                XBMC.Request("SetResponseFormat");
                initiallyConnected = true;
                timerShort.Enabled = true;
                timerLong.Enabled  = true;
                UpdateData();
                UpdateDataLong();
            }
        }

        private void ApplyApplicationSettings()
        {
            Language.SetLanguage(Properties.Settings.Default.Language);
            SetLanguageStrings();
            notifyIcon1.Visible = Properties.Settings.Default.ShowInSystemTray;
            this.Visible        = Properties.Settings.Default.ShowInTaskbar;
        }

        private void SetLanguageStrings()
        {
            //MainForm
            this.Text           = Language.GetString("global/appName") + " v" + Properties.Settings.Default.Version;
            lArtistTitle.Text   = Language.GetString("mainform/label/artist");
            lTitleTitle.Text    = Language.GetString("mainform/label/title");
            lDurationTitle.Text = Language.GetString("mainform/label/duration");
            lAlbumTitle.Text    = Language.GetString("mainform/label/album");

            //Context Menu
            cmsPrevious.Text    = Language.GetString("contextMenu/previous");
            cmsPlay.Text        = Language.GetString("contextMenu/play");
            cmsPause.Text       = Language.GetString("contextMenu/pause");
            cmsStop.Text        = Language.GetString("contextMenu/stop");
            cmsNext.Text        = Language.GetString("contextMenu/next");
            cmsMute.Text        = Language.GetString("contextMenu/mute");
            cmsControls.Text    = Language.GetString("contextMenu/controls");
            cmsShow.Text        = Language.GetString("contextMenu/show");
            cmsHide.Text        = Language.GetString("contextMenu/hide");
            cmsConfigure.Text   = Language.GetString("contextMenu/configure");
            cmsExit.Text        = Language.GetString("contextMenu/exit");
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
                if (initiallyConnected && connectionLost && Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/connection/reastablished"), ToolTipIcon.Info);
                else if (initiallyConnected && connectionLost && !Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    MessageBox.Show(Language.GetString("mainform/connection/reastablished"));
                if (connectionLost) connectionLost = false;
                if (Properties.Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingBalloonTip();
                if (Properties.Settings.Default.ShowPlayStausBalloonTips) ShowPlayStausBalloonTip();
                if (mediaCurrentlyPlaying != XBMC.GetNowPlayingInfo("filename"))
                {
                    ShowMediaTypeImage(resetToDefault);
                    SetNowPlayingBitrate(resetToDefault);
                    SetNowPlayingSamplerate(resetToDefault);
                    SetNowPlayingThumbnail(resetToDefault);
                    SetNowPlayingSongInfo(resetToDefault);
                }
            }
            else
            {
                if (!connectionLost && initiallyConnected && Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/connection/lost"), ToolTipIcon.Info);
                else if (!connectionLost && initiallyConnected && !Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    MessageBox.Show(Language.GetString("mainform/connection/lost"));
                if (!connectionLost) connectionLost = true;
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
                lArtistSong.Text = Language.GetString("mainform/playing/nothing");
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
            this.Enabled     = true;
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
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/nothing"), ToolTipIcon.Info);
                notPlayingMessageShowed = true;
            }
            else if (mediaCurrentlyPlaying != XBMC.GetNowPlayingInfo("filename") && XBMC.GetNowPlayingInfo("type") == "Audio")
            {
                notPlayingMessageShowed = false;
                mediaCurrentlyPlaying = XBMC.GetNowPlayingInfo("filename");
                string lastFM = (mediaCurrentlyPlaying.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";
                notifyIcon1.ShowBalloonTip(2000, "XBMControl : " + Language.GetString("mainform/playing/now") + lastFM, XBMC.GetNowPlayingInfo("artist") + " ~ " + XBMC.GetNowPlayingInfo("title"), ToolTipIcon.Info);
            }
        }

        private void ShowPlayStausBalloonTip()
        {
            if (XBMC.GetNowPlayingInfo("playstatus") == "Paused" && !pausedMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/paused"), ToolTipIcon.Info);
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
                this.Visible = Properties.Settings.Default.ShowInTaskbar;
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
            this.Visible = Properties.Settings.Default.ShowInTaskbar;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                this.Visible = Properties.Settings.Default.ShowInTaskbar;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = Properties.Settings.Default.ShowInTaskbar;
        }

        private void cmsConfigure_Click(object sender, EventArgs e)
        {
            ShowConfigurationForm();    
        }
    }
}
