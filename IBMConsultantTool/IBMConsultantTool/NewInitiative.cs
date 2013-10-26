﻿using System;
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

        private float criticality = 1;
        private float differentiation = 3;
        private float effectiveness = 2;
        private float totalBOMScore = 0;



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
        public void CalculateTotalBOMScore()
        {
            if (criticality == 0 || differentiation == 0 || effectiveness == 0)
                return;
            totalBOMScore = ((11-effectiveness)*criticality*.5f)/10 +differentiation*.5f;
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
