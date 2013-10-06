/*
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
    public partial class ChooseClient : Form
    {
        BOMTool bomForm;
        public ChooseClient(BOMTool parentForm)
        {
            InitializeComponent();

            bomForm = parentForm;
            ChooseClientComboBox.Items.AddRange(bomForm.db.GetClientNames());
            this.Focus();
        }

        private void OpenBOMButton_Click(object sender, EventArgs e)
        {
            CLIENT client;
            string clientName = ChooseClientComboBox.Text.Trim();

            if (bomForm.db.GetClient(clientName, out client))
            {
                bomForm.client = client;

                string catName;
                string busName;
                string iniName;

                foreach (BOM bom in client.BOM)
                {
                    catName = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                    NewCategory category = bomForm.Categories.Find(delegate(NewCategory cat)
                                    {
                                        return cat.Name == catName;
                                    });
                    if (category == null)
                    {
                        category = new NewCategory(bomForm, catName);
                        bomForm.Categories.Add(category);
                        //bomForm.categoryCount++;
                        category.Click += new EventHandler(bomForm.category_Click);
                    }

                    busName = bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                    BusinessObjective objective = category.Objectives.Find(delegate(BusinessObjective bus)
                    {
                        return bus.Name == busName;
                    });
                    if (objective == null)
                    {
                        objective = category.AddObjective(busName);
                    }

                    iniName = bom.INITIATIVE.NAME.TrimEnd();
                    Initiative initiative = objective.Initiatives.Find(delegate(Initiative ini)
                                                                       {
                                                                           return ini.Name == iniName;
                                                                       });
                    if(initiative == null)
                    {
                        initiative = objective.AddInitiative(iniName);
                        initiative.Effectiveness = bom.EFFECTIVENESS.HasValue ? bom.EFFECTIVENESS.Value : 0;
                        initiative.Criticality = bom.CRITICALITY.HasValue ? bom.CRITICALITY.Value : 0;
                        initiative.Differentiation = bom.DIFFERENTIAL.HasValue ? bom.DIFFERENTIAL.Value : 0;
                    }
                }

                this.Close();
            }

            else
            {
                MessageBox.Show("Client \"" + ChooseClientComboBox.Text + "\" not found", "Error");
            }
        }

        private void NewBOMButton_Click(object sender, EventArgs e)
        {
            CLIENT client;
            string clientName = NewClientTextBox.Text.Trim();
            if (!bomForm.db.GetClient(clientName, out client))
            {
                client = new CLIENT();
                client.NAME = NewClientTextBox.Text;
                bomForm.db.AddClient(client);

                if (!bomForm.db.AddGroup("Business", client) || !bomForm.db.AddGroup("IT", client))
                {
                    MessageBox.Show("Cannot create groups for client", "Error");
                    return;
                }

                if (!bomForm.db.SaveChanges())
                {
                    MessageBox.Show("Could not create new Client", "Error");
                    bomForm.db = new DBManager();
                    return;
                }
                bomForm.client = client;

                this.Close();
            }
            else
            {
                MessageBox.Show("Client \"" + clientName + "\" already exists", "Error");
            }
        }

        private void ChooseClient_Load(object sender, EventArgs e)
        {

        }
    }
}
*/
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
    public partial class ChooseClient : Form
    {


        public void OpenBOMButton_Click(object sender, EventArgs e)
        {

        }

        public void ChooseClient_Load(object sender, EventArgs e)
        {

        }
        public void NewBOMButton_Click(object sender, EventArgs e)
        {

        }

    }
   
}