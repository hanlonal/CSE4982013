using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public partial class BOMTool : Form
    {

        public DataManager db;
        public object client;
        public bool isOnline;
        List<NewCategory> categories = new List<NewCategory>();

        public BOMTool()
        {
            InitializeComponent();

            try
            {
                db = new DBManager();
                isOnline = true;
            }
            catch
            {
                db = new FileManager();
                isOnline = false;
                MessageBox.Show("Could not reach database: Offline mode set", "Error");
            }

            categoryNames.Items.AddRange(db.GetCategoryNames());

            new ChooseClient(this).ShowDialog();
        }

        public NewCategory AddCategory(string name)
        {
            catWorkspace.TabPages.Add(name, name);
            catWorkspace.SelectedIndex = catWorkspace.TabCount - 1;
            NewCategory category = new NewCategory(this, catWorkspace.TabPages.Count - 1, name);
            categories.Add(category);

            //catWorkspace.TabPages[name].Controls.Add(category.);
            catWorkspace.TabPages[name].BackColor = Color.LightGray;

            return category;
        }


        public TabControl CategoryWorkspace
        {
            get
            {
                return catWorkspace;
            }
        }

        private void newInitiativeButton_Click(object sender, EventArgs e)
        {
            categories[catWorkspace.SelectedIndex].AddInitiative(initiativeNames.Text);
        }

        public List<NewCategory> Categories
        {
            get
            {
                return categories;
            }
        }

        private void dataInputButton_Click(object sender, EventArgs e)
        {
            DataEntryForm form = new DataEntryForm(this);
            form.Show();
        }

        private void diffRadio_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByDifferentiation();
                    }
                }
            }
        }

        private void effectRadio_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByEffectiveness();
                    }
                }
            }
        }

        private void critRadio_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByCriticality();
                    }
                }
            }
        }

        private void categoryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedCategory(this);
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            db.ChangedCategory(this);
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedObjective(this);
        }

        private void objectiveNames_LostFocus(object sender, EventArgs e)
        {
            db.ChangedObjective(this);
        }

        private void AddInitiativeButton_Click(object sender, EventArgs e)
        {
            string catName = categoryNames.Text.Trim();
            string busName = objectiveNames.Text.Trim();
            string iniName = initiativeNames.Text.Trim();

            db.AddInitiativeToBOM(iniName, busName, catName, this);
        }
    } // end class
}
