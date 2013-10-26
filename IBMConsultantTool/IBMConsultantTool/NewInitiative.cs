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

        private int criticalAmount = 4;
        private int averageAmount = 7;
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

        public void ChangeColor(string param)
        {
            if (param == "criticality")
            {
                if (criticality < criticalAmount)
                    BackColor = Color.IndianRed;
                if (criticality >= criticalAmount && criticality <= averageAmount)
                    BackColor = Color.Yellow;
                if (criticality > averageAmount)
                    BackColor = Color.ForestGreen;
            }
            if (param == "differentiation")
            {
                if(differentiation < criticalAmount)
                    BackColor = Color.IndianRed;
                if (differentiation >= criticalAmount && differentiation <= averageAmount)
                    BackColor = Color.Yellow;
                if (differentiation > averageAmount)
                    BackColor = Color.ForestGreen;
            }

            if (param == "effectiveness")
            {
                if (effectiveness < criticalAmount)
                    BackColor = Color.IndianRed;
                if (effectiveness >= criticalAmount && effectiveness <= averageAmount)
                    BackColor = Color.Yellow;
                if (effectiveness > averageAmount)
                    BackColor = Color.ForestGreen;
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
            }
        }
    
    }// end class
}
