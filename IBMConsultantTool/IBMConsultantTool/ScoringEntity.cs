using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
   public abstract class ScoringEntity : INotifyPropertyChanged
    {
       protected float asIsScore = 0;
       protected float toBeScore = 0;
       protected int indexInGrid = 0;
       protected string name;
       protected bool isDefault = false;
       protected string id;
       private bool isInGrid = false;
       protected string type;
       private float prioritizedCapabilityGap = 0;
       protected float asisStandardDeviation = 0;
       protected float tobeStandardDeviation = 0;
       private bool visible = false;

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
           set { name = value; this.NotifyPropertyChanged("Name"); }
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
       public float TobeStandardDeviation
       {
           get { return tobeStandardDeviation; }
           set { tobeStandardDeviation = value; this.NotifyPropertyChanged("AsIsScore"); }
       }
       public float AsisStandardDeviation
       {
           get { return asisStandardDeviation; }
           set { asisStandardDeviation = value; this.NotifyPropertyChanged("AsIsScore"); }
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
       public bool Visible
       {
           get { return visible; }
           set { visible = value; }
       }


    }
}
