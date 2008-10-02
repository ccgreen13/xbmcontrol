using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XBMC.Communicator;

namespace XBMControl
{
    public partial class FullSizeImageF1 : Form
    {
        private XBMCcomm XBMC;

        public FullSizeImageF1(Image fImage)
        {
            XBMC = new XBMCcomm();
            InitializeComponent();
            ShowImage(fImage);
        }

        private void ShowImage(Image fImage)
        {
            pbFullSizeImage.Image = (fImage == null)? Properties.Resources.XBMClogo : fImage;
        }

        private void pbFullSizeImage_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FullSizeImageF1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void pbFullSizeImage_MouseEnter(object sender, EventArgs e)
        {
            pbFullSizeImage.Cursor = Cursors.Hand;
        }

        private void pbFullSizeImage_MouseLeave(object sender, EventArgs e)
        {
            pbFullSizeImage.Cursor = Cursors.Default;
        }
    }
}
