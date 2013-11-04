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
        StartPage owner;
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
                owner.Refresh();
                this.Close();
            }

            else
            {
                MessageBox.Show("Failed to load client: " + ChooseClientComboBox.Text + " does not exist", "Error");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public StartPage Owner
        {
            get { return owner; }
            set { owner = value; }
        }
    }
}