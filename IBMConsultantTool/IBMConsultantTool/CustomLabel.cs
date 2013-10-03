using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public class CustomLabel : Label
    {
        int questionID;
        CupeQuestion owner;


        public CustomLabel(CupeQuestion owner)
        {
            this.owner = owner;
        }


        public CupeQuestion Owner
        {
            get
            {
                return owner;
            }
        }

        public int QuestionID
        {
            set
            {
                questionID = value;
            }
            get
            {
                return questionID;
            }
        }

    }
}
