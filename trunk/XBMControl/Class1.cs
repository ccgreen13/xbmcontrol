/*
User Interfaces in C#: Windows Forms and Custom Controls
by Matthew MacDonald

Publisher: Apress
ISBN: 1590590457
*/
using System.Drawing;

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MarqueeLabelHost
{
    /// <summary>
    /// Summary description for MarqueeLabelHost.
    /// </summary>
    public class MarqueeLabelHost : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TrackBar tbInterval;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TrackBar tbAmount;
        private MarqueeLabel marqueeLabel1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MarqueeLabelHost()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.tbInterval = new System.Windows.Forms.TrackBar();
            this.Label1 = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TrackBar();
            this.marqueeLabel1 = new MarqueeLabel();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.GroupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.Label2,
                                                                                    this.tbInterval,
                                                                                    this.Label1,
                                                                                    this.tbAmount});
            this.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupBox1.Location = new System.Drawing.Point(24, 176);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(336, 132);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(12, 76);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 23);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Scroll Interval:";
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(96, 72);
            this.tbInterval.Maximum = 500;
            this.tbInterval.Minimum = 10;
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(228, 45);
            this.tbInterval.TabIndex = 5;
            this.tbInterval.TickFrequency = 10;
            this.tbInterval.Value = 100;
            this.tbInterval.Scroll += new System.EventHandler(this.tbInterval_Scroll);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 23);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Scroll Amount:";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(96, 16);
            this.tbAmount.Maximum = 20;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(228, 45);
            this.tbAmount.TabIndex = 3;
            this.tbAmount.Value = 1;
            this.tbAmount.Scroll += new System.EventHandler(this.tbAmount_Scroll);
            // 
            // marqueeLabel1
            // 
            this.marqueeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.marqueeLabel1.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.marqueeLabel1.ForeColor = System.Drawing.Color.Navy;
            this.marqueeLabel1.Location = new System.Drawing.Point(0, 12);
            this.marqueeLabel1.Name = "marqueeLabel1";
            this.marqueeLabel1.ScrollTimeInterval = 100;
            this.marqueeLabel1.Size = new System.Drawing.Size(384, 156);
            this.marqueeLabel1.TabIndex = 5;
            this.marqueeLabel1.Tag = "";
            this.marqueeLabel1.Text = "This scrolls!";
            // 
            // MarqueeLabelHost
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(380, 318);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.marqueeLabel1,
                                                                          this.GroupBox1});
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Name = "MarqueeLabelHost";
            this.Text = "MarqueeLabelHost";
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MarqueeLabelHost());
        }

        private void tbInterval_Scroll(object sender, System.EventArgs e)
        {
            marqueeLabel1.ScrollTimeInterval = tbInterval.Value;
        }

        private void tbAmount_Scroll(object sender, System.EventArgs e)
        {
            marqueeLabel1.ScrollPixelAmount = tbAmount.Value;
        }
    }
    /// <summary>
    /// Summary description for MarqueeLabel.
    /// </summary>
    public class MarqueeLabel : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.IContainer components;

        public MarqueeLabel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrScroll = new System.Windows.Forms.Timer(this.components);
            // 
            // tmrScroll
            // 
            this.tmrScroll.Tick += new System.EventHandler(this.tmrScroll_Tick);
            // 
            // MarqueeLabel
            // 
            this.Name = "MarqueeLabel";
            this.Size = new System.Drawing.Size(360, 104);
            this.Load += new System.EventHandler(this.MarqueeLabel_Load);

        }
        #endregion

        private string text;
        private int scrollAmount = 10;
        internal System.Windows.Forms.Timer tmrScroll;
        private int position = 0;

        private void MarqueeLabel_Load(object sender, System.EventArgs e)
        {
            this.ResizeRedraw = true;
            if (!this.DesignMode)
            {
                tmrScroll.Enabled = true;
            }

        }

        private void tmrScroll_Tick(object sender, System.EventArgs e)
        {
            position += scrollAmount;

            // Force a refresh.
            this.Invalidate();

        }

        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.Invalidate();
            }
        }

        public int ScrollTimeInterval
        {
            get
            {
                return tmrScroll.Interval;
            }
            set
            {
                tmrScroll.Interval = value;
            }
        }

        [DefaultValue(10)]
        public int ScrollPixelAmount
        {
            get
            {
                return scrollAmount;
            }
            set
            {
                scrollAmount = value;
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            // Do nothing.
            // To prevent flicker, we will draw both the background and the text
            // to a buffered image, and draw it to the control all at once.
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            // The following line avoids a design-time error that would
            // otherwise occur when the control is first loaded (but does not yet
            // have a defined size).
            if (e.ClipRectangle.Width == 0)
            {
                return;
            }

            base.OnPaint(e);
            if (position > this.Width)
            {
                // Reset the text to scroll back onto the control.
                position = -(int)e.Graphics.MeasureString(text, this.Font).Width;
            }

            // Create the drawing area in memory.
            // Double buffering is used to prevent flicker.
            Bitmap blt = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
            Graphics g = Graphics.FromImage(blt);

            g.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
            g.DrawString(text, this.Font, new SolidBrush(this.ForeColor), position, 0);

            // Render the finished image on the form.
            e.Graphics.DrawImageUnscaled(blt, 0, 0);

            g.Dispose();
        }


    }


}
