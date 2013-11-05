﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITAttributeTrendAnalysis : TrendAnalysisEntity
    {
        private float tobeScore;

        public float TobeScore
        {
            get { return tobeScore; }
            set { tobeScore = value; }
        }
        private float asisScore;

        public float AsisScore
        {
            get { return asisScore; }
            set { asisScore = value; }
        }

    }
}