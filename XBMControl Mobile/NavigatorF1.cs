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
        private Int16[,] arrayName;

        private Int16[,] buttons240x320 = new Int16[14, 4] {
        // 240x320
            {032, 003, 48, 48},   // Home   
            {096, 003, 48, 48},   // Options   
            {160, 003, 48, 48},   // Back   
            {096, 057, 48, 48},   // Up  
            {042, 111, 48, 48},   // Left  
            {096, 111, 48, 48},   // Select  
            {150, 111, 48, 48},   // Right  
            {096, 165, 48, 48},   // Down  
            {003, 165, 48, 48},   // Volume Down 
            {189, 165, 48, 48},   // Volume Up 
            {003, 220, 48, 48},   // Rewind 
            {057, 220, 48, 48},   // Stop 
            {125, 220, 48, 48},   // Play/Pause 
            {189, 220, 48, 48},   // Forward 
        };


        private Int16[,] buttons240x240 = new Int16[14, 4] {
        // 240x240
            {151, 003, 40, 40},   // Home   
            {197, 049, 40, 40},   // Options   
            {197, 003, 40, 40},   // Back   
            {049, 003, 40, 40},   // Up  
            {003, 049, 40, 40},   // Left  
            {049, 049, 40, 40},   // Select  
            {095, 049, 40, 40},   // Right  
            {049, 095, 40, 40},   // Down  
            {197, 145, 40, 40},   // Volume Down 
            {197, 099, 40, 40},   // Volume Up 
            {003, 145, 40, 40},   // Rewind 
            {049, 145, 40, 40},   // Stop 
            {095, 145, 40, 40},   // Play/Pause 
            {141, 145, 40, 40},   // Forward 
        };

        private Int16[,] buttons320x320 = new Int16[14, 4] {
        // 320x320
            {199, 003, 56, 56},   // Home   
            {261, 065, 56, 56},   // Options   
            {261, 003, 56, 56},   // Back   
            {065, 003, 56, 56},   // Up  
            {003, 065, 56, 56},   // Left  
            {065, 065, 56, 56},   // Select  
            {127, 065, 56, 56},   // Right  
            {065, 127, 56, 56},   // Down  
            {261, 192, 56, 56},   // Volume Down 
            {261, 130, 56, 56},   // Volume Up 
            {003, 192, 56, 56},   // Rewind 
            {065, 192, 56, 56},   // Stop 
            {127, 192, 56, 56},   // Play/Pause 
            {189, 192, 56, 56},   // Forward 
        };

        private Int16[,] buttons480x480 = new Int16[14, 4] {
        // 480x480
            {303, 013, 80, 80},   // Home   
            {389, 099, 80, 80},   // Options   
            {389, 013, 80, 80},   // Back   
            {123, 013, 80, 80},   // Up  
            {037, 099, 80, 80},   // Left  
            {123, 099, 80, 80},   // Select  
            {209, 099, 80, 80},   // Right  
            {123, 185, 80, 80},   // Down  
            {389, 195, 80, 80},   // Volume Down 
            {389, 281, 80, 80},   // Volume Up 
            {015, 281, 80, 80},   // Rewind 
            {101, 281, 80, 80},   // Stop 
            {187, 281, 80, 80},   // Play/Pause 
            {273, 281, 80, 80},   // Forward 
        };

        private Int16[,] buttons640x480 = new Int16[14, 4] {
        // 640x480
            {067, 015, 96, 96},   // Home   
            {184, 015, 96, 96},   // Options   
            {298, 015, 96, 96},   // Back   
            {184, 128, 96, 96},   // Up  
            {086, 226, 96, 96},   // Left  
            {184, 226, 96, 96},   // Select  
            {282, 226, 96, 96},   // Right  
            {184, 324, 96, 96},   // Down  
            {022, 128, 96, 96},   // Volume Down 
            {346, 128, 96, 96},   // Volume Up 
            {022, 430, 96, 96},   // Rewind 
            {132, 430, 96, 96},   // Stop 
            {239, 430, 96, 96},   // Play/Pause 
            {345, 430, 96, 96},   // Forward 
        };

        public NavigatorF1(MainForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
            this.Owner = parent;

            if (Screen.PrimaryScreen.Bounds.Height == 240 && Screen.PrimaryScreen.Bounds.Width == 240)
                arrayName = buttons240x240;
            else if (Screen.PrimaryScreen.Bounds.Height == 320 && Screen.PrimaryScreen.Bounds.Width == 320)
                arrayName = buttons320x320;
            else if (Screen.PrimaryScreen.Bounds.Height == 480 && Screen.PrimaryScreen.Bounds.Width == 480)
                arrayName = buttons480x480;
            else if (Screen.PrimaryScreen.Bounds.Height == 640 && Screen.PrimaryScreen.Bounds.Width == 480)
                arrayName = buttons640x480;
            else
                arrayName = buttons240x320;

            buttonHome = new ImageButton();
            buttonHome.Image = XBMControl.Properties.Resources.home;
            buttonHome.Location = new Point(arrayName[0, 0], arrayName[0, 1]);
            buttonHome.Size = new Size(arrayName[0, 2], arrayName[0, 3]);
            //Hook up into click event
            buttonHome.Click += new EventHandler(buttonHome_Click);
            this.Controls.Add(buttonHome);

            buttonOptions = new ImageButton();
            buttonOptions.Image = XBMControl.Properties.Resources.options;
            buttonOptions.Location = new Point(arrayName[1, 0], arrayName[1, 1]);
            buttonOptions.Size = new Size(arrayName[1, 2], arrayName[1, 3]);
            //Hook up into click event
            buttonOptions.Click += new EventHandler(buttonOptions_Click);
            this.Controls.Add(buttonOptions);

            buttonBack = new ImageButton();
            buttonBack.Image = XBMControl.Properties.Resources.back;
            buttonBack.Location = new Point(arrayName[2, 0], arrayName[2, 1]);
            buttonBack.Size = new Size(arrayName[2, 2], arrayName[2, 3]);
            //Hook up into click event
            buttonBack.Click += new EventHandler(buttonBack_Click);
            this.Controls.Add(buttonBack);

            buttonUp = new ImageButton();
            buttonUp.Image = XBMControl.Properties.Resources.up;
            buttonUp.Location = new Point(arrayName[3, 0], arrayName[3, 1]);
            buttonUp.Size = new Size(arrayName[3, 2], arrayName[3, 3]);
            //Hook up into click event
            buttonUp.Click += new EventHandler(buttonUp_Click);
            this.Controls.Add(buttonUp);

            buttonLeft = new ImageButton();
            buttonLeft.Image = XBMControl.Properties.Resources.left;
            buttonLeft.Location = new Point(arrayName[4, 0], arrayName[4, 1]);
            buttonLeft.Size = new Size(arrayName[4, 2], arrayName[4, 3]);
            //Hook up into click event
            buttonLeft.Click += new EventHandler(buttonLeft_Click);
            this.Controls.Add(buttonLeft);

            buttonSelect = new ImageButton();
            buttonSelect.Image = XBMControl.Properties.Resources.select;
            buttonSelect.Location = new Point(arrayName[5, 0], arrayName[5, 1]);
            buttonSelect.Size = new Size(arrayName[5, 2], arrayName[5, 3]);
            //Hook up into click event
            buttonSelect.Click += new EventHandler(buttonSelect_Click);
            this.Controls.Add(buttonSelect);

            buttonRight = new ImageButton();
            buttonRight.Image = XBMControl.Properties.Resources.right;
            buttonRight.Location = new Point(arrayName[6, 0], arrayName[6, 1]);
            buttonRight.Size = new Size(arrayName[6, 2], arrayName[6, 3]);
            //Hook up into click event
            buttonRight.Click += new EventHandler(buttonRight_Click);
            this.Controls.Add(buttonRight);

            buttonDown = new ImageButton();
            buttonDown.Image = XBMControl.Properties.Resources.down;
            buttonDown.Location = new Point(arrayName[7, 0], arrayName[7, 1]);
            buttonDown.Size = new Size(arrayName[7, 2], arrayName[7, 3]);
            //Hook up into click event
            buttonDown.Click += new EventHandler(buttonDown_Click);
            this.Controls.Add(buttonDown);

            buttonVolumeDown = new ImageButton();
            buttonVolumeDown.Image = XBMControl.Properties.Resources.vol_down;
            buttonVolumeDown.Location = new Point(arrayName[8, 0], arrayName[8, 1]);
            buttonVolumeDown.Size = new Size(arrayName[8, 2], arrayName[8, 3]);
            //Hook up into click event
            buttonVolumeDown.Click += new EventHandler(bVolDown_Click);
            this.Controls.Add(buttonVolumeDown);

            buttonVolumeUp = new ImageButton();
            buttonVolumeUp.Image = XBMControl.Properties.Resources.vol_up;
            buttonVolumeUp.Location = new Point(arrayName[9, 0], arrayName[9, 1]);
            buttonVolumeUp.Size = new Size(arrayName[9, 2], arrayName[9, 3]);
            //Hook up into click event
            buttonVolumeUp.Click += new EventHandler(bVolUp_Click);
            this.Controls.Add(buttonVolumeUp);

            buttonRewind = new ImageButton();
            buttonRewind.Image = XBMControl.Properties.Resources.rewind;
            buttonRewind.Location = new Point(arrayName[10, 0], arrayName[10, 1]);
            buttonRewind.Size = new Size(arrayName[10, 2], arrayName[10, 3]);
            //Hook up into click event
            buttonRewind.Click += new EventHandler(buttonRewind_Click);
            this.Controls.Add(buttonRewind);

            buttonStop = new ImageButton();
            buttonStop.Image = XBMControl.Properties.Resources.stop;
            buttonStop.Location = new Point(arrayName[11, 0], arrayName[11, 1]);
            buttonStop.Size = new Size(arrayName[11, 2], arrayName[11, 3]);
            //Hook up into click event
            buttonStop.Click += new EventHandler(buttonStop_Click);
            this.Controls.Add(buttonStop);

            buttonPausePlay = new ImageButton();
            buttonPausePlay.Image = XBMControl.Properties.Resources.play_pause;
            buttonPausePlay.Location = new Point(arrayName[12, 0], arrayName[12, 1]);
            buttonPausePlay.Size = new Size(arrayName[12, 2], arrayName[12, 3]);
            //Hook up into click event
            buttonPausePlay.Click += new EventHandler(buttonPausePlay_Click);
            this.Controls.Add(buttonPausePlay);

            buttonForward = new ImageButton();
            buttonForward.Image = XBMControl.Properties.Resources.forward;
            buttonForward.Location = new Point(arrayName[13, 0], arrayName[13, 1]);
            buttonForward.Size = new Size(arrayName[13, 2], arrayName[13, 3]);
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