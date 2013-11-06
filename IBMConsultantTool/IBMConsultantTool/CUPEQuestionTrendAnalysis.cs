using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class CUPEQuestionTrendAnalysis : TrendAnalysisEntity
    {
        //four answers

        private string cupeType;

        public string CupeType
        {
            get { return cupeType; }
            set { cupeType = value; }
        }
        private float currentAnswer = 0;

        public float CurrentAnswer
        {
            get { return currentAnswer; }
            set { currentAnswer = value; }
        }
        private float futureAnswer = 0;

        public float FutureAnswer
        {
            get { return futureAnswer; }
            set { futureAnswer = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}
