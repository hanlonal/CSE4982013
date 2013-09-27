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
    public partial class DataEntryForm : Form
    {
        //private int categoryCount = 2;
        BOMRedesign mainForm;
        int test;

        public DataEntryForm(BOMRedesign chart)
        {
            mainForm = chart;
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            int rowCount = 0;
            //int columnCount = 0;
            for (int i = 0; i < mainForm.CategoryCount ; i++)
            {
                dataGrid.Rows.Add(1);
                dataGrid.Rows[rowCount].Cells[0].Value = mainForm.Categories[i].Name;
                dataGrid.Rows[rowCount].ReadOnly = true;
                rowCount++;
                

                for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                {

                    dataGrid.Rows.Add(1);
                   dataGrid.Rows[rowCount].Cells[1].Value = mainForm.Categories[i].Objectives[j].Name;
                   dataGrid.Rows[rowCount].ReadOnly = true;
                   rowCount++;

                   for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                   {
                       dataGrid.Rows.Add(1);
                       dataGrid.Rows[rowCount].Cells[2].Value = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                       dataGrid.Rows[rowCount].Cells[3].Value = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                       dataGrid.Rows[rowCount].Cells[4].Value = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                       dataGrid.Rows[rowCount].Cells[5].Value = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;
                       //test = (int)dataGrid.Rows[rowCount].Cells[5].Value;
                       rowCount++;
                       
                   }
                }
               

            }
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(test.ToString());
            int rowCount = 0;
            for (int i = 0; i < mainForm.CategoryCount; i++)
            {
                rowCount++;
                for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                {
                    rowCount++;
                    for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                    {
                        mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[3].Value);
                        mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[4].Value);
                        mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[5].Value);

                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality = (float)(dataGrid.Rows[rowCount].Cells[3].Value);
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation = (float)dataGrid.Rows[rowCount].Cells[4].Value;
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness = (float)dataGrid.Rows[rowCount].Cells[5].Value;
                        rowCount++;
                    }
                }
            }
        }

       


    }
}
