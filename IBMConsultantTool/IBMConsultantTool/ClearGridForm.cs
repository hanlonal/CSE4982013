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
    public partial class ClearGridForm : Form
    {
        AnalyticsForm owner;

        public ClearGridForm(AnalyticsForm form)
        {
            InitializeComponent();
            owner = form;
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            owner.ClearGrid();
            this.Close();
        }


    }
}
