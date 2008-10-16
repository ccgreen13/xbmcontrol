using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMControl.Properties;
using XBMC.Communicator;
//using XBMControl.Language;

namespace XBMControl
{
    public partial class VolumeControlF1 : Form
    {
        private XBMCcomm XBMC;
        private bool connectedToXbmc = false;

        public VolumeControlF1()
        {
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (XBMC.IsConnected())
            {
                connectedToXbmc = XBMC.IsConnected();
                XBMC.GetXbmcProperties();
                GetCurrentVolume();
                timer1.Enabled = true;
                if (XBMC.IsMuted()) bMute.BackgroundImage = Resources.button_mute_click;
            }
            else
                this.Close();
        }

        private void GetCurrentVolume()
        {
            XBMC.GetXbmcProperties();
            tbVolumeSysTray.Value = XBMC.GetVolume();
        }

        private void VolumeControlF1_LostFocus(object sender, EventArgs e)
        {
            if (!tbVolumeSysTray.Focused && !bMute.Focused) this.Dispose();
        }

        private void tbVolumeSysTray_LostFocus(object sender, EventArgs e)
        {
            if (!this.Focused && !bMute.Focused) this.Dispose();
        }

        private void bMute_LostFocus(object sender, EventArgs e)
        {
            if (!this.Focused && !tbVolumeSysTray.Focused) this.Dispose();
        }

        private void tbVolumeSysTray_MouseHover(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
        }

        private void VolumeControlF1_MouseHover(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connectedToXbmc)
                this.GetCurrentVolume();
            else
                this.Close();

            if (XBMC.IsMuted()) bMute.BackgroundImage = Resources.button_mute_click;
        }

        private void tbVolumeSysTray_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void tbVolumeSysTray_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void bMute_Click(object sender, EventArgs e)
        {
            XBMC.ToggleMute();
        }

        private void tbVolumeSysTray_ValueChanged(object sender, EventArgs e)
        {
            XBMC.SetVolume(tbVolumeSysTray.Value);
        }

        private void bMute_MouseHover(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
        }

        private void bMute_MouseEnter(object sender, EventArgs e)
        {
            bMute.BackgroundImage = Resources.button_mute_hover;
        }

        private void bMute_MouseDown(object sender, MouseEventArgs e)
        {
            bMute.BackgroundImage = Resources.button_mute_click;
        }

        private void bMute_MouseLeave(object sender, EventArgs e)
        {
            bMute.BackgroundImage = Resources.button_mute;
        }

        private void bMute_MouseUp(object sender, MouseEventArgs e)
        {
            bMute.BackgroundImage = Resources.button_mute_hover;
        }
    }
}
