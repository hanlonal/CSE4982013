using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public partial class LoadClientForm : Form
    {
        public LoadClientForm()
        {
            InitializeComponent();

            ChooseClientComboBox.Items.AddRange(ClientDataControl.db.GetClientNames());
            this.Focus();
        }

        private void LoadClientButton_Click(object sender, EventArgs e)
        {
            if (ClientDataControl.LoadClient(ChooseClientComboBox.Text))
            {
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}