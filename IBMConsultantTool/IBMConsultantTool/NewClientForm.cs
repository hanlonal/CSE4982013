using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IBMConsultantTool
{
    public partial class NewClientForm : Form
    {
        DateTime selectedTime;
        MonthCalendar date;

        public NewClientForm()
        {
            InitializeComponent();
        }

        private void NewClientForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            okButton.Click += new EventHandler(okButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            startDateText.Click += new EventHandler(startDateText_Click);
            startDateText.LostFocus +=new EventHandler(startDateText_LostFocus);
            //countryComboBox.Items.AddRange(ClientDataControl.GetCoutnryNames());
            RegionComboBox.Items.AddRange(ClientDataControl.GetRegionNames());
            BusinessTypeComboBox.Items.AddRange(ClientDataControl.GetBusinessTypeNames());
            startDateText.ReadOnly = true;

            // make all input read only
            
        }



        #region Event Handlers

        private void okButton_Click(object sender, EventArgs e)
        {
            Client client = new Client();

            client.Name = clientNameTextBox.Text;
            client.Region = RegionComboBox.Text;
            client.Country = countryComboBox.Text;
            client.StartDate = selectedTime;
            client.BusinessType = BusinessTypeComboBox.Text;

            if (clientNameTextBox.Text == "" || RegionComboBox.Text == "" ||
                        countryComboBox.Text == "" || startDateText.Text == "" || BusinessTypeComboBox.Text == "")
            {
                MessageBox.Show("All fields required. Please check fields and try again.");
                return;
            }

            if (!ClientDataControl.NewClient(client))
            {
                MessageBox.Show("Failed to create new client: " + client.Name + " already exists", "Error");
                return;
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void startDateText_Click(object ender, EventArgs e)
        {
            date = new MonthCalendar();
            this.Controls.Add(date);
            date.DateSelected += new DateRangeEventHandler(date_DateSelected);
            date.Visible = true;
            date.Location = new Point(date.Parent.Location.X / 2, date.Parent.Location.Y / 2);
            
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

        private void startDateText_LostFocus(object sender, EventArgs e)
        {
            date.Visible = false;
        }

        #endregion

        private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            countryComboBox.Items.Clear();
            countryComboBox.Text = "";
            countryComboBox.Items.AddRange(ClientDataControl.db.GetCountryNames(RegionComboBox.Text).ToArray());
        }

        private void RegionComboBox_LostFocus(object sender, EventArgs e)
        {
            countryComboBox.Items.Clear();
            countryComboBox.Text = "";
            countryComboBox.Items.AddRange(ClientDataControl.db.GetCountryNames(RegionComboBox.Text).ToArray());
        }

    }
}