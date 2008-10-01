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

        public FullSizeImageF1(string imageUri)
        {
            XBMC = new XBMCcomm();
            InitializeComponent();
            GetImage(imageUri);
        }

        private Image Base64StringToImage(string base64ImageString)
        {
            byte[] imageBytes = Convert.FromBase64String(base64ImageString.ToString());
            MemoryStream imageStream = new MemoryStream(imageBytes);
            Bitmap image = new Bitmap(Image.FromStream(imageStream));

            return image;
        }

        private void GetImage(string imageUri)
        {
            MemoryStream xbmcImageStream = XBMC.GetFileFromXbmc(imageUri);
            
            if(xbmcImageStream == null)
                pbFullSizeImage.Image = Properties.Resources.XBMClogo;
            else
            {
                StreamReader reader = new StreamReader(xbmcImageStream);
                string imageBase64String = reader.ReadToEnd().Replace("<html>\n", "").Replace("\n</html>", "");
                pbFullSizeImage.Image = Base64StringToImage(imageBase64String);
            }
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
