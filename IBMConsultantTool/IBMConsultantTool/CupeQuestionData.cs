using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class CupeQuestionData
    {
        string questionText ="";
        string currentValue = "";
        string futureValue = "";
        int questionID;
        Person owner;
        int ownerId;



        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                questionText = value;
            }
        }
        public string CurrentValue
        {
            get
            {
                return currentValue;
            }
            set         
            {
                currentValue = value;
            }
        }
        public string FutureValue
        {
            get
            {
                return futureValue;
            }
            set
            {
                futureValue = value;    
            }
        }

        public int ID
        {
            get
            {
                return questionID;
            }
            set
            {
                questionID = value;
            }
        }

        public int OwnerId
        {
            get
            {
                return ownerId;
            }
            set
            {
                ownerId = value;
            }
        }
    }



}
