using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class Client
    {

        private string name;
        private string location;
        private string businessType;
        private DateTime startDate;


        private bool bomCompleted;
        private bool CUPEcompleted;
        private bool ITCapCompleted;

        public Client()
        {
            
        }

        #region PROPERTIES START

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string BusinessType
        {
            get { return businessType; }
            set { businessType = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set 
            {
                DateTime val = (DateTime)value;
                val.ToShortDateString();
                startDate = val; 

            }
        }
        public bool BomCompleted
        {
            get { return bomCompleted; }
            set { bomCompleted = value; }
        }
        public bool CUPEcompleted1
        {
            get { return CUPEcompleted; }
            set { CUPEcompleted = value; }
        }

        public bool ITCapCompleted1
        {
            get { return ITCapCompleted; }
            set { ITCapCompleted = value; }
        }


        #endregion

    }
}
