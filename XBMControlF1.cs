// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 3.5)
//    Copyright (C) 2008  Bram van Oploo (bramvano@gmail.com)
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------


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
using XBMControl;
using XBMControl.Properties;
using XBMControl.Language;
using System.Threading;

namespace XBMControl
{
    public partial class MainForm : Form
    {
        private XBMCcomm XBMC;
        private XBMCLanguage Language;
        private ConfigurationF1 ConfigForm;
        private FullSizeImageF1 fullSizeImage;
        private VolumeControlF1 sysTrayVolumeControl;
        private const int updateIntervalShort   = 1000;
        private const int updateIntervalLong    = 5000; 
        private string[,] maNowPlayingInfo      = new string[50, 2];
        private string mediaCurrentlyPlaying    = null;
        private bool pausedMessageShowed        = false;
        private bool configFormOpened           = false;
        private bool fullSizeImageOpened        = false;
        private bool volumeControlOpened        = false;
        private bool connected                  = false;
        private bool showedConnectionStatus     = false;

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
            {
                updateTimer.Enabled = false;
                ShowConfigurationForm();
            }
            else if (XBMC.IsConnected())
            {
                XBMC.Request("SetResponseFormat");
                updateTimer.Enabled = true;
                connected = true;
                updateTimer.Interval = updateIntervalShort;
            }
            else
            {
                updateTimer.Enabled = true;
                connected = false;
                this.Enabled = false;
                updateTimer.Interval = updateIntervalLong;
                ShowConnectionStatus();
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
            cmsControls.Text        = Language.GetString("contextMenu/controls/title");
                cmsPrevious.Text        = Language.GetString("contextMenu/controls/previous");
                cmsPlay.Text            = Language.GetString("contextMenu/controls/play");
                cmsPause.Text           = Language.GetString("contextMenu/controls/pause");
                cmsStop.Text            = Language.GetString("contextMenu/controls/stop");
                cmsNext.Text            = Language.GetString("contextMenu/controls/next");
                cmsMute.Text            = Language.GetString("contextMenu/controls/mute");
            cmsXBMC.Text            = Language.GetString("contextMenu/xbmc/title");
                cmsShowScreenshot.Text  = Language.GetString("contextMenu/xbmc/showScreenshot");
                cmsXBMCreboot.Text      = Language.GetString("contextMenu/xbmc/reboot");
                cmsXBMCrestart.Text     = Language.GetString("contextMenu/xbmc/restart");
                cmsXBMCshutdown.Text    = Language.GetString("contextMenu/xbmc/shutdown");
            cmsSaveMedia.Text       = Language.GetString("contextMenu/saveMedia");
            cmsShow.Text            = Language.GetString("contextMenu/show");
            cmsHide.Text            = Language.GetString("contextMenu/hide");
            cmsConfigure.Text       = Language.GetString("contextMenu/configure");
            cmsExit.Text            = Language.GetString("contextMenu/exit");
        }

