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
        public abstract Client AddClient(Client client);
        public abstract Client LoadClient(string clientName);
        public abstract List<string> GetObjectivesFromClientBOM(object clientObj);
        public abstract void ClientCompletedBOM(object clientObj);
        public abstract void ClientCompletedCUPE(object clientObj);
        public abstract void ClientCompletedITCAP(object clientObj);
        
        #endregion

        #region Region
        public abstract List<string> GetRegionNames();
        public abstract bool AddRegion(string regName);
        #endregion

        #region Country
        public abstract List<string> GetCountryNames(string regionName = "N/A");
        public abstract bool AddCountry(string countryName, string regionName);
        #endregion

        #region BusinessType
        public abstract List<string> GetBusinessTypeNames();
        public abstract bool AddBusinessType(string busTypeName);
        #endregion

        #region Group
        //group is a keyword in C#
        #endregion

        #region Contact
        public abstract void LoadParticipants();
        #endregion

        #region BOM
        public abstract bool UpdateBOM(object clientObj, NewImperative ini);

        public abstract bool AddBOM(object bom, object client);

        public abstract void BuildBOMForm(BOMTool bomForm);

        public abstract bool RemoveBOM(string bomName, object client);

        public abstract List<string> GetObjectivesFromCurrentClientBOM();
        public abstract List<string> GetImperativesFromCurrentClientBOM();
        #endregion

        #region ITCAP
        public abstract bool UpdateITCAP(object clientObj, ITCapQuestion itcapQuestion);

        public abstract bool LoadITCAP(ref ITCapQuestion question);

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

        #region Imperative
        public abstract bool AddImperativeToBOM(string iniName, string busName, string catName, BOMTool bomForm);
        #endregion

        #region CUPEQuestion
        public abstract List<CupeQuestionData> GetCUPEQuestionData();
        public abstract List<CupeQuestionStringData> GetCUPEQuestionStringData();
        public abstract List<CupeQuestionStringData> GetCUPEQuestionStringDataTwenty();
        public abstract List<CupeQuestionStringData> GetCUPEQuestionStringDataFifteen();
        public abstract List<CupeQuestionStringData> GetCUPEQuestionStringDataTen();
        public abstract bool AddCupeQuestion(CupeQuestionStringData cupeQuestion);
        public abstract bool UpdateCupeQuestion(string cupeQuestion, bool inTwenty, bool inFifteen, bool inTen);
        #endregion

        #region CUPE
        public abstract string UpdateCUPE(CupeQuestionStringData cupeQuestion);

        public abstract bool AddCUPE(string question, object clientObj);
        public abstract bool AddCUPEToGroup(string question, object groupObj);
        public abstract bool AddCUPEToContact(string question, object contactObj);

        public abstract void PopulateCUPEQuestionsForClient(CUPETool cupeForm);
        public abstract List<CupeQuestionStringData> GetCUPESForClient();

        public abstract void SaveCUPEParticipants();
        public abstract void ClearCUPE(object clientObj);
        #endregion

        #region CUPEResponse
        #endregion

        #region ScoringEntity
        public abstract string GetScoringEntityID(string entName);
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

        #region CapabilityGapInfo
        public abstract void SaveCapabilityGapInfo(Capability capability);
        #endregion

        #region ITCAPQuestion
        public abstract string[] GetITCAPQuestionNames(string capName, string domName);
        public abstract string[] GetITCAPQuestionNamesAndDefault(string capName, string domName);
        public abstract string[] GetDefaultITCAPQuestionNames(string capName, string domName);
        public abstract void RemoveQuestionToITCAP(string itcqName);
       // public abstract bool RemoveITCAP(object itcqObject, object clientObj);

        public abstract void AddQuestionToITCAP(string itcqName, string capName, string domName, ITCapTool itcapForm, out int alreadyExists, out string owner);
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