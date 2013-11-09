// $Id:$

using System;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class ObjectiveValuePropertyDescriptor : PropertyDescriptor
    {
        public ObjectiveValuePropertyDescriptor(string name)
            : base(name, null)
        {
        }

        public override bool CanResetValue(object component) { return true; }

        public override Type ComponentType { get { return typeof(ObjectiveValues); } }

        public override bool IsReadOnly { get { return false; } }

        public override Type PropertyType { get { return typeof(int); } }

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) { return false; }

        // this property descriptor is implemented read only, but setting
        // a value could be implemented much the same way as getting one
        public override void SetValue(object component, object value) 
        {
            OnValueChanged(component, EventArgs.Empty);
            Capability val = (Capability)component;
            foreach (ObjectiveValues dv in val.ObjectiveCollection)
                if (dv.Name == this.Name)
                    dv.Value = Convert.ToInt32((string)value);
        }

        public override object GetValue(object component)
        {
            // Okay, getting here we know that component must be of type MasterRecord
            // because we have the ComponentType return that type for us.
            Capability cap = (Capability)component;

            // Now we need to find if the current record has a detail value
            // with the name for which this descriptor has been configured.
            // If we find it, we return the value, if not, we return 0 (it's 
            // just an example, after all :-))
            // In the real world it might make more sense to make the property's 
            // type a string and return "n/a" instead... but then we'd need
            // additional type conversions.
            // Of course, this is all not very efficiently implemented, but 
            // again, it's just an example.

            foreach (ObjectiveValues dv in cap.ObjectiveCollection)
                if (dv.Name == this.Name)
                    return dv.Value;

            return 0;
        }
    }
}
