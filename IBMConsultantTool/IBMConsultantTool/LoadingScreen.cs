using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public partial class LoadingScreen : Form
    {
        
        Panel panel;
        Label label;
        Container container;
        CUPETool owner;

        private bool isClosed = false;

        public LoadingScreen(CUPETool form)
        {
            owner = form;
            //InitializeComponent();

            owner.Deactivate +=new EventHandler(owner_Deactivate);
            owner.Activated +=new EventHandler(owner_Activated);
            owner.Move +=new EventHandler(owner_Move);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Visible = false;

            AdjustLocation();
            InitializeComponent();
        }

        private void owner_Deactivate(object sender, EventArgs e)
        {
            //if (!this.isClosed)
            //{
            //    this.Visible = false;
            //}
        }

        private void owner_Activated(object sender, EventArgs e)
        {
            //if (!this.isClosed)
            //{
            //    this.Visible = true;
            //}
        }

        private void owner_Move(object sender, EventArgs e)
        {
            AdjustLocation();
        }

        private void LoadingScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosed = true;
        }

        private void AdjustLocation()
        {
            // Adjust the position relative to main form
            int dx = (owner.Width - this.Width) / 2;
            int dy = (owner.Height - this.Height) / 2;
            Point loc = new Point(owner.Location.X, owner.Location.Y);
            loc.Offset(dx, dy);
            this.Location = loc;
        }

    }// end class   
}
