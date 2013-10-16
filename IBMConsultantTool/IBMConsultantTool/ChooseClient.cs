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
    public partial class ChooseClient : Form
    {
        BOMTool bomForm;
        public ChooseClient(BOMTool parentForm)
        {
            InitializeComponent();

            bomForm = parentForm;
            ChooseClientComboBox.Items.AddRange(bomForm.db.GetClientNames());
            this.Focus();
        }

        private void OpenBOMButton_Click(object sender, EventArgs e)
        {
            if (bomForm.db.BuildBOMForm(bomForm, ChooseClientComboBox.Text.Trim()))
            {
                this.Close();
            }
        }

        private void NewBOMButton_Click(object sender, EventArgs e)
        {
            if (bomForm.db.NewBOMForm(bomForm, NewClientTextBox.Text.Trim()))
            {
                this.Close();
            }
        }
    }
}