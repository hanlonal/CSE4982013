using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class NewImperative : INotifyPropertyChanged
    {
        private NewObjective owner;
        private string name;
        private int baseHeight = 20;

        private float criticality = 0;
        private float differentiation = 0;
        private float effectiveness = 0;
        private float totalBOMScore = 0;

       public enum ScoreState { High, Medium, Low, None };
        ScoreState scoreState;

        public static int criticalAmount = 4;
        public static int averageAmount = 7;
        enum RatingsState { Dynamic, Static };
        static RatingsState state = RatingsState.Static;
       // private int goodAmount = 10;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        public NewImperative(NewObjective owner, string name)
        {
            this.owner = owner;
            //this.Text = name;
            this.name = name;


        }

        public string Name
        {
            get{ return name; }
            set { name = value; }
        }


        public void CalculateTotalBOMScore()
        {
            if (criticality == 0 || differentiation == 0 || effectiveness == 0)
                return;
            totalBOMScore = ((11-effectiveness)*criticality*.5f)/10 +differentiation*.5f;
        }

        public void ChangeColor(string param)
        {
            if (state == RatingsState.Static)
            {
                if (param == "criticality")
                {
                    if (criticality == 0)
                    {
                        scoreState = ScoreState.None;
                        return;
                    }
                    if (criticality < criticalAmount)
                        scoreState = ScoreState.High;
                    if (criticality >= criticalAmount && criticality <= averageAmount)
                        scoreState = ScoreState.Medium;
                    if (criticality > averageAmount)
                        scoreState = ScoreState.Low;
                }
                if (param == "differentiation")
                {
                    if (differentiation == 0)
                    {
                        scoreState = ScoreState.None;
                        return;
                    }
                    if (differentiation < criticalAmount)
                        scoreState = ScoreState.High;
                    if (differentiation >= criticalAmount && differentiation <= averageAmount)
                        scoreState = ScoreState.Medium;
                    if (differentiation > averageAmount)
                        scoreState = ScoreState.Low;
                }

                if (param == "effectiveness")
                {
                    if (effectiveness == 0)
                    {
                        scoreState = ScoreState.None;
                        return;
                    }
                    if (effectiveness < criticalAmount)
                        scoreState = ScoreState.High;
                    if (effectiveness >= criticalAmount && effectiveness <= averageAmount)
                        scoreState = ScoreState.Medium;
                    if (effectiveness > averageAmount)
                        scoreState = ScoreState.Low;
                }

                if (param == "bomscore")
                {
                    if (totalBOMScore == 0)
                    {
                        scoreState = ScoreState.None;
                        return;
                    }
                    if (totalBOMScore < criticalAmount)
                        scoreState = ScoreState.High;
                    if (totalBOMScore >= criticalAmount && totalBOMScore <= averageAmount)
                        scoreState = ScoreState.Medium;
                    if (totalBOMScore > averageAmount)
                        scoreState = ScoreState.Low;
                }
            }

            if (state == RatingsState.Dynamic)
            {
                

            }

        }

        public float Criticality
        {
            get { return criticality; }
            set { criticality = value; CalculateTotalBOMScore(); this.NotifyPropertyChanged("Criticality"); }
        }

        public float Effectiveness
        {
            get { return effectiveness; }
            set { effectiveness = value; CalculateTotalBOMScore(); this.NotifyPropertyChanged("Effectiveness"); }
        }

        public float Differentiation
        {
            get { return differentiation; }
            set { differentiation = value; CalculateTotalBOMScore(); this.NotifyPropertyChanged("Differentiation"); }
        }

        public float TotalBOMScore
        {
            get { return totalBOMScore; }
            set { totalBOMScore = value; this.NotifyPropertyChanged("TotalBOMScore"); }
        }
        [Browsable(false)]
        public ScoreState ScoreState1
        {
            get { return scoreState; }
            set { scoreState = value; }
        }
    
    }// end class
}
