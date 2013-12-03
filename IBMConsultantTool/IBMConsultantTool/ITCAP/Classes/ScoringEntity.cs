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
        private string id;


        private bool isInGrid = false;
        protected string type;
        protected float prioritizedCapabilityGap = 0;
        protected float asisStandardDeviation = 0;
        protected float tobeStandardDeviation = 0;
        private bool visible = false;
        protected bool flagged = false;

        protected int asisnumOnes = 0;
        protected int asisnumTwos = 0;
        protected int asisnumThrees = 0;
        protected int asisnumFours = 0;
        protected int asisnumFives = 0;
        protected int asisnumZeros = 0;

        protected int tobeNumZeros = 0;
        protected int tobeNumOnes = 0;
        protected int tobeNumTwos = 0;
        protected int tobeNumThrees = 0;
        protected int tobeNumFours = 0;
        protected int tobeNumFives = 0;


        protected string capabilityGapText;

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

        public string ID
        {
            get { return id; }
            set { id = value; this.NotifyPropertyChanged("ID"); }
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
        public int AsIsNumZeros
        {
            get { return asisnumZeros; }
            set { asisnumZeros = value; this.NotifyPropertyChanged("AsIsNumZeros"); }
        }
        public int AsIsNumOnes
        {
            get { return asisnumOnes; }
            set { asisnumOnes = value; this.NotifyPropertyChanged("AsIsNumOnes"); }
        }
        public int AsIsNumTwos
        {
            get { return asisnumTwos; }
            set { asisnumTwos = value; this.NotifyPropertyChanged("AsIsNumTwos"); }
        }
        public int AsIsNumThrees
        {
            get { return asisnumThrees; }
            set { asisnumThrees = value; this.NotifyPropertyChanged("AsIsNumThrees"); }
        }
        public int AsIsNumFours
        {
            get { return asisnumFours; }
            set { asisnumFours = value; this.NotifyPropertyChanged("AsIsNumFours"); }
        }
        public int AsIsNumFives
        {
            get { return asisnumFives; }
            set { asisnumFives = value; this.NotifyPropertyChanged("AsIsNumFives"); }
        }

        public int TobeNumZeros
        {
            get { return tobeNumZeros; }
            set { tobeNumZeros = value; this.NotifyPropertyChanged("ToBeNumZeros"); }
        }

        public int TobeNumOnes
        {
            get { return tobeNumOnes; }
            set { tobeNumOnes = value; this.NotifyPropertyChanged("ToBeNumOnes"); }
        }

        public int TobeNumTwos
        {
            get { return tobeNumTwos; }
            set { tobeNumTwos = value; this.NotifyPropertyChanged("ToBeNumTwos"); }
        }

        public int TobeNumThrees
        {
            get { return tobeNumThrees; }
            set { tobeNumThrees = value; this.NotifyPropertyChanged("ToBeNumThrees"); }
        }

        public int TobeNumFours
        {
            get { return tobeNumFours; }
            set { tobeNumFours = value; this.NotifyPropertyChanged("ToBeNumFours"); }
        }

        public int TobeNumFives
        {
            get { return tobeNumFives; }
            set { tobeNumFives = value; this.NotifyPropertyChanged("ToBeNumFives"); }
        }


        public float AsisStandardDeviation
        {
            get { return asisStandardDeviation; }
            set { asisStandardDeviation = value; this.NotifyPropertyChanged("AsisStandardDeviation"); }
        }
        public float TobeStandardDeviation
        {
            get { return tobeStandardDeviation; }
            set { tobeStandardDeviation = value; this.NotifyPropertyChanged("TobeStandardDeviation"); }
        }


        public float AsIsScore
        {
            get { return asIsScore; }
            set
            {
                asIsScore = value;

                decimal asIs = Convert.ToDecimal(asIsScore);
                asIs = Math.Round(asIs, 2);
                asIsScore = (float)asIs;

                this.NotifyPropertyChanged("AsIsScore");
            }
        }
        public float ToBeScore
        {
            get { return toBeScore; }
            set 
            { 
                toBeScore = value;

                decimal toBe = Convert.ToDecimal(toBeScore);
                toBe = Math.Round(toBe, 2);
                toBeScore = (float)toBe;

                this.NotifyPropertyChanged("ToBeScore");
            }
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
            set 
            {
                
                prioritizedCapabilityGap = value; this.NotifyPropertyChanged("PrioritizedCapabilityGap"); 
            
            }
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
