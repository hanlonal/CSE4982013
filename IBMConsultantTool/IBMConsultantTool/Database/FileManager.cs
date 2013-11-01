using System;
using System.Collections.Generic;
using System.IO;
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
                dbo = XElement.Load("Resources/Data.xml");
            }

            catch
            {
                dbo = new XElement("root");
                dbo.Add(new XElement("CLIENTS"));
                dbo.Add(new XElement("CATEGORIES"));
                dbo.Add(new XElement("DOMAINS"));
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }

                dbo.Save("Resources/Data.xml");
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
                    select ent.Element("NAME").Value).ToArray();
        }

        public bool GetClient(string cntName, out XElement client)
        {
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

        public bool AddClient(XElement client)
        {
            //If already in DB, return false
            if ((from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                 where ent.Element("NAME").Value == client.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            client.Add(new XElement("GROUPS"));
            client.Add(new XElement("BOMS"));

            dbo.Element("CLIENTS").Add(client);

            changeLog.Add("ADD CLIENT " + client.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override List<string> GetObjectivesFromClientBOM(object clientObj)
        {
            XElement client = clientObj as XElement;

            List<string> stringList = (from ent in client.Element("BOMS").Elements("BOM")
                                    select ent.Element("BUSINESSOBJECTIVE").Value).ToList();

            List<string> stringListNoRepeat = new List<string>();
            foreach (string busName in stringList)
            {
                if (!stringListNoRepeat.Contains(busName))
                {
                    stringListNoRepeat.Add(busName);
                }
            }

            return stringListNoRepeat;
        }
        #endregion

        #region Group
        //group is a keyword in C#
        public bool AddGroup(string grpName, XElement client)
        {
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

            grp.Add(new XElement("BOMS"));

            client.Element("GROUPS").Add(grp);

            changeLog.Add("ADD GROUP " + grp.Element("NAME").Value.Replace(' ', '~') + " " +
                            client.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }
        #endregion

        #region BOM
        public override bool UpdateBOM(object clientObj, NewInitiative ini)
        {
            XElement client = clientObj as XElement;
            try
            {
                XElement bom = (from ent in client.Element("BOMS").Elements("BOM")
                                where ent.Element("INITIATIVE").Value == ini.Name
                                select ent).Single();

                bom.Element("EFFECTIVENESS").Value = ini.Effectiveness.ToString();
                bom.Element("CRITICALITY").Value = ini.Criticality.ToString();
                bom.Element("DIFFERENTIAL").Value = ini.Differentiation.ToString();

                changeLog.Add("UPDATE BOM " + client.Element("NAME").Value.Replace(' ', '~') + " " + ini.Name.Replace(' ', '~') + " " +
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
            string iniXML = bom.Element("INITIATIVE").Value;
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value;
            string catXML = bom.Element("CATEGORY").Value;

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

            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            client.Element("BOMS").Add(bom);

            changeLog.Add("ADD BOM CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " + iniXML.Replace(' ', '~'));

            return true;
        }

        public override bool AddBOMToGroup(object bomObj, object groupObj)
        {
            XElement bom = bomObj as XElement;
            XElement grp = groupObj as XElement;
            string iniXML = bom.Element("INITIATIVE").Value;
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value;
            string catXML = bom.Element("CATEGORY").Value;

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

            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            grp.Element("BOMS").Add(bom);

            XElement client = grp.Parent.Parent;

            changeLog.Add("ADD BOM GROUP " + grp.Element("NAME").Value.Replace(' ', '~') + " " +
                          client.Element("NAME").Value.Replace(' ', '~') + " " + iniXML.Replace(' ', '~'));

            return true;
        }

        public override bool AddBOMToContact(object bomObj, object contactObj)
        {
            XElement bom = bomObj as XElement;
            XElement contact = contactObj as XElement;
            string iniXML = bom.Element("INITIATIVE").Value;
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value;
            string catXML = bom.Element("CATEGORY").Value;

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

            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            contact.Element("BOMS").Add(bom);

            XElement grp = contact.Parent.Parent;
            XElement client = grp.Parent.Parent;

            changeLog.Add("ADD BOM CONTACT " + contact.Element("NAME").Value.Replace(' ', '~') + " " +
                          grp.Element("NAME").Value.Replace(' ', '~') + " " +
                          client.Element("NAME").Value.Replace(' ', '~') + " " + iniXML.Replace(' ', '~'));

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
                    catName = bom.Element("CATEGORY").Value.TrimEnd();
                    category = bomForm.Categories.Find(delegate(NewCategory cat)
                    {
                        return cat.name == catName;
                    });
                    if (category == null)
                    {
                        category = bomForm.AddCategory(catName);
                    }

                    busName = bom.Element("BUSINESSOBJECTIVE").Value.TrimEnd();
                    objective = category.Objectives.Find(delegate(NewObjective bus)
                    {
                        return bus.Name == busName;
                    });
                    if (objective == null)
                    {
                        objective = category.AddObjective(busName);
                    }

                    iniName = bom.Element("INITIATIVE").Value.TrimEnd();
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

        public override bool RemoveITCAP(string itcqName, object clientObj)
        {
            XElement itcap;
            XElement client = clientObj as XElement;

            if (GetITCAP(itcqName, client, out itcap))
            {
                itcap.RemoveAll();
                return true;
            }

            return true;
        }

        public override bool UpdateITCAP(object clientObj, ITCapQuestion itcapQuestion)
        {
            XElement client = clientObj as XElement;
            string formattedName = itcapQuestion.Name;
            try
            {
                XElement itcap = (from ent in client.Element("ITCAPS").Elements("ITCAP")
                                where ent.Element("ITCAPQUESTION").Value == formattedName
                                select ent).Single();

                itcap.Element("ASIS").Value = itcapQuestion.AsIsScore.ToString();
                itcap.Element("TOBE").Value = itcapQuestion.ToBeScore.ToString();

                changeLog.Add("UPDATE ITCAP " + client.Element("NAME").Value.Replace(' ', '~') + " " + formattedName.Replace(' ', '~') + " " +
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

        public override bool AddITCAP(object itcapObj, object clientObj)
        {
            XElement itcap = itcapObj as XElement;
            XElement client = clientObj as XElement;
            string itcqXML = itcap.Element("ITCAPQUESTION").Value;
            string capXML = itcap.Element("CAPABILITY").Value;
            string domXML = itcap.Element("DOMAIN").Value;

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

            itcap.Add(new XElement("ASIS", 0));
            itcap.Add(new XElement("TOBE", 0));
            itcap.Add(new XElement("COMMENT", ""));

            client.Element("ITCAPS").Add(itcap);

            changeLog.Add("ADD ITCAP CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " + itcqXML.Replace(' ', '~'));

            return true;
        }

        public override bool AddITCAPToGroup(object itcapObj, object groupObj)
        {
            XElement itcap = itcapObj as XElement;
            XElement grp = groupObj as XElement;
            string itcqXML = itcap.Element("ITCAPQUESTION").Value;
            string capXML = itcap.Element("CAPABILITY").Value;
            string domXML = itcap.Element("DOMAIN").Value;

            List<XElement> itcapList = grp.Element("ITCAPS").Elements("ITCAP").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in itcapList
                 where ent != null &&
                       ent.Element("ITCAPQUESTION") != null &&
                       ent.Element("ITCAPQUESTION").Value == itcqXML
                 select ent).Count() != 0)
            {
                return false;
            }

            itcap.Add(new XElement("ASIS", 0));
            itcap.Add(new XElement("TOBE", 0));
            itcap.Add(new XElement("COMMENT", ""));

            grp.Element("ITCAPS").Add(itcap);

            changeLog.Add("ADD ITCAP GROUP " + grp.Element("NAME").Value.Replace(' ', '~') + " " + itcqXML.Replace(' ', '~'));

            return true;
        }

        public override bool AddITCAPToContact(object itcapObj, object contactObj)
        {
            XElement itcap = itcapObj as XElement;
            XElement contact = contactObj as XElement;
            string itcqXML = itcap.Element("ITCAPQUESTION").Value;
            string capXML = itcap.Element("CAPABILITY").Value;
            string domXML = itcap.Element("DOMAIN").Value;

            List<XElement> itcapList = contact.Element("ITCAPS").Elements("ITCAP").ToList();
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in itcapList
                 where ent != null &&
                       ent.Element("ITCAPQUESTION") != null &&
                       ent.Element("ITCAPQUESTION").Value == itcqXML
                 select ent).Count() != 0)
            {
                return false;
            }

            itcap.Add(new XElement("ASIS", 0));
            itcap.Add(new XElement("TOBE", 0));
            itcap.Add(new XElement("COMMENT", ""));

            contact.Element("ITCAPS").Add(itcap);

            changeLog.Add("ADD ITCAP CONTACT " + contact.Element("NAME").Value.Replace(' ', '~') + " " + itcqXML.Replace(' ', '~'));

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
                itcqName = itcap.Element("ITCAPQUESTION").Value;
                capName = itcap.Element("CAPABILITY").Value;
                domName = itcap.Element("DOMAIN").Value;

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
                        MessageBox.Show("Error: Could not find domain related to this ITCAP Entry", "Error");
                        break;
                    }
                    domain.IsDefault = domEnt.Element("DEFAULT").Value == "Y";
                    domain.Type = "domain";
                    domain.Visible = true;
                    itcapForm.domains.Add(domain);
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
                        MessageBox.Show("Error: Could not find capability related to this ITCAP Entry", "Error");
                        break;
                    }
                    capability.IsDefault = capEnt.Element("DEFAULT").Value == "Y";
                    domain.CapabilitiesOwned.Add(capability);
                    domain.TotalChildren++;
                    capability.Type = "capability";
                    itcapForm.capabilities.Add(capability);
                    capability.Owner = domain;
                    capability.ID = domain.CapabilitiesOwned.Count.ToString();
                    //LoadQuestions(cap);
                }

                itcapQuestion = new ITCapQuestion();
                itcapQuestion.Name = itcqName;
                if (!GetITCAPQuestion(itcqName, out itcqEnt))
                {
                    MessageBox.Show("Error: Could not find question related to this ITCAP Entry", "Error");
                    break;
                }
                itcapQuestion.IsDefault = itcqEnt.Element("DEFAULT").Value == "Y";
                itcapQuestion.AsIsScore = Convert.ToSingle(itcap.Element("ASIS").Value);
                itcapQuestion.ToBeScore = Convert.ToSingle(itcap.Element("TOBE").Value);
                capability.Owner.TotalChildren++;
                itcapQuestion.Type = "attribute";
                capability.Owner.TotalChildren++;
                capability.QuestionsOwned.Add(itcapQuestion);
                itcapQuestion.Owner = capability;
                itcapQuestion.ID = capability.QuestionsOwned.Count.ToString();
            }

            foreach (Domain domain in itcapForm.domains)
            {
                itcapForm.entities.Add(domain);
                foreach (Capability capability in domain.CapabilitiesOwned)
                {
                    itcapForm.entities.Add(capability);
                    foreach (ITCapQuestion itcapQuestion in capability.QuestionsOwned)
                    {
                        itcapForm.entities.Add(itcapQuestion);
                    }
                }
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
                    select ent.Element("NAME").Value).ToArray();
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

            category.Add(new XElement("BUSINESSOBJECTIVES"));

            dbo.Element("CATEGORIES").Add(category);

            changeLog.Add("ADD CATEGORY " + category.Element("NAME").Value.Replace(' ', '~'));

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
                                                       select ent.Element("NAME").Value).ToArray());
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

            objective.Add(new XElement("INITIATIVES"));

            category.Element("BUSINESSOBJECTIVES").Add(objective);

            changeLog.Add("ADD BUSINESSOBJECTIVE " + objective.Element("NAME").Value.Replace(' ', '~') + " " +
                            category.Element("NAME").Value.Replace(' ', '~'));

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
                                                        select ent.Element("NAME").Value).ToArray());
            }
        }

        #endregion

        #region Initiative
        public bool GetInitiative(string iniName, out XElement Initiative)
        {
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

            objective.Element("INITIATIVES").Add(initiative);

            changeLog.Add("ADD INITIATIVE " + initiative.Element("NAME").Value.Replace(' ', '~') + " " +
                            objective.Element("NAME").Value.Replace(' ', '~') + " " +
                            category.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override bool AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
        {
            XElement categoryXML;
            if (!GetCategory(catName, out categoryXML))
            {
                categoryXML = new XElement("CATEGORY");
                categoryXML.Add(new XElement("NAME", catName));
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
                objectiveXML.Add(new XElement("NAME", busName));
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
                initiativeXML.Add(new XElement("NAME", iniName));
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

        #region CUPEQuestion
        public override List<CupeQuestionStringData> GetCUPEQuestions()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                      select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionsTwenty()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  where ent.Element("INTWENTY").Value == "Y"
                                                  select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionsFifteen()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  where ent.Element("INFIFTEEN").Value == "Y"
                                                  select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionsTen()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  where ent.Element("INTEN").Value == "Y"
                                                  select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override bool AddCupeQuestion(CupeQuestionStringData cupeQuestion)
        {
            string question = cupeQuestion.QuestionText;
            if ((from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                 where ent.Element("NAME").Value == question
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding question: Question already exists", "Error");
                return false;
            }

            string commodity = cupeQuestion.ChoiceA;
            string utility = cupeQuestion.ChoiceB;
            string partner = cupeQuestion.ChoiceC;
            string enabler = cupeQuestion.ChoiceD;

            XElement cupeQuestionEnt = new XElement("CUPEQUESTION");
            cupeQuestionEnt.Add(new XElement("NAME", question));
            cupeQuestionEnt.Add(new XElement("COMMODITY", commodity));
            cupeQuestionEnt.Add(new XElement("UTILITY", utility));
            cupeQuestionEnt.Add(new XElement("PARTNER", partner));
            cupeQuestionEnt.Add(new XElement("ENABLER", enabler));
            cupeQuestionEnt.Add(new XElement("INTWENTY", "N"));
            cupeQuestionEnt.Add(new XElement("INFIFTEEN", "N"));
            cupeQuestionEnt.Add(new XElement("INTEN", "N"));

            dbo.Add(cupeQuestionEnt);

            changeLog.Add("ADD CUPEQUESTION " + question.Replace(' ', '~') + " " +
                          commodity.Replace(' ', '~') + " " + utility.Replace(' ', '~') + " " +
                          partner.Replace(' ', '~') + " " + enabler.Replace(' ', '~'));

            return true;
        }
        public override bool UpdateCupeQuestion(string cupeQuestion, bool inTwenty, bool inFifteen, bool inTen)
        {
            XElement cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                   where ent.Element("NAME").Value == cupeQuestion
                                   select ent).Single();

                string inTwentyStr = inTwenty ? "Y" : "N";
                string inFifteenStr = inFifteen? "Y" : "N";
                string inTenStr = inTen ? "Y" : "N";

                cupeQuestionEnt.Element("INTWENTY").Value = inTwentyStr;
                cupeQuestionEnt.Element("INFIFTEEN").Value = inFifteenStr;
                cupeQuestionEnt.Element("INTEN").Value = inTenStr;

                changeLog.Add("UPDATE CUPEQUESTION " + inTwentyStr.Replace(' ', '~') + " " +
                          inFifteenStr.Replace(' ', '~') + " " + inTenStr.Replace(' ', '~'));
            }

            catch (Exception e)
            {
                MessageBox.Show("Error updating CupeQuestion\n\n" + e.Message, "Error");
                return false;
            }

            return true;
        }
        #endregion

        #region CUPE
        public override bool UpdateCUPE(object clientObj, string cupeQuestion, string current, string future)
        {
            XElement client = clientObj as XElement;
            try
            {
                XElement cupe = (from ent in client.Element("CUPES").Elements("CUPE")
                             where ent.Element("NAME").Value == cupeQuestion
                             select ent).Single();
                cupe.Element("CURRENT").Value = current;
                cupe.Element("FUTURE").Value = future;

                changeLog.Add("UPDATE CUPE " + cupeQuestion.Replace(' ', '~') + " " +
                          current + " " + future);
            }

            catch
            {
                return false;
            }


            return true;   
        }
        public override bool AddCUPE(string question, object clientObj)
        {
            XElement client = clientObj as XElement;
            XElement cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                   where ent.Element("NAME").Value == question
                                   select ent).Single();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error adding Cupe:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in client.Element("CUPES").Elements("CUPE")
                 where ent.Element("CUPEQUESTION").Value == question
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            XElement cupe = new XElement("CUPE");
            cupe.Add(new XElement("CUPEQUESTION", question));
            cupe.Add(new XElement("NAME", question));
            cupe.Add(new XElement("COMMODITY", cupeQuestionEnt.Element("COMMODITY").Value));
            cupe.Add(new XElement("UTILITY", cupeQuestionEnt.Element("UTILITY").Value));
            cupe.Add(new XElement("PARTNER", cupeQuestionEnt.Element("PARTNER").Value));
            cupe.Add(new XElement("ENABLER", cupeQuestionEnt.Element("ENABLER").Value));
            cupe.Add(new XElement("CURRENT", "A"));
            cupe.Add(new XElement("FUTURE", "A"));
            client.Add(cupe);

            changeLog.Add("ADD CUPE CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " + question.Replace(' ', '~'));

            return true;
        }
        public override bool AddCUPEToGroup(string question, object groupObj)
        {
            XElement grp = groupObj as XElement;
            XElement cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                   where ent.Element("NAME").Value == question
                                   select ent).Single();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error adding Cupe:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in grp.Element("CUPES").Elements("CUPE")
                 where ent.Element("CUPEQUESTION").Value == question
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            XElement cupe = new XElement("CUPE");
            cupe.Add(new XElement("CUPEQUESTION", question));
            cupe.Add(new XElement("NAME", question));
            cupe.Add(new XElement("COMMODITY", cupeQuestionEnt.Element("COMMODITY").Value));
            cupe.Add(new XElement("UTILITY", cupeQuestionEnt.Element("UTILITY").Value));
            cupe.Add(new XElement("PARTNER", cupeQuestionEnt.Element("PARTNER").Value));
            cupe.Add(new XElement("ENABLER", cupeQuestionEnt.Element("ENABLER").Value));
            cupe.Add(new XElement("CURRENT", "A"));
            cupe.Add(new XElement("FUTURE", "A"));
            grp.Add(cupe);

            changeLog.Add("ADD CUPE GROUP " + grp.Element("NAME").Value.Replace(' ', '~') + " " + question.Replace(' ', '~'));

            return true;
        }
        public override bool AddCUPEToContact(string question, object contactObj)
        {
            XElement contact = contactObj as XElement;
            XElement cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                   where ent.Element("NAME").Value == question
                                   select ent).Single();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error adding Cupe:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in contact.Element("CUPES").Elements("CUPE")
                 where ent.Element("CUPEQUESTION").Value == question
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            XElement cupe = new XElement("CUPE");
            cupe.Add(new XElement("CUPEQUESTION", question));
            cupe.Add(new XElement("NAME", question));
            cupe.Add(new XElement("COMMODITY", cupeQuestionEnt.Element("COMMODITY").Value));
            cupe.Add(new XElement("UTILITY", cupeQuestionEnt.Element("UTILITY").Value));
            cupe.Add(new XElement("PARTNER", cupeQuestionEnt.Element("PARTNER").Value));
            cupe.Add(new XElement("ENABLER", cupeQuestionEnt.Element("ENABLER").Value));
            cupe.Add(new XElement("CURRENT", "A"));
            cupe.Add(new XElement("FUTURE", "A"));
            contact.Add(cupe);

            changeLog.Add("ADD CUPE CONTACT " + contact.Element("NAME").Value.Replace(' ', '~') + " " + question.Replace(' ', '~'));

            return true;
        }
        public override bool BuildCUPEForm(CUPETool cupeForm, string clientName)
        {
            XElement client;

            if (GetClient(clientName, out client))
            {
                cupeForm.client = client;

                return true;
            }

            else
            {
                MessageBox.Show("Client could not be found", "Error");
                return false;
            }
        }
        public override bool NewCUPEForm(CUPETool cupeForm, string clientName)
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

                if (!AddGroup("Business", client))
                {
                    MessageBox.Show("Failed to add group to client", "Error");
                    return false;
                }

                if (!AddGroup("IT", client))
                {
                    MessageBox.Show("Failed to add group to client", "Error");
                    return false;
                }

                if (!SaveChanges())
                {
                    MessageBox.Show("Failed to save changes to database", "Error");
                    return false;
                }

                cupeForm.client = client;

                return true;
            }
            else
            {
                MessageBox.Show("Client already exists", "Error");
                return false;
            }
        }
        public override void PopulateCUPEQuestionsForClient(CUPETool cupeForm)
        {
            XElement client = cupeForm.client as XElement;
            CupeQuestionStringData data = new CupeQuestionStringData();
            List<XElement> cupeList = client.Element("CUPES").Elements("CUPE").ToList();
            if (cupeList.Count != 0)
            {
                foreach (XElement cupe in cupeList)
                {
                    data.QuestionText = cupe.Element("NAME").Value;
                    data.ChoiceA = cupe.Element("COMMODITY").Value;
                    data.ChoiceB = cupe.Element("UTILITY").Value;
                    data.ChoiceC = cupe.Element("PARTNER").Value;
                    data.ChoiceD = cupe.Element("ENABLER").Value;

                    ClientDataControl.AddCupeQuestion(data);
                    data = new CupeQuestionStringData();
                }
            }

            else
            {
                List<XElement> cupeQuestionList = dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION").ToList();
                foreach (XElement cupeQuestion in cupeQuestionList)
                {
                    if (cupeQuestion.Element("INTWENTY").Value == "Y")
                    {
                        data.QuestionText = cupeQuestion.Element("NAME").Value;
                        data.ChoiceA = cupeQuestion.Element("COMMODITY").Value;
                        data.ChoiceB = cupeQuestion.Element("UTILITY").Value;
                        data.ChoiceC = cupeQuestion.Element("PARTNER").Value;
                        data.ChoiceD = cupeQuestion.Element("ENABLER").Value;

                        ClientDataControl.AddCupeQuestion(data);
                        data = new CupeQuestionStringData();
                    }
                }
            }
        }
        #endregion

        #region Domain
        public override string[] GetDomainNames()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    select ent.Element("NAME").Value).ToArray();
        }

        public override string[] GetDomainNamesAndDefault()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    select ent.Element("NAME").Value + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultDomainNames()
        {
            return (from ent in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value).ToArray();
        }

        public bool GetDomain(string domName, out XElement domain)
        {
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

            domain.Add(new XElement("DEFAULT", "N"));
            domain.Add(new XElement("CAPABILITIES"));

            dbo.Element("DOMAINS").Add(domain);

            changeLog.Add("ADD DOMAIN " + domain.Element("NAME").Value.Replace(' ', '~'));

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
                                                       select ent.Element("NAME").Value).ToArray());
            }
        }

        public override bool ChangeDomainDefault(string domName, bool isDefault)
        {
            XElement domain;
            if (GetDomain(domName, out domain))
            {
                domain.Element("DEFAULT").Value = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find Domain: " + domName, "Error");
            return false;
        }
        #endregion

        #region Capability

        public override string[] GetCapabilityNames(string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    select ent.Element("NAME").Value).ToArray();
        }

        public override string[] GetCapabilityNamesAndDefault(string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    select ent.Element("NAME").Value + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultCapabilityNames(string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from ent in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value).ToArray();
        }

        public bool GetCapability(string capName, out XElement capability)
        {
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

            capability.Add(new XElement("DEFAULT", "N"));
            capability.Add(new XElement("ITCAPQUESTIONS"));

            domain.Element("CAPABILITIES").Add(capability);

            changeLog.Add("ADD CAPABILITY " + capability.Element("NAME").Value.Replace(' ', '~') + " " +
                            domain.Element("NAME").Value.Replace(' ', '~'));

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
                                                       select ent.Element("NAME").Value).ToArray());
            }
        }

        public override bool ChangeCapabilityDefault(string capName, bool isDefault)
        {
            XElement capability;
            if (GetCapability(capName, out capability))
            {
                capability.Element("DEFAULT").Value = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find Capability: " + capName, "Error");
            return false;
        }
        #endregion

        #region ITCAPQuestion

        public bool GetITCAP(string itcqName, XElement client, out XElement itcap)
        {
            try
            {
                itcap = (from ent in client.Element("ITCAPS").Elements("ITCAP")
                         where ent.Element("ITCAPQUESTION").Value == itcqName
                         select ent).Single();
            }

            catch
            {
                itcap = null;
                return false;
            }

            return true;
        }

        public override string[] GetITCAPQuestionNames(string capName, string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    select ent.Element("NAME").Value).ToArray();
        }

        public override string[] GetITCAPQuestionNamesAndDefault(string capName, string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    select ent.Element("NAME").Value + ent.Element("DEFAULT").Value).ToArray();
        }

        public override string[] GetDefaultITCAPQuestionNames(string capName, string domName)
        {
            return (from dom in dbo.Element("DOMAINS").Elements("DOMAIN")
                    where dom.Element("NAME").Value == domName
                    from cap in dom.Element("CAPABILITIES").Elements("CAPABILITY")
                    where cap.Element("NAME").Value == capName
                    from ent in cap.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION")
                    where ent.Element("DEFAULT").Value == "Y"
                    select ent.Element("NAME").Value).ToArray();
        }

        public bool GetITCAPQuestion(string itcapName, out XElement itcapQuestion)
        {
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

            itcapQuestion.Add(new XElement("DEFAULT", "N"));

            capability.Element("ITCAPQUESTIONS").Add(itcapQuestion);

            changeLog.Add("ADD ITCAPQUESTION " + itcapQuestion.Element("NAME").Value.Replace(' ', '~') + " " +
                            capability.Element("NAME").Value.Replace(' ', '~') + " " +
                            domain.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm)
        {
            XElement domainXML;
            if (!GetDomain(domName, out domainXML))
            {
                domainXML = new XElement("DOMAIN");
                domainXML.Add(new XElement("NAME", domName));
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
                capabilityXML.Add(new XElement("NAME", capName));
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
                itcapQuestionXML.Add(new XElement("NAME", itcqName));
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
        public override void RemoveQuestionToITCAP(string itcqName)
        {
            throw new NotImplementedException();
        }

        public override bool ChangeITCAPQuestionDefault(string itcqName, bool isDefault)
        {
            XElement itcapQuestion;
            if (GetITCAPQuestion(itcqName, out itcapQuestion))
            {
                itcapQuestion.Element("DEFAULT").Value = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find ITCAPQuestion: " + itcqName, "Error");
            return false;
        }

        #endregion

        #region ITCAPOBJMAP
        public override bool GetITCAPOBJMAPScore(object clientObj, string capName, string busName, out int score)
        {
            XElement client = clientObj as XElement;
            try
            {
                score = Convert.ToInt32((from ent in client.Element("ITCAPOBJMAP").Elements("ITCAPOBJMAPS")
                                         where ent.Element("CAPABILITY").Value == capName &&
                                         ent.Element("BUSINESSOBJECTIVE").Value == busName
                                         select ent.Element("SCORE").Value).Single());
            }

            catch
            {
                score = -1;
                return false;
            }
            return true;
        }

        public override bool AddITCAPOBJMAP(object clientObj, string capName, string busName)
        {
            XElement client = clientObj as XElement;

            if ((from ent in client.Element("ITCAPOBJMAP").Elements("ITCAPOBJMAPS")
                 where ent.Element("CAPABILITY").Value == capName &&
                       ent.Element("BUSINESSOBJECTIVE").Value == busName
                 select ent).Count() == 0)
            {
                XElement itcapObjMap = new XElement("ITCAPOBJMAP");
                XElement capability;
                XElement objective;

                if (!GetCapability(capName, out capability))
                {
                    MessageBox.Show("Could not create mapping: Capability not found", "Error");
                    return false;
                }

                itcapObjMap.Add(new XElement("CAPABILITY", capName));

                if (!GetObjective(busName, out objective))
                {
                    MessageBox.Show("Could not create mapping: Objective not found", "Error");
                    return false;
                }

                itcapObjMap.Add(new XElement("BUSINESSOBJECTIVE", busName));

                itcapObjMap.Add(new XElement("SCORE", 0));

                client.Add(itcapObjMap);

                changeLog.Add("ADD ITCAPOBJMAP " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                               capName.Replace(' ', '~') + " " + busName.Replace(' ', '~'));
            }

            else
            {
                MessageBox.Show("Could not create mapping: Mapping already exists", "Error");
                return false;
            }

            return true;
        }

        public override bool UpdateITCAPOBJMAPScore(object clientObj, string capName, string busName, int score)
        {
            XElement client = clientObj as XElement;

            XElement itcapObjMap;
            try
            {
                itcapObjMap = (from ent in client.Element("ITCAPOBJMAP").Elements("ITCAPOBJMAPS")
                               where ent.Element("CAPABILITY").Value == capName &&
                                     ent.Element("BUSINESSOBJECTIVE").Value == busName
                               select ent).Single();

                itcapObjMap.Element("SCORE").Value = score.ToString();

                changeLog.Add("UPDATE ITCAPOBJMAP " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                              capName.Replace(' ', '~') + " " + busName.Replace(' ', '~') + " " + score.ToString());
            }

            catch (Exception e)
            {
                MessageBox.Show("Could not add Capability/Objective Mapping\n\n" + e.Message, "Error");
                return false;
            }

            return true;
        }
        #endregion

        #region General

        public override bool SaveChanges()
        {
            try
            {
                if(!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }

                dbo.Save("Resources/Data.xml");

                if (!File.Exists("Resources/Changes.log"))
                {
                    File.Create("Resources/Changes.log");
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Resources/Changes.log", true))
                {
                    foreach (string change in changeLog)
                    {
                        file.WriteLine(change);
                    }
                }

                changeLog.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Save Changes Failed:\n\n" + e.Message + e.InnerException, "Error");
                return false;
            }

            return true;
        }

        #endregion
    }
}