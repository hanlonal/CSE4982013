using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public class DBManager : DataManager
    {
        public SAMPLEEntities dbo;

        public DBManager()
        {
            dbo = new SAMPLEEntities();
            UpdateDataFile();
        }

        #region Client
        public override List<CLIENT> GetClients()
        {
            return (from ent in dbo.CLIENT
                    select ent).ToList();
        }

        public override string[] GetClientNames()
        {
            return (from ent in dbo.CLIENT
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override bool GetClient(string cntName, out CLIENT client)
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

        public override bool GetClient(int cntID, out CLIENT client)
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

        public override bool AddClient(CLIENT client)
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
        public override List<GROUP> GetGroups()
        {
            return (from ent in dbo.GROUP
                    select ent).ToList();
        }

        public override bool GetGroup(string grpName, CLIENT client, out GROUP grp)
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
        public override bool AddGroup(string grpName, CLIENT client)
        {
            //If Client points to 2 BOMs with same Initiative, return false
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
        public override List<BOM> GetBOMs()
        {
            return (from ent in dbo.BOM
                    select ent).ToList();
        }

        public override bool UpdateBOM(CLIENT client, NewInitiative ini)
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

        public override bool AddBOM(BOM bom)
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
        #endregion

        #region Category
        public override List<CATEGORY> GetCategories()
        {
            return (from ent in dbo.CATEGORY
                    select ent).ToList();
        }

        public override string[] GetCategoryNames()
        {
            return (from ent in dbo.CATEGORY
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override bool GetCategory(int catID, out CATEGORY category)
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

        public override bool GetCategory(string catName, out CATEGORY category)
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

        public override bool AddCategory(CATEGORY category)
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
        public override List<BUSINESSOBJECTIVE> GetObjectives()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent).ToList();
        }

        public override string[] GetObjectiveNames()
        {
            return (from ent in dbo.BUSINESSOBJECTIVE
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override bool GetObjective(int busID, out BUSINESSOBJECTIVE objective)
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

        public override bool GetObjective(string busName, out BUSINESSOBJECTIVE objective)
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

        public override bool AddObjective(BUSINESSOBJECTIVE objective)
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
        public override List<INITIATIVE> GetInitiatives()
        {
            return (from ent in dbo.INITIATIVE
                    select ent).ToList();
        }

        public override string[] GetInitiativeNames()
        {
            return (from ent in dbo.INITIATIVE
                    select ent.NAME.TrimEnd()).ToArray();
        }

        public override bool GetInitiative(string iniName, out INITIATIVE Initiative)
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

        public override bool GetInitiative(int iniID, out INITIATIVE Initiative)
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

        public override bool AddInitiative(INITIATIVE initiative)
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


            UpdateXMLFile();

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
        }
        #endregion
    }
}
