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
        private bool playStatusMessageShowed    = false;
        private bool configFormOpened           = false;
        private bool showedConnectionStatus     = false;
        private bool resetToDefault             = false;
        private bool isDragging                 = false;
        private bool volumeControlOpened        = false;
        private int clickOffsetX, clickOffsetY;
        private int originalWindowHeight;

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
            originalWindowHeight = this.Height;
            ToggleShowDetails();
            if( Properties.Settings.Default.Ip != "") XBMC.GetXbmcProperties();

            if (Properties.Settings.Default.Ip == "")
            {
                updateTimer.Enabled = false;
                ShowConfigurationForm();
            }
            else if (XBMC.IsConnected())
            {
                XBMC.Request("SetResponseFormat");
                updateTimer.Enabled = true;
            }
            else
                updateTimer.Enabled = true;

            GetNowPlayingSongInfo(resetToDefault);
            if (Properties.Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingBalloonTip(resetToDefault);
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
            XBMC.GetXbmcProperties();

            if (XBMC.IsConnected())
            {
                SetControlsEnabled(true);
                resetToDefault = (XBMC.IsPlaying() || XBMC.IsPaused()) ? false : true;
                updateTimer.Interval = updateIntervalShort;

                if (XBMC.IsNewMediaPlaying())
                {
                    GetNowPlayingSongInfo(resetToDefault);
                    if (Properties.Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingBalloonTip(resetToDefault);
                }

                tbProgress.Value = XBMC.GetProgress();
                tbVolume.Value   = XBMC.GetVolume();
                SetNowPlayingTimePlayed(resetToDefault);
                if (Properties.Settings.Default.ShowPlayStausBalloonTips) ShowPlayStausBalloonTip();

                //Set control button states
                bPause.BackgroundImage  = (XBMC.IsPaused()) ? Resources.button_pause_click : Resources.button_pause;
                bPlay.BackgroundImage   = (XBMC.IsPlaying()) ? Resources.button_play_click : Resources.button_play;
                bStop.BackgroundImage = (!XBMC.IsPlaying() && !XBMC.IsPaused()) ? Resources.button_stop_click : Resources.button_stop;
                bMute.BackgroundImage   = (XBMC.IsMuted()) ? Resources.button_mute_click : Resources.button_mute;
            }
            else
            {
                SetControlsEnabled(false);
                if (!showedConnectionStatus) ShowConnectionStatus();
                bPause.BackgroundImage  = Resources.button_pause;
                bPlay.BackgroundImage   = Resources.button_play;
                bMute.BackgroundImage   = Resources.button_mute;
                updateTimer.Interval    = updateIntervalLong;
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            bPrevious.Enabled           = enabled;
            bPlay.Enabled               = enabled;
            bPause.Enabled              = enabled;
            bStop.Enabled               = enabled;
            bNext.Enabled               = enabled;
            bOpen.Enabled               = enabled;
            bMute.Enabled               = enabled;
            tbProgress.Enabled          = enabled;
            tbVolume.Enabled            = enabled;
            cmsXBMC.Visible             = enabled;
            cmsControls.Visible         = enabled;
            cmsSeperatorFolders.Visible = enabled; 
        }

        private void SetNowPlayingTimePlayed(bool resetToDefault)
        {
            lTimePlayed.Text = (resetToDefault) ? "00:00" : XBMC.GetNowPlayingInfo("time");
        }

        private void GetNowPlayingSongInfo(bool resetToDefault)
        {
            if (resetToDefault)
            {
                lArtistSong.Text    = Language.GetString("mainform/playing/nothing");
                pbThumbnail.Image   = Properties.Resources.XBMClogo;
                lBitrate.Text       = "";
                lSamplerate.Text    = "";
                lArtist.Text        = "";
                lTitle.Text         = "";
                lAlbum.Text         = "";
            }
            else
            {
                Image coverArt      = XBMC.GetNowPlayingCoverArt();
                pbThumbnail.Image   = (coverArt == null) ? Properties.Resources.XBMClogo : coverArt;
                string year         = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " [" + XBMC.GetNowPlayingInfo("year") + "]";
                lBitrate.Text       = XBMC.GetNowPlayingInfo("bitrate");
                lSamplerate.Text    = XBMC.GetNowPlayingInfo("samplerate");
                lArtistSong.Text    = XBMC.GetNowPlayingInfo("artist") + " - " + XBMC.GetNowPlayingInfo("title");
                lArtist.Text        = XBMC.GetNowPlayingInfo("artist");
                lTitle.Text         = XBMC.GetNowPlayingInfo("title") + " [" + XBMC.GetNowPlayingInfo("duration") + "]";
                lAlbum.Text         = XBMC.GetNowPlayingInfo("album") + year;
            }

            string mediaFile    = XBMC.GetNowPlayingInfo("filename");
            pbMediaType.Visible = (resetToDefault) ? false : true;

            if (XBMC.GetNowPlayingMediaType() == "Audio" || XBMC.IsPlaying("lastfm"))
            {
                pbMediaType.Cursor = Cursors.Hand;
                pbMediaType.Image = (XBMC.IsPlaying("lastfm")) ? Resources.lastfm_32x32 : Properties.Resources.audio_cd_32x32;
            }
            else if (XBMC.GetNowPlayingMediaType() == "Video")
            {
                pbMediaType.Cursor = Cursors.Default;
                pbMediaType.Image = Properties.Resources.video_32x32;
            }
            else if (XBMC.GetNowPlayingMediaType() == "Picture")
            {
                pbMediaType.Cursor = Cursors.Default;
                pbMediaType.Image = Properties.Resources.pictures_32x32;
            }
        }

        private void ShowNowPlayingBalloonTip(bool resetToDefault)
        {
            if (resetToDefault)
                ShowPlayStausBalloonTip();
            else
            {
                string currentFilename  = XBMC.GetNowPlayingInfo("filename");
                string artist           = XBMC.GetNowPlayingInfo("artist") + "\n";
                string title            = XBMC.GetNowPlayingInfo("title") + " [" + XBMC.GetNowPlayingInfo("duration") + "]\n";
                string year             = XBMC.GetNowPlayingInfo("year");
                year                    = (year == "" || year == null) ? "" : " [" + year + "]";
                string album            = XBMC.GetNowPlayingInfo("album") + year;
                string lastFM           = (currentFilename.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";
                
                notifyIcon1.ShowBalloonTip(2000, "XBMControl : " + Language.GetString("mainform/playing/now") + lastFM, artist + title + album, ToolTipIcon.Info);
            }
        }

        private void ShowPlayStausBalloonTip()
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            if (XBMC.IsPaused() && !playStatusMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/paused"), ToolTipIcon.Info);
                playStatusMessageShowed = true;
            }
            else if (!XBMC.IsPlaying() && !playStatusMessageShowed)
            {
                notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/nothing"), ToolTipIcon.Info);
                playStatusMessageShowed = true;
            }
            else if (XBMC.IsPlaying())
                playStatusMessageShowed = false;
        }

        private void ShowConnectionStatus() 
        {
            if (!XBMC.IsConnected())
            {
                lArtistSong.Text = Language.GetString("mainform/connection/none");
                if (Properties.Settings.Default.ShowConnectionStatusBalloonTip)
                    notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/connection/none"), ToolTipIcon.Error);
                else
                    MessageBox.Show(Language.GetString("mainform/connection/none"));

                showedConnectionStatus  = true;
            }
            else
                showedConnectionStatus = false;
        }

        public void SetConfigFormClosed(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Ip == "")
                Close();
            else
            {
                XBMC.GetXbmcProperties();
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

        private void pbLastFM_Click()
        {
            string lastFmUrl = "http://www.last.fm/music/" + XBMC.GetNowPlayingInfo("artist").Replace(" ", "+");
            Help.ShowHelp(this, lastFmUrl);
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

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void tbProgress_MouseUp(object sender, MouseEventArgs e)
        {
            XBMC.Request("SeekPercentage", Convert.ToString(tbProgress.Value));
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
                fullSizeImage.Show();
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
            Image screenshot = XBMC.GetScreenshot();

            if (screenshot == null)
                MessageBox.Show("Failed taking screenshot");
            else
            {
                fullSizeImage = new FullSizeImageF1(screenshot);
                fullSizeImage.Show();
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
                this.ToggleShowMainWindow();
            else if (e.Button == MouseButtons.Middle)
                if(XBMC.IsConnected()) this.ToggleShowVolumeControl();
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

        private void lArtistSong_MouseHover(object sender, EventArgs e)
        {

        }

//START PREVIOUS BUTTON
        private void bPrevious_MouseEnter(object sender, EventArgs e)
        {
            bPrevious.BackgroundImage = Resources.button_previous_hover;
            //ToggelHoverImage(bPrevious);
        }

        private void bPrevious_MouseLeave(object sender, EventArgs e)
        {
            bPrevious.BackgroundImage = Resources.button_previous;
        }

        private void bPrevious_MouseDown(object sender, MouseEventArgs e)
        {
            bPrevious.BackgroundImage = Resources.button_previous_click;
            XBMC.Request("PlayPrev");
        }

        private void bPrevious_MouseUp(object sender, MouseEventArgs e)
        {
            bPrevious.BackgroundImage = Resources.button_previous_hover;
        }
//END PREVIOUS BUTTON

//START PLAY BUTTON
        private void bPlay_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.BackgroundImage = Resources.button_play_hover;
        }

        private void bPlay_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.BackgroundImage = Resources.button_play_hover;
        }

        private void bPlay_MouseDown(object sender, MouseEventArgs e)
        {
            bPlay.BackgroundImage = Resources.button_play_click;
            if (XBMC.IsPaused()) XBMC.Request("Pause");
        }

        private void bPlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsPlaying()) bPlay.BackgroundImage = Resources.button_play_hover;
        }
//END PLAY BUTTON

//START PAUSE BUTTON
        private void bPause_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.BackgroundImage = Resources.button_pause_hover;
        }

        private void bPause_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.BackgroundImage = Resources.button_pause;
        }

        private void bPause_MouseDown(object sender, MouseEventArgs e)
        {
            bPause.BackgroundImage = Resources.button_pause_click;
            XBMC.Request("Pause");
        }

        private void bPause_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsPaused()) bPause.BackgroundImage = Resources.button_pause_hover;
        }
