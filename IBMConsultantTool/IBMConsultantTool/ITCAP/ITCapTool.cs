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
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic.PowerPacks;

namespace IBMConsultantTool
{

    public partial class ITCapTool : Form
    {
        private ITCapQuestion activequestion;
        Capability currentcap = new Capability();
        MasterCollection coll = new MasterCollection();



        public List<Domain> domains = new List<Domain>();
        public List<Capability> capabilities = new List<Capability>();
        public List<ScoringEntity> entities = new List<ScoringEntity>();
        public ITCapQuestion[] questionsArray = new ITCapQuestion[1024];
        enum FormStates { SurveryMaker, LiveDataEntry, Prioritization, Open };
        FormStates states;
        private List<Control> surverymakercontrols = new List<Control>();
        private List<Control> liveDataEntryControls = new List<Control>();
        private List<Control> prioritizationControls = new List<Control>();
        DataGridView currentGrid;
        private Button button13322345;

        //only used for testing
        private int numBoms = 3;
        int questionCount = 1;
        //Functions just used for testing until we have save and load

        private void LoadDomains()
        {
            string[] domainInfoArray = ClientDataControl.db.GetDefaultDomainNames();
            int domCount = 1;
            foreach (string domainInfo in domainInfoArray)
            {
                Domain dom = new Domain();
                dom.Name = domainInfo;
                dom.IsDefault = true;
                dom.ID = ClientDataControl.db.GetScoringEntityID(dom.Name);
                dom.Type = "domain";
                entities.Add(dom);
                LoadCapabilities(dom);
                domains.Add(dom);

                domCount++;
            }
        }

        private void LoadCapabilities(Domain dom)
        {
            string[] capabilityInfoArray = ClientDataControl.db.GetDefaultCapabilityNames(dom.Name);

            int capCount = 1;
            foreach (string capabilityInfo in capabilityInfoArray)
            {
                Capability cap = new Capability();
                cap.Name = capabilityInfo;
                cap.IsDefault = true;
                dom.CapabilitiesOwned.Add(cap);
                dom.TotalChildren++;
                capabilities.Add(cap);
                cap.Owner = dom;
                cap.Type = "capability";
                cap.ID = ClientDataControl.db.GetScoringEntityID(cap.Name);
                entities.Add(cap);
                LoadQuestions(cap);

                capCount++;
            }
        }

        private void LoadQuestions(Capability cap)
        {
            string[] questionInfoArray = ClientDataControl.db.GetDefaultITCAPQuestionNames(cap.Name, cap.Owner.Name);

            questionCount = 1;
            foreach (string questionInfo in questionInfoArray)
            {
                ITCapQuestion question = new ITCapQuestion();
                question.Name = questionInfo;
                question.IsDefault = questionInfo.Last() == 'Y';
                question.comment = new List<string>();
                cap.Owner.TotalChildren++;
                cap.QuestionsOwned.Add(question);
                question.Owner = cap;
                question.Type = "attribute";
                question.ID = ClientDataControl.db.GetScoringEntityID(question.Name);
                entities.Add(question);
                questionCount++;
            }
        }

        public ITCapTool()
        {
            InitializeComponent();
            currentGrid = surveryMakerGrid;

            states = FormStates.Open;

            surverymakercontrols.Add(capabilityNameTextBox);


            surverymakercontrols.Add(questionNameTextBox);
            surverymakercontrols.Add(capabilitiesList);
            surverymakercontrols.Add(domainList);
            surverymakercontrols.Add(questionList);
            surverymakercontrols.Add(surveryMakerGrid);
            surverymakercontrols.Add(addEntityButton);
            //surverymakercontrols.Add(editQuestionTextbox);
            //surverymakercontrols.Add(changeTextButton);

            liveDataEntryControls.Add(liveDataEntryGrid);
            liveDataEntryControls.Add(LiveDataSaveITCAPButton);

            prioritizationControls.Add(prioritizationGrid);

            //loadSurveyFromDataGrid.Columns["Collapse"] = new DataGridViewDisableButtonColumn();
        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {
            // remove default [x] image for data DataGridViewImageColumn columns
            foreach (var column in loadSurveyFromDataGrid.Columns)
            {
                if (column is DataGridViewImageColumn)
                    (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
            }

            domainList.Items.AddRange(ClientDataControl.db.GetDomainNames());

            //LoadDomains();
            //LoadCapabilities();
            //LoadQuestions();
        }

        private void addDomainButton_Click(object sender, EventArgs e)
        {
            Domain dom = new Domain();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: Creating a new survey will overwrite the existing ITCAP Survey for this client. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetSurveyGrid();
                LoadDomains();
                if (ClientDataControl.db.RewriteITCAP(this))
                {
                    ChangeStates(FormStates.SurveryMaker);
                }
            }
        }

        private void ChangeStates(FormStates stateToGoInto)
        {
            states = stateToGoInto;
            switch (states)
            {
                case FormStates.SurveryMaker:
                    currentGrid = surveryMakerGrid;
                    LoadChartSurvey();
                    //Console.WriteLine("here");
                    ToggleControlsVisible(surverymakercontrols, true);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, false);
                    loadSurveyFromDataGrid.Visible = false;
                    break;
                case FormStates.LiveDataEntry:
                    //probablly onlt used for testing
                    LoadChartSurvey();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, true);
                    ToggleControlsVisible(prioritizationControls, false);
                    loadSurveyFromDataGrid.Visible = false;
                    break;
                case FormStates.Prioritization:
                    //MakePrioritizationGrid();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, true);
                    loadSurveyFromDataGrid.Visible = false;
                    break;
                case FormStates.Open:
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, false);
                    loadSurveyFromDataGrid.Visible = true;
                    currentGrid = loadSurveyFromDataGrid;
                    LoadChartSurvey();

