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

        #region ITCAP

        public override bool UpdateITCAP(object clientObj, ITCapQuestion itcapQuestion)
        {
            XElement client = clientObj as XElement;
            string formattedName = itcapQuestion.Name.Replace(' ', '~');
            try
            {
                XElement itcap = (from ent in client.Element("ITCAPS").Elements("ITCAP")
                                where ent.Element("ITCAPQUESTION").Value == formattedName
                                select ent).Single();

                itcap.Element("ASIS").Value = itcapQuestion.AsIsScore.ToString();
                itcap.Element("TOBE").Value = itcapQuestion.ToBeScore.ToString();

                changeLog.Add("UPDATE ITCAP " + client.Element("NAME").Value + " " + formattedName + " " +
                                itcapQuestion.AsIsScore + " " + itcapQuestion.ToBeScore);
            }

            catch
            {
                return false;
            }


            return true;
        }

        public override bool BuildITCAPForm(ITCapTool itcapForm, string clientName)
        {
            XElement client;

            if (GetClient(clientName, out client))
            {
                itcapForm.client = client;

                return true;
            }

            else
            {
                MessageBox.Show("Client could not be found", "Error");
                return false;
            }
        }

        public override bool AddITCAP(object itcapObj, object clientObj, List<int> otherIDList = null)
        {
            XElement itcap = itcapObj as XElement;
            XElement client = clientObj as XElement;
            string itcqXML = itcap.Element("ITCAPQUESTION").Value.Replace(' ', '~');
            string capXML = itcap.Element("CAPABILITY").Value.Replace(' ', '~');
            string domXML = itcap.Element("DOMAIN").Value.Replace(' ', '~');

            List<XElement> itcapList = client.Element("ITCAPS").Elements("ITCAP").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in itcapList
                 where ent != null &&
                       ent.Element("ITCAPQUESTION") != null &&
                       ent.Element("ITCAPQUESTION").Value == itcqXML
                 select ent).Count() != 0)
            {
                return false;
            }

            itcap.Add(new XElement("ITCAPID", -1));
            itcap.Add(new XElement("ASIS", 0));
            itcap.Add(new XElement("TOBE", 0));
            itcap.Add(new XElement("COMMENT", ""));

            client.Element("ITCAPS").Add(itcap);

            changeLog.Add("ADD ITCAP CLIENT " + client.Element("NAME").Value + " " + itcqXML);

            return true;
        }
        public override bool NewITCAPForm(ITCapTool itcapForm, string clientName)
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

                itcapForm.client = client;

                return true;
            }
            else
            {
                MessageBox.Show("Client already exists", "Error");
                return false;
            }
        }

        public override bool OpenITCAP(ITCapTool itcapForm)
        {
            if (itcapForm.client == null)
            {
                MessageBox.Show("Must choose client before opening ITCAP", "Error");
                return false;
            }

            List<XElement> itcapList = (itcapForm.client as XElement).Element("ITCAPS").Elements("ITCAP").ToList();

            XElement itcqEnt;
            XElement capEnt;
            XElement domEnt;

            string itcqName;
            string capName;
            string domName;

            foreach (XElement itcap in itcapList)
            {
                itcqName = itcap.Element("ITCAPQUESTION").Value.Replace('~', ' ');
                capName = itcap.Element("CAPABILITY").Value.Replace('~', ' ');
                domName = itcap.Element("DOMAIN").Value.Replace('~', ' ');

                Domain domain;
                Capability capability;
                ITCapQuestion itcapQuestion;

                domain = itcapForm.domains.Find(delegate(Domain dom)
                {
                    return dom.Name == domName;
                });
                if (domain == null)
                {
                    domain = new Domain();
                    domain.Name = domName;
                    if (!GetDomain(domName, out domEnt))
                    {
                        break;
                    }
                    domain.IsDefault = domEnt.Element("DEFAULT").Value == "Y";
                    //itcapForm.LoadCapabilities(dom);
                    itcapForm.domains.Add(domain);
                    itcapForm.entities.Add(domain);
                    domain.ID = itcapForm.domains.Count.ToString();
                }

                capability = itcapForm.capabilities.Find(delegate(Capability cap)
                {
                    return cap.Name == capName;
                });
                if (capability == null)
                {
                    capability = new Capability();
                    capability.Name = capName;
                    if(!GetCapability(capName, out capEnt))
                    {
                        break;
                    }
                    capability.IsDefault = capEnt.Element("DEFAULT").Value == "Y";
                    domain.CapabilitiesOwned.Add(capability);
                    domain.TotalChildren++;
                    itcapForm.capabilities.Add(capability);
                    capability.Owner = domain;
                    capability.ID = domain.CapabilitiesOwned.Count.ToString();
                    //LoadQuestions(cap);
                    itcapForm.entities.Add(capability);
                }

                itcapQuestion = new ITCapQuestion();
                itcapQuestion.Name = itcqName;
                if (!GetITCAPQuestion(itcqName, out itcqEnt))
                {
                    break;
                }
                itcapQuestion.IsDefault = itcqEnt.Element("DEFAULT").Value == "Y";
                itcapQuestion.AsIsScore = Convert.ToSingle(itcap.Element("ASIS").Value);
                itcapQuestion.ToBeScore = Convert.ToSingle(itcap.Element("TOBE").Value);
                capability.Owner.TotalChildren++;
                capability.QuestionsOwned.Add(itcapQuestion);
                itcapQuestion.Owner = capability;
                itcapQuestion.ID = capability.QuestionsOwned.Count.ToString();
                itcapForm.entities.Add(itcapQuestion);
            }
            return true;
        }

        public override bool RewriteITCAP(ITCapTool itcapForm)
        {
            XElement client = itcapForm.client as XElement;
            List<XElement> itcapList = client.Element("ITCAPS").Elements("ITCAP").ToList();
            foreach (XElement itcap in itcapList)
            {
                itcap.RemoveAll();
            }

            XElement itcapEnt = new XElement("ITCAP");
            XElement itcqEnt;
            foreach (Domain domain in itcapForm.domains)
            {
                foreach (Capability capability in domain.CapabilitiesOwned)
                {
                    foreach (ITCapQuestion itcapQuestion in capability.QuestionsOwned)
                    {
                        if (GetITCAPQuestion(itcapQuestion.Name, out itcqEnt))
                        {
                            itcapEnt.Add(new XElement("ITCAPQUESTION", itcqEnt.Name));
                            if (!AddITCAP(itcapEnt, client))
                            {
                                MessageBox.Show("Failed to add ITCAPQuestion: " + itcapEnt.Element("ITCAPQUESTION").Value, "Error");
                            }
                        }
                    }
                }
            }
            if (SaveChanges())
            {
                return true;
            }

            else
            {
                MessageBox.Show("Failed to rewrite ITCAP", "Error");
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
                objective = (from cnt in dbo.Element("CATEGORIES").Elements("CATEGORY")
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

        public override bool AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
        {
            XElement categoryXML;
            if (!GetCategory(catName, out categoryXML))
            {
                categoryXML = new XElement("CATEGORY");
                categoryXML.Add(new XElement("NAME", catName.Replace(' ', '~')));
                if (!AddCategory(categoryXML))
                {
                    MessageBox.Show("Failed to add Category to File", "Error");
                    return false;
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
                    return false;
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
                    return false;
                }
            }

            XElement bom = new XElement("BOM");
            bom.Add(new XElement("INITIATIVE", initiativeXML.Element("NAME").Value));
            bom.Add(new XElement("BUSINESSOBJECTIVE", objectiveXML.Element("NAME").Value));
            bom.Add(new XElement("CATEGORY", categoryXML.Element("NAME").Value));


            if (!AddBOM(bom, bomForm.client))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return false;
            }

            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to File", "Error");
                return false;
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

            return true;
        }

        #endregion

        #region Domain
        public override string[] GetDomainNames()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public override string[] GetDomainNamesAndDefault()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    select ent.Element("NAME").Value.Replace('~', ' ') + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultDomainNames()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public bool GetDomain(string domName, out XElement domain)
        {
            domName = domName.Replace(' ', '~');
            try
            {
                domain = (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                            where ent.Element("NAME").Value == domName
                            select ent).Single();
            }

            catch
            {
                domain = null;
                return false;
            }

            return true;
        }

        public bool AddDomain(XElement domain)
        {
            //If already in DB, return 1
            if ((from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                 where ent.Element("NAME").Value == domain.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            domain.Add(new XElement("DOMAINID", -1));
            domain.Add(new XElement("CAPABILITIES"));

            dbo.Element("DOMAINS").Add(domain);

            changeLog.Add("ADD DOMAIN " + domain.Element("NAME").Value);

            return true;
        }

        public override void ChangedDomain(ITCapTool itcapForm)
        {
            itcapForm.capabilitiesList.Items.Clear();
            itcapForm.capabilitiesList.Text = "<Select Capability>";
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "";
            XElement domain;

            if (GetDomain(itcapForm.domainList.Text.Trim(), out domain))
            {
                itcapForm.capabilitiesList.Items.AddRange((from ent in domain.Element("CAPABILITIES").Elements("CAPABILITY")
                                                       select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
            }
        }
        #endregion

        #region Capability

        public override string[] GetCapabilityNames(string domName)
        {
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public override string[] GetCapabilityNamesAndDefault(string domName)
        {
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    select ent.Element("NAME").Value.Replace('~', ' ') + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultCapabilityNames(string domName)
        {
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public bool GetCapability(string capName, out XElement capability)
        {
            capName = capName.Replace(' ', '~');
            try
            {
                capability = (from cnt in dbo.Element("DOMAINS").Elements("DOMAIN")
                             from ent in cnt.Element("CAPABILITIES").Elements("CAPABILITY")
                             where ent.Element("NAME").Value == capName
                             select ent).Single();
            }

            catch
            {
                capability = null;
                return false;
            }

            return true;
        }

        public bool AddCapability(XElement capability, XElement domain)
        {
            //If already in DB, return 1
            if ((from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                 where dom.Element("NAME").Value == domain.Element("NAME").Value
                 from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                 where ent.Element("NAME").Value == capability.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            capability.Add(new XElement("CAPABILITYID", -1));
            capability.Add(new XElement("ITCAPQUESTIONS"));

            domain.Element("CAPABILITIES").Add(capability);

            changeLog.Add("ADD CAPABILITY " + capability.Element("NAME").Value + " " +
                            domain.Element("NAME").Value);

            return true;
        }

        public override void ChangedCapability(ITCapTool itcapForm)
        {
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "<Select ITCAPQuestion>";
            XElement capability;

            if (GetCapability(itcapForm.capabilitiesList.Text.Trim(), out capability))
            {
                itcapForm.questionList.Items.AddRange((from ent in capability.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                                                       select ent.Element("NAME").Value.Replace('~', ' ')).ToArray());
            }
        }
        #endregion

        #region ITCAPQuestion

        public override string[] GetITCAPQuestionNames(string capName, string domName)
        {
            capName = capName.Replace(' ', '~');
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public override string[] GetITCAPQuestionNamesAndDefault(string capName, string domName)
        {
            capName = capName.Replace(' ', '~');
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    select ent.Element("NAME").Value.Replace('~', ' ') + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultITCAPQuestionNames(string capName, string domName)
        {
            capName = capName.Replace(' ', '~');
            domName = domName.Replace(' ', '~');
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value.Replace('~', ' ')).ToArray();
        }

        public bool GetITCAPQuestion(string itcapName, out XElement itcapQuestion)
        {
            itcapName = itcapName.Replace(' ', '~');
            try
            {
                itcapQuestion = (from cat in dbo.Element("DOMAINS").Elements("DOMAIN")
                              from bus in cat.Element("CAPABILITIES").Elements("CAPABILITY")
                              from ent in bus.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                              where ent.Element("NAME").Value == itcapName
                              select ent).Single();
            }

            catch
            {
                itcapQuestion = null;
                return false;
            }

            return true;
        }

        public bool AddITCAPQuestion(XElement itcapQuestion, XElement capability, XElement domain)
        {
            //If already in DB, return 1
            if ((from cat in dbo.Element("DOMAINS").Elements("DOMAIN")
                 where cat.Element("NAME").Value == domain.Element("NAME").Value
                 from bus in cat.Element("CAPABILITIES").Elements("CAPABILITY")
                 where bus.Element("NAME").Value == capability.Element("NAME").Value
                 from ent in bus.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                 where ent.Element("NAME").Value == itcapQuestion.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            itcapQuestion.Add(new XElement("ITCAPQUESTIONID", -1));

            capability.Element("ITCAPQUESTIONS").Add(itcapQuestion);

            changeLog.Add("ADD ITCAPQUESTION " + itcapQuestion.Element("NAME").Value + " " +
                            capability.Element("NAME").Value + " " +
                            domain.Element("NAME").Value);

            return true;
        }

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm)
        {
            XElement domainXML;
            if (!GetDomain(domName, out domainXML))
            {
                domainXML = new XElement("DOMAIN");
                domainXML.Add(new XElement("NAME", domName.Replace(' ', '~')));
                domainXML.Add(new XElement("DEFAULT", 'N'));
                if (!AddDomain(domainXML))
                {
                    MessageBox.Show("Failed to add Domain to File", "Error");
                    return;
                }
            }

            XElement capabilityXML;
            if (!GetCapability(capName, out capabilityXML))
            {
                capabilityXML = new XElement("CAPABILITY");
                capabilityXML.Add(new XElement("NAME", capName.Replace(' ', '~')));
                if (!AddCapability(capabilityXML, domainXML))
                {
                    MessageBox.Show("Failed to add Capability to File", "Error");
                    return;
                }
            }

            XElement itcapQuestionXML;
            if (!GetITCAPQuestion(itcqName, out itcapQuestionXML))
            {
                itcapQuestionXML = new XElement("ITCAPQUESTION");
                itcapQuestionXML.Add(new XElement("NAME", itcqName.Replace(' ', '~')));
                if (!AddITCAPQuestion(itcapQuestionXML, capabilityXML, domainXML))
                {
                    MessageBox.Show("Failed to add ITCAPQuestion to File", "Error");
                    return;
                }
            }

            XElement itcap = new XElement("ITCAP");
            itcap.Add(new XElement("ITCAPQUESTION", itcapQuestionXML.Element("NAME").Value));
            itcap.Add(new XElement("CAPABILITY", capabilityXML.Element("NAME").Value));
            itcap.Add(new XElement("DOMAIN", domainXML.Element("NAME").Value));


            if (!AddITCAP(itcap, itcapForm.client))
            {
                MessageBox.Show("Failed to add ITCAPQuestion to ITCAP", "Error");
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
                Domain domain = itcapForm.domains.Find(delegate(Domain dom)
                {
                    return dom.Name == domName;
                });
                if (domain == null)
                {
                    domain = new Domain();
                    domain.Name = domName;
                    domain.IsDefault = false;
                    domain.ID = itcapForm.domains.Count.ToString();
                    itcapForm.domains.Add(domain);
                    itcapForm.entities.Add(domain);
                    itcapForm.domainList.Items.Add(domain);
                }

                Capability capability = domain.CapabilitiesOwned.Find(delegate(Capability cap)
                {
                    return cap.Name == capName;
                });
                if (capability == null)
                {
                    capability = new Capability();
                    capability.Name = capName;
                    capability.IsDefault = false;
                    domain.CapabilitiesOwned.Add(capability);
                    domain.TotalChildren++;
                    itcapForm.capabilities.Add(capability);
                    capability.Owner = domain;
                    capability.ID = domain.CapabilitiesOwned.Count.ToString();
                    //LoadQuestions(cap);
                    itcapForm.entities.Add(capability);
                    itcapForm.capabilitiesList.Items.Add(capability);
                }

                ITCapQuestion itcqObject = capability.QuestionsOwned.Find(delegate(ITCapQuestion itcq)
                {
                    return itcq.Name == itcqName;
                });
                if (itcqObject == null)
                {
                    itcqObject = new ITCapQuestion();
                    itcqObject.Name = itcqName;
                    itcqObject.IsDefault = false;
                    itcqObject.AsIsScore = 0;
                    itcqObject.ToBeScore = 0;
                    capability.Owner.TotalChildren++;
                    capability.QuestionsOwned.Add(itcqObject);
                    itcqObject.Owner = capability;
                    itcqObject.ID = capability.QuestionsOwned.Count.ToString();
                    itcapForm.entities.Add(itcqObject);
                    itcapForm.questionList.Items.Add(itcqObject);
                }
                else
                {
                    MessageBox.Show("ITCAPQuestion already exists in ITCAP", "Error");
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