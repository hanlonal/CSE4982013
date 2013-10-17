using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
   public abstract class ScoringEntity
    {
       protected float asIsScore = 0;
       protected float toBeScore = 0;
       protected int indexInGrid = 0;
       protected string name;
       protected bool isDefault = false;
       protected string id;
       private bool isInGrid = false;





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
       

       


       public int IndexInGrid
       {
           get { return indexInGrid; }
           set { indexInGrid = value; }
       }
       public float AsIsScore
       {
           get { return asIsScore; }
           set { asIsScore = value; }
       }
       public float ToBeScore
       {
           get { return toBeScore; }
           set { toBeScore = value; }
       }
       public string Name
       {
           get { return name; }
           set { name = value; }
       }
       public bool IsDefault
       {
           get { return isDefault; }
           set { isDefault = value; }
       }
       public bool IsInGrid
       {
           get { return isInGrid; }
           set { isInGrid = value; }
       }


    }
}
