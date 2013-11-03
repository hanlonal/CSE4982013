using System;
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
        private int numAnswers = 0;

        bool highStandardDeviation = false;

        private Dictionary<int, int> answers = new Dictionary<int, int>();
        private static float standardDeviationThreshold = 1.00f;





        public ITCapQuestion()
        {
            Console.WriteLine("question created");
            comment = new List<string>();
            answers.Add(1, 0);
            answers.Add(2, 0);
            answers.Add(3, 0);
            answers.Add(4, 0);
            answers.Add(5, 0);
            answers.Add(0, 0);
        }

        public override void UpdateIndexDecrease(int index)
        {

        }
        public void AddAsIsAnswer(float num)
        {
            asIsAnswers.Add(num);
            asIsScore = asIsAnswers.Average();
            numAnswers++;
            if (num == 0)
                NumZeros++;
            if (num == 1)
            {
                NumOnes++;
            }
            else if (num == 2)
                NumTwos++;
            else if (num == 3)
                NumThrees++;
            else if (num == 4)
                NumFours++;
            else if (num == 5)
                NumFives++;

            StandardAsIsDeviation();
            asIsScore = (float)(((1 * numOnes) + (2 * numTwos) + (3 * numThrees) + (4 * numFours) + (5 * numFives)) / (float)numAnswers);
           
                
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
            if (numAnswers > 0)
            {
                float sum = 0;
                for (int i = 0; i < numOnes; i++)
                {
                    sum += (float)Math.Pow(1 - asIsScore, 2);
                }
                for (int i = 0; i < numTwos; i++)
                {
                    sum += (float)Math.Pow(2 - asIsScore, 2);
                }
                for (int i = 0; i < numThrees; i++)
                {
                    sum += (float)Math.Pow(3 - asIsScore, 2);
                }
                for (int i = 0; i < numFours; i++)
                {
                    sum += (float)Math.Pow(4 - asIsScore, 2);
                }
                for (int i = 0; i < numFives; i++)
                {
                    sum += (float)Math.Pow(5 - asIsScore, 2);
                }

                dev = (float)Math.Sqrt((sum) / (numAnswers - 1));
                asisStandardDeviation = dev;
                owner.CalculateAsIsAverage();
                if (asisStandardDeviation > standardDeviationThreshold)
                {
                    owner.Flagged = true;
                    flagged = true;
                    highStandardDeviation = true;
                }
                else
                {
                    flagged = false;
                    highStandardDeviation = false;
                }
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
            if (tobeStandardDeviation > standardDeviationThreshold)
            {
                //owner.Flagged = true;
                //Flagged = true;
            }

        }


        public override float CalculateAsIsAverage()
        {
            numAnswers = numOnes + numTwos + numThrees + numFours + numFives + numZeros;
            asIsScore = (float)(((1 * numOnes) + (2 * numTwos) + (3 * numThrees) + (4 * numFours) + (5 * numFives)) / (float)numAnswers);

            StandardAsIsDeviation();
            return asIsScore;
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
        public bool HighStandardDeviation
        {
            get { return highStandardDeviation; }
            set { highStandardDeviation = value; }
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
