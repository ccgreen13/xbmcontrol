using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using XBMControl.Properties;
using XBMC.Communicator;

namespace XBMControl
{
    public partial class SendMediaUrl : Form
    {
        XBMCcomm XBMC;

        public SendMediaUrl()
        {
            XBMC = new XBMCcomm();
            XBMC.SetXbmcIp(Settings.Default.Ip);
            XBMC.SetCredentials(Settings.Default.Username, Settings.Default.Password);
            InitializeComponent();
        }

        private void bSendMediaUrl_Click(object sender, EventArgs e)
        {
            if (tbMediaUrl.Text != "") XBMC.PlayMedia(tbMediaUrl.Text);
            Close();
        }
    }
}
