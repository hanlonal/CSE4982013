﻿using System;
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
        private double BOMlowThreshold = 4;
        private bool BOMSortModeStatic = false;

        public bool BOMSortModeStatic1
        {
            get { return BOMSortModeStatic; }
            set { BOMSortModeStatic = value; }
        }

        public double BOMlowThreshold1
        {
            get { return BOMlowThreshold; }
            set { BOMlowThreshold = value; }
        }
        private double BOMhighThreshold = 7;

        public double BOMhighThreshold1
        {
            get { return BOMhighThreshold; }
            set { BOMhighThreshold = value; }
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
