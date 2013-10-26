﻿using System;
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


       private float capabilityGap = 0;


       public event PropertyChangedEventHandler PropertyChanged;


       public virtual void CalculateCapabilityGap()
       {
           capabilityGap = toBeScore - asIsScore;
       }
       public virtual void CalculatePrioritizedCapabilityGap()
       {

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
       public float CapabilityGap
       {
           get { return capabilityGap; }
           set { capabilityGap = value; this.NotifyPropertyChanged("CapabilityGap"); }
       }
       public float PrioritizedCapabilityGap
       {
           get { return prioritizedCapabilityGap; }
           set { prioritizedCapabilityGap = value; this.NotifyPropertyChanged("PrioritizedCapabilityGap"); }
       }


    }
}
