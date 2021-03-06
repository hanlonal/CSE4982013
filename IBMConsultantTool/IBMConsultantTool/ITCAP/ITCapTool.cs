﻿using System;
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
using System.Threading;

namespace IBMConsultantTool
{

    public partial class ITCapTool : Form
    {
        private ITCapQuestion activequestion;
        Capability currentcap = new Capability();
        MasterCollection coll = new MasterCollection();        

        public List<Domain> domains = new List<Domain>();
        public List<Capability> capabilities = new List<Capability>();
        //list of all the entities to be used. These are stored in order from how they appear in the grid
        public List<ScoringEntity> entities = new List<ScoringEntity>();
        public ITCapQuestion[] questionsArray = new ITCapQuestion[1024];
        //enum to determine which state the form is currently in
        enum FormStates { SurveryMaker, LiveDataEntry, Prioritization, Open, None };
        FormStates states;
        //different buttons to be used depending on the state of the tool
        private List<Control> surverymakercontrols = new List<Control>();
        private List<Control> liveDataEntryControls = new List<Control>();
        private List<Control> prioritizationControls = new List<Control>();
        private List<int> availablePriorityValues = new List<int>();
        private List<Control> loadFromSurveyControls = new List<Control>();
        DataGridView currentGrid;
        //creates loading screen 
        public delegate void UpdateUIDelegate(bool IsDataLoaded);
        private delegate void ObjectDelegate(object obj);
        private LoadingScreen loadingScreen;
        //the current entity that is selected in the grid view
        ScoringEntity currentEnt;

        //only used for testing
        int questionCount = 1;
        //Functions just used for testing until we have save and load

        string closeState = "close";
        //loads the default domains into the entities list
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
                //load all capabilities that it owns
                LoadCapabilities(dom);
                domains.Add(dom);

