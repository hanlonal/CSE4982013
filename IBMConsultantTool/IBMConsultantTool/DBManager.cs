using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public class DBManager
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

        public string[] GetClientNames()
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
                       where ent.INITIATIVE.NAME == iniName
                       select ent).Single();
            }

            catch
            {
                bom = null;
                return false;
            }

            return true;
        }

        public bool UpdateBOM(CLIENT client, NewInitiative ini)
        {
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

        public bool AddBOM(BOM bom)
        {
            //If Client points to 2 BOMs with same Initiative, return false
            if ((from ent in bom.CLIENT.BOM
                 where ent.INITIATIVE.NAME.TrimEnd() == bom.INITIATIVE.NAME.TrimEnd()
                 select ent).Count() != 1)
            {
                return false;
            }

            List<int> idList = (from ent in dbo.BOM
                                select ent.BOMID).ToList();

            bom.BOMID = GetUniqueID(idList);

            dbo.AddToBOM(bom);

            return true;
        }

        public bool BuildBOMForm(BOMTool bomForm, string clientName)
        {
            CLIENT client;

            if (GetClient(clientName, out client))
            {
                bomForm.dbclient = client;

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

        public string[] GetCategoryNames()
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
        #endregion

        #region BusinessObjective
        public List<BUSINESSOBJECTIVE> GetObjectives()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent).ToList();
        }

        public string[] GetObjectiveNames()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent.NAME.TrimEnd()).ToArray();
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
        #endregion

        #region General
        public bool SaveChanges()
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

            root.Save("Data.xml");
        }
        public void CheckChangeLog()
        {
            bool success = true;

            string line;
            string[] lineArray;

            CLIENT client;
            CATEGORY category;
            BUSINESSOBJECTIVE objective;
            INITIATIVE initiative;
            BOM bom;

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
                                client.NAME = lineArray[2];
                                AddClient(client);
                                break;

                            case "GROUP":
                                if (GetClient(lineArray[3], out client))
                                {
                                    AddGroup(lineArray[2], client);
                                }
                                break;

                            case "CATEGORY":
                                category = new CATEGORY();
                                category.NAME = lineArray[2];
                                AddCategory(category);
                                break;

                            case "BUSINESSOBJECTIVE":
                                if (GetCategory(lineArray[3], out category))
                                {
                                    objective = new BUSINESSOBJECTIVE();
                                    objective.NAME = lineArray[2];
                                    objective.CATEGORY = category;
                                    AddObjective(objective);
                                }
                                break;

                            case "INITIATIVE":
                                if (GetObjective(lineArray[3], out objective))
                                {
                                    initiative = new INITIATIVE();
                                    initiative.NAME = lineArray[2];
                                    initiative.BUSINESSOBJECTIVE = objective;
                                    AddInitiative(initiative);
                                }
                                break;

                            case "BOM":
                                if (GetClient(lineArray[2], out client))
                                {
                                    if (GetInitiative(lineArray[3], out initiative))
                                    {
                                        bom = new BOM();
                                        bom.CLIENT = client;
                                        bom.INITIATIVE = initiative;
                                        AddBOM(bom);
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
                                if (GetClient(lineArray[2], out client))
                                {
                                    if (GetBOM(lineArray[3], client, out bom))
                                    {
                                        bom.EFFECTIVENESS = Convert.ToSingle(lineArray[4]);
                                        bom.CRITICALITY = Convert.ToSingle(lineArray[5]);
                                        bom.DIFFERENTIAL = Convert.ToSingle(lineArray[6]);
                                    }
                                }
                                break;
                        }
                    }

                    if (!SaveChanges())
                    {
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
