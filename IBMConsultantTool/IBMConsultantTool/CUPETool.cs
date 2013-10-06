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
        public CUPETool()
        {
            InitializeComponent();
        }

        private void CUPETool_Load(object sender, EventArgs e)
        {

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
            
            personCount++;
            col.HeaderText = "Person " + personCount;
            col.Name = "Person" + personCount; ;
            
            col.Width = 50;
         
            questionGrid.Columns.Insert(personCount, col);
            
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            CreatePerson();
        }
    }


}
