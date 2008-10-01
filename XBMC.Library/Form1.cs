using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMC.Communicator;

namespace XBMControl.XBMC.Library
{
    public partial class Form1 : Form
    {
        private XBMCcomm XBMC;

        public Form1()
        {
            XBMC = new XBMCcomm();
            InitializeComponent();
        }
    }
}
