using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class ObjectiveValues : INotifyPropertyChanged
    {
        private string name;
        private int value =1;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public ObjectiveValues(string name, int value)
        {
            this.name = name;
            this.value = value;
        }


        #region PROPERTIES

        public int Value
        {
            get { return this.value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}
