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
        private bool playStatusMessageShowed    = false;
        private bool configFormOpened           = false;
        private bool fullSizeImageOpened        = false;
        private bool volumeControlOpened        = false;
        private bool connected                  = false;
        private bool showedConnectionStatus     = false;
        private bool resetToDefault             = false;
        private bool isDragging                 = false;
        private int clickOffsetX, clickOffsetY;
        private int originalWindowHeight;

        public MainForm()
        {
            XBMC     = new XBMCcomm();
            Language = new XBMCLanguage();
            InitializeComponent();
            ApplyApplicationSettings();
            Initialize();
            originalWindowHeight = this.Height;
            ToggleShowDetails();
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

            //Set control button states
            if (XBMC.IsPlaying()) bPlay.ImageIndex = 5;
            if (XBMC.IsPaused()) bPause.ImageIndex = 8;
            if (XBMC.IsMuted()) bMute.ImageIndex = 20;
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
            lMainTitle.Text     = Language.GetString("global/appName") + " v" + Properties.Settings.Default.Version;
            lArtistTitle.Text   = Language.GetString("mainform/label/artist");
            lTitleTitle.Text    = Language.GetString("mainform/label/title");
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
                resetToDefault = (XBMC.IsPlaying() && !XBMC.IsPaused()) ? false : true;
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

            //Set control button states
            bPause.ImageIndex = (XBMC.IsPaused())? 8 : 6 ;
            bPlay.ImageIndex = (XBMC.IsPlaying())? 5 : 3;
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
                lAlbum.Text = "";
            }
            else
            {
                Image coverArt = XBMC.GetNowPlayingCoverArt();
                pbThumbnail.Image = (coverArt == null) ? Properties.Resources.XBMClogo : coverArt;

                string year = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " [" + XBMC.GetNowPlayingInfo("year") + "]";
                lBitrate.Text = XBMC.GetNowPlayingInfo("bitrate");
                lSamplerate.Text = XBMC.GetNowPlayingInfo("samplerate");
                lArtistSong.Text = XBMC.GetNowPlayingInfo("artist") + " - " + XBMC.GetNowPlayingInfo("title");
                lArtist.Text = XBMC.GetNowPlayingInfo("artist");
                lTitle.Text = XBMC.GetNowPlayingInfo("title") + " [" + XBMC.GetNowPlayingInfo("duration") + "]";
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
                ShowPlayStausBalloonTip();
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
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            if (!XBMC.IsPlaying() && !playStatusMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/nothing"), ToolTipIcon.Info);
                playStatusMessageShowed = true;
            }
            else if (XBMC.GetNowPlayingInfo("playstatus") == "Paused" && !playStatusMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/paused"), ToolTipIcon.Info);
                playStatusMessageShowed = true;
            }
            else if (XBMC.IsPlaying())
                playStatusMessageShowed = false;
        }

        private void ShowConnectionStatus() 
        {
            if (!XBMC.IsConnected() && !showedConnectionStatus)
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

        private void ToggleShowDetails()
        {
            if (this.Height == originalWindowHeight)
            {
                this.Height = (this.Height - pDetails.Height);
                pDetails.Height = 0;
            }
            else
            {
                pDetails.Height = (originalWindowHeight - this.Height);
                this.Height = originalWindowHeight;
            }
        }

        private void ToggelHoverImage(Button target)
        {
            target.ImageIndex = ((target.ImageIndex % 2) == 0) ? target.ImageIndex-- : target.ImageIndex++;  
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
                if (!XBMC.IsConnected())
                    ShowConnectionStatus();
                else if (!XBMC.IsPlaying())
                    ShowPlayStausBalloonTip();
                else
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

        private void lArtistSong_Click(object sender, EventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused())  ToggleShowDetails();   
        }

        private void lArtistSong_MouseEnter(object sender, EventArgs e)
        {
            if(XBMC.IsPlaying() || XBMC.IsPaused()) this.Cursor = Cursors.Hand;
        }

        private void lArtistSong_MouseLeave(object sender, EventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused()) this.Cursor = Cursors.Default;
        }


