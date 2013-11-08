using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class TrendAnalysisEntity : INotifyPropertyChanged
    {

        public enum Type { Master, Child };
        private Type type;
        [Browsable(false)]
        public Type Type1
        {
            get { return type; }
            set { type = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private int children = 0;

        public int Children
        {
            get { return children; }
            set { children = value; }
        }

        protected string name;

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        protected string region;

        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        protected string businessType;

        public string BusinessType
        {
            get { return businessType; }
            set { businessType = value; }
        }
        protected DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


    }
}
