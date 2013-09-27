using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
   public class BusinessObjective : Label
    {
        private string name;
        private Category owner;
        private int initiativesCount;
        private List<Initiative> initiatives = new List<Initiative>();

        public BusinessObjective(Category owner, string name)
        {
            this.owner = owner;
            this.name = name;
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
            initiative.Text = name;
            initiative.Location = new Point(20, 40);
            initiative.BackColor = Color.CornflowerBlue;
            Console.WriteLine(name + "belongs to " + this.name + "which belongs to " + Owner.Name);
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
