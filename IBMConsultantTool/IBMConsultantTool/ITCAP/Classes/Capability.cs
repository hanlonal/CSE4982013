using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class Capability : ScoringEntity
    {
        private List<ITCapQuestion> questionsOwned = new List<ITCapQuestion>();
        private Dictionary<string, int> OBJECTIVESCORES = new Dictionary<string, int>();

        static private float staticThreshold = 1;
        enum SortTpe { Static, Dynamic };
        static SortTpe sortType;
        static private float percentToCategorizeAsHigh = .33f;
        static private float percentToCategorizeAsLow = .33f;
        static private float staticHighGapThreshold = 1.5f;
        static private float staticLowGapThreshold = 1;
        static private Dictionary<Capability, float> dynamicCapabilityGaps = new Dictionary<Capability, float>();

        static private float dynamicAutoHighGap = 4;
        static private float dynamicAutoLowGap = .5f;


        ObjectiveValueCollection objectiveCollection = new ObjectiveValueCollection();

        public ObjectiveValueCollection ObjectiveCollection
        {
            get { return objectiveCollection; }
            set { objectiveCollection = value; }
        }

        private Domain owner;

        public Capability()
        {
            Console.WriteLine("capability created");
            sortType = SortTpe.Dynamic;
        }

        public override void UpdateIndexDecrease(int index)
        {
            foreach (ITCapQuestion question in questionsOwned)
            {
                if (question.IsInGrid && question.IndexInGrid > index)
                    question.IndexInGrid--;
            }
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
            return asIsScore;
        }

        public override void CalculateCapabilityGap()
        {
            base.CalculateCapabilityGap();
            if (sortType == SortTpe.Static)
            {
                if (capabilityGap >= staticHighGapThreshold)
                {
                    CapabilityGapText = "High Gap";
                    gapType = GapType.High;
                }
                else if (capabilityGap >= staticLowGapThreshold && capabilityGap < staticHighGapThreshold)
                {
                    CapabilityGapText = "Medium Gap";
                    gapType = GapType.Middle;
                }
                else
                {
                    CapabilityGapText = "Low/No Gap";
                    gapType = GapType.Low;
                }
            }
            else if (sortType == SortTpe.Dynamic)
            {
                if (dynamicCapabilityGaps.ContainsKey(this))
                {
                    dynamicCapabilityGaps[this] = capabilityGap;
                }
                else
                {
                    if (this.asIsScore == 0 && this.toBeScore == 0)
                    {
                        this.gapType = GapType.None;
                        return;
                    }
                    dynamicCapabilityGaps.Add(this, capabilityGap);
                }

                if (capabilityGap >= dynamicAutoHighGap)
                {
                    CapabilityGapText = "High Gap";
                    gapType = GapType.High;
                    return;
                }
                else if (capabilityGap <= dynamicAutoLowGap)
                {
                    CapabilityGapText = "Low Gap";
                    gapType = GapType.Low;
                    return;
                }

                var items = from pair in dynamicCapabilityGaps
                            orderby pair.Value ascending
                            select pair;

                if (dynamicCapabilityGaps.Count > 3)
                {
                    int numberForLow = (int)(decimal)(dynamicCapabilityGaps.Count * percentToCategorizeAsLow);
                    int numberForMid = (int)(decimal)(dynamicCapabilityGaps.Count * (1 - (percentToCategorizeAsHigh + percentToCategorizeAsLow)));
                    int numberForHigh = (int)(decimal)(dynamicCapabilityGaps.Count * percentToCategorizeAsHigh) + numberForLow + numberForMid;

                    int count = 0;
                    foreach (KeyValuePair<Capability, float> pair in items)
                    {
                        if (count < numberForLow)
                        {
                            pair.Key.CapabilityGapText = "Low Gap";
                            pair.Key.gapType = GapType.Low;
                        }

                        else if (count >= numberForLow && count < numberForHigh)
                        {
                            pair.Key.CapabilityGapText = "Middle Gap";
                            pair.Key.gapType = GapType.Middle;
                        }

                        else if (count >= numberForHigh)
                        {
                            pair.Key.CapabilityGapText = "High Gap";
                            pair.Key.gapType = GapType.High;
                        }

                        if (pair.Value == 0)
                            pair.Key.gapType = GapType.None;

                        count++;



                    }

                }
                else
                {

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
            return toBeScore;
        }

        public void AddObjectiveToTrack(string name)
        {
            //OBJECTIVESCORES.Add(name, 0);
            ObjectiveValues val = new ObjectiveValues(name, 0);
            objectiveCollection.Add(val);
        }

        public override void CalculatePrioritizedCapabilityGap()
        {
            prioritizedCapabilityGap = 0;
            foreach (ObjectiveValues val in ObjectiveCollection)
            {
                prioritizedCapabilityGap += val.Value;
            }
            CalculatePrioritizedGapText();

        }

        public void CalculatePrioritizedGapText()
        {
            if (prioritizedCapabilityGap >= 20)
                PrioritizedGap = "High Gap";
            else if (prioritizedCapabilityGap > 10 && prioritizedCapabilityGap < 20)
                PrioritizedGap = "Medium Gap";
            else
                PrioritizedGap = "Low Gap";
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
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
        public string ID
        {
            get { return id; }
            set
            {
                string a = value;
                id = owner.ID + "." + a;
            }
        }
        public bool Flagged
        {
            set { flagged = value; owner.Flagged = value; }
        }
        public Dictionary<string, int> OBJECTIVESCORES2
        {
            get { return OBJECTIVESCORES; }
            // set { objectiveScores = value; }
        }


    }
}
