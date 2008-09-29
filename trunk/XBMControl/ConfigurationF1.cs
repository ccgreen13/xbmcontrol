using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using XBMControl.Properties;
using XBMC.Communicator;
using XBMControl.Language;

namespace XBMControl
{
    public partial class ConfigurationF1 : Form
    {
        XBMCcomm XBMC;
        XBMCLanguage Language;

        public ConfigurationF1()
        {
            XBMC     = new XBMCcomm();
            Language = new XBMCLanguage();
            InitializeComponent();
            LoadConfiguration();
            Language.SetModule("configuration");
            Language.SetLanguage(Settings.Default.Language);
            SetLanguageStrings();
            bApply.Enabled = false;
        }

        private void SetLanguageStrings()
        {
            lLanguageTitle.Text                     = Language.GetString("labelLanguage");
            lIpTitle.Text                           = Language.GetString("labelIp");
            lUsernameTitle.Text                     = Language.GetString("labelUsername");
            lPasswordTitle.Text                     = Language.GetString("labelPassword");
            cbShowInTray.Text                       = Language.GetString("labelShowInTray");
            cbShowInTaskbar.Text                    = Language.GetString("labelShowInTaskbar");
            cbShowNowPlayingBalloonTip.Text         = Language.GetString("labelShowNowPlayingBalloonTip");
            cbShowPlayStatusBalloonTip.Text         = Language.GetString("labelShowPlayStatusBalloonTip");
            cbShowConnectionStatusBalloonTip.Text   = Language.GetString("labelShowConnectionStatusBalloonTip");
            bApply.Text                             = Language.GetString("buttonApply");
            bConfirm.Text                           = Language.GetString("buttonConfirm");
            bCancel.Text                            = Language.GetString("buttonCancel");
        }

        private bool SaveConfiguration()
        {
            if (ip.Text == "")
            {
                MessageBox.Show(Language.GetString("ipRequired"));
                ip.Focus();
                return false;
            }
            else if (!XBMC.IsConnected(ip.Text))
            {
                MessageBox.Show(Language.GetString("invalidIp"));
                ip.Focus();
                return false;
            }
            else
                Settings.Default.Ip = ip.Text;

            Settings.Default.Username                       = username.Text;
            Settings.Default.Password                       = password.Text;
            Settings.Default.ShowInSystemTray               = cbShowInTray.Checked;
            Settings.Default.ShowNowPlayingBalloonTips      = cbShowNowPlayingBalloonTip.Checked;
            Settings.Default.ShowPlayStausBalloonTips       = cbShowPlayStatusBalloonTip.Checked;
            Settings.Default.ShowInTaskbar                  = cbShowInTaskbar.Checked;
            Settings.Default.ShowConnectionStatusBalloonTip = cbShowConnectionStatusBalloonTip.Checked;
            Settings.Default.Language                       = cbLanguage.Text;
            if (!Settings.Default.ShowInSystemTray) Settings.Default.ShowInTaskbar = true;

            Settings.Default.Save();

            return true;
        }

        private void LoadConfiguration()
        {
            SetSystrayChackboxesEnabled(Settings.Default.ShowInSystemTray);
            ip.Text                                  = Settings.Default.Ip;
            username.Text                            = Settings.Default.Username;
            password.Text                            = Settings.Default.Password;
            cbShowInTray.Checked                     = Settings.Default.ShowInSystemTray;
            cbShowNowPlayingBalloonTip.Checked       = Settings.Default.ShowNowPlayingBalloonTips;
            cbShowPlayStatusBalloonTip.Checked      = Settings.Default.ShowPlayStausBalloonTips;
            cbShowInTaskbar.Checked                  = Settings.Default.ShowInTaskbar;
            cbShowConnectionStatusBalloonTip.Checked = Settings.Default.ShowConnectionStatusBalloonTip;
            cbLanguage.Text                          = Settings.Default.Language;
        }

        private void SetSystrayChackboxesEnabled(bool enabled)
        {
            cbShowNowPlayingBalloonTip.Enabled       = enabled;
            cbShowPlayStatusBalloonTip.Enabled      = enabled;
            cbShowInTaskbar.Enabled                  = enabled;
            cbShowConnectionStatusBalloonTip.Enabled = enabled;
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

        private void cbShowInTray_Click(object sender, EventArgs e)
        {
            SetSystrayChackboxesEnabled(cbShowInTray.Checked);
            if (!cbShowInTray.Checked) cbShowInTaskbar.Checked = true;
            bApply.Enabled = true;
        }

        private void cbShowNowPlayingBalloonTip_Click(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void cbShowPlayStatusBalloonTips_Click(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void cbMinimizeToTray_Click(object sender, EventArgs e)
        {
            bApply.Enabled = true;
        }

        private void cbLanguage_TextChanged(object sender, EventArgs e)
        {
            Language.SetLanguage(cbLanguage.Text);
            SetLanguageStrings();
        }
    }
}
