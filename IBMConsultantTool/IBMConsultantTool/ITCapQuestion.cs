using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ITCapQuestion
    {
        private float toBeScore;
        private float asIsScore;

        private Capability owner;

        private int indexInGrid;



        public Capability Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private bool isDefault = true;

        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        string questionText;

        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }

        string toolId;

        public string ToolId
        {
            get { return toolId; }
            set { toolId = value; }
        }
        public int IndexInGrid
        {
            get { return indexInGrid; }
            set { indexInGrid = value; }
        }


        public ITCapQuestion()
        {
            Console.WriteLine("question created");
        }
    }
}