                domCount++;
            }
        }
        //loads the cpaabilities for the given domain paramenter
        private void LoadCapabilities(Domain dom)
        {
            string[] capabilityInfoArray = ClientDataControl.db.GetDefaultCapabilityNames(dom.Name);

            int capCount = 1;
            foreach (string capabilityInfo in capabilityInfoArray)
            {
                Capability cap = new Capability();
                cap.CapName = capabilityInfo;
                cap.IsDefault = true;
                Capability.AllCapabilities.Add(cap);
                dom.CapabilitiesOwned.Add(cap);
                dom.TotalChildren++;
                capabilities.Add(cap);
                cap.Owner = dom;
                cap.Type = "capability";
                cap.ID = ClientDataControl.db.GetScoringEntityID(cap.CapName);
                entities.Add(cap);
                //loads all questions that are owned by this capability
                LoadQuestions(cap);
                //panel1.MouseEnter += new EventHandler(panel1_MouseEnter);
                capCount++;
            }
        }
        //load all the questions ownes by the given paramter capability
        private void LoadQuestions(Capability cap)
        {
            string[] questionInfoArray = ClientDataControl.db.GetDefaultITCAPQuestionNames(cap.CapName, cap.Owner.Name);

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
            //opens to a blank screen and waits for user input
            states = FormStates.None;

            surverymakercontrols.Add(capabilityNameTextBox);

            ClearOptionsPanelControls("Start");

            availablePriorityValues.Add(0);
            availablePriorityValues.Add(1);
            availablePriorityValues.Add(2);
            availablePriorityValues.Add(3);

            surverymakercontrols.Add(questionNameTextBox);
            surverymakercontrols.Add(capabilitiesList);
            surverymakercontrols.Add(domainList);
            surverymakercontrols.Add(questionList);
            surverymakercontrols.Add(surveryMakerGrid);
            surverymakercontrols.Add(addEntityButton);
            surverymakercontrols.Add(deleteEntityButton);


            liveDataEntryControls.Add(liveDataEntryGrid);
            liveDataEntryControls.Add(LiveDataSaveITCAPButton);

            prioritizationControls.Add(prioritizationGrid);


            loadFromSurveyControls.Add(panel1);
            loadFromSurveyControls.Add(capabilityNameLabel);

            loadFromSurveyControls.Add(seperatorLabel);
            //loadSurveyFromDataGrid.Columns["Collapse"] = new DataGridViewDisableButtonColumn();
        }
        //decides if the form is in the closed state, or just switching tools
        private void ITCapTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (closeState == "close")
            {
                Capability.AllCapabilities.Clear();
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNTEST));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
        }

        private void ClearOptionsPanelControls(string tag)
        {
            foreach (Control con in optionsPanel.Controls)
            {
                if ((string)con.Tag != tag)
                {
                    con.Visible = false;
                }
                else
                    con.Visible = true;
            }
        }
        //UI deletegate to create loading screen
        private void UpdateUI(bool IsDataLoaded)
        {
            if (IsDataLoaded && this.loadingScreen != null)
            {
                loadingScreen.Close();
            }
            else
            {
                loadingScreen.Show();               
            }
        }
        // test form is the first form when the tool opens with the new, load, and trend anaylisys options
        private void RUNTEST()
        {
            Application.Run(new TestForm());
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

        private void CreateNewSurvey()
        {
            
                 // do we need to switch threads?
	            if (InvokeRequired)
	            {
	                // slightly different now, as we dont need params
	                // we can just use MethodInvoker
	                MethodInvoker method = new MethodInvoker(CreateNewSurvey);
	                Invoke(method);
	                return;
	            }
                //loadingScreen.Visible = false;
                if (MessageBox.Show("WARNING: Creating a new survey will overwrite the existing ITCAP Survey for this client. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    UpdateUI(false);
                    ResetSurveyGrid();
                    LoadDomains();
                    if (ClientDataControl.db.RewriteITCAP(this))
                    {
                        ChangeStates(FormStates.SurveryMaker);
                    }

                    UpdateUI(true);
                }
                else
                    loadingScreen.Visible = false;

            
        }

        private void DisplayLoadingScreen()
        {
            loadingScreen = new LoadingScreen(surveryMakerGrid.Location.X, surveryMakerGrid.Location.Y, this);
            UpdateUI(false);
        }
        //creates new survey
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

                loadingScreen = new LoadingScreen(surveryMakerGrid.Location.X, surveryMakerGrid.Location.Y, this);
                 
                Thread t = new Thread(new ThreadStart(CreateNewSurvey));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
               // CreateNewSurvey();
                UpdateUI(false);

        }
        //which state are we changing into? set the current grid based on that state, and hide controls
        // that are not currently being used.
        private void ChangeStates(FormStates stateToGoInto)
        {


            states = stateToGoInto;
            switch (states)
            {
                case FormStates.SurveryMaker:
                    currentGrid = surveryMakerGrid;
                    ClearBottomPanel();
                    LoadChartSurvey();
                    seperatorLabel.Text = "Survey Customization";
                    //Console.WriteLine("here"); 
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, false);
                    ToggleControlsVisible(loadFromSurveyControls, false);
                    loadSurveyFromDataGrid.Visible = false;
                    panel1.Visible = true;
                    seperatorLabel.Visible = true;
                    ClearOptionsPanelControls("SurveyMaker"); 
                    ToggleControlsVisible(surverymakercontrols, true);
                    ToggleControlsToFront(surverymakercontrols);
                    break;
                case FormStates.LiveDataEntry:
                    //probablly onlt used for testing
                    LoadChartSurvey();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, true);
                    ToggleControlsVisible(prioritizationControls, false);
                    ToggleControlsVisible(loadFromSurveyControls, false);
                    loadSurveyFromDataGrid.Visible = false;
                    break;
                case FormStates.Prioritization:
                    //MakePrioritizationGrid();
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, true);
                    ToggleControlsVisible(loadFromSurveyControls, false);
                    loadSurveyFromDataGrid.Visible = false;
                    break;
                case FormStates.Open:
                    seperatorLabel.Text = "Business Objective Mapping";
                    ToggleControlsVisible(surverymakercontrols, false);
                    ToggleControlsVisible(liveDataEntryControls, false);
                    ToggleControlsVisible(prioritizationControls, false);
                    loadSurveyFromDataGrid.Visible = true;
                    currentGrid = loadSurveyFromDataGrid;
                    LoadChartSurvey();

                    ToggleControlsVisible(loadFromSurveyControls, true);
                    break;
            }
            openSurveyButton.Visible = true;
            changeDefaultsButton.Visible = true;
            createQuestionairreButton.Visible = true;
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

        private void ToggleControlsVisible(List<Control> controls, bool value)
        {
            foreach (Control con in controls)
            {
                con.Visible = value;
            }
        }

        private void ToggleControlsToFront(List<Control> controls)
        {
            
            foreach (Control con in controls)
            {
                con.BringToFront();
            }
        }
        //sets the grid source to the entities list
        //this will triggle the databinding complete event
        private void LoadChartSurvey()
        {
            currentGrid.DataSource = null;
            currentGrid.DataSource = entities;
        }
        //check if user has right clicked on a cell, and then give options based on the cell type
        private void surveryMakerGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = surveryMakerGrid.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
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
                }
            }
        }
        //open the change button text
        private void editQuestionText_Click(object sender, EventArgs e)
        {
            ITCapQuestion question = surveryMakerGrid.SelectedRows[0].DataBoundItem as ITCapQuestion;
            editQuestionTextbox.Enabled = true;
            editQuestionTextbox.Text = question.Name;
            changeTextButton.Enabled = true;

        }

        private void addAttribute_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)surveryMakerGrid.Rows[0].Clone();

            
        }

        private void deleteDomain_Click(object sender, EventArgs e)
        {
            DeleteDomain();

        }
        // removes capability from datagrid and from the clients profile in the database
        private void DeleteCapability()
        {
            Capability cap = surveryMakerGrid.SelectedRows[0].DataBoundItem as Capability;

            if (cap.QuestionsOwned != null)
            {
                foreach (ITCapQuestion question in cap.QuestionsOwned)
                {
                     entities.Remove(question);
                     ClientDataControl.db.RemoveITCAP(question.Name, ClientDataControl.Client.EntityObject);                    
                }
            }

            entities.Remove(cap);
            ClientDataControl.db.RemoveITCAP(cap.Name, ClientDataControl.Client.EntityObject);

            Domain dom = cap.Owner;
            dom.CapabilitiesOwned.Remove(cap);
            if (dom.CapabilitiesOwned.Count == 0)
            {
                entities.Remove(dom);
                domains.Remove(dom);
            }
            LoadChartSurvey();
        }
        //deletes domain from datagrid as well as from the clients profile in the database
        private void DeleteDomain()
        {
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
        //deletes attribute from the datagrid as well as from the clients profile in the database
        private void DeleteAttribute()
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
        //delete capability button clicked
        private void deleteCapability_Click(object sender, EventArgs e)
        {
            DeleteCapability();
        }
        // delete attr button clicked
        private void deleteAttribute_Click(object sender, EventArgs e)
        {
            DeleteAttribute();
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
            ResetSurveyGrid();

            ClientDataControl.db.OpenITCAP(this);

            GetAnswers();
            ChangeStates(FormStates.Open);
            //GetClientObjectives();
            PopulateCapabilitiesWithObjectives();
        }
        //switch state to survey maker
        private void SurveryMaker_Click(object sender, EventArgs e)
        {
            ResetSurveyGrid();

            ClientDataControl.db.OpenITCAP(this);

            GetAnswers();
            ChangeStates(FormStates.SurveryMaker);
            //GetClientObjectives();
            PopulateCapabilitiesWithObjectives();
        }

        private void liveDataEntryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                DataGridViewComboBoxCell col = liveDataEntryGrid.Rows[e.RowIndex].Cells[4] as DataGridViewComboBoxCell;
            }
        }

        //open clients survey with the questions yoi have chose for them
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ResetSurveyGrid();
            ClientDataControl.db.OpenITCAP(this);
            
            GetAnswers();
            ChangeStates(FormStates.Open);
            //GetClientObjectives();
            PopulateCapabilitiesWithObjectives();
        }
        //receive a list of objectives the client made for BOM tool, and load them in now for prioritization
        private void PopulateCapabilitiesWithObjectives()
        {
            Dictionary<string, float> BOMS = ClientDataControl.db.GetObjectivesFromClientBOM(ClientDataControl.Client.EntityObject);

            foreach (KeyValuePair<string, float> bom in BOMS)
            {
                foreach (ScoringEntity ent in entities)
                {
                    if (ent.Type == "capability")
                    {
                        Capability cap = (Capability)ent;
                        cap.AddObjectiveToTrack(bom.Key, bom.Value);
                    }
                }
            }
        }
        //get answers from database
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
            capabilityNameLabel.Visible = true;
            BuildObjectiveMappingArea();
        }
        //clear objectives bottom panel when switching between capabilities
        //this is raised when a capability is clicked on to fill the panel with options
        // for changing the prioritization for each capability
        private void ClearBottomPanel()
        {
            panel1.Controls.Cast<Control>();
            foreach (Control con in panel1.Controls)
            {
                //Console.Write(con.Name + "\n");
                if ((string)con.Tag != "permenant" && con.Visible)
                {
                    con.DataBindings.Clear();
                    panel1.Controls.Remove(con);
                }
            }

            for (int i = panel1.Controls.Count -1; i >-1; i--)
            {
                Console.WriteLine(panel1.Controls[i].Name);
                if (panel1.Controls[i].Name.StartsWith("Testing"))
                {
                    panel1.Controls[i].DataBindings.Clear();
                    panel1.Controls.RemoveAt(i);
                }
            }
            

        }
        //this sets up all of the labels and combo boxes for the user too chahnge prioritization amounts
        private void BuildObjectiveMappingArea()
        {
            Font font = new Font("Arial", 12, FontStyle.Underline | FontStyle.Bold);

            ClearBottomPanel();
            Console.WriteLine("ObjectiveValue " + currentcap.PrioritizedCapabilityGap.ToString());
            int count = 1;
            Label nameLabel = new Label();
            //nameLabel.Font = font;
            nameLabel.Width = 150;
            nameLabel.AutoEllipsis = true;
            nameLabel.Text = currentcap.CapName;
            panel1.Controls.Add(nameLabel);
            nameLabel.Location = new Point(capabilityNameLabel.Location.X, capabilityNameLabel.Location.Y + 50);
            int width = 150;
            
            foreach (ObjectiveValues val in currentcap.ObjectiveCollection)
            {
                val.PropertyChanged +=new PropertyChangedEventHandler(val_PropertyChanged);
                Label label = new Label();
                label.Font = font;
                label.Width = 100;
                label.AutoEllipsis = true;
                label.Text = val.Name;
                ComboBox combo = new ComboBox();
                //combo.DataBindings.Clear();
                combo.Items.Add((int)0);
                combo.Items.Add((int)1);
                combo.Items.Add((int)2);
                combo.Items.Add((int)3);
                combo.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                combo.DataBindings.Add("SelectedItem", val, "Score");
                
                combo.Name = "Testing" + count.ToString();
                combo.Tag = "permenant";
                label.Name = "Testing" + count.ToString();
                label.Tag = "permenant";    
                

                combo.Width = 50;                

                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                panel1.Controls.Add(combo);                
                panel1.Controls.Add(label);
                width += label.Width;
                label.Location = new Point(width, capabilityNameLabel.Location.Y);
                combo.Location = new Point(label.Location.X, label.Location.Y + 50);
                
                count++;
            }
        }

        //triggers when a combo box is changed. updates the datagrid with new values
        private void val_PropertyChanged(object sender, EventArgs e)
        {
            currentcap.CalculatePrioritizedCapabilityGap();
            Capability.CalculatePrioritizedCapabilityGaps();
            foreach (DataGridViewRow row in loadSurveyFromDataGrid.Rows)
            {
                ScoringEntity ent = row.DataBoundItem as ScoringEntity;
                if (ent.GetType() == typeof(Capability))
                {
                    Capability cap = (Capability)ent;
                    //currentcap = loadSurveyFromDataGrid.SelectedRows[0].DataBoundItem as Capability;
                    if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.High)
                    {
                        row.Cells["PrioritizedGap"].Style.BackColor = Color.IndianRed;
                    }
                    else if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Middle)
                    {
                        row.Cells["PrioritizedGap"].Style.BackColor = Color.Yellow;
                    }
                    else if (cap.PrioritizedGapType1 == ScoringEntity.PrioritizedGapType.Low)
                    {
                        row.Cells["PrioritizedGap"].Style.BackColor = Color.LawnGreen;
                    }
                }
            }
            currentGrid.Refresh();
            currentGrid.Update();
        }

        
        private void combo_ControlRemoved(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            box.Visible = false;
            box.DataBindings.Clear();
        }
        //clear survey grid and entities list  of all values
        public void ResetSurveyGrid()
        {
            currentGrid.DataSource = null;
            domains.Clear();
            capabilities.Clear();
            entities.Clear();

        }
        //these functions control what is in the combo boxes in order to add entities to the datagrid
        //they pull values from the database depending on what the parent combo box has in it
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
            Capability.AllCapabilities.Add(cap);
            owner.TotalChildren++;
            owner.CapabilitiesOwned.Add(cap);
            cap.CapName = capabilitiesList.Text;
            cap.IsDefault = false;
            cap.Owner = owner;
            cap.Type = "capability";
            cap.ID = ClientDataControl.db.GetScoringEntityID(cap.CapName);
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

        //adds the entity based on the combo box values
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
        //check the UI backcolor of all the cells that need to be updated
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
        // same but for forecolor
        private void CheckForeColor(ScoringEntity ent, DataGridViewRow row)
        {
            if (ent.GapType1 == ScoringEntity.GapType.High)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.Red;
            else if (ent.GapType1 == ScoringEntity.GapType.Middle)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.Orange;
            else if (ent.GapType1 == ScoringEntity.GapType.Low)
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.DarkGreen;
            else
            {
                row.Cells["CapabilityGapText"].Style.BackColor = Color.LightGray;
                row.Cells["CapabilityGapText"].Style.ForeColor = Color.Black;
            }
        }
        //Check if there needs to be a yellow exclamation point for a flag based on the updated values
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
        //check if the backcolor of the standard deviation fields needto be updated
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
        //wrong data input in the datagrid triggers this event
        private void currentGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            e.Cancel = false;
            e.ThrowException = false;
        }


        //when setting a new entities list, this will get called after the source as updated all of its values.
        //this functions checks the current state, and does various grid manipulations on the rows, such as giding values, setting some to read only
        // and so on
        //it also sets back and forecolors depending on which type of entitity is bound to it
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
                surveryMakerGrid.ReadOnly = true;
                surveryMakerGrid.Columns["Name"].Width = 800;
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
                loadSurveyFromDataGrid.Columns["Name"].ReadOnly = true;


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
                currentGrid.Columns["Name"].Width = 400;
            }

            
            currentGrid.Refresh();
        }
        //if either the as is or to be answers are updated, make sure to update all of its entities that need to know about it
        private void currentGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            int value = Convert.ToInt32(currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            if(value < 0)
            {
                currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
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
        //this is for the button in the first colun of datagrid.
        //checks what syayes its currently in, and changes the collapsable text
        private void loadSurveyFromDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
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
        //opens cupe tool
        private void cUPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = "CUPE";
            Capability.AllCapabilities.Clear();
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
        //opens bom tool
        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = "BOM";
            Capability.AllCapabilities.Clear();
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
                    //Capability cap = (Capability)ent;
                    //cap.AddObjectiveToTrack(bom);
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
        //hitting the button in the toolstrip menu hides the columns for more visibility
        private void HideDeviations()
        {
            currentGrid.Columns["AsisStandardDeviation"].Visible = !currentGrid.Columns["AsisStandardDeviation"].Visible;
            currentGrid.Columns["TobeStandardDeviation"].Visible = !currentGrid.Columns["TobeStandardDeviation"].Visible;
        }
        //opens the systems agenda chart data
        private void systemsAgendaCapabilityAssesmentResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> capAsIs = new List<float>();
            List<float> capToBe = new List<float>();
            List<string> capName = new List<string>();

            foreach (Capability cap in capabilities)
            {
                capAsIs.Add(cap.AsIsScore);
                capToBe.Add(cap.ToBeScore);
                capName.Add(cap.CapName);
            }

            CreateChartAgenda(capName, capAsIs, capToBe);
        }

        public void CreateChartAgenda(List<string> name, List<float> current, List<float> future)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = "System Agenda Capability Assessment Results";
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

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
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

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }
        //opens chart data
        private void capabilityAssesmentSummaryScoresToolStripMenuItem_Click(object sender, EventArgs e)
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

            CreateChart(domName, domAsIs, domToBe);
        }
        //capability heatmap open
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
                    capName.Add(cap.CapName);

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

            HeatMapChart chart = new HeatMapChart(domName, capName, capPerDom, capGap, gap, notAFocus, total, numberOfGap, capGapType, "Capability Gap");
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


        //creates the survey document from the questions currently laid out. 
        // it begins sending commands to word in order to build the survey
        //word will open up shortly
        private void createSurveyDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyGenerator generator = new SurveyGenerator();
            List<ITCapQuestion> questionTempList = new List<ITCapQuestion>();
            foreach (ITCapQuestion question in questionsArray)
            {
                questionTempList.Add(question);
            }
            
            if(!generator.CreateITCapSurvey(entities))
            {
                MessageBox.Show("Word encountered an error. Please retry");
            }
        }

        private void openSurveyDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SurveyReader = new SurveyReader();

            SurveyReader.ReadSurveyITCap(entities);



        }
        //only partially implemented, left in for later use
        private void createPowerPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Powah = new PowerPointGenerator();

            Powah.ReplaceTemplatePowerpoint();
        }
        //hides as is and to be answers from data grid
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
            if (e.ColumnIndex == 1 && e.RowIndex >=0)
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
                        currentEnt = ent;
                        addToDebate.Click += new EventHandler(this.AddToDebate);
                        strip.Items.Add(addToDebate);
                        strip.Show(loadSurveyFromDataGrid, e.Location, ToolStripDropDownDirection.BelowRight);
                    }
                }
            }
        }

        private void AddToDebate(object sender, EventArgs e)
        {
            ClientDataControl.itcapQuestionsForDiscussion.Add(currentEnt);
        }

        private void asIsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentGrid == loadSurveyFromDataGrid)
            {
                HideAsIsAnswers();
            }
        }
        //this just hides as is answers
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
                    capName.Add(cap.CapName);

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

            HeatMapChart chart = new HeatMapChart(domName, capName, capPerDom, capGap, gap, notAFocus, total, numberOfGap, capGapType, "Prioritized Capability Gap");
            chart.Show();

            /*Bitmap bmp = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp.Save(Directory.GetCurrentDirectory() + @"/Charts/" + "Heat Map Prioritized Capability Gap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);*/
        }

        private void createCommentsDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyGenerator generator = new SurveyGenerator();
            List<ITCapQuestion> questionTempList = new List<ITCapQuestion>();
            foreach (ITCapQuestion question in questionsArray)
            {
                questionTempList.Add(question);
            }

            if (!generator.CreateCommentDoc(entities))
            {
                MessageBox.Show("Word encountered an error. Please retry");
            }
        
        }

        private void changeDefaultsButton_Click(object sender, EventArgs e)
        {
            new ChangeITCAPDefaults(this).ShowDialog();
        }

        private void createQuestionairreButton_Click(object sender, EventArgs e)
        {
            loadingScreen = new LoadingScreen(surveryMakerGrid.Location.X, surveryMakerGrid.Location.Y, this);

            Thread t = new Thread(new ThreadStart(CreateNewSurvey));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            // CreateNewSurvey();
            UpdateUI(false);
        }

        private void openSurveyButton_Click(object sender, EventArgs e)
        {
            ResetSurveyGrid();
            ClientDataControl.db.OpenITCAP(this);

            GetAnswers();
            ChangeStates(FormStates.Open);
            //GetClientObjectives();
            PopulateCapabilitiesWithObjectives();
        }

        private void deleteEntityButton_Click(object sender, EventArgs e)
        {
            if (surveryMakerGrid.SelectedRows.Count != 0)
            {
                if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(Domain))
                {
                    DeleteDomain();
                }
                else if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(Capability))
                {
                    DeleteCapability();
                }
                else if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(ITCapQuestion))
                {
                    DeleteAttribute();
                }
            }
            else
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Select a row before deleting.",
                    this, 740, 850, 8000);
            }
        }

        private void addEntittyButton_Click(object sender, EventArgs e)
        {
            if (surveryMakerGrid.SelectedRows.Count != 0)
            {
                if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(Domain))
                {

                }
                else if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(Capability))
                {

                }
                else if (surveryMakerGrid.SelectedRows[0].DataBoundItem.GetType() == typeof(ITCapQuestion))
                {

                }
            }
            else
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Select a row before adding.",
                    this, 120, 250, 8000);
            }

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(this);
            form.Show();
        }

    }// end class
}
