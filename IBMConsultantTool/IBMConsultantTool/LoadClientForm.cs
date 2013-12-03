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
            List<CLIENT> clients = new List<CLIENT>();

            ClientDataControl.db.GetClientNames();
            ChooseClientComboBox.Items.AddRange(ClientDataControl.db.GetClientNames());
            clientsgridView.DataSource = clients;
            clientsgridView.Columns[0].HeaderText = "Client Name";
            this.Focus();
        }

        private void LoadClientButton_Click(object sender, EventArgs e)
        {
            if (ClientDataControl.LoadClient(ChooseClientComboBox.Text))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

            else
            {
                MessageBox.Show("Failed to load client: " + ChooseClientComboBox.Text + " does not exist", "Error");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}