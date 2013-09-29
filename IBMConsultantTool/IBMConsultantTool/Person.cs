using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    class Person
    {
        public enum EmployeeType 
        { 
            IT, 
            Business 
        }

       // LinkedList<CupeQuestion> questions = new LinkedList<CupeQuestion>();

        private List<CupeQuestionData> questionData = new List<CupeQuestionData>();

        EmployeeType type = new EmployeeType();
        string name;
        CupeForm owner;
        string clientName;
        int totalCommodity = 0;
        int totalPartner = 0;
        int totalEnabler = 0;
        int totalUtility = 0;
        int totalFutureCommodity = 0;
        int totalFuturePartner = 0;
        int totalFutureUtility = 0;
        int totalFutureEnabler = 0;


        public Person()
        {
           // PopulateQuestionData();
        }
        public void ClearCurrentValues()
        {
            totalCommodity = 0;
            totalPartner = 0;
            totalEnabler = 0;
            totalUtility = 0;
        }

        public void ClearFutureValues()
        {
            totalFutureCommodity = 0;
            totalFuturePartner = 0;
            totalFutureUtility = 0;
            totalFutureEnabler = 0;
        }

        public CupeForm Owner
        {
            set
            {
                owner = value;
            }
        }
        public void PopulateQuestionData()
        {
            foreach (CupeQuestion question in owner.Questions)
            {
                CupeQuestionData data = new CupeQuestionData();
                data.CurrentValue = "";
                data.FutureValue = "";
                data.ID = question.ID;
                questionData.Add(data);

            }
        }

        public override string ToString()
        {
            return name;
        }

        public List<CupeQuestionData> Questions
        {
            get
            {
                return questionData;
            }
        }

        public List<CupeQuestionData> Data
        {
            get
            {
                return questionData;
            }
            
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        #region GET TOTAL VALUE PROPERTIES


        public int TotalPartner
        {
            get
            {
                return totalPartner;
            }
            set
            {
                totalPartner = value;
            }
        }
        public int TotalEnabler
        {
            get
            {
                return totalEnabler;
            }
            set
            {
                totalEnabler = value;
            }
        }
        public int TotalCommodity
        {
            get
            {
                return totalCommodity;
            }
            set
            {
                totalCommodity = value;
            }
        }
        public int TotalUtility
        {
            get
            {
                return totalUtility;
            }
            set
            {
                totalUtility = value;
            }
        }

        public int TotalFuturePartner
        {
            get
            {
                return totalFuturePartner;
            }
            set
            {
                totalFuturePartner = value;
            }
        }
        public int TotalFutureCommodity
        {
            get
            {
                return totalFutureCommodity;
            }
            set
            {
                totalFutureCommodity = value;
            }
        }
        public int TotalFutureUtility
        {
            get
            {
                return totalFutureUtility;
            }
            set
            {
                totalFutureUtility = value;
            }
        }
        public int TotalFutureEnabler
        {
            get
            {
                return totalFutureEnabler;
            }
            set
            {
                totalFutureEnabler = value;
            }
        }


        #endregion


    }// end class
}
