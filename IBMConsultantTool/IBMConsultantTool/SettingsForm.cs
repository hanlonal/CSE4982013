﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public partial class SettingsForm : Form
    {
        Form owner;
        //This holds the data bindings for all of the objects in the form
        public SettingsForm(Form form)
        {
            
            this.owner = form;
            InitializeComponent();
            lowGapThresholdText.DataBindings.Add("Text", ConfigurationSettings.Instance, "StaticLowGapThreshold");
            highGapThresholdText.DataBindings.Add("Text", ConfigurationSettings.Instance, "StaticHighGapThreshold");
            dynamicAutoHighGapText.DataBindings.Add("Text", ConfigurationSettings.Instance, "DynamicAutoHighGap");
            dynamicAutoLowGaText.DataBindings.Add("Text", ConfigurationSettings.Instance, "DynamicAutoLowGap");
            staticSortRadio.DataBindings.Add("Checked", ConfigurationSettings.Instance, "StaticSort");
            bomhighTextbox.DataBindings.Add("Text", ConfigurationSettings.Instance, "BOMhighThreshold1");
            bomlowTextbox.DataBindings.Add("Text", ConfigurationSettings.Instance, "BOMlowThreshold1");
            staticSortRadio.CheckedChanged += new EventHandler(staticSortRadio_CheckedChanged);
            dynamicSortRadio.Checked = !staticSortRadio.Checked;
            CUPEHighThresholdBox.DataBindings.Add("Text", ConfigurationSettings.Instance, "CupeHigh");
            stdDeviationFlgAmount.DataBindings.Add("Text", ConfigurationSettings.Instance, "ITCapstdDevThreshold1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            owner.Update();
            owner.Refresh();
            this.Close();
        }

        
        private void staticSortRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (staticSortRadio.Checked == true)
            {
                ConfigurationSettings.Instance.StaticSort = true;
            }
          else
               ConfigurationSettings.Instance.StaticSort = false;
           Console.WriteLine(ConfigurationSettings.Instance.StaticSort);
       }


    }
}
