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
            this.BackgroundImage = (fImage == null)? Properties.Resources.XBMClogo : fImage;
            this.Width = fImage.Width;
            this.Height = fImage.Height;
        }

        private void FullSizeImageF1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void FullSizeImageF1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
