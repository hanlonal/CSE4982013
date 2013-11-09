using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    class Person
    {
        public enum EmployeeType 
        { 
            IT, 
            Business 
        }
        EmployeeType type = new EmployeeType();
        

       // LinkedList<CupeQuestion> questions = new LinkedList<CupeQuestion>();

        public List<CupeQuestionData> questionData = new List<CupeQuestionData>();

        public CupeData cupeDataHolder;

        //EmployeeType type = new EmployeeType();
        CupeForm owner;
        string email;
        string clientName;
        int id;


        int totalCommodity = 0;
        int totalPartner = 0;
        int totalEnabler = 0;
        int totalUtility = 0;
        int totalFutureCommodity = 0;
        int totalFuturePartner = 0;
        int totalFutureUtility = 0;
        int totalFutureEnabler = 0;
        
        Color listBoxColor = Color.Red;

        float CUPEFutureScore;
        float CUPECurrentScore;


        public Person(int theID)
        {
            id = theID;
            cupeDataHolder = new CupeData(id);
        }
        public void ClearCurrentValues()
        {
            totalCommodity = 0;
            totalPartner = 0;
            totalEnabler = 0;
            totalUtility = 0;
        }
        public EmployeeType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public Color Color
        {
            get
            {
                if (CUPEFutureScore - CUPECurrentScore < 2)
                    return Color.Red;
                else
                    return Color.White;
            }
        }

        public void CalculateTotalFutureCupeScore()
        {
            float score = 0;
            foreach (CupeQuestionData question in questionData)
            {
                if (question.FutureValue == "a")
                    score += 1;
                if (question.FutureValue == "b")
                    score += 2;
                if (question.FutureValue == "c")
                    score += 3;
                if (question.FutureValue == "d")
                    score += 4;

            }
            CUPEFutureScore = score / owner.Questions.Count;

        }

       public void CalculateTotalCurrentCupeScore()
        {
            float score = 0;
            foreach (CupeQuestionData question in questionData)
            {
                if (question.CurrentValue == "a")
                    score += 1;
                if (question.CurrentValue == "b")
                    score += 2;
                if (question.CurrentValue == "c")
                    score += 3;
                if (question.CurrentValue == "d")
                    score += 4;

            }
            CUPECurrentScore = score / owner.Questions.Count;
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

        public float FutureCUPEScore
        {
            get
            {
                return CUPEFutureScore;
            }
        }
        public float CurrentCUPEScore
        {
            get
            {
                return CUPECurrentScore;
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
            return "Person " + id.ToString();
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

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public int CalculateFutureDataForQuestion(int id)
        {
            int totalFutureIT = 0;
            


                if (Questions[id].FutureValue == "a")
                {
                    totalFutureIT += 1;

                }

                if (Questions[id].FutureValue == "b")
                {
                    totalFutureIT += 2;

                }

                if (Questions[id].FutureValue == "c")
                {
                    totalFutureIT += 3;

                }

                if (Questions[id].FutureValue == "d")
                {
                    totalFutureIT += 4;

                }

                return totalFutureIT;
            
        }

        public int CalculateCurrentDataForQuestion(int id)
        {
            int totalCurrentIT = 0;

            if (Questions[id].CurrentValue == "a")
            {
                totalCurrentIT += 1;

            }
            if (Questions[id].CurrentValue == "b")
            {
                totalCurrentIT += 2;

            }
            if (Questions[id].CurrentValue == "c")
            {
                totalCurrentIT += 3;

            }
            if (Questions[id].CurrentValue == "d")
            {
                totalCurrentIT += 4;

            }

            return totalCurrentIT;
        }

        public string GetAnswerToQuestion(int id)
        {
            //if(Questions[id].CurrentValue == "a")


            return "";
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
