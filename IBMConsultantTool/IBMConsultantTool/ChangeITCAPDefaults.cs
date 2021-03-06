﻿using System;
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

            string[] domainList = ClientDataControl.db.GetDomainNamesAndDefault();
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

                domRow.DefaultCellStyle.BackColor = Color.LightBlue;
                domRow.ReadOnly = true;
                ITCAPQuestionDataGridView.Rows.Add(domRow);
                capabilityList = ClientDataControl.db.GetCapabilityNamesAndDefault(domainName).ToList();
                foreach(string capability in capabilityList)
                {
                    capabilityName = capability.Substring(0, capability.Length - 1);
                    DataGridViewRow capRow = (DataGridViewRow)ITCAPQuestionDataGridView.Rows[0].Clone();
                    capRow.Cells[0].Value = capabilityName;
                    (capRow.Cells[1] as DataGridViewCheckBoxCell).Value = capability.Last() == 'Y';
                    capRow.DefaultCellStyle.BackColor = Color.LightSlateGray;
                    capRow.ReadOnly = true;
                    ITCAPQuestionDataGridView.Rows.Add(capRow);
                    questionList = ClientDataControl.db.GetITCAPQuestionNamesAndDefault(capabilityName, domainName).ToList();
                    foreach (string question in questionList)
                    {
                        itcapQuestionName = question.Substring(0, question.Length - 1);
                        DataGridViewRow itcqRow = (DataGridViewRow)ITCAPQuestionDataGridView.Rows[0].Clone();
                        itcqRow.Cells[0].Value = itcapQuestionName;
                        (itcqRow.Cells[1] as DataGridViewCheckBoxCell).Value = question.Last() == 'Y';
                        itcqRow.DefaultCellStyle.BackColor = Color.White;
                        itcqRow.Cells[0].ReadOnly = true;
                        itcqRow.Cells[1].ReadOnly = false;
                        ITCAPQuestionDataGridView.Rows.Add(itcqRow);
                    }  
                } 
            }

            ITCAPQuestionDataGridView.AllowUserToAddRows = false;
            ITCAPQuestionDataGridView.AllowUserToDeleteRows = false;
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            string domName;
            string capName;
            string itcqName;

            foreach (DataGridViewRow row in ITCAPQuestionDataGridView.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.White)
                {
                    itcqName = (string)row.Cells[0].Value;
                    if (!ClientDataControl.db.ChangeITCAPQuestionDefault(itcqName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("ITCAPQuestion \"" + itcqName + "\" Not Found", "Error");
                        return;
                    }
                }

                else if (row.DefaultCellStyle.BackColor == Color.LightSlateGray)
                {
                    capName = (string)row.Cells[0].Value;
                    if (!ClientDataControl.db.ChangeCapabilityDefault(capName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("Capability \"" + capName + "\" Not Found", "Error");
                        return;
                    }
                }

                else if (row.DefaultCellStyle.BackColor == Color.LightBlue)
                {
                    domName = (string)row.Cells[0].Value;
                    if (!ClientDataControl.db.ChangeDomainDefault(domName, (bool)row.Cells[1].Value))
                    {
                        MessageBox.Show("Domain \"" + domName + "\" Not Found", "Error");
                        return;
                    }
                }
            }

            if (!ClientDataControl.db.SaveChanges())
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
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewCheckBoxCell senderCell = ITCAPQuestionDataGridView.Rows[e.RowIndex].Cells[1] as DataGridViewCheckBoxCell;
            bool capabilityIsDefault = false; //becomes true if we find another attribute that is default
            bool hitCapability = false;
            bool domainIsDefault = false; //becomes true if we find another capability that is default
            DataGridViewRow row;
            //MessageBox.Show(e.RowIndex.ToString() + " " + ((bool)senderCell.EditedFormattedValue).ToString());
            if (((bool)senderCell.EditedFormattedValue) == true)
            {
                if (senderCell.OwningRow.DefaultCellStyle.BackColor == Color.White)
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
                        if (!hitCapability && row.DefaultCellStyle.BackColor == Color.LightSlateGray)
                        {
                            row.Cells[1].Value = true;
                            hitCapability = true;
                        }
                        else if (row.DefaultCellStyle.BackColor == Color.LightBlue)
                        {
                            row.Cells[1].Value = true;
                            break;
                        }
                    }
                }
            }

            else
            {
                if (senderCell.OwningRow.DefaultCellStyle.BackColor == Color.White)
                {
                    for (int i = senderCell.OwningRow.Index+1; ; i++)
                    {
                        //MessageBox.Show(i.ToString());
                        if (i >= ITCAPQuestionDataGridView.Rows.Count)
                        {
                            break;
                        }
                        row = ITCAPQuestionDataGridView.Rows[i];
                        if (!hitCapability && row.DefaultCellStyle.BackColor == Color.White && (bool)(row.Cells[1].EditedFormattedValue))
                        {
                            capabilityIsDefault = true;
                        }
                        if (row.DefaultCellStyle.BackColor == Color.LightSlateGray && (bool)(row.Cells[1].EditedFormattedValue))
                        {
                            hitCapability = true;
                            domainIsDefault = true;
                        }
                        else if (row.DefaultCellStyle.BackColor == Color.LightBlue)
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
                        if (row.DefaultCellStyle.BackColor == Color.White && (bool)(row.Cells[1].Value))
                        {
                            capabilityIsDefault = true;
                        }
                        if (row.DefaultCellStyle.BackColor == Color.LightSlateGray)
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
                        else if (row.DefaultCellStyle.BackColor == Color.LightBlue)
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
