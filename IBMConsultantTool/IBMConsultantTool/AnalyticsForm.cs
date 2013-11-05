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
            List<string> values = new List<string>();

            if (value == "Initiatives")
            {
                values = db.GetCategoryNames().ToList();
                firstLevelComboBox.DataSource = values;
            }
        }


        # endregion


    }
}
