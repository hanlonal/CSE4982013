using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITCapQuestion : ScoringEntity
    {
        private Capability owner;

        public ITCapQuestion()
        {
            Console.WriteLine("question created");
        }

        public override void UpdateIndexDecrease(int index)
        {

        }

        public override float CalculateAsIsAverage()
        {
            return 0;
        }
        public override float CalculateToBeAverage()
        {
            return 0;
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

    }
}
