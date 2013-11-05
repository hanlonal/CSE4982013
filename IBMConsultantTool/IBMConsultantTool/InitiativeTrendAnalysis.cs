using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class InitiativeTrendAnalysis : TrendAnalysisEntity
    {
        private float effectiveness;

        public float Effectiveness
        {
            get { return effectiveness; }
            set { effectiveness = value; }
        }

        private float criticality;

        public float Criticality
        {
            get { return criticality; }
            set { criticality = value; }
        }

        private float differentiation;

        public float Differentiation
        {
            get { return differentiation; }
            set { differentiation = value; }
        }


    }
}