        private void UpdateData()
        {
            if (XBMC.IsConnected())
            {
                if (this.Enabled == false && !configFormOpened && !fullSizeImageOpened) this.Enabled = true;
                bool resetToDefault = (XBMC.IsPlaying()) ? false : true;
                updateTimer.Interval = updateIntervalShort;

                if (IsNewMediaPlaying() || !connected)
                {
                    GetNowPlayingSongInfo(resetToDefault);
                    if (Properties.Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingBalloonTip(resetToDefault);
                    if (!connected) connected = true;
                }

                ShowConnectionStatus();
                GetProgressPosition();
                GetVolumePosition();
                SetNowPlayingTimePlayed(resetToDefault);
                if (Properties.Settings.Default.ShowPlayStausBalloonTips) ShowPlayStausBalloonTip();
            }
            else
            {
                if (connected) connected = false;
                updateTimer.Interval     = updateIntervalLong;
                this.Enabled             = false;
                ShowConnectionStatus();
            }
        }

        private void GetProgressPosition()
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

        private bool IsNewMediaPlaying()
        {
            if (mediaCurrentlyPlaying == XBMC.GetNowPlayingInfo("filename"))
                return false;
            else
            {
                mediaCurrentlyPlaying = XBMC.GetNowPlayingInfo("filename");
                return true;
            }
        }

        private void GetVolumePosition()
        {
            string[] aCurVolume = XBMC.Request("GetVolume");
            int newVolumePosition = (aCurVolume.Length > 1 && aCurVolume[1] != "Error") ? Convert.ToInt32(aCurVolume[1]) : 0 ;
            tbVolume.Value = newVolumePosition;
        }

        private void SetNowPlayingTimePlayed(bool resetToDefault)
        {
            lTimePlayed.Text = (resetToDefault) ? "00:00" : XBMC.GetNowPlayingInfo("time");
        }

        private void GetNowPlayingSongInfo(bool resetToDefault)
        {
            if (resetToDefault)
            {
                lArtistSong.Text = Language.GetString("mainform/playing/nothing");
                pbThumbnail.Image = Properties.Resources.XBMClogo;
                lBitrate.Text = "";
                lSamplerate.Text = "";
                lArtist.Text = "";
                lTitle.Text = "";
                lDuration.Text = "";
                lAlbum.Text = "";
            }
            else
            {
                Image coverArt = XBMC.GetNowPlayingCoverArt();
                pbThumbnail.Image = (coverArt == null) ? Properties.Resources.XBMClogo : coverArt;

                string year = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " (" + XBMC.GetNowPlayingInfo("year") + ")";
                lBitrate.Text = XBMC.GetNowPlayingInfo("bitrate");
                lSamplerate.Text = XBMC.GetNowPlayingInfo("samplerate");
                lArtistSong.Text = XBMC.GetNowPlayingInfo("artist") + " - " + XBMC.GetNowPlayingInfo("title");
                lArtist.Text = XBMC.GetNowPlayingInfo("artist");
                lTitle.Text = XBMC.GetNowPlayingInfo("title");
                lDuration.Text = XBMC.GetNowPlayingInfo("duration");
                lAlbum.Text = XBMC.GetNowPlayingInfo("album") + year;
            }

            string mediaType = XBMC.GetNowPlayingInfo("type");
            string mediaFile = XBMC.GetNowPlayingInfo("filename");

            pbLastFM.Visible = (mediaFile.Substring(0, 6) == "lastfm") ? true : false;
            pbMediaType.Visible = (resetToDefault) ? false : true;

            if (mediaType == "Audio")
                pbMediaType.Image = Properties.Resources.audio_cd_32x32;
            else if (mediaType == "Video")
                pbMediaType.Image = Properties.Resources.video_32x32;
            else if (mediaType == "Picture")
                pbMediaType.Image = Properties.Resources.pictures_32x32;
        }

        private void ShowNowPlayingBalloonTip(bool resetToDefault)
        {
            if (resetToDefault)
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/nothing"), ToolTipIcon.Info);
            }
            else
            {
                string artist = XBMC.GetNowPlayingInfo("artist") + "\n";
                string title  = XBMC.GetNowPlayingInfo("title") + " [" + XBMC.GetNowPlayingInfo("duration") + "]\n";
                string year   = XBMC.GetNowPlayingInfo("year");
                year = (year == "" || year == null) ? "" : " [" + year + "]";
                string album = XBMC.GetNowPlayingInfo("album") + year;
                string lastFM = (mediaCurrentlyPlaying != null && mediaCurrentlyPlaying.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";
                notifyIcon1.ShowBalloonTip(2000, "XBMControl : " + Language.GetString("mainform/playing/now") + lastFM, artist + title + album, ToolTipIcon.Info);
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

        private void ShowConnectionStatus() 
        {
            if (!connected && !showedConnectionStatus)
            {
                if (Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                {
                    notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/connection/none"), ToolTipIcon.Info);
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                }
                else
                    MessageBox.Show(Language.GetString("mainform/connection/none"));

                showedConnectionStatus  = true;
            }
            else if (connected)
                showedConnectionStatus = false;
        }

        public void SetConfigFormClosed(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Ip == "")
                Close();
            else
            {
                configFormOpened = false;
                this.Enabled = true;
                ApplyApplicationSettings();
                Initialize();
            }
        }

        private void ShowConfigurationForm()
        {
            if (!configFormOpened)
            {
                configFormOpened = true;
                ConfigForm = new ConfigurationF1();
                ConfigForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(SetConfigFormClosed);
                ConfigForm.Show();
                this.Enabled = false;
            }
        }

        public void EnableMainForm(object sender, EventArgs e)
        {
            this.Enabled = true;
            fullSizeImageOpened = false;
        }

        public void VolumeControlClosed(object sender, EventArgs e)
        {
            volumeControlOpened = false;    
        }

        private void ToggleShowMainWindow()
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

        private void ToggleShowVolumeControl()
        {
            if (!volumeControlOpened)
            {
                sysTrayVolumeControl = new VolumeControlF1();
                sysTrayVolumeControl.FormClosed += new FormClosedEventHandler(VolumeControlClosed);
                sysTrayVolumeControl.Left = Cursor.Position.X - (sysTrayVolumeControl.Width / 2);
                sysTrayVolumeControl.Top = Cursor.Position.Y - (sysTrayVolumeControl.Height + 15);
                sysTrayVolumeControl.Show();
                sysTrayVolumeControl.Focus();
                volumeControlOpened = true;
            }
            else
            {
                sysTrayVolumeControl.Dispose();
                volumeControlOpened = false;
            }
        }


//------------------START EVENTS-------------------------

        private void timerShort_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void tbProgress_MouseUp(object sender, MouseEventArgs e)
        {
            SetProgress();
            updateTimer.Enabled = true;
        }

        private void tbProgress_MouseDown(object sender, MouseEventArgs e)
        {
            updateTimer.Enabled = false;
        }

        private void tbVolume_ValueChanged(object sender, EventArgs e)
        {
            XBMC.Request("SetVolume", Convert.ToString(tbVolume.Value));
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
            this.Focus();
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

        private void cmsXBMCrebootComputer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/reboot"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Request("Restart");
        }

        private void cmsXBMCrebootXBMC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/restart"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Request("RestartApp");
        }

        private void cmsXBMCshutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/shutdown"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Request("Shutdown");
        }

        private void pbThumbnail_Click(object sender, EventArgs e)
        {
            Image coverArt = XBMC.GetNowPlayingCoverArt();
            if( coverArt != null)
            {
                fullSizeImage = new FullSizeImageF1(coverArt);
                fullSizeImage.FormClosed += new FormClosedEventHandler(EnableMainForm);
                fullSizeImage.Show();
                this.Enabled = false;
                fullSizeImageOpened = true;
            }
        }

        private void pbThumbnail_MouseHover(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingInfo("thumb") != "defaultAlbumCover.png")
                pbThumbnail.Cursor = Cursors.Hand;
        }

