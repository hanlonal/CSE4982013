using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    public class ConfigurationSettings
    {
        private float standardDeviationThreshold = 1.00f;

        public float StandardDeviationThreshold
        {
            get { return standardDeviationThreshold; }
            set { standardDeviationThreshold = value; }
        }
        private static ConfigurationSettings instance = new ConfigurationSettings();
        private double staticHighGapThreshold = 3;

        public double StaticHighGapThreshold
        {
            get { return staticHighGapThreshold; }
            set { staticHighGapThreshold = value; }
        }
        private double staticLowGapThreshold = 1;

        public double StaticLowGapThreshold
        {
            get { return staticLowGapThreshold; }
            set { staticLowGapThreshold = value; }
        }


        bool staticSort = false;

        public bool StaticSort
        {
            get { return staticSort; }
            set { staticSort = value; }
        }

        private float dynamicAutoHighGap = 4;

        public float DynamicAutoHighGap
        {
            get { return dynamicAutoHighGap; }
            set { dynamicAutoHighGap = value; }
        }
        float dynamicAutoLowGap = .5f;

        public float DynamicAutoLowGap
        {
            get { return dynamicAutoLowGap; }
            set { dynamicAutoLowGap = value; }
        }

        private ConfigurationSettings() { }

        public static ConfigurationSettings Instance
        {
            get
            {
                if(instance == null)
                    instance = new ConfigurationSettings();

                return instance;
            }
        }


    }
}
