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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ITCAPQuestionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           // if(!((sender as DataGridViewCheckBoxCell).Value as bool))
        }
    }
}
