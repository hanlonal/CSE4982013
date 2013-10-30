// $Id:$

using System;
using System.Collections;

using System.ComponentModel;

namespace IBMConsultantTool
{
    public class MasterCollection : CollectionBase, ITypedList
    {
        // public methods...
        #region Add
        public int Add(Capability masterRecord)
        {
            return List.Add(masterRecord);
        }
        #endregion
        #region IndexOf
        public int IndexOf(Capability masterRecord)
        {
            for (int i = 0; i < List.Count; i++)
                if (this[i] == masterRecord)    // Found it
                    return i;
            return -1;
        }
        #endregion
        #region Insert
        public void Insert(int index, Capability masterRecord)
        {
            List.Insert(index, masterRecord);
        }
        #endregion
        #region Remove
        public void Remove(Capability masterRecord)
        {
            List.Remove(masterRecord);
        }
        #endregion
        #region Find
        // TODO: If desired, change parameters to Find method to search based on a property of Capability.
        public Capability Find(Capability masterRecord)
        {
            foreach (Capability lMasterRecord in this)
                if (lMasterRecord == masterRecord)    // Found it
                    return lMasterRecord;
            return null;    // Not found
        }
        #endregion
        #region Contains
        // TODO: If you changed the parameters to Find (above), change them here as well.
        public bool Contains(Capability masterRecord)
        {
            return (Find(masterRecord) != null);
        }
        #endregion

        // public properties...
        #region this[int aIndex]
        public Capability this[int index]
        {
            get
            {
                return (Capability)List[index];
            }
            set
            {
                List[index] = value;
            }
        }
        #endregion

        private static PropertyDescriptorCollection propertyDescriptors;

        public void CalculatePropertyDescriptors()
        {
            // Here we calculate the property descriptors that we actually need
            // for the data that's currently in the list. Of course this could
            // be easier if we didn't want things to be that dynamic.

            // start out with the properties that are really in the Capability
            PropertyDescriptorCollection origProperties = TypeDescriptor.GetProperties(typeof(Capability));

            ArrayList properties = new ArrayList();

            // Now we can filter on the origProperties, in case there are some we
            // really don't want at all.
            // For example, we filter out properties of a collection type because
            // having a column bound to these doesn't make any sense.
            foreach (PropertyDescriptor desc in origProperties)
            {
                if (desc.Name == "Name")
                    properties.Add(desc);
            }
                

            // Now we need to look at the DetailValues of all the current
            // MasterRecords and add a property descriptor for each unique
            // name of a detail value.

            ArrayList handledNames = new ArrayList();

            foreach (Capability mr in this)
                foreach (ObjectiveValues dv in mr.ObjectiveCollection)
                    if (!handledNames.Contains(dv.Name))
                    {
                        properties.Add(new ObjectiveValuePropertyDescriptor(dv.Name));
                        handledNames.Add(dv.Name);
                    }

            // Last, convert the ArrayList back into a PropertyDescriptorCollection.
            propertyDescriptors = new PropertyDescriptorCollection((PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor)));
        }

        #region ITypedList Members

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return propertyDescriptors;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Objectives";
        }

        #endregion
    }

}
