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
    public partial class NewClientForm : Form
    {
        public NewClientForm()
        {
            InitializeComponent();
        }

        private void NewClientForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            okButton.Click +=new EventHandler(okButton_Click);
            cancelButton.Click +=new EventHandler(cancelButton_Click);
            startDateText.Click +=new EventHandler(startDateText_Click);
        }



        #region Event Handlers

        private void okButton_Click(object sender, EventArgs e)
        {
            Client client = new Client();

            client.Name = clientNameTextBox.Text;
            client.Location = locationTextBox.Text;


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
        private void startDateText_Click(object ender, EventArgs e)
        {
            MonthCalendar date = new MonthCalendar();
            this.Controls.Add(date);

            date.Visible = true;
            date.BringToFront();
        }


        #endregion

    }
}
