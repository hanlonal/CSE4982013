using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public partial class CheckoutClients : Form
    {
        public CheckoutClients()
        {
            InitializeComponent();

            List<string> checkedOutClientFileNames = Directory.EnumerateFiles(@"Resources\Clients").ToList();

            List<string> checkedOutClientNames = (from ent in checkedOutClientFileNames
                                                  select Path.GetFileNameWithoutExtension(ent)).ToList();

            string[] clientNames = ClientDataControl.db.GetClientNames();
            DataGridViewRow row;
            foreach (string clientName in clientNames)
            {
                row = ClientDataGridView.Rows[0].Clone() as DataGridViewRow;
                row.Cells[0].Value = checkedOutClientNames.Contains(clientName);
                row.Cells[1].Value = clientName;
                ClientDataGridView.Rows.Add(row);
            }
            ClientDataGridView.AllowUserToAddRows = false;
            ClientDataGridView.AllowUserToDeleteRows = false;
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@"Resources\Clients"))
            {
                Directory.Delete(@"Resources\Clients", true);
            }

            Directory.CreateDirectory(@"Resources\Clients");
            FileStream fileStream;
            foreach (DataGridViewRow clientRow in ClientDataGridView.Rows)
            {
                if ((bool)clientRow.Cells[0].Value)
                {
                    fileStream = File.Create(@"Resources\Clients\" + clientRow.Cells[1].Value + ".xml");
                    fileStream.Close();
                }
            }

            if (ClientDataControl.db.SaveChanges())
            {
                this.Close();
            }
        }
    }
}
