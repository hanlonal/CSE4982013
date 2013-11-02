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
        DBManager db;
        public CrossClientForm()
        {
            InitializeComponent();
        }

        private void AnalyzeInitiativeButton_Click(object sender, EventArgs e)
        {
            INITIATIVE initiative;
            if(!db.GetInitiative(InitiativeComboBox.Text, out initiative))
            {
                MessageBox.Show("Initiative not found in database", "Error");
                return;
            }

            List<BOM> bomList = initiative.BOM.ToList();
            int eCount = 0;
            int cCount = 0;
            int dCount = 0;
            float effectiveness = 0;
            float criticality = 0;
            float differential = 0;
            foreach (BOM bom in bomList)
            {
                if (bom.EFFECTIVENESS.HasValue)
                {
                    effectiveness += bom.EFFECTIVENESS.Value;
                    eCount++;
                }
                if (bom.CRITICALITY.HasValue)
                {
                    criticality += bom.CRITICALITY.Value;
                    cCount++;
                }
                if (bom.DIFFERENTIAL.HasValue)
                {
                    differential += bom.DIFFERENTIAL.Value;
                    dCount++;
                }
            }

            MessageBox.Show("Effectiveness: " + effectiveness/eCount + "\n" + 
                            "Criticality: " + criticality/cCount + "\n" + 
                            "Differential: " + differential/dCount);
        }

        private void CrossClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DBManager();
                InitiativeComboBox.Items.AddRange(db.GetInitiativeNames());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not reach database\n\n" + ex.Message + "\n\n" + "Cannot perform trend analysis", "Error");
                this.Close();
            }
        }
    }
}
