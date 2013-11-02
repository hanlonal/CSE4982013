﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITCapQuestion : ScoringEntity
    {
        private Capability owner;

        public List<string> comments = new List<string>();

        public List<string> comment;

        private List<float> AsIsanswersToAttributes = new List<float>();
        private List<float> ToBeanswersToAttributes = new List<float>();
        private List<float> asIsAnswers = new List<float>();
        private List<float> toBeAnswers = new List<float>();








        public ITCapQuestion()
        {
            Console.WriteLine("question created");
            comment = new List<string>();

        }

        public override void UpdateIndexDecrease(int index)
        {

        }
        public void AddAsIsAnswer(float num)
        {
            asIsAnswers.Add(num);
            asIsScore = asIsAnswers.Average();
            StandardAsIsDeviation();
            owner.CalculateAsIsAverage();
        }
        public void AddToBeAnswer(float num)
        {
            toBeAnswers.Add(num);
            toBeScore = toBeAnswers.Average();
            StandardToBeDeviation();
            owner.CalculateToBeAverage();
        }

        private void StandardAsIsDeviation()
        {
            float dev = 0;
            if (asIsAnswers.Count > 0)
            {
                float sum = (float)asIsAnswers.Sum(d => Math.Pow(d - asIsScore, 2));

                dev = (float)Math.Sqrt((sum) / (asIsAnswers.Count - 1));
            }
            asisStandardDeviation = dev;
            if (asisStandardDeviation > .6f)
            {
                owner.Flagged = true;
                flagged = true;
            }
        }

        private void StandardToBeDeviation()
        {
            float dev = 0;
            if (toBeAnswers.Count > 0)
            {
                float sum = (float)toBeAnswers.Sum(d => Math.Pow(d - toBeScore, 2));

                dev = (float)Math.Sqrt((sum) / (toBeAnswers.Count - 1));
            }
            tobeStandardDeviation = dev;
            if (tobeStandardDeviation > .6f)
            {
                //owner.Flagged = true;
                //Flagged = true;
            }

        }


        public override float CalculateAsIsAverage()
        {
            return 0;
        }
        public override float CalculateToBeAverage()
        {
            return 0;
        }
        public override void CalculateCapabilityGap()
        {
            base.CalculateCapabilityGap();
            if (capabilityGap >= 1.5)
            {
                CapabilityGapText = "High Gap";
                GapType1 = GapType.High;

            }
            else if (capabilityGap < 1.5 && capabilityGap >= 1)
            {
                CapabilityGapText = "Medium Gap";
                GapType1 = GapType.Middle;
            }
            else if (capabilityGap < 1)
            {
                CapabilityGapText = "Low/No Gap";
                GapType1 = GapType.Low;
            }

            if (capabilityGap == 0)
            {

            }
        }

        public Capability Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = owner.ID + "." + (string)value; }
        }

        public void AddComment(string comm)
        {
            if (!String.IsNullOrEmpty(comm))
            {
                comment.Add(comm);
            }
        }
    }
}
