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
            if (!Directory.Exists(@"Resources"))
            {
                Directory.CreateDirectory(@"Resources");
            }
            if (!Directory.Exists(@"Resources\Clients"))
            {
                Directory.CreateDirectory(@"Resources\Clients");
            }
            try
            {
                dbo = XElement.Load(@"Resources\Data.xml");
            }

            catch
            {
                dbo = new XElement("root");
                dbo.Add(new XElement("REGIONS"));
                dbo.Add(new XElement("BUSINESSTYPES"));
                dbo.Add(new XElement("CATEGORIES"));
                dbo.Add(new XElement("CUPEQUESTIONS"));
                dbo.Add(new XElement("DOMAINS"));
                if (!Directory.Exists("Resources"))
                {
                    Directory.CreateDirectory("Resources");
                }

                dbo.Save(@"Resources\Data.xml");
            }

            changeLog = new List<string>();
        }

        #region Client
        public List<XElement> GetClients()
        {
            /*return (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                    select ent).ToList();*/
            List<XElement> clients = new List<XElement>();
            foreach (string fileName in Directory.GetFiles(@"Resources\Clients"))
            {
                clients.Add(XElement.Load(fileName));
            }

            return clients;
        }

        public override string[] GetClientNames()
        {
            /*return (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                    select ent.Element("NAME").Value).ToArray();*/
            return (from ent in Directory.GetFiles(@"Resources\Clients")
                    select Path.GetFileNameWithoutExtension(ent)).ToArray();
        }

        public bool GetClient(string cntName, out XElement client)
        {
            try
            {
                /*client = (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                          where ent.Element("NAME").Value == cntName
                          select ent).Single();*/
                client = (from ent in Directory.GetFiles(@"Resources\Clients")
                          where Path.GetFileNameWithoutExtension(ent) == cntName
                          select XElement.Load(ent)).Single();
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
            if ((from ent in Directory.GetFiles(@"Resources\Clients")
                 where Path.GetFileNameWithoutExtension(ent) == client.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            client.Add(new XElement("GROUPS"));
            client.Add(new XElement("BOMS"));
            client.Add(new XElement("CUPES"));
            client.Add(new XElement("ITCAPS"));
            client.Add(new XElement("ITCAPOBJMAPS"));
            client.Add(new XElement("CAPABILITYGAPINFOS"));

            client.Save(@"Resources\Clients\" + client.Element("NAME").Value + ".xml");

            changeLog.Add("ADD CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                           client.Element("REGION").Value.Replace(' ', '~') + " " +
                           client.Element("COUNTRY").Value.Replace(' ', '~') + " " +
                           client.Element("STARTDATE").Value.Replace(' ', '~') + " " +
                           client.Element("BUSINESSTYPE").Value.Replace(' ', '~'));

            AddGroup("Business", client);
            AddGroup("IT", client);
            AddGroup("ITCAP", client);

            return true;
        }

        //Used by ClientDataControl
        public override Client AddClient(Client client)
        {
            XElement clientEnt = new XElement("CLIENT");

            clientEnt.Add(new XElement("NAME", client.Name));

            XElement region;
            try
            {
                region = (from ent in dbo.Element("REGIONS").Elements("REGION")
                          where ent.Element("NAME").Value == client.Region
                          select ent).Single();
            }
            catch
            {
                region = new XElement("REGION");
                region.Add(new XElement("NAME", client.Region));
                region.Add(new XElement("COUNTRIES"));
                dbo.Element("REGIONS").Add(region);
            }
            clientEnt.Add(new XElement("REGION", client.Region));

            XElement country;
            try
            {
                country = (from ent in region.Element("COUNTRIES").Elements("COUNTRY")
                           where ent.Element("NAME").Value == client.Country
                           select ent).Single();
            }
            catch
            {
                country = new XElement("COUNTRY");
                country.Add(new XElement("NAME", client.Country));
                region.Element("COUNTRIES").Add(country);
            }
            clientEnt.Add(new XElement("COUNTRY", client.Country));

            XElement busType;
            try
            {
                busType = (from ent in dbo.Element("BUSINESSTYPES").Elements("BUSINESSTYPE")
                           where ent.Element("NAME").Value == client.BusinessType
                           select ent).Single();
            }
            catch
            {
                busType = new XElement("BUSINESSTYPE");
                busType.Add(new XElement("NAME", client.BusinessType));
                dbo.Element("BUSINESSTYPES").Add(busType);
            }
            clientEnt.Add(new XElement("BUSINESSTYPE", busType));

            clientEnt.Add(new XElement("STARTDATE", client.StartDate));

            if (!AddClient(clientEnt))
            {
                return null;
            }

            clientEnt.Add(new XElement("BOMCOMPLETE", "N"));
            clientEnt.Add(new XElement("CUPECOMPLETE", "N"));
            clientEnt.Add(new XElement("ITCAPCOMPLETE", "N"));

            client.EntityObject = clientEnt;
            return client;
        }

        //Used by ClientDataControl
        public override Client LoadClient(string clientName)
        {
            XElement clientEnt;

            if (!GetClient(clientName, out clientEnt))
            {
                return null;
            }

            Client client = new Client();

            client.Name = clientEnt.Element("NAME").Value;
            client.Country = clientEnt.Element("COUNTRY").Value;
            client.Region = clientEnt.Element("REGION").Value;
            client.BusinessType = clientEnt.Element("BUSINESSTYPE").Value;
            client.StartDate = DateTime.Parse(clientEnt.Element("STARTDATE").Value);
            client.EntityObject = clientEnt;
            client.BomCompleted = clientEnt.Element("BOMCOMPLETE").Value == "Y";
            client.CupeCompleted = clientEnt.Element("CUPECOMPLETE").Value == "Y";
            client.ITCapCompleted = clientEnt.Element("ITCAPCOMPLETE").Value == "Y";
            return client;
        }

        public override Dictionary<string, float> GetObjectivesFromClientBOM(object clientObj)
        {
            XElement client = clientObj as XElement;

            List<XElement> entList = client.Element("BOMS").Elements("BOM").ToList();

            Dictionary<string, float> objDictionary = new Dictionary<string, float>();
            Dictionary<string, float> objImpCount = new Dictionary<string, float>();
            float bomScore;
            string objectiveName;
            foreach (XElement bomObj in entList)
            {
                if(bomObj.Element("EFFECTIVENESS").Value == "" || bomObj.Element("CRITICALITY").Value == "" || bomObj.Element("DIFFERENTIAL").Value == "") 
                {
                    continue;
                }
                bomScore = ((11 - Convert.ToSingle(bomObj.Element("EFFECTIVENESS").Value))*(Convert.ToSingle(bomObj.Element("CRITICALITY").Value)*.5f)) / (1+(Convert.ToSingle(bomObj.Element("DIFFERENTIAL").Value)*.5f));
                objectiveName = bomObj.Element("BUSINESSOBJECTIVE").Value;
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

        public override void ClientCompletedBOM(object clientObj)
        {
            XElement client = clientObj as XElement;
            if (client.Element("BOMCOMPLETE").Value == "N")
            {
                changeLog.Add("UPDATE CLIENT BOMCOMPLETE " + client.Element("NAME").Value.Replace(' ', '~'));
            }
            client.Element("BOMCOMPLETE").Value = "Y";
        }
        public override void ClientCompletedCUPE(object clientObj)
        {
            XElement client = clientObj as XElement;
            if (client.Element("CUPECOMPLETE").Value == "N")
            {
                changeLog.Add("UPDATE CLIENT CUPECOMPLETE " + client.Element("NAME").Value.Replace(' ', '~'));
            }
            client.Element("CUPECOMPLETE").Value = "Y";
        }
        public override void ClientCompletedITCAP(object clientObj)
        {
            XElement client = clientObj as XElement;
            if (client.Element("ITCAPCOMPLETE").Value == "N")
            {
                changeLog.Add("UPDATE CLIENT ITCAPCOMPLETE " + client.Element("NAME").Value.Replace(' ', '~'));
            }
            client.Element("ITCAPCOMPLETE").Value = "Y";
        }
        #endregion

        #region Region
        public override List<string> GetRegionNames()
        {
            return (from ent in dbo.Element("REGIONS").Elements("REGION")
                    select ent.Element("NAME").Value).ToList();
        }
        public override bool AddRegion(string regName)
        {
            //If already in DB, return false
            if ((from ent in dbo.Element("REGIONS").Elements("REGION")
                 where ent.Element("NAME").Value == regName
                 select ent).Count() != 0)
            {
                return false;
            }

            XElement region = new XElement("REGION");
            region.Add(new XElement("NAME", regName));
            region.Add(new XElement("COUNTRIES"));
            dbo.Element("REGIONS").Add(region);

            changeLog.Add("ADD REGION " + regName.Replace(' ', '~'));

            return true;
        }
        #endregion

        #region Country
        public override List<string> GetCountryNames(string regionName = "N/A")
        {
            if (regionName == "N/A")
            {
                return (from reg in dbo.Element("REGIONS").Elements("REGION")
                        from ent in reg.Element("COUNTRIES").Elements("COUNTRY")
                        select ent.Element("NAME").Value).ToList();
            }

            else
            {
                return (from reg in dbo.Element("REGIONS").Elements("REGION")
                        where reg.Element("NAME").Value == regionName
                        from ent in reg.Element("COUNTRIES").Elements("COUNTRY")
                        select ent.Element("NAME").Value).ToList();
            }
        }
        public override bool AddCountry(string countryName, string regionName)
        {
            //If already in DB, return false
            XElement region;
            try
            {
                region = (from ent in dbo.Element("REGIONS").Elements("REGION")
                          where ent.Element("NAME").Value == regionName
                          select ent).Single();
            }

            catch
            {
                return false;
            }

            if ((from ent in region.Element("COUNTRIES").Elements("COUNTRY")
                 where ent.Element("NAME").Value == countryName
                 select ent).Count() != 0)
            {
                return false;
            }

            XElement country = new XElement("COUNTRY");
            country.Add(new XElement("NAME", countryName));
            region.Element("COUNTRIES").Add(country);

            changeLog.Add("ADD COUNTRY " + countryName.Replace(' ', '~') + " " + regionName.Replace(' ', '~'));

            return true;
        }
        #endregion

        #region BusinessType
        public override List<string> GetBusinessTypeNames()
        {
            return (from ent in dbo.Element("BUSINESSTYPES").Elements("BUSINESSTYPE")
                    select ent.Element("NAME").Value).ToList();
        }
        public override bool AddBusinessType(string busTypeName)
        {
            if ((from ent in dbo.Element("BUSINESSTYPES").Elements("BUSINESSTYPE")
                 where ent.Element("NAME").Value == busTypeName
                 select ent).Count() != 0)
            {
                return false;
            }

            XElement busType = new XElement("BUSINESSTYPE");
            busType.Add(new XElement("NAME", busTypeName));
            dbo.Element("BUSINESSTYPES").Add(busType);

            changeLog.Add("ADD BUSINESSTYPE " + busTypeName.Replace(' ', '~'));

            return true;
        }
        #endregion

        #region Group
        //group is a keyword in C#
        public bool GetGroup(string grpName, XElement client, out XElement grp)
        {
            try
            {
                grp = (from ent in client.Element("GROUPS").Elements("GROUP")
                       where ent.Element("NAME").Value == grpName
                       select ent).Single();
            }

            catch
            {
                grp = null;
                return false;
            }

            return true;
        }
        public bool AddGroup(string grpName, XElement client)
        {
            //If Client points to 2 BOMs with same Imperative, return false
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
            grp.Add(new XElement("CUPES"));
            grp.Add(new XElement("ITCAPS"));
            grp.Add(new XElement("CONTACTS"));

            client.Element("GROUPS").Add(grp);

            return true;
        }
        #endregion

        #region Contact

        public bool GetContact(string contactName, XElement grp, out XElement contact)
        {
            try
            {
                contact = (from ent in grp.Element("CONTACTS").Elements("CONTACT")
                           where ent.Element("NAME").Value == contactName
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
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            XElement busGrp;
            XElement itGrp;
            GetGroup("Business", client, out busGrp);
            GetGroup("IT", client, out itGrp);
            Person person;
            CupeData cupeData;
            int id = 1;
            int questionIndex = 0;
            List<XElement> busContacts = busGrp.Element("CONTACTS").Elements("CONTACT").ToList();
            List<XElement> itContacts = itGrp.Element("CONTACTS").Elements("CONTACT").ToList();
            foreach (XElement contact in busContacts)
            {
                person = new Person(id);
                person.Type = Person.EmployeeType.Business;
                person.CodeName = "Business" + (id).ToString();
                cupeData = new CupeData(id);
                foreach (XElement response in contact.Element("CUPERESPONSES").Elements("CUPERESPONSE"))
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                    {
                        return question.QuestionText == response.Element("CUPE").Value;
                    });
                    if (questionIndex != -1)
                    {
                        cupeData.CurrentAnswers.Add("Question " + (questionIndex+1).ToString(), response.Element("CURRENT").Value != "" ? response.Element("CURRENT").Value[0] : ' ');
                        cupeData.FutureAnswers.Add("Question " + (questionIndex+1).ToString(), response.Element("FUTURE").Value != "" ? response.Element("FUTURE").Value[0] : ' ');
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }

            foreach (XElement contact in itContacts)
            {
                person = new Person(id);
                person.Type = Person.EmployeeType.IT;
                person.CodeName = "IT" + (id).ToString();
                cupeData = new CupeData(id);
                foreach (XElement response in contact.Element("CUPERESPONSES").Elements("CUPERESPONSE"))
                {
                    questionIndex = ClientDataControl.cupeQuestions.FindIndex(delegate(CupeQuestionStringData question)
                    {
                        return question.QuestionText == response.Element("CUPE").Value;
                    });
                    if (questionIndex != -1)
                    {
                        cupeData.CurrentAnswers.Add("Question " + (questionIndex + 1).ToString(), response.Element("CURRENT").Value != "" ? response.Element("CURRENT").Value[0] : ' ');
                        cupeData.FutureAnswers.Add("Question " + (questionIndex + 1).ToString(), response.Element("FUTURE").Value != "" ? response.Element("FUTURE").Value[0] : ' ');
                    }
                }
                person.cupeDataHolder = cupeData;
                ClientDataControl.AddParticipant(person);
                id++;
            }
        }
        #endregion

        #region BOM

        public bool GetBOM(string impName, XElement client, out XElement bom)
        {
            try
            {
                bom = (from ent in client.Element("BOMS").Elements("BOM")
                       where ent.Element("IMPERATIVE").Value == impName
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
            XElement client = clientObj as XElement;
            try
            {
                XElement bom = (from ent in client.Element("BOMS").Elements("BOM")
                                where ent.Element("IMPERATIVE").Value == ini.Name
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
            string iniXML = bom.Element("IMPERATIVE").Value;
            string busXML = bom.Element("BUSINESSOBJECTIVE").Value;
            string catXML = bom.Element("CATEGORY").Value;

            List<XElement> bomList = client.Element("BOMS").Elements("BOM").ToList();
            //If Client points to 2 BOMs with same Imperative, return false
            if ((from ent in bomList
                 where ent != null &&
                       ent.Element("IMPERATIVE") != null &&
                       ent.Element("IMPERATIVE").Value == iniXML
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

        public override bool RemoveBOM(string bomName, object clientObj)
        {
            XElement client = clientObj as XElement;
            XElement bom;
            if (!GetBOM(bomName, client, out bom))
            {
                return false;
            }

            bom.Remove();

            changeLog.Add("DELETE BOM " + client.Element("NAME").Value.Replace(' ','~') + " " +
                          bomName.Replace(' ', '~'));

            return true;
        }

        public override void BuildBOMForm(BOMTool bomForm)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;

            string catName;
            string busName;
            string iniName;

            NewCategory category;
            NewObjective objective;
            NewImperative imperative;

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
                    return bus.ObjName == busName;
                });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

                iniName = bom.Element("IMPERATIVE").Value.TrimEnd();
                imperative = objective.Imperatives.Find(delegate(NewImperative ini)
                {
                    return ini.Name == iniName;
                });
                if (imperative == null)
                {
                    imperative = objective.AddImperative(iniName);
                    imperative.Effectiveness = Convert.ToSingle(bom.Element("EFFECTIVENESS").Value);
                    imperative.Criticality = Convert.ToSingle(bom.Element("CRITICALITY").Value);
                    imperative.Differentiation = Convert.ToSingle(bom.Element("DIFFERENTIAL").Value);
                }
            }
        }

        public override List<string> GetObjectivesFromCurrentClientBOM()
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;

            List<string> allObjectiveNamesList = (from bom in client.Element("BOMS").Elements("BOM")
                                                  select bom.Element("BUSINESSOBJECTIVE").Value).ToList();

            List<string> result = new List<string>();
            foreach (string objectiveName in allObjectiveNamesList)
            {
                if (!result.Contains(objectiveName))
                {
                    result.Add(objectiveName);
                }
            }

            return result;
        }
        public override List<string> GetImperativesFromCurrentClientBOM()
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;

            return (from bom in client.Element("BOMS").Elements("BOM")
                    select bom.Element("IMPERATIVE").Value).ToList();

        }
        #endregion

        #region ITCAP

        public override bool RemoveITCAP(string itcqName, object clientObj)
        {
            XElement itcap;
            XElement client = clientObj as XElement;

            if (GetITCAP(itcqName, client, out itcap))
            {
                itcap.Remove();
                return true;
            }

            changeLog.Add("DELETE ITCAP " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                          itcap.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override bool UpdateITCAP(object clientObj, ITCapQuestion itcapQuestion)
        {
            Random rnd = new Random();
            XElement client = clientObj as XElement;
            try
            {
                XElement itcap = (from ent in client.Element("ITCAPS").Elements("ITCAP")
                                where ent.Element("ITCAPQUESTION").Value == itcapQuestion.Name
                                select ent).Single();

                itcap.Element("ASIS").Value = itcapQuestion.AsIsScore.ToString();
                itcap.Element("TOBE").Value = itcapQuestion.ToBeScore.ToString();
                itcap.Element("ASISZEROS").Value = itcapQuestion.AsIsNumZeros.ToString();
                itcap.Element("TOBEZEROS").Value = itcapQuestion.TobeNumZeros.ToString();
                itcap.Element("ASISONES").Value = itcapQuestion.AsIsNumOnes.ToString();
                itcap.Element("TOBEONES").Value = itcapQuestion.TobeNumOnes.ToString();
                itcap.Element("ASISTWOS").Value = itcapQuestion.AsIsNumTwos.ToString();
                itcap.Element("TOBETWOS").Value = itcapQuestion.TobeNumTwos.ToString();
                itcap.Element("ASISTHREES").Value = itcapQuestion.AsIsNumThrees.ToString();
                itcap.Element("TOBETHREES").Value = itcapQuestion.TobeNumThrees.ToString();
                itcap.Element("ASISFOURS").Value = itcapQuestion.AsIsNumFours.ToString();
                itcap.Element("TOBEFOURS").Value = itcapQuestion.TobeNumFours.ToString();
                itcap.Element("ASISFIVES").Value = itcapQuestion.AsIsNumFives.ToString();
                itcap.Element("TOBEFIVES").Value = itcapQuestion.TobeNumFives.ToString();

                string changeLogString = ("UPDATE ITCAP " + client.Element("NAME").Value.Replace(' ', '~') + " " + itcapQuestion.Name.Replace(' ', '~') + " " +
                                           itcapQuestion.AsIsScore + " " + itcapQuestion.ToBeScore + " " +
                                           itcapQuestion.AsIsNumZeros + " " + itcapQuestion.TobeNumZeros + " " +
                                           itcapQuestion.AsIsNumOnes + " " + itcapQuestion.TobeNumOnes + " " +
                                           itcapQuestion.AsIsNumTwos + " " + itcapQuestion.TobeNumTwos + " " +
                                           itcapQuestion.AsIsNumThrees + " " + itcapQuestion.TobeNumThrees + " " +
                                           itcapQuestion.AsIsNumFours + " " + itcapQuestion.TobeNumFours + " " +
                                           itcapQuestion.AsIsNumFives + " " + itcapQuestion.TobeNumFives);
                
                XElement commentsElement = itcap.Element("COMMENTS");
                commentsElement.RemoveAll();
                foreach (string comment in itcapQuestion.comment)
                {
                    commentsElement.Add(new XElement("COMMENT", comment));
                    changeLogString += (" " + comment.Replace(' ', '~'));
                }

                changeLog.Add(changeLogString);
            }

            catch
            {
                return false;
            }


            return true;
        }

        public override bool LoadITCAP(ref ITCapQuestion question)
        {
            XElement itcap;
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            if(GetITCAP(question.Name, client, out itcap))
            {
                if (itcap.Element("ASISZEROS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISZEROS").Value); i++)
                    {
                        question.AddAsIsAnswer(0);
                    }
                }
                if (itcap.Element("ASISONES").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISONES").Value); i++)
                    {
                        question.AddAsIsAnswer(1);
                    }
                }
                if (itcap.Element("ASISTWOS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISTWOS").Value); i++)
                    {
                        question.AddAsIsAnswer(2);
                    }
                }
                if (itcap.Element("ASISTHREES").Value != null)
                    {
                        for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISTHREES").Value); i++)
                        {
                            question.AddAsIsAnswer(3);
                        }
                    }
                if (itcap.Element("ASISFOURS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISFOURS").Value); i++)
                    {
                        question.AddAsIsAnswer(4);
                    }
                }
                if (itcap.Element("ASISFIVES").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("ASISFIVES").Value); i++)
                    {
                        question.AddAsIsAnswer(5);
                    }
                }
                if (itcap.Element("ASISZEROS").Value != null)
                {
                    question.AsIsScore = Convert.ToSingle(itcap.Element("ASISZEROS").Value);
                }
                if (itcap.Element("TOBEZEROS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBEZEROS").Value); i++)
                    {
                        question.AddToBeAnswer(0);
                    }
                }
                if (itcap.Element("TOBEONES").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBEONES").Value); i++)
                    {
                        question.AddToBeAnswer(1);
                    }
                }
                if (itcap.Element("TOBETWOS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBETWOS").Value); i++)
                    {
                        question.AddToBeAnswer(2);
                    }
                }
                if (itcap.Element("TOBETHREES").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBETHREES").Value); i++)
                    {
                        question.AddToBeAnswer(3);
                    }
                }
                if (itcap.Element("TOBEFOURS").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBEFOURS").Value); i++)
                    {
                        question.AddToBeAnswer(4);
                    }
                }
                if (itcap.Element("TOBEFIVES").Value != null)
                {
                    for (int i = 0; i < Convert.ToSingle(itcap.Element("TOBEFIVES").Value); i++)
                    {
                        question.AddToBeAnswer(5);
                    }
                }
                if (itcap.Element("TOBE").Value != null)
                {
                    question.ToBeScore = Convert.ToSingle(itcap.Element("TOBE").Value);
                }
                foreach (XElement comment in itcap.Element("COMMENTS").Elements("COMMENT"))
                {
                    question.AddComment(comment.Value);
                }
            }

            else
            {
                return false;
            }

            return true;
        }

        public override bool AddITCAP(object itcapObj, object clientObj)
        {
            XElement itcap = itcapObj as XElement;
            XElement client = clientObj as XElement;
            string itcqXML = itcap.Element("ITCAPQUESTION").Value;
            string capXML = itcap.Element("CAPABILITY").Value;
            string domXML = itcap.Element("DOMAIN").Value;

            List<XElement> itcapList = client.Element("ITCAPS").Elements("ITCAP").ToList();
            //If Client points to 2 BOMs with same Imperative, return false
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
            itcap.Add(new XElement("ASISZEROS", 0));
            itcap.Add(new XElement("TOBEZEROS", 0));
            itcap.Add(new XElement("ASISONES", 0));
            itcap.Add(new XElement("TOBEONES", 0));
            itcap.Add(new XElement("ASISTWOS", 0));
            itcap.Add(new XElement("TOBETWOS", 0));
            itcap.Add(new XElement("ASISTHREES", 0));
            itcap.Add(new XElement("TOBETHREES", 0));
            itcap.Add(new XElement("ASISFOURS", 0));
            itcap.Add(new XElement("TOBEFOURS", 0));
            itcap.Add(new XElement("ASISFIVES", 0));
            itcap.Add(new XElement("TOBEFIVES", 0));
            itcap.Add(new XElement("COMMENTS"));

            client.Element("ITCAPS").Add(itcap);

            changeLog.Add("ADD ITCAP CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " + itcqXML.Replace(' ', '~'));

            return true;
        }

        public override bool OpenITCAP(ITCapTool itcapForm)
        {
            if (ClientDataControl.Client.EntityObject == null)
            {
                MessageBox.Show("Must choose client before opening ITCAP", "Error");
                return false;
            }

            List<XElement> itcapList = (ClientDataControl.Client.EntityObject as XElement).Element("ITCAPS").Elements("ITCAP").ToList();

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
                    domain.ID = domEnt.Element("ID").Value;
                }

                capability = itcapForm.capabilities.Find(delegate(Capability cap)
                {
                    return cap.CapName == capName;
                });
                if (capability == null)
                {
                    capability = new Capability();
                    capability.CapName = capName;
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
                    capability.ID = capEnt.Element("ID").Value;
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
                itcapQuestion.ID = itcqEnt.Element("ID").Value;
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
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            client.Element("ITCAPS").RemoveAll();
            client.Element("CAPABILITYGAPINFOS").RemoveAll();

            changeLog.Add("CLEAR ITCAP " + client.Element("NAME").Value.Replace(' ', '~'));

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
                            itcapEnt = new XElement("ITCAP");
                            itcapEnt.Add(new XElement("ITCAPQUESTION", itcapQuestion.Name));
                            itcapEnt.Add(new XElement("CAPABILITY", capability.CapName));
                            itcapEnt.Add(new XElement("DOMAIN", domain.Name));
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
            bomForm.imperativeNames.Items.Clear();
            bomForm.imperativeNames.Text = "";
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

            objective.Add(new XElement("IMPERATIVES"));

            category.Element("BUSINESSOBJECTIVES").Add(objective);

            changeLog.Add("ADD BUSINESSOBJECTIVE " + objective.Element("NAME").Value.Replace(' ', '~') + " " +
                            category.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override void ChangedObjective(BOMTool bomForm)
        {
            bomForm.imperativeNames.Items.Clear();
            bomForm.imperativeNames.Text = "<Select Imperative>";
            XElement objective;
            if (GetObjective(bomForm.objectiveNames.Text.Trim(), out objective))
            {
                bomForm.imperativeNames.Items.AddRange((from ent in objective.Element("IMPERATIVES").Elements("IMPERATIVE")
                                                        select ent.Element("NAME").Value).ToArray());
            }
        }

        #endregion

        #region Imperative
        public bool GetImperative(string iniName, out XElement Imperative)
        {
            try
            {
                Imperative = (from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                              from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                              from ent in bus.Element("IMPERATIVES").Elements("IMPERATIVE")
                              where ent.Element("NAME").Value == iniName
                              select ent).Single();
            }

            catch
            {
                Imperative = null;
                return false;
            }

            return true;
        }

        public bool GetImperative(int iniID, out XElement Imperative)
        {
            try
            {
                Imperative = (from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                              from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                              from ent in bus.Element("IMPERATIVES").Elements("IMPERATIVE")
                              where ent.Element("IMPERATIVEID").Value == iniID.ToString()
                              select ent).Single();
            }

            catch
            {
                Imperative = null;
                return false;
            }

            return true;
        }

        public bool AddImperative(XElement imperative, XElement objective, XElement category)
        {
            //If already in DB, return 1
            if ((from cat in dbo.Element("CATEGORIES").Elements("CATEGORY")
                 where cat.Element("NAME").Value == category.Element("NAME").Value
                 from bus in cat.Element("BUSINESSOBJECTIVES").Elements("BUSINESSOBJECTIVE")
                 where bus.Element("NAME").Value == objective.Element("NAME").Value
                 from ent in bus.Element("IMPERATIVES").Elements("IMPERATIVE")
                 where ent.Element("NAME").Value == imperative.Element("NAME").Value
                 select ent).Count() != 0)
            {
                return false;
            }

            objective.Element("IMPERATIVES").Add(imperative);

            changeLog.Add("ADD IMPERATIVE " + imperative.Element("NAME").Value.Replace(' ', '~') + " " +
                            objective.Element("NAME").Value.Replace(' ', '~') + " " +
                            category.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override bool AddImperativeToBOM(string iniName, string busName, string catName, BOMTool bomForm)
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

            XElement imperativeXML;
            if (!GetImperative(iniName, out imperativeXML))
            {
                imperativeXML = new XElement("IMPERATIVE");
                imperativeXML.Add(new XElement("NAME", iniName));
                if (!AddImperative(imperativeXML, objectiveXML, categoryXML))
                {
                    MessageBox.Show("Failed to add Imperative to File", "Error");
                    return false;
                }
            }

            XElement bom = new XElement("BOM");
            bom.Add(new XElement("IMPERATIVE", imperativeXML.Element("NAME").Value));
            bom.Add(new XElement("BUSINESSOBJECTIVE", objectiveXML.Element("NAME").Value));
            bom.Add(new XElement("CATEGORY", categoryXML.Element("NAME").Value));


            if (!AddBOM(bom, ClientDataControl.Client.EntityObject))
            {
                MessageBox.Show("Failed to add Imperative to BOM", "Error");
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
                    return bus.ObjName == busName;
                });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

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

        public bool GetCUPEQuestion(string cqName, out XElement cupeQuestion)
        {
            try
            {
                cupeQuestion = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                where ent.Element("NAME").Value == cqName
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
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  select ent).ToList();

            List<CupeQuestionData> cupeQuestionDataList = new List<CupeQuestionData>();
            CupeQuestionData cupeQuestionData;
            CupeQuestionStringData cupeQuestionStringData;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestionStringData = new CupeQuestionStringData();
                cupeQuestionStringData.OriginalQuestionText = cupeQuestionStringData.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestionStringData.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestionStringData.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestionStringData.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestionStringData.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionData = new CupeQuestionData();
                cupeQuestionData.StringData = cupeQuestionStringData;
                cupeQuestionData.InDefault20 = cupeQuestionEnt.Element("INTWENTY").Value == "Y";
                cupeQuestionData.InDefault10 = cupeQuestionEnt.Element("INTEN").Value == "Y";
                cupeQuestionDataList.Add(cupeQuestionData);
            }

            return cupeQuestionDataList;
        }

        public override List<CupeQuestionStringData> GetCUPEQuestionStringData()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                      select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        public override List<CupeQuestionStringData> GetCUPEQuestionStringDataTwenty()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  where ent.Element("INTWENTY").Value == "Y"
                                                  select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
                cupeQuestion.ChoiceA = cupeQuestionEnt.Element("COMMODITY").Value;
                cupeQuestion.ChoiceB = cupeQuestionEnt.Element("UTILITY").Value;
                cupeQuestion.ChoiceC = cupeQuestionEnt.Element("PARTNER").Value;
                cupeQuestion.ChoiceD = cupeQuestionEnt.Element("ENABLER").Value;
                cupeQuestionList.Add(cupeQuestion);
            }

            return cupeQuestionList;
        }
        
        public override List<CupeQuestionStringData> GetCUPEQuestionStringDataTen()
        {
            List<XElement> cupeQuestionEntList = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                                  where ent.Element("INTEN").Value == "Y"
                                                  select ent).ToList();

            List<CupeQuestionStringData> cupeQuestionList = new List<CupeQuestionStringData>();
            CupeQuestionStringData cupeQuestion;
            foreach (XElement cupeQuestionEnt in cupeQuestionEntList)
            {
                cupeQuestion = new CupeQuestionStringData();
                cupeQuestion.OriginalQuestionText = cupeQuestion.QuestionText = cupeQuestionEnt.Element("NAME").Value;
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
            cupeQuestionEnt.Add(new XElement("INTEN", "N"));

            dbo.Element("CUPEQUESTIONS").Add(cupeQuestionEnt);

            changeLog.Add("ADD CUPEQUESTION " + question.Replace(' ', '~') + " " +
                          commodity.Replace(' ', '~') + " " + utility.Replace(' ', '~') + " " +
                          partner.Replace(' ', '~') + " " + enabler.Replace(' ', '~'));

            return true;
        }

        public override List<CupeQuestionStringData> GetCUPESForClient()
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            List<XElement> cupeList = (from ent in client.Element("CUPES").Elements("CUPE")
                                       orderby Convert.ToInt32(ent.Element("QUESTIONNUMBER").Value)
                                       select ent).ToList();
            List<CupeQuestionStringData> cupeQuestions = new List<CupeQuestionStringData>();
            CupeQuestionStringData data;
            foreach (XElement cupe in cupeList)
            {
                data = new CupeQuestionStringData();
                data.QuestionText = cupe.Element("NAME").Value;
                data.OriginalQuestionText = cupe.Element("CUPEQUESTION").Value;
                data.ChoiceA = cupe.Element("COMMODITY").Value;
                data.ChoiceB = cupe.Element("UTILITY").Value;
                data.ChoiceC = cupe.Element("PARTNER").Value;
                data.ChoiceD = cupe.Element("ENABLER").Value;
                cupeQuestions.Add(data);
            }

            return cupeQuestions;
        } 

        public override bool UpdateCupeQuestion(string cupeQuestion, bool inTwenty, bool inTen)
        {
            XElement cupeQuestionEnt;
            try
            {
                cupeQuestionEnt = (from ent in dbo.Element("CUPEQUESTIONS").Elements("CUPEQUESTION")
                                   where ent.Element("NAME").Value == cupeQuestion
                                   select ent).Single();

                string inTwentyStr = inTwenty ? "Y" : "N";
                string inTenStr = inTen ? "Y" : "N";

                cupeQuestionEnt.Element("INTWENTY").Value = inTwentyStr;
                cupeQuestionEnt.Element("INTEN").Value = inTenStr;

                changeLog.Add("UPDATE CUPEQUESTION " + cupeQuestion.Replace(' ', '~') + " " +
                              inTwentyStr + " " + 
                              inTenStr);
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
            XElement client = clientObj as XElement;
            List<XElement> responsesToDelete;
            foreach (XElement grp in client.Element("GROUPS").Elements("GROUP"))
            {
                foreach (XElement contact in grp.Element("CONTACTS").Elements("CONTACT"))
                {
                    responsesToDelete = contact.Element("CUPERESPONSES").Elements("CUPERESPONSE").ToList();
                    foreach(XElement cupeResponse in responsesToDelete)
                    {
                        cupeResponse.Remove();
                    }
                }
            }
            List<XElement> cupesToDelete = client.Element("CUPES").Elements("CUPE").ToList();
            foreach (XElement cupe in cupesToDelete)
            {
                cupe.Remove();
            }
            changeLog.Add("DELETE CUPE " + client.Element("NAME").Value.Replace(' ', '~'));
        }

        public bool GetCUPE(string name, out XElement cupe)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            try
            {
                cupe = (from ent in client.Element("CUPES").Elements("CUPE")
                        where ent.Element("NAME").Value == name
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
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            XElement busGrp;
            XElement itGrp;
            if (!GetGroup("Business", client, out busGrp))
            {
                AddGroup("Business", client);
                GetGroup("Business", client, out busGrp);
            }
            if (!GetGroup("IT", client, out itGrp))
            {
                AddGroup("IT", client);
                GetGroup("IT", client, out itGrp);
            }
            List<XElement> contactsToDelete = busGrp.Element("CONTACTS").Elements("CONTACT").ToList();
            List<XElement> responsesToDelete;
            foreach (XElement contactToDelete in contactsToDelete)
            {
                responsesToDelete = contactToDelete.Element("CUPERESPONSES").Elements("CUPERESPONSE").ToList();
                foreach (XElement responseToDelete in responsesToDelete)
                {
                    responseToDelete.Remove();
                }
                contactToDelete.Remove();
            }
            contactsToDelete = itGrp.Element("CONTACTS").Elements("CONTACT").ToList();
            foreach (XElement contactToDelete in contactsToDelete)
            {
                responsesToDelete = contactToDelete.Element("CUPERESPONSES").Elements("CUPERESPONSE").ToList();
                foreach (XElement responseToDelete in responsesToDelete)
                {
                    responseToDelete.Remove();
                }
                contactToDelete.Remove();
            }
            changeLog.Add("DELETE CONTACTS " + client.Element("NAME").Value.Replace(' ', '~'));
            XElement contact;
            XElement response;
            XElement cupe;
            foreach (Person person in personList)
            {
                if (person.Type == Person.EmployeeType.Business)
                {
                    contact = new XElement("CONTACT");
                    contact.Add(new XElement("ID", rnd.Next()));
                    contact.Add(new XElement("CUPERESPONSES"));
                    busGrp.Element("CONTACTS").Add(contact);
                    changeLog.Add("ADD CONTACT " + client.Element("NAME").Value.Replace(' ', '~') +
                                  " Business " + contact.Element("ID").Value.Replace(' ', '~'));

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for (int i = 0; i < questionList.Count; i++)
                    {
                        CupeQuestionStringData data = questionList[i];
                        if (!GetCUPE(data.QuestionText, out cupe))
                        {
                            MessageBox.Show("Error: couldn't find cupe: " + data.QuestionText);
                            continue;
                        }

                        response = new XElement("CUPERESPONSE");
                        response.Add(new XElement("CUPE", cupe.Element("NAME").Value));
                        response.Add(new XElement("CURRENT", person.cupeDataHolder.CurrentAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.CurrentAnswers["Question " + (i + 1).ToString()].ToString() : ""));
                        response.Add(new XElement("FUTURE", person.cupeDataHolder.FutureAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.FutureAnswers["Question " + (i + 1).ToString()].ToString() : ""));
                        AddCupeResponse(response, contact);
                    }
                }

                else if (person.Type == Person.EmployeeType.IT)
                {
                    contact = new XElement("CONTACT");
                    contact.Add(new XElement("ID", rnd.Next()));
                    contact.Add(new XElement("CUPERESPONSES"));
                    itGrp.Element("CONTACTS").Add(contact);
                    changeLog.Add("ADD CONTACT " + client.Element("NAME").Value.Replace(' ', '~') +
                                  " IT " + contact.Element("ID").Value.Replace(' ', '~'));

                    List<CupeQuestionStringData> questionList = ClientDataControl.GetCupeQuestions();
                    for (int i = 0; i < questionList.Count; i++)
                    {
                        CupeQuestionStringData data = questionList[i];
                        if (!GetCUPE(data.QuestionText, out cupe))
                        {
                            MessageBox.Show("Error: couldn't find cupe: " + data.QuestionText);
                            continue;
                        }
                        response = new XElement("CUPERESPONSE");
                        response.Add(new XElement("CUPE", cupe.Element("NAME").Value));
                        response.Add(new XElement("CURRENT", person.cupeDataHolder.CurrentAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.CurrentAnswers["Question " + (i + 1).ToString()].ToString() : ""));
                        response.Add(new XElement("FUTURE", person.cupeDataHolder.FutureAnswers.ContainsKey("Question " + (i + 1).ToString()) ? person.cupeDataHolder.FutureAnswers["Question " + (i + 1).ToString()].ToString() : ""));
                        AddCupeResponse(response, contact);
                    }
                }
            }
        }

        public override string UpdateCUPE(CupeQuestionStringData cupeQuestion)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            try
            {
                XElement cupe = (from ent in client.Element("CUPES").Elements("CUPE")
                             where ent.Element("CUPEQUESTION").Value == cupeQuestion.OriginalQuestionText
                             select ent).Single();
                cupe.Element("NAME").Value = cupeQuestion.QuestionText;
                cupe.Element("COMMODITY").Value = cupeQuestion.ChoiceA;
                cupe.Element("UTILITY").Value = cupeQuestion.ChoiceB;
                cupe.Element("PARTNER").Value = cupeQuestion.ChoiceC;
                cupe.Element("ENABLER").Value = cupeQuestion.ChoiceD;

                if (cupe.Element("NAME").Value != cupe.Element("CUPEQUESTION").Value)
                {
                    XElement cupeQuestionEnt;
                    if (!GetCUPEQuestion(cupe.Element("NAME").Value, out cupeQuestionEnt))
                    {
                        string question = cupe.Element("NAME").Value;
                        string commodity = cupe.Element("COMMODITY").Value;
                        string utility = cupe.Element("UTILITY").Value;
                        string partner = cupe.Element("PARTNER").Value;
                        string enabler = cupe.Element("ENABLER").Value;

                        cupeQuestionEnt = new XElement("CUPEQUESTION");
                        cupeQuestionEnt.Add(new XElement("NAME", question));
                        cupeQuestionEnt.Add(new XElement("COMMODITY", commodity));
                        cupeQuestionEnt.Add(new XElement("UTILITY", utility));
                        cupeQuestionEnt.Add(new XElement("PARTNER", partner));
                        cupeQuestionEnt.Add(new XElement("ENABLER", enabler));
                        cupeQuestionEnt.Add(new XElement("INTWENTY", "N"));
                        cupeQuestionEnt.Add(new XElement("INTEN", "N"));


                        changeLog.Add("ADD CUPEQUESTION " + question.Replace(' ', '~') + " " +
                                      commodity.Replace(' ', '~') + " " + utility.Replace(' ', '~') + " " +
                                      partner.Replace(' ', '~') + " " + enabler.Replace(' ', '~'));
                    }

                    changeLog.Add("UPDATE CUPE " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                              cupeQuestion.OriginalQuestionText.Replace(' ', '~') + " " +
                              cupeQuestion.QuestionText.Replace(' ', '~') + " " +
                              cupeQuestion.ChoiceA.Replace(' ', '~') + " " +
                              cupeQuestion.ChoiceB.Replace(' ', '~') + " " +
                              cupeQuestion.ChoiceC.Replace(' ', '~') + " " +
                              cupeQuestion.ChoiceD.Replace(' ', '~'));

                    cupe.Element("CUPEQUESTION").Value = cupeQuestion.OriginalQuestionText = cupeQuestionEnt.Element("NAME").Value;
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
            cupe.Add(new XElement("QUESTIONNUMBER", questionNumber));
            client.Element("CUPES").Add(cupe);

            changeLog.Add("ADD CUPE CLIENT " + client.Element("NAME").Value.Replace(' ', '~') + " " + question.Replace(' ', '~') + " " + questionNumber);

            return true;
        }
        
        public override void PopulateCUPEQuestionsForClient(CUPETool cupeForm)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
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

        #region CupeResponse
        public bool AddCupeResponse(XElement cupeResponse, XElement contact)
        {
            string cupe = cupeResponse.Element("CUPE").Value;
            if((from ent in contact.Element("CUPERESPONSES").Elements("CUPERESPONSE")
                where ent.Element("CUPE").Value == cupe
                select ent).Count() != 0)
            {
                return false;
            }

            contact.Element("CUPERESPONSES").Add(cupeResponse);
            changeLog.Add("ADD CUPERESPONSE " + contact.Parent.Parent.Parent.Parent.Element("NAME").Value.Replace(' ', '~') + " " +
                          contact.Parent.Parent.Element("NAME").Value.Replace(' ', '~') + " " +
                          contact.Element("ID").Value + " " +
                          cupe.Replace(' ', '~') + " " +
                          cupeResponse.Element("CURRENT").Value + " " + 
                          cupeResponse.Element("FUTURE").Value);
            return true;
        }
        #endregion

        #region ScoringEntity
        public override string GetScoringEntityID(string entName)
        {
            XElement domain;
            if (GetDomain(entName, out domain))
            {
                return domain.Element("ID").Value;
            }

            XElement capability;
            if (GetCapability(entName, out capability))
            {
                return capability.Element("ID").Value;
            }

            XElement itcapQuestion;
            if (GetITCAPQuestion(entName, out itcapQuestion))
            {
                return itcapQuestion.Element("ID").Value;
            }

            return "";
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

            domain.Add(new XElement("ID", (dbo.Element("DOMAINS").Elements("DOMAIN").Count()+1).ToString() + ".0.0"));

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

            capability.Add(new XElement("ID", domain.Element("ID").Value[0] + "." + 
                                             (domain.Element("CAPABILITIES").Elements("CAPABILITY").Count()+1).ToString() + ".0"));

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

        #region CapabilityGapInfo
        public bool GetCapabilityGapInfo(string capName, out XElement capGapInfo)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            try
            {
                capGapInfo = (from ent in client.Element("CAPABILITYGAPINFOS").Elements("CAPABILITYGAPINFO")
                              where ent.Element("CAPABILITY").Value == capName
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
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            XElement capGapInfo;
            if (!GetCapabilityGapInfo(capability.CapName, out capGapInfo))
            {
                capGapInfo = new XElement("CAPABILITYGAPINFO");
                XElement capabilityEnt;
                GetCapability(capability.CapName, out capabilityEnt);
                capGapInfo.Add(new XElement("CAPABILITY", capability.CapName));
                capGapInfo.Add(new XElement("GAPTYPE", "None"));
                capGapInfo.Add(new XElement("GAP", "0"));
                capGapInfo.Add(new XElement("PRIORITIZEDGAPTYPE", "None"));
                capGapInfo.Add(new XElement("PRIORITIZEDGAP", "0"));
                client.Element("CAPABILITYGAPINFOS").Add(capGapInfo);

                changeLog.Add("ADD CAPABILITYGAPINFO " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                              capability.CapName.Replace(' ', '~'));
            }
            switch (capability.GapType1)
            {
                case ScoringEntity.GapType.High:
                    capGapInfo.Element("GAPTYPE").Value = "High";
                    break;

                case ScoringEntity.GapType.Middle:
                    capGapInfo.Element("GAPTYPE").Value = "Middle";
                    break;

                case ScoringEntity.GapType.Low:
                    capGapInfo.Element("GAPTYPE").Value = "Low";
                    break;

                case ScoringEntity.GapType.None:
                    capGapInfo.Element("GAPTYPE").Value = "None";
                    break;
            }

            switch (capability.PrioritizedGapType1)
            {
                case ScoringEntity.PrioritizedGapType.High:
                    capGapInfo.Element("PRIORITIZEDGAPTYPE").Value = "High";
                    break;

                case ScoringEntity.PrioritizedGapType.Middle:
                    capGapInfo.Element("PRIORITIZEDGAPTYPE").Value = "Middle";
                    break;

                case ScoringEntity.PrioritizedGapType.Low:
                    capGapInfo.Element("PRIORITIZEDGAPTYPE").Value = "Low";
                    break;

                case ScoringEntity.PrioritizedGapType.None:
                    capGapInfo.Element("PRIORITIZEDGAPTYPE").Value = "None";
                    break;
            }

            capGapInfo.Element("PRIORITIZEDGAP").Value = capability.PrioritizedCapabilityGap.ToString();
            capGapInfo.Element("GAP").Value = capability.CapabilityGap.ToString();

            changeLog.Add("UPDATE CAPABILITYGAPINFO " + client.Element("NAME").Value.Replace(' ', '~') + " " +
                          capability.CapName.Replace(' ', '~') + " " +
                          capGapInfo.Element("GAPTYPE").Value.Replace(' ', '~') + " " +
                          capGapInfo.Element("PRIORITIZEDGAPTYPE").Value.Replace(' ', '~') + " " +
                          capGapInfo.Element("GAP").Value + " " +
                          capGapInfo.Element("PRIORITIZEDGAP").Value);
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

        public List<XElement> GetITCAPs(string itcqName, XElement client)
        {
            XElement grp;
            if (GetGroup("ITCAP", client, out grp))
            {
                return (from con in grp.Element("CONTACTS").Elements("CONTACT")
                        from ent in con.Element("ITCAPS").Elements("ITCAP")
                        where ent.Element("ITCAPQUESTION").Value == itcqName
                        select ent).ToList();
            }
            else
            {
                return null;
            }
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

            itcapQuestion.Add(new XElement("ID", domain.Element("ID").Value[0] + "." + 
                                                 capability.Element("ID").Value[2] + "." +
                                                 (capability.Element("ITCAPQUESTIONS").Elements("ITCAPQUESTION").Count()+1).ToString()));

            capability.Element("ITCAPQUESTIONS").Add(itcapQuestion);

            changeLog.Add("ADD ITCAPQUESTION " + itcapQuestion.Element("NAME").Value.Replace(' ', '~') + " " +
                            capability.Element("NAME").Value.Replace(' ', '~') + " " +
                            domain.Element("NAME").Value.Replace(' ', '~'));

            return true;
        }

        public override void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm, out int alreadyExists, out string owner)
        {
            alreadyExists = 0;
            owner = "";
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

            XElement itcap = new XElement("ITCAP");
            itcap.Add(new XElement("ITCAPQUESTION", itcapQuestionXML.Element("NAME").Value));
            itcap.Add(new XElement("CAPABILITY", capabilityXML.Element("NAME").Value));
            itcap.Add(new XElement("DOMAIN", domainXML.Element("NAME").Value));


            if (!AddITCAP(itcap, ClientDataControl.Client.EntityObject))
            {
                MessageBox.Show("Failed to add ITCAPQuestion to ITCAP", "Error");
                return;
            }

            if (!SaveChanges())
            {
                MessageBox.Show("Failed to save changes to File", "Error");
                return;
            }
        }
        public override void RemoveQuestionToITCAP(string itcqName)
        {
            XElement client = ClientDataControl.Client.EntityObject as XElement;
            XElement itcap;
            if (GetITCAP(itcqName, client, out itcap))
            {
                itcap.Remove();
            }
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

                dbo.Save(@"Resources\Data.xml");

                XElement client = ClientDataControl.Client.EntityObject as XElement;
                client.Save(@"Resources\Clients/" + client.Element("NAME").Value + ".xml");

                if (!File.Exists(@"Resources\Changes.log"))
                {
                    FileStream file = File.Create(@"Resources\Changes.log");
                    file.Close();
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Resources\Changes.log", true))
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