//START PREVIOUS BUTTON
        private void bPrevious_MouseEnter(object sender, EventArgs e)
        {
            bPrevious.ImageIndex = 1;
            //ToggelHoverImage(bPrevious);
        }

        private void bPrevious_MouseLeave(object sender, EventArgs e)
        {
            bPrevious.ImageIndex = 0;
        }

        private void bPrevious_MouseDown(object sender, MouseEventArgs e)
        {
            bPrevious.ImageIndex = 2;
        }

        private void bPrevious_MouseUp(object sender, MouseEventArgs e)
        {
            bPrevious.ImageIndex = 1;
        }
//END PREVIOUS BUTTON

//START PLAY BUTTON
        private void bPlay_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.ImageIndex = 4;
        }

        private void bPlay_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.ImageIndex = 3;
        }

        private void bPlay_MouseDown(object sender, MouseEventArgs e)
        {
            bPlay.ImageIndex = 5;
            if (XBMC.IsPaused()) XBMC.Request("Pause");
        }

        private void bPlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.ImageIndex = 4;
            bPause.ImageIndex = (XBMC.IsPaused()) ? 8 : 6;
            bPlay.ImageIndex = (XBMC.IsPlaying()) ? 5 : 3;
        }
//END PLAY BUTTON

//START PAUSE BUTTON
        private void bPause_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.ImageIndex = 7;
        }

        private void bPause_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.ImageIndex = 6;
        }

        private void bPause_MouseDown(object sender, MouseEventArgs e)
        {
            bPause.ImageIndex = 8;
            XBMC.Request("Pause");
        }

        private void bPause_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.ImageIndex = 7;
            bPause.ImageIndex = (XBMC.IsPaused()) ? 8 : 6;
            bPlay.ImageIndex = (XBMC.IsPlaying()) ? 5 : 3;
        }
//END PAUSE BUTTON

//START STOP BUTTON
        private void bStop_MouseEnter(object sender, EventArgs e)
        {
            bStop.ImageIndex = 10;
        }

        private void bStop_MouseLeave(object sender, EventArgs e)
        {
            bStop.ImageIndex = 9;
        }

        private void bStop_MouseDown(object sender, MouseEventArgs e)
        {
            bStop.ImageIndex = 11;
        }

        private void bStop_MouseUp(object sender, MouseEventArgs e)
        {
            bStop.ImageIndex = 10;
        }
//END STOP BUTTON

//START NEXT BUTTON
        private void bNext_MouseEnter(object sender, EventArgs e)
        {
            bNext.ImageIndex = 13;
        }

        private void bNext_MouseLeave(object sender, EventArgs e)
        {
            bNext.ImageIndex = 12;
        }

        private void bNext_MouseDown(object sender, MouseEventArgs e)
        {
            bNext.ImageIndex = 14;
        }

        private void bNext_MouseUp(object sender, MouseEventArgs e)
        {
            bNext.ImageIndex = 13;
        }

//END NEXT BUTTON

//START OPEN BUTTON
        private void bOpen_MouseEnter(object sender, EventArgs e)
        {
            bOpen.ImageIndex = 16;
        }

        private void bOpen_MouseLeave(object sender, EventArgs e)
        {
            bOpen.ImageIndex = 15;
        }

        private void bOpen_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void bOpen_MouseUp(object sender, MouseEventArgs e)
        {

        }

//END OPEN BUTTON

//START MUTE BUTTON
        private void bMute_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsMuted()) bMute.ImageIndex = 19;
        }

        private void bMute_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsMuted()) bMute.ImageIndex = 18;
        }

        private void bMute_MouseDown(object sender, MouseEventArgs e)
        {
            bMute.ImageIndex = 20;
            XBMC.Request("Mute");
        }

        private void bMute_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsMuted()) bMute.ImageIndex = 18;
        }
//END MUTE BUTTON

//START MINIMIZE BUTTON
        private void pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            pbMinimize.BackgroundImage = Resources.minimize1_hover;
        }

        private void pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            pbMinimize.BackgroundImage = Resources.minimize1;
        }

        private void pbMinimize_MouseUp(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
//END MINIMIZE BUTTON

//START CLOSE BUTTON
        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.BackgroundImage = Resources.close1_hover;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.BackgroundImage = Resources.close1;
        }

        private void pbClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.Dispose();
        }
//END CLOSE BUTTON

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
    }
}
