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

        public DataManager db;
        public CLIENT client;
        List<NewCategory> categories = new List<NewCategory>();
       // public DBManager db;
       // public CLIENT client;

        public BOMTool()
        {
            InitializeComponent();

            db = new DBManager();

            new ChooseClient(this).ShowDialog();


            categoryNames.Items.AddRange(db.GetCategoryNames());
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
            ChangedCategory();
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            ChangedCategory();
        }

        public void ChangedCategory()
        {
            CATEGORY category;

            objectiveNames.Items.Clear();
            objectiveNames.Text = "<Select Objective>";
            initiativeNames.Items.Clear();
            initiativeNames.Text = "";
            if (db.GetCategory(categoryNames.Text.Trim(), out category))
            {
                objectiveNames.Items.AddRange((from ent in category.BUSINESSOBJECTIVE
                                               select ent.NAME.TrimEnd()).ToArray());
            }
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedObjective();
        }

        private void objectiveNames_LostFocus(object sender, EventArgs e)
        {
            ChangedObjective();
        }

        private void ChangedObjective()
        {
            BUSINESSOBJECTIVE objective;

            initiativeNames.Items.Clear();
            initiativeNames.Text = "<Select Initiative>";
            if (db.GetObjective(objectiveNames.Text.Trim(), out objective))
            {
                initiativeNames.Items.AddRange((from ent in objective.INITIATIVE
                                                select ent.NAME.TrimEnd()).ToArray());
            }
        }

        private void AddInitiativeButton_Click(object sender, EventArgs e)
        {
            string catName;
            string busName;
            string iniName = initiativeNames.Text.Trim();
            INITIATIVE initiative;
            if (!db.GetInitiative(iniName, out initiative))
            {
                initiative = new INITIATIVE();
                initiative.NAME = iniName;
                BUSINESSOBJECTIVE objective;
                busName = objectiveNames.Text.Trim();
                if (!db.GetObjective(busName, out objective))
                {
                    objective = new BUSINESSOBJECTIVE();
                    objective.NAME = objectiveNames.Text.Trim();
                    CATEGORY category;
                    catName = categoryNames.Text.Trim();
                    if (!db.GetCategory(catName, out category))
                    {
                        category = new CATEGORY();
                        category.NAME = catName;
                        if (!db.AddCategory(category))
                        {
                            MessageBox.Show("Failed to add Category to Database", "Error");
                            return;
                        }
                    }

                    objective.CATEGORY = category;
                    if (!db.AddObjective(objective))
                    {
                        MessageBox.Show("Failed to add Objective to Database", "Error");
                        return;
                    }
                }

                initiative.BUSINESSOBJECTIVE = objective;
                if (!db.AddInitiative(initiative))
                {
                    MessageBox.Show("Failed to add Initiative to Database", "Error");
                    return;
                }
            }

            BOM bom = new BOM();
            bom.CLIENT = client;
            bom.INITIATIVE = initiative;
            if (!db.AddBOM(bom))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return;
            }
            if (!db.SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
                db = new DBManager();
                return;
            }

            else
            {
                //Successfully added to database, update GUI
                catName = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                NewCategory category = categories.Find(delegate(NewCategory cat)
                                                       {
                                                           return cat.name == catName;
                                                       });
                if (category == null)
                {
                    category = AddCategory(catName);
                }

                busName = bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                NewObjective objective = category.Objectives.Find(delegate(NewObjective bus)
                                                                  {
                                                                      return bus.Name == busName;
                                                                  });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

                iniName = bom.INITIATIVE.NAME.TrimEnd();
                NewInitiative initiativeObj = objective.Initiatives.Find(delegate(NewInitiative ini)
                                                                         {
                                                                             return ini.Name == iniName;
                                                                         });
                if (initiativeObj == null)
                {
                    initiativeObj = objective.AddInitiative(iniName);
                }
                else
                {
                    MessageBox.Show("Initiative already exists in BOM", "Error");
                }
            }
        }
    } // end class
}
