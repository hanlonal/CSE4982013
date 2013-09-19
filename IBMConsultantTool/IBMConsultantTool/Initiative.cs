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
