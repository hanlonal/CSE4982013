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
        AnalyticsForm owner;
        Panel panel;
        Label label;
        Container container;

        public LoadingScreen()
        {
            InitializeComponent();
        }

    }

   // public LoadingScreen(AnalyticsForm form):this() 
   // {
      // Store the reference to parent form
     // this.owner = form;

      // Attach to parent form events
     // owner.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
      //owner.Activated += new System.EventHandler(this.MainForm_Activated);
      ////owner.Move += new System.EventHandler(this.MainForm_Move);

      // Adjust appearance
     // this.ShowInTaskbar = false; // do not show form in task bar
      //this.TopMost = true; // show splash form on top of main form
     // this.StartPosition = FormStartPosition.Manual;
     // this.Visible = false;

      // Adjust location
      //AdjustLocation();
   // }
    
}
