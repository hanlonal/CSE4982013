using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class PropertyBagPropertyDescriptor : PropertyDescriptor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public PropertyBagPropertyDescriptor(string name) : base(name, null) { }
        public override object GetValue(object component)
        {


            return ((PropertyBag)component)[Name];
        }
        public override void SetValue(object component, object value)
        {
            ((PropertyBag)component)[Name] = (string)value;
            //this.NotifyPropertyChanged();

        }
        public override void ResetValue(object component)
        {
            ((PropertyBag)component)[Name] = null;
        }
        public override bool CanResetValue(object component)
        {
            return true;
        }
        public override bool ShouldSerializeValue(object component)
        {
            return ((PropertyBag)component)[Name] != null;
        }
        public override Type PropertyType
        {
            get { return typeof(string); }
        }
        public override bool IsReadOnly
        {
            get { return false; }
        }
        public override Type ComponentType
        {
            get { return typeof(PropertyBag); }
        }
    }
}
