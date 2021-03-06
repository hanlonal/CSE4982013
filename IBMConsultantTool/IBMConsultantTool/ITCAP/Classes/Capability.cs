﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class Capability : ScoringEntity
    {
        private List<ITCapQuestion> questionsOwned = new List<ITCapQuestion>();
        private Dictionary<string, int> OBJECTIVESCORES = new Dictionary<string, int>();

        enum SortTpe { Static, Dynamic };
        static SortTpe sortType;

        public float currentScore = 0;
        public float futureScore = 0;

        static private float percentToCategorizeAsHigh = .33f;
        static private float percentToCategorizeAsLow = .33f;
        static private Dictionary<Capability, float> dynamicCapabilityGaps = new Dictionary<Capability, float>();
        static private Dictionary<Capability, float> prioritizedDyanmicCapabilityGaps = new Dictionary<Capability, float>();

        static private List<Capability> allCapabilities = new List<Capability>();

        public static List<Capability> AllCapabilities
        {
            get { return Capability.allCapabilities; }
            set { Capability.allCapabilities = value; }
        }

        ObjectiveValueCollection objectiveCollection = new ObjectiveValueCollection();

        public ObjectiveValueCollection ObjectiveCollection
        {
            get { return objectiveCollection; }
            set { objectiveCollection = value; }
        }

        private Domain owner;

        public Capability()
        {
            sortType = SortTpe.Dynamic;
            PrioritizedGapType1 = PrioritizedGapType.None;

        }

        public override void UpdateIndexDecrease(int index)
        {
            foreach (ITCapQuestion question in questionsOwned)
            {
                if (question.IsInGrid && question.IndexInGrid > index)
                    question.IndexInGrid--;
            }
        }

        public void CheckFlags()
        {
            foreach (ITCapQuestion ques in questionsOwned)
            {
                if (ques.Flagged)
                    return;
            }
            flagged = false;
            owner.CheckFlags();
        }

        public override float CalculateAsIsAverage()
        {
            float total = 0;
            float activeQuestionCount = 0;
            foreach (ITCapQuestion question in questionsOwned)
            {
                if (question.AsIsScore != 0)
                {
                    total += question.AsIsScore;
                    activeQuestionCount++;
                }
            }
            asIsScore = activeQuestionCount == 0 ? 0 : total / activeQuestionCount;
            owner.CalculateAsIsAverage();
            CalculateCapabilityGap();

            decimal asIs = Convert.ToDecimal(asIsScore);
            asIs = Math.Round(asIs, 0);
            asIsScore = (float)asIs;

            currentScore = asIsScore;

            return asIsScore;
        }

        public static void CalculatePrioritizedCapabilityGaps()
        {

            List<Capability> sortingList = new List<Capability>();
            foreach (Capability cap in allCapabilities)
            {
                if (cap.PrioritizedCapabilityGap == 0)
                    continue;
                else
                {
                    sortingList.Add(cap);
                }
            }
            var items = from x in sortingList
                        orderby x.PrioritizedCapabilityGap ascending
                        select x;




            int count = 0;
            if (sortingList.Count > 3)
            {
                int numberForLow = sortingList.Count / 3;
                int numberForMid = (sortingList.Count / 3) + numberForLow;
                int numberForHigh = (sortingList.Count / 3) + numberForMid;
                foreach (Capability cap in items)
                {

                    if (count < numberForLow)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.Low;
                        cap.PrioritizedGap = "Low Gap";
                    }
                    else if (count >= numberForLow && count <= numberForMid)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.Middle;
                        cap.PrioritizedGap = "Medium Gap";
                    }
                    else if (count > numberForMid)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.High;
                        cap.PrioritizedGap = "High Gap";

                    }


                    count++;
                }
            }
            else if (sortingList.Count == 3)
            {
                int count2 = 0;
                foreach (Capability cap in items)
                {
                    if (count2 == 0)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.Low;
                        cap.PrioritizedGap = "Low Gap";

                    }
                    if (count2 == 1)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.Middle;
                        cap.PrioritizedGap = "Medium Gap";
                    }
                    if (count2 == 2)
                    {
                        cap.PrioritizedGapType1 = PrioritizedGapType.High;
                        cap.PrioritizedGap = "High Gap";
                    }
                    
                    count2++;
                }
            }
            else if (sortingList.Count < 3)
            {
                foreach (Capability cap in sortingList)
                {
                    cap.PrioritizedGapType1 = PrioritizedGapType.Middle;
                    cap.PrioritizedGap = "Middle Gap";
                }
            }

        }

        public override void CalculateCapabilityGap()
        {
            base.CalculateCapabilityGap();

            if (dynamicCapabilityGaps.ContainsKey(this))
            {
                if (this.asIsScore == 0.00 && this.toBeScore == 0.00)
                {
                    this.gapType = GapType.None;
                    gapType = GapType.None;
                    capabilityGapText = "Not Focus";
                    dynamicCapabilityGaps.Remove(this);
                    return;
                }
                dynamicCapabilityGaps[this] = capabilityGap;
            }
            else
            {
                if (this.asIsScore == 0.00 && this.toBeScore == 0.00)
                {
                    this.gapType = GapType.None;
                    return;
                }
                dynamicCapabilityGaps.Add(this, capabilityGap);
            }


            if (ConfigurationSettings.Instance.StaticSort || dynamicCapabilityGaps.Count < 3)
            {
                if (capabilityGap >= ConfigurationSettings.Instance.StaticHighGapThreshold)
                {
                    CapabilityGapText = "High Gap";
                    gapType = GapType.High;
                }
                else if (capabilityGap >= ConfigurationSettings.Instance.StaticLowGapThreshold && capabilityGap < ConfigurationSettings.Instance.StaticHighGapThreshold)
                {
                    CapabilityGapText = "Medium Gap";
                    gapType = GapType.Middle;
                }
                else if (capabilityGap < ConfigurationSettings.Instance.StaticLowGapThreshold)
                {
                    CapabilityGapText = "Low/No Gap";
                    gapType = GapType.Low;
                }
                else
                {
                    CapabilityGapText = "Not Focus";
                    gapType = GapType.None;
                }
            }
            else if (!ConfigurationSettings.Instance.StaticSort)
            {
                var items = from pair in dynamicCapabilityGaps
                            orderby pair.Value ascending
                            select pair;

                if (dynamicCapabilityGaps.Count > 3)
                {
                    int numberForLow = (int)(dynamicCapabilityGaps.Count * percentToCategorizeAsLow);
                    int numberForMid = (int)(dynamicCapabilityGaps.Count * (1 - (percentToCategorizeAsHigh + percentToCategorizeAsLow)));
                    int numberForHigh = (int)(dynamicCapabilityGaps.Count * percentToCategorizeAsHigh) + numberForLow + numberForMid;

                    int count = 0;
                    foreach (KeyValuePair<Capability, float> pair in items)
                    {
                        if (count < numberForLow)
                        {
                            if (pair.Key.CapabilityGap >= ConfigurationSettings.Instance.DynamicAutoHighGap)
                            {
                                CapabilityGapText = "High Gap";
                                gapType = GapType.High;
                            }
                            else if (pair.Key.CapabilityGap <= ConfigurationSettings.Instance.DynamicAutoLowGap)
                            {
                                pair.Key.CapabilityGapText = "Low Gap";
                                pair.Key.GapType1 = GapType.Low;
                            }
                            else
                            {
                                pair.Key.CapabilityGapText = "High Gap";
                                pair.Key.gapType = GapType.High;
                            }
                        }

                        else if (count >= numberForLow && count < numberForHigh)
                        {
                            if (pair.Key.CapabilityGap >= ConfigurationSettings.Instance.DynamicAutoHighGap)
                            {
                                CapabilityGapText = "High Gap";
                                gapType = GapType.High;

                            }
                            else if (pair.Key.CapabilityGap <= ConfigurationSettings.Instance.DynamicAutoLowGap)
                            {
                                pair.Key.CapabilityGapText = "Low Gap";
                                pair.Key.GapType1 = GapType.Low;
                            }
                            else
                            {
                                pair.Key.CapabilityGapText = "Middle Gap";
                                pair.Key.gapType = GapType.Middle;
                            }
                        }

                        else if (count >= numberForHigh)
                        {
                            if (pair.Key.CapabilityGap >= ConfigurationSettings.Instance.DynamicAutoHighGap)
                            {
                                CapabilityGapText = "High Gap";
                                gapType = GapType.High;

                            }
                            else if (pair.Key.CapabilityGap <= ConfigurationSettings.Instance.DynamicAutoLowGap)
                            {
                                pair.Key.CapabilityGapText = "Low Gap";
                                pair.Key.GapType1 = GapType.Low;
                            }
                            else
                            {
                                pair.Key.CapabilityGapText = "Low Gap";
                                pair.Key.gapType = GapType.Low;
                            }
                        }
                        if (pair.Value == 0.00)
                        {
                            pair.Key.gapType = GapType.None;
                            pair.Key.CapabilityGapText = "Not Focus";
                        }

                        count++;
                    }
                }
                else if (dynamicCapabilityGaps.Count == 3)
                {
                    int count = 0;
                    foreach (KeyValuePair<Capability, float> pair in items)
                    {
                        if (pair.Key.CapabilityGap >= ConfigurationSettings.Instance.DynamicAutoHighGap)
                        {
                            CapabilityGapText = "High Gap";
                            gapType = GapType.High;
                            count++;
                            continue;
                        }
                        else if (pair.Key.CapabilityGap <= ConfigurationSettings.Instance.DynamicAutoLowGap)
                        {
                            pair.Key.CapabilityGapText = "Low Gap";
                            pair.Key.GapType1 = GapType.Low;
                            count++;
                            continue;
                        }

                        if (count == 0)
                        {
                            pair.Key.CapabilityGapText = "Low Gap";
                            pair.Key.GapType1 = GapType.Low;
                        }
                        else if (count == 1)
                        {
                            pair.Key.CapabilityGapText = "Medium Gap";
                            pair.Key.GapType1 = GapType.Middle;
                        }
                        else if (count == 2)
                        {
                            pair.Key.CapabilityGapText = "High Gap";
                            pair.Key.GapType1 = GapType.High;
                        }

                        count++;
                    }
                }

            }

        }



        public override void ChangeChildrenVisibility()
        {
            foreach (ITCapQuestion ques in questionsOwned)
            {
                ques.Visible = !ques.Visible;
            }
            base.ChangeChildrenVisibility();
        }


        /*   public static DictionaryBindingList<TKey, TValue>
               ToBindingList<TKey, TValue>(IDictionary<TKey, TValue> data)
           {
               return new DictionaryBindingList<TKey, TValue>(data);
           }*/

        public override float CalculateToBeAverage()
        {
            float total = 0;
            float activeQuestionCount = 0;
            foreach (ITCapQuestion question in questionsOwned)
            {
                if (question.ToBeScore != 0)
                {
                    total += question.ToBeScore;
                    activeQuestionCount++;
                }
            }
            toBeScore = activeQuestionCount == 0 ? 0 : total / activeQuestionCount;
            owner.CalculateToBeAverage();
            CalculateCapabilityGap();

            decimal toBe = Convert.ToDecimal(toBeScore);
            toBe = Math.Round(toBe, 2);
            toBeScore = (float)toBe;

            futureScore = toBeScore;

            return toBeScore;
        }

        public void GetNumberOfAsIsAnswers()
        {
            int zeros = 0;
            int ones = 0;
            int twos = 0;
            int threes = 0;
            int fours = 0;
            int fives = 0;
            foreach (ITCapQuestion ques in questionsOwned)
            {
                zeros += ques.AsIsNumZeros;
                ones += ques.AsIsNumOnes;
                twos += ques.AsIsNumTwos;
                threes += ques.AsIsNumThrees;
                fours += ques.AsIsNumFours;
                fives += ques.AsIsNumFives;
            }

            asisnumZeros = zeros;
            asisnumOnes = ones;
            asisnumTwos = twos;
            asisnumThrees = threes;
            asisnumFours = fours;
            asisnumFives = fives;

        }

        public void GetNumberOfToBeAnswers()
        {
            int zeros = 0;
            int ones = 0;
            int twos = 0;
            int threes = 0;
            int fours = 0;
            int fives = 0;
            foreach (ITCapQuestion ques in questionsOwned)
            {
                zeros += ques.TobeNumZeros;
                ones += ques.TobeNumOnes;
                twos += ques.TobeNumTwos;
                threes += ques.TobeNumThrees;
                fours += ques.TobeNumFours;
                fives += ques.TobeNumFives;
            }

            tobeNumZeros = zeros;
            tobeNumOnes = ones;
            tobeNumTwos = twos;
            tobeNumThrees = threes;
            tobeNumFours = fours;
            tobeNumFives = fives;
        }



        public void AddObjectiveToTrack(string name, float score)
        {
            //OBJECTIVESCORES.Add(name, 0);'
            ObjectiveValues val = new ObjectiveValues(name, 0, score);
            objectiveCollection.Add(val);
        }

        public override void CalculatePrioritizedCapabilityGap()
        {
            int numBoms = objectiveCollection.Count;
            float sum = 0;

            foreach (ObjectiveValues val in objectiveCollection)
            {
                sum += ((val.Score * val.BomScore) / (3 * numBoms));
            }

            PrioritizedCapabilityGap = sum;

            //CalculatePrioritizedGapText();

        }

        public void CalculatePrioritizedGapText()
        {
            /* if (prioritizedCapabilityGap >= 6)
             {
                 PrioritizedGap = "High Gap";
                 PrioritizedGapType1 = PrioritizedGapType.High;
             }
             else if (prioritizedCapabilityGap > 4 && prioritizedCapabilityGap < 6)
             {
                 PrioritizedGap = "Medium Gap";
                 PrioritizedGapType1 = PrioritizedGapType.Middle;
             }
             else if (prioritizedCapabilityGap <= 4)
             {
                 PrioritizedGap = "Low Gap";
                 PrioritizedGapType1 = PrioritizedGapType.Low;
             }
             else
             {
                 PrioritizedGap = "No Gap";
                 PrioritizedGapType1 = PrioritizedGapType.None;
             }
             */

        }

        public string CapName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public bool IsFlagged
        {
            get
            {
                return flagged;
            }
            set
            {
                flagged = value;
            }
        }
        public Domain Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public List<ITCapQuestion> QuestionsOwned
        {
            get { return questionsOwned; }
            set { questionsOwned = value; }
        }

        public Dictionary<string, int> OBJECTIVESCORES2
        {
            get { return OBJECTIVESCORES; }
            // set { objectiveScores = value; }
        }

        private static SortTpe SortType
        {
            get { return Capability.sortType; }
            set { Capability.sortType = value; }
        }


    }
}
