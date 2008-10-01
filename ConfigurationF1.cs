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
            Language.SetLanguage(XBMControl.Properties.Settings.Default.Language);
            SetLanguageStrings();
        }

        private void SetLanguageStrings()
        {
            this.Text                               = Language.GetString("global/appName") + " " + Language.GetString("configuration/title");
            lLanguageTitle.Text                     = Language.GetString("configuration/label/language");
            lIpTitle.Text                           = Language.GetString("configuration/label/ip");
            lUsernameTitle.Text                     = Language.GetString("configuration/label/username");
            lPasswordTitle.Text                     = Language.GetString("configuration/label/password");
            cbShowInTray.Text                       = Language.GetString("configuration/label/showInTray");
            cbShowInTaskbar.Text                    = Language.GetString("configuration/label/showInTaskbar");
            cbShowNowPlayingBalloonTip.Text         = Language.GetString("configuration/label/showNowPlayingBalloonTip");
            cbShowPlayStatusBalloonTip.Text         = Language.GetString("configuration/label/showPlayStatusBalloonTip");
            cbShowConnectionStatusBalloonTip.Text   = Language.GetString("configuration/label/showConnectionStatusBalloonTip");
            bConfirm.Text                           = Language.GetString("global/button/confirm");
            bCancel.Text                            = Language.GetString("global/button/cancel");
        }

        private void SaveConfiguration()
        {
            XBMControl.Properties.Settings.Default.Ip                             = tbIp.Text;
            XBMControl.Properties.Settings.Default.Username                       = tbUsername.Text;
            XBMControl.Properties.Settings.Default.Password                       = tbPassword.Text;
            XBMControl.Properties.Settings.Default.ShowInSystemTray               = cbShowInTray.Checked;
            XBMControl.Properties.Settings.Default.ShowNowPlayingBalloonTips      = cbShowNowPlayingBalloonTip.Checked;
            XBMControl.Properties.Settings.Default.ShowPlayStausBalloonTips       = cbShowPlayStatusBalloonTip.Checked;
            XBMControl.Properties.Settings.Default.ShowInTaskbar                  = cbShowInTaskbar.Checked;
            XBMControl.Properties.Settings.Default.ShowConnectionStatusBalloonTip = cbShowConnectionStatusBalloonTip.Checked;
            XBMControl.Properties.Settings.Default.Language                       = cbLanguage.Text;
            if (!XBMControl.Properties.Settings.Default.ShowInSystemTray) XBMControl.Properties.Settings.Default.ShowInTaskbar = true;

            XBMControl.Properties.Settings.Default.Save();
        }

        private bool IsValidIp()
        {
            if (tbIp.Text == "")
            {
                MessageBox.Show(Language.GetString("configuration/ipAddress/required"));
                return false;
            }
            else if (!XBMC.IsConnected(tbIp.Text))
            {
                if (MessageBox.Show(Language.GetString("configuration/ipAddress/proceedMessage"), Language.GetString("global/appName"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private void LoadConfiguration()
        {
            ShowAvailableLanguages();
            SetSystrayChackboxesEnabled(XBMControl.Properties.Settings.Default.ShowInSystemTray);
            tbIp.Text                                = XBMControl.Properties.Settings.Default.Ip;
            tbUsername.Text                          = XBMControl.Properties.Settings.Default.Username;
            tbPassword.Text                          = XBMControl.Properties.Settings.Default.Password;
            cbShowInTray.Checked                     = XBMControl.Properties.Settings.Default.ShowInSystemTray;
            cbShowNowPlayingBalloonTip.Checked       = XBMControl.Properties.Settings.Default.ShowNowPlayingBalloonTips;
            cbShowPlayStatusBalloonTip.Checked       = XBMControl.Properties.Settings.Default.ShowPlayStausBalloonTips;
            cbShowInTaskbar.Checked                  = XBMControl.Properties.Settings.Default.ShowInTaskbar;
            cbShowConnectionStatusBalloonTip.Checked = XBMControl.Properties.Settings.Default.ShowConnectionStatusBalloonTip;
            cbLanguage.Text                          = XBMControl.Properties.Settings.Default.Language;
        }

        private void SetSystrayChackboxesEnabled(bool enabled)
        {
            cbShowNowPlayingBalloonTip.Enabled       = enabled;
            cbShowPlayStatusBalloonTip.Enabled       = enabled;
            cbShowInTaskbar.Enabled                  = enabled;
            cbShowConnectionStatusBalloonTip.Enabled = enabled;
        }

        private void ShowAvailableLanguages()
        {
            string[] languages = Language.GetAvailableLanguages();

            if (languages.Length > 0)
            {
                cbLanguage.Items.Clear();
                foreach(string lang in languages)
                    cbLanguage.Items.Add(lang);
            }
            else
                MessageBox.Show(Language.GetString("configuration/language/noLanguages"));
        }

        private void ConfigurationF1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            if( tbIp.Text == "" )
                MessageBox.Show(Language.GetString("configuration/ipAddress/required"));
            else
                Close();
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            if (IsValidIp())
            {
                SaveConfiguration();
                Close();
            }
        }

        private void cbShowInTray_Click(object sender, EventArgs e)
        {
            SetSystrayChackboxesEnabled(cbShowInTray.Checked);
            if (!cbShowInTray.Checked) cbShowInTaskbar.Checked = true;
        }

        private void cbLanguage_TextChanged(object sender, EventArgs e)
        {
            Language.SetLanguage(cbLanguage.Text);
            SetLanguageStrings();
        }
    }
}
