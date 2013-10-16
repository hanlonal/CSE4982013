using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public class FileManager : DataManager
    {
        public XElement dbo;
        public List<string> changeLog;

        public FileManager()
        {
            try
            {
                dbo = XElement.Load("Data.xml");
            }

            catch
            {
                dbo = new XElement("root");
                dbo.Add(new XElement("CLIENTS"));
                dbo.Add(new XElement("CATEGORIES"));
                dbo.Save("Data.xml");
            }

            changeLog = new List<string>();
        }

        #region Client
        public List<XElement> GetClients()
        {
            return (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                    select ent).ToList();
        }

        public override string[] GetClientNames()
        {
            return (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public bool GetClient(string cntName, out XElement client)
        {
            cntName = cntName.Replace(' ', '~');
            try
            {
                client = (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                          where ent.Element("NAME").Value == cntName
                          select ent).Single();
            }

            catch
            {
                client = null;
                return false;
            }

            return true;
        }

        public bool GetClient(int cntID, out XElement client)
        {
            try
            {
                client = (from ent in dbo.Element("CLIENTS").Elements("CLIENTS")
                          where ent.Element("CLIENTID").Value == cntID.ToString()
                          select ent).Single();
            }

            catch
            {
                client = null;
                return false;
            }

            return true;
        }

        public bool AddClient(XElement client)
        {
            //If already in DB, return false
            if ((from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                 where ent.Element("NAME").Value == client.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            client.Add(new XElement("CLIENTID", -1));
            client.Add(new XElement("GROUPS"));
            client.Add(new XElement("BOMS"));

            dbo.Element("CLIENTS").Add(client);

            changeLog.Add("ADD CLIENT " + client.Element("NAME").Value);

            return true;
        }
        #endregion

        #region Group
        //group is a keyword in C#
        public bool AddGroup(string grpName, XElement client)
        {
            grpName = grpName.Replace(' ', '~');
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in client.Element("GROUPS").Elements("GROUP")
                 where ent.Element("NAME") != null &&
                       ent.Element("NAME").Value == grpName
                 select ent).Count() != 0)
            {
                return false;
            }

            XElement grp = new XElement("GROUP");
            grp.Add(new XElement("NAME", grpName));

            grp.Add(new XElement("GROUPID", -1));

            grp.Add(new XElement("BOMS"));

            client.Element("GROUPS").Add(grp);

            changeLog.Add("ADD GROUP " + grp.Element("NAME").Value + " " +
                            client.Element("NAME").Value);

            return true;
        }
        #endregion

        #region BOM
        public override bool UpdateBOM(object clientObj, NewInitiative ini)
        {
            XElement client = clientObj as XElement;
            string formattedName = ini.Name.Replace(' ', '~');
            try
            {
                XElement bom = (from ent in client.Element("BOMS").Elements("BOM")
                                where ent.Element("INITIATIVE").Value == formattedName
                                select ent).Single();

                bom.Element("EFFECTIVENESS").Value = ini.Effectiveness.ToString();
                bom.Element("CRITICALITY").Value = ini.Criticality.ToString();
                bom.Element("DIFFERENTIAL").Value = ini.Differentiation.ToString();

                changeLog.Add("UPDATE BOM " + client.Element("NAME").Value + " " + formattedName + " " +
                                ini.Effectiveness + " " + ini.Criticality + " " + ini.Differentiation);
            }

            catch
            {
                return false;
            }


            return true;
        }

        public override bool AddBOM(object bomObj, object clientObj)
        {
            XElement bom = bomObj as XElement;
            XElement client = clientObj as XElement;
            string iniXML = bom.Element("INITIATIVE").Value.Replace(' ', '~');
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value.Replace(' ', '~');
            string catXML = bom.Element("CATEGORY").Value.Replace(' ', '~');

            List<XElement> bomList = client.Element("BOMS").Elements("BOM").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in bomList
                 where ent != null &&
                       ent.Element("INITIATIVE") != null &&
                       ent.Element("INITIATIVE").Value == iniXML
                 select ent).Count() != 0)
            {
                return false;
            }

            bom.Add(new XElement("BOMID", -1));
            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            client.Element("BOMS").Add(bom);

            changeLog.Add("ADD BOM CLIENT " + client.Element("NAME").Value + " " + iniXML);

            return true;
        }

        public override bool AddBOMToGroup(object bomObj, object groupObj)
        {
            XElement bom = bomObj as XElement;
            XElement grp = groupObj as XElement;
            string iniXML = bom.Element("INITIATIVE").Value.Replace(' ', '~');
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value.Replace(' ', '~');
            string catXML = bom.Element("CATEGORY").Value.Replace(' ', '~');

            List<XElement> bomList = grp.Element("BOMS").Elements("BOM").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in bomList
                 where ent != null &&
                       ent.Element("INITIATIVE") != null &&
                       ent.Element("INITIATIVE").Value == iniXML
                 select ent).Count() != 0)
            {
                return false;
            }

            bom.Add(new XElement("BOMID", -1));
            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            grp.Element("BOMS").Add(bom);

            XElement client = grp.Parent.Parent;

            changeLog.Add("ADD BOM GROUP " + grp.Element("NAME").Value + " " +
                          client.Element("NAME").Value + " " + iniXML);

            return true;
        }

        public override bool AddBOMToContact(object bomObj, object contactObj)
        {
            XElement bom = bomObj as XElement;
            XElement contact = contactObj as XElement;
            string iniXML = bom.Element("INITIATIVE").Value.Replace(' ', '~');
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value.Replace(' ', '~');
            string catXML = bom.Element("CATEGORY").Value.Replace(' ', '~');

            List<XElement> bomList = contact.Element("BOMS").Elements("BOM").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in bomList
                 where ent != null &&
                       ent.Element("INITIATIVE") != null &&
                       ent.Element("INITIATIVE").Value == iniXML
                 select ent).Count() != 0)
            {
                return false;
            }

            bom.Add(new XElement("BOMID", -1));
            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            contact.Element("BOMS").Add(bom);

            XElement grp = contact.Parent.Parent;
            XElement client = grp.Parent.Parent;

            changeLog.Add("ADD BOM CONTACT " + contact.Element("NAME").Value + " " +
                          grp.Element("NAME").Value + " " +
                          client.Element("NAME").Value + " " + iniXML);

            return true;
        }

        public override bool BuildBOMForm(BOMTool bomForm, string clientName)
        {
            XElement client;

            if (GetClient(clientName, out client))
            {
                bomForm.client = client;

                string catName;
                string busName;
                string iniName;

                NewCategory category;
                NewObjective objective;
                NewInitiative initiative;

                foreach (XElement bom in client.Element("BOMS").Elements("BOM"))
                {
                    catName = bom.Element("CATEGORY").Value.TrimEnd().Replace('~', ' ');
                    category = bomForm.Categories.Find(delegate(NewCategory cat)
                    {
                        return cat.name == catName;
                    });
                    if (category == null)
                    {
                        category = bomForm.AddCategory(catName);
                    }

                    busName = bom.Element("BUSINESSOBJECTIVE").Value.TrimEnd().Replace('~', ' ');
                    objective = category.Objectives.Find(delegate(NewObjective bus)
                    {
                        return bus.Name == busName;
                    });
                    if (objective == null)
                    {
                        objective = category.AddObjective(busName);
                    }

                    iniName = bom.Element("INITIATIVE").Value.TrimEnd().Replace('~', ' ');
                    initiative = objective.Initiatives.Find(delegate(NewInitiative ini)
                    {
                        return ini.Name == iniName;
                    });
                    if (initiative == null)
                    {
                        initiative = objective.AddInitiative(iniName);
                        initiative.Effectiveness = Convert.ToSingle(bom.Element("EFFECTIVENESS").Value);
                        initiative.Criticality = Convert.ToSingle(bom.Element("CRITICALITY").Value);
                        initiative.Differentiation = Convert.ToSingle(bom.Element("DIFFERENTIAL").Value);
                    }
                }

                return true;
            }

            else
            {
                MessageBox.Show("Client could not be found", "Error");
                return false;
            }
        }
        public override bool NewBOMForm(BOMTool bomForm, string clientName)
        {
            XElement client;
            if (!GetClient(clientName, out client))
            {
                client = new XElement("CLIENT");
                client.Add(new XElement("NAME", clientName));
                if (!AddClient(client))
                {
                    MessageBox.Show("Failed to add client", "Error");
                    return false;
                }

                if (!AddGroup("Business", client) || !AddGroup("IT", client))
                {
                    MessageBox.Show("Failed to add groups to client", "Error");
                    return false;
                }

                if (!SaveChanges())
                {
                    MessageBox.Show("Failed to save changes to filesystem", "Error");
                    return false;
                }

                bomForm.client = client;

                return true;
            }
            else
            {
                MessageBox.Show("Client already exists", "Error");
                return false;
            }
        }
        #endregion

        #region Category
        public List<XElement> GetCategoriesXML()
        {
            return (from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                    select ent).ToList();
        }

        public override string[] GetCategoryNames()
        {
            return (from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public bool GetCategory(int catID, out XElement category)
        {
            try
            {
                category = (from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                            where ent.Element("CATEGORYID").Value == catID.ToString()
                            select ent).Single();
            }

            catch
            {
                category = null;
                return false;
            }

            return true;
        }

        public bool GetCategory(string catName, out XElement category)
        {
            catName = catName.Replace(' ', '~');
            try
            {
                category = (from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                            where ent.Element("NAME").Value == catName
                            select ent).Single();
            }

            catch
            {
                category = null;
                return false;
            }

            return true;
        }

        public bool AddCategory(XElement category)
        {
            //If already in DB, return 1
            if ((from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                 where ent.Element("NAME").Value == category.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            category.Add(new XElement("CATEGORYID", -1));
            category.Add(new XElement("BUSINESSOBJECTIVES"));

            dbo.Element("CATEGORIES").Add(category);

            changeLog.Add("ADD CATEGORY " + category.Element("NAME").Value);

            return true;
        }

        public override void ChangedCategory(BOMTool bomForm)
        {
            bomForm.objectiveNames.Items.Clear();
            bomForm.objectiveNames.Text = "<Select Objective>";
            bomForm.initiativeNames.Items.Clear();
            bomForm.initiativeNames.Text = "";
            XElement category;
            if (GetCategory(bomForm.categoryNames.Text.Trim(), out category))
            {
                bomForm.objectiveNames.Items.AddRange((from ent in category.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                                                       select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
            }
        }
        #endregion

        #region BusinessObjective

        public bool GetObjective(int busID, out XElement objective)
        {
            try
            {
                objective = (from cnt in dbo.Element("CLIENTS").Elements("CLIENT")
                             from ent in cnt.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                             where ent.Element("BUSINESSOBJECTIVEID").Value == busID.ToString()
                             select ent).Single();
            }

            catch
            {
                objective = null;
                return false;
            }

            return true;
        }

        public bool GetObjective(string busName, out XElement objective)
        {
            busName = busName.Replace(' ', '~');
            try
            {
                objective = (from cnt in dbo.Element("CATEGORIES").Elements("CATEGORY")
                             from ent in cnt.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                             where ent.Element("NAME").Value == busName
                             select ent).Single();
            }

            catch
            {
                objective = null;
                return false;
            }

            return true;
        }

        public bool AddObjective(XElement objective, XElement category)
        {
            //If already in DB, return 1
            if ((from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                 where cat.Element("NAME").Value == category.Element("NAME").Value
                 from ent in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                 where ent.Element("NAME").Value == objective.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            objective.Add(new XElement("BUSINESSOBJECTIVEID", -1));
            objective.Add(new XElement("INITIATIVES"));

            category.Element("BUSINESSOBJECTIVES").Add(objective);

            changeLog.Add("ADD BUSINESSOBJECTIVE " + objective.Element("NAME").Value + " " +
                            category.Element("NAME").Value);

            return true;
        }

        public override void ChangedObjective(BOMTool bomForm)
        {
            bomForm.initiativeNames.Items.Clear();
            bomForm.initiativeNames.Text = "<Select Initiative>";
            XElement objective;
            if (GetObjective(bomForm.objectiveNames.Text.Trim(), out objective))
            {
                bomForm.initiativeNames.Items.AddRange((from ent in objective.Element("INITIATIVES").Elements("INITIATIVE")
                                                        select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
            }
        }

        #endregion

        #region Initiative
        public bool GetInitiative(string iniName, out XElement Initiative)
        {
            iniName = iniName.Replace(' ', '~');
            try
            {
                Initiative = (from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                              from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                              from ent in bus.Element("INITIATIVES").Elements("INITIATIVE")
                              where ent.Element("NAME").Value == iniName
                              select ent).Single();
            }

            catch
            {
                Initiative = null;
                return false;
            }

            return true;
        }

        public bool GetInitiative(int iniID, out XElement Initiative)
        {
            try
            {
                Initiative = (from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                              from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                              from ent in bus.Element("INITIATIVES").Elements("INITIATIVE")
                              where ent.Element("INITIATIVEID").Value == iniID.ToString()
                              select ent).Single();
            }

            catch
            {
                Initiative = null;
                return false;
            }

            return true;
        }

        public bool AddInitiative(XElement initiative, XElement objective, XElement category)
        {
            //If already in DB, return 1
            if ((from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                 where cat.Element("NAME").Value == category.Element("NAME").Value
                 from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                 where bus.Element("NAME").Value == objective.Element("NAME").Value
                 from ent in bus.Element("INITIATIVES").Elements("INITIATIVE")
                 where ent.Element("NAME").Value == initiative.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            initiative.Add(new XElement("INITIATIVEID", -1));

            objective.Element("INITIATIVES").Add(initiative);

            changeLog.Add("ADD INITIATIVE " + initiative.Element("NAME").Value + " " +
                            objective.Element("NAME").Value + " " +
                            category.Element("NAME").Value);

            return true;
        }

        public override void AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
        {
            XElement categoryXML;
            if (!GetCategory(catName, out categoryXML))
            {
                categoryXML = new XElement("CATEGORY");
                categoryXML.Add(new XElement("NAME", catName.Replace(' ', '~')));
                if (!AddCategory(categoryXML))
                {
                    MessageBox.Show("Failed to add Category to File", "Error");
                    return;
                }
            }

            XElement objectiveXML;
            if (!GetObjective(busName, out objectiveXML))
            {
                objectiveXML = new XElement("BUSINESSOBJECTIVE");
                objectiveXML.Add(new XElement("NAME", busName.Replace(' ', '~')));
                if (!AddObjective(objectiveXML, categoryXML))
                {
                    MessageBox.Show("Failed to add Objective to File", "Error");
                    return;
                }
            }

            XElement initiativeXML;
            if (!GetInitiative(iniName, out initiativeXML))
            {
                initiativeXML = new XElement("INITIATIVE");
                initiativeXML.Add(new XElement("NAME", iniName.Replace(' ', '~')));
                if (!AddInitiative(initiativeXML, objectiveXML, categoryXML))
                {
                    MessageBox.Show("Failed to add Initiative to File", "Error");
                    return;
                }
            }

            XElement bom = new XElement("BOM");
            bom.Add(new XElement("INITIATIVE", initiativeXML.Element("NAME").Value));
            bom.Add(new XElement("BUSINESSOBJECTIVE", objectiveXML.Element("NAME").Value));
            bom.Add(new XElement("CATEGORY", categoryXML.Element("NAME").Value));


            if (!AddBOM(bom, bomForm.client))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return;
            }

            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to File", "Error");
                return;
            }

            else
            {
                //Successfully added to database, update GUI
                NewCategory category = bomForm.Categories.Find(delegate(NewCategory cat)
                {
                    return cat.name == catName;
                });
                if (category == null)
                {
                    category = bomForm.AddCategory(catName);
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

        #endregion

        #region General

        public override bool SaveChanges()
        {
            try
            {
                dbo.Save("Data.xml");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Changes.log", true))
                {
                    foreach (string change in changeLog)
                    {
                        file.WriteLine(change);
                    }
                }

                changeLog.Clear();
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}