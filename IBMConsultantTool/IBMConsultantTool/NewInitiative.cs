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

            p.X = 20;
            p.Y = 20 +(owner.Initiatives.Count) * 20;
            owner.UpdateHeight();
            return p;
            
        
        }

        public void ChangeColor(string param)
        {
            if (param == "criticality")
            {
                if (criticality < 4)
                    BackColor = Color.Red;
                if (criticality >= 4 && criticality <= 7)
                    BackColor = Color.Yellow;
                if (criticality > 7)
                    BackColor = Color.Green;
            }
            if (param == "differentiation")
            {
                if(differentiation < 4)
                    BackColor = Color.Red;
                if (differentiation >= 4 && differentiation <= 7)
                    BackColor = Color.Yellow;
                if (differentiation > 7)
                    BackColor = Color.Green;
            }

            if (param == "effectiveness")
            {
                if (effectiveness < 4)
                    BackColor = Color.Red;
                if (effectiveness >= 4 && effectiveness <= 7)
                    BackColor = Color.Yellow;
                if (effectiveness > 7)
                    BackColor = Color.Green;
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
