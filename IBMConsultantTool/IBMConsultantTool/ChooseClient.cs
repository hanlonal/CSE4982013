
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
    public partial class ChooseClient : Form
    {
        BOMTool bomForm;
        public ChooseClient(BOMTool parentForm)
        {
            InitializeComponent();

            bomForm = parentForm;
            if (bomForm.isOnline)
            {
                ChooseClientComboBox.Items.AddRange(bomForm.db.GetClientNames());
            }

            else
            {
                ChooseClientComboBox.Items.AddRange(bomForm.fm.GetClientNames());
            }
            this.Focus();
        }

        private void OpenBOMButton_Click(object sender, EventArgs e)
        {
            if (bomForm.isOnline)
            {
                if(bomForm.db.BuildBOMForm(bomForm, ChooseClientComboBox.Text.Trim()))
                {
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Client \"" + ChooseClientComboBox.Text + "\" not found", "Error");
                }
            }

            else
            {
                if(bomForm.fm.BuildBOMForm(bomForm, ChooseClientComboBox.Text.Trim()))
                {
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Client \"" + ChooseClientComboBox.Text + "\" not found", "Error");
                }
            }
        }

        private void NewBOMButton_Click(object sender, EventArgs e)
        {
            if (bomForm.isOnline)
            {
                CLIENT client;
                string clientName = NewClientTextBox.Text.Trim();
                if (!bomForm.db.GetClient(clientName, out client))
                {
                    client = new CLIENT();
                    client.NAME = NewClientTextBox.Text;
                    bomForm.db.AddClient(client);

                    if (!bomForm.db.AddGroup("Business", client) || !bomForm.db.AddGroup("IT", client))
                    {
                        MessageBox.Show("Cannot create groups for client", "Error");
                        return;
                    }

                    if (!bomForm.db.SaveChanges())
                    {
                        MessageBox.Show("Could not create new Client", "Error");
                        bomForm.db = new DBManager();
                        return;
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Client \"" + clientName + "\" already exists", "Error");
                }
            }

            else
            {
                XElement client;
                string clientName = NewClientTextBox.Text.Trim();
                if (!bomForm.fm.GetClient(clientName, out client))
                {
                    client = new XElement("CLIENT");
                    client.Add(new XElement("NAME", NewClientTextBox.Text));
                    bomForm.fm.AddClient(client);

                    if (!bomForm.fm.AddGroup("Business", client) || !bomForm.fm.AddGroup("IT", client))
                    {
                        MessageBox.Show("Cannot create groups for client", "Error");
                        return;
                    }

                    if (!bomForm.fm.SaveChanges())
                    {
                        MessageBox.Show("Could not create new Client", "Error");
                        bomForm.fm = new FileManager();
                        return;
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Client \"" + clientName + "\" already exists", "Error");
                }
            }
        }
    }
}
/*
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
    public partial class ChooseClient : Form
    {


        public void OpenBOMButton_Click(object sender, EventArgs e)
        {

        }

        public void ChooseClient_Load(object sender, EventArgs e)
        {

        }
        public void NewBOMButton_Click(object sender, EventArgs e)
        {

        }

    }
   
}
*/