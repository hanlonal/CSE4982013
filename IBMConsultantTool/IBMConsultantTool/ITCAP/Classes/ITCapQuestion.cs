using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITCapQuestion : ScoringEntity
    {
        private Capability owner;

        public List<string> comment;

        /*private List<float> AsIsanswersToAttributes = new List<float>();
        private List<float> ToBeanswersToAttributes = new List<float>();*/
        public List<float> asIsAnswers = new List<float>();
        public List<float> toBeAnswers = new List<float>();
        private int numasIsAnswers = 0;
        private int numtoBeAnswers = 0;

        bool asishighStandardDeviation = false;
        bool tobehighStandardDeviation = false;

        

        private Dictionary<int, int> answers = new Dictionary<int, int>();
        

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
            capabilityGap = -1;
            capabilityGapText = "";
        }

        public override void UpdateIndexDecrease(int index)
        {

        }

        public void AddAsIsAnswer(float num)
        {
            asIsAnswers.Add(num);
            asIsScore = asIsAnswers.Average();
            numasIsAnswers++;
            if (num == 0)
                AsIsNumZeros++;
            if (num == 1)            
               AsIsNumOnes++;            
            else if (num == 2)
                AsIsNumTwos++;
            else if (num == 3)
                AsIsNumThrees++;
            else if (num == 4)
                AsIsNumFours++;
            else if (num == 5)
                AsIsNumFives++;

            StandardAsIsDeviation();
            asIsScore = (float)(((1 * asisnumOnes) + (2 * asisnumTwos) + (3 * asisnumThrees) + (4 * asisnumFours) + (5 * asisnumFives)) / (float)numasIsAnswers);

            decimal asIs = Convert.ToDecimal(asIsScore);
            asIs = Math.Round(asIs, 2);
            asIsScore = (float)asIs;
                
            owner.CalculateAsIsAverage();
        }
        public void AddToBeAnswer(float num)
        {
            toBeAnswers.Add(num);
            toBeScore = toBeAnswers.Average();
            numtoBeAnswers++;

            if (num == 0)
                TobeNumZeros++;
            if (num == 1)
                TobeNumOnes++;            
            else if (num == 2)
                TobeNumTwos++;
            else if (num == 3)
                TobeNumThrees++;
            else if (num == 4)
                TobeNumFours++;
            else if (num == 5)
                TobeNumFives++;

            StandardToBeDeviation();
            toBeScore = (float)(((1 * tobeNumOnes) + (2 * tobeNumTwos) + (3 * tobeNumThrees) + (4 * tobeNumFours) + (5 * tobeNumFives)) / (float)numtoBeAnswers);
            owner.CalculateToBeAverage();

            decimal toBe = Convert.ToDecimal(toBeScore);
            toBe = Math.Round(toBe, 2);
            toBeScore = (float)toBe;

            owner.CalculateToBeAverage();
        }

        private void StandardAsIsDeviation()
        {
            float dev = 0;            
            if (numasIsAnswers > 0)
            {
                float sum = 0;
                for (int i = 0; i < asisnumOnes; i++)
                {
                    sum += (float)Math.Pow(1 - asIsScore, 2);
                }
                for (int i = 0; i < asisnumTwos; i++)
                {
                    sum += (float)Math.Pow(2 - asIsScore, 2);
                }
                for (int i = 0; i < asisnumThrees; i++)
                {
                    sum += (float)Math.Pow(3 - asIsScore, 2);
                }
                for (int i = 0; i < asisnumFours; i++)
                {
                    sum += (float)Math.Pow(4 - asIsScore, 2);
                }
                for (int i = 0; i < asisnumFives; i++)
                {
                    sum += (float)Math.Pow(5 - asIsScore, 2);
                }

                dev = (float)Math.Sqrt((sum) / (numasIsAnswers - 1));
                asisStandardDeviation = dev;

                // Make two decimals
                if (sum > 0)
                {
                    decimal asIsdev = Convert.ToDecimal(asisStandardDeviation);
                    asIsdev = Math.Round(asIsdev, 2);
                    asisStandardDeviation = (float)asIsdev;
                }

                owner.CalculateAsIsAverage();
                if (asisStandardDeviation > ConfigurationSettings.Instance.ITCapstdDevThreshold1)
                {
                    owner.IsFlagged = true;
                    flagged = true;
                    asishighStandardDeviation = true;
                }
                else
                {
                    if(!tobehighStandardDeviation)
                         flagged = false;
                    owner.CheckFlags();
                    asishighStandardDeviation = false;
                }
            }


        }

        private void StandardToBeDeviation()
        {
            float dev = 0;            
            if (numtoBeAnswers > 0)
            {
                float sum = 0;
                for (int i = 0; i < tobeNumOnes; i++)
                {
                    sum += (float)Math.Pow(1 - toBeScore, 2);
                }
                for (int i = 0; i < tobeNumTwos; i++)
                {
                    sum += (float)Math.Pow(2 - toBeScore, 2);
                }
                for (int i = 0; i < tobeNumThrees; i++)
                {
                    sum += (float)Math.Pow(3 - toBeScore, 2);
                }
                for (int i = 0; i < tobeNumFours; i++)
                {
                    sum += (float)Math.Pow(4 - toBeScore, 2);
                }
                for (int i = 0; i < tobeNumFives; i++)
                {
                    sum += (float)Math.Pow(5 - toBeScore, 2);
                }
                dev = (float)Math.Sqrt((sum) / (numtoBeAnswers - 1));
                tobeStandardDeviation = dev;

            // Make two decimals
            if (sum > 0)
            {
                decimal toBedev = Convert.ToDecimal(tobeStandardDeviation);
                toBedev = Math.Round(toBedev, 2);
                tobeStandardDeviation = (float)toBedev;
            }
                owner.CalculateToBeAverage();

                if (tobeStandardDeviation > ConfigurationSettings.Instance.ITCapstdDevThreshold1)
                {
                    owner.IsFlagged = true;                    
                    flagged = true;
                    tobehighStandardDeviation = true;
                }
                else
                {
                    if(!asishighStandardDeviation)
                        flagged = false;
                    owner.CheckFlags();
                    tobehighStandardDeviation = false;
                }
            }
            

        }


        public override float CalculateAsIsAverage()
        {
            numasIsAnswers = asisnumOnes + asisnumTwos + asisnumThrees + asisnumFours + asisnumFives + asisnumZeros;
            asIsScore = (float)(((1 * asisnumOnes) + (2 * asisnumTwos) + (3 * asisnumThrees) + (4 * asisnumFours) + (5 * asisnumFives)) / (float)numasIsAnswers);
            asIsAnswers.Clear();
            owner.GetNumberOfAsIsAnswers();
            for (int i = 0; i < asisnumOnes; i++)
            {
                asIsAnswers.Add(1);
            }
            for (int i = 0; i < asisnumTwos; i++)
            {
                asIsAnswers.Add(2);
            }
            for (int i = 0; i < asisnumThrees; i++)
            {
                asIsAnswers.Add(3);
            }
            for (int i = 0; i < asisnumFours; i++)
            {
                asIsAnswers.Add(4);
            }
            for (int i = 0; i < asisnumFives; i++)
            {
                asIsAnswers.Add(5);
            }

            StandardAsIsDeviation();

            decimal asIs = Convert.ToDecimal(!float.IsNaN(asIsScore) ? asIsScore : 0);
            asIs = Math.Round(asIs, 2);
            asIsScore = (float)asIs;

            return asIsScore;
        }

        public override float CalculateToBeAverage()
        {
            numtoBeAnswers = tobeNumOnes + tobeNumTwos + tobeNumThrees + tobeNumFours + tobeNumFives + tobeNumZeros;
            toBeScore = (float)(((1 * tobeNumOnes) + (2 * tobeNumTwos) + (3 * tobeNumThrees) + (4 * tobeNumFours) + (5 * tobeNumFives)) / (float)numtoBeAnswers);
            owner.GetNumberOfToBeAnswers();
            for (int i = 0; i < tobeNumOnes; i++)
            {
                toBeAnswers.Add(1);
            }
            for (int i = 0; i < tobeNumTwos; i++)
            {
                toBeAnswers.Add(2);
            }
            for (int i = 0; i < tobeNumThrees; i++)
            {
                toBeAnswers.Add(3);
            }
            for (int i = 0; i < tobeNumFours; i++)
            {
                toBeAnswers.Add(4);
            }
            for (int i = 0; i < tobeNumFives; i++)
            {
                toBeAnswers.Add(5);
            }

            StandardToBeDeviation();

            decimal toBe = Convert.ToDecimal(!float.IsNaN(toBeScore) ? toBeScore : 0);
            toBe = Math.Round(toBe, 2);
            toBeScore = (float)toBe;

            return toBeScore;
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
            else if (capabilityGap < 1 )
            {
                CapabilityGapText = "Low Gap";
                GapType1 = GapType.Low;
            }
            if (capabilityGap == 0)
            {
                GapType1 = GapType.None;
                capabilityGapText = "Not Focus";
            }
        }
        public bool AsIsHighStandardDeviation
        {
            get { return asishighStandardDeviation; }
            set { asishighStandardDeviation = value; }
        }

        public bool TobehighStandardDeviation
        {
            get { return tobehighStandardDeviation; }
            set { tobehighStandardDeviation = value; }
        }
        public Capability Owner
        {
            get { return owner; }
            set { owner = value; }
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
