using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

/*--------------------------------------------------------------------------------------------------
 * Name: DBManager
 * 
 * Purpose: This class is instantiated each time online mode is set. It is responsible for
 *          online mode data management. Each region includes all functions related to
 *          that database table. Also includes functions for pushing offline changes to the 
 *          database when back in online mode, and updating client/data files whenever changes
 *          are made to the database. These two functions are called in the constructor.
 *-------------------------------------------------------------------------------------------------*/

namespace IBMConsultantTool
{
    public class DBManager : DataManager
    {
        public SAMPLEEntities dbo;

        public DBManager(LoadingDatabase loadingScreen)
        {
            loadingScreen.Show();
            loadingScreen.LoadingTextLabel.Update();
            dbo = new SAMPLEEntities();
            loadingScreen.LoadingTextLabel.Text = "Applying offline changes to database...";
            loadingScreen.LoadingTextLabel.Update();
            CheckChangeLog(loadingScreen);
            loadingScreen.LoadingTextLabel.Text = "Updating filesystem...";
            loadingScreen.LoadingTextLabel.Update();
            UpdateDataFile(loadingScreen);
        }

        #region Client

        public List<CLIENT> GetClients()
        {
            return (from ent in dbo.CLIENT
                    select ent).ToList();
        }
        public List<CLIENT> GetClientInfo()
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
            string clientName = client.NAME.TrimEnd();
            //If already in DB, return false
            if ((from ent in dbo.CLIENT
                 where ent.NAME.TrimEnd() == clientName
                 select ent).Count() != 0)
            {
                dbo.Detach(client);
                return false;
            }

            dbo.AddToCLIENT(client);

            AddGroup("Business", client);
            AddGroup("IT", client);
            AddGroup("ITCAP", client);

            File.Create(@"Resources\Clients\" + clientName + ".xml").Close();

            return true;
        }

        //Used by ClientDataControl

        public override Client AddClient(Client client)
        {
            CLIENT clientEnt = new CLIENT();
            
            clientEnt.NAME = client.Name.TrimEnd();

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

            string countryName = client.Country;
            COUNTRY country;
            try
            {
                country = (from ent in region.COUNTRY
                          where ent.NAME.TrimEnd() == countryName
                          select ent).Single();
            }
            catch
            {
                country = new COUNTRY();
                country.NAME = client.Country;
                country.REGION = region;
                dbo.AddToCOUNTRY(country);
            }

            clientEnt.COUNTRY = country;

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

            client.Name = clientEnt.NAME.TrimEnd();
            client.Country = clientEnt.COUNTRY.NAME.TrimEnd();
            client.Region = clientEnt.COUNTRY.REGION.NAME.TrimEnd();
            client.BusinessType = clientEnt.BUSINESSTYPE.NAME.TrimEnd();
            client.StartDate = clientEnt.STARTDATE;
            client.EntityObject = clientEnt;
            client.BomCompleted = clientEnt.BOMCOMPLETE == "Y";
            client.CupeCompleted = clientEnt.CUPECOMPLETE == "Y";
            client.ITCapCompleted = clientEnt.ITCAPCOMPLETE == "Y";
            return client;
        }

        public override Dictionary<string, float> GetObjectivesFromClientBOM(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;

            List<BOM> entList = client.BOM.ToList();

            Dictionary<string, float> objDictionary = new Dictionary<string, float>();
            Dictionary<string, float> objImpCount = new Dictionary<string, float>();
            float bomScore;
            string objectiveName;
            foreach (BOM bomObj in entList)
            {
                if(!bomObj.EFFECTIVENESS.HasValue || !bomObj.CRITICALITY.HasValue || !bomObj.DIFFERENTIAL.HasValue) 
                {
                    continue;
                }
                bomScore = ((11 - bomObj.EFFECTIVENESS.Value) * (bomObj.CRITICALITY.Value))/20 + (bomObj.DIFFERENTIAL.Value * .5f);
                objectiveName = bomObj.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                if(!objDictionary.ContainsKey(objectiveName))
                {
                    objDictionary.Add(objectiveName, bomScore);
                    objImpCount.Add(objectiveName, 1);
                }

                else
                {
                    objDictionary[objectiveName] = (objDictionary[objectiveName] * (objImpCount[objectiveName] / (objImpCount[objectiveName] + 1f))) + 
                                                   (bomScore / (objImpCount[objectiveName] + 1f));
                    objImpCount[objectiveName]++;
                }
            }

            return objDictionary;
        }

        public override Dictionary<string, List<string>> GetObjectivesAndImperativesFromClientBOM(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;

            List<BOM> entList = client.BOM.ToList();

            Dictionary<string, List<string>> objDictionary = new Dictionary<string, List<string>>();
            string objectiveName;
            foreach (BOM bomObj in entList)
            {
                objectiveName = bomObj.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                if (!objDictionary.ContainsKey(objectiveName))
                {
                    objDictionary.Add(objectiveName, new List<string>());
                }
                
                objDictionary[objectiveName].Add(bomObj.IMPERATIVE.NAME.TrimEnd());
            }

            return objDictionary;
        }

