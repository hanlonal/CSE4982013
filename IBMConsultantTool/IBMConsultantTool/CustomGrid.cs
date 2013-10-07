using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public class CustomGrid : DataGridView
    {
        int personCount =0;

        public CustomGrid()
        {

        }

        public int PersonCount
        {
            get
            {
                return personCount;
            }
            set
            {
                personCount = value;
            }
        }
    }
}
