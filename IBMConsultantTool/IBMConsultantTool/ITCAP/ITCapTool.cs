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
                dom.ID = domCount.ToString();
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
                cap.ID = capCount.ToString();
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
                question.ID = questionCount.ToString();
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
            surverymakercontrols.Add(editQuestionTextbox);
            surverymakercontrols.Add(changeTextButton);

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
            // LoadQuestions();
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
                    MakePrioritizationGrid();
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

        private void MakePrioritizationGrid()
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

        }

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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
        }

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
                    ToolStripMenuItem editQuestionText = new ToolStripMenuItem();
                    editQuestionText.Click += new EventHandler(editQuestionText_Click);
                    deletecapability.Click += new EventHandler(deleteAttribute_Click);
                    deletecapability.Text = "Delete Attribute";
                    editQuestionText.Text = "Edit Question Text";
                    strip.Items.Add(deletecapability);
                    strip.Items.Add(editQuestionText);
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
                        entities.Remove(question);
                }
                if (entities.Contains(cap))
                    entities.Remove(cap);

            }
            entities.Remove(dom);
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
                }
            }
            cap.IsInGrid = false;
            surveryMakerGrid.Rows.RemoveAt(cap.IndexInGrid);
        }

        private void deleteAttribute_Click(object sender, EventArgs e)
        {
            ITCapQuestion question = surveryMakerGrid.SelectedRows[0].DataBoundItem as ITCapQuestion;

            ClientDataControl.db.RemoveITCAP(question.Name, ClientDataControl.Client.EntityObject);


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
            Random rand = new Random();

            foreach (ScoringEntity ent in entities)
            {
                if (ent.GetType() == typeof(ITCapQuestion))
                {
                    ITCapQuestion ques = (ITCapQuestion)ent;
                    ques.AddAsIsAnswer((float)rand.Next(4));
                    ques.AddAsIsAnswer((float)rand.Next(4));
                    ques.AddAsIsAnswer((float)rand.Next(4));
                    ques.AddAsIsAnswer((float)rand.Next(4));
                    ques.AddAsIsAnswer((float)rand.Next(4));
                    ques.AddAsIsAnswer((float)rand.Next(4));


                    ques.AddToBeAnswer((float)rand.Next(5) );
                    ques.AddToBeAnswer((float)rand.Next(5) );
                    ques.AddToBeAnswer((float)rand.Next(5));
                    ques.AddToBeAnswer((float)rand.Next(5));
                }
            }
        }

        private void GetClientObjectives(Capability cap)
        {
            objectiveMappingGrid.DataSource = null;
            coll.Clear();
            //Some kind of function like this is needed
            //ClientDataControl.db.GetClientObjectives();
            //DataGrid grid = new DataGrid();
            capabilityNameLabel.Visible = true;
            coll.Add(cap);
            
            coll.CalculatePropertyDescriptors();
            //currentcap.ObjectiveCollection.CalculatePropertyDescriptors();

            //objectiveMappingGrid.DataSource = coll;
            //objectiveMappingGrid.Columns[0].ReadOnly = true;
            BuildObjectiveMappingArea();
            //objectiveMappingGrid.RowHeadersVisible = false;



        }

        private void BuildObjectiveMappingArea()
        {
            Font font = new Font("Arial", 12, FontStyle.Underline | FontStyle.Bold);
            
            foreach (Control con in panel1.Controls)
            {
                if ((string)con.Tag != "permenant")
                {
                    panel1.Controls.Remove(con);
                }
            }
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
                ComboBox combo = new ComboBox();
                //combo.SelectedValueChanged +=new EventHandler(combo_SelectedValueChanged);
                
                //combo.DataBindings.Add("Text", val, "Value");
                combo.DataBindings.Add("SelectedIndex", val, "Value", true, DataSourceUpdateMode.OnPropertyChanged);
                combo.SelectedIndexChanged  +=new EventHandler(combo_SelectedIndexChanged);
                int x = 1;
                int y = 2;
                int z = 3;
                combo.Items.Add("0");
                combo.Items.Add("1");
                combo.Items.Add("2");
                combo.Items.Add("3");
                combo.Width = 50;
               
                //combo.ValueMemberChanged +=new EventHandler(combo_ValueMemberChanged);
                panel1.Controls.Add(combo);
               
                panel1.Controls.Add(label);
                width += label.Width;
                label.Location = new Point(width, capabilityNameLabel.Location.Y);
                combo.Location = new Point(label.Location.X, label.Location.Y + 50);
                
                count++;
            }

        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentcap.CalculatePrioritizedCapabilityGap();
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

                
            currentGrid.Refresh();
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            int value;
            string name;
            ClientDataControl.db.AddQuestionToITCAP(questionList.Text, capabilitiesList.Text, domainList.Text, this, out value, out name);
            if (value == 0)
            {

                ITCapQuestion ques = new ITCapQuestion();
                Capability cap = new Capability();
                Domain dom = new Domain();

                dom.Name = domainList.Text;
                dom.IsDefault = false;                
                dom.Type = "domain";
                //entities.Add(dom);
                //LoadCapabilities(dom);
                domains.Add(dom);
                dom.ID = domains.Count.ToString();

                cap.Name = capabilitiesList.Text;
                cap.IsDefault = false;
                dom.CapabilitiesOwned.Add(cap);
                dom.TotalChildren++;
                capabilities.Add(cap);
                cap.Owner = dom;
                cap.Type = "capability";
                cap.ID = capabilities.Count.ToString();
               // entities.Add(cap);


                ques.Name = questionList.Text;
                ques.IsDefault = false;
                cap.QuestionsOwned.Add(ques);
                ques.Type = "attribute";
                ques.Owner = cap;
                ques.ID = questionCount.ToString();

                entities.Add(dom);
                entities.Add(cap);
                entities.Add(ques);
            }
            LoadChartSurvey();
            surveryMakerGrid.Refresh();
        }

        private void SaveITCAPButton_Click(object sender, EventArgs e)
        {
            bool success = true;
            foreach (ITCapQuestion question in questionsArray)
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

            if (success && ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Saved Changes Successfully", "Success");
            }

            else
            {
                MessageBox.Show("Failed to save changes", "Error");
            }
        }

        private void changeDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeITCAPDefaults(this).ShowDialog();
        }

        private void prioritizationGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                row.Cells["AsisStandardDeviation"].Style.BackColor = Color.IndianRed;
                DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                cell.Value = Properties.Resources.exclamation;
            }
            else
            {
                //row.Cells["AsisStandardDeviation"].Style.BackColor = Color.LawnGreen;
                DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells["Flags"];
                cell.Style.NullValue = null;
                cell.Value = null;
            }
        }

        private void currentGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                ScoringEntity ent = row.DataBoundItem as ScoringEntity;
                

                if (ent.Type == "domain")
                {
                    row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    row.ReadOnly = true;
                    row.Cells["NumZeros"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["NumOnes"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["NumTwos"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["NumThrees"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["NumFours"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                    row.Cells["NumFives"].Style.ForeColor = row.DefaultCellStyle.BackColor;
                }
                else if (ent.Type == "capability")
                {
                    CheckBackColor(ent, row);
                    row.DefaultCellStyle.BackColor = Color.LightSlateGray;

                    row.ReadOnly = true;
                    row.Cells["NumOnes"].Style.ForeColor = row.Cells["NumOnes"].Style.BackColor;
                    row.Cells["NumTwos"].Style.ForeColor = row.Cells["NumTwos"].Style.BackColor;
                    row.Cells["NumThrees"].Style.ForeColor = row.Cells["NumThrees"].Style.BackColor;
                    row.Cells["NumFours"].Style.ForeColor = row.Cells["NumFours"].Style.BackColor;
                    row.Cells["NumFives"].Style.ForeColor = row.Cells["NumFives"].Style.BackColor;
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
                            row.Cells["AsisStandardDeviation"].Style.BackColor = Color.IndianRed;
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
                surveryMakerGrid.Columns["NumOnes"].Visible = false;
                surveryMakerGrid.Columns["NumTwos"].Visible = false;
                surveryMakerGrid.Columns["NumThrees"].Visible = false;
                surveryMakerGrid.Columns["NumFours"].Visible = false;
                surveryMakerGrid.Columns["NumFives"].Visible = false;
                surveryMakerGrid.Columns["NumZeros"].Visible = false;
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
                loadSurveyFromDataGrid.Columns["NumOnes"].HeaderText = "1s";
                loadSurveyFromDataGrid.Columns["NumOnes"].Width = 30;
                loadSurveyFromDataGrid.Columns["NumTwos"].HeaderText = "2s";
                loadSurveyFromDataGrid.Columns["NumTwos"].Width = 30;
                loadSurveyFromDataGrid.Columns["NumThrees"].HeaderText = "3s";
                loadSurveyFromDataGrid.Columns["NumThrees"].Width = 30;
                loadSurveyFromDataGrid.Columns["NumFours"].HeaderText = "4s";
                loadSurveyFromDataGrid.Columns["NumFours"].Width = 30;
                loadSurveyFromDataGrid.Columns["NumFives"].HeaderText = "5s";
                loadSurveyFromDataGrid.Columns["NumFives"].Width = 30;
                loadSurveyFromDataGrid.Columns["NumZeros"].HeaderText = "0s";
                loadSurveyFromDataGrid.Columns["NumZeros"].Width = 30;


                //loadSurveyFromDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            }
            currentGrid.Columns["Name"].Width = 400;
            currentGrid.Refresh();
        }

        private void currentGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ITCapQuestion ent = currentGrid.Rows[e.RowIndex].DataBoundItem as ITCapQuestion;
            ent.CalculateCapabilityGap();
            ent.Owner.CalculateAsIsAverage();
            if (e.ColumnIndex == 3)
            {
                ent.CalculateAsIsAverage();
            }
            else if (e.ColumnIndex == 4)
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

            CheckForeColor(ent, currentGrid.Rows[e.RowIndex]);
            CheckFlags(ent, currentGrid.Rows[e.RowIndex]);
            if (ent.HighStandardDeviation)
                currentGrid.Rows[e.RowIndex].Cells["AsisStandardDeviation"].Style.BackColor = Color.IndianRed;
            else if (!ent.HighStandardDeviation)
            {
                //ent.HighStandardDeviation = false;
                currentGrid.Rows[e.RowIndex].Cells["AsisStandardDeviation"].Style.BackColor = Color.White;
            }

            foreach(DataGridViewRow row in currentGrid.Rows)
            {
                
                ScoringEntity update = row.DataBoundItem as ScoringEntity;
                if(update.Type == "capability" || update.Type == "domain")
                    CheckBackColor(update, row);
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

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataLoader();
        }

        private void DataLoader()
        {
            foreach (ScoringEntity ent in entities)
            {

            }
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
            newChart.Titles["title"].Text = "System Agenda Capability Assessment Results";
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

            for (int i = 0; i < currentCount; i++)
            {
                newChart.Series["As_Is"].Points.AddXY(name[i], current[i]);
                currentTotal += current[i];
            }

            for (int i = 0; i < futureCount; i++)
            {
                newChart.Series["To_Be"].Points.AddXY(name[i], future[i]);
                futureTotal += future[i];
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

            for (int i = 0; i < currentCount; i++)
            {
                newChart.Series["As_Is"].Points.AddXY(name[i], current[i]);
                currentTotal += current[i];
            }

            for (int i = 0; i < futureCount; i++)
            {
                newChart.Series["To_Be"].Points.AddXY(name[i], future[i]);
                futureTotal += future[i];
            }

            newChart.Series["As_Is"].Points.AddXY("Total", currentTotal);
            newChart.Series["To_Be"].Points.AddXY("Total", futureTotal);

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
            List<string> capName = new List<string>();
            List<string> domName = new List<string>();
            List<int> capPerDom = new List<int>();
            List<bool> notAFocus = new List<bool>();
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
                    counting++;
                }
                capPerDom.Add(counting);
                domName.Add(dom.Name);
            }


            float gap = CalculateGap(capGap);
            float total = CalculateTotalGap(capGap, notAFocus);

            HeatMapChart chart = new HeatMapChart(domName, capName, capPerDom, capGap, gap, notAFocus, total, numberOfGap);
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
            /*var Powah = new PowerPointGenerator();

            Powah.CreatePowerPoint();*/
        }

        private void answersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                loadSurveyFromDataGrid.Columns["NumZeros"].Visible = !loadSurveyFromDataGrid.Columns["NumZeros"].Visible;
                loadSurveyFromDataGrid.Columns["NumOnes"].Visible = !loadSurveyFromDataGrid.Columns["NumOnes"].Visible;
                loadSurveyFromDataGrid.Columns["NumTwos"].Visible = !loadSurveyFromDataGrid.Columns["NumTwos"].Visible;
                loadSurveyFromDataGrid.Columns["NumThrees"].Visible = !loadSurveyFromDataGrid.Columns["NumThrees"].Visible;
                loadSurveyFromDataGrid.Columns["NumFours"].Visible = !loadSurveyFromDataGrid.Columns["NumFours"].Visible;
                loadSurveyFromDataGrid.Columns["NumFives"].Visible = !loadSurveyFromDataGrid.Columns["NumFives"].Visible;
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
                        addToDebate.Click += new EventHandler(addToDebate_Click);
                        addToDebate.Text = "Add to Discussion";
                        strip.Items.Add(addToDebate);
                        strip.Show(loadSurveyFromDataGrid, e.Location, ToolStripDropDownDirection.BelowRight);

                    }

                }
            }
        }

        private void addToDebate_Click(object sender, EventArgs e)
        {

        }

        private void asIsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        


    }// end class
}
