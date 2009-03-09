using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Permissions;
using Microsoft.Win32;
using XBMControl.Properties;

//[assembly: RegistryPermissionAttribute(SecurityAction.RequestMinimum, ViewAndModify = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run")]

namespace XBMControl
{
    public partial class ConfigurationF1 : Form
    {
        MainForm parent;
        
        public ConfigurationF1(MainForm parentForm)
        {
            string[] tempNetworkInfo = {"",""};
            

            InitializeComponent();
            parent = parentForm;

            if (parent.XBMC.GetIp() != "")
                tempNetworkInfo = parent.XBMC.GetIp().Split(':');

            tbIp.Text = tempNetworkInfo[0];
            tbPort.Text = tempNetworkInfo[1];

            if (parent.XBMC.getPowerSaver() == true)
                cbPowerSaver.Checked = true;
            else
                cbPowerSaver.Checked = false;
           
        }

        private bool IsValidIp()
        {
            parent.XBMC.SetIp(tbIp.Text + ":" + tbPort.Text);

            if (tbIp.Text == "")
            {
                MessageBox.Show("IP Address not configured");
                return false;
            }
            else if (!parent.XBMC.Status.IsConnected())
            {
                MessageBox.Show("Is not Connected");
                return false;
            }
            else
                return true;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            parent.XBMC.SetCredentials(tbUsername.Text, tbPassword.Text);
            if (IsValidIp())
            {
                parent.XBMC.SetPowerSaver(cbPowerSaver.Checked);
                //this.SaveConfiguration();
                this.Close();
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (tbIp.Text == "")
                MessageBox.Show("IP Address required");

            Close();

        }

        private void cbPowerSaver_CheckStateChanged(object sender, EventArgs e)
        {
            //parent.XBMC.SetPowerSaver(cbPowerSaver.Checked);
        }

    }
}