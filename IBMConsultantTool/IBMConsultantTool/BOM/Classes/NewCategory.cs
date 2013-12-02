using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class NewCategory : TabPage
    {
        float[] heights = new float[3];
        NewObjective lastClicked;
        private BOMTool owner;
        public List<NewObjective> objectives = new List<NewObjective>();
        private int ID;
        public string name;

        public NewCategory(BOMTool owner, int id, string name)
        {
            this.owner = owner;
            this.ID = id;
            this.name = name;

        }

        public NewCategory(int id, string name)
        {
            this.ID = id;
            this.name = name;

        }

        public NewObjective AddObjective(string name)
        {
            NewObjective objective = new NewObjective(this, name);
            objective.Click +=new EventHandler(objective_Click);
            objectives.Add(objective);
            
            return objective;
            
        }
        public void RemoveObjective(NewObjective obj)
        {
           // Controls.RemoveByKey(obj.Name);
            objectives.Remove(obj);
            lastClicked = null;
            //obj.Controls.Clear();
            
            owner.RemoveObjective(obj);
            Refresh();
        }

        public void ChooseColor(NewObjective obj)
        {
            owner.ChooseColor(obj);
            Refresh();
        }

        public void objective_Click(object sender, EventArgs e)
        {
            NewObjective obj = (NewObjective)sender;
            //Console.WriteLine(obj.name);
            lastClicked = obj;
            owner.ObjectiveClicked(obj);
        }

        public void AddImperative(string name)
        {
            lastClicked.AddImperative(name);
        }

        public List<NewObjective> Objectives
        {
            get
            {
                return objectives;
            }
        }

        public TabPage Owner
        {
            get
            {
                return owner.CategoryWorkspace.SelectedTab;
            }
        }

        public BOMTool bomTool
        {
            get
            {
                return owner;
            }
        }
        public NewObjective LastClicked
        {
            get
            {
                return lastClicked;
            }
            set
            {
                lastClicked = value;
            }
        }

        public float[] Heights
        {
            get { return heights; }
            set { heights = value; }
        }

        
    }// end class
}
