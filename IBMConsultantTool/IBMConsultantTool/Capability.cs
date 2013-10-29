﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{    
    public class Capability : ScoringEntity
    {
        private List<ITCapQuestion> questionsOwned = new List<ITCapQuestion>();
        private static List<ObjectiveToTrack> priorityForObjective = new List<ObjectiveToTrack>();
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
        private List<decimal> objectiveScores = new List<decimal>();



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

        public static void AddObjectiveToTrack( string name)
        {
            ObjectiveToTrack track = new ObjectiveToTrack();
            track.Name = name;
            
            Capability.priorityForObjective.Add(track);
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

        public void AddObjectiveToTrack()
        {
            objectiveScores.Add(0);

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
        public List<decimal> ObjectiveScores
        {
            get { return objectiveScores; }
            set { objectiveScores = value; }
        }
    }
}
