using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    public partial class ConfigurationF1 : Form
    {
        public ConfigurationF1()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private bool SaveConfiguration()
        {
            if (ip.Text == "")
            {
                MessageBox.Show("The 'ip address' is a required field.");
                ip.Focus();
                return false;
            }
            else if (!HostExists(ip.Text))
            {
                MessageBox.Show("Invalid ip address. Could not connect to host.");
                ip.Focus();
                return false;
            }
            else
                Settings.Default.Ip = ip.Text;

            Settings.Default.Username   = username.Text;
            Settings.Default.Password   = password.Text;
            Settings.Default.Save();

            return true;
        }

        private void LoadConfiguration()
        {
            ip.Text         = Settings.Default.Ip;
            username.Text   = Settings.Default.Username;
            password.Text   = Settings.Default.Password;
        }

        private bool HostExists(string host)
        {
            HttpWebResponse response = null;
            HttpWebRequest connection = (HttpWebRequest)WebRequest.Create("http://" + host);
            connection.Method = "GET";

            try
            {
                response = (HttpWebResponse)connection.GetResponse();
            }
            catch (Exception e)
            {
                return false;
            }

            if (response != null) response.Close();

            return true ;
        }

        private void ConfigurationF1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void tbIp_TextChanged(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            if(SaveConfiguration())
                Close();
        }

        private void bApply_Click(object sender, EventArgs e)
        {
            if(SaveConfiguration())
                bApply.Enabled = false;
        }
    }
}
