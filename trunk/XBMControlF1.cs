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
using System.Threading;
using XBMControl;
using XBMControl.Properties;
using XBMControl.Language;
using XBMControl.PlayStatusForm;

namespace XBMControl
{
    public partial class MainForm : Form
    {
        private MediaBrowserF1 ShareBrowser;
        internal XBMCcomm XBMC;
        internal XBMCLanguage Language;
        private ConfigurationF1 ConfigForm;
        private FullSizeImageF1 fullSizeImage;
        private VolumeControlF1 sysTrayVolumeControl;
        internal PlaylistF1 Playlist = null;

        internal bool configFormOpened = false;
        internal bool shareBrowserOpened = false;
        internal bool volumeControlOpened = false;

        private const int updateIntervalShort   = 1000;
        private const int updateIntervalLong    = 20000; 
        private string[,] maNowPlayingInfo      = new string[50, 2];

        private bool playStatusMessageShowed    = false;
        private bool showedConnectionStatus     = false;
        private bool resetToDefault             = false;
        private bool isDragging                 = false;
        private int clickOffsetX, clickOffsetY;
        private int originalWindowHeight;

        private bool repeatEnabled              = false; 
       
        public MainForm()
        {
            Language = new XBMCLanguage();
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();
            this.ApplySettings();
            this.SetLanguageStrings();
            this.Initialize();
        }

        private void Initialize()
        {
            originalWindowHeight = this.Height;
            ToggleShowDetails();

            if (XBMC.IsConnected())
            {
                if (!XBMC.WebServerEnabled())
                {
                    MessageBox.Show(Language.GetString("mainform/dialog/webserverDisabled"), Language.GetString("mainform/dialog/webserverDisabledTitle"));
                    this.Dispose();
                }
                else
                {
                    updateTimer.Interval = updateIntervalShort;
                    updateTimer.Enabled = true;
                }
            }
            else
            {
                if (Settings.Default.Ip == "")
                    MessageBox.Show(Language.GetString("mainform/dialog/ipNotConfigured"), Language.GetString("mainform/dialog/ipNotConfiguredTitle"));
                else
                    MessageBox.Show(Language.GetString("mainform/dialog/unableToConnect"), Language.GetString("mainform/dialog/unableToConnectTitle"));

                updateTimer.Interval = updateIntervalLong;
                updateTimer.Enabled = false;
                ShowConfigurationForm();
            }
        }

//START Helper functions
        private void SetControlsEnabled(bool enabled)
        {
            bPrevious.Enabled = enabled;
            bPlay.Enabled = enabled;
            bPause.Enabled = enabled;
            bStop.Enabled = enabled;
            bNext.Enabled = enabled;
            bOpen.Enabled = enabled;
            bMute.Enabled = enabled;
            bRepeat.Enabled = enabled;
            bShuffle.Enabled = enabled;
            bPartymode.Enabled = enabled;
            tbProgress.Enabled = enabled;
            tbVolume.Enabled = enabled;
            cmsXBMC.Visible = enabled;
            cmsControls.Visible = enabled;
            cmsSeperatorFolders.Visible = enabled;
        }

        internal void ApplySettings()
        {
            Language.SetLanguage(Settings.Default.Language);
            notifyIcon1.Visible = Settings.Default.ShowInSystemTray;
            this.Visible = Settings.Default.ShowInTaskbar;
            this.WindowState = (Settings.Default.StartMinimized) ? FormWindowState.Minimized : FormWindowState.Normal;
            this.SetLanguageStrings();
        }

