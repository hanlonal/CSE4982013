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
        BOMTool mainForm;

        public DataEntryForm(BOMTool chart)
        {
            mainForm = chart;
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            int rowCount = 0;
            //int columnCount = 0;
            //for (int i = 0; i < mainForm.CategoryCount ; i++)
            foreach(NewCategory category in mainForm.Categories)
            {
                dataGrid.Rows.Add(1);
                dataGrid.Rows[rowCount].Cells[0].Value = category.Name;
                dataGrid.Rows[rowCount].ReadOnly = true;
                rowCount++;
                

                //for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                foreach(NewObjective objective in category.Objectives)
                {

                   dataGrid.Rows.Add(1);
                   dataGrid.Rows[rowCount].Cells[1].Value = objective.ObjName;
                   dataGrid.Rows[rowCount].ReadOnly = true;
                   rowCount++;

                   //for (int k = 0; k < mainForm.Categories[i].Objectives[j].ImperativesCount; k++)
                    foreach(NewImperative imperative in objective.Imperatives)
                   {
                       dataGrid.Rows.Add(1);
                       dataGrid.Rows[rowCount].Cells[2].Value = imperative.Name;
                       dataGrid.Rows[rowCount].Cells[3].Value = imperative.Effectiveness;
                       dataGrid.Rows[rowCount].Cells[4].Value = imperative.Criticality;
                       dataGrid.Rows[rowCount].Cells[5].Value = imperative.Differentiation;
                       //test = (int)dataGrid.Rows[rowCount].Cells[5].Value;
                       rowCount++;
                       
                   }
                }
               

            }
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            //for (int i = 0; i < mainForm.CategoryCount; i++)
            foreach(NewCategory category in mainForm.Categories)
            {
                rowCount++;
                //for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                foreach(NewObjective objective in category.Objectives)
                {
                    rowCount++;
                    //for (int k = 0; k < mainForm.Categories[i].Objectives[j].ImperativesCount; k++)
                    foreach(NewImperative imperative in objective.Imperatives)
                    {
                        imperative.Effectiveness = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[3].Value);
                        imperative.Criticality = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[4].Value);
                        imperative.Differentiation = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[5].Value);

                        if (!ClientDataControl.db.UpdateBOM(ClientDataControl.Client.EntityObject, imperative))
                        {
                            MessageBox.Show("BOM \"" + imperative.Name + "\" could not be saved to database", "Error");
                            return;
                        }

                        rowCount++;
                    }
                }
            }
            if (!ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Could not save changes to database", "Error");
                return;
            } 

            else
            {
                MessageBox.Show("Changes saved successfully", "Success");
            }
        }
    }
}
