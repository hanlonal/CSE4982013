using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public class FileManager
    {
        public XElement dbo;

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
                dbo.Add(new XElement("GROUPS"));
                dbo.Add(new XElement("CATEGORIES"));
                dbo.Add(new XElement("BUSINESSOBJECTIVES"));
                dbo.Add(new XElement("INITIATIVES"));
                dbo.Add(new XElement("BOMS"));
                dbo.Save("Data.xml");
            }
        }

        #region Client
        public List<XElement> GetClients()
        {
            return (from ent in dbo.Element("CLIENTS").Elements("CLIENT")
                    select ent).ToList();
        }

        public string[] GetClientNames()
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

            return true;
        }
        #endregion
/*
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
        */
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
            grp.Add("NAME", grpName);

            grp.Add(new XElement("GROUPID", -1));

            grp.Add(new XElement("BOMS"));

            client.Element("GROUPS").Add(grp);

            return true;
        }
        /*
        #endregion

        #region BOM
        public List<BOM> GetBOMs()
        {
            return (from ent in dbo.BOM
                    select ent).ToList();
        }
        */
        public bool UpdateBOM(XElement client, NewInitiative ini)
        {
            string formattedName = ini.Name.Replace(' ', '~');
            try
            {
                XElement bom = (from ent in client.Element("BOMS").Elements("BOM")
                                where ent.Element("INITIATIVE").Value == formattedName
                                select ent).Single();

                bom.Element("EFFECTIVENESS").Value = ini.Effectiveness.ToString();
                bom.Element("CRITICALITY").Value = ini.Criticality.ToString();
                bom.Element("DIFFERENTIAL").Value = ini.Differentiation.ToString();
            }

            catch
            {
                return false;
            }


            return true;
        }

        public bool AddBOM(XElement bom, XElement client, string iniName, string busName, string catName)
        {
            iniName = iniName.Replace(' ', '~');
            busName = busName.Replace(' ', '~');
            catName = catName.Replace(' ', '~');
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in client.Element("BOMS").Elements("BOM")
                 where ent != null &&
                       ent.Element("INITIATIVE") != null &&
                       ent.Element("INITIATIVE").Value == iniName
                 select ent).Count() != 0)
            {
                return false;
            }

            bom.Add(new XElement("BOMID", -1));
            bom.Add(new XElement("INITIATIVE", iniName));
            bom.Add(new XElement("BUSINESSOBJECTIVE", busName));
            bom.Add(new XElement("CATEGORY", catName));
            bom.Add(new XElement("EFFECTIVENESS", 0));
            bom.Add(new XElement("CRITICALITY", 0));
            bom.Add(new XElement("DIFFERENTIAL", 0));

            client.Element("BOMS").Add(bom);

            return true;
        }

        public bool BuildBOMForm(BOMTool bomForm, string clientName)
        {
            XElement client;

            if (GetClient(clientName, out client))
            {
                bomForm.flclient = client;

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
                return false;
            }
        }
 /*       #endregion
*/
        #region Category
        public List<XElement> GetCategoriesXML()
        {
            return (from ent in dbo.Element("CATEGORIES").Elements("CATEGORY")
                    select ent).ToList();
        }

        public string[] GetCategoryNames()
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

            return true;
        }
        /*
        #endregion

        #region BusinessObjective
        public string[] GetObjectiveNames()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent.NAME.TrimEnd()).ToArray();
        }
        */
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

            return true;
        }
        /*
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
        */
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

            return true;
        }
        /*
        #endregion

        #region General
        */
        public bool SaveChanges()
        {
            try
            {
                dbo.Save("Data.xml");
            }
            catch
            {
                return false;
            }

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
        }/*

        public void UpdateDataFile()
        {
            XElement root = new XElement("root");

            List<CLIENT> clientList = GetClients();
            XElement clientElement = new XElement("CLIENTS");
            foreach (CLIENT client in clientList)
            {
                XElement temp = new XElement("CLIENT");
                temp.Add(new XElement("NAME", client.NAME.TrimEnd().Replace(' ', '_')));
                temp.Add(new XElement("CLIENTID", client.CLIENTID));
                clientElement.Add(temp);
            }
            root.Add(clientElement);

            List<GROUP> grpList = GetGroups();
            XElement grpElement = new XElement("GROUPS");
            foreach (GROUP grp in grpList)
            {
                XElement temp = new XElement("GROUP");
                temp.Add(new XElement("NAME", grp.NAME.TrimEnd().Replace(' ', '_')));
                temp.Add(new XElement("GROUPID", grp.GROUPID));
                grpElement.Add(temp);
            }
            root.Add(grpElement);

            List<CATEGORY> catList = GetCategories();
            XElement catElement = new XElement("CATEGORYS");
            foreach (CATEGORY category in catList)
            {
                XElement temp = new XElement("CATEGORY");
                temp.Add(new XElement("NAME", category.NAME.TrimEnd().Replace(' ', '_')));
                temp.Add(new XElement("CATEGORYID", category.CATEGORYID));
                catElement.Add(temp);
            }
            root.Add(catElement);

            List<BUSINESSOBJECTIVE> busList = GetObjectives();
            XElement busElement = new XElement("BUSINESSOBJECTIVES");
            foreach (BUSINESSOBJECTIVE objective in busList)
            {
                XElement temp = new XElement("BUSINESSOBJECTIVE");
                temp.Add(new XElement("NAME", objective.NAME.TrimEnd().Replace(' ', '_')));
                temp.Add(new XElement("BUSINESSOBJECTIVEID", objective.BUSINESSOBJECTIVEID));
                busElement.Add(temp);
            }
            root.Add(busElement);

            List<INITIATIVE> iniList = GetInitiatives();
            XElement iniElement = new XElement("INITIATIVES");
            foreach (INITIATIVE initiative in iniList)
            {
                XElement temp = new XElement("INITIATIVE");
                temp.Add(new XElement("NAME", initiative.NAME.TrimEnd().Replace(' ', '_')));
                temp.Add(new XElement("INITIATIVEID", initiative.INITIATIVEID));
                iniElement.Add(temp);
            }
            root.Add(iniElement);

            List<BOM> bomList = GetBOMs();
            XElement bomElement = new XElement("BOMS");
            foreach (BOM bom in bomList)
            {
                XElement temp = new XElement("BOM");
                temp.Add(new XElement("BOMID", bom.BOMID));
                temp.Add(new XElement("CLIENT", bom.CLIENTReference.Value != null ? bom.CLIENT.CLIENTID : -1));
                temp.Add(new XElement("GROUP", bom.GROUPReference.Value != null ? bom.GROUP.GROUPID : -1));
                temp.Add(new XElement("INITIATIVE", bom.INITIATIVE.INITIATIVEID));
                temp.Add(new XElement("EFFECTIVENESS", bom.EFFECTIVENESS != null ? bom.EFFECTIVENESS : -1));
                temp.Add(new XElement("CRITICALITY", bom.CRITICALITY != null ? bom.CRITICALITY : -1));
                temp.Add(new XElement("DIFFERENTIAL", bom.DIFFERENTIAL != null ? bom.DIFFERENTIAL : -1));
                bomElement.Add(temp);
            }
            root.Add(bomElement);



            root.Save("Data.xml");
        }*/
        #endregion
    }
}

