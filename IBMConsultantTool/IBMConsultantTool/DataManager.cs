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

        #region Contact
        #endregion

        #region BOM
        public abstract bool UpdateBOM(object clientObj, NewInitiative ini);

        public abstract bool AddBOM(object bom, object client);
        public abstract bool AddBOMToGroup(object bom, object group);
        public abstract bool AddBOMToContact(object bom, object contact);

        public abstract bool BuildBOMForm(BOMTool bomForm, string clientName);
        public abstract bool NewBOMForm(BOMTool bomForm, string clientName);
        #endregion

        #region ITCAP
        public abstract bool BuildITCAPForm(ITCapTool itcapForm, string clientName);
        public abstract bool NewITCAPForm(ITCapTool itcapForm, string clientName);
        public abstract bool OpenITCAP(ITCapTool itcapForm);
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

        #region Domain
        public abstract string[] GetDomainNames();
        public abstract string[] GetDomainNamesAndDefault();
        #endregion

        #region Capability
        public abstract string[] GetCapabilityNames(string domName);
        public abstract string[] GetCapabilityNamesAndDefault(string domName);
        #endregion

        #region ITCAPQuestion
        public abstract string[] GetITCAPQuestionNames(string capName, string domName);
        public abstract string[] GetITCAPQuestionNamesAndDefault(string capName, string domName);
        #endregion

        #region General
        public abstract bool SaveChanges();
        #endregion
    }
}