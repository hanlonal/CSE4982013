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
    public partial class AddInitiative : Form
    {
        public AddInitiative()
        {
            InitializeComponent();

            CategoryComboBox.Items.Add("<Select Category>");
            CategoryComboBox.SelectedIndex = 0;
            BusinessObjectiveComboBox.Items.Add("<Select Business Objective>");
            BusinessObjectiveComboBox.SelectedIndex = 0;
            InitiativeComboBox.Items.Add("<Select Initiative>");
            InitiativeComboBox.SelectedIndex = 0;
        }

        private void BusinessObjectiveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