                    loadSurveyFromDataGrid.Visible = true;
                    break;
            }
        }

        private void CopyGrid()
        {
            liveDataEntryGrid.Rows.Clear();
            foreach (Domain dom in domains)
            {
                DataGridViewRow row = (DataGridViewRow)liveDataEntryGrid.Rows[0].Clone();
                row.Cells[1].Value = dom.ToString();
                row.Cells[0].Value = dom.ID;
                row.Cells[6].Value = "domain";
                row.ReadOnly = true;
                row.DefaultCellStyle.BackColor = Color.Orange;
                liveDataEntryGrid.Rows.Add(row);
                dom.IndexInGrid = liveDataEntryGrid.Rows.Count - 2;
                Console.WriteLine(dom.Name + "index in list is " + dom.IndexInGrid.ToString());
                dom.IsInGrid = true;
                foreach (Capability cap in dom.CapabilitiesOwned)
                {
                    DataGridViewRow caprow = (DataGridViewRow)liveDataEntryGrid.Rows[0].Clone();
                    caprow.Cells[1].Value = cap.ToString();
                    caprow.Cells[0].Value = cap.ID;
                    caprow.Cells[6].Value = "capability";
                    caprow.ReadOnly = true;
                    caprow.DefaultCellStyle.BackColor = Color.Yellow;
                    liveDataEntryGrid.Rows.Add(caprow);
                    cap.IndexInGrid = liveDataEntryGrid.Rows.Count - 2;
                    cap.IsInGrid = true;

                    foreach (ITCapQuestion question in cap.QuestionsOwned)
                    {
                        DataGridViewRow qrow = (DataGridViewRow)liveDataEntryGrid.Rows[0].Clone();
                        qrow.Cells[1].Value = question.ToString();
                        qrow.Cells[0].Value = question.ID;
                        qrow.Cells[2].Value = question.AsIsScore.ToString();
                        qrow.Cells[3].Value = question.ToBeScore.ToString();
                        if (question.comment != null) (qrow.Cells[4] as DataGridViewComboBoxCell).Items.Add(question.comment);
                        qrow.ReadOnly = false;
                        qrow.DefaultCellStyle.BackColor = Color.LawnGreen;
                        liveDataEntryGrid.Rows.Add(qrow);
                        question.IndexInGrid = liveDataEntryGrid.Rows.Count - 2;
                        question.IsInGrid = true;
                        questionsArray[question.IndexInGrid] = question;
                    }
                    liveDataEntryGrid.Rows[cap.IndexInGrid].Cells[2].Value = cap.CalculateAsIsAverage();
                    liveDataEntryGrid.Rows[cap.IndexInGrid].Cells[3].Value = cap.CalculateToBeAverage();
                }
                liveDataEntryGrid.Rows[dom.IndexInGrid].Cells[2].Value = dom.CalculateAsIsAverage();
                liveDataEntryGrid.Rows[dom.IndexInGrid].Cells[3].Value = dom.CalculateToBeAverage();
            }
        }

     /*   private void MakePrioritizationGrid()
        {
            for (int i = 0; i < numBoms; i++)
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
                if (newrow.Cells[0].Value != null)
                    UpdateGapColumns(newrow.Index);
            }

        }*/

        private void UpdateGapColumns(int rowindex)
        {
            DataGridViewRow row = prioritizationGrid.Rows[rowindex];
            float asis = row.Cells[1].Value != null ? (float)Convert.ToDouble(row.Cells[1].Value.ToString()) : 0;
            float tobe = row.Cells[2].Value != null ? (float)Convert.ToDouble(row.Cells[2].Value.ToString()) : 0;
            float gap = tobe - asis;

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
            foreach (Control con in controls)
            {
                con.Visible = value;
            }
        }

        private void LoadChartSurvey()
        {
            currentGrid.DataSource = null;
            currentGrid.DataSource = entities;
        }
        //not used
        /*private void liveDataEntryGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                    (string)liveDataEntryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "5")
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
        }*/


        private void surveryMakerGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = surveryMakerGrid.HitTest(e.X, e.Y);
                //Console.WriteLine(hit.RowIndex.ToString());
                surveryMakerGrid.Rows[hit.RowIndex].Selected = true;
                ScoringEntity ent = surveryMakerGrid.SelectedRows[0].DataBoundItem as ScoringEntity;
                if (ent.Type == "domain")
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem deleteDomain = new ToolStripMenuItem();
                    deleteDomain.Click += new EventHandler(deleteDomain_Click);
                    deleteDomain.Text = "Delete Domain";
                    strip.Items.Add(deleteDomain);
                    strip.Show(surveryMakerGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                }
                if (ent.Type == "capability")
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem deletecapability = new ToolStripMenuItem();
                    deletecapability.Click += new EventHandler(deleteCapability_Click);
                    deletecapability.Text = "Delete Capability";
                    strip.Items.Add(deletecapability);
                    strip.Show(surveryMakerGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                }
                if (ent.Type == "attribute")
                {
                    activequestion = (ITCapQuestion)ent;
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem deletecapability = new ToolStripMenuItem();
                    //ToolStripMenuItem editQuestionText = new ToolStripMenuItem();
                    //editQuestionText.Click += new EventHandler(editQuestionText_Click);
                    deletecapability.Click += new EventHandler(deleteAttribute_Click);
                    deletecapability.Text = "Delete Attribute";
                    //editQuestionText.Text = "Edit Question Text";
                    strip.Items.Add(deletecapability);
                    //strip.Items.Add(editQuestionText);
                    strip.Show(surveryMakerGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                }


                Console.WriteLine(hit.ToString());
            }
        }

        private void editQuestionText_Click(object sender, EventArgs e)
        {
            ITCapQuestion question = surveryMakerGrid.SelectedRows[0].DataBoundItem as ITCapQuestion;
            editQuestionTextbox.Enabled = true;
            editQuestionTextbox.Text = question.Name;
            changeTextButton.Enabled = true;

        }

        private void deleteDomain_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Domain would be deleted");
            Domain dom = surveryMakerGrid.SelectedRows[0].DataBoundItem as Domain;

            foreach (Capability cap in dom.CapabilitiesOwned)
            {
                foreach (ITCapQuestion question in cap.QuestionsOwned)
                {
                    if (entities.Contains(question))
                    {
                        entities.Remove(question);
                        ClientDataControl.db.RemoveITCAP(question.Name, ClientDataControl.Client.EntityObject);
                    }
                }
                if (entities.Contains(cap))
                {
                    entities.Remove(cap);
                }

            }
            entities.Remove(dom);
            domains.Remove(dom);
            ClientDataControl.db.SaveChanges();
            LoadChartSurvey();
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
                    ClientDataControl.db.RemoveITCAP(question.Name, ClientDataControl.Client.EntityObject);
                }
            }
            cap.IsInGrid = false;
            surveryMakerGrid.Rows.RemoveAt(cap.IndexInGrid);

            Domain dom = cap.Owner;
            dom.CapabilitiesOwned.Remove(cap);
            if (dom.CapabilitiesOwned.Count == 0)
            {
                entities.Remove(dom);
                domains.Remove(dom);
            }
            LoadChartSurvey();
        }

        private void deleteAttribute_Click(object sender, EventArgs e)
        {
            ITCapQuestion question = surveryMakerGrid.SelectedRows[0].DataBoundItem as ITCapQuestion;
            entities.Remove(question);
            ClientDataControl.db.RemoveITCAP(question.Name, ClientDataControl.Client.EntityObject);

            Capability cap = question.Owner;
            cap.QuestionsOwned.Remove(question);
            if (cap.QuestionsOwned.Count == 0)
            {
                entities.Remove(cap);
                Domain dom = cap.Owner;
                dom.CapabilitiesOwned.Remove(cap);
                if (dom.CapabilitiesOwned.Count == 0)
                {
                    entities.Remove(dom);
                    domains.Remove(dom);
                }
            }

            LoadChartSurvey();
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
            //ChangeStates(FormStates.LiveDataEntry);
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
            //ChangeStates(FormStates.Prioritization);
        }

        private void surveryMakerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ResetSurveyGrid();

            ClientDataControl.db.OpenITCAP(this);
            
            GetAnswers();
            ChangeStates(FormStates.Open);
            //GetClientObjectives();
            PopulateCapabilitiesWithObjectives();

        }

        private void PopulateCapabilitiesWithObjectives()
        {
            string[] BOMS = ClientDataControl.db.GetObjectivesFromClientBOM(ClientDataControl.Client.EntityObject).ToArray();

            foreach (string bom in BOMS)
            {
                foreach (ScoringEntity ent in entities)
                {
                    if (ent.Type == "capability")
                    {
                        Capability cap = (Capability)ent;
                        cap.AddObjectiveToTrack(bom);
                    }
                }
            }
        }

        private void GetAnswers()
        {
            ITCapQuestion question;
            foreach (ScoringEntity ent in entities)
            {
                if (ent.GetType() == typeof(ITCapQuestion))
                {
                    question = ent as ITCapQuestion;
                    ClientDataControl.db.LoadITCAP(ref question);
                }
            }
        }

        private void GetClientObjectives(Capability cap)
        {
           // objectiveMappingGrid.DataSource = null;
            //coll.Clear();
            //Some kind of function like this is needed
            //ClientDataControl.db.GetClientObjectives();
            //DataGrid grid = new DataGrid();
            capabilityNameLabel.Visible = true;
            //coll.Add(cap);
            
            //coll.CalculatePropertyDescriptors();
            //currentcap.ObjectiveCollection.CalculatePropertyDescriptors();

            //objectiveMappingGrid.DataSource = coll;
            //objectiveMappingGrid.Columns[0].ReadOnly = true;
            BuildObjectiveMappingArea();
            //objectiveMappingGrid.RowHeadersVisible = false;
        }

        private void ClearBottomPanel()
        {
            foreach (Control con in panel1.Controls)
            {
                //Console.Write(con.Name + "\n");
                if ((string)con.Tag != "permenant")
                {
                    con.DataBindings.Clear();
                    panel1.Controls.Remove(con);
                }
            }

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                Console.WriteLine(panel1.Controls[i].Name + "\n");
                if (panel1.Controls[i].Name == "Testing")
                {
                    panel1.Controls[i].DataBindings.Clear();
                    panel1.Controls.RemoveAt(i);
                }
            }

        }

        private void BuildObjectiveMappingArea()
        {
            Font font = new Font("Arial", 12, FontStyle.Underline | FontStyle.Bold);

            ClearBottomPanel();

            int count = 1;
            Label nameLabel = new Label();
            //nameLabel.Font = font;
            nameLabel.Width = 150;
            nameLabel.AutoEllipsis = true;
            nameLabel.Text = currentcap.Name;
            panel1.Controls.Add(nameLabel);
            nameLabel.Location = new Point(capabilityNameLabel.Location.X, capabilityNameLabel.Location.Y + 50);
            int width = 150;
            foreach (ObjectiveValues val in currentcap.ObjectiveCollection)
            {
                Label label = new Label();
                label.Font = font;
                label.Width = 100;
                label.AutoEllipsis = true;
                label.Text = val.Name;
                TextBox combo = new TextBox();
                combo.Name = "Testing";
                //combo.SelectedValueChanged +=new EventHandler(combo_SelectedValueChanged);
                combo.DataBindings.Clear();
                //combo.DataBindings.Add("Text", val, "Value");
                combo.DataBindings.Add("Text", val, "Value");
               // combo.TextChanged +=new EventHandler(combo_TextChanged);
                combo.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                combo.LostFocus +=new EventHandler(combo_LostFocus);
                //combo.ValueMemberChanged +=new EventHandler(combo_ValueMemberChanged);
                panel1.Controls.Add(combo);
                combo.Tag = " ";
                panel1.Controls.Add(label);
                width += label.Width;
                label.Location = new Point(width, capabilityNameLabel.Location.Y);
                combo.Location = new Point(label.Location.X, label.Location.Y + 50);
                combo.ControlRemoved +=new ControlEventHandler(combo_ControlRemoved);
                count++;
            }
        }


        private void combo_LostFocus(object sender, EventArgs e)
        {
            currentcap.CalculatePrioritizedCapabilityGap();
            //currentcap = loadSurveyFromDataGrid.SelectedRows[0].DataBoundItem as Capability;
            if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.High)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.IndianRed;
            }
            else if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Middle)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.Yellow;
            }
            else if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Low)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.LawnGreen;
            }


            currentGrid.Refresh();
        }
        private void combo_TextChanged(object sender, EventArgs e)
        {
            
            
        }
        private void combo_ControlRemoved(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            box.Visible = false;
            box.DataBindings.Clear();
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
          /*  currentcap.CalculatePrioritizedCapabilityGap();
            currentcap = loadSurveyFromDataGrid.SelectedRows[0].DataBoundItem as Capability;
            if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.High)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.IndianRed;
            }
            else if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Middle)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.Yellow;
            }
            else if (currentcap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Low)
            {
                loadSurveyFromDataGrid.SelectedRows[0].Cells["PrioritizedGap"].Style.BackColor = Color.LawnGreen;
            }

                
            currentGrid.Refresh();*/
        }



        public void ResetSurveyGrid()
        {
            currentGrid.DataSource = null;
            domains.Clear();
            capabilities.Clear();
            entities.Clear();

        }

        private void domainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedDomain(this);
        }

        private void domainList_LostFocus(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedDomain(this);
        }

        private void capabilitiesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedCapability(this);
        }

        private void capabilitiesList_LostFocus(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedCapability(this);
        }

        private Domain CreateDomain()
        {
            Domain dom = new Domain();
            dom.Name = domainList.Text;
            dom.IsDefault = false;
            dom.Type = "domain";
            domains.Add(dom);
            dom.ID = ClientDataControl.db.GetScoringEntityID(dom.Name);
            return dom;
        }

        private Capability CreateCapability(Domain owner)
        {
            Capability cap = new Capability();
            owner.TotalChildren++;
            owner.CapabilitiesOwned.Add(cap);
            cap.Name = capabilitiesList.Text;
            cap.IsDefault = false;
            cap.Owner = owner;
            cap.Type = "capability";
            cap.ID = ClientDataControl.db.GetScoringEntityID(cap.Name);
            capabilities.Add(cap);
            return cap;
        }
        private ITCapQuestion CreateQuestion(Capability owner)
        {
            ITCapQuestion ques = new ITCapQuestion();
            owner.Owner.TotalChildren++;
            
            ques.Name = questionList.Text;
            ques.IsDefault = false;
            owner.QuestionsOwned.Add(ques);
            ques.Type = "attribute";
            ques.Owner = owner;
            ques.ID = ClientDataControl.db.GetScoringEntityID(ques.Name);
            return ques;
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            int value;
            string name;
            ClientDataControl.db.AddQuestionToITCAP(questionList.Text, capabilitiesList.Text, domainList.Text, this, out value, out name);
            if (value == 0)
            {
                Domain dom = CreateDomain();               
                Capability cap = CreateCapability(dom);
                ITCapQuestion ques = CreateQuestion(cap);

                entities.Add(dom);
                entities.Add(cap);
                entities.Add(ques);
            }
            if (value == 1)
            {
                
                Domain dom = (Domain)entities.Find(d => d.Name == domainList.Text);
                int index = entities.FindIndex(d => d.Name == domainList.Text);
                Capability cap = CreateCapability(dom);

                ITCapQuestion ques = CreateQuestion(cap);

                entities.Insert(index + dom.TotalChildren -1, cap);
                entities.Insert(index + dom.TotalChildren, ques);
            }
            if (value == 2)
            {
                int index = entities.FindIndex(d => d.Name == capabilitiesList.Text);
                Capability cap = (Capability)entities[index];

                ITCapQuestion ques = CreateQuestion(cap);

                entities.Insert(index + cap.QuestionsOwned.Count, ques);
            }

            LoadChartSurvey();
            surveryMakerGrid.Refresh();
        }

        private void changeDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeITCAPDefaults(this).ShowDialog();
        }

        private void CheckBackColor(ScoringEntity ent, DataGridViewRow row)
        {
            if (ent.GapType1 == ScoringEntity.GapType.High)
                row.Cells["CapabilityGapText"].Style.BackColor = Color.IndianRed;
            else if (ent.GapType1 == ScoringEntity.GapType.Middle)
                row.Cells["CapabilityGapText"].Style.BackColor = Color.Yellow;
            else if (ent.GapType1 == ScoringEntity.GapType.Low)
                row.Cells["CapabilityGapText"].Style.BackColor = Color.LawnGreen;
            else
                row.Cells["CapabilityGapText"].Style.BackColor = Color.LightGray;
        }
        private void CheckForeColor(ScoringEntity ent, DataGridViewRow row)
        {
            if (ent.GapType1 == ScoringEntity.GapType.High)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.Red;
            else if (ent.GapType1 == ScoringEntity.GapType.Middle)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.Orange;
            else if (ent.GapType1 == ScoringEntity.GapType.Low)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.DarkGreen;
            else
                row.Cells["CapabilityGapText"].Style.BackColor = Color.LightGray;
        }

        private void CheckFlags(ScoringEntity ent, DataGridViewRow row)
        {
            if (ent.Flagged)
            {
                
                DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                cell.Value = Properties.Resources.exclamation;
            }
            else
            {
                
                DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                cell.Style.NullValue = null;
                cell.Value = null;
            }
        }

        private void CheckStandardDeviations(ITCapQuestion ent, DataGridViewRow row)
        {
            if (ent.AsIsHighStandardDeviation)
                row.Cells["AsisStandardDeviation"].Style.BackColor = Color.IndianRed;
            else
                row.Cells["AsisStandardDeviation"].Style.BackColor = Color.White;

            if (ent.TobehighStandardDeviation)
                row.Cells["TobeStandardDeviation"].Style.BackColor = Color.IndianRed;
            else
                row.Cells["TobeStandardDeviation"].Style.BackColor = Color.White;                
        }



        private void currentGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            currentGrid.CurrentCell = null;
            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                ScoringEntity ent = row.DataBoundItem as ScoringEntity;                
               
                if (ent.Type == "domain")
                {
                    row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;

                    
                    row.ReadOnly = true;
                    row.Cells["AsIsNumZeros"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["AsIsNumOnes"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["AsIsNumTwos"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["AsIsNumThrees"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["AsIsNumFours"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["AsIsNumFives"].Style.ForeColor = row.DefaultCellStyle.BackColor;

                    row.Cells["TobeNumZeros"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["TobeNumOnes"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["TobeNumTwos"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["TobeNumThrees"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["TobeNumFours"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["TobeNumFives"].Style.ForeColor = row.DefaultCellStyle.BackColor;

                    row.DefaultCellStyle.SelectionBackColor = row.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor;
                }
                else if (ent.Type == "capability")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSlateGray;

                    CheckBackColor(ent, row);
                    

                    row.ReadOnly = true;
                    row.Cells["AsIsNumOnes"].Style.ForeColor = row.Cells["AsIsNumOnes"].Style.BackColor;
                    row.Cells["AsIsNumTwos"].Style.ForeColor = row.Cells["AsIsNumTwos"].Style.BackColor;
                    row.Cells["AsIsNumThrees"].Style.ForeColor = row.Cells["AsIsNumThrees"].Style.BackColor;
                    row.Cells["AsIsNumFours"].Style.ForeColor = row.Cells["AsIsNumFours"].Style.BackColor;
                    row.Cells["AsIsNumFives"].Style.ForeColor = row.Cells["AsIsNumFives"].Style.BackColor;

                    row.DefaultCellStyle.SelectionBackColor = row.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor;

                    if (states == FormStates.Open)
                        row.Visible = false;
                }
                else if (ent.Type == "attribute")
                {
                    ent.CalculateCapabilityGap();
                    
                    row.Cells["CapabilityGapText"].Style.BackColor = Color.LightGray;
                    CheckForeColor(ent, row);


                    if (states == FormStates.SurveryMaker)
                        row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    if (states == FormStates.Open)
                    {
                        row.Visible = false;
                        DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)row.Cells["Collapse"];
                        cell.Enabled = false;
                    }

                    if (currentGrid != surveryMakerGrid)
                    {
                        if (ent.Flagged)
                        {
                            //row.Cells["AsisStandardDeviation"].Style.BackColor = Color.IndianRed;
                            DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                            cell.Value = Properties.Resources.exclamation;
                        }
                        else
                        {
                            //row.Cells["AsisStandardDeviation"].Style.BackColor = Color.LawnGreen;
                            DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                            cell.Style.NullValue = null;
                        }
                    }
                    ITCapQuestion ques = (ITCapQuestion)ent;
                    CheckStandardDeviations(ques, row);
                }

            }
            if (states != FormStates.SurveryMaker)
            {
                foreach (DataGridViewRow row in currentGrid.Rows)
                {

                    ScoringEntity ent = row.DataBoundItem as ScoringEntity;
                    if (ent.Flagged)
                    {
                        row.Cells["Flags"].Value = Properties.Resources.exclamation;
                    }
                }
            }
            else
            {
                surveryMakerGrid.Columns["AsIsScore"].Visible = false;
                surveryMakerGrid.Columns["TobeStandardDeviation"].Visible = false;
                surveryMakerGrid.Columns["AsisStandardDeviation"].Visible = false;
                surveryMakerGrid.Columns["ToBeScore"].Visible = false;
                surveryMakerGrid.Columns["CapabilityGapText"].Visible = false;
                surveryMakerGrid.Columns["PrioritizedGap"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumZeros"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumOnes"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumTwos"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumThrees"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumFours"].Visible = false;
                surveryMakerGrid.Columns["AsIsNumFives"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumZeros"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumOnes"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumTwos"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumThrees"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumFours"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumFives"].Visible = false;
                surveryMakerGrid.Columns["ToBeNumZeros"].Visible = false;
                surveryMakerGrid.RowHeadersVisible = false;
                //surveryMakerGrid.Columns["Flags"].Visible = false;

            }
            if (states == FormStates.Open)
            {
                loadSurveyFromDataGrid.Columns["AsisStandardDeviation"].Width = 50;
                loadSurveyFromDataGrid.Columns["TobeStandardDeviation"].Width = 50;
                loadSurveyFromDataGrid.Columns["TobeStandardDeviation"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                loadSurveyFromDataGrid.Columns["AsisStandardDeviation"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                loadSurveyFromDataGrid.Columns["AsisStandardDeviation"].HeaderText = "As Is Std Dev";
                loadSurveyFromDataGrid.Columns["TobeStandardDeviation"].HeaderText = "To Be Std Dev";
                loadSurveyFromDataGrid.Columns["AsIsNumOnes"].HeaderText = "1s";
                loadSurveyFromDataGrid.Columns["AsIsNumOnes"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsNumTwos"].HeaderText = "2s";
                loadSurveyFromDataGrid.Columns["AsIsNumTwos"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsNumThrees"].HeaderText = "3s";
                loadSurveyFromDataGrid.Columns["AsIsNumThrees"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsNumFours"].HeaderText = "4s";
                loadSurveyFromDataGrid.Columns["AsIsNumFours"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsNumFives"].HeaderText = "5s";
                loadSurveyFromDataGrid.Columns["AsIsNumFives"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsNumFives"].DividerWidth = 3;
                loadSurveyFromDataGrid.Columns["AsIsNumZeros"].HeaderText = "0s";
                loadSurveyFromDataGrid.Columns["AsIsNumZeros"].Width = 30;
                loadSurveyFromDataGrid.Columns["AsIsScore"].HeaderText = "Current Score";
                loadSurveyFromDataGrid.Columns["ToBeScore"].HeaderText = "Future Score";


                loadSurveyFromDataGrid.Columns["ToBeNumOnes"].HeaderText = "1s";
                loadSurveyFromDataGrid.Columns["ToBeNumOnes"].Width = 30;
                loadSurveyFromDataGrid.Columns["ToBeNumTwos"].HeaderText = "2s";
                loadSurveyFromDataGrid.Columns["ToBeNumTwos"].Width = 30;
                loadSurveyFromDataGrid.Columns["ToBeNumThrees"].HeaderText = "3s";
                loadSurveyFromDataGrid.Columns["ToBeNumThrees"].Width = 30;
                loadSurveyFromDataGrid.Columns["ToBeNumFours"].HeaderText = "4s";
                loadSurveyFromDataGrid.Columns["ToBeNumFours"].Width = 30;
                loadSurveyFromDataGrid.Columns["ToBeNumFives"].HeaderText = "5s";
                loadSurveyFromDataGrid.Columns["ToBeNumFives"].Width = 30;
                loadSurveyFromDataGrid.Columns["ToBeNumFives"].DividerWidth = 3;
                loadSurveyFromDataGrid.Columns["ToBeNumZeros"].HeaderText = "0s";
                loadSurveyFromDataGrid.Columns["ToBeNumZeros"].Width = 30;


                loadSurveyFromDataGrid.Columns["AsIsScore"].ReadOnly = true;
                loadSurveyFromDataGrid.Columns["ToBeScore"].ReadOnly = true;
                loadSurveyFromDataGrid.Columns["AsisStandardDeviation"].ReadOnly = true;
                loadSurveyFromDataGrid.Columns["TobeStandardDeviation"].ReadOnly = true;
                loadSurveyFromDataGrid.Columns["CapabilityGapText"].ReadOnly = true;


                //loadSurveyFromDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            }
            if (states == FormStates.Open)
            {
                foreach (DataGridViewRow row in currentGrid.Rows)
                {

                    ScoringEntity update = row.DataBoundItem as ScoringEntity;
                    if (update.Type == "capability" || update.Type == "domain")
                    {
                        CheckBackColor(update, row);
                        CheckFlags(update, row);
                    }
                }
            }

            currentGrid.Columns["Name"].Width = 400;
            currentGrid.Refresh();
        }

        private void currentGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            ITCapQuestion ent = currentGrid.Rows[e.RowIndex].DataBoundItem as ITCapQuestion;


            if (e.ColumnIndex == 4)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 5)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 6)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 7)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 8)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 9)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 10)
            {
                ent.CalculateToBeAverage();
            }
            else if (e.ColumnIndex == 11)
            {
                ent.CalculateToBeAverage();
            }
            else if (e.ColumnIndex == 12)
            {
                ent.CalculateToBeAverage();
            }
            else if (e.ColumnIndex == 13)
            {
                ent.CalculateToBeAverage();
            }
            else if (e.ColumnIndex == 14)
            {
                ent.CalculateToBeAverage();
            }
            else if (e.ColumnIndex == 15)
            {
                ent.CalculateToBeAverage();
            }

            ent.CalculateCapabilityGap();
            ent.Owner.CalculateAsIsAverage();
            ent.Owner.CalculateToBeAverage();

            CheckForeColor(ent, currentGrid.Rows[e.RowIndex]);
            CheckFlags(ent, currentGrid.Rows[e.RowIndex]);
            CheckStandardDeviations(ent, currentGrid.Rows[e.RowIndex]);
            

            foreach (DataGridViewRow row in currentGrid.Rows)
            {

                ScoringEntity update = row.DataBoundItem as ScoringEntity;
                if (update.Type == "capability" || update.Type == "domain")
                {
                    CheckBackColor(update, row);
                    CheckFlags(update, row);
                }
            }

            currentGrid.Refresh();


        }

        private void loadSurveyFromDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
            if (e.ColumnIndex == 0)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)loadSurveyFromDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.UseColumnTextForButtonValue = false;
                if ((string)cell.Value == "-")
                    cell.Value = "+";
                else
                    cell.Value = "-";

                ScoringEntity ent = currentGrid.Rows[e.RowIndex].DataBoundItem as ScoringEntity;
                ent.ChangeChildrenVisibility();
                ChangeGridVisibility();
                return;
            }
            else if (e.ColumnIndex == 1)
            {



            }
            else if (e.RowIndex > 0)
            {
                ScoringEntity ent = currentGrid.Rows[e.RowIndex].DataBoundItem as ScoringEntity;
                if (ent.Type == "capability")
                {
                    Capability cap = (Capability)ent;
                    currentcap = cap;
                    GetClientObjectives(currentcap);
                }
                else
                {
                    ClearBottomPanel();
                }
            }
        }

        private void ChangeGridVisibility()
        {
            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                ScoringEntity ent = row.DataBoundItem as ScoringEntity;
                if (ent.Visible == true)
                    row.Visible = true;
                else
                    row.Visible = false;
            }
        }

        private void changeTextButton_Click(object sender, EventArgs e)
        {
            activequestion.Name = editQuestionTextbox.Text;
            editQuestionTextbox.Text = "";
            editQuestionTextbox.Enabled = false;
            changeTextButton.Enabled = false;
            surveryMakerGrid.Refresh();

        }


        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: Creating a new survey will overwrite the existing ITCAP Survey for this client. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetSurveyGrid();
                LoadDomains();
                if (ClientDataControl.db.RewriteITCAP(this))
                {
                    ChangeStates(FormStates.SurveryMaker);
                }
            }
        }

        private void cUPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNCUPE));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNCUPE()
        {
            Application.Run(new CUPETool());
        }

        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNBOM));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNBOM()
        {
            Application.Run(new BOMTool());
        }

        public void AddObjectiveToITCAP(string bom)
        {
            foreach (ScoringEntity ent in entities)
            {
                if (ent.Type == "capability")
                {
                    Capability cap = (Capability)ent;
                    cap.AddObjectiveToTrack(bom);
                }
            }
            GetClientObjectives(currentcap);
        }

        private void standardDeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                HideDeviations();
            }
        }
        private void HideDeviations()
        {
            currentGrid.Columns["AsisStandardDeviation"].Visible = !currentGrid.Columns["AsisStandardDeviation"].Visible;
            currentGrid.Columns["TobeStandardDeviation"].Visible = !currentGrid.Columns["TobeStandardDeviation"].Visible;
        }

        private void systemsAgendaCapabilityAssesmentResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> domAsIs = new List<float>();
            List<float> domToBe = new List<float>();
            List<string> domName = new List<string>();

            foreach (Domain dom in domains)
            {
                domAsIs.Add(dom.AsIsScore);
                domToBe.Add(dom.ToBeScore);

                domName.Add(dom.Name);
            }

            CreateChartAgenda(domName, domAsIs, domToBe);
        }

        public void CreateChartAgenda(List<string> name, List<float> current, List<float> future)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = "Capability Assessment Summary Score";
            newChart.Parent = formChart;

            int maxQuestion = 0;

            if (current.Count < future.Count)
            {
                maxQuestion = future.Count;
            }
            else
            {
                maxQuestion = current.Count;
            }

            newChart.Size = new Size(800, 750);
            newChart.Visible = true;
            newChart.Text = "System Agenda Capability Assessment Results";
            newChart.Name = newChart.Text;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisY.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisY.Title = "Average Capability Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisX.LabelStyle.Format = "#.##";

            /*newChart.ChartAreas["chart1"].AxisX.Title = "Question";
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;*/
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = "System Agenda Capability Assessment Results";
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("As_Is");
            newChart.Series["As_Is"].ChartArea = "chart1";
            newChart.Series["As_Is"].ChartType = SeriesChartType.Bar;
            newChart.Series["As_Is"].IsValueShownAsLabel = true;

            newChart.Series.Add("To_Be");
            newChart.Series["To_Be"].ChartArea = "chart1";
            newChart.Series["To_Be"].ChartType = SeriesChartType.Bar;
            newChart.Series["To_Be"].IsValueShownAsLabel = true;
            newChart.Series["To_Be"].IsVisibleInLegend = true;

            int currentCount = current.Count;
            int futureCount = future.Count;
            double cur;

            for (int i = 0; i < currentCount; i++)
            {
                cur = Convert.ToDouble(current[i]);
                decimal tmp = Convert.ToDecimal(cur);
                tmp = Math.Round(tmp, 2);
                cur = (double)tmp;

                newChart.Series["As_Is"].Points.AddXY(name[i], cur);
            }

            for (int i = 0; i < futureCount; i++)
            {
                cur = Convert.ToDouble(future[i]);
                decimal tmp = Convert.ToDecimal(cur);
                tmp = Math.Round(tmp, 2);
                cur = (double)tmp;

                newChart.Series["To_Be"].Points.AddXY(name[i], cur);
            }

            newChart.SaveImage(Application.StartupPath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }

        public void CreateChart(List<string> name, List<float> current, List<float> future)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = "Capability Assessment Summary Score";
            newChart.Parent = formChart;

            int maxQuestion = 0;

            if (current.Count < future.Count)
            {
                maxQuestion = future.Count;
            }
            else
            {
                maxQuestion = current.Count;
            }

            newChart.Size = new Size(800, 800);
            newChart.Visible = true;
            newChart.Text = "Capability Assessment Summary Score";
            newChart.Name = newChart.Text;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisY.MajorGrid.Enabled = false;
            /*newChart.ChartAreas["chart1"].AxisX.Title = "Question";
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;*/
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;
            //newChart.Legends["legend"].LegendStyle = LegendStyle.Table;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = "Capability Assessment Summary Score";
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("As_Is");
            newChart.Series["As_Is"].ChartArea = "chart1";
            newChart.Series["As_Is"].ChartType = SeriesChartType.Bar;
            newChart.Series["As_Is"].IsValueShownAsLabel = true;
            newChart.Series["As_Is"].IsVisibleInLegend = true;
            newChart.Series["As_Is"].YValueType = ChartValueType.Double;
            newChart.Series.Add("To_Be");
            newChart.Series["To_Be"].ChartArea = "chart1";
            newChart.Series["To_Be"].ChartType = SeriesChartType.Bar;
            newChart.Series["To_Be"].IsValueShownAsLabel = true;
            newChart.Series["To_Be"].IsVisibleInLegend = true;
            newChart.Series["To_Be"].YValueType = ChartValueType.Double;

            int currentCount = current.Count;
            int futureCount = future.Count;
            float currentTotal = 0;
            float futureTotal = 0;
            double temp;

            for (int i = 0; i < currentCount; i++)
            {
                temp = Convert.ToDouble(current[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                newChart.Series["As_Is"].Points.AddXY(name[i], temp);
                currentTotal += current[i];
            }

            for (int i = 0; i < futureCount; i++)
            {
                temp = Convert.ToDouble(future[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                newChart.Series["To_Be"].Points.AddXY(name[i], temp);
                futureTotal += future[i];
            }

            temp = Convert.ToDouble(currentTotal);
            decimal converter = Convert.ToDecimal(temp);
            converter = Math.Round(converter, 2);
            temp = (double)converter;

            newChart.Series["As_Is"].Points.AddXY("Total", temp);

            temp = Convert.ToDouble(futureTotal);
            converter = Convert.ToDecimal(temp);
            converter = Math.Round(converter, 2);
            temp = (double)converter;

            newChart.Series["To_Be"].Points.AddXY("Total", temp);

            newChart.SaveImage(Application.StartupPath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }

        private void capabilityAssesmentSummaryScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> capAsIs = new List<float>();
            List<float> capToBe = new List<float>();
            List<string> capName = new List<string>();

            foreach (Capability cap in capabilities)
            {
                capAsIs.Add(cap.AsIsScore);
                capToBe.Add(cap.ToBeScore);
                capName.Add(cap.Name);
            }

            CreateChart(capName, capAsIs, capToBe);
        }

        private void capabilityGapHeatmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> capAsIs = new List<float>();
            List<float> capToBe = new List<float>();
            List<float> capGap = new List<float>();
            List<float> entGap = new List<float>();
            List<string> capName = new List<string>();
            List<string> domName = new List<string>();
            List<int> capPerDom = new List<int>();
            List<bool> notAFocus = new List<bool>();
            List<int> entPerCap = new List<int>();
            List<string> capGapType = new List<string>();
            int counting = 0;

            int domCount = domains.Count;
            int capCount = capabilities.Count;



            foreach (Domain dom in domains)
            {
                counting = 0;
                foreach (Capability cap in dom.CapabilitiesOwned)
                {
                    
                    capAsIs.Add(cap.AsIsScore);
                    capToBe.Add(cap.ToBeScore);
                    if (cap.AsIsScore == 0 && cap.ToBeScore == 0)
                        notAFocus.Add(true);
                    else
                        notAFocus.Add(false);
                    capGap.Add(cap.ToBeScore - cap.AsIsScore);
                    capName.Add(cap.Name);

                    if (cap.GapType1 == ScoringEntity.GapType.High)
                        capGapType.Add("High");
                    else if (cap.GapType1 == ScoringEntity.GapType.Middle)
                        capGapType.Add("Middle");
                    else if (cap.GapType1 == ScoringEntity.GapType.Low)
                        capGapType.Add("Low");
                    else
                        capGapType.Add("None");
                    counting++;
                }
                capPerDom.Add(counting);
                domName.Add(dom.Name);
            }


            float gap = CalculateGap(capGap);
            float total = CalculateTotalGap(capGap, notAFocus);

            HeatMapChart chart = new HeatMapChart(domName, capName, capPerDom, capGap, gap, notAFocus, total, numberOfGap, capGapType);
            chart.Show();
        }

        public float CalculateGap(List<float> gap)
        {
            int count = gap.Count;
            float totalGap = 0;

            for (int cnt = 0; cnt < count; cnt++)
            {
                totalGap += gap[cnt];
            }

            float averageGap = totalGap / count;

            return averageGap;
        }

        int numberOfGap = 0;
        public float CalculateTotalGap(List<float> gap, List<bool> notFocus)
        {
            float totalGap = 0;

            for (int cnt = 0; cnt < gap.Count; cnt++)
            {
                if (!notFocus[cnt])
                {
                    totalGap += gap[cnt];
                    numberOfGap++;
                }
            }

            return totalGap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddObjectiveToITCAP(objectiveToAddButton.Text);
        }

        private void createSurveyDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyGenerator generator = new SurveyGenerator();
            List<ITCapQuestion> questionTempList = new List<ITCapQuestion>();
            foreach (ITCapQuestion question in questionsArray)
            {
                questionTempList.Add(question);
            }
            
            generator.CreateITCapSurvey(entities);
        }

        private void openSurveyDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SurveyReader = new SurveyReader();

            SurveyReader.ReadSurveyITCap(entities);



        }

        private void createPowerPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Powah = new PowerPointGenerator();

            Powah.ReplaceTemplatePowerpoint();
        }

        private void answersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                HideAsIsAnswers();
                HideToBeAnswers();
            }
        }

        private void loadSurveyFromDataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Console.WriteLine(e.RowIndex.ToString());
            if (e.ColumnIndex == 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView.HitTestInfo hit = loadSurveyFromDataGrid.HitTest(e.X, e.Y);
                    //Console.WriteLine(hit.RowIndex.ToString());
                    loadSurveyFromDataGrid.Rows[e.RowIndex].Selected = true;
                    ScoringEntity ent = loadSurveyFromDataGrid.SelectedRows[0].DataBoundItem as ScoringEntity;
                    if (ent.Type == "attribute")
                    {
                        ContextMenuStrip strip = new ContextMenuStrip();
                        ToolStripMenuItem addToDebate = new ToolStripMenuItem();
                        addToDebate.Text = "Add to Discussion";
                        strip.Items.Add(addToDebate);
                        strip.Show(loadSurveyFromDataGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                    }
                }
            }
        }

        private void asIsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                HideAsIsAnswers();
            }
        }

        private void HideAsIsAnswers()
        {
            loadSurveyFromDataGrid.Columns["AsIsNumZeros"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumZeros"].Visible;
            loadSurveyFromDataGrid.Columns["AsIsNumOnes"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumOnes"].Visible;
            loadSurveyFromDataGrid.Columns["AsIsNumTwos"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumTwos"].Visible;
            loadSurveyFromDataGrid.Columns["AsIsNumThrees"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumThrees"].Visible;
            loadSurveyFromDataGrid.Columns["AsIsNumFours"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumFours"].Visible;
            loadSurveyFromDataGrid.Columns["AsIsNumFives"].Visible = !loadSurveyFromDataGrid.Columns["AsIsNumFives"].Visible;
        }

        private void toBeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                HideToBeAnswers();
            }
        }

        private void HideToBeAnswers()
        {
            loadSurveyFromDataGrid.Columns["ToBeNumZeros"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumZeros"].Visible;
            loadSurveyFromDataGrid.Columns["ToBeNumOnes"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumOnes"].Visible;
            loadSurveyFromDataGrid.Columns["ToBeNumTwos"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumTwos"].Visible;
            loadSurveyFromDataGrid.Columns["ToBeNumThrees"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumThrees"].Visible;
            loadSurveyFromDataGrid.Columns["ToBeNumFours"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumFours"].Visible;
            loadSurveyFromDataGrid.Columns["ToBeNumFives"].Visible = !loadSurveyFromDataGrid.Columns["ToBeNumFives"].Visible;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool success = true;
            foreach (Domain domain in domains)
            {
                foreach (Capability capability in domain.CapabilitiesOwned)
                {
                    if (capability != null)
                    {
                        ClientDataControl.db.SaveCapabilityGapInfo(capability);
                    }

                    foreach (ITCapQuestion question in capability.QuestionsOwned)
                    {
                        if (question != null)
                        {
                            if (!ClientDataControl.db.UpdateITCAP(ClientDataControl.Client.EntityObject, question))
                            {
                                success = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (success && ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Saved Changes Successfully", "Success");
                ClientDataControl.db.ClientCompletedITCAP(ClientDataControl.Client.EntityObject);
            }

            else
            {
                MessageBox.Show("Failed to save changes", "Error");
            }
        }

        private void prioritizedCapabilityGapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> capAsIs = new List<float>();
            List<float> capToBe = new List<float>();
            List<float> capGap = new List<float>();
            List<float> entGap = new List<float>();
            List<string> capName = new List<string>();
            List<string> domName = new List<string>();
            List<int> capPerDom = new List<int>();
            List<bool> notAFocus = new List<bool>();
            List<int> entPerCap = new List<int>();
            List<string> capGapType = new List<string>();
            int counting = 0;

            int domCount = domains.Count;
            int capCount = capabilities.Count;



            foreach (Domain dom in domains)
            {
                counting = 0;
                foreach (Capability cap in dom.CapabilitiesOwned)
                {

                    capAsIs.Add(cap.AsIsScore);
                    capToBe.Add(cap.ToBeScore);
                    if (cap.AsIsScore == 0 && cap.ToBeScore == 0)
                        notAFocus.Add(true);
                    else
                        notAFocus.Add(false);
                    capGap.Add(cap.PrioritizedCapabilityGap);
                    capName.Add(cap.Name);

                    if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.High)
                        capGapType.Add("High");
                    else if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Middle)
                        capGapType.Add("Middle");
                    else if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Low)
                        capGapType.Add("Low");
                    else
                        capGapType.Add("None");
                    counting++;
                }
                capPerDom.Add(counting);
                domName.Add(dom.Name);
            }


            float gap = CalculateGap(capGap);
            float total = CalculateTotalGap(capGap, notAFocus);

            HeatMapChart chart = new HeatMapChart(domName, capName, capPerDom, capGap, gap, notAFocus, total, numberOfGap, capGapType);
            chart.Show();
        }

    }// end class
}
