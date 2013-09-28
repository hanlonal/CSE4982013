using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class Initiative : Label
    {
        private BusinessObjective owner;
        private string name;

        private float criticality = 0;
        private float differentiation = 0;
        private float effectiveness = 0;

        public Initiative(BusinessObjective owner,string name)
        {
            this.owner = owner;
            this.name = name;
            this.Text = name;
            owner.Controls.Add(this);
            this.Location = FindLocation();
            this.BackColor = Color.SeaGreen;
            this.Width = owner.Width;
            this.Height = 20;

            owner.UpdateHeight();
           // owner.UpdateLocation();
        }

        private Point FindLocation()
        {
            Point p = new Point();

            p.X = 20;
            p.Y = 20 +owner.InitiativesCount * 20;
            return p;
            
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
        public string Name
        {
            get
            {
                return name;
            }
        }


    }
}
