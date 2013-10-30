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
    public partial class NewBOM : Form
    {
        public MainForm mainForm;

        public NewBOM(Form parentForm)
        {
            InitializeComponent();

            mainForm = parentForm as MainForm;
        }

        private void NewBOMCreateBOMButton_Click(object sender, EventArgs e)
        {
            List<int> idList;

            CLIENT newClient = new CLIENT();
            idList = (from cnt in mainForm.dbo.CLIENT
                     select cnt.CLIENTID).ToList();
            newClient.CLIENTID = mainForm.GetUniqueID(idList);
            newClient.NAME = NewClientTextBox.Text;

            mainForm.dbo.AddToCLIENT(newClient);
            mainForm.dbo.SaveChanges();

            mainForm.currentClient = newClient;
            mainForm.BOMTable.Rows.Clear();
            Close();
        }
    }
}
