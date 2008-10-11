using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;
using XBMControl.Animations;

namespace XBMControl.PlayStatusForm
{
    public partial class PlayStatusF1 : Form
    {
        XBMCcomm XBMC; 
        Animation Animate;
        DateTime startTime;
        int screenHeight;
        int screenWidth;
        double hideTimeout = 10.0;
        bool activated = false;

        public PlayStatusF1()
        {
            XBMC          = new XBMCcomm(); 
            Animate       = new Animation();
            screenHeight  = SystemInformation.PrimaryMonitorSize.Height;
            screenWidth   = SystemInformation.PrimaryMonitorSize.Width;
            InitializeComponent();
            SetFormPosition();
            ShowCoverArt();
        }

        private void SetFormPosition()
        {
            int pixelLeft           = screenWidth - this.Width;
            int pixelTop            = screenHeight - (this.Height + 32);
            this.DesktopLocation    = new Point(pixelLeft, pixelTop);
        }

        private void ShowCoverArt()
        {
            Image coverArt = XBMC.GetNowPlayingCoverArt();
            pbThumbnail.Image = (coverArt == null)? Properties.Resources.XBMClogo : coverArt;
        }
        
        private void UpdateMediaInfo()
        {
            string year       = (XBMC.GetNowPlayingInfo("year") == null) ? "" : " [" + XBMC.GetNowPlayingInfo("year") + "]";
            lArtist.Text      = XBMC.GetNowPlayingInfo("artist");
            string duration   = XBMC.GetNowPlayingInfo("duration");
            string time       = (duration == null) ? "" : " [" + duration + "]";
            lTitle.Text       = XBMC.GetNowPlayingInfo("title") + time;
            lAlbum.Text       = XBMC.GetNowPlayingInfo("album") + year;
        }

        private void PlayStatusF1_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                startTime = DateTime.Now;
                XBMC.GetXbmcProperties();
                UpdateMediaInfo();
                Animate.StartFadeIn(this);
                hideFormTimer.Enabled = true;
                activated = true;
            }
        }

        private void hideFormTimer_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - startTime).TotalSeconds > hideTimeout)
                Animate.StartFadeOut(this);

            if (this.Opacity == 0)
            {
                hideFormTimer.Enabled = false;
                activated = false;
                this.Hide();
            }
        }

        private void PlayStatusF1_Click(object sender, EventArgs e)
        {
            Animate.StartFadeOut(this);
        }
    }
}
