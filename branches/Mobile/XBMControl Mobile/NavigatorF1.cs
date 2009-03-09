using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XBMControl
{
    public partial class NavigatorF1 : Form
    {
        private MainForm parent;
        private ImageButton buttonHome;
        private ImageButton buttonOptions;
        private ImageButton buttonBack;
        private ImageButton buttonUp;
        private ImageButton buttonLeft;
        private ImageButton buttonSelect;
        private ImageButton buttonRight;
        private ImageButton buttonDown;
        private ImageButton buttonVolumeDown;
        private ImageButton buttonRewind;
        private ImageButton buttonStop;
        private ImageButton buttonVolumeUp;
        private ImageButton buttonPausePlay;
        private ImageButton buttonForward;

        public NavigatorF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
            this.Owner = parent;

            buttonHome = new ImageButton();
            buttonHome.Image = XBMControl.Properties.Resources.home;
            buttonHome.Location = new Point(32,3);
            buttonHome.Size = new Size(48, 48);
            //Hook up into click event
            buttonHome.Click += new EventHandler(buttonHome_Click);
            this.Controls.Add(buttonHome);

            buttonOptions = new ImageButton();
            buttonOptions.Image = XBMControl.Properties.Resources.options;
            buttonOptions.Location = new Point(96, 3);
            buttonOptions.Size = new Size(48, 48);
            //Hook up into click event
            buttonOptions.Click += new EventHandler(buttonOptions_Click);
            this.Controls.Add(buttonOptions);

            buttonBack = new ImageButton();
            buttonBack.Image = XBMControl.Properties.Resources.back;
            buttonBack.Location = new Point(160, 3);
            buttonBack.Size = new Size(48, 48);
            //Hook up into click event
            buttonBack.Click += new EventHandler(buttonBack_Click);
            this.Controls.Add(buttonBack);

            buttonUp = new ImageButton();
            buttonUp.Image = XBMControl.Properties.Resources.up;
            buttonUp.Location = new Point(96, 57);
            buttonUp.Size = new Size(48, 48);
            //Hook up into click event
            buttonUp.Click += new EventHandler(buttonUp_Click);
            this.Controls.Add(buttonUp);

            buttonLeft = new ImageButton();
            buttonLeft.Image = XBMControl.Properties.Resources.left;
            buttonLeft.Location = new Point(42, 111);
            buttonLeft.Size = new Size(48, 48);
            //Hook up into click event
            buttonLeft.Click += new EventHandler(buttonLeft_Click);
            this.Controls.Add(buttonLeft);

            buttonSelect = new ImageButton();
            buttonSelect.Image = XBMControl.Properties.Resources.select;
            buttonSelect.Location = new Point(96,111);
            buttonSelect.Size = new Size(48, 48);
            //Hook up into click event
            buttonSelect.Click += new EventHandler(buttonSelect_Click);
            this.Controls.Add(buttonSelect);

            buttonRight = new ImageButton();
            buttonRight.Image = XBMControl.Properties.Resources.right;
            buttonRight.Location = new Point(150,111);
            buttonRight.Size = new Size(48, 48);
            //Hook up into click event
            buttonRight.Click += new EventHandler(buttonRight_Click);
            this.Controls.Add(buttonRight);

            buttonDown = new ImageButton();
            buttonDown.Image = XBMControl.Properties.Resources.down;
            buttonDown.Location = new Point(96,165);
            buttonDown.Size = new Size(48, 48);
            //Hook up into click event
            buttonDown.Click += new EventHandler(buttonDown_Click);
            this.Controls.Add(buttonDown);

            buttonVolumeDown = new ImageButton();
            buttonVolumeDown.Image = XBMControl.Properties.Resources.vol_down;
            buttonVolumeDown.Location = new Point(3,165);
            buttonVolumeDown.Size = new Size(48, 48);
            //Hook up into click event
            buttonVolumeDown.Click += new EventHandler(bVolDown_Click);
            this.Controls.Add(buttonVolumeDown);

            buttonVolumeUp = new ImageButton();
            buttonVolumeUp.Image = XBMControl.Properties.Resources.vol_up;
            buttonVolumeUp.Location = new Point(189, 165);
            buttonVolumeUp.Size = new Size(48, 48);
            //Hook up into click event
            buttonVolumeUp.Click += new EventHandler(bVolUp_Click);
            this.Controls.Add(buttonVolumeUp);

            buttonRewind = new ImageButton();
            buttonRewind.Image = XBMControl.Properties.Resources.rewind;
            buttonRewind.Location = new Point(3,220);
            buttonRewind.Size = new Size(48, 48);
            //Hook up into click event
            buttonRewind.Click += new EventHandler(buttonRewind_Click);
            this.Controls.Add(buttonRewind);

            buttonStop = new ImageButton();
            buttonStop.Image = XBMControl.Properties.Resources.stop;
            buttonStop.Location = new Point(57,220);
            buttonStop.Size = new Size(48, 48);
            //Hook up into click event
            buttonStop.Click += new EventHandler(buttonStop_Click);
            this.Controls.Add(buttonStop);

            buttonPausePlay = new ImageButton();
            buttonPausePlay.Image = XBMControl.Properties.Resources.play_pause;
            buttonPausePlay.Location = new Point(125,220);
            buttonPausePlay.Size = new Size(48, 48);
            //Hook up into click event
            buttonPausePlay.Click += new EventHandler(buttonPausePlay_Click);
            this.Controls.Add(buttonPausePlay);

            buttonForward = new ImageButton();
            buttonForward.Image = XBMControl.Properties.Resources.forward;
            buttonForward.Location = new Point(189,220);
            buttonForward.Size = new Size(48, 48);
            //Hook up into click event
            buttonForward.Click += new EventHandler(buttonForward_Click);
            this.Controls.Add(buttonForward);
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("10000");

        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("10500");

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("10");

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("3");
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("2");
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("4");
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("1");
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            parent.XBMC.Video.sendAction("7");
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            parent.XBMC.Controls.Stop();
        }

        private void buttonPausePlay_Click(object sender, EventArgs e)
        {
            parent.XBMC.Controls.Play();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            parent.XBMC.Controls.Next();
        }

        private void buttonRewind_Click(object sender, EventArgs e)
        {
            parent.XBMC.Controls.Previous();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pNavigator_Click(object sender, EventArgs e)
        {

        }

        private void bVolDown_Click(object sender, EventArgs e)
        {
            int tempVolume;

            tempVolume = parent.XBMC.Status.GetVolume();

            tempVolume -= 10;
            if (tempVolume < 0)
                tempVolume = 0;

            if (parent.XBMC.Status.IsConnected())
            {
                parent.XBMC.Controls.SetVolume(tempVolume);
            }

        }

        private void bVolUp_Click(object sender, EventArgs e)
        {
            int tempVolume;

            tempVolume = parent.XBMC.Status.GetVolume();

            tempVolume += 10;
            if (tempVolume > 99)
                tempVolume = 99;

            if (parent.XBMC.Status.IsConnected())
            {
                parent.XBMC.Controls.SetVolume(tempVolume);
            }
        }
    }
}