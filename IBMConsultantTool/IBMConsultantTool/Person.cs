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



        public Person()
        {
           // PopulateQuestionData();
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
    }// end class
}
