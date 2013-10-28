using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class PropertyBagList : List<PropertyBag>, ITypedList
    {
        public PropertyBag Add(params string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (args.Length != Columns.Count) throw new ArgumentException("args");
            PropertyBag bag = new PropertyBag();
            for (int i = 0; i < args.Length; i++)
            {
                bag[Columns[i]] = args[i];
            }
            Add(bag);
            return bag;
        }
        public PropertyBagList() { Columns = new List<string>(); }
        public List<string> Columns { get; private set; }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors == null || listAccessors.Length == 0)
            {
                PropertyDescriptor[] props = new PropertyDescriptor[Columns.Count];
                for (int i = 0; i < props.Length; i++)
                {
                    props[i] = new PropertyBagPropertyDescriptor(Columns[i]);
                }
                return new PropertyDescriptorCollection(props, true);
            }
            throw new NotImplementedException("Relations not implemented");
        }

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Foo";
        }

    }
}
