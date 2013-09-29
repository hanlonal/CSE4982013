using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    class Person
    {
        public enum EmployeeType 
        { 
            IT, 
            Business 
        }

        LinkedList<CupeQuestion> questions = new LinkedList<CupeQuestion>();
        
        EmployeeType type = new EmployeeType();
        string name;

        public Person()
        {

        }

        public LinkedList<CupeQuestion> Questions
        {
            get
            {
                return questions;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }// end class
}
