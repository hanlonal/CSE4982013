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
        public abstract List<string> GetObjectivesFromClientBOM(object clientObj);
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
        public abstract bool UpdateITCAP(object clientObj, ITCapQuestion itcapQuestion);

        public abstract bool BuildITCAPForm(ITCapTool itcapForm, string clientName);
        public abstract bool NewITCAPForm(ITCapTool itcapForm, string clientName);
        public abstract bool OpenITCAP(ITCapTool itcapForm);
        public abstract bool AddITCAP(object itcap, object client);
        public abstract bool AddITCAPToGroup(object itcap, object grp);
        public abstract bool AddITCAPToContact(object itcap, object contact);
        public abstract bool RemoveITCAP(string name, object client);

        public abstract bool RewriteITCAP(ITCapTool itcapForm);
        #endregion

        #region Category
        public abstract string[] GetCategoryNames();
        public abstract void ChangedCategory(BOMTool bomForm);
        #endregion

        #region BusinessObjective
        public abstract void ChangedObjective(BOMTool bomForm);
        #endregion

        #region Initiative
        public abstract bool AddInitiativeToBOM(string iniName, string busName, string catName, BOMTool bomForm);
        #endregion

        #region CUPEQuestion
        public abstract List<CupeQuestion> GetCUPEQuestions();
        public abstract bool AddCupeQuestion(CupeQuestion cupeQuestion);
        #endregion

        #region CUPE
        public abstract bool UpdateCUPE(object clientObj, CupeQuestion cq);

        public abstract bool AddCUPE(object cupeObj, object clientObj);
        public abstract bool AddCUPEToGroup(object cupeObj, object groupObj);
        public abstract bool AddCUPEToContact(object cupeObj, object contactObj);

        public abstract bool BuildCUPEForm(CUPETool cupeForm, string clientName);
        public abstract bool NewCUPEForm(CUPETool cupeForm, string clientName);

        public abstract void PopulateCUPEQuestions(CUPETool cupeForm);
        //public abstract void EditQuestionForCUPE(CLIENT client, CupeQuestionStringData data);
        #endregion

        #region Domain
        public abstract string[] GetDomainNames();
        public abstract string[] GetDomainNamesAndDefault();
        public abstract string[] GetDefaultDomainNames();

        public abstract void ChangedDomain(ITCapTool itcapForm);
        public abstract bool ChangeDomainDefault(string domName, bool isDefault);
        #endregion

        #region Capability
        public abstract string[] GetCapabilityNames(string domName);
        public abstract string[] GetCapabilityNamesAndDefault(string domName);
        public abstract string[] GetDefaultCapabilityNames(string domName);

        public abstract void ChangedCapability(ITCapTool itcapForm);
        public abstract bool ChangeCapabilityDefault(string capName, bool isDefault);

        #endregion

        #region ITCAPQuestion
        public abstract string[] GetITCAPQuestionNames(string capName, string domName);
        public abstract string[] GetITCAPQuestionNamesAndDefault(string capName, string domName);
        public abstract string[] GetDefaultITCAPQuestionNames(string capName, string domName);
        public abstract void RemoveQuestionToITCAP(string itcqName);
       // public abstract bool RemoveITCAP(object itcqObject, object clientObj);

        public abstract void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm);
        public abstract bool ChangeITCAPQuestionDefault(string itcq, bool isDefault);
        #endregion

        #region ITCAPOBJMAP
        public abstract bool GetITCAPOBJMAPScore(object clientObj, string capName, string busName, out int score);
        public abstract bool AddITCAPOBJMAP(object clientObj, string capName, string busName);
        public abstract bool UpdateITCAPOBJMAPScore(object clientObj, string capName, string busName, int score);
        #endregion

        #region General
        public abstract bool SaveChanges();
        #endregion
    }
}