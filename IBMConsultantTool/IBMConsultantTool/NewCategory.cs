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

        private BOMTool owner;
        List<NewObjective> objectives = new List<NewObjective>();
        private int ID;

        public NewCategory(BOMTool owner, int id)
        {
            this.owner = owner;
            this.ID = id;

        }

        public void AddObjective(string name)
        {
            NewObjective objective = new NewObjective(this, name);
            objectives.Add(objective);

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


    }// end class
}
