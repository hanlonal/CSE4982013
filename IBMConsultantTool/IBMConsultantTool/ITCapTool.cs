using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace IBMConsultantTool
{

    public partial class ITCapTool : Form
    {
        private List<Domain> domains = new List<Domain>();
        private List<Capability> capabilities = new List<Capability>();
        private List<ScoringEntity> entities = new List<ScoringEntity>();
        private ITCapQuestion[] questionsArray = new ITCapQuestion[1024];
        enum FormStates { SurveryMaker, LiveDataEntry, Prioritization, Open };
        FormStates states;
        private List<Control> surverymakercontrols = new List<Control>();
        private List<Control> liveDataEntryControls = new List<Control>();

        //Functions just used for testing until we have save and load

        private void LoadDomains()
        {
            for (int i = 0; i < 5; i++)
            {
                Domain dom = new Domain();
                dom.Name = "Domain " + i.ToString();
                if (i % 2 == 0)
                {
                    dom.IsDefault = true;
                }
                dom.ID = (i +1).ToString();
                LoadCapabilities(dom);
                domains.Add(dom);
                domainList.Items.Add(dom);
                entities.Add(dom);
            }
        }

        private void LoadCapabilities(Domain dom)
        {
            for (int i = 0; i < 3; i++)
            {
                Capability cap = new Capability();
                cap.Name = "Capability " + i.ToString() + " Owned By " + dom.ToString();
                if (i %2 == 0)
                {
                    cap.IsDefault = true;
                }                
                dom.CapabilitiesOwned.Add(cap);
                dom.TotalChildren++;
                capabilities.Add(cap);
                cap.Owner = dom;
                cap.ID = (i +1).ToString();
                LoadQuestions(cap);
                capabilitiesList.Items.Add(cap);
                entities.Add(cap);

            }
        }

        private void LoadQuestions(Capability cap)
        {
            for (int i = 0; i < 4; i++)
            {
                ITCapQuestion question = new ITCapQuestion();
                question.Name = "Question " + i.ToString() + " Owned By " + cap.ToString();
                if (i %2 == 0)
                {
                    question.IsDefault = true;
                }
                cap.Owner.TotalChildren++;
                cap.QuestionsOwned.Add(question);
                question.Owner = cap;
                question.ID = (i +1).ToString();
                questionList.Items.Add(question);
                entities.Add(question);
            }
        }

        public ITCapTool()
        {
            InitializeComponent();
            states = FormStates.Open;
            surverymakercontrols.Add(domainNameTextBox);
            surverymakercontrols.Add(capabilityNameTextBox);
            surverymakercontrols.Add(addCapabilityButton);
            surverymakercontrols.Add(addDomainButton);
            surverymakercontrols.Add(addQuestionButton);
            surverymakercontrols.Add(questionNameTextBox);
            surverymakercontrols.Add(capabilitiesList);
            surverymakercontrols.Add(domainList);
            surverymakercontrols.Add(questionList);
            surverymakercontrols.Add(surveryMakerGrid);

            liveDataEntryControls.Add(liveDataEntryGrid);

            


        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {
            LoadDomains();
            //LoadCapabilities();
           // LoadQuestions();
        }

        private void addDomainButton_Click(object sender, EventArgs e)
        {
            Domain dom = new Domain();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStates(FormStates.SurveryMaker);

        }

        private void ChangeStates(FormStates stateToGoInto)
        {
            states = stateToGoInto;
            switch (states)
            {
                case FormStates.SurveryMaker:
                    LoadDefaultChartSurvey();
                    ToggleControlsVisible(surverymakercontrols, true);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    break;
                case FormStates.LiveDataEntry:
                    //probablly onlt used for testing
                    CopyGrid();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, true);
                    break;
                case FormStates.Prioritization:
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    break;
            }
        }

        private void CopyGrid()
        {
            foreach (DataGridViewRow row in surveryMakerGrid.Rows)
            {
                DataGridViewRow rowCopy = (DataGridViewRow)liveDataEntryGrid.Rows[0].Clone();
                rowCopy.Cells[0].Value = row.Cells[0].Value;
                rowCopy.Cells[1].Value = row.Cells[1].Value;
                
                //rowCopy.Cells[5]
                rowCopy.DefaultCellStyle.BackColor = row.DefaultCellStyle.BackColor;
                rowCopy.ReadOnly = row.ReadOnly;
                liveDataEntryGrid.Rows.Add(rowCopy);
            }
        }

        private void ToggleControlsVisible(List<Control> controls, bool value)
        {
            foreach(Control con in controls)
            {
                con.Visible = value;
            }
        }

        private void LoadDefaultChartSurvey()
        {
            foreach (Domain dom in domains)
            {
                if (dom.IsDefault)
                {
                    Console.WriteLine("default");
                    DataGridViewRow row = (DataGridViewRow)surveryMakerGrid.Rows[0].Clone();
                    row.Cells[1].Value = dom.ToString();
                    row.Cells[0].Value = dom.ID;
                    row.Cells[4].Value = "domain";
                   // row.Cells[4].
                   // row.DefaultCellStyle.Font = new Font(surveryMakerGrid.Font.FontFamily,surveryMakerGrid.Font.Size,  row.FontStyle.Bold);
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    surveryMakerGrid.Rows.Add(row);
                    dom.IndexInGrid = surveryMakerGrid.Rows.Count -2;
                    Console.WriteLine(dom.Name + "index in list is " + dom.IndexInGrid.ToString());
                    dom.IsInGrid = true;
                    foreach (Capability cap in dom.CapabilitiesOwned)
                    {
                        if (cap.IsDefault)
                        {
                            DataGridViewRow caprow = (DataGridViewRow)surveryMakerGrid.Rows[0].Clone();
                            caprow.Cells[1].Value = cap.ToString();
                            caprow.Cells[0].Value = cap.ID;
                            caprow.Cells[4].Value = "capability";
                            caprow.ReadOnly = true;
                            caprow.DefaultCellStyle.BackColor = Color.Yellow;
                            surveryMakerGrid.Rows.Add(caprow);
                            cap.IndexInGrid = surveryMakerGrid.Rows.Count -2;
                            cap.IsInGrid = true;

                            foreach (ITCapQuestion question in cap.QuestionsOwned)
                            {
                                DataGridViewRow qrow = (DataGridViewRow)surveryMakerGrid.Rows[0].Clone();
                                qrow.Cells[1].Value = question.ToString();
                                qrow.Cells[0].Value = question.ID;
                                qrow.ReadOnly = false;
                                qrow.DefaultCellStyle.BackColor = Color.LawnGreen;
                                surveryMakerGrid.Rows.Add(qrow);
                                question.IndexInGrid = surveryMakerGrid.Rows.Count -2;
                                question.IsInGrid = true;
                                questionsArray[question.IndexInGrid] = question;
                            }
                        }

                    }
                    
                }
            }
        }

        private void liveDataEntryGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if ((string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "1" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "2" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "3" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "4" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "5")
                {
                    questionsArray[e.RowIndex].AsIsScore = (float)Convert.ToDouble((string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    float value = questionsArray[e.RowIndex].Owner.CalculateAsIsAverage();
                    liveDataEntryGrid.Rows[questionsArray[e.RowIndex].Owner.IndexInGrid].Cells[2].Value = value;
                    value = questionsArray[e.RowIndex].Owner.Owner.CalculateAsIsAverage();
                    liveDataEntryGrid.Rows[questionsArray[e.RowIndex].Owner.Owner.IndexInGrid].Cells[2].Value = value;
                }
                else
                {
                    liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                }
                
            }
            if (e.ColumnIndex == 3)
            {
                if ((string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "1" || 
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "2" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "3" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "4" ||
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "5" )

                {
                    questionsArray[e.RowIndex].ToBeScore = (float)Convert.ToDouble((string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    float value = questionsArray[e.RowIndex].Owner.CalculateToBeAverage();
                    liveDataEntryGrid.Rows[questionsArray[e.RowIndex].Owner.IndexInGrid].Cells[3].Value = value;
                    value = questionsArray[e.RowIndex].Owner.Owner.CalculateToBeAverage();
                    liveDataEntryGrid.Rows[questionsArray[e.RowIndex].Owner.Owner.IndexInGrid].Cells[3].Value = value;
                }
                else
                {
                    liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = surveryMakerGrid.HitTest(e.X, e.Y);
                    Console.WriteLine(hit.RowIndex.ToString());
                surveryMakerGrid.Rows[hit.RowIndex].Selected = true;
                if ((string)surveryMakerGrid.Rows[hit.RowIndex].Cells[4].Value == "domain")
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem deleteDomain = new ToolStripMenuItem();
                    deleteDomain.Click += new EventHandler(deleteDomain_Click);
                    deleteDomain.Text = "Delete Domain";
                    strip.Items.Add(deleteDomain);
                    strip.Show(surveryMakerGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                }
                if ((string)surveryMakerGrid.Rows[hit.RowIndex].Cells[4].Value == "capability")
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem deletecapability = new ToolStripMenuItem();
                    deletecapability.Click += new EventHandler(deleteCapability_Click);
                    deletecapability.Text = "Delete Capability";
                    strip.Items.Add(deletecapability);
                    strip.Show(surveryMakerGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                }
                
               
                Console.WriteLine(hit.ToString());
            }
        }

        private void deleteDomain_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Domain would be deleted");
            int index = surveryMakerGrid.SelectedRows[0].Index;
           // Console.WriteLine(index.ToString());
            Domain dom = FindDomainByIndex(index);
            //Console.WriteLine(dom.IndexInGrid.ToString() + " is index");
            foreach (Capability cap in dom.CapabilitiesOwned)
            {
                if (cap.IsInGrid)
                {
                    foreach (ITCapQuestion question in cap.QuestionsOwned)
                    {
                        if (question.IsInGrid)
                        {
                            question.IsInGrid = false;
                            surveryMakerGrid.Rows.RemoveAt(question.IndexInGrid);
                        }
                    }
                    cap.IsInGrid = false;
                    surveryMakerGrid.Rows.RemoveAt(cap.IndexInGrid);
                }
            }
            dom.IsInGrid = false;
            surveryMakerGrid.Rows.RemoveAt(dom.IndexInGrid);                        
        }

        private void deleteCapability_Click(object sender, EventArgs e)
        {
            int index = surveryMakerGrid.SelectedRows[0].Index;
            Capability cap = FindCapabilityByIndex(index);
            foreach (ITCapQuestion question in cap.QuestionsOwned)
            {
                if (question.IsInGrid)
                {
                    question.IsInGrid = false;
                    surveryMakerGrid.Rows.RemoveAt(question.IndexInGrid);
                }
            }
            cap.IsInGrid = false;
            surveryMakerGrid.Rows.RemoveAt(cap.IndexInGrid);
        }


        private Domain FindDomainByIndex(int index)
        {
            foreach (Domain dom in domains)
            {
                if (dom.IndexInGrid == index)
                {
                    return dom;
                }
            }
            return null;
        }
        private Capability FindCapabilityByIndex(int index)
        {
            foreach (Capability cap in capabilities)
            {
                if (cap.IndexInGrid == index)
                {
                    return cap;
                }
            }
            return null;
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            foreach (ScoringEntity entity in entities)
            {
                if (entity.IsInGrid && entity.IndexInGrid >= e.RowIndex)
                    entity.IndexInGrid--;
            }
        }

        private void LiveDataEntry_Click(object sender, EventArgs e)
        {
            ChangeStates(FormStates.LiveDataEntry);
        }

        private void SurveryMaker_Click(object sender, EventArgs e)
        {
            ChangeStates(FormStates.SurveryMaker);
        }

        private void liveDataEntryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                DataGridViewComboBoxCell col = liveDataEntryGrid.Rows[e.RowIndex].Cells[4] as DataGridViewComboBoxCell;

                
                
            }
        }

        private void Prioritization_Click(object sender, EventArgs e)
        {
            ChangeStates(FormStates.Prioritization);
        }

        private void capabilityGapHeatmapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }






    }// end class
}