//END PAUSE BUTTON

//START STOP BUTTON
        private void bStop_MouseEnter(object sender, EventArgs e)
        {
            if(XBMC.IsPlaying() || XBMC.IsPaused()) bStop.BackgroundImage = Resources.button_stop_hover;
        }

        private void bStop_MouseLeave(object sender, EventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused()) bStop.BackgroundImage = Resources.button_stop;
        }

        private void bStop_MouseDown(object sender, MouseEventArgs e)
        {
            bStop.BackgroundImage = Resources.button_stop_click;
            XBMC.Request("Stop");
        }

        private void bStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused()) bStop.BackgroundImage = Resources.button_stop_hover;
        }
//END STOP BUTTON

//START NEXT BUTTON
        private void bNext_MouseEnter(object sender, EventArgs e)
        {
            bNext.BackgroundImage = Resources.button_next_hover;
        }

        private void bNext_MouseLeave(object sender, EventArgs e)
        {
            bNext.BackgroundImage = Resources.button_next;
        }

        private void bNext_MouseDown(object sender, MouseEventArgs e)
        {
            bNext.BackgroundImage = Resources.button_next_click;
            XBMC.Request("PlayNext");
        }

        private void bNext_MouseUp(object sender, MouseEventArgs e)
        {
            bNext.BackgroundImage = Resources.button_next_hover;
        }

