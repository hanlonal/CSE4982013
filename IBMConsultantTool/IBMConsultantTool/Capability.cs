using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    

    public class Capability
    {
        private List<ITCapQuestion> questions = new List<ITCapQuestion>();
        private float asIsAverage;
        private float toBeAverage;
        private Domain owner;

        private bool isDefault = true;



        string name;




        string toolID;



        string listIndex;

        public Capability()
        {
            Console.WriteLine("capability created");
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
        public Domain Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public override string ToString()
        {
            return name + "::" + toolID;
        }
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

    }
}
