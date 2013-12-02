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
        private int score;

        public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangedEventHandler PropertyChanged;
      /*  private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }*/

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ObjectiveValues(string name, int value)
        {
            this.name = name;
            this.score = value;
        }


        #region PROPERTIES

        public int Score
        {
            get { return this.score; }
            set 
            {
                int val = Convert.ToInt32(value);
                this.score = val; this.NotifyPropertyChanged("Score"); 
            }
        }

        public override string ToString()
        {
            return score.ToString();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}
