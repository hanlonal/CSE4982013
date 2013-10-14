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
        int numCapabilities = 0;

        int indexInList;

        float averageAsIs = 0;
        float averageToBe = 0;

        private List<Capability> capabilities = new List<Capability>();

        public Domain()
        {
            Console.WriteLine("new domain created");

        }

        public void AddCapabilitytoList(Capability cap)
        {
            capabilities.Add(cap);
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
        public int Index
        {
            get { return indexInList; }
            set { indexInList = value; }
        }

    }// end class
}
