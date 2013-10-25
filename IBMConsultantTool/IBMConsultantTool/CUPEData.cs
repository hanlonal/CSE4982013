using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    //This is what is actually used. Disregard other CUPE data classes
    class CupeData
    {
        public Dictionary<string, char> CurrentAnswers;
        public Dictionary<string, char> FutureAnswers;
        public int ParticipantId;


        public CupeData(int Id)
        {
            CurrentAnswers = new Dictionary<string, char>();
            FutureAnswers = new Dictionary<string, char>();
            ParticipantId = Id;
        }


    }
}
