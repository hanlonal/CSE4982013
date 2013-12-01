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
    public partial class AddCupeQuestionForm : Form
    {
        private ChangeCUPEDefaults owner;

        public AddCupeQuestionForm(ChangeCUPEDefaults form)
        {
            this.owner = form;
            InitializeComponent();
        }

        private void addQuestionButton_Click(object sender, EventArgs e)
        {
            if (owner.AddQuestion(mainTextBox.Text, commTextBox.Text, utilTextBox.Text, partTextBox.Text, enabTextBox.Text))
            {
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
