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
    public partial class AnalyticsForm : Form
    {

        DBManager db = new DBManager();
        public AnalyticsForm()
        {
            InitializeComponent();
            

            analyticsListBox.SelectedValueChanged +=new EventHandler(analyticsListBox_SelectedValueChanged);
        }

        private void AnalyticsForm_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Event Handlers

        private void analyticsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)analyticsListBox.SelectedItem;
            List<BUSINESSOBJECTIVE> values = new List<BUSINESSOBJECTIVE>();
            List<string> names = new List<string>();

            if (value == "Objectives")
            {
                values = db.GetObjectives().ToList();

                foreach (BUSINESSOBJECTIVE obj in values)
                {
                    names.Add(obj.NAME.Trim());
                }
                firstLevelComboBox.DataSource = names;
                firstLevelComboBox.SelectedValueChanged +=new EventHandler(firstLevelComboBox_SelectedValueChanged);
            }

            if (value == "Capabilities")
            {
                domainsComboBox.DataSource = null;
                domainsComboBox.Items.Clear();
                names = db.GetDomainNames().ToList();
                domainsComboBox.DataSource = names;

                domainsComboBox.SelectedValueChanged +=new EventHandler(domainsComboBox_SelectedValueChanged);

            }
        }

        private void firstLevelComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)firstLevelComboBox.SelectedText;
        }

        private void domainsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            capabilitiesComboBox.DataSource = null;
            capabilitiesComboBox.Items.Clear();

            List<String> names = new List<string>();

           // names = db.(domainsComboBox.SelectedText).ToList();

            capabilitiesComboBox.DataSource = names;

        }

        # endregion




    }
}
