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
        NewObjective lastClicked;
        private BOMTool owner;
        List<NewObjective> objectives = new List<NewObjective>();
        private int ID;
        public string name;

        public NewCategory(BOMTool owner, int id, string name)
        {
            this.owner = owner;
            this.ID = id;
            this.name = name;

        }

        public NewObjective AddObjective(string name)
        {
            NewObjective objective = new NewObjective(this, name);
            objectives.Add(objective);
            return objective;
        }

        public void AddInitiative(string name)
        {
            lastClicked.AddInitiative(name);
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


    }// end class
}
