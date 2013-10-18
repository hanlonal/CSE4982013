using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public partial class ChooseITCAPClient : Form
    {
        ITCapTool itcapForm;
        public ChooseITCAPClient(ITCapTool parentForm)
        {
            InitializeComponent();

            itcapForm = parentForm;
            ChooseClientComboBox.Items.AddRange(itcapForm.db.GetClientNames());
            this.Focus();
        }

        private void OpenITCAPButton_Click(object sender, EventArgs e)
        {
            if (itcapForm.db.BuildITCAPForm(itcapForm, ChooseClientComboBox.Text.Trim()))
            {
                this.Close();
            }
        }

        private void NewITCAPButton_Click(object sender, EventArgs e)
        {
            if (itcapForm.db.NewITCAPForm(itcapForm, ChooseClientComboBox.Text.Trim()))
            {
                this.Close();
            }
        }
    }
}