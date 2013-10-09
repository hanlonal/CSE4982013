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
        int test;

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
                   dataGrid.Rows[rowCount].Cells[1].Value = objective.Name;
                   dataGrid.Rows[rowCount].ReadOnly = true;
                   rowCount++;

                   //for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                    foreach(NewInitiative initiative in objective.Initiatives)
                   {
                       dataGrid.Rows.Add(1);
                       dataGrid.Rows[rowCount].Cells[2].Value = initiative.Name;
                       dataGrid.Rows[rowCount].Cells[3].Value = initiative.Effectiveness;
                       dataGrid.Rows[rowCount].Cells[4].Value = initiative.Criticality;
                       dataGrid.Rows[rowCount].Cells[5].Value = initiative.Differentiation;
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
            //for (int i = 0; i < mainForm.CategoryCount; i++)
            foreach(NewCategory category in mainForm.Categories)
            {
                rowCount++;
                //for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                foreach(NewObjective objective in category.Objectives)
                {
                    rowCount++;
                    //for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                    foreach(NewInitiative initiative in objective.Initiatives)
                    {
                        initiative.Effectiveness = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[3].Value);
                        initiative.Criticality = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[4].Value);
                        initiative.Differentiation = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[5].Value);

                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[3].Value);
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[4].Value);
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness = (float)Convert.ToDouble(dataGrid.Rows[rowCount].Cells[5].Value);

                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality = (float)(dataGrid.Rows[rowCount].Cells[3].Value);
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation = (float)dataGrid.Rows[rowCount].Cells[4].Value;
                        //mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness = (float)dataGrid.Rows[rowCount].Cells[5].Value;

                        if (mainForm.isOnline)
                        {
                            if (!mainForm.db.UpdateBOM(mainForm.dbclient, initiative))
                            {
                                MessageBox.Show("BOM \"" + initiative.Name + "\" could not be saved to database", "Error");
                                return;
                            }
                        }

                        else
                        {
                            if (!mainForm.fm.UpdateBOM(mainForm.flclient, initiative))
                            {
                                MessageBox.Show("BOM \"" + initiative.Name + "\" could not be saved to File", "Error");
                                return;
                            }
                        }

                        rowCount++;
                    }
                }
            }
            if(mainForm.isOnline)
            {
                if (!mainForm.db.SaveChanges())
                {
                    MessageBox.Show("Could not save changes to database", "Error");
                    return;
                } 

                else
                {
                    MessageBox.Show("Changes saved successfully", "Success");
                }
            }

            else
            {
                if (!mainForm.fm.SaveChanges())
                {
                    MessageBox.Show("Could not save changes to File", "Error");
                    return;
                } 

                else
                {
                    MessageBox.Show("Changes saved successfully", "Success");
                }
            }
        }
    }
}
