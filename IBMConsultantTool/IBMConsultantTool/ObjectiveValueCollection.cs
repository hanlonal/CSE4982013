using System;
using System.Collections;
using System.ComponentModel;

namespace IBMConsultantTool
{
    public class ObjectiveValueCollection : CollectionBase
    {
        // public methods...
        #region Add
        public int Add(ObjectiveValues objValue)
        {
            return List.Add(objValue);
        }
        #endregion
        #region IndexOf
        public int IndexOf(ObjectiveValues objValue)
        {
            for (int i = 0; i < List.Count; i++)
                if (this[i] == objValue)    // Found it
                    return i;
            return -1;
        }
        #endregion
        #region Insert
        public void Insert(int index, ObjectiveValues objValue)
        {
            List.Insert(index, objValue);
        }
        #endregion
        #region Remove
        public void Remove(ObjectiveValues objValue)
        {
            List.Remove(objValue);
        }
        #endregion
        #region Find
        // TODO: If desired, change parameters to Find method to search based on a property of ObjectiveValues.
        public ObjectiveValues Find(ObjectiveValues objValue)
        {
            foreach (ObjectiveValues lDetailValue in this)
                if (lDetailValue == objValue)    // Found it
                    return lDetailValue;
            return null;    // Not found
        }
        #endregion
        #region Contains
        // TODO: If you changed the parameters to Find (above), change them here as well.
        public bool Contains(ObjectiveValues objValue)
        {
            return (Find(objValue) != null);
        }
        #endregion

        // public properties...
        #region this[int aIndex]
        public ObjectiveValues this[int index]
        {
            get
            {
                return (ObjectiveValues)List[index];
            }
            set
            {
                List[index] = value;
            }
        }
        #endregion

      
    }

}
