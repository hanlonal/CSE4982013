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
                row.Cells[1].Value = data.InDefault10;
                row.Cells[2].Value = data.StringData.OriginalQuestionText;
                row.Cells[3].Value = data.StringData.ChoiceA;
                row.Cells[4].Value = data.StringData.ChoiceB;
                row.Cells[5].Value = data.StringData.ChoiceC;
                row.Cells[6].Value = data.StringData.ChoiceD;
                CUPEQuestionDataGridView.Rows.Add(row);
            }

            CUPEQuestionDataGridView.AllowUserToAddRows = false;
            CUPEQuestionDataGridView.AllowUserToDeleteRows = false;
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            int inTwenty = 0;
            int inTen = 0;
            foreach (DataGridViewRow row in CUPEQuestionDataGridView.Rows)
            {
                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "")
                {
                    row.Cells[0].Value = row.Cells[0].Value == null ? false : row.Cells[0].Value;
                    row.Cells[1].Value = row.Cells[1].Value == null ? false : row.Cells[1].Value;
                    inTwenty += row.Cells[0].Value != null && (bool)row.Cells[0].Value ? 1 : 0;
                    inTen += row.Cells[1].Value != null && (bool)row.Cells[1].Value ? 1 : 0;
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
                if (row.Cells[2].Value != null)
                {
                    if (!ClientDataControl.db.UpdateCupeQuestion(row.Cells[2].Value as string, (bool)row.Cells[0].Value, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("CUPEQuestion \"" + row.Cells[2].Value as string + "\" Not Found", "Error");
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
