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

        public DBManager db;
        public FileManager fm;
        public CLIENT dbclient;
        public XElement flclient;
        public bool isOnline;
        List<NewCategory> categories = new List<NewCategory>();

        public BOMTool()
        {
            InitializeComponent();

            try
            {
                db = new DBManager();
                isOnline = true;
                categoryNames.Items.AddRange(db.GetCategoryNames());
            }
            catch
            {
                fm = new FileManager();
                isOnline = false;
                categoryNames.Items.AddRange(fm.GetCategoryNames());
                MessageBox.Show("Could not reach database: Offline mode set", "Error");
            }

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
            ChangedCategory();
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            ChangedCategory();
        }

        public void ChangedCategory()
        {
            objectiveNames.Items.Clear();
            objectiveNames.Text = "<Select Objective>";
            initiativeNames.Items.Clear();
            initiativeNames.Text = "";
            if (isOnline)
            {
                CATEGORY category;
                if (db.GetCategory(categoryNames.Text.Trim(), out category))
                {
                    objectiveNames.Items.AddRange((from ent in category.BUSINESSOBJECTIVE
                                                   select ent.NAME.TrimEnd()).ToArray());
                }
            }

            else
            {
                XElement category;
                if (fm.GetCategory(categoryNames.Text.Trim(), out category))
                {
                    objectiveNames.Items.AddRange((from ent in category.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                                                   select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
                }
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
            initiativeNames.Items.Clear();
            initiativeNames.Text = "<Select Initiative>";
            if (isOnline)
            {
                BUSINESSOBJECTIVE objective;
                if (db.GetObjective(objectiveNames.Text.Trim(), out objective))
                {
                    initiativeNames.Items.AddRange((from ent in objective.INITIATIVE
                                                    select ent.NAME.TrimEnd()).ToArray());
                }
            }

            else
            {
                XElement objective;
                if (fm.GetObjective(objectiveNames.Text.Trim(), out objective))
                {
                    initiativeNames.Items.AddRange((from ent in objective.Element("INITIATIVES").Elements("INITIATIVE")
                                                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
                }
            }
        }

        private void AddInitiativeButton_Click(object sender, EventArgs e)
        {
            string catName = categoryNames.Text.Trim();
            string busName = objectiveNames.Text.Trim();
            string iniName = initiativeNames.Text.Trim();

            if (isOnline)
            {
                INITIATIVE initiative;
                if (!db.GetInitiative(iniName, out initiative))
                {
                    initiative = new INITIATIVE();
                    initiative.NAME = iniName;
                    BUSINESSOBJECTIVE objective;
                    if (!db.GetObjective(busName, out objective))
                    {
                        objective = new BUSINESSOBJECTIVE();
                        objective.NAME = objectiveNames.Text.Trim();
                        CATEGORY category;
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
                bom.CLIENT = dbclient;
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

            else
            {
                XElement categoryXML;
                if (!fm.GetCategory(catName, out categoryXML))
                {
                    categoryXML = new XElement("CATEGORY");
                    categoryXML.Add(new XElement("NAME", catName.Replace(' ', '~')));
                    if (!fm.AddCategory(categoryXML))
                    {
                        MessageBox.Show("Failed to add Category to File", "Error");
                        return;
                    }
                }

                XElement objectiveXML;
                if (!fm.GetObjective(busName, out objectiveXML))
                {
                    objectiveXML = new XElement("BUSINESSOBJECTIVE");
                    objectiveXML.Add(new XElement("NAME", busName.Replace(' ', '~')));
                    if (!fm.AddObjective(objectiveXML, categoryXML))
                    {
                        MessageBox.Show("Failed to add Objective to File", "Error");
                        return;
                    }
                }

                XElement initiativeXML;
                if (!fm.GetInitiative(iniName, out initiativeXML))
                {
                    initiativeXML = new XElement("INITIATIVE");
                    initiativeXML.Add(new XElement("NAME", iniName.Replace(' ', '~')));
                    if (!fm.AddInitiative(initiativeXML, objectiveXML, categoryXML))
                    {
                        MessageBox.Show("Failed to add Initiative to File", "Error");
                        return;
                    }
                }

                XElement bom = new XElement("BOM");

                if (!fm.AddBOM(bom, flclient, initiativeXML.Element("NAME").Value, objectiveXML.Element("NAME").Value, categoryXML.Element("NAME").Value))
                {
                    MessageBox.Show("Failed to add Initiative to BOM", "Error");
                    return;
                }

                if (!fm.SaveChanges())
                {
                    MessageBox.Show("Failed to save changes to File", "Error");
                    fm = new FileManager();
                    return;
                }

                else
                {
                    //Successfully added to database, update GUI
                    NewCategory category = categories.Find(delegate(NewCategory cat)
                    {
                        return cat.name == catName;
                    });
                    if (category == null)
                    {
                        category = AddCategory(catName);
                    }

                    NewObjective objective = category.Objectives.Find(delegate(NewObjective bus)
                    {
                        return bus.Name == busName;
                    });
                    if (objective == null)
                    {
                        objective = category.AddObjective(busName);
                    }

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
        }
    } // end class
}
