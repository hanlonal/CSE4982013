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
        public DataManager db;
        public bool isOnline;
        public object client;

        public List<Domain> domains = new List<Domain>();
        public List<Capability> capabilities = new List<Capability>();
        public List<ScoringEntity> entities = new List<ScoringEntity>();
        public ITCapQuestion[] questionsArray = new ITCapQuestion[1024];
        enum FormStates { SurveryMaker, LiveDataEntry, Prioritization, Open };
        FormStates states;
        private List<Control> surverymakercontrols = new List<Control>();
        private List<Control> liveDataEntryControls = new List<Control>();
        private List<Control> prioritizationControls = new List<Control>();

        //only used for testing
        private int numBoms = 3;

        //Functions just used for testing until we have save and load

        private void LoadDomains()
        {
            string[] domainInfoArray = db.GetDomainNamesAndDefault();
            int domCount = 1;
            foreach (string domainInfo in domainInfoArray)
            {
                Domain dom = new Domain();
                dom.Name = domainInfo.Substring(0, domainInfo.Length - 1);
                dom.IsDefault = domainInfo.Last() == 'Y';
                dom.ID = domCount.ToString();
                LoadCapabilities(dom);
                domains.Add(dom);
                entities.Add(dom);
                domCount++;
            }
        }

        private void LoadCapabilities(Domain dom)
        {
            string[] capabilityInfoArray = db.GetCapabilityNamesAndDefault(dom.Name);

            int capCount = 1;
            foreach (string capabilityInfo in capabilityInfoArray)
            {
                Capability cap = new Capability();
                cap.Name = capabilityInfo.Substring(0, capabilityInfo.Length - 1);
                cap.IsDefault = capabilityInfo.Last() == 'Y';
                dom.CapabilitiesOwned.Add(cap);
                dom.TotalChildren++;
                capabilities.Add(cap);
                cap.Owner = dom;
                cap.ID = capCount.ToString();
                LoadQuestions(cap);
                entities.Add(cap);
                capCount++;
            }
        }

        private void LoadQuestions(Capability cap)
        {
            string[] questionInfoArray = db.GetITCAPQuestionNamesAndDefault(cap.Name, cap.Owner.Name);

            int questionCount = 1;
            foreach (string questionInfo in questionInfoArray)
            {
                ITCapQuestion question = new ITCapQuestion();
                question.Name = questionInfo.Substring(0, questionInfo.Length - 1);
                question.IsDefault = questionInfo.Last() == 'Y';
                cap.Owner.TotalChildren++;
                cap.QuestionsOwned.Add(question);
                question.Owner = cap;
                question.ID = questionCount.ToString();
                entities.Add(question);
                questionCount++;
            }
        }

        public ITCapTool()
        {
            InitializeComponent();
            
            try
            {
                //---Force offline mode for testing---
                //throw new System.Exception();
                db = new DBManager();
                isOnline = true;
            }

            catch
            {
                db = new FileManager();
                isOnline = false;
                MessageBox.Show("Could not reach database: Offline mode set", "Error");
            }

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

            prioritizationControls.Add(prioritizationGrid);

            questionsArray.ToList();

            new ChooseITCAPClient(this).ShowDialog();
        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {
            if (client == null)
            {
                this.Close();
            }

            domainList.Items.AddRange(db.GetDomainNames());

            //LoadDomains();
            //LoadCapabilities();
           // LoadQuestions();
        }

        private void addDomainButton_Click(object sender, EventArgs e)
        {
            Domain dom = new Domain();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSurveyGrid();
            LoadDomains();
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
                    ToggleControlsVisible(prioritizationControls, false);
                    break;
                case FormStates.LiveDataEntry:
                    //probablly onlt used for testing
                    CopyGrid();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, true);
                    ToggleControlsVisible(prioritizationControls, false);
                    break;
                case FormStates.Prioritization:
                    MakePrioritizationGrid();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, true);
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
                rowCopy.Cells[6].Value = row.Cells[4].Value;
                
                //rowCopy.Cells[5]
                rowCopy.DefaultCellStyle.BackColor = row.DefaultCellStyle.BackColor;
                rowCopy.ReadOnly = row.ReadOnly;
                liveDataEntryGrid.Rows.Add(rowCopy);
            }
        }

        private void MakePrioritizationGrid()
        {
            for(int i=0;i <numBoms; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                
                prioritizationGrid.Columns.Add(col);
            }
            foreach (DataGridViewRow row in liveDataEntryGrid.Rows)
            {
                if ((string)row.Cells[6].Value == "domain" || (string)row.Cells[6].Value == "capability")
                {
                    DataGridViewRow rowCopy = (DataGridViewRow)prioritizationGrid.Rows[0].Clone();
                    rowCopy.Cells[0].Value = row.Cells[1].Value;
                    rowCopy.Cells[1].Value = row.Cells[2].Value;
                    rowCopy.Cells[2].Value = row.Cells[3].Value;
                    if ((string)row.Cells[6].Value == "domain")
                        rowCopy.DefaultCellStyle.BackColor = Color.Orange;
                    else
                        rowCopy.DefaultCellStyle.BackColor = Color.GhostWhite;

                    prioritizationGrid.Rows.Add(rowCopy);
                }
            }
            foreach (DataGridViewRow newrow in prioritizationGrid.Rows)
            {
                if(newrow.Cells[0].Value != null)
                    UpdateGapColumns(newrow.Index);
            }

        }

        private void UpdateGapColumns(int rowindex)
        {
            DataGridViewRow row = prioritizationGrid.Rows[rowindex];
            float gap = (float)Convert.ToDouble(row.Cells[2].Value.ToString()) - (float)Convert.ToDouble(row.Cells[1].Value.ToString());

            if (gap > 1.5 || gap < 3)
            {
                row.Cells[3].Style.BackColor = Color.Yellow;
                row.Cells[3].Value = "Medium Gap";
            }
            if (gap >= 3)
            {
                row.Cells[3].Style.BackColor = Color.IndianRed;
                row.Cells[3].Value = "Large Gap";
            }
            if (gap <= 1.5)
            {
                row.Cells[3].Style.BackColor = Color.LawnGreen;
                row.Cells[3].Value = "Small Gap";
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
            surveryMakerGrid.Rows.Clear();
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

        private void surveyMakerGrid_MouseDown(object sender, MouseEventArgs e)
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
                if (entity.IsInGrid && entity.IndexInGrid > e.RowIndex)
                {
                    entity.IndexInGrid--;

                    if (entity.GetType() == typeof(ITCapQuestion))
                    {
                        questionsArray[entity.IndexInGrid - 1] = questionsArray[entity.IndexInGrid];
                    }
                }
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

        private void surveryMakerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSurveyGrid();
            db.OpenITCAP(this);
            ChangeStates(FormStates.SurveryMaker);
        }

        public void ResetSurveyGrid()
        {
            domains.Clear();
            capabilities.Clear();
            entities.Clear();
            surveryMakerGrid.Rows.Clear();
        }

        private void domainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedDomain(this);
        }

        private void domainList_LostFocus(object sender, EventArgs e)
        {
            db.ChangedDomain(this);
        }

        private void capabilitiesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedCapability(this);
        }

        private void capabilitiesList_LostFocus(object sender, EventArgs e)
        {
            db.ChangedCapability(this);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            db.AddQuestionToITCAP(questionList.Text, capabilitiesList.Text, domainList.Text, this);
        }
    }// end class
}
