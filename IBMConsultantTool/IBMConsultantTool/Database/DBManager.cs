﻿using System;
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

            AddGroup("Business", client);
            AddGroup("IT", client);

            dbo.AddToCLIENT(client);

            return true;
        }

        //Used by ClientDataControl
        public override Client AddClient(Client client)
        {
            CLIENT clientEnt = new CLIENT();
            
            clientEnt.NAME = client.Name.TrimEnd();
            
            clientEnt.LOCATION = client.Location;

            string regionName = client.Region;
            REGION region;
            try
            {
                region = (from ent in dbo.REGION
                          where ent.NAME.TrimEnd() == regionName
                          select ent).Single();
            }
            catch
            {
                region = new REGION();
                region.NAME = client.Region;
                dbo.AddToREGION(region);
            }
            clientEnt.REGION = region;

            string busTypeName = client.BusinessType;
            BUSINESSTYPE busType;
            try
            {
                busType = (from ent in dbo.BUSINESSTYPE
                          where ent.NAME.TrimEnd() == busTypeName
                          select ent).Single();
            }
            catch
            {
                busType = new BUSINESSTYPE();
                busType.NAME = client.BusinessType;
                dbo.AddToBUSINESSTYPE(busType);
            }
            clientEnt.BUSINESSTYPE = busType;

            clientEnt.STARTDATE = client.StartDate;
            clientEnt.BOMCOMPLETE = clientEnt.CUPECOMPLETE = clientEnt.ITCAPCOMPLETE = "N";

            if (!AddClient(clientEnt))
            {
                return null;
            }

            dbo.AddToCLIENT(clientEnt);

            client.EntityObject = clientEnt;
            return client;
        }

        //Used by ClientDataControl
        public override Client LoadClient(string clientName)
        {
            CLIENT clientEnt;

            if (!GetClient(clientName, out clientEnt))
            {
                return null;
            }

            Client client = new Client();

            client.Name = clientEnt.NAME;
            client.Location = clientEnt.LOCATION.TrimEnd();
            client.Region = clientEnt.REGION.NAME.TrimEnd();
            client.BusinessType = clientEnt.BUSINESSTYPE.NAME.TrimEnd();
            client.StartDate = clientEnt.STARTDATE;
            client.EntityObject = clientEnt;
            client.BomCompleted = clientEnt.BOMCOMPLETE == "Y";
            client.CupeCompleted = clientEnt.CUPECOMPLETE == "Y";
            client.ITCapCompleted = clientEnt.ITCAPCOMPLETE == "Y";
            return client;
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

        public override void ClientCompletedBOM(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            client.BOMCOMPLETE = "Y";
        }
        public override void ClientCompletedCUPE(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            client.CUPECOMPLETE = "Y";
        }
        public override void ClientCompletedITCAP(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            client.ITCAPCOMPLETE = "Y";
        }
        #endregion

        #region Region
        public override List<string> GetRegionNames()
        {
            return (from ent in dbo.REGION
                    select ent.NAME.TrimEnd()).ToList();
        }
        public override bool AddRegion(string regName)
        {
            //If already in DB, return false
            if ((from ent in dbo.REGION
                 where ent.NAME.TrimEnd() == regName
                 select ent).Count() != 0)
            {
                return false;
            }

            REGION region = new REGION();
            region.NAME = regName;
            dbo.AddToREGION(region);

            return true;
        }
        #endregion

        #region BusinessType
        public override List<string> GetBusinessTypeNames()
        {
            return (from ent in dbo.BUSINESSTYPE
                    select ent.NAME.TrimEnd()).ToList();
        }
        public override bool AddBusinessType(string busTypeName)
        {
            //If already in DB, return false
            if ((from ent in dbo.BUSINESSTYPE
                 where ent.NAME.TrimEnd() == busTypeName
                 select ent).Count() != 0)
            {
                return false;
            }

            BUSINESSTYPE busType = new BUSINESSTYPE();
            busType.NAME = busTypeName;
            dbo.AddToBUSINESSTYPE(busType);

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

        public override void LoadParticipants()
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            GROUP busGrp;
            GROUP itGrp;
            GetGroup("Business", client, out busGrp);
            GetGroup("IT", client, out itGrp);
            Person person;
            CupeData cupeData;
            int id = 1;
            int questionIndex = 0;
            foreach (CONTACT contact in busGrp.CONTACT)
            {
                person = new Person();
                person.Name = contact.NAME.TrimEnd();
                person.Email = contact.EMAIL.TrimEnd();
                person.Type = Person.EmployeeType.Business;
                person.ID = id;
                cupeData = new CupeData(id);
                foreach (CUPERESPONSE response in contact.CUPERESPONSE)
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                                                                              {
                                                                                  return question.QuestionText == response.CUPE.NAME.TrimEnd();
                                                                              });
                    if (questionIndex != -1)
                    {
                        cupeData.CurrentAnswers.Add("Question " + questionIndex.ToString(), response.CURRENT[0]);
                        cupeData.FutureAnswers.Add("Question " + questionIndex.ToString(), response.FUTURE[0]);
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }

            foreach (CONTACT contact in itGrp.CONTACT)
            {
                person = new Person();
                person.Name = contact.NAME.TrimEnd();
                person.Email = contact.EMAIL.TrimEnd();
                person.Type = Person.EmployeeType.IT;
                person.ID = id;
                cupeData = new CupeData(id);
                foreach (CUPERESPONSE response in contact.CUPERESPONSE)
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                                                                              {
                                                                                  return question.QuestionText == response.CUPE.NAME.TrimEnd();
                                                                              });
                    if (questionIndex != -1)
                    {
                        cupeData.CurrentAnswers.Add("Question " + questionIndex.ToString(), response.CURRENT[0]);
                        cupeData.FutureAnswers.Add("Question " + questionIndex.ToString(), response.FUTURE[0]);
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }
        }

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

        public override void BuildBOMForm(BOMTool bomForm)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;

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


        public override bool OpenITCAP(ITCapTool itcapForm)
        {
            List<ITCAP> itcapList = (ClientDataControl.Client.EntityObject as CLIENT).ITCAP.ToList();

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
                //itcapQuestion.AddComment(itcap.COMMENT);
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
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
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
                            //itcapEnt.COMMENT = "";
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
            if (!AddBOM(bom, ClientDataControl.Client.EntityObject))
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
        public bool GetCUPEQuestion(string cqName, out CUPEQUESTION cupeQuestion)
        {
            try
            {
                cupeQuestion = (from ent in dbo.CUPEQUESTION
                                where ent.NAME.TrimEnd() == cqName
                                select ent).Single();
            }

            catch
            {
                cupeQuestion = null;
                return false;
            }

            return true;
        }
        public override List<CupeQuestionData> GetCUPEQuestionData()
        {
            List<CUPEQUESTION> cupeQuestionEntList = (from ent in dbo.CUPEQUESTION
                                                      select ent).ToList();

            List<CupeQuestionData> cupeQuestionDataList = new List<CupeQuestionData>();
            CupeQuestionData cupeQuestionData;
            CupeQuestionStringData cupeQuestionStringData;
            foreach (CUPEQUESTION cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestionStringData = new CupeQuestionStringData();
                cupeQuestionStringData.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestionStringData.QuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestionStringData.ChoiceA = cupeQuestionEnt.COMMODITY.TrimEnd();
                cupeQuestionStringData.ChoiceB = cupeQuestionEnt.UTILITY.TrimEnd();
                cupeQuestionStringData.ChoiceC = cupeQuestionEnt.PARTNER.TrimEnd();
                cupeQuestionStringData.ChoiceD = cupeQuestionEnt.ENABLER.TrimEnd();
                cupeQuestionData = new CupeQuestionData();
                cupeQuestionData.StringData = cupeQuestionStringData;
                cupeQuestionData.InDefault20 = cupeQuestionEnt.INTWENTY == "Y";
                cupeQuestionData.InDefault15 = cupeQuestionEnt.INFIFTEEN == "Y";
                cupeQuestionData.InDefault10 = cupeQuestionEnt.INTEN == "Y";

                cupeQuestionDataList.Add(cupeQuestionData);
            }

            return cupeQuestionDataList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionStringData()
        {
            List<CUPEQUESTION> cupeQuestionEntList = (from ent in dbo.CUPEQUESTION
                                                     select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (CUPEQUESTION cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.QuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.ChoiceA = cupeQuestionEnt.COMMODITY.TrimEnd();
                cupeQuestion.ChoiceB = cupeQuestionEnt.UTILITY.TrimEnd();
                cupeQuestion.ChoiceC = cupeQuestionEnt.PARTNER.TrimEnd();
                cupeQuestion.ChoiceD = cupeQuestionEnt.ENABLER.TrimEnd();
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionStringDataTwenty()
        {
            List<CUPEQUESTION> cupeQuestionEntList = (from ent in dbo.CUPEQUESTION
                                                      where ent.INTWENTY == "Y"
                                                      select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (CUPEQUESTION cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.QuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.ChoiceA = cupeQuestionEnt.COMMODITY.TrimEnd();
                cupeQuestion.ChoiceB = cupeQuestionEnt.UTILITY.TrimEnd();
                cupeQuestion.ChoiceC = cupeQuestionEnt.PARTNER.TrimEnd();
                cupeQuestion.ChoiceD = cupeQuestionEnt.ENABLER.TrimEnd();
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionStringDataFifteen()
        {
            List<CUPEQUESTION> cupeQuestionEntList = (from ent in dbo.CUPEQUESTION
                                                      where ent.INFIFTEEN == "Y"
                                                      select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (CUPEQUESTION cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.QuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.ChoiceA = cupeQuestionEnt.COMMODITY.TrimEnd();
                cupeQuestion.ChoiceB = cupeQuestionEnt.UTILITY.TrimEnd();
                cupeQuestion.ChoiceC = cupeQuestionEnt.PARTNER.TrimEnd();
                cupeQuestion.ChoiceD = cupeQuestionEnt.ENABLER.TrimEnd();
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }

        public override List<CupeQuestionStringData> GetCUPEQuestionStringDataTen()
        {
            List<CUPEQUESTION> cupeQuestionEntList = (from ent in dbo.CUPEQUESTION
                                                      where ent.INTEN == "Y"
                                                      select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (CUPEQUESTION cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.QuestionText = cupeQuestionEnt.NAME.TrimEnd();
                cupeQuestion.ChoiceA = cupeQuestionEnt.COMMODITY.TrimEnd();
                cupeQuestion.ChoiceB = cupeQuestionEnt.UTILITY.TrimEnd();
                cupeQuestion.ChoiceC = cupeQuestionEnt.PARTNER.TrimEnd();
                cupeQuestion.ChoiceD = cupeQuestionEnt.ENABLER.TrimEnd();
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override bool AddCupeQuestion(CupeQuestionStringData cupeQuestion)
        {
            if ((from ent in dbo.CUPEQUESTION
                 where ent.NAME.TrimEnd() == cupeQuestion.QuestionText
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding question: Question already exists", "Error");
                return false;
            }

            CUPEQUESTION cupeQuestionEnt = new CUPEQUESTION();
            cupeQuestionEnt.NAME = cupeQuestion.QuestionText;
            cupeQuestionEnt.COMMODITY = cupeQuestion.ChoiceA;
            cupeQuestionEnt.UTILITY = cupeQuestion.ChoiceB;
            cupeQuestionEnt.PARTNER = cupeQuestion.ChoiceC;
            cupeQuestionEnt.ENABLER = cupeQuestion.ChoiceD;
            cupeQuestionEnt.INTWENTY = cupeQuestionEnt.INFIFTEEN = cupeQuestionEnt.INTEN = "N";

            dbo.AddToCUPEQUESTION(cupeQuestionEnt);

            return true;
        }
        public override bool UpdateCupeQuestion(string cupeQuestion, bool inTwenty, bool inFifteen, bool inTen)
        {
            CUPEQUESTION cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.CUPEQUESTION
                                   where ent.NAME.TrimEnd() == cupeQuestion
                                   select ent).Single();

                cupeQuestionEnt.INTWENTY = inTwenty ? "Y" : "N";
                cupeQuestionEnt.INFIFTEEN = inFifteen ? "Y" : "N";
                cupeQuestionEnt.INTEN = inTen ? "Y" : "N";
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

        public override void ClearCUPE(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            foreach (CUPE cupe in client.CUPE)
            {
                foreach (CUPERESPONSE cr in cupe.CUPERESPONSE)
                {
                    dbo.DeleteObject(cr);
                }

                dbo.DeleteObject(cupe);
            }
        }

        public bool GetCUPE(string name, CLIENT client, out CUPE cupe)
        {
            try
            {
                cupe = (from ent in client.CUPE
                        where ent.NAME.TrimEnd() == name
                        select ent).Single();

                return true;
            }

            catch
            {
                cupe = null;
                return false;
            }
        }

        public override void SaveCUPEParticipants()
        {
            List<Person> personList = ClientDataControl.GetParticipants();
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            GROUP busGrp;
            GROUP itGrp;
            if(!GetGroup("Business", client, out busGrp))
            {
                AddGroup("Business", client);
                GetGroup("Business", client, out busGrp);
            }
            if(!GetGroup("IT", client, out itGrp))
            {
                AddGroup("IT", client);
                GetGroup("IT", client, out itGrp);
            }
            CONTACT contact;
            CUPERESPONSE response;
            CUPE cupe;
            foreach (Person person in personList)
            {
                if (person.Type == Person.EmployeeType.Business)
                {
                    if (GetContact(person.Name, busGrp, out contact))
                    {
                        contact.EMAIL = person.Email;
                    }

                    else
                    {
                        contact = new CONTACT();
                        contact.NAME = person.Name;
                        contact.EMAIL = person.Email;
                        busGrp.CONTACT.Add(contact);
                        dbo.AddToCONTACT(contact);
                    }

                    //Clear out responses in case survey is different
                    List<CUPERESPONSE> responseList = contact.CUPERESPONSE.ToList();
                    foreach (CUPERESPONSE responseToDelete in responseList)
                    {
                        dbo.DeleteObject(responseToDelete);
                    }

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for (int i = 0; i < questionList.Count - 1; i++)
                    {
                        CupeQuestionStringData data = questionList[i];
                        if (!GetCUPE(data.QuestionText, client, out cupe))
                        {
                            MessageBox.Show("Error: couldn't find cupe: " + data.QuestionText);
                            continue;
                        }
                        response = new CUPERESPONSE();
                        response.CONTACT = contact;
                        response.CUPE = cupe;
                        dbo.AddToCUPERESPONSE(response);

                        response.CURRENT = person.cupeDataHolder.CurrentAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.CurrentAnswers["Question " + (i + 1).ToString()].ToString() : "";
                        response.FUTURE = person.cupeDataHolder.FutureAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.FutureAnswers["Question " + (i + 1).ToString()].ToString() : "";
                    }
                }

                else if(person.Type == Person.EmployeeType.IT)
                {
                    if (GetContact(person.Name, itGrp, out contact))
                    {
                        contact.EMAIL = person.Email;
                    }

                    else
                    {
                        contact = new CONTACT();
                        contact.NAME = person.Name;
                        contact.EMAIL = person.Email;
                        itGrp.CONTACT.Add(contact);
                        dbo.AddToCONTACT(contact);
                    }

                    //Clear out responses in case survey is different
                    List<CUPERESPONSE> responseList = contact.CUPERESPONSE.ToList();
                    foreach (CUPERESPONSE responseToDelete in responseList)
                    {
                        dbo.DeleteObject(responseToDelete);
                    }

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for(int i = 0; i < questionList.Count - 1; i++)
                    {
                        CupeQuestionStringData data = questionList[i];
                        if (!GetCUPE(data.QuestionText, client, out cupe))
                        {
                            MessageBox.Show("Error: couldn't find cupe: " + data.QuestionText);
                            continue;
                        }

                        response = new CUPERESPONSE();
                        response.CONTACT = contact;
                        response.CUPE = cupe;
                        dbo.AddToCUPERESPONSE(response);

                        response.CURRENT = person.cupeDataHolder.CurrentAnswers.ContainsKey("Question " + (i+1).ToString()) ? person.cupeDataHolder.CurrentAnswers["Question " + (i+1).ToString()].ToString() : "";
                        response.FUTURE = person.cupeDataHolder.FutureAnswers.ContainsKey("Question " + (i+1).ToString()) ? person.cupeDataHolder.FutureAnswers["Question " + (i+1).ToString()].ToString() : "";
                    }
                }
            }
        }

        public override List<CupeQuestionStringData> GetCUPESForClient()
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            List<CUPE> cupeList = client.CUPE.ToList();
            List<CupeQuestionStringData> cupeQuestions = new List<CupeQuestionStringData>();
            CupeQuestionStringData data;
            foreach (CUPE cupe in cupeList)
            {
                data = new CupeQuestionStringData();
                data.OriginalQuestionText = cupe.CUPEQUESTION.NAME.TrimEnd();
                data.QuestionText = cupe.NAME.TrimEnd();
                data.ChoiceA = cupe.COMMODITY.TrimEnd();
                data.ChoiceB = cupe.UTILITY.TrimEnd();
                data.ChoiceC = cupe.PARTNER.TrimEnd();
                data.ChoiceD = cupe.ENABLER.TrimEnd();
                cupeQuestions.Add(data);
            }

            return cupeQuestions;
        }

        public override bool UpdateCUPE(CupeQuestionStringData cupeQuestion)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            try
            {
                CUPE cupe = (from ent in client.CUPE
                             where ent.CUPEQUESTION.NAME.TrimEnd() == cupeQuestion.OriginalQuestionText
                             select ent).Single();

                cupe.NAME = cupeQuestion.QuestionText;
                cupe.COMMODITY = cupeQuestion.ChoiceA;
                cupe.UTILITY = cupeQuestion.ChoiceB;
                cupe.PARTNER = cupeQuestion.ChoiceC;
                cupe.ENABLER = cupeQuestion.ChoiceD;
            }

            catch
            {
                return false;
            }


            return true;
        }
        public override bool AddCUPE(string question, object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            CUPEQUESTION cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.CUPEQUESTION
                                   where ent.NAME.TrimEnd() == question
                                   select ent).Single();
            }

            catch(Exception e)
            {
                MessageBox.Show("Error retrieving CUPEQuestion:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in client.CUPE
                 where ent.CUPEQUESTION == cupeQuestionEnt
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            CUPE cupe = new CUPE();
            cupe.CUPEQUESTION = cupeQuestionEnt;
            cupe.NAME = cupeQuestionEnt.NAME;
            cupe.COMMODITY = cupeQuestionEnt.COMMODITY;
            cupe.UTILITY = cupeQuestionEnt.UTILITY;
            cupe.PARTNER = cupeQuestionEnt.PARTNER;
            cupe.ENABLER = cupeQuestionEnt.ENABLER;
            client.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
        public override bool AddCUPEToGroup(string question, object groupObj)
        {
            GROUP grp = groupObj as GROUP;
            CUPEQUESTION cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.CUPEQUESTION
                                   where ent.NAME.TrimEnd() == question
                                   select ent).Single();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error adding Cupe:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in grp.CUPE
                 where ent.CUPEQUESTION == cupeQuestionEnt
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            CUPE cupe = new CUPE();
            cupe.CUPEQUESTION = cupeQuestionEnt;
            cupe.NAME = cupeQuestionEnt.NAME;
            cupe.COMMODITY = cupeQuestionEnt.COMMODITY;
            cupe.UTILITY = cupeQuestionEnt.UTILITY;
            cupe.PARTNER = cupeQuestionEnt.PARTNER;
            cupe.ENABLER = cupeQuestionEnt.ENABLER;
            grp.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
        public override bool AddCUPEToContact(string question, object contactObj)
        {
            CONTACT contact = contactObj as CONTACT;
            CUPEQUESTION cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.CUPEQUESTION
                                   where ent.NAME.TrimEnd() == question
                                   select ent).Single();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error adding Cupe:\n\n" + e.Message, "Error");
                return false;
            }

            if ((from ent in contact.CUPE
                 where ent.CUPEQUESTION == cupeQuestionEnt
                 select ent).Count() != 0)
            {
                MessageBox.Show("Error adding Cupe: Cupe already exists", "Error");
                return false;
            }

            CUPE cupe = new CUPE();
            cupe.CUPEQUESTION = cupeQuestionEnt;
            cupe.NAME = cupeQuestionEnt.NAME;
            cupe.COMMODITY = cupeQuestionEnt.COMMODITY;
            cupe.UTILITY = cupeQuestionEnt.UTILITY;
            cupe.PARTNER = cupeQuestionEnt.PARTNER;
            cupe.ENABLER = cupeQuestionEnt.ENABLER;
            contact.CUPE.Add(cupe);

            dbo.AddToCUPE(cupe);

            return true;
        }
       
        public override void PopulateCUPEQuestionsForClient(CUPETool cupeForm)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            CupeQuestionStringData data = new CupeQuestionStringData();
            List<CupeQuestionStringData> templist = new List<CupeQuestionStringData>();
            if (client.CUPE.Count != 0)
            {
                foreach (CUPE cupe in client.CUPE)
                {
                    data.QuestionText = cupe.NAME.TrimEnd();
                    data.ChoiceA = cupe.COMMODITY.TrimEnd();
                    data.ChoiceB = cupe.UTILITY.TrimEnd();
                    data.ChoiceC = cupe.PARTNER.TrimEnd();
                    data.ChoiceD = cupe.ENABLER.TrimEnd();

                    templist.Add(data);
                    data = new CupeQuestionStringData();
                }
                ClientDataControl.cupeQuestions.RemoveRange(0, templist.Count);
                foreach( CupeQuestionStringData temp in templist)
                {
                    ClientDataControl.AddCupeQuestion(temp);
                }
                
            }

            else
            {
                foreach (CUPEQUESTION cupeQuestion in dbo.CUPEQUESTION)
                {
                    if (cupeQuestion.INTWENTY == "Y")
                    {
                        data.QuestionText = cupeQuestion.NAME.TrimEnd();
                        data.ChoiceA = cupeQuestion.COMMODITY.TrimEnd();
                        data.ChoiceB = cupeQuestion.UTILITY.TrimEnd();
                        data.ChoiceC = cupeQuestion.PARTNER.TrimEnd();
                        data.ChoiceD = cupeQuestion.ENABLER.TrimEnd();

                        ClientDataControl.AddCupeQuestion(data);
                        ClientDataControl.cupeQuestions.RemoveAt(1);
                        data = new CupeQuestionStringData();
                    }
                }
            }
        }
        #endregion

        #region CUPEResponse
        public bool GetCUPEResponse(string cupeName, CONTACT contact, out CUPERESPONSE response)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            try
            {
                response = (from ent in contact.CUPERESPONSE
                            where ent.CUPE.NAME.TrimEnd() == cupeName  
                            select ent).Single();
            }

            catch
            {
                response = null;
                return false;
            }

            return true;
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

        #region CapabilityGapInfo
        public bool GetCapabilityGapInfo(string capName, CLIENT client, out CAPABILITYGAPINFO capGapInfo)
        {
            try
            {
                capGapInfo = (from ent in client.CAPABILITYGAPINFO
                              where ent.CAPABILITY.NAME.TrimEnd() == capName
                              select ent).Single();

                return true;
            }

            catch
            {
                capGapInfo = null;
                return false;
            }              
        }
        public override void SaveCapabilityGapInfo(Capability capability)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            CAPABILITYGAPINFO capGapInfo;
            if(!GetCapabilityGapInfo(capability.Name, client, out capGapInfo))
            {
                capGapInfo = new CAPABILITYGAPINFO();
                capGapInfo.CLIENT = ClientDataControl.Client.EntityObject as CLIENT;
                CAPABILITY capabilityEnt;
                GetCapability(capability.Name, out capabilityEnt);
                capGapInfo.CAPABILITY = capabilityEnt;
                dbo.AddToCAPABILITYGAPINFO(capGapInfo);
            }
            switch(capability.GapType1)
            {
                case ScoringEntity.GapType.High:
                    capGapInfo.GAPTYPE = "High";
                    break;

                case ScoringEntity.GapType.Middle:
                    capGapInfo.GAPTYPE = "Middle";
                    break;

                case ScoringEntity.GapType.Low:
                    capGapInfo.GAPTYPE = "Low";
                    break;

                case ScoringEntity.GapType.None:
                    capGapInfo.GAPTYPE = "None";
                    break;
            }

            switch (capability.PrioritizedGapType1)
            {
                case ScoringEntity.PrioritizedGapType.High:
                    capGapInfo.PRIORITIZEDGAPTYPE = "High";
                    break;

                case ScoringEntity.PrioritizedGapType.Middle:
                    capGapInfo.PRIORITIZEDGAPTYPE = "Middle";
                    break;

                case ScoringEntity.PrioritizedGapType.Low:
                    capGapInfo.PRIORITIZEDGAPTYPE = "Low";
                    break;

                case ScoringEntity.PrioritizedGapType.None:
                    capGapInfo.PRIORITIZEDGAPTYPE = "None";
                    break;
            }

            capGapInfo.GAP = capability.CapabilityGap;
            capGapInfo.PRIORITIZEDGAP = capability.PrioritizedCapabilityGap;
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

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm, out int alreadyExists, out string owner)
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
                        alreadyExists = 0;
                        owner = "";
                        if (!AddDomain(domain))
                        {
                            MessageBox.Show("Failed to add Domain to Database", "Error");
                            return;
                        }
                    }

                    else
                    {
                        alreadyExists = 1;
                        owner = domName;
                    }

                    capability.DOMAIN = domain;
                    if (!AddCapability(capability))
                    {
                        MessageBox.Show("Failed to add Capability to Database", "Error");
                        return;
                    }
                }

                else
                {
                    alreadyExists = 2;
                    owner = capName;
                }

                itcapQuestion.CAPABILITY = capability;
                if (!AddITCAPQuestion(itcapQuestion))
                {
                    MessageBox.Show("Failed to add ITCAPQuestion to Database", "Error");
                    return;
                }
            }

            else
            {
                alreadyExists = 3;
                owner = "";
            }

            ITCAP itcap = new ITCAP();
            itcap.ITCAPQUESTION = itcapQuestion;
            if (!AddITCAP(itcap, ClientDataControl.Client.EntityObject))
            {
                MessageBox.Show("Failed to add ITCAPQuestion to ITCAP", "Error");
                return;
            }
            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
                return;
            }
        }

        public override void RemoveQuestionToITCAP(string itcqName)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            ITCAP itcap;
            if (GetITCAP(itcqName, client, out itcap))
            {
                dbo.DeleteObject(itcap);
            }
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
                    dbo.Detach(itcapObjMap);
                    return false;
                }

                itcapObjMap.CAPABILITY = capability;

                if (!GetObjective(busName, out objective))
                {
                    MessageBox.Show("Could not create mapping: Objective not found", "Error");
                    dbo.Detach(itcapObjMap);
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

        #region TrendAnalysis
        public List<InitiativeTrendAnalysis> GetInitiativeTrendAnalysis(string iniName, string regName, string busTypeName, string fromDateStr, string toDateStr)
        {
            List<BOM> bomList;
            DateTime toDate;
            DateTime fromDate;
            if(!DateTime.TryParse(fromDateStr, out fromDate))
            {
                fromDate = DateTime.MinValue;
            }
            if(!DateTime.TryParse(toDateStr, out toDate))
            {
                toDate = DateTime.MaxValue;
            }
            if (regName != "All")
            {
                if (busTypeName != "All")
                {
                    bomList = GetBOMSForInitiativeRegionAndBusinessType(iniName, regName, busTypeName, fromDate, toDate);
                }

                else
                {
                    bomList = GetBOMSForInitiativeRegion(iniName, regName, fromDate, toDate);
                }
            }

            else
            {
                if (busTypeName != "All")
                {
                    bomList = GetBOMSForInitiativeBusinessType(iniName, busTypeName, fromDate, toDate);
                }

                else
                {
                    bomList = GetBOMSForInitiative(iniName, fromDate, toDate);
                }
            }

            List<InitiativeTrendAnalysis> itaList = new List<InitiativeTrendAnalysis>();
            InitiativeTrendAnalysis ita;
            CLIENT client;
            foreach (BOM bom in bomList)
            {
                if (bom.EFFECTIVENESS.HasValue && bom.CRITICALITY.HasValue && bom.DIFFERENTIAL.HasValue)
                {
                    ita = new InitiativeTrendAnalysis();
                    client = bom.CLIENT;
                    ita.Date = client.STARTDATE;
                    ita.Region = client.REGION.NAME.TrimEnd();
                    ita.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                    ita.Country = ita.Region;
                    ita.Effectiveness = bom.EFFECTIVENESS.Value;
                    ita.Criticality = bom.CRITICALITY.Value;
                    ita.Differentiation = bom.DIFFERENTIAL.Value;
                    ita.Name = iniName;
                    itaList.Add(ita);
                }
            }

            return itaList;
        }

        public List<ITAttributeTrendAnalysis> GetITAttributeTrendAnalysis(string attName, string regName, string busTypeName, string fromDateStr, string toDateStr)
        {
            List<ITCAP> itcapList;
            DateTime toDate;
            DateTime fromDate;
            if (!DateTime.TryParse(fromDateStr, out fromDate))
            {
                fromDate = DateTime.MinValue;
            }
            if (!DateTime.TryParse(toDateStr, out toDate))
            {
                toDate = DateTime.MaxValue;
            }
            if (regName != "All")
            {
                if (busTypeName != "All")
                {
                    itcapList = GetITCAPSForAttributeRegionAndBusinessType(attName, regName, busTypeName, fromDate, toDate);
                }

                else
                {
                    itcapList = GetITCAPSForAttributeRegion(attName, regName, fromDate, toDate);
                }
            }

            else
            {
                if (busTypeName != "All")
                {
                    itcapList = GetITCAPSForAttributeBusinessType(attName, busTypeName, fromDate, toDate);
                }

                else
                {
                    itcapList = GetITCAPSForAttribute(attName, fromDate, toDate);
                }
            }

            List<ITAttributeTrendAnalysis> itataList = new List<ITAttributeTrendAnalysis>();
            ITAttributeTrendAnalysis itata;
            CLIENT client;
            foreach (ITCAP itcap in itcapList)
            {
                if (itcap.ASIS.HasValue && itcap.TOBE.HasValue)
                {
                    itata = new ITAttributeTrendAnalysis();
                    client = itcap.CLIENT;
                    itata.Date = client.STARTDATE;
                    itata.Region = client.REGION.NAME.TrimEnd();
                    itata.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                    itata.Country = itata.Region;
                    itata.AsisScore = itcap.ASIS.Value;
                    itata.TobeScore = itcap.TOBE.Value;
                    itata.Name = attName;
                    itataList.Add(itata);
                }
            }

            return itataList;
        }

        public List<CUPEQuestionTrendAnalysis> GetCUPEQuestionTrendAnalysis(string cqName, string regName, string busTypeName, string fromDateStr, string toDateStr)
        {
            List<CUPERESPONSE> crList;
            DateTime toDate;
            DateTime fromDate;
            if (!DateTime.TryParse(fromDateStr, out fromDate))
            {
                fromDate = DateTime.MinValue;
            }
            if (!DateTime.TryParse(toDateStr, out toDate))
            {
                toDate = DateTime.MaxValue;
            }
            if (regName != "All")
            {
                if (busTypeName != "All")
                {
                    crList = GetCUPEResponsesForCUPEQuestionRegionAndBusinessType(cqName, regName, busTypeName, fromDate, toDate);
                }

                else
                {
                    crList = GetCUPEResponsesForCUPEQuestionRegion(cqName, regName, fromDate, toDate);
                }
            }

            else
            {
                if (busTypeName != "All")
                {
                    crList = GetCUPEResponsesForCUPEQuestionBusinessType(cqName, busTypeName, fromDate, toDate);
                }

                else
                {
                    crList = GetCUPEResponsesForCUPEQuestion(cqName, fromDate, toDate);
                }
            }

            List<CUPEQuestionTrendAnalysis> cqtaList = new List<CUPEQuestionTrendAnalysis>();
            CUPEQuestionTrendAnalysis cqta;
            CLIENT client;
            foreach (CUPERESPONSE cr in crList)
            {
                if (cr.CURRENT != "" && cr.FUTURE != "")
                {
                    cqta = new CUPEQuestionTrendAnalysis();
                    client = cr.CONTACT.GROUP.CLIENT;
                    cqta.Date = client.STARTDATE;
                    cqta.Region = client.REGION.NAME.TrimEnd();
                    cqta.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                    cqta.Country = cqta.Region;
                    cqta.CupeType = cr.CONTACT.GROUP.NAME;
                    switch (cr.CURRENT)
                    {
                        case "a":
                            cqta.CurrentAnswer = 1;
                            break;
                        case "b":
                            cqta.CurrentAnswer = 2;
                            break;
                        case "c":
                            cqta.CurrentAnswer = 3;
                            break;
                        case "d":
                            cqta.CurrentAnswer = 4;
                            break;
                    }
                    switch (cr.FUTURE)
                    {
                        case "a":
                            cqta.FutureAnswer = 1;
                            break;
                        case "b":
                            cqta.FutureAnswer = 2;
                            break;
                        case "c":
                            cqta.FutureAnswer = 3;
                            break;
                        case "d":
                            cqta.FutureAnswer = 4;
                            break;
                    }
                    cqta.Name = cqName;
                    cqtaList.Add(cqta);
                }
            }

            return cqtaList;
        }
        public List<CapabilityTrendAnalysis> GetCapabilityTrendAnalysis(string capName, string regName, string busTypeName, string fromDateStr, string toDateStr)
        {
            List<CAPABILITYGAPINFO> capGapInfoList;
            DateTime toDate;
            DateTime fromDate;
            if (!DateTime.TryParse(fromDateStr, out fromDate))
            {
                fromDate = DateTime.MinValue;
            }
            if (!DateTime.TryParse(toDateStr, out toDate))
            {
                toDate = DateTime.MaxValue;
            }
            if (regName != "All")
            {
                if (busTypeName != "All")
                {
                    capGapInfoList = GetCapabilityGapInfosFromCapabilityRegionAndBusinessType(capName, regName, busTypeName, fromDate, toDate);
                }

                else
                {
                    capGapInfoList = GetCapabilityGapInfosFromCapabilityRegion(capName, regName, fromDate, toDate);
                }
            }

            else
            {
                if (busTypeName != "All")
                {
                    capGapInfoList = GetCapabilityGapInfosFromCapabilityBusinessType(capName, busTypeName, fromDate, toDate);
                }

                else
                {
                    capGapInfoList = GetCapabilityGapInfosFromCapability(capName, fromDate, toDate);
                }
            }

            List<CapabilityTrendAnalysis> ctaList = new List<CapabilityTrendAnalysis>();
            CapabilityTrendAnalysis cta;
            CLIENT client;
            foreach (CAPABILITYGAPINFO capGapInfo in capGapInfoList)
            {
                if (capGapInfo.GAP.HasValue && capGapInfo.PRIORITIZEDGAP.HasValue)
                {
                    cta = new CapabilityTrendAnalysis();
                    client = capGapInfo.CLIENT;
                    cta.Date = client.STARTDATE;
                    cta.Region = client.REGION.NAME.TrimEnd();
                    cta.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                    cta.Country = cta.Region;
                    cta.CapabilityGap = capGapInfo.GAP.Value;
                    cta.PrioritizedCapabilityGap = capGapInfo.PRIORITIZEDGAP.Value;
                    cta.GapType = capGapInfo.GAPTYPE;
                    cta.PrioritizedGapType = capGapInfo.PRIORITIZEDGAPTYPE;
                    cta.Name = capName;
                    ctaList.Add(cta);
                }
            }

            return ctaList;
        }
        
        public List<string> GetObjectivesFromCategory(string catName)
        {
            CATEGORY category;
            if (GetCategory(catName, out category))
            {
                return ((from ent in category.BUSINESSOBJECTIVE
                         select ent.NAME.TrimEnd()).ToList());
            }

            else
            {
                return new List<string>();
            }
        }
        public List<string> GetInitiativesFromObjective(string busName)
        {
            BUSINESSOBJECTIVE objective;
            if (GetObjective(busName, out objective))
            {
                return ((from ent in objective.INITIATIVE
                         select ent.NAME.TrimEnd()).ToList());
            }

            else
            {
                return new List<string>();
            }
        }
        public List<string> GetCapabilitiesFromDomain(string domName)
        {
            DOMAIN domain;
            if (GetDomain(domName, out domain))
            {
                return ((from ent in domain.CAPABILITY
                         select ent.NAME.TrimEnd()).ToList());
            }

            else
            {
                return new List<string>();
            }
        }
        public List<string> GetAttributesFromCapability(string capName)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                return ((from ent in capability.ITCAPQUESTION
                         select ent.NAME.TrimEnd()).ToList());
            }

            else
            {
                return new List<string>();
            }
        }
        public List<BOM> GetBOMSForInitiative(string iniName, DateTime fromDate, DateTime toDate)
        {
            INITIATIVE initiative;
            if(GetInitiative(iniName, out initiative))
            {
                return (from ent in initiative.BOM
                        where ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForInitiativeRegion(string iniName, string regionName, DateTime fromDate, DateTime toDate)
        {
            INITIATIVE initiative;
            if (GetInitiative(iniName, out initiative))
            {
                return (from ent in initiative.BOM
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regionName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForInitiativeBusinessType(string iniName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            INITIATIVE initiative;
            if (GetInitiative(iniName, out initiative))
            {
                return (from ent in initiative.BOM
                        where ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForInitiativeRegionAndBusinessType(string iniName, string regName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            INITIATIVE initiative;
            if (GetInitiative(iniName, out initiative))
            {
                return (from ent in initiative.BOM
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regName &&
                              ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestionRegionAndBusinessType(string cqName, string regName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                return (from cupe in cupeQuestion.CUPE
                        where cupe.CLIENT.REGION != null &&
                              cupe.CLIENT.REGION.NAME == regName &&
                              cupe.CLIENT.BUSINESSTYPE != null &&
                              cupe.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              cupe.CLIENT.STARTDATE > fromDate &&
                              cupe.CLIENT.STARTDATE < toDate
                        from ent in cupe.CUPERESPONSE
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestionBusinessType(string cqName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                return (from cupe in cupeQuestion.CUPE
                        where cupe.CLIENT.BUSINESSTYPE != null &&
                              cupe.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              cupe.CLIENT.STARTDATE > fromDate &&
                              cupe.CLIENT.STARTDATE < toDate
                        from ent in cupe.CUPERESPONSE
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestionRegion(string cqName, string regName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                return (from cupe in cupeQuestion.CUPE
                        where cupe.CLIENT.REGION != null &&
                              cupe.CLIENT.REGION.NAME == regName &&
                              cupe.CLIENT.STARTDATE > fromDate &&
                              cupe.CLIENT.STARTDATE < toDate
                        from ent in cupe.CUPERESPONSE
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestion(string cqName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                return (from cupe in cupeQuestion.CUPE
                        where cupe.CLIENT.STARTDATE > fromDate &&
                              cupe.CLIENT.STARTDATE < toDate
                        from ent in cupe.CUPERESPONSE
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }

        public List<CUPE> GetCUPESForCUPEQuestion(string cqName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                return cq.CUPE.ToList();
 
            }

            catch
            {
                return null;
            }
        }
        public List<CUPE> GetCUPESForCUPEQuestionRegion(string cqName, string regionName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                return (from ent in cq.CUPE
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regionName
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<CUPE> GetCUPESForCUPEQuestionBusinessType(string cqName, string busTypeName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                return (from ent in cq.CUPE
                        where ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<CUPE> GetCUPESForCUPEQuestionRegionAndBusinessType(string cqName, string regName, string busTypeName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                return (from ent in cq.CUPE
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regName &&
                              ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }

        public List<ITCAP> GetITCAPSForAttribute(string itcqName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                      select ent).Single();

                return (from ent in itcq.ITCAP
                        where ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<ITCAP> GetITCAPSForAttributeRegion(string itcqName, string regionName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                        select ent).Single();

                return (from ent in itcq.ITCAP
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regionName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<ITCAP> GetITCAPSForAttributeBusinessType(string itcqName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                        select ent).Single();

                return (from ent in itcq.ITCAP
                        where ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<ITCAP> GetITCAPSForAttributeRegionAndBusinessType(string itcqName, string regName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                        select ent).Single();

                return (from ent in itcq.ITCAP
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regName &&
                              ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapability(string capName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                return (from ent in capability.CAPABILITYGAPINFO
                        where ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapabilityRegion(string capName, string regName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                return (from ent in capability.CAPABILITYGAPINFO
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapabilityBusinessType(string capName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                return (from ent in capability.CAPABILITYGAPINFO
                        where ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapabilityRegionAndBusinessType(string capName, string regName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                return (from ent in capability.CAPABILITYGAPINFO
                        where ent.CLIENT.REGION != null &&
                              ent.CLIENT.REGION.NAME == regName &&
                              ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
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
                temp.Add(new XElement("NAME", client.NAME.TrimEnd()));
                temp.Add(new XElement("LOCATION", client.LOCATION.TrimEnd()));
                temp.Add(new XElement("STARTDATE", client.STARTDATE.ToString()));
                temp.Add(new XElement("REGION", client.REGION.NAME.TrimEnd()));
                temp.Add(new XElement("BUSINESSTYPE", client.BUSINESSTYPE.NAME.TrimEnd()));
                temp.Add(new XElement("BOMCOMPLETE", client.BOMCOMPLETE));
                temp.Add(new XElement("CUPECOMPLETE", client.CUPECOMPLETE));
                temp.Add(new XElement("ITCAPCOMPLETE", client.ITCAPCOMPLETE));
                XElement grpElement = new XElement("GROUPS");
                foreach (GROUP grp in client.GROUP)
                {
                    XElement tempGrp = new XElement("GROUP");
                    tempGrp.Add(new XElement("NAME", grp.NAME.TrimEnd()));

                    XElement conElement = new XElement("CONTACTS");
                    foreach (CONTACT contact in grp.CONTACT)
                    {
                        XElement tempCon = new XElement("CONTACT");
                        tempCon.Add(new XElement("NAME", contact.NAME.TrimEnd()));

                        XElement bomConElement = new XElement("BOMS");
                        foreach (BOM bom in contact.BOM)
                        {
                            XElement tempBom = new XElement("BOM");
                            tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd()));
                            tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd()));
                            tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd()));
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
                            tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd()));
                            tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd()));
                            tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd()));
                            tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                            tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                            //tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                            itcapConElement.Add(tempItcap);
                        }
                        tempCon.Add(itcapConElement);

                        XElement cupeConElement = new XElement("CUPES");
                        foreach (CUPE cupe in contact.CUPE)
                        {
                            XElement tempCUPE = new XElement("CUPE");
                            tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd()));
                            tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd()));
                            tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY.TrimEnd()));
                            tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY.TrimEnd()));
                            tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER.TrimEnd()));
                            tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER.TrimEnd()));
                            itcapConElement.Add(tempCUPE);
                        }
                        tempCon.Add(cupeConElement);

                        XElement cupeResConElement = new XElement("CUPERESPONSES");
                        foreach (CUPERESPONSE cupeResponse in contact.CUPERESPONSE)
                        {
                            XElement tempCUPERes = new XElement("CUPERESPONSE");
                            tempCUPERes.Add(new XElement("CUPE", cupeResponse.CUPE.NAME.TrimEnd()));
                            tempCUPERes.Add(new XElement("CURRENT", cupeResponse.CURRENT));
                            tempCUPERes.Add(new XElement("FUTURE", cupeResponse.FUTURE));
                            cupeResConElement.Add(tempCUPERes);
                        }
                        tempCon.Add(cupeConElement);

                        conElement.Add(tempCon);
                    }

                    tempGrp.Add(conElement);

                    XElement bomGrpElement = new XElement("BOMS");
                    foreach (BOM bom in grp.BOM)
                    {
                        XElement tempBom = new XElement("BOM");
                        tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd()));
                        tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd()));
                        tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd()));
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
                        tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd()));
                        tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd()));
                        tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd()));
                        tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                        tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                        //tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                        itcapGrpElement.Add(tempItcap);
                    }
                    tempGrp.Add(itcapGrpElement);

                    XElement cupeGrpElement = new XElement("CUPES");
                    foreach (CUPE cupe in grp.CUPE)
                    {
                        XElement tempCUPE = new XElement("CUPE");
                        tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd()));
                        tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd()));
                        tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY.TrimEnd()));
                        tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY.TrimEnd()));
                        tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER.TrimEnd()));
                        tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER.TrimEnd()));
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
                    tempBom.Add(new XElement("INITIATIVE", bom.INITIATIVE.NAME.TrimEnd()));
                    tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd()));
                    tempBom.Add(new XElement("CATEGORY", bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd()));
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
                    tempItcap.Add(new XElement("ITCAPQUESTION", itcap.ITCAPQUESTION.NAME.TrimEnd()));
                    tempItcap.Add(new XElement("CAPABILITY", itcap.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd()));
                    tempItcap.Add(new XElement("DOMAIN", itcap.ITCAPQUESTION.CAPABILITY.DOMAIN.NAME.TrimEnd()));
                    tempItcap.Add(new XElement("ASIS", itcap.ASIS != null ? itcap.ASIS : 0));
                    tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                    //tempItcap.Add(new XElement("COMMENT", itcap.COMMENT));
                    itcapElement.Add(tempItcap);
                }
                temp.Add(itcapElement);

                XElement cupeElement = new XElement("CUPES");
                foreach (CUPE cupe in client.CUPE)
                {
                    XElement tempCUPE = new XElement("CUPE");
                    tempCUPE.Add(new XElement("CUPEQUESTION", cupe.CUPEQUESTION.NAME.TrimEnd()));
                    tempCUPE.Add(new XElement("NAME", cupe.NAME.TrimEnd()));
                    tempCUPE.Add(new XElement("COMMODITY", cupe.COMMODITY.TrimEnd()));
                    tempCUPE.Add(new XElement("UTILITY", cupe.UTILITY.TrimEnd()));
                    tempCUPE.Add(new XElement("PARTNER", cupe.PARTNER.TrimEnd()));
                    tempCUPE.Add(new XElement("ENABLER", cupe.ENABLER.TrimEnd()));
                    cupeElement.Add(tempCUPE);
                }
                temp.Add(cupeElement);

                XElement itcapObjMapElement = new XElement("ITCAPOBJMAPS");
                foreach (ITCAPOBJMAP itcapObjMap in client.ITCAPOBJMAP)
                {
                    XElement tempITCAPObjMap = new XElement("ITCAPOBJMAP");
                    tempITCAPObjMap.Add(new XElement("CAPABILITY", itcapObjMap.CAPABILITY.NAME.TrimEnd()));
                    tempITCAPObjMap.Add(new XElement("BUSINESSOBJECTIVE", itcapObjMap.BUSINESSOBJECTIVE.NAME.TrimEnd()));
                    tempITCAPObjMap.Add(new XElement("SCORE", itcapObjMap.SCORE));
                    itcapObjMapElement.Add(tempITCAPObjMap);
                }
                temp.Add(itcapObjMapElement);

                XElement capGapInfoElement = new XElement("CAPABILITYGAPINFOS");
                foreach (CAPABILITYGAPINFO capGapInfo in client.CAPABILITYGAPINFO)
                {
                    XElement tempCapGapInfo = new XElement("CAPABILITYGAPINFO");
                    tempCapGapInfo.Add(new XElement("CAPABILITY", capGapInfo.CAPABILITY.NAME.TrimEnd()));
                    tempCapGapInfo.Add(new XElement("GAP", capGapInfo.GAP));
                    tempCapGapInfo.Add(new XElement("GAPTYPE", capGapInfo.GAPTYPE.TrimEnd()));
                    tempCapGapInfo.Add(new XElement("PRIORITIZEDGAP", capGapInfo.PRIORITIZEDGAP));
                    tempCapGapInfo.Add(new XElement("PRIORITIZEDGAPTYPE", capGapInfo.PRIORITIZEDGAPTYPE.TrimEnd()));
                    capGapInfoElement.Add(tempCapGapInfo);
                }
                temp.Add(capGapInfoElement);

                clientElement.Add(temp);
            }
            root.Add(clientElement);

            List<string> regList = GetRegionNames();
            XElement regElement = new XElement("REGIONS");
            foreach (string regName in regList)
            {
                XElement tempReg = new XElement("REGION");
                tempReg.Add(new XElement("NAME", regName));
                regElement.Add(tempReg);
            }
            root.Add(regElement);

            List<string> busTypeList = GetBusinessTypeNames();
            XElement busTypeElement = new XElement("BUSINESSTYPES");
            foreach (string busTypeName in busTypeList)
            {
                XElement tempBusType = new XElement("BUSINESSTYPE");
                tempBusType.Add(new XElement("NAME", busTypeName));
                busTypeElement.Add(tempBusType);
            }
            root.Add(busTypeElement);

            List<CATEGORY> catList = GetCategories();
            XElement catElement = new XElement("CATEGORIES");
            foreach (CATEGORY category in catList)
            {
                XElement temp = new XElement("CATEGORY");
                temp.Add(new XElement("NAME", category.NAME.TrimEnd()));

                XElement busElement = new XElement("BUSINESSOBJECTIVES");
                foreach (BUSINESSOBJECTIVE objective in category.BUSINESSOBJECTIVE)
                {
                    XElement tempBus = new XElement("BUSINESSOBJECTIVE");
                    tempBus.Add(new XElement("NAME", objective.NAME.TrimEnd()));

                    XElement iniElement = new XElement("INITIATIVES");
                    foreach (INITIATIVE initiative in objective.INITIATIVE)
                    {
                        XElement tempIni = new XElement("INITIATIVE");
                        tempIni.Add(new XElement("NAME", initiative.NAME.TrimEnd()));
                        iniElement.Add(tempIni);
                    }
                    tempBus.Add(iniElement);

                    busElement.Add(tempBus);
                }
                temp.Add(busElement);

                catElement.Add(temp);
            }
            root.Add(catElement);

            List<CUPEQUESTION> cqList = dbo.CUPEQUESTION.ToList();
            XElement cqElement = new XElement("CUPEQUESTIONS");
            foreach (CUPEQUESTION cupeQuestion in cqList)
            {
                XElement tempCQ = new XElement("CUPEQUESTION");
                tempCQ.Add(new XElement("NAME", cupeQuestion.NAME.TrimEnd()));
                tempCQ.Add(new XElement("COMMODITY", cupeQuestion.COMMODITY.TrimEnd()));
                tempCQ.Add(new XElement("UTILITY", cupeQuestion.UTILITY.TrimEnd()));
                tempCQ.Add(new XElement("PARTNER", cupeQuestion.PARTNER.TrimEnd()));
                tempCQ.Add(new XElement("ENABLER", cupeQuestion.ENABLER.TrimEnd()));
                tempCQ.Add(new XElement("INTWENTY", cupeQuestion.INTWENTY));
                tempCQ.Add(new XElement("INFIFTEEN", cupeQuestion.INFIFTEEN));
                tempCQ.Add(new XElement("INTEN", cupeQuestion.INTEN));
                cqElement.Add(tempCQ);
            }
            root.Add(cqElement);

            List<DOMAIN> domList = GetDomains();
            XElement domElement = new XElement("DOMAINS");
            foreach (DOMAIN domain in domList)
            {
                XElement temp = new XElement("DOMAIN");
                temp.Add(new XElement("NAME", domain.NAME.TrimEnd()));
                temp.Add(new XElement("DEFAULT", domain.DEFAULT));

                XElement capElement = new XElement("CAPABILITIES");
                foreach (CAPABILITY capability in domain.CAPABILITY)
                {
                    XElement tempCap = new XElement("CAPABILITY");
                    tempCap.Add(new XElement("NAME", capability.NAME.TrimEnd()));
                    tempCap.Add(new XElement("DEFAULT", capability.DEFAULT));

                    XElement questionElement = new XElement("ITCAPQUESTIONS");
                    foreach (ITCAPQUESTION itcapQuestion in capability.ITCAPQUESTION)
                    {
                        XElement tempItcq = new XElement("ITCAPQUESTION");
                        tempItcq.Add(new XElement("NAME", itcapQuestion.NAME.TrimEnd()));
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
            List<string> failedChanges = new List<string>();

            string line;
            string[] lineArray;

            CLIENT client;
            REGION region;
            BUSINESSTYPE busType;
            GROUP grp;
            CONTACT contact;
            CATEGORY category;
            BUSINESSOBJECTIVE objective;
            INITIATIVE initiative;
            BOM bom;
            CUPEQUESTION cupeQuestion;
            CupeQuestionStringData cupeQuestionStringData;
            CUPE cupe;
            CUPERESPONSE cupeResponse;
            DOMAIN domain;
            CAPABILITY capability;
            ITCAPQUESTION itcapQuestion;
            ITCAP itcap;
            ITCAPOBJMAP itcapObjMap;
            CAPABILITYGAPINFO capGapInfo;

            if (!Directory.Exists("Resources"))
            {
                Directory.CreateDirectory("Resources");
            }

            else if (!File.Exists("Resources/Changes.log"))
            {
                FileStream file = File.Create("Resources/Changes.log");
                file.Close();
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
                                client.LOCATION = lineArray[3].Replace('~', ' ');

                                string regionName = lineArray[4].Replace('~', ' ');
                                try
                                {
                                    region = (from ent in dbo.REGION
                                              where ent.NAME.TrimEnd() == regionName
                                              select ent).Single();
                                }
                                catch
                                {
                                    region = new REGION();
                                    region.NAME = regionName;
                                    dbo.AddToREGION(region);
                                }
                                client.REGION = region;

                                client.STARTDATE = DateTime.Parse(lineArray[5].Replace('~', ' '));

                                string busTypeName = lineArray[6].Replace('~', ' ');
                                try
                                {
                                    busType = (from ent in dbo.BUSINESSTYPE
                                              where ent.NAME.TrimEnd() == busTypeName
                                              select ent).Single();
                                }
                                catch
                                {
                                    busType = new BUSINESSTYPE();
                                    busType.NAME = busTypeName;
                                    dbo.AddToBUSINESSTYPE(busType);
                                }
                                client.BUSINESSTYPE = busType;

                                client.BOMCOMPLETE = client.CUPECOMPLETE = client.ITCAPCOMPLETE = "N";

                                if (!AddClient(client))
                                {
                                    MessageBox.Show("Add Client Instruction Failed: Client already exists\n\n" + line, "Error");
                                }
                                break;

                            case "REGION":
                                if (!AddRegion(lineArray[2].Replace('~', ' ')))
                                {
                                    MessageBox.Show("Add Region Instruction Failed: Region already exists\n\n" + line, "Error");
                                }
                                break;

                            case "BUSINESSTYPE":
                                if (!AddBusinessType(lineArray[2].Replace('~', ' ')))
                                {
                                    MessageBox.Show("Add BusinessType Instruction Failed: BusinessType already exists\n\n" + line, "Error");
                                }
                                break;

                            case "GROUP":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    if (!AddGroup(lineArray[2].Replace('~', ' '), client))
                                    {
                                        MessageBox.Show("Add Group Instruction Failed: Group already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add Group Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;

                            case "CATEGORY":
                                category = new CATEGORY();
                                category.NAME = lineArray[2].Replace('~', ' ');
                                if (!AddCategory(category))
                                {
                                    MessageBox.Show("Add Category Instruction Failed: Category already exists\n\n" + line, "Error");
                                }
                                break;

                            case "BUSINESSOBJECTIVE":
                                if (GetCategory(lineArray[3].Replace('~', ' '), out category))
                                {
                                    objective = new BUSINESSOBJECTIVE();
                                    objective.NAME = lineArray[2].Replace('~', ' ');
                                    objective.CATEGORY = category;
                                    if (!AddObjective(objective))
                                    {
                                        MessageBox.Show("Add BusinessObjective Instruction Failed: BusinessObjective already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add BusinessObjective Instruction Failed: Category does not exist\n\n" + line, "Error");
                                }
                                break;

                            case "INITIATIVE":
                                if (GetObjective(lineArray[3].Replace('~', ' '), out objective))
                                {
                                    initiative = new INITIATIVE();
                                    initiative.NAME = lineArray[2].Replace('~', ' ');
                                    initiative.BUSINESSOBJECTIVE = objective;
                                    if (!AddInitiative(initiative))
                                    {
                                        MessageBox.Show("Add Initiative Instruction Failed: Initiative already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add Initiative Instruction Failed: BusinessObjective does not exist\n\n" + line, "Error");
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
                                            if (!AddBOM(bom, client))
                                            {
                                                MessageBox.Show("Add BOM Instruction Failed: BOM already exists\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add BOM Instruction Failed: Initiative does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add BOM Instruction Failed: Client does not exist\n\n" + line, "Error");
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
                                                if (!AddBOM(bom, grp))
                                                {
                                                    MessageBox.Show("Add BOM Instruction Failed: BOM already exists\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add BOM Instruction Failed: Initiative does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add BOM Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add BOM Instruction Failed: Client does not exist\n\n" + line, "Error");
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
                                                    if (!AddBOM(bom, contact))
                                                    {
                                                        MessageBox.Show("Add BOM Instruction Failed: BOM already exists\n\n" + line, "Error");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Add BOM Instruction Failed: Initiative does not exist\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add BOM Instruction Failed: Contact does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add BOM Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add BOM Instruction Failed: Client does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                }
                                break;

                            case "CUPERESPONSE":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                    {
                                        if (GetContact(lineArray[5].Replace('~', ' '), grp, out contact))
                                        {
                                            if (GetCUPE(lineArray[6].Replace('~', ' '), client, out cupe))
                                            {
                                                cupeResponse = new CUPERESPONSE();
                                                cupeResponse.CONTACT = contact;
                                                cupeResponse.CUPE = cupe;
                                                cupeResponse.CURRENT = lineArray[7];
                                                cupeResponse.FUTURE = lineArray[8];
                                                dbo.AddToCUPERESPONSE(cupeResponse);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add CUPEResponse Instruction Failed: CUPE does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add CUPEResponse Instruction Failed: Contact does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CUPEResponse Instruction Failed: Group does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add CUPEResponse Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;

                            case "CUPEQUESTION":
                                cupeQuestionStringData = new CupeQuestionStringData();
                                cupeQuestionStringData.QuestionText = lineArray[2].Replace('~', ' ');
                                cupeQuestionStringData.ChoiceA = lineArray[3].Replace('~', ' ');
                                cupeQuestionStringData.ChoiceB = lineArray[4].Replace('~', ' ');
                                cupeQuestionStringData.ChoiceC = lineArray[5].Replace('~', ' ');
                                cupeQuestionStringData.ChoiceD = lineArray[6].Replace('~', ' ');
                                AddCupeQuestion(cupeQuestionStringData);
                                break;

                            case "CUPE":
                                if (lineArray[2] == "CLIENT")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetCUPEQuestion(lineArray[4].Replace('~', ' '), out cupeQuestion))
                                        {
                                            if (!AddCUPE(cupeQuestion.NAME.TrimEnd(), client))
                                            {
                                                MessageBox.Show("Add CUPEResponse Instruction Failed: CUPE already exists\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add CUPEResponse Instruction Failed: CUPEQuestion does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CUPE Instruction Failed: Client does not exist\n\n" + line, "Error");
                                    }
                                }
                                else if (lineArray[2] == "GROUP")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                        {
                                            if (GetCUPEQuestion(lineArray[5].Replace('~', ' '), out cupeQuestion))
                                            {
                                                if(!AddCUPEToGroup(cupeQuestion.NAME.TrimEnd(), grp))
                                                {
                                                    MessageBox.Show("Add CUPEResponse Instruction Failed: CUPE already exists\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add CUPEResponse Instruction Failed: CUPEQuestion does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add CUPEResponse Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CUPEResponse Instruction Failed: Client does not exist\n\n" + line, "Error");
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
                                                if (GetCUPEQuestion(lineArray[6].Replace('~', ' '), out cupeQuestion))
                                                {
                                                    if (!AddCUPEToContact(cupeQuestion.NAME.TrimEnd(), contact))
                                                    {
                                                        MessageBox.Show("Add CupeResponse Instruction Failed: Cupe already exists\n\n" + line, "Error");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Add CUPEResponse Instruction Failed: CUPEQuestion does not exist\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add CUPEResponse Instruction Failed: Contact does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add CUPEResponse Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CUPEResponse Instruction Failed: Client does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                }
                                break;

                            case "DOMAIN":
                                domain = new DOMAIN();
                                domain.NAME = lineArray[2].Replace('~', ' ');
                                domain.DEFAULT = "N";
                                if (!AddDomain(domain))
                                {
                                    MessageBox.Show("Add Domain Instruction Failed: Domain already exists\n\n" + line, "Error");
                                }
                                break;

                            case "CAPABILITY":
                                if (GetDomain(lineArray[3].Replace('~', ' '), out domain))
                                {
                                    capability = new CAPABILITY();
                                    capability.NAME = lineArray[2].Replace('~', ' ');
                                    capability.DEFAULT = "N";
                                    capability.DOMAIN = domain;
                                    if (!AddCapability(capability))
                                    {
                                        MessageBox.Show("Add Capability Instruction Failed: Capability already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add Capability Instruction Failed: Domain does not exist\n\n" + line, "Error");
                                }
                                break;

                            case "ITCAPQUESTION":
                                if (GetCapability(lineArray[3].Replace('~', ' '), out capability))
                                {
                                    itcapQuestion = new ITCAPQUESTION();
                                    itcapQuestion.NAME = lineArray[2].Replace('~', ' ');
                                    itcapQuestion.DEFAULT = "N";
                                    itcapQuestion.CAPABILITY = capability;
                                    if (!AddITCAPQuestion(itcapQuestion))
                                    {
                                        MessageBox.Show("Add ITCAPQuestion Instruction Failed: ITCAPQuestion already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add ITCAPQuestion Instruction Failed: Capability does not exist\n\n" + line, "Error");
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
                                            if (!AddITCAP(itcap, client))
                                            {
                                                MessageBox.Show("Add ITCAP Instruction Failed: ITCAP already exists\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add ITCAP Instruction Failed: ITCAPQuestion does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add ITCAP Instruction Failed: Client does not exist\n\n" + line, "Error");
                                    }
                                }
                                else if (lineArray[2] == "GROUP")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetGroup(lineArray[4].Replace('~', ' '), client, out grp))
                                        {
                                            if (GetITCAPQuestion(lineArray[5].Replace('~', ' '), out itcapQuestion))
                                            {
                                                itcap = new ITCAP();
                                                itcap.ITCAPQUESTION = itcapQuestion;
                                                if (!AddITCAPToGroup(itcap, grp))
                                                {
                                                    MessageBox.Show("Add ITCAP Instruction Failed: ITCAP already exists\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add ITCAP Instruction Failed: ITCAPQuestion does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add ITCAP Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add ITCAP Instruction Failed: Client does not exist\n\n" + line, "Error");
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
                                                if (GetITCAPQuestion(lineArray[6].Replace('~', ' '), out itcapQuestion))
                                                {
                                                    itcap = new ITCAP();
                                                    itcap.ITCAPQUESTION = itcapQuestion;
                                                    if(!AddITCAPToContact(itcap, contact))
                                                    {
                                                        MessageBox.Show("Add ITCAP Instruction Failed: ITCAP already exists\n\n" + line, "Error");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Add ITCAP Instruction Failed: ITCAPPQuestion does not exist\n\n" + line, "Error");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add ITCAP Instruction Failed: Contact does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add ITCAP Instruction Failed: Group does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add ITCAP Instruction Failed: Client does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                }
                                break;

                            case "ITCAPOBJMAP":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    int temp;
                                    if (!GetITCAPOBJMAPScore(client, lineArray[4].Replace('~', ' '), lineArray[5].Replace('~', ' '), out temp))
                                    {
                                        if (!GetCapability(lineArray[4].Replace('~', ' '), out capability))
                                        {
                                            MessageBox.Show("Add ITCAPOBJMAP Instruction Failed: Capability does not exist\n\n" + line, "Error");
                                            break;
                                        }

                                        if (!GetObjective(lineArray[5].Replace('~', ' '), out objective))
                                        {
                                            MessageBox.Show("Add ITCAPOBJMAP Instruction Failed: BusinessObjective does not exist\n\n" + line, "Error");
                                            break;
                                        }

                                        itcapObjMap = new ITCAPOBJMAP();
                                        itcapObjMap.CLIENT = client;
                                        itcapObjMap.CAPABILITY = capability;
                                        itcapObjMap.BUSINESSOBJECTIVE = objective;
                                        itcapObjMap.SCORE = 0;

                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CapabilityObjectiveMap Instruction Failed: CapabilityObjectiveMap already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add CapabilityObjectiveMapping Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "CAPABILITYGAPINFO":
                                if (GetClient(lineArray[2], out client))
                                {
                                    if (GetCapability(lineArray[3], out capability))
                                    {
                                        capGapInfo = new CAPABILITYGAPINFO();
                                        capGapInfo.CLIENT = client;
                                        capGapInfo.CAPABILITY = capability;
                                        capGapInfo.GAP = 0;
                                        capGapInfo.PRIORITIZEDGAP = 0;
                                        capGapInfo.GAPTYPE = "None";
                                        capGapInfo.PRIORITIZEDGAPTYPE = "None";
                                        dbo.AddToCAPABILITYGAPINFO(capGapInfo);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add CapabilityGapInfo Instruction Failed: Capability does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add CapabilityGapInfo Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                break;
                        }
                    }

                    else if (lineArray[0] == "UPDATE")
                    {
                        switch (lineArray[1])
                        {
                            case "CLIENT":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    if (lineArray[2] == "BOMCOMPLETE")
                                    {
                                        client.BOMCOMPLETE = "Y";
                                    }
                                    if (lineArray[2] == "CUPECOMPLETE")
                                    {
                                        client.CUPECOMPLETE = "Y";
                                    }
                                    if (lineArray[2] == "ITCAPCOMPLETE")
                                    {
                                        client.ITCAPCOMPLETE = "Y";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update Client Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "BOM":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetBOM(lineArray[3].Replace('~', ' '), client, out bom))
                                    {
                                        bom.EFFECTIVENESS = Convert.ToSingle(lineArray[4]);
                                        bom.CRITICALITY = Convert.ToSingle(lineArray[5]);
                                        bom.DIFFERENTIAL = Convert.ToSingle(lineArray[6]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Update BOM Instruction Failed: BOM does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update BOM Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "CUPE":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetCUPE(lineArray[3].Replace('~', ' '), client, out cupe))
                                    {
                                        cupe.NAME = lineArray[4].Replace('~', ' ');
                                        cupe.COMMODITY = lineArray[5].Replace('~', ' ');
                                        cupe.UTILITY = lineArray[6].Replace('~', ' ');
                                        cupe.PARTNER = lineArray[7].Replace('~', ' ');
                                        cupe.ENABLER = lineArray[8].Replace('~', ' ');
                                    }
                                    else
                                    {
                                        MessageBox.Show("Update CUPE Instruction Failed: CUPE does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update CUPE Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "CUPEQUESTION":
                                if (GetCUPEQuestion(lineArray[2].Replace('~', ' '), out cupeQuestion))
                                {
                                    cupeQuestion.INTWENTY = lineArray[3];
                                    cupeQuestion.INFIFTEEN = lineArray[4];
                                    cupeQuestion.INTEN = lineArray[5];
                                }
                                else
                                {
                                    MessageBox.Show("Update CUPEQuestion Instruction Failed: CUPEQuestion does not exist\n\n" + line, "Error");
                                }
                                break;
                           case "ITCAP":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetITCAP(lineArray[3].Replace('~', ' '), client, out itcap))
                                    {
                                        itcap.ASIS = Convert.ToSingle(lineArray[4]);
                                        itcap.TOBE = Convert.ToSingle(lineArray[5]);
                                        itcap.COMMENT = lineArray[6].Replace('~', ' ');
                                    }
                                    else
                                    {
                                        MessageBox.Show("Update ITCAP Instruction Failed: ITCAP does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update ITCAP Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "ITCAPOBJMAP":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    if (GetITCAPOBJMAP(client, lineArray[4].Replace('~', ' '), lineArray[5].Replace('~', ' '), out itcapObjMap))
                                    {
                                        itcapObjMap.SCORE = Convert.ToInt32(lineArray[6]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Update CapabilityObjectiveMap Instruction Failed: CapabilityObjectiveMap does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update CapabilityObjectiveMap Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "CAPABILITYGAPINFO":
                                if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                {
                                    if (GetCapabilityGapInfo(lineArray[4].Replace('~', ' '), client, out capGapInfo))
                                    {
                                        capGapInfo.GAPTYPE = lineArray[5];
                                        capGapInfo.PRIORITIZEDGAPTYPE = lineArray[6];
                                        capGapInfo.GAP = Convert.ToSingle(lineArray[7]);
                                        capGapInfo.PRIORITIZEDGAP = Convert.ToSingle(lineArray[8]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Update CapabilityGapInfo Instruction Failed: CapabilityGapInfo does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Update CapabilityGapInfo Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                                break;
                        }
                    }

                    else if (lineArray[0] == "DELETE")
                    {
                        switch (lineArray[1])
                        {
                            case "CUPE":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    ClearCUPE(client);
                                }
                                else
                                {
                                    MessageBox.Show("Delete CUPE Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "CUPERESPONSE":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetGroup(lineArray[3].Replace('~', ' '), client, out grp))
                                    {
                                        if (GetContact(lineArray[4].Replace('~', ' '), grp, out contact))
                                        {
                                            List<CUPERESPONSE> responseList = contact.CUPERESPONSE.ToList();
                                            foreach (CUPERESPONSE response in responseList)
                                            {
                                                dbo.DeleteObject(response);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Delete CUPEResponse Instruction Failed: Contact does not exist\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Delete CUPEResponse Instruction Failed: Group does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Delete CUPEResponse Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            case "ITCAP":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetITCAP(lineArray[3].Replace('~', ' '), client, out itcap))
                                    {
                                        dbo.DeleteObject(itcap);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Delete ITCAP Instruction Failed: ITCAP does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Delete ITCAP Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid instruction detected\n\n" + line, "Error");
                                break;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Invalid instruction detected:\n\n" + line, "Error");
                    }

                    if (!SaveChanges())
                    {
                        if(MessageBox.Show("Instruction failed to execute: \n" + line +
                                           "\n\nKeep change in log?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            failedChanges.Add(line);
                        }
                        break;
                    }
                }
            }

            File.WriteAllText("Resources/Changes.log", string.Empty);
            File.WriteAllLines("Resources/Changes.log", failedChanges);
        }
        #endregion
    }
}