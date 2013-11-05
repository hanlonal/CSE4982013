using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class CapabilityTrendAnalysis : TrendAnalysisEntity
    {
        string gapType = "";

        public string GapType
        {
            get { return gapType; }
            set { gapType = value; }
        }

        string prioritizedGapType = "";

        public string PrioritizedGapType
        {
            get { return prioritizedGapType; }
            set { prioritizedGapType = value; }
        }

        float capabilityGap = 0;

        public float CapabilityGap
        {
            get { return capabilityGap; }
            set { capabilityGap = value; }
        }

        float prioritizedCapabilityGap = 0;

        public float PrioritizedCapabilityGap
        {
            get { return prioritizedCapabilityGap; }
            set { prioritizedCapabilityGap = value; }
        }

    }
}
