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
        Form owner;

        public LoadingScreen(int xPos, int yPos, Form form)
        {
            owner = form;
            InitializeComponent();

            //owner.Move +=new EventHandler(owner_Move);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            //this.StartPosition = FormStartPosition.Manual;
            this.Visible = false;
            this.Location = new Point(xPos, yPos);

            AdjustLocation();
            //InitializeComponent();
        }

        private void owner_Move(object sender, EventArgs e)
        {
            AdjustLocation();
        }

        private void AdjustLocation()
        {
            // Adjust the position relative to main form
            //int dx = (owner.Width - this.Width) / 2;
            //int dy = (owner.Height - this.Height) / 2;
            //Point loc = new Point(owner.Location.X, owner.Location.Y);
            //loc.Offset(dx, dy);
            //this.Location = loc;
        }

    }// end class   
}
