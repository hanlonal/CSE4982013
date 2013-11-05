using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class TrendAnalysisEntity
    {

        private string Location;

        public string Location1
        {
            get { return Location; }
            set { Location = value; }
        }
        private string businessType;

        public string BusinessType
        {
            get { return businessType; }
            set { businessType = value; }
        }
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

    }
}
