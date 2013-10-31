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
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
            NewClientForm form = new NewClientForm();
            form.Show();
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
           
            //this.Controls.Add(form);
            
        }
    }
}
