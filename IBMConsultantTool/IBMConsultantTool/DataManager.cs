using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IBMConsultantTool
{
    public abstract class DataManager
    {
        public DataManager()
        {
        }

        #region Client
        public abstract List<CLIENT> GetClients();

        public abstract string[] GetClientNames();

        public abstract bool GetClient(string cntName, out CLIENT client);

        public abstract bool GetClient(int cntID, out CLIENT client);

        public abstract bool AddClient(CLIENT client);

        public abstract bool GetClient(string cntName, out XElement client);

        public abstract bool GetClient(int cntID, out XElement client);

        public abstract bool AddClient(XElement client);
        #endregion

        #region Group
        //group is a keyword in C#
        public abstract List<GROUP> GetGroups();
        public abstract bool GetGroup(string grpName, CLIENT client, out GROUP grp);
        public abstract bool AddGroup(string grpName, CLIENT client);

        public abstract bool GetGroup(string grpName, XElement client, out XElement grp);
        public abstract bool AddGroup(string grpName, XElement client);
        #endregion

        #region BOM
        public abstract List<BOM> GetBOMs();

        public abstract bool UpdateBOM(CLIENT client, NewInitiative ini);

        public abstract bool AddBOM(BOM bom);
        #endregion

        #region Category
        public abstract List<CATEGORY> GetCategories();
        public abstract List<XElement> GetCategoriesXML();

        public abstract string[] GetCategoryNames();

        public abstract bool GetCategory(int catID, out CATEGORY category);

        public abstract bool GetCategory(string catName, out CATEGORY category);

        public abstract bool AddCategory(CATEGORY category);

        public abstract bool GetCategory(int catID, out XElement category);

        public abstract bool GetCategory(string catName, out XElement category);

        public abstract bool AddCategory(XElement category);

        #endregion

        #region BusinessObjective
        public abstract List<BUSINESSOBJECTIVE> GetObjectives();

        public abstract string[] GetObjectiveNames();

        public abstract bool GetObjective(int busID, out BUSINESSOBJECTIVE objective);

        public abstract bool GetObjective(string busName, out BUSINESSOBJECTIVE objective);

        public abstract bool AddObjective(BUSINESSOBJECTIVE objective);
        #endregion

        #region Initiative
        public abstract List<INITIATIVE> GetInitiatives();

        public abstract string[] GetInitiativeNames();

        public abstract bool GetInitiative(string iniName, out INITIATIVE Initiative);

        public abstract bool GetInitiative(int iniID, out INITIATIVE Initiative);

        public abstract bool AddInitiative(INITIATIVE initiative);
        #endregion

        #region General
        public abstract bool SaveChanges();
        #endregion
    }
}
