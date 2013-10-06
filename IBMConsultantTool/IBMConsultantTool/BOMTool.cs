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
    public partial class BOMTool : Form
    {
        List<NewCategory> categories = new List<NewCategory>();

        public BOMTool()
        {
            InitializeComponent();
        }

        private void newCategoryButton_Click(object sender, EventArgs e)
        {
            if (catNameTextBox.Text != "")
            {
                catWorkspace.TabPages.Add(catNameTextBox.Text, catNameTextBox.Text);
                catWorkspace.SelectedIndex = catWorkspace.TabCount - 1;
                NewCategory category = new NewCategory(this, catWorkspace.TabPages.Count - 1);
                categories.Add(category);

                //catWorkspace.TabPages[catNameTextBox.Text].Controls.Add(category.);
                catWorkspace.TabPages[catNameTextBox.Text].BackColor = Color.LightGray;
            }


        }


        public TabControl CategoryWorkspace
        {
            get
            {
                return catWorkspace;
            }
        }

        private void newObjectiveButton_Click(object sender, EventArgs e)
        {
            if (objNameTextBox.Text != "")
            {
                categories[catWorkspace.SelectedIndex].AddObjective(objNameTextBox.Text);
            }

        }
    } // end class
}
