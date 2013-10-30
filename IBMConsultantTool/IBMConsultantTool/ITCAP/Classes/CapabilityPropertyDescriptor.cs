/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class CapabilityPropertyDescriptor : PropertyDescriptor
    {

        public override object GetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            ((PropertyBag)component)[Name] = (string)value;
            //this.NotifyPropertyChanged();

        }

    }
}
*/