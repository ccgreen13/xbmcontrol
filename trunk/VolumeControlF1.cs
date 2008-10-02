using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;
//using XBMControl.Language;

namespace XBMControl
{
    public partial class VolumeControlF1 : Form
    {
        private XBMCcomm XBMC;
        private bool hadFocus = false;

        public VolumeControlF1()
        {
            XBMC = new XBMCcomm();
            InitializeComponent();
            GetVolume();
        }

        private void GetVolume()
        {
            string[] aCurVolume   = XBMC.Request("GetVolume");
            int newVolumePosition = (aCurVolume.Length > 1 && aCurVolume[1] != "Error") ? Convert.ToInt32(aCurVolume[1]) : 0;
            tbVolumeSysTray.Value = newVolumePosition;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            XBMC.Request("SetVolume", Convert.ToString(tbVolumeSysTray.Value));
        }

        private void tbVolumeSysTray_LostFocus(object sender, EventArgs e)
        {
            if (hadFocus) this.Close();   
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
            this.hadFocus = true;
        }

        private void VolumeControlF1_MouseHover(object sender, EventArgs e)
        {
            this.hadFocus = true;
        }

        
    }
}