        private void SetLanguageStrings()
        {
            //MainForm
            this.Text = Language.GetString("global/appName") + " v" + Settings.Default.Version;
            lMainTitle.Text = Language.GetString("global/appName") + " v" + Settings.Default.Version;
            lArtistTitle.Text = Language.GetString("mainform/label/artist");
            lTitleTitle.Text = Language.GetString("mainform/label/title");
            lAlbumTitle.Text = Language.GetString("mainform/label/album");

            //Context Menu
            cmsControls.Text = Language.GetString("contextMenu/controls/title");
            cmsPrevious.Text = Language.GetString("contextMenu/controls/previous");
            cmsPlay.Text = Language.GetString("contextMenu/controls/play");
            cmsPause.Text = Language.GetString("contextMenu/controls/pause");
            cmsStop.Text = Language.GetString("contextMenu/controls/stop");
            cmsNext.Text = Language.GetString("contextMenu/controls/next");
            cmsMute.Text = Language.GetString("contextMenu/controls/mute");
            cmsXBMC.Text = Language.GetString("contextMenu/xbmc/title");
            cmsSendMediaUrl.Text = Language.GetString("contextMenu/xbmc/sendMediaUrl");
            cmsShowScreenshot.Text = Language.GetString("contextMenu/xbmc/showScreenshot");
            cmsXBMCreboot.Text = Language.GetString("contextMenu/xbmc/reboot");
            cmsXBMCrestart.Text = Language.GetString("contextMenu/xbmc/restart");
            cmsXBMCshutdown.Text = Language.GetString("contextMenu/xbmc/shutdown");
            cmsSaveMedia.Text = Language.GetString("contextMenu/saveMedia");
            cmsShow.Text = Language.GetString("contextMenu/show");
            cmsHide.Text = Language.GetString("contextMenu/hide");
            cmsConfigure.Text = Language.GetString("contextMenu/configure");
            cmsExit.Text = Language.GetString("contextMenu/exit");
        }
        //END Helper functions

//START Timer events
        internal void UpdateData()
        {
            resetToDefault = (!XBMC.IsConnected() || XBMC.IsNotPlaying()) ? true : false;

            if (XBMC.IsConnected())
            {
                XBMC.GetXbmcProperties();
                SetControlsEnabled(true);
                updateTimer.Interval = updateIntervalShort;
                tbProgress.Value     = XBMC.GetProgress();
                tbVolume.Value       = XBMC.GetVolume();
                SetNowPlayingTimePlayed(resetToDefault);
                GetNowPlayingSongInfo(resetToDefault);
                ShowNowPlayingInfo(resetToDefault);
                ShowPlayStatusInfo();
                SetMediaTypeImage();

                //Set control button states
                bOpen.BackgroundImage   = (shareBrowserOpened) ? Resources.button_open_click : Resources.button_open;
                bPause.BackgroundImage  = (XBMC.IsPaused()) ? Resources.button_pause_click : Resources.button_pause;
                bPlay.BackgroundImage   = (XBMC.IsPlaying()) ? Resources.button_play_click : Resources.button_play;
                bStop.BackgroundImage   = (XBMC.IsNotPlaying()) ? Resources.button_stop_click : Resources.button_stop;
                bMute.BackgroundImage   = (XBMC.IsMuted()) ? Resources.button_mute_click : Resources.button_mute;
                bLastFmHate.Visible     = (XBMC.IsPlaying("lastfm")) ? true : false;
                bLastFmLove.Visible     = (XBMC.GetNowPlayingMediaType() == "Audio") ? true : false;
            }
            else
            {
                if (MessageBox.Show("Do you want to close XBMControl?", this.Language.GetString("mainform/dialog/unableToConnectTitle"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                    this.Dispose();
                else
                {
                    SetControlsEnabled(false);
                    this.ShowConnectionInfo();
                    bPause.BackgroundImage = Resources.button_pause;
                    bPlay.BackgroundImage = Resources.button_play;
                    bMute.BackgroundImage = Resources.button_mute;
                    updateTimer.Interval = updateIntervalLong;
                }
            }

            if (Settings.Default.playlistOpened && Playlist == null && XBMC.IsConnected())
                this.cmsViewPlaylist_Click(null, null);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }
//END Timer events

//START Main window events
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Visible = Settings.Default.ShowInTaskbar;
                if (Settings.Default.playlistOpened && Playlist != null) this.Playlist.Hide();
                if (shareBrowserOpened) this.ShareBrowser.Hide();
            }
            else
            {
                if (Settings.Default.playlistOpened && Playlist != null) this.Playlist.Show();
                if (shareBrowserOpened) this.ShareBrowser.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = Settings.Default.ShowInTaskbar;
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

        private void GetNowPlayingSongInfo(bool resetToDefault)
        {
            if (!XBMC.IsPlaying() && !XBMC.IsPaused())
            {
                lArtistSong.Text    = Language.GetString("mainform/playing/nothing");
                pbThumbnail.Image   = Resources.XBMClogo;
                lBitrate.Text       = "";
                lSamplerate.Text    = "";
                lArtist.Text        = "";
                lTitle.Text         = "";
                lAlbum.Text         = "";
            }
            else if (XBMC.IsNewMediaPlaying())
            {
                if(Settings.Default.playlistOpened && this.Playlist != null) this.Playlist.RefreshPlaylist();
                Image coverArt          = XBMC.GetNowPlayingCoverArt();
                pbThumbnail.Image       = (coverArt == null) ? Resources.XBMClogo : coverArt;
                string year             = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " [" + XBMC.GetNowPlayingInfo("year") + "]";
                lBitrate.Text           = XBMC.GetNowPlayingInfo("bitrate");
                lSamplerate.Text        = XBMC.GetNowPlayingInfo("samplerate");
                string genre            = (XBMC.GetNowPlayingInfo("genre") == null)? "" : " [" + XBMC.GetNowPlayingInfo("genre") + "]";
                string artistLable      = (XBMC.GetNowPlayingInfo("artist") == "" || XBMC.GetNowPlayingInfo("artist") == null) ? "" : XBMC.GetNowPlayingInfo("artist") + " - ";
                lArtistSong.Text        = artistLable + XBMC.GetNowPlayingInfo("title");
                lArtist.Text            = XBMC.GetNowPlayingInfo("artist") + genre;
                lTitle.Text             = XBMC.GetNowPlayingInfo("title") + " [" + XBMC.GetNowPlayingInfo("duration") + "]";
                lAlbum.Text             = XBMC.GetNowPlayingInfo("album") + year;
                pLastFmButtons.Visible  = (XBMC.LastFmEnabled()) ? true : false;
            }
        }

        public void SetMediaTypeImage()
        {
            if (!XBMC.IsPlaying() && !XBMC.IsPaused())
                pbMediaType.Visible = false;
            else
            {
                if (XBMC.GetNowPlayingMediaType() == "Audio" || XBMC.IsPlaying("lastfm"))
                {
                    pbMediaType.Cursor  = Cursors.Hand;
                    pbMediaType.Image   = (XBMC.IsPlaying("lastfm")) ? Resources.lastfm_32x32 : Resources.audio_cd_32x32;
                }
                else if (XBMC.GetNowPlayingMediaType() == "Video")
                {
                    pbMediaType.Cursor  = Cursors.Default;
                    pbMediaType.Image   = Resources.video_32x32;
                }
                else if (XBMC.GetNowPlayingMediaType() == "Picture")
                {
                    pbMediaType.Cursor  = Cursors.Default;
                    pbMediaType.Image   = Resources.pictures_32x32;
                }
                pbMediaType.Visible = true;
            }
        }

        private void SetNowPlayingTimePlayed(bool resetToDefault)
        {
            lTimePlayed.Text = (resetToDefault) ? "00:00" : XBMC.GetNowPlayingInfo("time");
        }

        private void pbThumbnail_Click(object sender, EventArgs e)
        {
            Image coverArt = XBMC.GetNowPlayingCoverArt();
            if (coverArt != null)
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

        private void pbMediaType_Click(object sender, EventArgs e)
        {
            if (XBMC.GetNowPlayingMediaType() == "Audio" || XBMC.IsPlaying("lastfm")) pbLastFM_Click();
        }

        private void pbLastFM_Click()
        {
            string artist = XBMC.GetNowPlayingInfo("artist");
            artist = (artist == null) ? "" : artist.Replace(" ", "+");

            string lastFmUrl = "http://www.last.fm/music/" + artist;
            Help.ShowHelp(this, lastFmUrl);
        }

        private void lArtistSong_Click(object sender, EventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused()) ToggleShowDetails();
        }
 //END Main window events

//START Notification events
        private void ShowNowPlayingInfo(bool resetToDefault)
        {
            if (Settings.Default.ShowNowPlayingBalloonTips)
            {
                if (!XBMC.IsNotPlaying() && XBMC.IsNewMediaPlaying())
                {
                    string currentFilename = XBMC.GetNowPlayingInfo("filename");
                    string genre = (XBMC.GetNowPlayingInfo("genre") == "" || XBMC.GetNowPlayingInfo("genre") == null) ? "" : " [" + XBMC.GetNowPlayingInfo("genre") + "]";
                    string artist = XBMC.GetNowPlayingInfo("artist") + genre + "\n";
                    string duration = XBMC.GetNowPlayingInfo("duration");
                    string time = (duration == "" || duration == null) ? "" : " [" + duration + "]";
                    string title = XBMC.GetNowPlayingInfo("title") + time + "\n";
                    string year = XBMC.GetNowPlayingInfo("year");
                    year = (year == "" || year == null) ? "" : " [" + year + "]";
                    string album = XBMC.GetNowPlayingInfo("album") + year;
                    string lastFM = (currentFilename.Substring(0, 6) == "lastfm") ? "(Last.FM)" : "";

                    notifyIcon1.ShowBalloonTip(2000, "XBMControl : " + Language.GetString("mainform/playing/now") + lastFM, artist + title + album, ToolTipIcon.Info);
                }  
            }
        }

        private void ShowPlayStatusInfo()
        {
            if (Settings.Default.ShowPlayStatusBalloonTips)
            {
                if (XBMC.IsNotPlaying())
                {
                    if (!playStatusMessageShowed)
                    {
                        notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/nothing"), ToolTipIcon.Info);
                        playStatusMessageShowed = true;
                    }
                }
                else if (XBMC.IsPaused())
                {
                    if (!playStatusMessageShowed)
                    {
                        notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/playing/paused"), ToolTipIcon.Info);
                        playStatusMessageShowed = true;
                    }
                }
                else
                    playStatusMessageShowed = false;
            }
        }

        private void ShowConnectionInfo() 
        {
            if (!XBMC.IsConnected() && Settings.Default.ShowConnectionInfo)
            {
                if (!showedConnectionStatus)
                {
                    lArtistSong.Text = Language.GetString("mainform/connection/none");
                    if (Settings.Default.ShowConnectionInfo)
                        notifyIcon1.ShowBalloonTip(2000, Language.GetString("global/appName"), Language.GetString("mainform/connection/none"), ToolTipIcon.Error);
                    else
                        MessageBox.Show(Language.GetString("mainform/connection/none"));

                    showedConnectionStatus = true;
                }
            }
            else
                showedConnectionStatus = false;
        }
//END Notification events

//START Configuration form events
        private void ShowConfigurationForm()
        {
            if (!configFormOpened)
            {
                ConfigForm              = new ConfigurationF1(this);
                ConfigForm.Show();
            }
        }
//END Configuration form events

//START Volume control events
        private void ToggleShowVolumeControl()
        {
            if (XBMC.IsConnected())
            {
                if (!volumeControlOpened)
                {
                    sysTrayVolumeControl = new VolumeControlF1(this);
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
        }

        public void VolumeControlClosed(object sender, EventArgs e)
        {
            volumeControlOpened = false;
        }
//END Volume control events

//START Progress bar events
        private void tbProgress_MouseUp(object sender, MouseEventArgs e)
        {
            XBMC.SeekPercentage(tbProgress.Value);
            updateTimer.Enabled = true;
        }

        private void tbProgress_MouseDown(object sender, MouseEventArgs e)
        {
            updateTimer.Enabled = false;
        }

        private void tbProgress_MouseHover(object sender, EventArgs e)
        {
            tbProgress.Focus();
        }
//END Progress bar events

//START Volume bar events
        private void tbVolume_MouseHover(object sender, EventArgs e)
        {
            tbVolume.Focus();
        }

        private void tbVolume_MouseDown(object sender, MouseEventArgs e)
        {
            updateTimer.Enabled = false;
        }

        private void tbVolume_MouseUp(object sender, MouseEventArgs e)
        {
            XBMC.SetVolume(tbVolume.Value);
            updateTimer.Enabled = true;
        }
//START Volume bar events

//START Context menu events
        private void MainContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (XBMC.IsPlaying())
            {
                //cmsSeperatorSaveMedia.Visible = true;
                //cmsSaveMedia.Visible          = true;
            }
        }

        private void cmsNotifyExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmsNotifyPrevious_Click(object sender, EventArgs e)
        {
            XBMC.Previous();
        }

        private void cmsNotifyPlay_Click(object sender, EventArgs e)
        {
            XBMC.Play();
        }

        private void cmsNotifyPause_Click(object sender, EventArgs e)
        {
            XBMC.Play();
        }

        private void cmsNotifyStop_Click(object sender, EventArgs e)
        {
            XBMC.Stop();
        }

        private void cmsNotifyNext_Click(object sender, EventArgs e)
        {
            XBMC.Next();
        }

        private void cmsNotifyMute_Click(object sender, EventArgs e)
        {
            XBMC.ToggleMute();
        }

        private void cmsNotifyShow_Click(object sender, EventArgs e)
        {
            this.Visible        = true;
            this.WindowState    = System.Windows.Forms.FormWindowState.Normal;
            this.Focus();
        }

        private void cmsNotifyHide_Click(object sender, EventArgs e)
        {
            this.WindowState    = System.Windows.Forms.FormWindowState.Minimized;
            this.Visible        = Settings.Default.ShowInTaskbar;
        }

        private void cmsConfigure_Click(object sender, EventArgs e)
        {
            ShowConfigurationForm();
        }

        private void cmsXBMCrebootComputer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/reboot"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Reboot();
        }

        private void cmsXBMCrebootXBMC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/restart"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Restart();
        }

        private void cmsXBMCshutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("contextMenu/xbmc/shutdown"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                XBMC.Shutdown();
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

        private void cmsSendMediaUrl_Click(object sender, EventArgs e)
        {
            SendMediaUrl SendMedia = new SendMediaUrl(this);
            SendMedia.Show();
        }

        private void cmsViewPlaylist_Click(object sender, EventArgs e)
        {
            if (Playlist == null || !Settings.Default.playlistOpened)
            {
                Playlist = new PlaylistF1(this);
                Playlist.Show();
                Playlist.Top = this.Top;
                Playlist.Left = (this.Left + this.Width);
            }
            else if(Settings.Default.playlistOpened)
                Playlist.Focus();
        }
//END Context menu events

//START Notify icon events
        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (volumeControlOpened) sysTrayVolumeControl.Focus();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ToggleShowMainWindow();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.ToggleShowVolumeControl();
        }

        private void ToggleShowMainWindow()
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.WindowState    = System.Windows.Forms.FormWindowState.Minimized;
                this.Visible        = Settings.Default.ShowInTaskbar;
            }
            else
            {
                this.Visible        = true;
                this.WindowState    = System.Windows.Forms.FormWindowState.Normal;
            }
        }
//END Notify icon events

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
            this.XBMC.Previous();
            if (Settings.Default.playlistOpened && this.Playlist != null)
                this.Playlist.RefreshPlaylist();
        }

