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
    public partial class ChooseCUPEClient : Form
    {
        CUPETool cupeForm;
        public ChooseCUPEClient(CUPETool parentForm)
        {
            InitializeComponent();

            cupeForm = parentForm;
            ChooseClientComboBox.Items.AddRange(cupeForm.db.GetClientNames());
            this.Focus();
        }

        private void OpenCUPEButton_Click(object sender, EventArgs e)
        {
            if (cupeForm.db.BuildCUPEForm(cupeForm, ChooseClientComboBox.Text.Trim()))
            {
                this.Close();
            }
        }

        private void NewCUPEButton_Click(object sender, EventArgs e)
        {
            if (cupeForm.db.NewCUPEForm(cupeForm, NewClientTextBox.Text.Trim()))
            {
                this.Close();
            }
        }
    }
}