using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public class DBManager : DataManager
    {
        public SAMPLEEntities dbo;

        public DBManager()
        {
            dbo = new SAMPLEEntities();
            CheckChangeLog();
            UpdateDataFile();
        }

        #region Client
        public List<CLIENT> GetClients()
        {
            return (from ent in dbo.CLIENT
                    select ent).ToList();
        }

        public override string[] GetClientNames()
        {
            return (from ent in dbo.CLIENT
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetClient(string cntName, out CLIENT client)
        {
            try
            {
                client = (from ent in dbo.CLIENT
                          where ent.NAME.TrimEnd() == cntName
                          select ent).Single();
            }

            catch
            {
                client = null;
                return false;
            }

            return true;
        }

        public bool GetClient(int cntID, out CLIENT client)
        {
            try
            {
                client = (from ent in dbo.CLIENT
                          where ent.CLIENTID == cntID
                          select ent).Single();
            }

            catch
            {
                client = null;
                return false;
            }

            return true;
        }

        public bool AddClient(CLIENT client)
        {
            //If already in DB, return false
            if ((from ent in dbo.CLIENT
                 where ent.NAME.TrimEnd() == client.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.CLIENT
                                select ent.CLIENTID).ToList();

            client.CLIENTID = GetUniqueID(idList);

            dbo.AddToCLIENT(client);

            return true;
        }
        #endregion

        #region Group
        //group is a keyword in C#
        public List<GROUP> GetGroups()
        {
            return (from ent in dbo.GROUP
                    select ent).ToList();
        }

        public bool GetGroup(string grpName, CLIENT client, out GROUP grp)
        {
            try
            {
                grp = (from ent in client.GROUP
                       where ent.NAME.TrimEnd() == grpName
                       select ent).Single();
            }

            catch
            {
                grp = null;
                return false;
            }

            return true;
        }
        public bool AddGroup(string grpName, CLIENT client)
        {
            if ((from ent in client.GROUP
                 where ent.NAME.TrimEnd() == grpName
                 select ent).Count() != 0)
            {
                return false;
            }

            GROUP grp = new GROUP();
            grp.NAME = grpName;
            grp.CLIENT = client;

            List<int> idList = (from ent in dbo.GROUP
                                select ent.GROUPID).ToList();

            grp.GROUPID = GetUniqueID(idList);

            dbo.AddToGROUP(grp);

            return true;
        }
        public bool AddGroups(string[] grpNames, CLIENT client)
        {
            List<int> idList = (from ent in dbo.GROUP
                                select ent.GROUPID).ToList();
            foreach (string grpName in grpNames)
            {
                if ((from ent in client.GROUP
                     where ent.NAME.TrimEnd() == grpName
                     select ent).Count() != 0)
                {
                    return false;
                }

                GROUP grp = new GROUP();
                grp.NAME = grpName;
                grp.CLIENT = client;

                int uniqueID = GetUniqueID(idList);
                grp.GROUPID = uniqueID;
                idList.Add(uniqueID);

                dbo.AddToGROUP(grp);
            }

            return true;
        }
        #endregion

        #region Contact

        public bool GetContact(string contactName, GROUP grp, out CONTACT contact)
        {
            try
            {
                contact = (from ent in grp.CONTACT
                       where ent.NAME.TrimEnd() == contactName
                       select ent).Single();
            }

            catch
            {
                contact = null;
                return false;
            }

            return true;
        }
        public bool AddContact(string contactName, GROUP grp)
        {
            if ((from ent in grp.CONTACT
                 where ent.NAME.TrimEnd() == contactName
                 select ent).Count() != 0)
            {
                return false;
            }

            CONTACT contact = new CONTACT();
            contact.NAME = contactName;
            contact.GROUP = grp;

            List<int> idList = (from ent in dbo.CONTACT
                                select ent.CONTACTID).ToList();

            contact.CONTACTID = GetUniqueID(idList);

            dbo.AddToCONTACT(contact);

            return true;
        }
        #endregion

        #region BOM
        public List<BOM> GetBOMs()
        {
            return (from ent in dbo.BOM
                    select ent).ToList();
        }

        public bool GetBOM(string iniName, CLIENT client, out BOM bom)
        {
            try
            {
                bom = (from ent in client.BOM
                       where ent.INITIATIVE.NAME.TrimEnd() == iniName
                       select ent).Single();
            }

            catch
            {
                bom = null;
                return false;
            }

            return true;
        }

        public override bool UpdateBOM(object clientObj, NewInitiative ini)
        {
            CLIENT client = clientObj as CLIENT;
            try
            {
                BOM bom = (from ent in client.BOM
                           where ent.INITIATIVE.NAME.TrimEnd() == ini.Name
                           select ent).Single();

                bom.EFFECTIVENESS = ini.Effectiveness;
                bom.CRITICALITY = ini.Criticality;
                bom.DIFFERENTIAL = ini.Differentiation;
            }

            catch
            {
                return false;
            }

            
            return true;
        }

        public override bool AddBOM(object bomObj, object clientObj)
        {
            BOM bom = bomObj as BOM;
            CLIENT client = clientObj as CLIENT;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in client.BOM
                 where ent.INITIATIVE.NAME.TrimEnd() == bom.INITIATIVE.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.BOM
                                select ent.BOMID).ToList();

            bom.BOMID = GetUniqueID(idList);
            bom.CLIENT = client;

            dbo.AddToBOM(bom);

            return true;
        }

        public override bool AddBOMToGroup(object bomObj, object groupObj)
        {
            BOM bom = bomObj as BOM;
            GROUP grp = groupObj as GROUP;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in grp.BOM
                 where ent.INITIATIVE.NAME.TrimEnd() == bom.INITIATIVE.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.BOM
                                select ent.BOMID).ToList();

            bom.BOMID = GetUniqueID(idList);
            bom.GROUP = grp;

            dbo.AddToBOM(bom);

            return true;
        }

        public override bool AddBOMToContact(object bomObj, object contactObj)
        {
            BOM bom = bomObj as BOM;
            CONTACT contact = contactObj as CONTACT;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in contact.BOM
                 where ent.INITIATIVE.NAME.TrimEnd() == bom.INITIATIVE.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.BOM
                                select ent.BOMID).ToList();

            bom.BOMID = GetUniqueID(idList);
            bom.CONTACT = contact;

            dbo.AddToBOM(bom);

            return true;
        }

        public override bool BuildBOMForm(BOMTool bomForm, string clientName)
        {
            CLIENT client;

            if (GetClient(clientName, out client))
            {
                bomForm.client = client;

                string catName;
                string busName;
                string iniName;

                NewCategory category;
                NewObjective objective;
                NewInitiative initiative;

                foreach (BOM bom in client.BOM)
                {
                    catName = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                    category = bomForm.Categories.Find(delegate(NewCategory cat)
                    {
                        return cat.name == catName;
                    });
                    if (category == null)
                    {
                        category = bomForm.AddCategory(catName);
                    }

                    busName = bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                    objective = category.Objectives.Find(delegate(NewObjective bus)
                    {
                        return bus.Name == busName;
                    });
                    if (objective == null)
                    {
                        objective = category.AddObjective(busName);
                    }

                    iniName = bom.INITIATIVE.NAME.TrimEnd();
                    initiative = objective.Initiatives.Find(delegate(NewInitiative ini)
                    {
                        return ini.Name == iniName;
                    });
                    if (initiative == null)
                    {
                        initiative = objective.AddInitiative(iniName);
                        initiative.Effectiveness = bom.EFFECTIVENESS.HasValue ? bom.EFFECTIVENESS.Value : 0;
                        initiative.Criticality = bom.CRITICALITY.HasValue ? bom.CRITICALITY.Value : 0;
                        initiative.Differentiation = bom.DIFFERENTIAL.HasValue ? bom.DIFFERENTIAL.Value : 0;
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
            CLIENT client;
            if (!GetClient(clientName, out client))
            {
                client = new CLIENT();
                client.NAME = clientName;
                if (!AddClient(client))
                {
                    MessageBox.Show("Failed to add client", "Error");
                    return false;
                }

                string[] groupNamesArray = {"Business", "IT"};

                if (!AddGroups(groupNamesArray, client))
                {
                    MessageBox.Show("Failed to add groups to client", "Error");
                    return false;
                }

                if (!SaveChanges())
                {
                    MessageBox.Show("Failed to save changes to database", "Error");
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
            CLIENT client = clientObj as CLIENT;
            try
            {
                ITCAP itcap = (from ent in client.ITCAP
                           where ent.ITCAPQUESTION.NAME.TrimEnd() == itcapQuestion.Name
                           select ent).Single();

                itcap.ASIS = itcapQuestion.AsIsScore;
                itcap.TOBE = itcapQuestion.ToBeScore;
            }

            catch
            {
                return false;
            }


            return true;
        }

        public override bool BuildITCAPForm(ITCapTool itcapForm, string clientName)
        {
            CLIENT client;

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

        public override bool AddITCAP(object itcqObject, object clientObj)
        {
            ITCAP itcap = itcqObject as ITCAP;
            CLIENT client = clientObj as CLIENT;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in client.ITCAP
                 where ent.ITCAPQUESTION.NAME.TrimEnd() == itcap.ITCAPQUESTION.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.ITCAP
                                select ent.ITCAPID).ToList();

            itcap.ITCAPID = GetUniqueID(idList);
            itcap.CLIENT = client;

            dbo.AddToITCAP(itcap);

            return true;
        }

        public override bool NewITCAPForm(ITCapTool itcapForm, string clientName)
        {
            CLIENT client;
            if (!GetClient(clientName, out client))
            {
                client = new CLIENT();
                client.NAME = clientName;
                if (!AddClient(client))
                {
                    MessageBox.Show("Failed to add client", "Error");
                    return false;
                }

                string[] groupNamesArray = { "Business", "IT" };

                if (!AddGroups(groupNamesArray, client))
                {
                    MessageBox.Show("Failed to add groups to client", "Error");
                    return false;
                }

                if (!SaveChanges())
                {
                    MessageBox.Show("Failed to save changes to database", "Error");
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

            List<ITCAP> itcapList = (itcapForm.client as CLIENT).ITCAP.ToList();

            ITCAPQUESTION itcqEnt;
            CAPABILITY capEnt;
            DOMAIN domEnt;

            string itcqName;
            string capName;
            string domName;

            foreach (ITCAP itcap in itcapList)
            {
                itcqEnt = itcap.ITCAPQUESTION;
                capEnt = itcqEnt.CAPABILITY;
                domEnt = capEnt.DOMAIN;

                itcqName = itcqEnt.NAME.TrimEnd();
                capName = capEnt.NAME.TrimEnd();
                domName = domEnt.NAME.TrimEnd();

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
                    domain.IsDefault = domEnt.DEFAULT == "Y";
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
                    capability.IsDefault = capEnt.DEFAULT == "Y";
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
                itcapQuestion.IsDefault = itcqEnt.DEFAULT == "Y";
                itcapQuestion.AsIsScore = itcap.ASIS.HasValue ? itcap.ASIS.Value : 0;
                itcapQuestion.ToBeScore = itcap.TOBE.HasValue ? itcap.TOBE.Value : 0;
                itcapQuestion.comment = itcap.COMMENT;
                capability.Owner.TotalChildren++;
                capability.QuestionsOwned.Add(itcapQuestion);
                itcapQuestion.Owner = capability;
                itcapQuestion.ID = capability.QuestionsOwned.Count.ToString();
                itcapForm.entities.Add(itcapQuestion);
            }
            return true;
        }
        #endregion

        #region Category
        public List<CATEGORY> GetCategories()
        {
            return (from ent in dbo.CATEGORY
                    select ent).ToList();
        }

        public override string[] GetCategoryNames()
        {
            return (from ent in dbo.CATEGORY
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetCategory(int catID, out CATEGORY category)
        {
            try
            {
                category = (from ent in dbo.CATEGORY
                            where ent.CATEGORYID == catID
                            select ent).Single();
            }

            catch
            {
                category = null;
                return false;
            }

            return true;
        }

        public bool GetCategory(string catName, out CATEGORY category)
        {
            try
            {
                category = (from ent in dbo.CATEGORY
                            where ent.NAME.TrimEnd() == catName
                            select ent).Single();
            }

            catch
            {
                category = null;
                return false;
            }

            return true;
        }

        public bool AddCategory(CATEGORY category)
        {
            //If already in DB, return 1
            if ((from ent in dbo.CATEGORY
                 where ent.NAME.TrimEnd() == category.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.CATEGORY
                                select ent.CATEGORYID).ToList();

            category.CATEGORYID = GetUniqueID(idList);

            dbo.AddToCATEGORY(category);

            return true;
        }

        public override void ChangedCategory(BOMTool bomForm)
        {
            bomForm.objectiveNames.Items.Clear();
            bomForm.objectiveNames.Text = "<Select Objective>";
            bomForm.initiativeNames.Items.Clear();
            bomForm.initiativeNames.Text = "";
            CATEGORY category;
            if (GetCategory(bomForm.categoryNames.Text.Trim(), out category))
            {
                bomForm.objectiveNames.Items.AddRange((from ent in category.BUSINESSOBJECTIVE
                                               select ent.NAME.TrimEnd()).ToArray());
            }
        }
        #endregion

        #region BusinessObjective
        public List<BUSINESSOBJECTIVE> GetObjectives()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent).ToList();
        }

        public bool GetObjective(int busID, out BUSINESSOBJECTIVE objective)
        {
            try
            {
                objective = (from ent in dbo.BUSINESSOBJECTIVE
                             where ent.BUSINESSOBJECTIVEID == busID
                             select ent).Single();
            }

            catch
            {
                objective = null;
                return false;
            }

            return true;
        }

        public bool GetObjective(string busName, out BUSINESSOBJECTIVE objective)
        {
            try
            {
                objective = (from ent in dbo.BUSINESSOBJECTIVE
                             where ent.NAME.TrimEnd() == busName
                             select ent).Single();
            }

            catch
            {
                objective = null;
                return false;
            }

            return true;
        }

        public bool AddObjective(BUSINESSOBJECTIVE objective)
        {
            //If already in DB, return false
            if ((from ent in dbo.BUSINESSOBJECTIVE
                 where ent.NAME.TrimEnd() == objective.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.BUSINESSOBJECTIVE
                                select ent.BUSINESSOBJECTIVEID).ToList();

            objective.BUSINESSOBJECTIVEID = GetUniqueID(idList);

            dbo.AddToBUSINESSOBJECTIVE(objective);

            return true;
        }

        public override void ChangedObjective(BOMTool bomForm)
        {
            bomForm.initiativeNames.Items.Clear();
            bomForm.initiativeNames.Text = "<Select Initiative>";
            BUSINESSOBJECTIVE objective;
            if (GetObjective(bomForm.objectiveNames.Text.Trim(), out objective))
            {
                bomForm.initiativeNames.Items.AddRange((from ent in objective.INITIATIVE
                                                select ent.NAME.TrimEnd()).ToArray());
            }
        }
        #endregion

        #region Initiative
        public List<INITIATIVE> GetInitiatives()
        {
            return (from ent in dbo.INITIATIVE
                    select ent).ToList();
        }

        public string[] GetInitiativeNames()
        {
            return (from ent in dbo.INITIATIVE
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetInitiative(string iniName, out INITIATIVE Initiative)
        {
            try
            {
                Initiative = (from ent in dbo.INITIATIVE
                          where ent.NAME.TrimEnd() == iniName
                          select ent).Single();
            }

            catch
            {
                Initiative = null;
                return false;
            }

            return true;
        }

        public bool GetInitiative(int iniID, out INITIATIVE Initiative)
        {
            try
            {
                Initiative = (from ent in dbo.INITIATIVE
                          where ent.INITIATIVEID == iniID
                          select ent).Single();
            }

            catch
            {
                Initiative = null;
                return false;
            }

            return true;
        }

        public bool AddInitiative(INITIATIVE initiative)
        {
            //If already in DB, return false
            if ((from ent in dbo.INITIATIVE
                 where ent.NAME.TrimEnd() == initiative.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.INITIATIVE
                                select ent.INITIATIVEID).ToList();

            initiative.INITIATIVEID = GetUniqueID(idList);

            dbo.AddToINITIATIVE(initiative);

            return true;
        }

        public override void AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
        {
            INITIATIVE initiative;
            if (!GetInitiative(iniName, out initiative))
            {
                initiative = new INITIATIVE();
                initiative.NAME = iniName;
                BUSINESSOBJECTIVE objective;
                if (!GetObjective(busName, out objective))
                {
                    objective = new BUSINESSOBJECTIVE();
                    objective.NAME = busName;
                    CATEGORY category;
                    if (!GetCategory(catName, out category))
                    {
                        category = new CATEGORY();
                        category.NAME = catName;
                        if (!AddCategory(category))
                        {
                            MessageBox.Show("Failed to add Category to Database", "Error");
                            return;
                        }
                    }

                    objective.CATEGORY = category;
                    if (!AddObjective(objective))
                    {
                        MessageBox.Show("Failed to add Objective to Database", "Error");
                        return;
                    }
                }

                initiative.BUSINESSOBJECTIVE = objective;
                if (!AddInitiative(initiative))
                {
                    MessageBox.Show("Failed to add Initiative to Database", "Error");
                    return;
                }
            }

            BOM bom = new BOM();
            bom.INITIATIVE = initiative;
            if (!AddBOM(bom, bomForm.client))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return;
            }
            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
                return;
            }

            else
            {
                //Successfully added to database, update GUI
                catName = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                NewCategory category = bomForm.Categories.Find(delegate(NewCategory cat)
                {
                    return cat.name == catName;
                });
                if (category == null)
                {
                    category = bomForm.AddCategory(catName);
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
        #endregion

        #region Domain

        public List<DOMAIN> GetDomains()
        {
            return (from ent in dbo.DOMAIN
                    select ent).ToList();
        }

        public override string[] GetDomainNames()
        {
            return (from ent in dbo.DOMAIN
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override string[] GetDomainNamesAndDefault()
        {
            return (from ent in dbo.DOMAIN
                    select ent.NAME.TrimEnd() + ent.DEFAULT).ToArray();
        }

        public override string[] GetDefaultDomainNames()
        {
            return (from ent in dbo.DOMAIN
                    where ent.DEFAULT == "Y"
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetDomain(string domName, out DOMAIN domain)
        {
            try
            {
                domain = (from ent in dbo.DOMAIN
                          where ent.NAME.TrimEnd() == domName
                          select ent).Single();
            }

            catch
            {
                domain = null;
                return false;
            }

            return true;
        }

        public bool AddDomain(DOMAIN domain)
        {
            //If already in DB, return 1
            if ((from ent in dbo.DOMAIN
                 where ent.NAME.TrimEnd() == domain.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.DOMAIN
                                select ent.DOMAINID).ToList();

            domain.DOMAINID = GetUniqueID(idList);

            dbo.AddToDOMAIN(domain);

            return true;
        }

        public override void ChangedDomain(ITCapTool itcapForm)
        {
            itcapForm.capabilitiesList.Items.Clear();
            itcapForm.capabilitiesList.Text = "<Select Objective>";
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "";
            DOMAIN domain;
            if (GetDomain(itcapForm.domainList.Text.Trim(), out domain))
            {
                itcapForm.capabilitiesList.Items.AddRange((from ent in domain.CAPABILITY
                                                       select ent.NAME.TrimEnd()).ToArray());
            }
        }
        #endregion

        #region Capability
        public override string[] GetCapabilityNames(string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from ent in dom.CAPABILITY
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override string[] GetCapabilityNamesAndDefault(string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from ent in dom.CAPABILITY
                    select ent.NAME.TrimEnd() + ent.DEFAULT).ToArray();
        }

        public override string[] GetDefaultCapabilityNames(string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from ent in dom.CAPABILITY
                    where ent.DEFAULT == "Y"
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetCapability(string capName, out CAPABILITY capability)
        {
            try
            {
                capability = (from ent in dbo.CAPABILITY
                              where ent.NAME.TrimEnd() == capName
                              select ent).Single();
            }

            catch
            {
                capability = null;
                return false;
            }

            return true;
        }

        public bool AddCapability(CAPABILITY capability)
        {
            //If already in DB, return false
            if ((from ent in dbo.CAPABILITY
                 where ent.NAME.TrimEnd() == capability.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.CAPABILITY
                                select ent.CAPABILITYID).ToList();

            capability.CAPABILITYID = GetUniqueID(idList);

            dbo.AddToCAPABILITY(capability);

            return true;
        }

        public override void ChangedCapability(ITCapTool itcapForm)
        {
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "<Select Initiative>";
            CAPABILITY capability;

            if (GetCapability(itcapForm.capabilitiesList.Text.Trim(), out capability))
            {
                itcapForm.questionList.Items.AddRange((from ent in capability.ITCAPQUESTION
                                                       select ent.NAME.TrimEnd()).ToArray());
            }
        }
        #endregion

        #region ITCAPQuestion
        public override string[] GetITCAPQuestionNames(string capName, string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from cap in dom.CAPABILITY
                    where cap.NAME == capName
                    from ent in cap.ITCAPQUESTION
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override string[] GetITCAPQuestionNamesAndDefault(string capName, string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from cap in dom.CAPABILITY
                    where cap.NAME == capName
                    from ent in cap.ITCAPQUESTION
                    select ent.NAME.TrimEnd() + ent.DEFAULT).ToArray();
        }

        public override string[] GetDefaultITCAPQuestionNames(string capName, string domName)
        {
            return (from dom in dbo.DOMAIN
                    where dom.NAME == domName
                    from cap in dom.CAPABILITY
                    where cap.NAME == capName
                    from ent in cap.ITCAPQUESTION
                    where ent.DEFAULT == "Y"
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetITCAPQuestion(string itcqName, out ITCAPQUESTION itcapQuestion)
        {
            try
            {
                itcapQuestion = (from ent in dbo.ITCAPQUESTION
                                 where ent.NAME.TrimEnd() == itcqName
                                 select ent).Single();
            }

            catch
            {
                itcapQuestion = null;
                return false;
            }

            return true;
        }

        public bool AddITCAPQuestion(ITCAPQUESTION itcapQuestion)
        {
            //If already in DB, return false
            if ((from ent in dbo.ITCAPQUESTION
                 where ent.NAME.TrimEnd() == itcapQuestion.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.ITCAPQUESTION
                                select ent.ITCAPQUESTIONID).ToList();

            itcapQuestion.ITCAPQUESTIONID = GetUniqueID(idList);

            dbo.AddToITCAPQUESTION(itcapQuestion);

            return true;
        }

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm)
        {
            ITCAPQUESTION itcapQuestion;
            if (!GetITCAPQuestion(itcqName, out itcapQuestion))
            {
                itcapQuestion = new ITCAPQUESTION();
                itcapQuestion.NAME = itcqName;
                itcapQuestion.DEFAULT = "N";
                CAPABILITY capability;
                if (!GetCapability(capName, out capability))
                {
                    capability = new CAPABILITY();
                    capability.NAME = capName;
                    capability.DEFAULT = "N";
                    DOMAIN domain;
                    if (!GetDomain(domName, out domain))
                    {
                        domain = new DOMAIN();
                        domain.NAME = domName;
                        domain.DEFAULT = "N";
                        if (!AddDomain(domain))
                        {
                            MessageBox.Show("Failed to add Domain to Database", "Error");
                            return;
                        }
                    }

                    capability.DOMAIN = domain;
                    if (!AddCapability(capability))
                    {
                        MessageBox.Show("Failed to add Capability to Database", "Error");
                        return;
                    }
                }

                itcapQuestion.CAPABILITY = capability;
                if (!AddITCAPQuestion(itcapQuestion))
                {
                    MessageBox.Show("Failed to add ITCAPQuestion to Database", "Error");
                    return;
                }
            }

            ITCAP itcap = new ITCAP();
            itcap.ITCAPQUESTION = itcapQuestion;
            if (!AddITCAP(itcap, itcapForm.client))
            {
                MessageBox.Show("Failed to add ITCAPQuestion to ITCAP", "Error");
                return;
            }
            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
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
                dbo.SaveChanges();
            }
            catch
            {
                return false;
            }


            UpdateDataFile();

            return true;
        }

        //Used to create a unique ID for DB Entities
        public int GetUniqueID(List<int> idList)
        {
            Random rnd = new Random();

            int id = rnd.Next();

            while (true)
            {
                var idQuery = from tmp in idList
                              where tmp == id
                              select tmp;

                if (idQuery.Count() == 0)
                {
                    break;
                }

                else
                {
                    id = rnd.Next();
                }
            }

            return id;
        }

        public void UpdateDataFile()
        {
            XElement root = new XElement("root");

            List<CLIENT> clientList = GetClients();
            XElement clientElement = new XElement("CLIENTS");
            foreach (CLIENT client in clientList)
            {
                XElement temp = new XElement("CLIENT");
                temp.Add(new XElement("NAME", client.NAME.TrimEnd().Replace(' ', '~')));
                temp.Add(new XElement("CLIENTID", client.CLIENTID));
                XElement grpElement = new XElement("GROUPS");
                foreach (GROUP grp in client.GROUP)
                {
                    XElement tempGrp = new XElement("GROUP");
                    tempGrp.Add(new XElement("NAME", grp.NAME.TrimEnd().Replace(' ', '~')));
                    tempGrp.Add(new XElement("GROUPID", grp.GROUPID));

                    XElement conElement = new XElement("CONTACTS");
                    foreach (CONTACT contact in grp.CONTACT)
                    {
                        XElement tempCon = new XElement("CONTACT");
                        tempCon.Add(new XElement("NAME", contact.NAME.TrimEnd().Replace(' ', '~')));
                        tempCon.Add(new XElement("CONTACTID", contact.CONTACTID));

                        XElement bomConElement = new XElement("BOMS");
                        foreach (BOM bom in contact.BOM)
                        {
                            XElement tempBom = new XElement("BOM");
                            tempBom.Add(new XElement("BOMID", bom.BOMID));
                            tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd().Replace(' ', '~')));
                            tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd().Replace(' ', '~')));
                            tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd().Replace(' ', '~')));
                            tempBom.Add(new XElement("EFFECTIVENESS", bom.EFFECTIVENESS != null ? bom.EFFECTIVENESS : 0));
                            tempBom.Add(new XElement("CRITICALITY", bom.CRITICALITY != null ? bom.CRITICALITY : 0));
                            tempBom.Add(new XElement("DIFFERENTIAL", bom.DIFFERENTIAL != null ? bom.DIFFERENTIAL : 0));
                            bomConElement.Add(tempBom);
                        }
                        tempCon.Add(bomConElement);

                        XElement itcapConElement = new XElement("ITCAPS");
                        foreach (ITCAP itcap in contact.ITCAP)
                        {
                            XElement tempItcap = new XElement("ITCAP");
                            tempItcap.Add(new XElement("ITCAPID", itcap.ITCAPID));
                            tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                            tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                            tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                            itcapConElement.Add(tempItcap);
                        }
                        temp.Add(itcapConElement);

                        conElement.Add(tempCon);
                    }

                    tempGrp.Add(conElement);

                    XElement bomGrpElement = new XElement("BOMS");
                    foreach (BOM bom in grp.BOM)
                    {
                        XElement tempBom = new XElement("BOM");
                        tempBom.Add(new XElement("BOMID", bom.BOMID));
                        tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd().Replace(' ', '~')));
                        tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd().Replace(' ', '~')));
                        tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd().Replace(' ', '~')));
                        tempBom.Add(new XElement("EFFECTIVENESS", bom.EFFECTIVENESS != null ? bom.EFFECTIVENESS : 0));
                        tempBom.Add(new XElement("CRITICALITY", bom.CRITICALITY != null ? bom.CRITICALITY : 0));
                        tempBom.Add(new XElement("DIFFERENTIAL", bom.DIFFERENTIAL != null ? bom.DIFFERENTIAL : 0));
                        bomGrpElement.Add(tempBom);
                    }
                    tempGrp.Add(bomGrpElement);

                    XElement itcapGrpElement = new XElement("ITCAPS");
                    foreach (ITCAP itcap in grp.ITCAP)
                    {
                        XElement tempItcap = new XElement("ITCAP");
                        tempItcap.Add(new XElement("ITCAPID", itcap.ITCAPID));
                        tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                        tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                        tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                        itcapGrpElement.Add(tempItcap);
                    }
                    tempGrp.Add(itcapGrpElement);

                    grpElement.Add(tempGrp);
                }
                temp.Add(grpElement);

                XElement bomElement = new XElement("BOMS");
                foreach (BOM bom in client.BOM)
                {
                    XElement tempBom = new XElement("BOM");
                    tempBom.Add(new XElement("BOMID", bom.BOMID));
                    tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd().Replace(' ', '~')));
                    tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd().Replace(' ', '~')));
                    tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd().Replace(' ', '~')));
                    tempBom.Add(new XElement("EFFECTIVENESS", bom.EFFECTIVENESS != null ? bom.EFFECTIVENESS : 0));
                    tempBom.Add(new XElement("CRITICALITY", bom.CRITICALITY != null ? bom.CRITICALITY : 0));
                    tempBom.Add(new XElement("DIFFERENTIAL", bom.DIFFERENTIAL != null ? bom.DIFFERENTIAL : 0));
                    bomElement.Add(tempBom);
                }
                temp.Add(bomElement);

                XElement itcapElement = new XElement("ITCAPS");
                foreach (ITCAP itcap in client.ITCAP)
                {
                    XElement tempItcap = new XElement("ITCAP");
                    tempItcap.Add(new XElement("ITCAPID", itcap.ITCAPID));
                    tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                    tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                    tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                    itcapElement.Add(tempItcap);
                }
                temp.Add(itcapElement);

                clientElement.Add(temp);
            }
            root.Add(clientElement);

            List<CATEGORY> catList = GetCategories();
            XElement catElement = new XElement("CATEGORIES");
            foreach (CATEGORY category in catList)
            {
                XElement temp = new XElement("CATEGORY");
                temp.Add(new XElement("NAME", category.NAME.TrimEnd().Replace(' ', '~')));
                temp.Add(new XElement("CATEGORYID", category.CATEGORYID));

                XElement busElement = new XElement("BUSINESSOBJECTIVES");
                foreach (BUSINESSOBJECTIVE objective in category.BUSINESSOBJECTIVE)
                {
                    XElement tempBus = new XElement("BUSINESSOBJECTIVE");
                    tempBus.Add(new XElement("NAME", objective.NAME.TrimEnd().Replace(' ', '~')));
                    tempBus.Add(new XElement("BUSINESSOBJECTIVEID", objective.BUSINESSOBJECTIVEID));

                    XElement iniElement = new XElement("INITIATIVES");
                    foreach (INITIATIVE initiative in objective.INITIATIVE)
                    {
                        XElement tempIni = new XElement("INITIATIVE");
                        tempIni.Add(new XElement("NAME", initiative.NAME.TrimEnd().Replace(' ', '~')));
                        tempIni.Add(new XElement("INITIATIVEID", initiative.INITIATIVEID));
                        iniElement.Add(tempIni);
                    }
                    tempBus.Add(iniElement);

                    busElement.Add(tempBus);
                }
                temp.Add(busElement);

                catElement.Add(temp);
            }
            root.Add(catElement);

            List<DOMAIN> domList = GetDomains();
            XElement domElement = new XElement("DOMAINS");
            foreach (DOMAIN domain in domList)
            {
                XElement temp = new XElement("DOMAIN");
                temp.Add(new XElement("NAME", domain.NAME.TrimEnd().Replace(' ', '~')));
                temp.Add(new XElement("DEFAULT", domain.DEFAULT));
                temp.Add(new XElement("DOMAINID", domain.DOMAINID));

                XElement capElement = new XElement("CAPABILITIES");
                foreach (CAPABILITY capability in domain.CAPABILITY)
                {
                    XElement tempCap = new XElement("CAPABILITY");
                    tempCap.Add(new XElement("NAME", capability.NAME.TrimEnd().Replace(' ', '~')));
                    tempCap.Add(new XElement("DEFAULT", capability.DEFAULT));
                    tempCap.Add(new XElement("CAPABILITYID", capability.CAPABILITYID));

                    XElement questionElement = new XElement("ITCAPQUESTIONS");
                    foreach (ITCAPQUESTION itcapQuestion in capability.ITCAPQUESTION)
                    {
                        XElement tempItcq = new XElement("ITCAPQUESTION");
                        tempItcq.Add(new XElement("NAME", itcapQuestion.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcq.Add(new XElement("DEFAULT", itcapQuestion.DEFAULT));
                        tempItcq.Add(new XElement("ITCAPQUESTIONID", itcapQuestion.ITCAPQUESTIONID));
                        questionElement.Add(tempItcq);
                    }
                    tempCap.Add(questionElement);

                    capElement.Add(tempCap);
                }
                temp.Add(capElement);

                domElement.Add(temp);
            }
            root.Add(domElement);

            root.Save("Data.xml");
        }
        public void CheckChangeLog()
        {
            bool success = true;

            string line;
            string[] lineArray;

            CLIENT client;
            GROUP grp;
            CONTACT contact;
            CATEGORY category;
            BUSINESSOBJECTIVE objective;
            INITIATIVE initiative;
            BOM bom;

            if (!File.Exists("Changes.log"))
            {
                File.Create("Changes.log");
            }

            using (System.IO.StreamReader file = new System.IO.StreamReader("Changes.log"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    lineArray = line.Split(' ');
                    if (lineArray[0] == "ADD")
                    {
                        switch(lineArray[1])
                        {
                            case "CLIENT":
                                client = new CLIENT();
                                client.NAME = lineArray[2].Replace('~', ' ');
                                AddClient(client);
                                break;

                            case "GROUP":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    AddGroup(lineArray[2].Replace('~', ' '), client);
                                }
                                break;

                            case "CATEGORY":
                                category = new CATEGORY();
                                category.NAME = lineArray[2].Replace('~', ' ');
                                AddCategory(category);
                                break;

                            case "BUSINESSOBJECTIVE":
                                if (GetCategory(lineArray[3].Replace('~', ' '), out category))
                                {
                                    objective = new BUSINESSOBJECTIVE();
                                    objective.NAME = lineArray[2].Replace('~', ' ');
                                    objective.CATEGORY = category;
                                    AddObjective(objective);
                                }
                                break;

                            case "INITIATIVE":
                                if (GetObjective(lineArray[3].Replace('~', ' '), out objective))
                                {
                                    initiative = new INITIATIVE();
                                    initiative.NAME = lineArray[2].Replace('~', ' ');
                                    initiative.BUSINESSOBJECTIVE = objective;
                                    AddInitiative(initiative);
                                }
                                break;

                            case "BOM":
                                if (lineArray[2] == "CLIENT")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetInitiative(lineArray[4].Replace('~', ' '), out initiative))
                                        {
                                            bom = new BOM();
                                            bom.INITIATIVE = initiative;
                                            AddBOM(bom, client);
                                        }
                                    }
                                }
                                else if (lineArray[2] == "GROUP")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                        {
                                            if (GetInitiative(lineArray[5].Replace('~', ' '), out initiative))
                                            {
                                                bom = new BOM();
                                                bom.INITIATIVE = initiative;
                                                AddBOM(bom, grp);
                                            }
                                        }
                                    }
                                }

                                else if (lineArray[2] == "CONTACT")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                        {
                                            if (GetContact(lineArray[5].Replace('~', ' '), grp, out contact))
                                            {
                                                if (GetInitiative(lineArray[6].Replace('~', ' '), out initiative))
                                                {
                                                    bom = new BOM();
                                                    bom.INITIATIVE = initiative;
                                                    AddBOM(bom, contact);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }

                    else if (lineArray[0] == "UPDATE")
                    {
                        switch (lineArray[1])
                        {
                            case "BOM":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetBOM(lineArray[3].Replace('~', ' '), client, out bom))
                                    {
                                        bom.EFFECTIVENESS = Convert.ToSingle(lineArray[4]);
                                        bom.CRITICALITY = Convert.ToSingle(lineArray[5]);
                                        bom.DIFFERENTIAL = Convert.ToSingle(lineArray[6]);
                                    }
                                }
                                break;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                        success = false;
                    }

                    if (!SaveChanges())
                    {
                        MessageBox.Show("Instruction failed to execute: \n" + line, "Error");
                        success = false;
                        break;
                    }
                }
            }

            if (success)
            {
                File.WriteAllText("Changes.log", string.Empty);
            }
        }
        #endregion
    }
}