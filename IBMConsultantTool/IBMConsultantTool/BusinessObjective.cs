using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
   public class BusinessObjective : Panel
    {
        private string name;
        private Category owner;
        private int initiativesCount;
       private int baseObjectiveHeight = 40;
       private int baseObjectiveWidth;
       private int labelHeight = 20;


        private List<Initiative> initiatives = new List<Initiative>();

        public BusinessObjective(Category owner, string name)
        {
            this.owner = owner;
            this.name = name;
            baseObjectiveWidth = owner.Width;
            owner.Controls.Add(this);
            this.Height = baseObjectiveHeight;
            this.Width = Parent.Width;
            this.BackColor = Color.Aquamarine;
            this.Location = FindLocation();


            MakeLabel();
            // objective.Name = name;
           owner.UpdateHeight();
            //owner.Controls.Add(this);
            //this.Location = FindLocation();
        }



        private void MakeLabel()
        {
            Label label = new Label();
            label.Text = name;
            label.BackColor = Color.Pink;
            label.ForeColor = Color.Black;
            label.Height = labelHeight;
            this.Controls.Add(label);
            label.Width = Parent.Width;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Location = new Point(0, 0);
            //label.BackColor = Color.Teal;

        }

        private Point FindLocation()
        {
            Point p = new Point();
            int totalHeight = labelHeight ;
            foreach (BusinessObjective obj in owner.Objectives)
            {
                totalHeight += obj.Height;
            }
            p.Y = totalHeight;
            p.X = 20;
            return p;
        }

        public void UpdateHeight()
        {


            this.Height = 20+ baseObjectiveHeight + initiatives.Count * 20;
            //UpdateLocation();
            owner.UpdateHeight();           
        }

        public void UpdateLocation()
        {
            int totalHeight = 20;
            for(int i = 0; i < owner.Objectives.Count; i++)
            {
                if (i == 0)
                    continue;
                totalHeight += owner.Objectives[i - 1].Height;

                    owner.Objectives[i].Location = new Point(20, totalHeight);

                
            }

            
        }


        public string Name
        {
            get
            {
                return name;
            }
        }
        public Category Owner
        {
            get
            {
                return owner;
            }
        }



        public void AddInitiative(string name)
        {
            Initiative initiative = new Initiative(this, name);
            this.Controls.Add(initiative);
            initiatives.Add(initiative);
            initiativesCount++;

            Console.WriteLine(name + "belongs to " + this.name + "which belongs to " + Owner.Name);

            UpdateLocation();
        }

        public List<Initiative> Initiatives
        {
            get
            {
                return initiatives;
            }
        }

        public int InitiativesCount
        {
            get
            {
                return initiativesCount;
            }
        }
    }
}
