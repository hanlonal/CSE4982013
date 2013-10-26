using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITCapQuestion : ScoringEntity
    {
        private Capability owner;
        public string comment;
        private List<float> AsIsanswersToAttributes = new List<float>();
        private List<float> ToBeanswersToAttributes = new List<float>();



        public ITCapQuestion()
        {
            Console.WriteLine("question created");
        }

        public override void UpdateIndexDecrease(int index)
        {

        }

        public void AddAsIsAnswer(float answer)
        {
            AsIsanswersToAttributes.Add(answer);
        }
        public void AddToBeAnswer(float answer)
        {
            ToBeanswersToAttributes.Add(answer);
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
