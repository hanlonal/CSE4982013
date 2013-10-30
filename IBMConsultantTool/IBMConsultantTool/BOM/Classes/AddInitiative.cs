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
    public partial class AddInitiative : Form
    {
        public MainForm mainForm;

        INITIATIVE selectedInitiative;
        BUSINESSOBJECTIVE selectedBusinessObjective;
        CATEGORY selectedCategory;

        public AddInitiative(Form parentForm)
        {
            InitializeComponent();

            mainForm = parentForm as MainForm;

            var categoryList = (from cat in mainForm.dbo.CATEGORY
                                select cat).ToList();

            foreach (CATEGORY category in categoryList)
            {
                CategoryComboBox.Items.Add(category.NAME);
            }
        }

        private void AddInitiativeToTableButton_Click(object sender, EventArgs e)
        {
            List<int> idList;
            var selectedInitiativeQuery = from ini in mainForm.dbo.INITIATIVE
                                          where ini.NAME == InitiativeComboBox.Text
                                          select ini;

            if (selectedInitiativeQuery.Count() == 0)
            {
                selectedInitiative = new INITIATIVE();
                selectedInitiative.NAME = InitiativeComboBox.Text;
                idList = (from ini in mainForm.dbo.INITIATIVE
                          select ini.INITIATIVEID).ToList();
                selectedInitiative.INITIATIVEID = mainForm.GetUniqueID(idList);
                var selectedBusinessObjectiveQuery = from bus in mainForm.dbo.BUSINESSOBJECTIVE
                                                     where bus.NAME == BusinessObjectiveComboBox.Text
                                                     select bus;

                if (selectedBusinessObjectiveQuery.Count() == 0)
                {
                    selectedBusinessObjective = new BUSINESSOBJECTIVE();
                    selectedBusinessObjective.NAME = BusinessObjectiveComboBox.Text;
                    idList = (from bus in mainForm.dbo.BUSINESSOBJECTIVE
                              select bus.BUSINESSOBJECTIVEID).ToList();
                    selectedBusinessObjective.BUSINESSOBJECTIVEID = mainForm.GetUniqueID(idList);
                    var selectedCategoryQuery = from cat in mainForm.dbo.CATEGORY
                                                where cat.NAME == CategoryComboBox.Text
                                                select cat;

                    if (selectedCategoryQuery.Count() == 0)
                    {
                        selectedCategory = new CATEGORY();
                        selectedCategory.NAME = CategoryComboBox.Text;
                        idList = (from cat in mainForm.dbo.CATEGORY
                                 select cat.CATEGORYID).ToList();
                        selectedCategory.CATEGORYID = mainForm.GetUniqueID(idList);
                        selectedCategory.BUSINESSOBJECTIVE.Add(selectedBusinessObjective);
                        mainForm.dbo.AddToCATEGORY(selectedCategory);
                    }

                    selectedBusinessObjective.CATEGORY = selectedCategory;
                    selectedBusinessObjective.INITIATIVE.Add(selectedInitiative);
                    mainForm.dbo.AddToBUSINESSOBJECTIVE(selectedBusinessObjective);
                }

                selectedInitiative.BUSINESSOBJECTIVE = selectedBusinessObjective;

                mainForm.dbo.AddToINITIATIVE(selectedInitiative);
            }

            BOM newBOM = new BOM();
            idList = (from bom in mainForm.dbo.BOM
                     select bom.BOMID).ToList();
            newBOM.BOMID = mainForm.GetUniqueID(idList);
            newBOM.CLIENT = mainForm.currentClient;
            newBOM.INITIATIVE = selectedInitiative;
            selectedInitiative.BOM.Add(newBOM);

            mainForm.dbo.SaveChanges();


            DataGridViewRow row = (DataGridViewRow)mainForm.BOMTable.Rows[0].Clone();
            row.Cells[0].Value = selectedCategory.NAME;
            row.Cells[1].Value = selectedBusinessObjective.NAME;
            row.Cells[2].Value = selectedInitiative.NAME;
            mainForm.BOMTable.Rows.Add(row);
            this.Close();
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedCategoryQuery = from cat in mainForm.dbo.CATEGORY
                                            where cat.NAME == CategoryComboBox.Text
                                            select cat;

                selectedCategory = selectedCategoryQuery.Single();
            }

            catch { }

            BusinessObjectiveComboBox.Items.Clear();
            BusinessObjectiveComboBox.Text = "";
            foreach (BUSINESSOBJECTIVE businessObjective in selectedCategory.BUSINESSOBJECTIVE)
            {
                BusinessObjectiveComboBox.Items.Add(businessObjective.NAME);
            }

            InitiativeComboBox.Items.Clear();
            InitiativeComboBox.Text = "";
        }

        private void BusinessObjectiveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedBOQuery = from bus in mainForm.dbo.BUSINESSOBJECTIVE
                                      where bus.NAME == BusinessObjectiveComboBox.Text
                                      select bus;


                selectedBusinessObjective = selectedBOQuery.Single();
            }

            catch { }

            InitiativeComboBox.Items.Clear();
            foreach (INITIATIVE initiative in selectedBusinessObjective.INITIATIVE)
            {
                InitiativeComboBox.Items.Add(initiative.NAME);
            }

            InitiativeComboBox.Text = "";
        }

        private void InitiativeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedInitiativeQuery = from ini in mainForm.dbo.INITIATIVE
                                              where ini.NAME == InitiativeComboBox.Text
                                              select ini;

                selectedInitiative = selectedInitiativeQuery.Single();
            }

            catch { }
        }
    }
}
