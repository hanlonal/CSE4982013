using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{    
    public class Capability : ScoringEntity
    {
        private List<ITCapQuestion> questionsOwned = new List<ITCapQuestion>();


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
    }
}
