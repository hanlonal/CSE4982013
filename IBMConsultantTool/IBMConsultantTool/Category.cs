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
        private int baseCategoryHeight = 100;
        private int baseCategoryWidth = 200;
        private int labelHeight = 20;
       // private TreeView treeView;
       // private this this;
        private List<BusinessObjective> objectives = new List<BusinessObjective>();
        private BOMRedesign owner;
        //constructor
        public Category(BOMRedesign owner, string name )
        {
            this.name = name;
            this.owner = owner;
            this.BackColor = Color.Orange;
            this.Visible = true;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Location = FindLocation();
            this.Height = baseCategoryHeight;
            this.Width = baseCategoryWidth;
            owner.MainWorkspace.Controls.Add(this);

            MakeLabel();
        }

        

        private Point FindLocation()
        {
            Point p = new Point();
           // Console.WriteLine(owner.Width.ToString());

            p.X = baseCategoryWidth * (owner.CategoryCount % 4 );
            if (owner.CategoryCount > 3)
                p.Y = owner.Categories[owner.CategoryCount - 4].Location.Y + baseCategoryHeight;
            else
                p.Y = 0;
                
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

        public BusinessObjective AddObjective(string name)
        {
            BusinessObjective objective = new BusinessObjective(this, name);
            objectivesCount++;
            objectives.Add(objective);
            objective.Click +=new EventHandler(objective_Click);
            return objective;

        }

        private void MakeLabel()
        {
            Label label = new Label();
            label.Text = name;
            label.ForeColor = Color.Black;
            label.Height = labelHeight;
            this.Controls.Add(label);
            label.Width = Parent.Width;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Location = new Point(0, 0);
            label.BackColor = Color.Teal;
    
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

        public void UpdateHeight()
        {

            int totalHeight = baseCategoryHeight;
            foreach (BusinessObjective obj in objectives)
            {
                totalHeight += obj.Height;
            }

            if (totalHeight < baseCategoryHeight)
                return;
            else
                this.Height = totalHeight;
        }


    } // end of class
}