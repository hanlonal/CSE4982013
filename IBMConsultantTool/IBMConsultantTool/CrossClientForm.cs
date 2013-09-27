using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public partial class CrossClientForm : Form
    {
        SAMPLEEntities dbo;

        public CrossClientForm()
        {
            InitializeComponent();

            dbo = new SAMPLEEntities();

            InitiativeComboBox.Items.AddRange((from ini in dbo.INITIATIVE
                                             select ini.NAME).ToArray());
        }

        private void AnalyzeInitiativeButton_Click(object sender, EventArgs e)
        {
            INITIATIVE initiative;
            try
            {
                initiative = (from ini in dbo.INITIATIVE
                              where ini.NAME.TrimEnd() == InitiativeComboBox.Text
                              select ini).Single();
            }

            catch
            {
                MessageBox.Show("Initiative not found in database", "Error");
                return;
            }

            List<BOM> bomList = initiative.BOM.ToList();
            int count = 0;
            int effectiveness = 0;
            int criticality = 0;
            int differential = 0;
            foreach (BOM bom in bomList)
            {
                if (bom.EFFECTIVENESS.HasValue)
                {
                    effectiveness += (int)bom.EFFECTIVENESS;
                }
                if (bom.CRITICALITY.HasValue)
                {
                    effectiveness += (int)bom.CRITICALITY;
                }
                if (bom.DIFFERENTIAL.HasValue)
                {
                    differential += (int)bom.DIFFERENTIAL;
                }

                count++;
            }

            MessageBox.Show(effectiveness/count + "," + criticality/count + "," + differential/count);
        }
    }
}
