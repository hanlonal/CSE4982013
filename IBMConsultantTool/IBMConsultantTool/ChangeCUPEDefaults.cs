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
    public partial class ChangeCUPEDefaults : Form
    {
        public ChangeCUPEDefaults()
        {
            InitializeComponent();

            List<CupeQuestionData> cupeQuestionList = ClientDataControl.GetCupeQuestionData();
            DataGridViewRow row;
            foreach (CupeQuestionData data in cupeQuestionList)
            {
                row = CUPEQuestionDataGridView.Rows[0].Clone() as DataGridViewRow;
                row.Cells[0].Value = data.InDefault20;
                row.Cells[1].Value = data.InDefault15;
                row.Cells[2].Value = data.InDefault10;
                row.Cells[0].ReadOnly = row.Cells[1].ReadOnly = row.Cells[2].ReadOnly = false;
                row.Cells[3].Value = data.StringData.OriginalQuestionText;
                row.Cells[4].Value = data.StringData.ChoiceA;
                row.Cells[5].Value = data.StringData.ChoiceB;
                row.Cells[6].Value = data.StringData.ChoiceC;
                row.Cells[7].Value = data.StringData.ChoiceD;
                row.Cells[3].ReadOnly = row.Cells[4].ReadOnly = row.Cells[5].ReadOnly = 
                    row.Cells[6].ReadOnly = row.Cells[7].ReadOnly = true;
                CUPEQuestionDataGridView.Rows.Add(row);
            }
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in CUPEQuestionDataGridView.Rows)
            {
                if (!ClientDataControl.db.UpdateCupeQuestion(row.Cells[3].Value as string, (bool)row.Cells[0].Value, (bool)row.Cells[1].Value, (bool)row.Cells[2].Value))
                {
                    MessageBox.Show("CUPEQuestion \"" + row.Cells[3].Value as string + "\" Not Found", "Error");
                    return;
                }
            }

            if (ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Changes Saved Successfully", "Success");
            }
        }
    }
}
