using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBMControl.Properties;

namespace XBMControl
{
    public partial class NavigatorF1 : Form
    {
        private MainForm parent;
        private bool isDragging = false;
        private int clickOffsetX, clickOffsetY;

        public NavigatorF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
            if (parent.XBMC.Status.IsConnected())
            {
                parent.XBMC.Status.Refresh();
            }

            Settings.Default.NavigatorOpened = true;
            Settings.Default.Save();

            this.Owner = parent;
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("270");
        }

        private void bLeft_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("272");
        }

        private void bRight_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("273");
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("271");
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Settings.Default.NavigatorOpened = false;
            Settings.Default.Save();
            this.Dispose();
        }

        
        //START FAKE DRAG DROP
        private void pToolbar_MouseDown(object sender, MouseEventArgs e)
        {
            parent.Focus();
            this.Focus();
            isDragging = true;
            clickOffsetX = e.X;
            clickOffsetY = e.Y;
        }

        private void pToolbar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pToolbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                this.Left = e.X + this.Left - clickOffsetX;
                this.Top = e.Y + this.Top - clickOffsetY;
            }
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("256");
        }

        private void bUndo_Click_1(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("275");
        }

        private void NavigatorF1_Load(object sender, EventArgs e)
        {
        }
        //END FAKE DRAG DROP
    }
}
