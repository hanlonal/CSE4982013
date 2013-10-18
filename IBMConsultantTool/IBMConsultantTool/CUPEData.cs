using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    class CupeData
    {
        public Dictionary<string, char> CurrentAnswers;
        public Dictionary<string, char> FutureAnswers;
        public int ParticipantId;


        public CupeData(int Id)
        {
            Dictionary<string, char> CurrentAnswers = new Dictionary<string, char>();
            Dictionary<string, char> FutureAnswers = new Dictionary<string, char>();
            ParticipantId = Id;
        }


    }
}
