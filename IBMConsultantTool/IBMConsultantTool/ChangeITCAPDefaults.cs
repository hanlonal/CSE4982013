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
    public partial class ChangeITCAPDefaults : Form
    {
        public ITCapTool itcapForm;
        public ChangeITCAPDefaults(ITCapTool parentForm)
        {
            InitializeComponent();

            itcapForm = parentForm;

            string[] domainList = itcapForm.db.GetDomainNamesAndDefault();
            List<string> capabilityList = new List<string>();
            List<string> questionList = new List<string>();
            string domainName;
            string capabilityName;
            string itcapQuestionName;
            foreach (string domain in domainList)
            {
                domainName = domain.Substring(0, domain.Length - 1);
                DataGridViewRow domRow = (DataGridViewRow)ITCAPQuestionDataGridView.Rows[0].Clone();
                domRow.Cells[0].Value = domainName;
                (domRow.Cells[1] as DataGridViewCheckBoxCell).Value = domain.Last() == 'Y';

                domRow.DefaultCellStyle.BackColor = Color.Orange;
                domRow.ReadOnly = true;
                ITCAPQuestionDataGridView.Rows.Add(domRow);
                capabilityList = itcapForm.db.GetCapabilityNamesAndDefault(domainName).ToList();
                foreach(string capability in capabilityList)
                {
                    capabilityName = capability.Substring(0, capability.Length - 1);
                    DataGridViewRow capRow = (DataGridViewRow)ITCAPQuestionDataGridView.Rows[0].Clone();
                    capRow.Cells[0].Value = capabilityName;
                    (capRow.Cells[1] as DataGridViewCheckBoxCell).Value = capability.Last() == 'Y';
                    capRow.DefaultCellStyle.BackColor = Color.Yellow;
                    capRow.ReadOnly = true;
                    ITCAPQuestionDataGridView.Rows.Add(capRow);
                    questionList = itcapForm.db.GetITCAPQuestionNamesAndDefault(capabilityName, domainName).ToList();
                    foreach (string question in questionList)
                    {
                        itcapQuestionName = question.Substring(0, question.Length - 1);
                        DataGridViewRow itcqRow = (DataGridViewRow)ITCAPQuestionDataGridView.Rows[0].Clone();
                        itcqRow.Cells[0].Value = itcapQuestionName;
                        (itcqRow.Cells[1] as DataGridViewCheckBoxCell).Value = question.Last() == 'Y';
                        itcqRow.DefaultCellStyle.BackColor = Color.LawnGreen;
                        itcqRow.Cells[0].ReadOnly = true;
                        itcqRow.Cells[1].ReadOnly = false;
                        ITCAPQuestionDataGridView.Rows.Add(itcqRow);
                    }  
                } 
            }
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            string domName;
            string capName;
            string itcqName;

            foreach (DataGridViewRow row in ITCAPQuestionDataGridView.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.LawnGreen)
                {
                    itcqName = (string)row.Cells[0].Value;
                    if (!itcapForm.db.ChangeITCAPQuestionDefault(itcqName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("ITCAPQuestion \"" + itcqName + "\" Not Found", "Error");
                        return;
                    }
                }

                else if (row.DefaultCellStyle.BackColor == Color.Yellow)
                {
                    capName = (string)row.Cells[0].Value;
                    if (!itcapForm.db.ChangeCapabilityDefault(capName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("Capability \"" + capName + "\" Not Found", "Error");
                        return;
                    }
                }

                else if (row.DefaultCellStyle.BackColor == Color.Orange)
                {
                    domName = (string)row.Cells[0].Value;
                    if (!itcapForm.db.ChangeDomainDefault(domName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("Domain \"" + domName + "\" Not Found", "Error");
                        return;
                    }
                }
            }

            if (!itcapForm.db.SaveChanges())
            {
                MessageBox.Show("Failed to Save Changes", "Error");
            }

            else
            {
                MessageBox.Show("Changes Saved Successfully", "Success");
            }
        }

        private void ITCAPQuestionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewCheckBoxCell senderCell = ITCAPQuestionDataGridView.Rows[e.RowIndex].Cells[1] as DataGridViewCheckBoxCell;
            bool capabilityIsDefault = false; //becomes true if we find another attribute that is default
            bool hitCapability = false;
            bool domainIsDefault = false; //becomes true if we find another capability that is default
            DataGridViewRow row;
            //MessageBox.Show(e.RowIndex.ToString() + " " + ((bool)senderCell.EditedFormattedValue).ToString());
            if (((bool)senderCell.EditedFormattedValue) == true)
            {
                if (senderCell.OwningRow.DefaultCellStyle.BackColor == Color.LawnGreen)
                {
                    for (int i = senderCell.OwningRow.Index; ; i--)
                    {
                        //MessageBox.Show(i.ToString());
                        if (i < 0)
                        {
                            MessageBox.Show("Error Encountered: Did not find parent domain");
                            break;
                        }
                        row = ITCAPQuestionDataGridView.Rows[i];
                        if (!hitCapability && row.DefaultCellStyle.BackColor == Color.Yellow)
                        {
                            row.Cells[1].Value = true;
                            hitCapability = true;
                        }
                        else if (row.DefaultCellStyle.BackColor == Color.Orange)
                        {
                            row.Cells[1].Value = true;
                            break;
                        }
                    }
                }
            }

            else
            {
                if (senderCell.OwningRow.DefaultCellStyle.BackColor == Color.LawnGreen)
                {
                    for (int i = senderCell.OwningRow.Index+1; ; i++)
                    {
                        //MessageBox.Show(i.ToString());
                        if (i > ITCAPQuestionDataGridView.Rows.Count)
                        {
                            break;
                        }
                        row = ITCAPQuestionDataGridView.Rows[i];
                        if (!hitCapability && row.DefaultCellStyle.BackColor == Color.LawnGreen && (bool)(row.Cells[1].EditedFormattedValue))
                        {
                            capabilityIsDefault = true;
                        }
                        if (row.DefaultCellStyle.BackColor == Color.Yellow && (bool)(row.Cells[1].EditedFormattedValue))
                        {
                            hitCapability = true;
                            domainIsDefault = true;
                        }
                        else if (row.DefaultCellStyle.BackColor == Color.Orange)
                        {
                            break;
                        }
                    }

                    hitCapability = false;
                    for (int i = senderCell.OwningRow.Index-1; ; i--)
                    {
                        if (i < 0)
                        {
                            MessageBox.Show("Error Encountered: Did not find parent domain");
                            break;
                        }
                        //MessageBox.Show(i.ToString());
                        row = ITCAPQuestionDataGridView.Rows[i];
                        if (row.DefaultCellStyle.BackColor == Color.LawnGreen && (bool)(row.Cells[1].Value))
                        {
                            capabilityIsDefault = true;
                        }
                        if (row.DefaultCellStyle.BackColor == Color.Yellow)
                        {
                            if (!hitCapability)
                            {
                                if (capabilityIsDefault)
                                {
                                    row.Cells[1].Value = true;
                                    domainIsDefault = true;
                                }

                                else
                                {
                                    row.Cells[1].Value = false;
                                }
                                hitCapability = true;
                            }
                        }
                        else if (row.DefaultCellStyle.BackColor == Color.Orange)
                        {
                            row.Cells[1].Value = domainIsDefault;
                            break;
                        }
                    }
                }
            }
        }
    }
}
