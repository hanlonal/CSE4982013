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
        public abstract string[] GetClientNames();
        #endregion

        #region Group
        //group is a keyword in C#
        #endregion

        #region BOM
        public abstract bool UpdateBOM(object clientObj, NewInitiative ini);

        public abstract bool AddBOM(object bom, object client);

        public abstract bool BuildBOMForm(BOMTool bomForm, string clientName);

        public abstract bool NewBOMForm(BOMTool bomForm, string clientName);
        #endregion

        #region Category

        public abstract string[] GetCategoryNames();
        public abstract void ChangedCategory(BOMTool bomForm);

        #endregion

        #region BusinessObjective

        public abstract void ChangedObjective(BOMTool bomForm);

        #endregion

        #region Initiative
        public abstract void AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm);
        #endregion

        #region General
        public abstract bool SaveChanges();
        #endregion
    }
}