//END NEXT BUTTON

//START OPEN BUTTON
        private void bOpen_MouseEnter(object sender, EventArgs e)
        {
            bOpen.BackgroundImage = Resources.button_open_hover;
        }

        private void bOpen_MouseLeave(object sender, EventArgs e)
        {
            bOpen.BackgroundImage = Resources.button_open;
        }

        private void bOpen_MouseDown(object sender, MouseEventArgs e)
        {
            bOpen.BackgroundImage = Resources.button_open_click;
        }

        private void bOpen_MouseUp(object sender, MouseEventArgs e)
        {
            bOpen.BackgroundImage = Resources.button_open_hover;
        }

//END OPEN BUTTON

//START MUTE BUTTON
        private void bMute_MouseEnter(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bMute.BackgroundImage = Resources.button_mute_hover;
        }

        private void bMute_MouseLeave(object sender, EventArgs e)
        {
            if (!XBMC.IsPaused()) bMute.BackgroundImage = Resources.button_mute;
        }

        private void bMute_MouseDown(object sender, MouseEventArgs e)
        {
            bMute.BackgroundImage = Resources.button_mute_click;
            XBMC.Request("Mute");
        }

        private void bMute_MouseUp(object sender, MouseEventArgs e)
        {
            if (!XBMC.IsPaused()) bMute.BackgroundImage = Resources.button_mute_hover;
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
            isDragging   = true;
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
                this.Top  = e.Y + this.Top - clickOffsetY;
            }
        }

        private void pbMediaType_Click(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingMediaType() == "Audio" || XBMC.IsPlaying("lastfm")) pbLastFM_Click();
        }
//END FAKE DRAG DROP
    }
}
