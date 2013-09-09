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
    public partial class NewCategory : Form
    {
        public AddInitiative aiForm;
        public NewCategory(Form parentForm)
        {
            InitializeComponent();

            aiForm = parentForm as AddInitiative;
        }

        private void NewCategoryCreateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
