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
    public partial class CUPETool : Form
    {

        int personCount = 0;

        int totalAIndex = 1;
        int totalBIndex = 2;
        int totalCIndex = 3;
        int totalDIndex = 4;
        int averageIndex = 6;
        int totalAnswers = 5;

        public CUPETool()
        {
            InitializeComponent();
            
        }

        private void CUPETool_Load(object sender, EventArgs e)
        {
            //questionGrid.CellValueChanged +=new DataGridViewCellEventHandler(questionGrid_CellValueChanged);
            CreatePerson();
            for (int i = 1; i < 21; i++)
            {
                DataGridViewRow row = (DataGridViewRow)questionGrid.Rows[0].Clone();
                row.Cells[0].Value = "Question " + i;
                row.Visible = true;
               
                questionGrid.Rows.Add(row);
            }
        }



        public void CreatePerson()
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            Person person = new Person();
            personCount++;
            col.HeaderText = "Person " + personCount;
            col.Name = "Person" + personCount; ;
            
            col.Width = 60;
         
            questionGrid.Columns.Insert(personCount, col);
            
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            CreatePerson();
        }



        private void questionGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


            ChangeTotals(e.RowIndex);
        }

        public void ChangeTotals(int index)
        {
            int totalA=0;
            int totalB=0;
            int totalC=0;
            int totalD=0;
            int count = 0;
            int total = 0;
            foreach (DataGridViewCell cell in questionGrid.Rows[index].Cells)
            {
                if ((string)(cell.Value) == "a" || (string)(cell.Value) == "A")
                {
                    totalA++;
                    count++;
                    total +=1;
                }
                if ((string)(cell.Value) == "b" || (string)(cell.Value) == "B")
                {
                    totalB++;
                    count++;
                    total+=2;
                }
                if ((string)cell.Value == "c" || (string)cell.Value == "C")
                {
                    totalC++;
                    count++;
                    total += 3;
                }
                if ((string)cell.Value == "d" || (string)cell.Value == "D")
                {
                    totalD++;
                    count++;
                    total += 4;

                }
            }
            questionGrid.Rows[index].Cells[totalAIndex + personCount].Value = totalA.ToString();
            questionGrid.Rows[index].Cells[totalBIndex + personCount].Value = totalB.ToString();
            questionGrid.Rows[index].Cells[totalCIndex + personCount].Value = totalC.ToString();
            questionGrid.Rows[index].Cells[totalDIndex + personCount].Value = totalD.ToString();
            questionGrid.Rows[index].Cells[totalAnswers + personCount].Value = count.ToString();
            questionGrid.Rows[index].Cells[averageIndex + personCount].Value = (total / personCount).ToString();
            
        }

    }


}
