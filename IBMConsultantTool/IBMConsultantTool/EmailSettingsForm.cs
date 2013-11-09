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
    public partial class EmailSettingsForm : Form
    {
        string originalEmail;
        string originalPassword;

        public EmailSettingsForm()
        {
            InitializeComponent();
        }

        private void EmailSettingsForm_Load(object sender, EventArgs e)
        {
            emailTextBox.DataBindings.Add("Text", ClientDataControl.Instance, "EmailAddress");
            passwordTextBox.DataBindings.Add("Text", ClientDataControl.Instance, "EmailPassword");

            originalEmail = emailTextBox.Text;
            originalPassword = passwordTextBox.Text;
        }

        //cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            ClientDataControl.Instance.EmailAddress = originalEmail;
            ClientDataControl.Instance.EmailPassword = originalPassword;
            this.Close();
        }
        //save button
        // need file manager to write to filesystem to update new email address
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
