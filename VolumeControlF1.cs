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
        private bool hadFocus        = false;
        private bool connectedToXbmc = false;

        public VolumeControlF1()
        {
            XBMC = new XBMCcomm();
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            connectedToXbmc = XBMC.IsConnected(XBMC.GetXbmcIp());

            if (connectedToXbmc)
            {
                XBMC.GetXbmcProperties();
                this.GetCurrentVolume();
                this.timer1.Enabled = true;
            }
            else
                this.Close();
        }

        private void GetCurrentVolume()
        {
            XBMC.GetXbmcProperties();
            tbVolumeSysTray.Value = XBMC.GetVolume();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            XBMC.SetVolume(tbVolumeSysTray.Value);
        }

        private void tbVolumeSysTray_LostFocus(object sender, EventArgs e)
        {
            if (hadFocus) this.Dispose();   
        }

        private void VolumeControlF1_MouseLeave(object sender, EventArgs e)
        {
            this.hadFocus = true;
        }

        private void tbVolumeSysTray_MouseLeave(object sender, EventArgs e)
        {
            this.hadFocus = true;
        }

        private void tbVolumeSysTray_MouseHover(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
            this.hadFocus = true;
        }

        private void VolumeControlF1_MouseHover(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
            this.hadFocus = true;
        }

        private void tbVolumeSysTray_MouseEnter(object sender, EventArgs e)
        {
            tbVolumeSysTray.Focus();
            this.hadFocus = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (XBMC.IsConnected(Settings.Default.Ip))
                this.GetCurrentVolume();
            else
                this.Close();
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

        
    }
}
