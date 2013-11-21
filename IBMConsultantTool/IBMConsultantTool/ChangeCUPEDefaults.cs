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
            int inTwenty = 0;
            int inTen = 0;
            foreach (DataGridViewRow row in CUPEQuestionDataGridView.Rows)
            {
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "")
                {
                    row.Cells[0].Value = row.Cells[0].Value == null ? false : row.Cells[0].Value;
                    row.Cells[2].Value = row.Cells[2].Value == null ? false : row.Cells[2].Value;
                    inTwenty += row.Cells[0].Value != null && (bool)row.Cells[0].Value ? 1 : 0;
                    inTen += row.Cells[2].Value != null && (bool)row.Cells[2].Value ? 1 : 0;
                }
            }
            if (inTwenty != 20)
            {
                MessageBox.Show("Must choose 20 questions for default 20-question survey", "Error");
                return;
            }
            if (inTen != 10)
            {
                MessageBox.Show("Must choose 10 questions for default 10-question survey", "Error");
                return;
            }
            foreach (DataGridViewRow row in CUPEQuestionDataGridView.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    if (!ClientDataControl.db.UpdateCupeQuestion(row.Cells[3].Value as string, (bool)row.Cells[0].Value, (bool)row.Cells[2].Value))
                    {
                        MessageBox.Show("CUPEQuestion \"" + row.Cells[3].Value as string + "\" Not Found", "Error");
                        return;
                    }
                }
            }

            if (ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Changes Saved Successfully", "Success");
            }
        }
    }
}
