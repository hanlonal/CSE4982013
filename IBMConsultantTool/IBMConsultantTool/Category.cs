using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public class Category : Panel
    {

      //  private Panel panel;
        private List<BusinessObjective> objectives;
        private BOMRedesign mainForm;
        //constructor
        public Category(BOMRedesign form)
        {
           // panel = new Panel();
            mainForm = form;

            this.Location = new System.Drawing.Point(0, 0);      

            this.Size = new System.Drawing.Size(100, 100);


            this.Visible = true;
        }
    }
}
