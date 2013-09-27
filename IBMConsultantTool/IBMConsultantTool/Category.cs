using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class Category : Panel
    {
        private BusinessObjective lastClicked;
        private string name;
        private int objectivesCount = 0;
       // private TreeView treeView;
       // private this this;
        private List<BusinessObjective> objectives = new List<BusinessObjective>();
        private BOMRedesign owner;
        //constructor
        public Category(BOMRedesign owner, string name )
        {
            this.name = name;
            this.owner = owner;
            this.BackColor = Color.Red;
            this.Visible = true;
            this.Location = FindLocation();
            this.Height = 100;
            this.Width = 100;
            owner.Controls.Add(this);
        }

        

        private Point FindLocation()
        {
            Point p = new Point();
            Console.WriteLine(owner.Width.ToString());
            if (100 * owner.CategoryCount < owner.Width)
            
                p.X = owner.CategoryCount * 100;



            return p;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
    


        //public this Category

        public void AddObjective(string name)
        {
            BusinessObjective objective = new BusinessObjective(this, name);
           
            objectivesCount++;

            
            objective.BackColor = Color.Wheat;
            objective.Text = name;
           // objective.Name = name;
            objective.Width = 70;
            objective.Height = 50;
            this.Controls.Add(objective);
            objective.Location = new Point(0, 0);
            objectives.Add(objective);
            objective.Click +=new EventHandler(objective_Click);

        }

        private void objective_Click(object sender, EventArgs e)
        {
            BusinessObjective obj = (BusinessObjective)sender;
            Console.WriteLine(obj.Name);
            lastClicked = obj;
            owner.LastClickedCategory = obj.Owner;
        }

        public BusinessObjective LastClicked
        {
            get
            {
                return lastClicked;
            }
        }

        public List<BusinessObjective> Objectives
        {
            get
            {
                return objectives;
            }
        }
        public int BusinessObjectivesCount
        {
            get
            {
                return objectivesCount;
            }
        }

    } // end of class
}