        private void pbThumbnail_MouseLeave(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingInfo("thumb") != "defaultAlbumCover.png")
                pbThumbnail.Cursor = Cursors.Default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainContextMenu.Show(pictureBox1, new System.Drawing.Point(0, pictureBox1.Height));
        }

        private void MainContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (XBMC.IsPlaying())
            {
                //cmsSeperatorSaveMedia.Visible = true;
                //cmsSaveMedia.Visible          = true;
            }
        }

        private void cmsSaveMedia_Click(object sender, EventArgs e)
        {

        }

        private void cmsShowScreenshot_Click(object sender, EventArgs e)
        {
            Image screenshot = XBMC.TakeScreenshot();

            if (screenshot == null)
                MessageBox.Show("Failed taking screenshot");
            else
            {
                fullSizeImage = new FullSizeImageF1(screenshot);
                fullSizeImage.FormClosed += new FormClosedEventHandler(EnableMainForm);
                fullSizeImage.Show();
                this.Enabled = false;
                fullSizeImageOpened = true;
            }
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ShowNowPlayingBalloonTip(false);
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                this.ToggleShowMainWindow();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                this.ToggleShowVolumeControl();
            } 
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && volumeControlOpened)
                sysTrayVolumeControl.Focus();
        }
    }
}