        private void bPrevious_MouseUp(object sender, MouseEventArgs e)
        {
            bPrevious.BackgroundImage = Resources.button_previous_hover;
        }
//END PREVIOUS BUTTON

//START PLAY BUTTON
        private void bPlay_MouseEnter(object sender, EventArgs e)
        {
            bPlay.BackgroundImage = Resources.button_play_hover;
        }

        private void bPlay_MouseLeave(object sender, EventArgs e)
        {
            bPlay.BackgroundImage = Resources.button_play_hover;
        }

        private void bPlay_MouseDown(object sender, MouseEventArgs e)
        {
            bPlay.BackgroundImage = Resources.button_play_click;
            if(this.XBMC.IsPaused())
                this.XBMC.Play();
            else if (Settings.Default.playlistOpened && this.Playlist != null)
                this.Playlist.PlaySelectedEntry();
        }

        private void bPlay_MouseUp(object sender, MouseEventArgs e)
        {
            bPlay.BackgroundImage = Resources.button_play_hover;
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
            XBMC.Play();
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
            this.XBMC.Stop();
            if (Settings.Default.playlistOpened && this.Playlist != null)
                this.Playlist.RefreshPlaylist();
        }

        private void bStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (XBMC.IsPlaying() || XBMC.IsPaused()) bStop.BackgroundImage = Resources.button_stop_hover;
            GetNowPlayingSongInfo(resetToDefault);
            if (Settings.Default.ShowNowPlayingBalloonTips) ShowNowPlayingInfo(resetToDefault);
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
            this.XBMC.Next();
            if (Settings.Default.playlistOpened && this.Playlist != null)
                this.Playlist.RefreshPlaylist();
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
            if (!shareBrowserOpened)
            {
                this.ShareBrowser = new MediaBrowserF1(this);
                this.ShareBrowser.Show();
                this.shareBrowserOpened = true;
            }
            else
                ShareBrowser.Focus();
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
            this.XBMC.ToggleMute();
        }

        private void bMute_MouseUp(object sender, MouseEventArgs e)
        {
            if (!this.XBMC.IsPaused()) bMute.BackgroundImage = Resources.button_mute_hover;
        }
