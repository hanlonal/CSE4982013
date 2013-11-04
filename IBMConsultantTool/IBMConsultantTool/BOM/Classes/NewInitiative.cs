using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class NewInitiative : Label
    {
        private NewObjective owner;
        private string name;
        private int baseHeight = 20;

        private float criticality = 0;
        private float differentiation = 0;
        private float effectiveness = 0;
        private float totalBOMScore = 0;



        public static int criticalAmount = 4;
        public static int averageAmount = 7;
        enum RatingsState { Dynamic, Static };
        static RatingsState state = RatingsState.Static;
       // private int goodAmount = 10;
        

        public NewInitiative(NewObjective owner, string name)
        {
            this.owner = owner;
            this.Text = name;
            this.name = name;
            //Console.WriteLine(name + "belongs to " + owner.Name);
            owner.Controls.Add(this);
            this.Location = FindLocation();
            this.Height = baseHeight;
            this.BackColor = Color.White;
            this.AutoEllipsis = true;
            
            this.Width = owner.Width;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private Point FindLocation()
        {

            Point p = new Point();

            p.X = 10;
            p.Y = 30 +(owner.Initiatives.Count) * 30;
            owner.UpdateHeight();
            return p;
            
        
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
                        BackColor = Color.LightSlateGray;
                        return;
                    }
                    if (criticality < criticalAmount)
                        BackColor = Color.IndianRed;
                    if (criticality >= criticalAmount && criticality <= averageAmount)
                        BackColor = Color.Yellow;
                    if (criticality > averageAmount)
                        BackColor = Color.LawnGreen;
                }
                if (param == "differentiation")
                {
                    if (differentiation == 0)
                    {
                        BackColor = Color.LightSlateGray;
                        return;
                    }
                    if (differentiation < criticalAmount)
                        BackColor = Color.IndianRed;
                    if (differentiation >= criticalAmount && differentiation <= averageAmount)
                        BackColor = Color.Yellow;
                    if (differentiation > averageAmount)
                        BackColor = Color.LawnGreen;
                }

                if (param == "effectiveness")
                {
                    if (effectiveness == 0)
                    {
                        BackColor = Color.LightSlateGray;
                        return;
                    }
                    if (effectiveness < criticalAmount)
                        BackColor = Color.IndianRed;
                    if (effectiveness >= criticalAmount && effectiveness <= averageAmount)
                        BackColor = Color.Yellow;
                    if (effectiveness > averageAmount)
                        BackColor = Color.LawnGreen;
                }

                if (param == "bomscore")
                {
                    if (totalBOMScore == 0)
                    {
                        BackColor = Color.LightSlateGray;
                        return;
                    }
                    if (totalBOMScore < criticalAmount)
                        BackColor = Color.IndianRed;
                    if (totalBOMScore >= criticalAmount && totalBOMScore <= averageAmount)
                        BackColor = Color.Yellow;
                    if (totalBOMScore > averageAmount)
                        BackColor = Color.LawnGreen;
                }
            }

            if (state == RatingsState.Dynamic)
            {
                

            }

        }

        public float Criticality
        {
            get
            {
                return criticality;
            }
            set
            {
                criticality = value;
                Console.WriteLine("here");
                CalculateTotalBOMScore();

            }
        }

        public float Effectiveness
        {
            get
            {
                return effectiveness;
            }
            set
            {
                effectiveness = value;
                Console.WriteLine("here");
                CalculateTotalBOMScore();
            }
        }

        public float Differentiation
        {
            get
            {
                return differentiation;
            }
            set
            {

                differentiation = value;
                Console.WriteLine("here");
                CalculateTotalBOMScore();

            }
        }

        public float TotalBOMScore
        {
            get { return totalBOMScore; }
            set { totalBOMScore = value; }
        }
    
    }// end class
}
