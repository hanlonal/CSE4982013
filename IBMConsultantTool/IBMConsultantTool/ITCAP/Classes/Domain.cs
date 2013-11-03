using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class Domain : ScoringEntity
    {
        private List<Capability> capabilitiesOwned = new List<Capability>();
        private int totalChildren = 0;



        public Domain()
        {
            
            Console.WriteLine("domain created");
            
        }

        public override void UpdateIndexDecrease(int index)
        {
            foreach (Capability cap in capabilitiesOwned)
            {
                if (cap.IsInGrid && cap.IndexInGrid > index)
                    cap.IndexInGrid--;
            }
        }

        public override float CalculateAsIsAverage()
        {
            float total = 0;
            float activeCaps = 0;
            foreach (Capability cap in capabilitiesOwned)
            {
                if (cap.AsIsScore != 0)
                {
                    total += cap.AsIsScore;
                    activeCaps++;
                }
            }
            asIsScore = activeCaps == 0 ? 0 : total / activeCaps;
            return asIsScore;
        }

        public override float CalculateToBeAverage()
        {
            float total = 0;
            float activeCaps = 0;
            foreach (Capability cap in capabilitiesOwned)
            {
                if (cap.ToBeScore != 0)
                {
                    total += cap.ToBeScore;
                    activeCaps++;
                }
            }
            toBeScore = activeCaps == 0 ? 0 : total / activeCaps;

            return toBeScore;
        }

        public override void ChangeChildrenVisibility()
        {
            foreach (Capability cap in capabilitiesOwned)
            {
                cap.Visible = !cap.Visible;
                foreach (ITCapQuestion ques in cap.QuestionsOwned)
                {
                    if (!cap.Visible)
                        ques.Visible = cap.Visible;
                }
            }
            base.ChangeChildrenVisibility();
        }

        public List<Capability> CapabilitiesOwned
        {
            get { return capabilitiesOwned; }
            set { capabilitiesOwned = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public int TotalChildren
        {
            get { return totalChildren; }
            set { totalChildren = value; }
        }


    }// end class
}
