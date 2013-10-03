using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    class DBManager
    {
        public SAMPLEEntities dbo;

        public DBManager()
        {
            dbo = new SAMPLEEntities();
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
        #endregion
    }
}
