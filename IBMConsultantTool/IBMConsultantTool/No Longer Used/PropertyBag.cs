using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class PropertyBag
    {
        private Dictionary<string, string> values = new Dictionary<string, string>();

        public string this[string key]
        {
            get
            {
                string value;
                values.TryGetValue(key, out value);
                return value;
            }
            set
            {
                if (value == null) values.Remove(key);
                else values[key] = value;
            }

        }
    }
}