        public override Dictionary<string, List<string>> GetCapabilitiesAndAttributesFromClientITCAP(object clientObj)
        {
            CLIENT client = clientObj as CLIENT;

            List<ITCAP> entList = client.ITCAP.ToList();

            Dictionary<string, List<string>> capDictionary = new Dictionary<string, List<string>>();
            string capabilityName;
            foreach (ITCAP itcapObj in entList)
            {
                capabilityName = itcapObj.ITCAPQUESTION.CAPABILITY.NAME.TrimEnd();
                if (!capDictionary.ContainsKey(capabilityName))
                {
                    capDictionary.Add(capabilityName, new List<string>());
                }

                capDictionary[capabilityName].Add(itcapObj.ITCAPQUESTION.NAME.TrimEnd());
            }

            return capDictionary;
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

        #region Country
        public override List<string> GetCountryNames(string regionName = "N/A")
        {
            if (regionName == "N/A")
            {
                return (from ent in dbo.COUNTRY
                        select ent.NAME.TrimEnd()).ToList();
            }

            else
            {
                return (from ent in dbo.COUNTRY
                        where ent.REGION.NAME.TrimEnd() == regionName
                        select ent.NAME.TrimEnd()).ToList();
            }
        }
        public override bool AddCountry(string countryName, string regionName)
        {
            //If already in DB, return false
            REGION region;
            try
            {
                region = (from ent in dbo.REGION
                          where ent.NAME.TrimEnd() == regionName
                          select ent).Single();
            }

            catch
            {
                return false;
            }

            if ((from ent in region.COUNTRY
                 where ent.NAME.TrimEnd() == countryName
                 select ent).Count() != 0)
            {
                return false;
            }

            COUNTRY country = new COUNTRY();
            country.NAME = countryName;
            country.REGION = region;
            dbo.AddToCOUNTRY(country);

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
        #endregion

        #region Contact

        public bool AddContact(int id, GROUP grp)
        {
            if ((from ent in grp.CONTACT
                 where ent.ID == id
                 select ent).Count() != 0)
            {
                return false;
            }

            CONTACT contact = new CONTACT();
            contact.ID = id;
            grp.CONTACT.Add(contact);
            dbo.AddToCONTACT(contact);

            return true;
        }

        public bool GetContact(int id, out CONTACT contact)
        {
            try
            {
                contact = (from ent in dbo.CONTACT
                          where ent.ID == id
                          select ent).Single();
            }

            catch
            {
                contact = null;
                return false;
            }

            return true;
        }

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
            int questionIndex = 1;
            foreach (CONTACT contact in busGrp.CONTACT)
            {
                person = new Person(id);
                person.Type = Person.EmployeeType.Business;
                person.CodeName = "Business" + (id).ToString();
                cupeData = new CupeData(id);
                foreach (CUPERESPONSE response in contact.CUPERESPONSE)
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                                                                              {
                                                                                  return question.QuestionText == response.CUPE.NAME.TrimEnd();
                                                                              });
                    if (questionIndex != -1)
                    {
                        try
                        {
                            cupeData.CurrentAnswers.Add("Question " + (questionIndex + 1).ToString(), response.CURRENT[0]);
                            cupeData.FutureAnswers.Add("Question " + (questionIndex + 1).ToString(), response.FUTURE[0]);
                        }
                        catch
                        {

                        }
                        
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }

            foreach (CONTACT contact in itGrp.CONTACT)
            {
                person = new Person(id);
                person.Type = Person.EmployeeType.IT;
                person.CodeName = "IT" + (id).ToString();
                cupeData = new CupeData(id);
                foreach (CUPERESPONSE response in contact.CUPERESPONSE)
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                                                                              {
                                                                                  return question.QuestionText == response.CUPE.NAME.TrimEnd();
                                                                              });
                    if (questionIndex != -1)
                    {
                        if(response.CURRENT.Length > 0)
                        {
                            cupeData.CurrentAnswers.Add("Question " + (questionIndex + 1).ToString(), response.CURRENT[0]);
                        }
                        if (response.FUTURE.Length > 0)
                        {
                            cupeData.FutureAnswers.Add("Question " + (questionIndex + 1).ToString(), response.FUTURE[0]);
                        }
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }
        }
        #endregion

        #region BOM

        public bool GetBOM(string iniName, CLIENT client, out BOM bom)
        {
            try
            {
                bom = (from ent in client.BOM
                       where ent.IMPERATIVE.NAME.TrimEnd() == iniName
                       select ent).Single();
            }

            catch
            {
                bom = null;
                return false;
            }

            return true;
        }

        public override bool UpdateBOM(object clientObj, NewImperative ini)
        {
            CLIENT client = clientObj as CLIENT;
            Console.WriteLine(ini.Effectiveness.ToString());
            try
            {
                BOM bom = (from ent in client.BOM
                           where ent.IMPERATIVE.NAME.TrimEnd() == ini.Name
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

            //If Client points to 2 BOMs with same Imperative, return false
            if ((from ent in client.BOM
                 where ent.IMPERATIVE.NAME.TrimEnd() == bom.IMPERATIVE.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(bom);
                return false;
            }

            bom.CLIENT = client;

            dbo.AddToBOM(bom);

            return true;
        }

        public override bool RemoveBOM(string bomName, object clientObj)
        {
            CLIENT client = clientObj as CLIENT;
            BOM bom;
            if (!GetBOM(bomName, client, out bom))
            {
                return false;
            }

            dbo.DeleteObject(bom);

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
            NewImperative imperative;

            foreach (BOM bom in client.BOM)
            {
                catName = bom.IMPERATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                category = bomForm.Categories.Find(delegate(NewCategory cat)
                {
                    return cat.name == catName;
                });
                if (category == null)
                {
                    category = bomForm.AddCategory(catName);
                }

                bomForm.CategoryWorkspace.SelectTab(category.name);

                busName = bom.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                objective = category.Objectives.Find(delegate(NewObjective bus)
                {
                    return bus.ObjName == busName;
                });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

                iniName = bom.IMPERATIVE.NAME.TrimEnd();
                imperative = objective.Imperatives.Find(delegate(NewImperative ini)
                {
                    return ini.Name == iniName;
                });
                if (imperative == null)
                {
                    imperative = objective.AddImperative(iniName);
                    imperative.Effectiveness = bom.EFFECTIVENESS.HasValue ? bom.EFFECTIVENESS.Value : 0;
                    imperative.Criticality = bom.CRITICALITY.HasValue ? bom.CRITICALITY.Value : 0;
                    imperative.Differentiation = bom.DIFFERENTIAL.HasValue ? bom.DIFFERENTIAL.Value : 0;
                }
            }
        }

        public override List<string> GetObjectivesFromCurrentClientBOM()
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;

            List<string> allObjectiveNamesList =  (from bom in client.BOM
                                                   select bom.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd()).ToList();

            List<string> result = new List<string>();
            foreach(string objectiveName in allObjectiveNamesList)
            {
                if(!result.Contains(objectiveName))
                {
                    result.Add(objectiveName);
                }
            }

            return result;
        }
        public override List<string> GetImperativesFromCurrentClientBOM()
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;

            return (from bom in client.BOM
                    select bom.IMPERATIVE.NAME.TrimEnd()).ToList();
        }
        #endregion

        #region ITCAP

        public bool GetITCAP(string itcqName, CLIENT client, out ITCAP itcap)
        {
            try
            {
                itcap = (from ent in client.ITCAP
                         where ent.ITCAPQUESTION.NAME.TrimEnd() == itcqName
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
            Random rnd = new Random();
            CLIENT client = clientObj as CLIENT;
            COMMENT commentEnt;
            try
            {
                ITCAP itcap = (from ent in client.ITCAP
                           where ent.ITCAPQUESTION.NAME.TrimEnd() == itcapQuestion.Name
                           select ent).Single();

                itcap.ASIS = itcapQuestion.AsIsScore;
                itcap.TOBE = itcapQuestion.ToBeScore;
                itcap.ASISZEROS = itcapQuestion.AsIsNumZeros;
                itcap.ASISONES = itcapQuestion.AsIsNumOnes;
                itcap.ASISTWOS = itcapQuestion.AsIsNumTwos;
                itcap.ASISTHREES = itcapQuestion.AsIsNumThrees;
                itcap.ASISFOURS = itcapQuestion.AsIsNumFours;
                itcap.ASISFIVES = itcapQuestion.AsIsNumFives;
                itcap.TOBEZEROS = itcapQuestion.TobeNumZeros;
                itcap.TOBEONES = itcapQuestion.TobeNumOnes;
                itcap.TOBETWOS = itcapQuestion.TobeNumTwos;
                itcap.TOBETHREES = itcapQuestion.TobeNumThrees;
                itcap.TOBEFOURS = itcapQuestion.TobeNumFours;
                itcap.TOBEFIVES = itcapQuestion.TobeNumFives;
                List<COMMENT> commentsToDelete = itcap.COMMENT.ToList();
                foreach (COMMENT commentEntToDelete in commentsToDelete)
                {
                    dbo.DeleteObject(commentEntToDelete);
                }
                foreach (string comment in itcapQuestion.comment)
                {
                    commentEnt = new COMMENT();
                    commentEnt.NAME = comment;
                    commentEnt.ITCAP = itcap;
                    dbo.AddToCOMMENT(commentEnt);
                }
            }

            catch
            {
                return false;
            }


            return true;
        }

        public override bool LoadITCAP(ref ITCapQuestion question)
        {
            ITCAP itcap;
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            if (GetITCAP(question.Name, client, out itcap))
            {
                if (itcap.ASISZEROS.HasValue)
                {
                    for (int i = 0; i < itcap.ASISZEROS.Value; i++)
                    {
                        question.AddAsIsAnswer(0);
                    }
                }
                if (itcap.ASISONES.HasValue)
                {
                    for (int i = 0; i < itcap.ASISONES.Value; i++)
                    {
                        question.AddAsIsAnswer(1);
                    }
                }
                if (itcap.ASISTWOS.HasValue)
                {
                    for (int i = 0; i < itcap.ASISTWOS.Value; i++)
                    {
                        question.AddAsIsAnswer(2);
                    }
                }
                if (itcap.ASISTHREES.HasValue)
                {
                    for (int i = 0; i < itcap.ASISTHREES.Value; i++)
                    {
                        question.AddAsIsAnswer(3);
                    }
                }
                if (itcap.ASISFOURS.HasValue)
                {
                    for (int i = 0; i < itcap.ASISFOURS.Value; i++)
                    {
                        question.AddAsIsAnswer(4);
                    }
                }
                if (itcap.ASISFIVES.HasValue)
                {
                    for (int i = 0; i < itcap.ASISFIVES.Value; i++)
                    {
                        question.AddAsIsAnswer(5);
                    }
                }
                if (itcap.ASIS.HasValue)
                {
                    question.AsIsScore = itcap.ASIS.Value;
                }
                if (itcap.TOBEZEROS.HasValue)
                {
                    for (int i = 0; i < itcap.TOBEZEROS.Value; i++)
                    {
                        question.AddToBeAnswer(0);
                    }
                }
                if (itcap.TOBEONES.HasValue)
                {
                    for (int i = 0; i < itcap.TOBEONES.Value; i++)
                    {
                        question.AddToBeAnswer(1);
                    }
                }
                if (itcap.TOBETWOS.HasValue)
                {
                    for (int i = 0; i < itcap.TOBETWOS.Value; i++)
                    {
                        question.AddToBeAnswer(2);
                    }
                }
                if (itcap.TOBETHREES.HasValue)
                {
                    for (int i = 0; i < itcap.TOBETHREES.Value; i++)
                    {
                        question.AddToBeAnswer(3);
                    }
                }
                if (itcap.TOBEFOURS.HasValue)
                {
                    for (int i = 0; i < itcap.TOBEFOURS.Value; i++)
                    {
                        question.AddToBeAnswer(4);
                    }
                }
                if (itcap.TOBEFIVES.HasValue)
                {
                    for (int i = 0; i < itcap.TOBEFIVES.Value; i++)
                    {
                        question.AddToBeAnswer(5);
                    }
                }
                if (itcap.TOBE.HasValue)
                {
                    question.ToBeScore = itcap.TOBE.Value;
                }
                foreach (COMMENT comment in itcap.COMMENT)
                {
                    question.AddComment(comment.NAME.TrimEnd());
                }
            }

            else
            {
                return false;
            }

            return true;
        }

        public override bool AddITCAP(object itcqObject, object clientObj)
        {
            ITCAP itcap = itcqObject as ITCAP;
            CLIENT client = clientObj as CLIENT;

            //If Client points to 2 BOMs with same Imperative, return false
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

        public override bool RemoveITCAP(string itcqName, object clientObj)
        {
            ITCAP itcap;
            CLIENT client = clientObj as CLIENT;
            
            if(GetITCAP(itcqName, client, out itcap))
            {
                //Should delete related comments as well
                dbo.DeleteObject(itcap);
                return true;
            }
            
            return false;
        }


        public override bool OpenITCAP(ITCapTool itcapForm)
        {
            CLIENT client = ClientDataControl.Client.EntityObject as CLIENT;
            List<ITCAP> itcapList = (from ent in client.ITCAP
                                     orderby ent.ITCAPQUESTION.ID
                                     select ent).ToList();

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
                    domain.ID = domEnt.ID.TrimEnd();
                    //itcapForm.LoadCapabilities(dom);
                    itcapForm.domains.Add(domain);
                }

                capability = itcapForm.capabilities.Find(delegate(Capability cap)
                                                         {
                                                             return cap.CapName == capName;
                                                         });
                if (capability == null)
                {
                    capability = new Capability();
                    Capability.AllCapabilities.Add(capability);
                    capability.CapName = capName;
                    capability.IsDefault = capEnt.DEFAULT == "Y";
                    domain.CapabilitiesOwned.Add(capability);
                    domain.TotalChildren++;
                    capability.Type = "capability";
                    capability.ID = capEnt.ID.TrimEnd();
                    itcapForm.capabilities.Add(capability);
                    capability.Owner = domain;
                    //LoadQuestions(cap);
                }

                itcapQuestion = new ITCapQuestion();
                itcapQuestion.Name = itcqName;
                itcapQuestion.IsDefault = itcqEnt.DEFAULT == "Y";
                itcapQuestion.AsIsScore = itcap.ASIS.HasValue ? itcap.ASIS.Value : 0;
                itcapQuestion.ToBeScore = itcap.TOBE.HasValue ? itcap.TOBE.Value : 0;
                //itcapQuestion.AddComment(itcap.COMMENT);
                itcapQuestion.Type = "attribute";
                itcapQuestion.ID = itcqEnt.ID.TrimEnd();
                capability.Owner.TotalChildren++;
                capability.QuestionsOwned.Add(itcapQuestion);
                itcapQuestion.Owner = capability;
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

            List<CAPABILITYGAPINFO> capGapInfoList = client.CAPABILITYGAPINFO.ToList();
            foreach (CAPABILITYGAPINFO capGapInfo in capGapInfoList)
            {
                dbo.DeleteObject(capGapInfo);
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
            bomForm.imperativeNames.Items.Clear();
            bomForm.imperativeNames.Text = "";
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
            bomForm.imperativeNames.Items.Clear();
            bomForm.imperativeNames.Text = "<Select Imperative>";
            BUSINESSOBJECTIVE objective;
            if (GetObjective(bomForm.objectiveNames.Text.Trim(), out objective))
            {
                bomForm.imperativeNames.Items.AddRange((from ent in objective.IMPERATIVE
                                                select ent.NAME.TrimEnd()).ToArray());
            }
        }
        #endregion

        #region Imperative
        public List<IMPERATIVE> GetImperatives()
        {
            return (from ent in dbo.IMPERATIVE
                    select ent).ToList();
        }

        public string[] GetImperativeNames()
        {
            return (from ent in dbo.IMPERATIVE
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public bool GetImperative(string iniName, out IMPERATIVE Imperative)
        {
            try
            {
                Imperative = (from ent in dbo.IMPERATIVE
                          where ent.NAME.TrimEnd() == iniName
                          select ent).Single();
            }

            catch
            {
                Imperative = null;
                return false;
            }

            return true;
        }

        public bool AddImperative(IMPERATIVE imperative)
        {
            //If already in DB, return false
            if ((from ent in dbo.IMPERATIVE
                 where ent.NAME.TrimEnd() == imperative.NAME.TrimEnd()
                 select ent).Count() != 0)
            {
                dbo.Detach(imperative);
                return false;
            }

            dbo.AddToIMPERATIVE(imperative);

            return true;
        }

        public override bool AddImperativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
        {
            IMPERATIVE imperative;
            if (!GetImperative(iniName, out imperative))
            {
                imperative = new IMPERATIVE();
                imperative.NAME = iniName;
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

                else if (objective.CATEGORY.NAME.TrimEnd() != catName)
                {
                    MessageBox.Show("Objective already exists under category " + objective.CATEGORY.NAME.TrimEnd(), "Error");
                    return false;
                }

                imperative.BUSINESSOBJECTIVE = objective;
                if (!AddImperative(imperative))
                {
                    MessageBox.Show("Failed to add Imperative to Database", "Error");
                    return false;
                }
            }

            else if (imperative.BUSINESSOBJECTIVE.NAME.TrimEnd() != busName)
            {
                MessageBox.Show("Imperative already exists under objective " + imperative.BUSINESSOBJECTIVE.NAME.TrimEnd(), "Error");
                return false;
            }

            BUSINESSOBJECTIVE testObjective;
            if (GetObjective(busName, out testObjective) && testObjective.CATEGORY.NAME.TrimEnd() != catName)
            {
                MessageBox.Show("Objective already exists under category " + testObjective.CATEGORY.NAME.TrimEnd(), "Error");
                return false;
            }

            BOM bom = new BOM();
            bom.IMPERATIVE = imperative;
            if (!AddBOM(bom, ClientDataControl.Client.EntityObject))
            {
                MessageBox.Show("Failed to add Imperative to BOM", "Error");
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
                catName = bom.IMPERATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                NewCategory category = bomForm.Categories.Find(delegate(NewCategory cat)
                {
                    return cat.name == catName;
                });
                if (category == null)
                {
                    category = bomForm.AddCategory(catName);
                }

                bomForm.CategoryWorkspace.SelectTab(category.name);

                busName = bom.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                NewObjective objective = category.Objectives.Find(delegate(NewObjective bus)
                {
                    return bus.ObjName == busName;
                });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

                iniName = bom.IMPERATIVE.NAME.TrimEnd();
                NewImperative imperativeObj = objective.Imperatives.Find(delegate(NewImperative ini)
                {
                    return ini.Name == iniName;
                });
                if (imperativeObj == null)
                {
                    imperativeObj = objective.AddImperative(iniName);
                }
                else
                {
                    MessageBox.Show("Imperative already exists in BOM", "Error");
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
            cupeQuestionEnt.INTWENTY = cupeQuestionEnt.INTEN = "N";
            

            dbo.AddToCUPEQUESTION(cupeQuestionEnt);

            return true;
        }
        public override bool UpdateCupeQuestion(string cupeQuestion, bool inTwenty, bool inTen)
        {
            CUPEQUESTION cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.CUPEQUESTION
                                   where ent.NAME.TrimEnd() == cupeQuestion
                                   select ent).Single();

                cupeQuestionEnt.INTWENTY = inTwenty ? "Y" : "N";
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
            List<CUPE> cupesToDelete = client.CUPE.ToList();
            List<CUPERESPONSE> responsesToDelete;
            foreach (CUPE cupe in cupesToDelete)
            {
                responsesToDelete = cupe.CUPERESPONSE.ToList();
                foreach (CUPERESPONSE cr in responsesToDelete)
                {
                    dbo.DeleteObject(cr);
                }

                dbo.DeleteObject(cupe);
            }

            GROUP grp;
            List<CONTACT> contactsToDelete;

            if (GetGroup("Business", client, out grp))
            {
                contactsToDelete = grp.CONTACT.ToList();
                foreach (CONTACT contactToDelete in contactsToDelete)
                {
                    dbo.DeleteObject(contactToDelete);
                }
            }

            if (GetGroup("IT", client, out grp))
            {
                contactsToDelete = grp.CONTACT.ToList();
                foreach (CONTACT contactToDelete in contactsToDelete)
                {
                    dbo.DeleteObject(contactToDelete);
                }
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
            Random rnd = new Random();
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

            List<CONTACT> contactsToDelete = busGrp.CONTACT.ToList();
            List<CUPERESPONSE> responsesToDelete;
            foreach (CONTACT contactToDelete in contactsToDelete)
            {
                responsesToDelete = contactToDelete.CUPERESPONSE.ToList();
                foreach (CUPERESPONSE responseToDelete in responsesToDelete)
                {
                    dbo.DeleteObject(responseToDelete);
                }
                dbo.DeleteObject(contactToDelete);
            }
            contactsToDelete = itGrp.CONTACT.ToList();
            foreach (CONTACT contactToDelete in contactsToDelete)
            {
                responsesToDelete = contactToDelete.CUPERESPONSE.ToList();
                foreach (CUPERESPONSE responseToDelete in responsesToDelete)
                {
                    dbo.DeleteObject(responseToDelete);
                }
                dbo.DeleteObject(contactToDelete);
            }
            foreach (Person person in personList)
            {
                if (person.Type == Person.EmployeeType.Business)
                {
                    contact = new CONTACT();
                    contact.ID = rnd.Next();
                    busGrp.CONTACT.Add(contact);
                    dbo.AddToCONTACT(contact);

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for (int i = 0; i < questionList.Count; i++)
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
                    contact = new CONTACT();
                    contact.ID = rnd.Next();
                    itGrp.CONTACT.Add(contact);
                    dbo.AddToCONTACT(contact);

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for(int i = 0; i < questionList.Count; i++)
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
            List<CUPE> cupeList = (from ent in client.CUPE
                                   orderby ent.QUESTIONNUMBER
                                   select ent).ToList();
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

        public override string UpdateCUPE(CupeQuestionStringData cupeQuestion)
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

                if (cupe.NAME.TrimEnd() != cupe.CUPEQUESTION.NAME.TrimEnd())
                {
                    CUPEQUESTION cupeQuestionEnt;
                    if(!GetCUPEQuestion(cupe.NAME.TrimEnd(), out cupeQuestionEnt))
                    {
                        cupeQuestionEnt = new CUPEQUESTION();
                        cupeQuestionEnt.NAME = cupe.NAME;
                        cupeQuestionEnt.COMMODITY = cupe.COMMODITY;
                        cupeQuestionEnt.UTILITY = cupe.UTILITY;
                        cupeQuestionEnt.PARTNER = cupe.PARTNER;
                        cupeQuestionEnt.ENABLER = cupe.ENABLER;
                        cupeQuestionEnt.INTWENTY = cupeQuestionEnt.INTEN = "N";
                    }

                    cupe.CUPEQUESTION = cupeQuestionEnt;
                    cupeQuestion.OriginalQuestionText = cupeQuestionEnt.NAME.TrimEnd();
                }
            }

            catch
            {
                MessageBox.Show("Failed to update CUPE: " + cupeQuestion.QuestionText, "Error");
            }


            return cupeQuestion.OriginalQuestionText;
        }
        public override bool AddCUPE(string question, object clientObj, int questionNumber)
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
            cupe.QUESTIONNUMBER = questionNumber;
            client.CUPE.Add(cupe);

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

        #region ScoringEntity
        public override string GetScoringEntityID(string entName)
        {
            DOMAIN domain;
            if(GetDomain(entName, out domain))
            {
                return domain.ID.TrimEnd();
            }

            CAPABILITY capability;
            if (GetCapability(entName, out capability))
            {
                return capability.ID.TrimEnd();
            }

            ITCAPQUESTION itcapQuestion;
            if (GetITCAPQuestion(entName, out itcapQuestion))
            {
                return itcapQuestion.ID.TrimEnd();
            }

            return "";
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

            domain.ID = (dbo.DOMAIN.Count()+1).ToString() + ".0.0";
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

            capability.ID = capability.DOMAIN.ID[0] + "." + (capability.DOMAIN.CAPABILITY.Count()) + ".0";

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
            if(!GetCapabilityGapInfo(capability.CapName, client, out capGapInfo))
            {
                capGapInfo = new CAPABILITYGAPINFO();
                capGapInfo.CLIENT = ClientDataControl.Client.EntityObject as CLIENT;
                CAPABILITY capabilityEnt;
                GetCapability(capability.CapName, out capabilityEnt);
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

            itcapQuestion.ID = itcapQuestion.CAPABILITY.DOMAIN.ID[0] + "." + itcapQuestion.CAPABILITY.ID[2] + "." + (itcapQuestion.CAPABILITY.ITCAPQUESTION.Count());

            dbo.AddToITCAPQUESTION(itcapQuestion);

            return true;
        }

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm, out int alreadyExists, out string owner)
        {
            alreadyExists = 0;
            owner = "";
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

            Domain domForSearch = itcapForm.domains.Find(delegate(Domain dom)
                                            {
                                                return dom.Name == domName;
                                            });
            if (domForSearch != null)
            {
                alreadyExists = 1;
                owner = domName;
                Capability capForSearch = domForSearch.CapabilitiesOwned.Find(delegate(Capability cap)
                                                                              {
                                                                                  return cap.CapName == capName;
                                                                              });
                if (capForSearch != null)
                {
                    alreadyExists = 2;
                    owner = capName;
                    ITCapQuestion itcqForSearch = capForSearch.QuestionsOwned.Find(delegate(ITCapQuestion itcq)
                                                                                   {
                                                                                       return itcq.Name == itcqName;
                                                                                   });
                    if (itcqForSearch != null)
                    {
                        alreadyExists = 3;
                        owner = "";
                    }
                }
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

        #region GetTrendAnalysisObjects
        public List<ImperativeTrendAnalysis> GetImperativeTrendAnalysis(string iniName, string regName, string counName, string busTypeName, string fromDateStr, string toDateStr)
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
                    bomList = GetBOMSForImperativeRegionAndBusinessType(iniName, regName, counName, busTypeName, fromDate, toDate);
                }

                else
                {
                    bomList = GetBOMSForImperativeRegion(iniName, regName, counName, fromDate, toDate);
                }
            }

            else
            {
                if (busTypeName != "All")
                {
                    bomList = GetBOMSForImperativeBusinessType(iniName, busTypeName, fromDate, toDate);
                }

                else
                {
                    bomList = GetBOMSForImperative(iniName, fromDate, toDate);
                }
            }

            List<ImperativeTrendAnalysis> itaList = new List<ImperativeTrendAnalysis>();
            ImperativeTrendAnalysis ita;
            CLIENT client;
            foreach (BOM bom in bomList)
            {
                if (bom.EFFECTIVENESS.HasValue && bom.CRITICALITY.HasValue && bom.DIFFERENTIAL.HasValue)
                {
                    ita = new ImperativeTrendAnalysis();
                    client = bom.CLIENT;
                    ita.Date = client.STARTDATE;
                    ita.Region = client.COUNTRY.REGION.NAME.TrimEnd();
                    ita.Country = client.COUNTRY.NAME.TrimEnd();
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

        public List<ITAttributeTrendAnalysis> GetITAttributeTrendAnalysis(string attName, string regName, string counName, string busTypeName, string fromDateStr, string toDateStr)
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
                    itcapList = GetITCAPSForAttributeRegionAndBusinessType(attName, regName, counName, busTypeName, fromDate, toDate);
                }

                else
                {
                    itcapList = GetITCAPSForAttributeRegion(attName, regName, counName, fromDate, toDate);
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
            if (itcapList != null)
            {
                foreach (ITCAP itcap in itcapList)
                {
                    if (itcap.ASIS.HasValue && itcap.TOBE.HasValue)
                    {
                        itata = new ITAttributeTrendAnalysis();
                        client = itcap.CLIENT;
                        itata.Date = client.STARTDATE;
                        itata.Country = client.COUNTRY.NAME.Trim();
                        itata.Region = client.COUNTRY.REGION.NAME.TrimEnd();
                        itata.BusinessType = client.BUSINESSTYPE.NAME.Trim();
                        itata.Country = itata.Region;
                        itata.AsisScore = itcap.ASIS.Value;
                        itata.TobeScore = itcap.TOBE.Value;
                        itata.Name = attName;
                        itataList.Add(itata);
                    }
                }
            }


            return itataList;
        }

        public List<CUPEQuestionTrendAnalysis> GetCUPEQuestionTrendAnalysis(string cqName, string regName, string counName, string busTypeName, string fromDateStr, string toDateStr)
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
                    crList = GetCUPEResponsesForCUPEQuestionRegionAndBusinessType(cqName, regName, counName, busTypeName, fromDate, toDate);
                }

                else
                {
                    crList = GetCUPEResponsesForCUPEQuestionRegion(cqName, regName, counName, fromDate, toDate);
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
            if(crList != null)
            {
                foreach (CUPERESPONSE cr in crList)
                {
                    if (cr.CURRENT != "" && cr.FUTURE != "")
                    {
                        cqta = new CUPEQuestionTrendAnalysis();
                        client = cr.CONTACT.GROUP.CLIENT;
                        cqta.Date = client.STARTDATE;
                        cqta.Region = client.COUNTRY.REGION.NAME.TrimEnd();
                        cqta.Country = client.COUNTRY.NAME.TrimEnd();
                        cqta.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                        cqta.Country = cqta.Region.TrimEnd();
                        cqta.CupeType = cr.CONTACT.GROUP.NAME.TrimEnd();
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
            }


            return cqtaList;
        }
        
        public List<CapabilityTrendAnalysis> GetCapabilityTrendAnalysis(string capName, string regName, string counName, string busTypeName, string fromDateStr, string toDateStr)
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
                    capGapInfoList = GetCapabilityGapInfosFromCapabilityRegionAndBusinessType(capName, regName, counName, busTypeName, fromDate, toDate);
                }

                else
                {
                    capGapInfoList = GetCapabilityGapInfosFromCapabilityRegion(capName, regName, counName, fromDate, toDate);
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
            if (capGapInfoList != null)
            {
                foreach (CAPABILITYGAPINFO capGapInfo in capGapInfoList)
                {
                    if (capGapInfo.GAP.HasValue && capGapInfo.PRIORITIZEDGAP.HasValue)
                    {
                        cta = new CapabilityTrendAnalysis();
                        client = capGapInfo.CLIENT;
                        cta.Date = client.STARTDATE;
                        cta.Country = client.COUNTRY.NAME.TrimEnd();
                        cta.Region = client.COUNTRY.REGION.NAME.TrimEnd();
                        cta.BusinessType = client.BUSINESSTYPE.NAME.TrimEnd();
                        cta.Country = cta.Region;
                        cta.CapabilityGap = capGapInfo.GAP.Value;
                        cta.PrioritizedCapabilityGap = capGapInfo.PRIORITIZEDGAP.Value;
                        cta.GapType = capGapInfo.GAPTYPE.TrimEnd();
                        cta.PrioritizedGapType = capGapInfo.PRIORITIZEDGAPTYPE.TrimEnd();
                        cta.Name = capName;
                        ctaList.Add(cta);
                    }
                }
            }


            return ctaList;
        }
        #endregion

        #region Helpers
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
        public List<string> GetImperativesFromObjective(string busName)
        {
            BUSINESSOBJECTIVE objective;
            if (GetObjective(busName, out objective))
            {
                return ((from ent in objective.IMPERATIVE
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
        #endregion

        #region GetTrendAnalysisDataFromDB
        public List<BOM> GetBOMSForImperative(string iniName, DateTime fromDate, DateTime toDate)
        {
            IMPERATIVE imperative;
            if(GetImperative(iniName, out imperative))
            {
                return (from ent in imperative.BOM
                        where ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForImperativeRegion(string iniName, string regionName, string counName, DateTime fromDate, DateTime toDate)
        {
            IMPERATIVE imperative;
            if (GetImperative(iniName, out imperative))
            {
                if (counName != "All")
                {
                    return (from ent in imperative.BOM
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
                else
                {
                    return (from ent in imperative.BOM
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regionName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForImperativeBusinessType(string iniName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            IMPERATIVE imperative;
            if (GetImperative(iniName, out imperative))
            {
                return (from ent in imperative.BOM
                        where ent.CLIENT.BUSINESSTYPE != null &&
                              ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<BOM> GetBOMSForImperativeRegionAndBusinessType(string iniName, string regName, string counName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            IMPERATIVE imperative;
            if (GetImperative(iniName, out imperative))
            {
                if (counName != "All")
                {
                    return (from ent in imperative.BOM
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
                else
                {
                    return (from ent in imperative.BOM
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
            }

            else
            {
                return null;
            }
        }

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestionRegionAndBusinessType(string cqName, string regName, string counName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                if (counName != "All")
                {
                    return (from cupe in cupeQuestion.CUPE
                            where cupe.CLIENT.COUNTRY != null &&
                                  cupe.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  cupe.CLIENT.BUSINESSTYPE != null &&
                                  cupe.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  cupe.CLIENT.STARTDATE > fromDate &&
                                  cupe.CLIENT.STARTDATE < toDate
                            from ent in cupe.CUPERESPONSE
                            select ent).ToList();
                }
                else
                {
                    return (from cupe in cupeQuestion.CUPE
                            where cupe.CLIENT.COUNTRY.REGION != null &&
                                  cupe.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  cupe.CLIENT.BUSINESSTYPE != null &&
                                  cupe.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  cupe.CLIENT.STARTDATE > fromDate &&
                                  cupe.CLIENT.STARTDATE < toDate
                            from ent in cupe.CUPERESPONSE
                            select ent).ToList();
                }
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
                              cupe.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
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

        public List<CUPERESPONSE> GetCUPEResponsesForCUPEQuestionRegion(string cqName, string regName, string counName, DateTime fromDate, DateTime toDate)
        {
            CUPEQUESTION cupeQuestion;
            if (GetCUPEQuestion(cqName, out cupeQuestion))
            {
                if (counName != "All")
                {
                    return (from cupe in cupeQuestion.CUPE
                            where cupe.CLIENT.COUNTRY != null &&
                                  cupe.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  cupe.CLIENT.STARTDATE > fromDate &&
                                  cupe.CLIENT.STARTDATE < toDate
                            from ent in cupe.CUPERESPONSE
                            select ent).ToList();
                }
                else
                {
                    return (from cupe in cupeQuestion.CUPE
                            where cupe.CLIENT.COUNTRY.REGION != null &&
                                  cupe.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  cupe.CLIENT.STARTDATE > fromDate &&
                                  cupe.CLIENT.STARTDATE < toDate
                            from ent in cupe.CUPERESPONSE
                            select ent).ToList();
                }
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
        public List<CUPE> GetCUPESForCUPEQuestionRegion(string cqName, string regionName, string counName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                if (counName != "All")
                {
                    return (from ent in cq.CUPE
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName
                            select ent).ToList();
                }
                else
                {
                    return (from ent in cq.CUPE
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regionName
                            select ent).ToList(); 
                }

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
                                ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName
                        select ent).ToList();
            }

            catch
            {
                return null;
            }
        }
        public List<CUPE> GetCUPESForCUPEQuestionRegionAndBusinessType(string cqName, string regName, string counName, string busTypeName)
        {
            CUPEQUESTION cq;
            try
            {
                cq = (from ent in dbo.CUPEQUESTION
                      where ent.NAME.TrimEnd() == cqName
                      select ent).Single();

                if (counName != "All")
                {
                    return (from ent in cq.CUPE
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName
                            select ent).ToList();
                }
                else
                {
                    return (from ent in cq.CUPE
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName
                            select ent).ToList();
                }

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
        public List<ITCAP> GetITCAPSForAttributeRegion(string itcqName, string regionName, string counName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                        select ent).Single();


                if (counName != "All")
                {
                    return (from ent in itcq.ITCAP
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }

                else
                {
                    return (from ent in itcq.ITCAP
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regionName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }

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
                              ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();

            }

            catch
            {
                return null;
            }
        }
        public List<ITCAP> GetITCAPSForAttributeRegionAndBusinessType(string itcqName, string regName, string counName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            ITCAPQUESTION itcq;
            try
            {
                itcq = (from ent in dbo.ITCAPQUESTION
                        where ent.NAME.TrimEnd() == itcqName
                        select ent).Single();

                if (counName != "All")
                {
                    return (from ent in itcq.ITCAP
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
                else
                {
                    return (from ent in itcq.ITCAP
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }

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
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapabilityRegion(string capName, string regName, string counName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {

                if (counName != "All")
                {
                    return (from ent in capability.CAPABILITYGAPINFO
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
                else
                {
                    return (from ent in capability.CAPABILITYGAPINFO
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
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
                              ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                              ent.CLIENT.STARTDATE > fromDate &&
                              ent.CLIENT.STARTDATE < toDate
                        select ent).ToList();
            }

            else
            {
                return null;
            }
        }
        public List<CAPABILITYGAPINFO> GetCapabilityGapInfosFromCapabilityRegionAndBusinessType(string capName, string regName, string counName, string busTypeName, DateTime fromDate, DateTime toDate)
        {
            CAPABILITY capability;
            if (GetCapability(capName, out capability))
            {
                if (counName != "All")
                {
                    return (from ent in capability.CAPABILITYGAPINFO
                            where ent.CLIENT.COUNTRY != null &&
                                  ent.CLIENT.COUNTRY.NAME.TrimEnd() == counName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }

                else
                {
                    return (from ent in capability.CAPABILITYGAPINFO
                            where ent.CLIENT.COUNTRY.REGION != null &&
                                  ent.CLIENT.COUNTRY.REGION.NAME.TrimEnd() == regName &&
                                  ent.CLIENT.BUSINESSTYPE != null &&
                                  ent.CLIENT.BUSINESSTYPE.NAME.TrimEnd() == busTypeName &&
                                  ent.CLIENT.STARTDATE > fromDate &&
                                  ent.CLIENT.STARTDATE < toDate
                            select ent).ToList();
                }
            }

            else
            {
                return null;
            }
        }
        #endregion
        
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
        
        public void UpdateDataFile(LoadingDatabase loadingScreen = null)
        {
            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Clearing old files";
                loadingScreen.LoadingTextLabel.Update();
            }

            if (!Directory.Exists(@"Resources\Clients"))
            {
                Directory.CreateDirectory(@"Resources\Clients");
            }

            List<string> clientFileNames = Directory.EnumerateFiles(@"Resources\Clients").ToList();
            List<string> clientNames = (from ent in clientFileNames
                                        select Path.GetFileNameWithoutExtension(ent)).ToList();

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Loading Clients";
                loadingScreen.LoadingTextLabel.Update();
            }

            CLIENT client;
            foreach (string clientName in clientNames)
            {
                if (!GetClient(clientName, out client)) continue;

                if (loadingScreen != null)
                {
                    loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing Client " + client.NAME.TrimEnd();
                    loadingScreen.LoadingTextLabel.Update();
                }
                XElement temp = new XElement("CLIENT");
                temp.Add(new XElement("NAME", client.NAME.TrimEnd()));
                temp.Add(new XElement("STARTDATE", client.STARTDATE.ToString()));
                temp.Add(new XElement("COUNTRY", client.COUNTRY.NAME.TrimEnd()));
                temp.Add(new XElement("REGION", client.COUNTRY.REGION.NAME.TrimEnd()));
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
                        tempCon.Add(new XElement("ID", contact.ID));

                        XElement cupeResConElement = new XElement("CUPERESPONSES");
                        foreach (CUPERESPONSE cupeResponse in contact.CUPERESPONSE)
                        {
                            XElement tempCUPERes = new XElement("CUPERESPONSE");
                            tempCUPERes.Add(new XElement("CUPE", cupeResponse.CUPE.NAME.TrimEnd()));
                            tempCUPERes.Add(new XElement("CURRENT", cupeResponse.CURRENT));
                            tempCUPERes.Add(new XElement("FUTURE", cupeResponse.FUTURE));
                            cupeResConElement.Add(tempCUPERes);
                        }
                        tempCon.Add(cupeResConElement);

                        conElement.Add(tempCon);
                    }
                    tempGrp.Add(conElement);

                    grpElement.Add(tempGrp);
                }
                temp.Add(grpElement);

                XElement bomElement = new XElement("BOMS");
                foreach (BOM bom in client.BOM)
                {
                    XElement tempBom = new XElement("BOM");
                    tempBom.Add(new XElement("IMPERATIVE", bom.IMPERATIVE.NAME.TrimEnd()));
                    tempBom.Add(new XElement("BUSINESSOBJECTIVE", bom.IMPERATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd()));
                    tempBom.Add(new XElement("CATEGORY", bom.IMPERATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd()));
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
                    tempItcap.Add(new XElement("ASISZEROS", itcap.ASISZEROS != null ? itcap.ASISZEROS : 0));
                    tempItcap.Add(new XElement("ASISONES", itcap.ASISONES != null ? itcap.ASISONES : 0));
                    tempItcap.Add(new XElement("ASISTWOS", itcap.ASISTWOS != null ? itcap.ASISTWOS : 0));
                    tempItcap.Add(new XElement("ASISTHREES", itcap.ASISTHREES != null ? itcap.ASISTHREES : 0));
                    tempItcap.Add(new XElement("ASISFOURS", itcap.ASISFOURS != null ? itcap.ASISFOURS : 0));
                    tempItcap.Add(new XElement("ASISFIVES", itcap.ASISFIVES != null ? itcap.ASISFIVES : 0));
                    tempItcap.Add(new XElement("TOBE", itcap.TOBE != null ? itcap.TOBE : 0));
                    tempItcap.Add(new XElement("TOBEZEROS", itcap.TOBEZEROS != null ? itcap.TOBEZEROS : 0));
                    tempItcap.Add(new XElement("TOBEONES", itcap.TOBEONES != null ? itcap.TOBEONES : 0));
                    tempItcap.Add(new XElement("TOBETWOS", itcap.TOBETWOS != null ? itcap.TOBETWOS : 0));
                    tempItcap.Add(new XElement("TOBETHREES", itcap.TOBETHREES != null ? itcap.TOBETHREES : 0));
                    tempItcap.Add(new XElement("TOBEFOURS", itcap.TOBEFOURS != null ? itcap.TOBEFOURS : 0));
                    tempItcap.Add(new XElement("TOBEFIVES", itcap.TOBEFIVES != null ? itcap.TOBEFIVES : 0));
                    
                    XElement commentsElement = new XElement("COMMENTS");
                    foreach (COMMENT comment in itcap.COMMENT)
                    {
                        commentsElement.Add(new XElement("COMMENT", comment.NAME.TrimEnd()));
                    }
                    tempItcap.Add(commentsElement);

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
                    tempCUPE.Add(new XElement("QUESTIONNUMBER", cupe.QUESTIONNUMBER));
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

                temp.Save(@"Resources\Clients\" + client.NAME.TrimEnd() + ".xml");
            }

            XElement root = new XElement("root");

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing Regions";
                loadingScreen.LoadingTextLabel.Update();
            }

            List<REGION> regList = dbo.REGION.ToList();
            XElement regElement = new XElement("REGIONS");
            foreach (REGION region in regList)
            {
                XElement tempReg = new XElement("REGION");
                tempReg.Add(new XElement("NAME", region.NAME.TrimEnd()));

                XElement counElement = new XElement("COUNTRIES");
                foreach (COUNTRY country in region.COUNTRY)
                {
                    XElement tempCoun = new XElement("COUNTRY");
                    tempCoun.Add(new XElement("NAME", country.NAME.TrimEnd()));
                    counElement.Add(tempCoun);
                }
                tempReg.Add(counElement);

                regElement.Add(tempReg);
            }
            root.Add(regElement);

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing BusinessTypes";
                loadingScreen.LoadingTextLabel.Update();
            }

            List<string> busTypeList = GetBusinessTypeNames();
            XElement busTypeElement = new XElement("BUSINESSTYPES");
            foreach (string busTypeName in busTypeList)
            {
                XElement tempBusType = new XElement("BUSINESSTYPE");
                tempBusType.Add(new XElement("NAME", busTypeName));
                busTypeElement.Add(tempBusType);
            }
            root.Add(busTypeElement);

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing BOM";
                loadingScreen.LoadingTextLabel.Update();
            }

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

                    XElement iniElement = new XElement("IMPERATIVES");
                    foreach (IMPERATIVE imperative in objective.IMPERATIVE)
                    {
                        XElement tempIni = new XElement("IMPERATIVE");
                        tempIni.Add(new XElement("NAME", imperative.NAME.TrimEnd()));
                        iniElement.Add(tempIni);
                    }
                    tempBus.Add(iniElement);

                    busElement.Add(tempBus);
                }
                temp.Add(busElement);

                catElement.Add(temp);
            }
            root.Add(catElement);

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing CUPE";
                loadingScreen.LoadingTextLabel.Update();
            }

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
                tempCQ.Add(new XElement("INTEN", cupeQuestion.INTEN));
                cqElement.Add(tempCQ);
            }
            root.Add(cqElement);

            if (loadingScreen != null)
            {
                loadingScreen.LoadingTextLabel.Text = "Updating filesystem... Writing ITCAP";
                loadingScreen.LoadingTextLabel.Update();
            }

            List<DOMAIN> domList = GetDomains();
            XElement domElement = new XElement("DOMAINS");
            foreach (DOMAIN domain in domList)
            {
                XElement temp = new XElement("DOMAIN");
                temp.Add(new XElement("NAME", domain.NAME.TrimEnd()));
                temp.Add(new XElement("ID", domain.ID.TrimEnd()));
                temp.Add(new XElement("DEFAULT", domain.DEFAULT));

                XElement capElement = new XElement("CAPABILITIES");
                foreach (CAPABILITY capability in domain.CAPABILITY)
                {
                    XElement tempCap = new XElement("CAPABILITY");
                    tempCap.Add(new XElement("NAME", capability.NAME.TrimEnd()));
                    tempCap.Add(new XElement("ID", capability.ID.TrimEnd()));
                    tempCap.Add(new XElement("DEFAULT", capability.DEFAULT));

                    XElement questionElement = new XElement("ITCAPQUESTIONS");
                    foreach (ITCAPQUESTION itcapQuestion in capability.ITCAPQUESTION)
                    {
                        XElement tempItcq = new XElement("ITCAPQUESTION");
                        tempItcq.Add(new XElement("NAME", itcapQuestion.NAME.TrimEnd()));
                        tempItcq.Add(new XElement("ID", itcapQuestion.ID.TrimEnd()));
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

            root.Save(@"Resources\Data.xml");
        }
        public void CheckChangeLog(LoadingDatabase loadingScreen = null)
        {
            List<string> failedChanges = new List<string>();

            //string line;
            string[] lineArray;

            CLIENT client;
            REGION region;
            COUNTRY country;
            BUSINESSTYPE busType;
            GROUP grp;
            CONTACT contact;
            CATEGORY category;
            BUSINESSOBJECTIVE objective;
            IMPERATIVE imperative;
            BOM bom;
            CUPEQUESTION cupeQuestion;
            CupeQuestionStringData cupeQuestionStringData;
            CUPE cupe;
            CUPERESPONSE cupeResponse;
            DOMAIN domain;
            CAPABILITY capability;
            ITCAPQUESTION itcapQuestion;
            ITCAP itcap;
            COMMENT comment;
            ITCAPOBJMAP itcapObjMap;
            CAPABILITYGAPINFO capGapInfo;

            if (!Directory.Exists("Resources"))
            {
                Directory.CreateDirectory("Resources");
            }

            if (!Directory.Exists(@"Resources\Clients"))
            {
                Directory.CreateDirectory(@"Resources\Clients");
            }

            if (!File.Exists(@"Resources\Changes.log"))
            {
                FileStream file = File.Create(@"Resources\Changes.log");
                file.Close();
            }

            using (System.IO.StreamReader file = new System.IO.StreamReader(@"Resources\Changes.log"))
            {
                if (loadingScreen != null)
                {
                    loadingScreen.LoadingTextLabel.Text = "Applying offline changes to database... Reading change log";
                    loadingScreen.LoadingTextLabel.Update();
                }
                string allLines = file.ReadToEnd();
                string[] allLinesArray = allLines.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //while ((line = file.ReadLine()) != null)
                float totalLines = allLinesArray.Length;
                int linesComplete = 0;
                foreach(string line in allLinesArray)
                {
                    lineArray = line.Split(' ');
                    if (lineArray[0] == "ADD")
                    {
                        switch(lineArray[1])
                        {
                            case "CLIENT":
                                client = new CLIENT();
                                client.NAME = lineArray[2].Replace('~', ' ');

                                string regionName = lineArray[3].Replace('~', ' ');
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

                                string countryName = lineArray[4].Replace('~', ' ');
                                try
                                {
                                    country = (from ent in region.COUNTRY
                                              where ent.NAME.TrimEnd() == countryName
                                              select ent).Single();
                                }
                                catch
                                {
                                    country = new COUNTRY();
                                    country.NAME = countryName;
                                    country.REGION = region;
                                    dbo.AddToCOUNTRY(country);
                                }

                                client.COUNTRY = country;

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

                            case "CONTACT":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetGroup(lineArray[3].Replace('~', ' '), client, out grp))
                                    {
                                        if (!AddContact(Convert.ToInt32(lineArray[4]), grp))
                                        {
                                            MessageBox.Show("Add Contact Instruction Failed: Contact ID already exists\n\n" + line, "Error");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Add Contact Instruction Failed: Group does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add Contact Instruction Failed: Client does not exist\n\n" + line, "Error");
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

                            case "IMPERATIVE":
                                if (GetObjective(lineArray[3].Replace('~', ' '), out objective))
                                {
                                    imperative = new IMPERATIVE();
                                    imperative.NAME = lineArray[2].Replace('~', ' ');
                                    imperative.BUSINESSOBJECTIVE = objective;
                                    if (!AddImperative(imperative))
                                    {
                                        MessageBox.Show("Add Imperative Instruction Failed: Imperative already exists\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Add Imperative Instruction Failed: BusinessObjective does not exist\n\n" + line, "Error");
                                }
                                break;

                            case "BOM":
                                if (lineArray[2] == "CLIENT")
                                {
                                    if (GetClient(lineArray[3].Replace('~', ' '), out client))
                                    {
                                        if (GetImperative(lineArray[4].Replace('~', ' '), out imperative))
                                        {
                                            bom = new BOM();
                                            bom.IMPERATIVE = imperative;
                                            if (!AddBOM(bom, client))
                                            {
                                                MessageBox.Show("Add BOM Instruction Failed: BOM already exists\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add BOM Instruction Failed: Imperative does not exist\n\n" + line, "Error");
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
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetGroup(lineArray[3].Replace('~', ' '), client, out grp))
                                    {
                                        if (GetContact(Convert.ToInt32(lineArray[4]), out contact))
                                        {
                                            if (GetCUPE(lineArray[5].Replace('~', ' '), client, out cupe))
                                            {
                                                cupeResponse = new CUPERESPONSE();
                                                cupeResponse.CONTACT = contact;
                                                cupeResponse.CUPE = cupe;
                                                cupeResponse.CURRENT = lineArray[6];
                                                cupeResponse.FUTURE = lineArray[7];
                                                dbo.AddToCUPERESPONSE(cupeResponse);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Add CUPEResponse Instruction Failed: CUPE does not exist\n\n" + line, "Error");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Add CUPEResponse Instruction Failed: Contact ID does not exist\n\n" + line, "Error");
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
                                            if (!AddCUPE(cupeQuestion.NAME.TrimEnd(), client, Convert.ToInt32(lineArray[5])))
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
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetCapability(lineArray[3].Replace('~', ' '), out capability))
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
                                    else if (lineArray[2] == "CUPECOMPLETE")
                                    {
                                        client.CUPECOMPLETE = "Y";
                                    }
                                    else if (lineArray[2] == "ITCAPCOMPLETE")
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
                                        if (GetCUPEQuestion(lineArray[4].Replace('~', ' '), out cupeQuestion))
                                        {
                                            cupe.NAME = lineArray[4].Replace('~', ' ');
                                            cupe.COMMODITY = lineArray[5].Replace('~', ' ');
                                            cupe.UTILITY = lineArray[6].Replace('~', ' ');
                                            cupe.PARTNER = lineArray[7].Replace('~', ' ');
                                            cupe.ENABLER = lineArray[8].Replace('~', ' ');
                                            cupe.CUPEQUESTION = cupeQuestion;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Update CUPE Instruction Failed: CUPEQuestion does not exist\n\n" + line, "Error");
                                        }
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
                                    cupeQuestion.INTEN = lineArray[4];
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
                                        itcap.ASISZEROS = Convert.ToInt32(lineArray[6]);
                                        itcap.TOBEZEROS = Convert.ToInt32(lineArray[7]);
                                        itcap.ASISONES = Convert.ToInt32(lineArray[8]);
                                        itcap.TOBEONES = Convert.ToInt32(lineArray[9]);
                                        itcap.ASISTWOS = Convert.ToInt32(lineArray[10]);
                                        itcap.TOBETWOS = Convert.ToInt32(lineArray[11]);
                                        itcap.ASISTHREES = Convert.ToInt32(lineArray[12]);
                                        itcap.TOBETHREES = Convert.ToInt32(lineArray[13]);
                                        itcap.ASISFOURS = Convert.ToInt32(lineArray[14]);
                                        itcap.TOBEFOURS = Convert.ToInt32(lineArray[15]);
                                        itcap.ASISFIVES = Convert.ToInt32(lineArray[16]);
                                        itcap.TOBEFIVES = Convert.ToInt32(lineArray[17]);
                                        List<COMMENT> commentsToDelete = itcap.COMMENT.ToList();
                                        foreach (COMMENT commentToDelete in commentsToDelete)
                                        {
                                            dbo.DeleteObject(commentToDelete);
                                        }
                                        for (int i = 18; i < lineArray.Count(); i++)
                                        {
                                            comment = new COMMENT();
                                            comment.NAME = lineArray[i].Replace('~', ' ');
                                            comment.ITCAP = itcap;
                                            dbo.AddToCOMMENT(comment);
                                        }
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
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetITCAPOBJMAP(client, lineArray[3].Replace('~', ' '), lineArray[4].Replace('~', ' '), out itcapObjMap))
                                    {
                                        itcapObjMap.SCORE = Convert.ToInt32(lineArray[5]);
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
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetCapabilityGapInfo(lineArray[3].Replace('~', ' '), client, out capGapInfo))
                                    {
                                        capGapInfo.GAPTYPE = lineArray[4];
                                        capGapInfo.PRIORITIZEDGAPTYPE = lineArray[5];
                                        capGapInfo.GAP = Convert.ToSingle(lineArray[6]);
                                        capGapInfo.PRIORITIZEDGAP = Convert.ToSingle(lineArray[7]);
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
                            case "BOM":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    if (GetBOM(lineArray[3].Replace('~', ' '), client, out bom))
                                    {
                                        dbo.DeleteObject(bom);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Delete BOM Instruction Failed: BOM does not exist\n\n" + line, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Delete BOM Instruction Failed: Client does not exist\n\n" + line, "Error");
                                }
                                break;
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
                            case "CONTACTS":
                                List<CONTACT> contactsToDelete;
                                List<CUPERESPONSE> responsesToDelete;
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    foreach (GROUP grpToClear in client.GROUP)
                                    {
                                        contactsToDelete = grpToClear.CONTACT.ToList();
                                        foreach (CONTACT contactToDelete in contactsToDelete)
                                        {
                                            responsesToDelete = contactToDelete.CUPERESPONSE.ToList();
                                            foreach (CUPERESPONSE responseToDelete in responsesToDelete)
                                            {
                                                dbo.DeleteObject(responseToDelete);
                                            }
                                            dbo.DeleteObject(contactToDelete);
                                        }
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

                    else if (lineArray[0] == "CLEAR")
                    {
                        switch (lineArray[1])
                        {
                            case "ITCAP":
                                if (GetClient(lineArray[2].Replace('~', ' '), out client))
                                {
                                    List<ITCAP> itcapsToDelete = client.ITCAP.ToList();
                                    foreach (ITCAP itcapToDelete in itcapsToDelete)
                                    {
                                        dbo.DeleteObject(itcapToDelete);
                                    }

                                    List<CAPABILITYGAPINFO> capGapInfosToDelete = client.CAPABILITYGAPINFO.ToList();
                                    foreach (CAPABILITYGAPINFO capGapInfoToDelete in capGapInfosToDelete)
                                    {
                                        dbo.DeleteObject(capGapInfoToDelete);
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
                        if (MessageBox.Show("Instruction failed to execute: \n" + line +
                                           "\n\nKeep change in log?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            failedChanges.Add(line);
                        }
                        break;
                    }

                    if (loadingScreen != null)
                    {
                        loadingScreen.LoadingTextLabel.Text = "Applying offline changes to database... " + (int)((linesComplete+=100)/totalLines) + "%";
                        loadingScreen.LoadingTextLabel.Update();
                    }
                }
            }

            File.WriteAllText(@"Resources\Changes.log", string.Empty);
            File.WriteAllLines(@"Resources\Changes.log", failedChanges);
        }
        #endregion
    }
}