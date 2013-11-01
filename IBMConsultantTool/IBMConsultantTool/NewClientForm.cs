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
    public partial class NewClientForm : Form
    {
        StartPage owner;
        DateTime selectedTime;

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
            client.StartDate = selectedTime;
            client.BusinessType = buisnessTypeComboBox.Text;

            ClientDataControl.Client = client;
            owner.CurrentClient = client;
            owner.Refresh();
            

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void startDateText_Click(object ender, EventArgs e)
        {
            MonthCalendar date = new MonthCalendar();
            this.Controls.Add(date);
            date.DateSelected +=new DateRangeEventHandler(date_DateSelected);
            date.Visible = true;
            date.BringToFront();
            //date.MinDate = new System.DateTime
        }
        private void date_DateSelected(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = (MonthCalendar)sender;
            
            startDateText.Text = e.Start.Date.ToShortDateString();
            selectedTime = e.Start.Date;
            cal.Visible = false;
        }


        #endregion

        public StartPage Owner
        {
            get { return owner; }
            set { owner = value; }
        }

    }
}