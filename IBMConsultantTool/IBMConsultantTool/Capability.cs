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

        int numQuestions = 0;
        int totalChildren = 0;






        string name;




        string toolID;

        int indexInDataGrid = 0;



        string listIndex;

        public Capability()
        {
            Console.WriteLine("capability created");
        }


        public void AddQuestionToList(ITCapQuestion question)
        {
            questions.Add(question);
            numQuestions++;
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
        public int NumQuestions
        {
            get { return numQuestions; }
            set { numQuestions = value; }
        }
        public List<ITCapQuestion> Questions
        {
            get { return questions; }
            set { questions = value; }
        }
        public int IndexInDataGrid
        {
            get { return indexInDataGrid; }
            set { indexInDataGrid = value; }
        }
        public int TotalChildren
        {
            get { return totalChildren; }
            set { totalChildren = value; }
        }

    }
}
