using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{    
    public class Capability : ScoringEntity
    {
        private List<ITCapQuestion> questionsOwned = new List<ITCapQuestion>();
        private static List<ObjectiveToTrack> priorityForObjective = new List<ObjectiveToTrack>();
        private Dictionary<string, int> OBJECTIVESCORES = new Dictionary<string, int>();
        
// ignore for now ********************************************************************
        public static List<ObjectiveToTrack> PriorityForObjective
        {
            get { return priorityForObjective; }
            set { priorityForObjective = value; }
        }
        public class ObjectiveToTrack
        {
            private string name;
            public ObjectiveToTrack()
            {

            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }        
        }
        
//**************************************************************************************************

        //USE THIS ONE!!!!!!!!!!
        private List<string> objectiveScores = new List<string>();



        private Domain owner;

        public Capability()
        {
            Console.WriteLine("capability created");
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
                if (question.AsIsScore != 0 )
                {
                    total += question.AsIsScore;
                    activeQuestionCount++;
                }
            }
            asIsScore = activeQuestionCount == 0 ? 0 : total / activeQuestionCount;
            owner.CalculateAsIsAverage();
            return asIsScore;
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
            return toBeScore;
        }

        public void AddObjectiveToTrack(string name)
        {
            OBJECTIVESCORES.Add(name, 0);

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
        
        public int this[string key]
        {
            get
            {
                int value;
                OBJECTIVESCORES.TryGetValue(key, out value);
                return value;
            }
            set
            {
                if (value == null) OBJECTIVESCORES.Remove(key);
                else OBJECTIVESCORES[key] = value;
            }

        }
    }
}
