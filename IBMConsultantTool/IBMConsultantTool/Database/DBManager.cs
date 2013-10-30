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

        public bool AddClient(CLIENT client)
        {
            //If already in DB, return false
            if ((from ent in dbo.CLIENT
                 where ent.NAME.TrimEnd() == client.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(client);
                return false;
            }

            dbo.AddToCLIENT(client);

            return true;
        }

        public override List<string> GetObjectivesFromClientBOM(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;

            List<BUSINESSOBJECTIVE> entList = (from ent in client.BOM
                                               select ent.INITIATIVE.BUSINESSOBJECTIVE).ToList();

            List<string> stringList = new List<string>();
            foreach (BUSINESSOBJECTIVE busObj in entList)
            {
                if(!stringList.Contains(busObj.NAME.TrimEnd()))
                {
                    stringList.Add(busObj.NAME.TrimEnd());
                }
            }

            return stringList;
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

            dbo.AddToGROUP(grp);

            return true;
        }
        public bool AddGroups(string[] grpNames, CLIENT client)
        {
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
            Console.WriteLine(ini.Effectiveness.ToString());
            try
            {
                BOM bom = (from ent in client.BOM
                           where ent.INITIATIVE.NAME.TrimEnd() == ini.Name
                           select ent).Single();

                bom.EFFECTIVENESS = (float)ini.Effectiveness;
                
                bom.CRITICALITY = (float)ini.Criticality;
                bom.DIFFERENTIAL = (float)ini.Differentiation;
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
                dbo.Detach(bom);
                return false;
            }

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
                dbo.Detach(bom);
                return false;
            }

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
                dbo.Detach(bom);
                return false;
            }

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

                    bomForm.CategoryWorkspace.SelectTab(category.name);

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

        public bool GetITCAP(string itcqName, CLIENT client, out ITCAP itcap)
        {
            try
            {
                itcap = (from ent in client.ITCAP
                         where ent.ITCAPQUESTION.NAME == itcqName
                         select ent).Single();
            }

            catch
            {
                itcap = null;
                return false;
            }

            return true;
        }

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
                dbo.Detach(itcqObject);
                return false;
            }

            itcap.CLIENT = client;

            dbo.AddToITCAP(itcap);

            return true;
        }

        public override bool AddITCAPToGroup(object itcqObject, object groupObj)
        {
            ITCAP itcap = itcqObject as ITCAP;
            GROUP grp = groupObj as GROUP;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in grp.ITCAP
                 where ent.ITCAPQUESTION.NAME.TrimEnd() == itcap.ITCAPQUESTION.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(itcqObject);
                return false;
            }

            itcap.GROUP = grp;

            dbo.AddToITCAP(itcap);

            return true;
        }

        public override bool AddITCAPToContact(object itcqObject, object contactObj)
        {
            ITCAP itcap = itcqObject as ITCAP;
            CONTACT contact = contactObj as CONTACT;

            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in contact.ITCAP
                 where ent.ITCAPQUESTION.NAME.TrimEnd() == itcap.ITCAPQUESTION.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(itcqObject);
                return false;
            }

            itcap.CONTACT = contact;

            dbo.AddToITCAP(itcap);

            return true;
        }

        public override bool RemoveITCAP(string itcqName, object clientObj)
        {
            ITCAP itcap;
            CLIENT client = clientObj as CLIENT;
            
            if(GetITCAP(itcqName, client, out itcap))
            {
                dbo.DeleteObject(itcap);
                return true;
            }
            
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
                    domain.Type = "domain";
                    domain.Visible = true;
                    //itcapForm.LoadCapabilities(dom);
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
                    capability.IsDefault = capEnt.DEFAULT == "Y";
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
                itcapQuestion.IsDefault = itcqEnt.DEFAULT == "Y";
                itcapQuestion.AsIsScore = itcap.ASIS.HasValue ? itcap.ASIS.Value : 0;
                itcapQuestion.ToBeScore = itcap.TOBE.HasValue ? itcap.TOBE.Value : 0;
                itcapQuestion.comment = itcap.COMMENT;
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
            CLIENT client = itcapForm.client as CLIENT;
            List<ITCAP> itcapList = client.ITCAP.ToList();
            foreach (ITCAP itcap in itcapList)
            {
                dbo.DeleteObject(itcap);
            }

            SaveChanges();

            ITCAP itcapEnt;
            ITCAPQUESTION itcqEnt;
            foreach (Domain domain in itcapForm.domains)
            {
                foreach(Capability capability in domain.CapabilitiesOwned)
                {
                    foreach(ITCapQuestion itcapQuestion in capability.QuestionsOwned)
                    {
                        if (GetITCAPQuestion(itcapQuestion.Name, out itcqEnt))
                        {
                            itcapEnt = new ITCAP();
                            itcapEnt.ITCAPQUESTION = itcqEnt;
                            itcapEnt.ASIS = 0;
                            itcapEnt.TOBE = 0;
                            itcapEnt.COMMENT = "";
                            if (!AddITCAP(itcapEnt, client))
                            {
                                MessageBox.Show("Failed to add ITCAPQuestion: " + itcapEnt.ITCAPQUESTION.NAME);
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
                dbo.Detach(category);
                return false;
            }

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
                dbo.Detach(objective);
                return false;
            }

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

        public bool AddInitiative(INITIATIVE initiative)
        {
            //If already in DB, return false
            if ((from ent in dbo.INITIATIVE
                 where ent.NAME.TrimEnd() == initiative.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(initiative);
                return false;
            }

            dbo.AddToINITIATIVE(initiative);

            return true;
        }

        public override bool AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
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
                            return false;
                        }
                    }

                    objective.CATEGORY = category;
                    if (!AddObjective(objective))
                    {
                        MessageBox.Show("Failed to add Objective to Database", "Error");
                        return false;
                    }
                }

                initiative.BUSINESSOBJECTIVE = objective;
                if (!AddInitiative(initiative))
                {
                    MessageBox.Show("Failed to add Initiative to Database", "Error");
                    return false;
                }
            }

            BOM bom = new BOM();
            bom.INITIATIVE = initiative;
            if (!AddBOM(bom, bomForm.client))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return false;
            }
            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
                return false;
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

                bomForm.CategoryWorkspace.SelectTab(category.name);

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

            return true;
        }
        #endregion

        #region CUPEQuestion
        public override List<CupeQuestion> GetCUPEQuestions()
        {
            throw new NotImplementedException();
        }
        public override bool AddCupeQuestion(CupeQuestion cupeQuestion)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CUPE
        public override bool UpdateCUPE(object clientObj, CupeQuestion cq)
        {
            CLIENT client = clientObj as CLIENT;
            try
            {
                CUPE cupe = (from ent in client.CUPE
                             where ent.NAME.TrimEnd() == cq.QuestionText
                             select ent).Single();

                cupe.CURRENT = cq.Current;
                cupe.FUTURE = cq.Future;
            }

            catch
            {
                return false;
            }


            return true;   
        }
        public override bool AddCUPE(object cupeObj, object clientObj)
        {
            CUPE cupe = cupeObj as CUPE;
            CLIENT client = clientObj as CLIENT;

            if ((from ent in client.CUPE
                 where ent.NAME.TrimEnd() == cupe.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(cupe);
                return false;
            }

            client.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
        public override bool AddCUPEToGroup(object cupeObj, object groupObj)
        {
            CUPE cupe = cupeObj as CUPE;
            GROUP grp = groupObj as GROUP;

            if ((from ent in grp.CUPE
                 where ent.NAME.TrimEnd() == cupe.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(cupe);
                return false;
            }

            grp.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
        public override bool AddCUPEToContact(object cupeObj, object contactObj)
        {
            CUPE cupe = cupeObj as CUPE;
            CONTACT contact = contactObj as CONTACT;

            if ((from ent in contact.CUPE
                 where ent.NAME.TrimEnd() == cupe.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(cupe);
                return false;
            }

            contact.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
        public override bool BuildCUPEForm(CUPETool cupeForm, string clientName)
        {
            CLIENT client;

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

                cupeForm.client = client;

                return true;
            }
            else
            {
                MessageBox.Show("Client already exists", "Error");
                return false;
            }
        }

        public override void PopulateCUPEQuestions(CUPETool cupeForm)
        {
            CLIENT client = cupeForm.client as CLIENT;
            CupeQuestionStringData data = new CupeQuestionStringData();
            if (client.CUPE.Count != 0)
            {
                foreach (CUPE cupe in client.CUPE)
                {
                    data.QuestionText = cupe.NAME;
                    data.ChoiceA = cupe.COMMODITY;
                    data.ChoiceB = cupe.UTILITY;
                    data.ChoiceC = cupe.PARTNER;
                    data.ChoiceD = cupe.ENABLER;

                    ClientDataControl.AddCupeQuestion(data);
                    data = new CupeQuestionStringData();
                }
            }

            else
            {
                foreach (CUPEQUESTION cupeQuestion in dbo.CUPEQUESTION)
                {
                    if (cupeQuestion.INTWENTY == "Y")
                    {
                        data.QuestionText = cupeQuestion.NAME;
                        data.ChoiceA = cupeQuestion.COMMODITY;
                        data.ChoiceB = cupeQuestion.UTILITY;
                        data.ChoiceC = cupeQuestion.PARTNER;
                        data.ChoiceD = cupeQuestion.ENABLER;

                        ClientDataControl.AddCupeQuestion(data);
                        data = new CupeQuestionStringData();
                    }
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
                dbo.Detach(domain);
                return false;
            }

            dbo.AddToDOMAIN(domain);

            return true;
        }

        public override void ChangedDomain(ITCapTool itcapForm)
        {
            itcapForm.capabilitiesList.Items.Clear();
            itcapForm.capabilitiesList.Text = "<Select Capability>";
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "";
            DOMAIN domain;
            if (GetDomain(itcapForm.domainList.Text.Trim(), out domain))
            {
                itcapForm.capabilitiesList.Items.AddRange((from ent in domain.CAPABILITY
                                                       select ent.NAME.TrimEnd()).ToArray());
            }
        }

        public override bool ChangeDomainDefault(string domName, bool isDefault)
        {
            DOMAIN domain;
            if (GetDomain(domName, out domain))
            {
                domain.DEFAULT = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find Domain: " + domName, "Error");
            return false;
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
                dbo.Detach(capability);
                return false;
            }

            dbo.AddToCAPABILITY(capability);

            return true;
        }

        public override void ChangedCapability(ITCapTool itcapForm)
        {
            itcapForm.questionList.Items.Clear();
            itcapForm.questionList.Text = "<Select ITCAPQuestion>";
            CAPABILITY capability;

            if (GetCapability(itcapForm.capabilitiesList.Text.Trim(), out capability))
            {
                itcapForm.questionList.Items.AddRange((from ent in capability.ITCAPQUESTION
                                                       select ent.NAME.TrimEnd()).ToArray());
            }
        }

        public override bool ChangeCapabilityDefault(string capName, bool isDefault)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                capability.DEFAULT = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find Capability: " + capName, "Error");
            return false;
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
                dbo.Detach(itcapQuestion);
                return false;
            }

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
                    domain.ID = (itcapForm.domains.Count+1).ToString();
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
                    capability.ID = (domain.CapabilitiesOwned.Count+1).ToString();
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
                    itcqObject.ID = (capability.QuestionsOwned.Count+1).ToString();
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
            /*ITCAP itcapQuestion;
            if (GetITCAP(itcqName, out itcapQuestion))
            {
                dbo.RemoveITCAPQUESTION(itcapQuestion);
                dbo.SaveChanges();
            }*/
        }

        public override bool ChangeITCAPQuestionDefault(string itcqName, bool isDefault)
        {
            ITCAPQUESTION itcapQuestion;
            if (GetITCAPQuestion(itcqName, out itcapQuestion))
            {
                itcapQuestion.DEFAULT = isDefault ? "Y" : "N";
                return true;
            }

            MessageBox.Show("Could Not Find ITCAPQuestion: " + itcqName, "Error");
            return false;
        }
        #endregion

        #region ITCAPOBJMAP

        private bool GetITCAPOBJMAP(object clientObj, string capName, string busName, out ITCAPOBJMAP itcapObjMap)
        {
            CLIENT client = clientObj as CLIENT;
            try
            {
                itcapObjMap = (from ent in client.ITCAPOBJMAP
                               where ent.CAPABILITY.NAME.TrimEnd() == capName &&
                                     ent.BUSINESSOBJECTIVE.NAME.TrimEnd() == busName
                               select ent).Single();
            }

            catch
            {
                itcapObjMap = null;
                return false;
            }
            return true;
        }

        public override bool GetITCAPOBJMAPScore(object clientObj, string capName, string busName, out int score)
        {
            CLIENT client = clientObj as CLIENT;
            try
            {
                score = (from ent in client.ITCAPOBJMAP
                         where ent.CAPABILITY.NAME.TrimEnd() == capName &&
                               ent.BUSINESSOBJECTIVE.NAME.TrimEnd() == busName
                         select ent.SCORE).Single();
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
            CLIENT client = clientObj as CLIENT;
            if((from ent in client.ITCAPOBJMAP
                               where ent.CAPABILITY.NAME.TrimEnd() == capName &&
                                     ent.BUSINESSOBJECTIVE.NAME.TrimEnd() == busName
                               select ent).Count() == 0)
            {
                ITCAPOBJMAP itcapObjMap = new ITCAPOBJMAP();
                CAPABILITY capability;
                BUSINESSOBJECTIVE objective;

                itcapObjMap.CLIENT = client;

                if (!GetCapability(capName, out capability))
                {
                    MessageBox.Show("Could not create mapping: Capability not found", "Error");
                    dbo.DeleteObject(itcapObjMap);
                    return false;
                }

                itcapObjMap.CAPABILITY = capability;

                if (!GetObjective(busName, out objective))
                {
                    MessageBox.Show("Could not create mapping: Objective not found", "Error");
                    dbo.DeleteObject(itcapObjMap);
                    return false;
                }

                itcapObjMap.BUSINESSOBJECTIVE = objective;
                itcapObjMap.SCORE = 0;

                dbo.AddToITCAPOBJMAP(itcapObjMap);
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
            CLIENT client = clientObj as CLIENT;
            ITCAPOBJMAP itcapObjMap;
            try
            {
                itcapObjMap = (from ent in client.ITCAPOBJMAP
                               where ent.CAPABILITY.NAME.TrimEnd() == capName &&
                                     ent.BUSINESSOBJECTIVE.NAME.TrimEnd() == busName
                               select ent).Single();

                itcapObjMap.SCORE = score;
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
                dbo.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Save Changes Failed:\n\n" + e.Message + e.InnerException, "Error");
                return false;
            }


            UpdateDataFile();

            return true;
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
                XElement grpElement = new XElement("GROUPS");
                foreach (GROUP grp in client.GROUP)
                {
                    XElement tempGrp = new XElement("GROUP");
                    tempGrp.Add(new XElement("NAME", grp.NAME.TrimEnd().Replace(' ', '~')));

                    XElement conElement = new XElement("CONTACTS");
                    foreach (CONTACT contact in grp.CONTACT)
                    {
                        XElement tempCon = new XElement("CONTACT");
                        tempCon.Add(new XElement("NAME", contact.NAME.TrimEnd().Replace(' ', '~')));

                        XElement bomConElement = new XElement("BOMS");
                        foreach (BOM bom in contact.BOM)
                        {
                            XElement tempBom = new XElement("BOM");
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
                            tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                            tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                            tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                            tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                            itcapConElement.Add(tempItcap);
                        }
                        tempCon.Add(itcapConElement);

                        XElement cupeConElement = new XElement("CUPES");
                        foreach (CUPE cupe in contact.CUPE)
                        {
                            XElement tempCUPE = new XElement("CUPE");
                            tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                            tempCUPE.Add(new XElement("CURRENT", cupe.CURRENT));
                            tempCUPE.Add(new XElement("FUTURE", cupe.FUTURE));
                            tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd().Replace(' ', '~')));
                            tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY));
                            tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY));
                            tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER));
                            tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER));
                            itcapConElement.Add(tempCUPE);
                        }
                        tempCon.Add(cupeConElement);

                        conElement.Add(tempCon);
                    }

                    tempGrp.Add(conElement);

                    XElement bomGrpElement = new XElement("BOMS");
                    foreach (BOM bom in grp.BOM)
                    {
                        XElement tempBom = new XElement("BOM");
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
                        tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                        tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                        tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                        itcapGrpElement.Add(tempItcap);
                    }
                    tempGrp.Add(itcapGrpElement);

                    XElement cupeGrpElement = new XElement("CUPES");
                    foreach (CUPE cupe in grp.CUPE)
                    {
                        XElement tempCUPE = new XElement("CUPE");
                        tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                        tempCUPE.Add(new XElement("CURRENT", cupe.CURRENT));
                        tempCUPE.Add(new XElement("FUTURE", cupe.FUTURE));
                        tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd().Replace(' ', '~')));
                        tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY));
                        tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY));
                        tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER));
                        tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER));
                        itcapGrpElement.Add(tempCUPE);
                    }
                    tempGrp.Add(cupeGrpElement);

                    grpElement.Add(tempGrp);
                }
                temp.Add(grpElement);

                XElement bomElement = new XElement("BOMS");
                foreach (BOM bom in client.BOM)
                {
                    XElement tempBom = new XElement("BOM");
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
                    tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd().Replace(' ', '~')));
                    tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                    tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                    tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                    itcapElement.Add(tempItcap);
                }
                temp.Add(itcapElement);

                XElement cupeElement = new XElement("CUPES");
                foreach (CUPE cupe in client.CUPE)
                {
                    XElement tempCUPE = new XElement("CUPE");
                    tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd().Replace(' ', '~')));
                    tempCUPE.Add(new XElement("CURRENT", cupe.CURRENT));
                    tempCUPE.Add(new XElement("FUTURE", cupe.FUTURE));
                    tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd().Replace(' ', '~')));
                    tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY));
                    tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY));
                    tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER));
                    tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER));
                    cupeElement.Add(tempCUPE);
                }
                temp.Add(cupeElement);

                XElement itcapObjMapElement = new XElement("ITCAPOBJMAPS");
                foreach (ITCAPOBJMAP itcapObjMap in client.ITCAPOBJMAP)
                {
                    XElement tempITCAPObjMap = new XElement("ITCAPOBJMAP");
                    tempITCAPObjMap.Add(new XElement("CAPABILITY", itcapObjMap.CAPABILITY.NAME.TrimEnd().Replace(' ', '~')));
                    tempITCAPObjMap.Add(new XElement("BUSINESSOBJECTIVE", itcapObjMap.BUSINESSOBJECTIVE.NAME.TrimEnd().Replace(' ', '~')));
                    tempITCAPObjMap.Add(new XElement("SCORE", itcapObjMap.SCORE));
                    itcapObjMapElement.Add(tempITCAPObjMap);
                }
                temp.Add(itcapObjMapElement);

                clientElement.Add(temp);
            }
            root.Add(clientElement);

            List<CATEGORY> catList = GetCategories();
            XElement catElement = new XElement("CATEGORIES");
            foreach (CATEGORY category in catList)
            {
                XElement temp = new XElement("CATEGORY");
                temp.Add(new XElement("NAME", category.NAME.TrimEnd().Replace(' ', '~')));

                XElement busElement = new XElement("BUSINESSOBJECTIVES");
                foreach (BUSINESSOBJECTIVE objective in category.BUSINESSOBJECTIVE)
                {
                    XElement tempBus = new XElement("BUSINESSOBJECTIVE");
                    tempBus.Add(new XElement("NAME", objective.NAME.TrimEnd().Replace(' ', '~')));

                    XElement iniElement = new XElement("INITIATIVES");
                    foreach (INITIATIVE initiative in objective.INITIATIVE)
                    {
                        XElement tempIni = new XElement("INITIATIVE");
                        tempIni.Add(new XElement("NAME", initiative.NAME.TrimEnd().Replace(' ', '~')));
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

                XElement capElement = new XElement("CAPABILITIES");
                foreach (CAPABILITY capability in domain.CAPABILITY)
                {
                    XElement tempCap = new XElement("CAPABILITY");
                    tempCap.Add(new XElement("NAME", capability.NAME.TrimEnd().Replace(' ', '~')));
                    tempCap.Add(new XElement("DEFAULT", capability.DEFAULT));

                    XElement questionElement = new XElement("ITCAPQUESTIONS");
                    foreach (ITCAPQUESTION itcapQuestion in capability.ITCAPQUESTION)
                    {
                        XElement tempItcq = new XElement("ITCAPQUESTION");
                        tempItcq.Add(new XElement("NAME", itcapQuestion.NAME.TrimEnd().Replace(' ', '~')));
                        tempItcq.Add(new XElement("DEFAULT", itcapQuestion.DEFAULT));
                        questionElement.Add(tempItcq);
                    }
                    tempCap.Add(questionElement);

                    capElement.Add(tempCap);
                }
                temp.Add(capElement);

                domElement.Add(temp);
            }
            root.Add(domElement);

            if (!Directory.Exists("Resources"))
            {
                Directory.CreateDirectory("Resources");
            }

            root.Save("Resources/Data.xml");
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
            CUPEQUESTION cupeQuestion;
            CUPE cupe;
            DOMAIN domain;
            CAPABILITY capability;
            ITCAPQUESTION itcapQuestion;
            ITCAP itcap;
            ITCAPOBJMAP itcapObjMap;

            if (!Directory.Exists("Resources"))
            {
                Directory.CreateDirectory("Resources");
            }

            else if (!File.Exists("Resources/Changes.log"))
            {
                File.Create("Resources/Changes.log");
            }

            using (System.IO.StreamReader file = new System.IO.StreamReader("Resources/Changes.log"))
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
                                else
                                {
                                    MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                    success = false;
                                }
                                break;

                            case "DOMAIN":
                                domain = new DOMAIN();
                                domain.NAME = lineArray[2].Replace('~', ' ');
                                domain.DEFAULT = "N";
                                AddDomain(domain);
                                break;

                            case "CAPABILITY":
                                if (GetDomain(lineArray[3].Replace('~', ' '), out domain))
                                {
                                    capability = new CAPABILITY();
                                    capability.NAME = lineArray[2].Replace('~', ' ');
                                    capability.DEFAULT = "N";
                                    capability.DOMAIN = domain;
                                    AddCapability(capability);
                                }
                                break;

                            case "ITCAPQUESTION":
                                if (GetCapability(lineArray[3].Replace('~', ' '), out capability))
                                {
                                    itcapQuestion = new ITCAPQUESTION();
                                    itcapQuestion.NAME = lineArray[2].Replace('~', ' ');
                                    itcapQuestion.DEFAULT = "N";
                                    itcapQuestion.CAPABILITY = capability;
                                    AddITCAPQuestion(itcapQuestion);
                                }
                                break;

                            case "ITCAP":
                                if (lineArray[2] == "CLIENT")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetITCAPQuestion(lineArray[4].Replace('~', ' '), out itcapQuestion))
                                        {
                                            itcap = new ITCAP();
                                            itcap.ITCAPQUESTION = itcapQuestion;
                                            AddITCAP(itcap, client);
                                        }
                                    }
                                }
                                else if (lineArray[2] == "GROUP")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                        {
                                            if (GetITCAPQuestion(lineArray[4].Replace('~', ' '), out itcapQuestion))
                                            {
                                                itcap = new ITCAP();
                                                itcap.ITCAPQUESTION = itcapQuestion;
                                                AddITCAPToGroup(itcap, grp);
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
                                                if (GetITCAPQuestion(lineArray[4].Replace('~', ' '), out itcapQuestion))
                                                {
                                                    itcap = new ITCAP();
                                                    itcap.ITCAPQUESTION = itcapQuestion;
                                                    AddITCAPToContact(itcap, contact);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                    success = false;
                                }
                                break;

                            case "ITCAPOBJMAP":
                                if(GetClient(lineArray[3], out client))
                                {
                                    int temp;
                                    if(!GetITCAPOBJMAPScore(client, lineArray[4], lineArray[5], out temp))
                                    {
                                        if(!GetCapability(lineArray[4], out capability))
                                        {
                                            MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                            success = false;
                                        }

                                        if(!GetObjective(lineArray[5], out objective))
                                        {
                                            MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                            success = false;
                                        }

                                        itcapObjMap = new ITCAPOBJMAP();
                                        itcapObjMap.CLIENT = client;
                                        itcapObjMap.CAPABILITY = capability;
                                        itcapObjMap.BUSINESSOBJECTIVE = objective;
                                        itcapObjMap.SCORE = 0;

                                    }
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                success = false;
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
                           case "ITCAP":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetITCAP(lineArray[3].Replace('~', ' '), client, out itcap))
                                    {
                                        itcap.ASIS = Convert.ToSingle(lineArray[4]);
                                        itcap.TOBE = Convert.ToSingle(lineArray[5]);
                                        itcap.COMMENT = lineArray[6];
                                    }
                                }
                                break;
                            case "ITCAPOBJMAP":
                                if(GetClient(lineArray[3], out client))
                                {
                                    if(GetITCAPOBJMAP(client, lineArray[4], lineArray[5], out itcapObjMap))
                                    {
                                        itcapObjMap.SCORE = Convert.ToInt32(lineArray[6]);
                                    }
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid instruction detected: \n" + line, "Error");
                                success = false;
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
                File.WriteAllText("Resources/Changes.log", string.Empty);
            }
        }
        #endregion
    }
}