//END MUTE BUTTON

//START LASTFM BUTTONS
        private void bLastFmLove_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(this.Language.GetString("mainform/dialog/lastfmLove"), this.Language.GetString("mainform/dialog/lastfmLoveTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.XBMC.LastFmLove();
        }

        private void bLastFmHate_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(this.Language.GetString("mainform/dialog/lastfmHate"), this.Language.GetString("mainform/dialog/lastfmHateTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.XBMC.LastFmHate();
        }
//END LASTFM BUTTONS

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

//START REPEAT BUTTON
        private void bRepeat_MouseEnter(object sender, EventArgs e)
        {
            bRepeat.BackgroundImage = Resources.button_repeat_hover;
        }

        private void bRepeat_MouseLeave(object sender, EventArgs e)
        {
            bRepeat.BackgroundImage = Resources.button_repeat;
        }

        private void bRepeat_MouseDown(object sender, MouseEventArgs e)
        {
            bRepeat.BackgroundImage = Resources.button_repeat_click;
            repeatEnabled           = (repeatEnabled) ? false : true;
            this.XBMC.Repeat(repeatEnabled);  
        }

        private void bRepeat_MouseUp(object sender, MouseEventArgs e)
        {
            bRepeat.BackgroundImage = Resources.button_repeat_hover;
        }
//END REPEAT BUTTON

//START SHUFFLE BUTTON
        private void bShuffle_MouseEnter(object sender, EventArgs e)
        {
            bShuffle.BackgroundImage = Resources.button_shuffle_hover;
        }

        private void bShuffle_MouseLeave(object sender, EventArgs e)
        {
            bShuffle.BackgroundImage = Resources.button_shuffle;
        }

        private void bShuffle_MouseDown(object sender, MouseEventArgs e)
        {
            bShuffle.BackgroundImage = Resources.button_shuffle_click;
            this.XBMC.ToggleShuffle();
        }

        private void bShuffle_MouseUp(object sender, MouseEventArgs e)
        {
            bShuffle.BackgroundImage = Resources.button_shuffle_hover;
        }
//END SHUFFLE BUTTON

//START PARTYMODE BUTTON
        private void bPartymode_MouseEnter(object sender, EventArgs e)
        {
            bPartymode.BackgroundImage = Resources.button_partymode_hover;
        }

        private void bPartymode_MouseLeave(object sender, EventArgs e)
        {
            bPartymode.BackgroundImage = Resources.button_partymode;
        }

        private void bPartymode_MouseDown(object sender, MouseEventArgs e)
        {
            bPartymode.BackgroundImage = Resources.button_partymode_click;
            this.XBMC.TogglePartymode();
        }

        private void bPartymode_MouseUp(object sender, MouseEventArgs e)
        {
            bPartymode.BackgroundImage = Resources.button_partymode_hover;
        }
//END PARTYMODE BUTTON

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
            if (e.Button == MouseButtons.Right)
                MainContextMenu.Show(pToolbar, new System.Drawing.Point(0, pToolbar.Height));
        }

        private void pToolbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                this.Left = e.X + this.Left - clickOffsetX;
                this.Top  = e.Y + this.Top - clickOffsetY;

                if (Settings.Default.playlistOpened && this.Playlist != null)
                {
                    Playlist.Top = this.Top;
                    Playlist.Left = this.Left + this.Width;
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainContextMenu.Show(pToolbar, new System.Drawing.Point(0, pToolbar.Height));
        }
//END FAKE DRAG DROP

        private void cmsMediaBrowser_Click(object sender, EventArgs e)
        {
            this.bOpen_MouseDown(null, null);
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
