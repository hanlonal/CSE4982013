using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class CupeQuestionData
    {
        CupeQuestionStringData stringData = new CupeQuestionStringData();
        bool inDefault20 = false;
        bool inDefault15 = false;
        bool inDefault10 = false;
        string currentValue = "";
        string futureValue = "";
        int questionID;
        int ownerId;



        public CupeQuestionStringData StringData
        {
            get
            {
                return stringData;
            }
            set
            {
                stringData = value;
            }
        }
        public bool InDefault20
        {
            get
            {
                return inDefault20;
            }
            set
            {
                inDefault20 = value;
            }
        }
        public bool InDefault15
        {
            get
            {
                return inDefault15;
            }
            set
            {
                inDefault15 = value;
            }
        }
        public bool InDefault10
        {
            get
            {
                return inDefault10;
            }
            set
            {
                inDefault10 = value;
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
