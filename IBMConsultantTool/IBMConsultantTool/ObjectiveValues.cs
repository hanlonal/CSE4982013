using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ObjectiveValues
    {
        private string name;
        private int value;


        public ObjectiveValues(string name, int value)
        {
            this.name = name;
            this.value = value;
        }


        #region PROPERTIES

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}
