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
        private string region;
        private string businessType;
        private DateTime startDate;


        private bool bomCompleted;
        private bool cupeCompleted;
        private bool itcapCompleted;

        private object entityObject;

        public Client()
        {
            bomCompleted = false;
            cupeCompleted = false;
            itcapCompleted = false;
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
        public string Region
        {
            get { return region; }
            set { region = value; }
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
        public bool CupeCompleted
        {
            get { return cupeCompleted; }
            set { cupeCompleted = value; }
        }

        public bool ITCapCompleted
        {
            get { return itcapCompleted; }
            set { itcapCompleted = value; }
        }

        public object EntityObject
        {
            get
            {
                return entityObject;
            }

            set
            {
                entityObject = value;
            }
        }


        #endregion

    }
}
