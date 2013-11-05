using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class TrendAnalysisEntity : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        protected string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        protected string businessType;

        public string BusinessType
        {
            get { return businessType; }
            set { businessType = value; }
        }
        protected DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        protected DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

    }
}
