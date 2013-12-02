using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IBMConsultantTool
{
    public class RadioButtonNoTrueToFalseConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (targetType == typeof(System.Nullable<int>))
            {
                if (System.Convert.ToInt32(value) == 1)
                    return 0;
                else
                    return 1;
            }
            else if (targetType == typeof(System.Nullable<bool>))
            {
                if (System.Convert.ToBoolean(value) == true)
                    return false;
                else
                    return true;
            }

            return value;

        }  // end Convert ()

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (targetType == typeof(System.Nullable<int>))
            {
                if (System.Convert.ToInt32(value) == 1)
                    return 0;
                else
                    return 1;
            }
            else if (targetType == typeof(System.Nullable<bool>))
            {
                if (System.Convert.ToBoolean(value) == true)
                    return false;
                else
                    return true;
            }

            return value;

        }  // end ConvertBack ()

    }
}
