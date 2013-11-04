using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public abstract class ScoringEntity : INotifyPropertyChanged
    {
        public enum GapType { High, Low, Middle, None };
        public enum PrioritizedGapType {High, Low, Middle, None};
        protected GapType gapType;
        private PrioritizedGapType prioritizedGapType;



        protected float asIsScore = 0;
        protected float toBeScore = 0;
        protected int indexInGrid = 0;
        protected string name;
        protected bool isDefault = false;
        protected string id;
        private bool isInGrid = false;
        protected string type;
        protected float prioritizedCapabilityGap = 0;
        protected float asisStandardDeviation = 0;
        protected float tobeStandardDeviation = 0;
        private bool visible = false;
        protected bool flagged = false;

        protected int numOnes = 0;
        protected int numTwos = 0;
        protected int numThrees = 0;
        protected int numFours = 0;
        protected int numFives = 0;
        protected int numZeros = 0;






        private string capabilityGapText;


        private string prioritizedGap;








        protected float capabilityGap = 0;


        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void CalculateCapabilityGap()
        {
            capabilityGap = toBeScore - asIsScore;
        }
        public virtual void CalculatePrioritizedCapabilityGap()
        {

        }
        public virtual void ChangeChildrenVisibility()
        {
            return;
        }

        public ScoringEntity()
        {
            Console.WriteLine("scoring entity created");
            gapType = GapType.None;
        }

        public virtual void UpdateIndexDecrease(int index)
        {

        }
        public virtual void UpdateIndexIncrease()
        {

        }

        public override string ToString()
        {
            return name;
        }
        public abstract float CalculateAsIsAverage();
        public abstract float CalculateToBeAverage();

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [Browsable(false)]
        public int IndexInGrid
        {
            get { return indexInGrid; }
            set
            {
                indexInGrid = value;

            }
        }
        public int NumZeros
        {
            get { return numZeros; }
            set { numZeros = value; this.NotifyPropertyChanged("NumZeros"); }
        }
        public int NumOnes
        {
            get { return numOnes; }
            set { numOnes = value; this.NotifyPropertyChanged("NumOnes"); }
        }
        public int NumTwos
        {
            get { return numTwos; }
            set { numTwos = value; this.NotifyPropertyChanged("NumTwos"); }
        }
        public int NumThrees
        {
            get { return numThrees; }
            set { numThrees = value; this.NotifyPropertyChanged("NumThrees"); }
        }
        public int NumFours
        {
            get { return numFours; }
            set { numFours = value; this.NotifyPropertyChanged("NumFours"); }
        }
        public int NumFives
        {
            get { return numFives; }
            set { numFives = value; this.NotifyPropertyChanged("NumFives"); }
        }


        public float AsisStandardDeviation
        {
            get { return asisStandardDeviation; }
            set { asisStandardDeviation = value; this.NotifyPropertyChanged("AsIsScore"); }
        }
        public float TobeStandardDeviation
        {
            get { return tobeStandardDeviation; }
            set { tobeStandardDeviation = value; this.NotifyPropertyChanged("AsIsScore"); }
        }


        public float AsIsScore
        {
            get { return asIsScore; }
            set
            {
                asIsScore = value;

                this.NotifyPropertyChanged("AsIsScore");
            }
        }
        public float ToBeScore
        {
            get { return toBeScore; }
            set { toBeScore = value; this.NotifyPropertyChanged("ToBeScore"); }
        }
        [Browsable(false)]
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }
        [Browsable(false)]
        public bool IsInGrid
        {
            get { return isInGrid; }
            set { isInGrid = value; }
        }
        [Browsable(false)]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        [Browsable(false)]
        public float CapabilityGap
        {
            get { return capabilityGap; }
            set { capabilityGap = value;/* this.NotifyPropertyChanged("CapabilityGap");*/ }
        }
        public string CapabilityGapText
        {
            get { return capabilityGapText; }
            set { capabilityGapText = value; this.NotifyPropertyChanged("CapabilityGapText"); }
        }
        public string PrioritizedGap
        {
            get { return prioritizedGap; }
            set { prioritizedGap = value; this.NotifyPropertyChanged("PrioritizedGap"); }
        }
        [Browsable(false)]
        public float PrioritizedCapabilityGap
        {
            get { return prioritizedCapabilityGap; }
            set { prioritizedCapabilityGap = value; this.NotifyPropertyChanged("PrioritizedCapabilityGap"); }
        }
        [Browsable(false)]
        public PrioritizedGapType PrioritizedGapType1
        {
            get { return prioritizedGapType; }
            set { prioritizedGapType = value; }
        }

        [Browsable(false)]
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        [Browsable(false)]
        public bool Flagged
        {
            get { return flagged; }
            set { flagged = value; }
        }
        [Browsable(false)]
        public GapType GapType1
        {
            get { return gapType; }
            set { gapType = value; }
        }


    }
}
