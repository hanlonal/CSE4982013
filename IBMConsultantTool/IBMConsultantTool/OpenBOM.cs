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
    public partial class OpenBOM : Form
    {
        MainForm mainForm;
        public OpenBOM(Form parentForm)
        {
            InitializeComponent();

            mainForm = parentForm as MainForm;
        }

        private void OpenBOM_Load(object sender, EventArgs e)
        {
            ClientComboBox.Items.AddRange((from cnt in mainForm.dbo.CLIENT
                                           select cnt.NAME.TrimEnd()).ToArray());
        }

        private void OpenBOMButton_Click(object sender, EventArgs e)
        {
            CLIENT selectedClient = (from cnt in mainForm.dbo.CLIENT
                                     where cnt.NAME.TrimEnd() == ClientComboBox.Text
                                     select cnt).Single();

            mainForm.currentClient = selectedClient;

            mainForm.BOMTable.Rows.Clear();

            List<BOM> bomList = selectedClient.BOM.ToList();
            /*List<BOM> bomList = (from bom in mainForm.dbo.BOM
                                where bom.CLIENT.CLIENTID == selectedClient.CLIENTID
                                select bom).ToList();*/

            DataGridViewRow row;
            foreach (BOM bom in bomList)
            {
                row = (DataGridViewRow)mainForm.BOMTable.Rows[0].Clone();
                row.Cells[0].Value = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                row.Cells[1].Value = bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                row.Cells[2].Value = bom.INITIATIVE.NAME.TrimEnd();
                row.Cells[3].Value = bom.EFFECTIVENESS;
                row.Cells[4].Value = bom.CRITICALITY;
                row.Cells[5].Value = bom.DIFFERENTIAL;
                mainForm.BOMTable.Rows.Add(row);
            }

            this.Close();
        }
    }
}
