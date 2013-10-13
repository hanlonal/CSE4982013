using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class Domain
    {
        string databaseID;
        string toolID;
        string name;
        bool defaultDomain = true;

        float averageAsIs = 0;
        float averageToBe = 0;

        private LinkedList<Capability> capabilities = new LinkedList<Capability>();

        public Domain()
        {
            Console.WriteLine("new domain created");

        }

        public string Name
        {
            get { return name; }
            set { name = value; } 
        }
        public string ToolID
        {
            get { return toolID; }
            set { toolID = value; }
        }

        public override string ToString()
        {
            return name + " :: " + toolID;
        }
        public bool IsDefault
        {
            get { return defaultDomain; }
            set { defaultDomain = value; }
        }

    }// end class
}
