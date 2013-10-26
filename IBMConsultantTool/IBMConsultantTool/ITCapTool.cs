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

namespace IBMConsultantTool
{

    public partial class ITCapTool : Form
    {
        public DataManager db;
        public bool isOnline;
        public object client;
        private ITCapQuestion activequestion;



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

        //only used for testing
        private int numBoms = 3;

        //Functions just used for testing until we have save and load

        private void LoadDomains()
        {
            string[] domainInfoArray = db.GetDefaultDomainNames();
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
            string[] capabilityInfoArray = db.GetDefaultCapabilityNames(dom.Name);

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
            string[] questionInfoArray = db.GetDefaultITCAPQuestionNames(cap.Name, cap.Owner.Name);

            int questionCount = 1;
            foreach (string questionInfo in questionInfoArray)
            {
                ITCapQuestion question = new ITCapQuestion();
                question.Name = questionInfo;
                question.IsDefault = questionInfo.Last() == 'Y';
                question.comment = "";
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
            if (MessageBox.Show("WARNING: Creating a new survey will overwrite the existing ITCAP Survey for this client. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetSurveyGrid();
                LoadDomains();
                if (db.RewriteITCAP(this))
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
                    Console.WriteLine("here");
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
                    loadSurveyFromDataGrid.Visible = false;
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
                        if(question.comment != null) (qrow.Cells[4] as DataGridViewComboBoxCell).Items.Add(question.comment);
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
            foreach(Control con in controls)
            {
                con.Visible = value;
            }
        }

        private void LoadChartSurvey()
        {
            currentGrid.DataSource = entities;
            
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
                    editQuestionText.Click +=new EventHandler(editQuestionText_Click);
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
            Domain dom =  surveryMakerGrid.SelectedRows[0].DataBoundItem as Domain;

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
            surveryMakerGrid.Refresh();
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
            
            db.RemoveITCAP(question.Name, client);
            

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
            ChangeStates(FormStates.Open);
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
            LoadChartSurvey();
        }

        private void SaveITCAPButton_Click(object sender, EventArgs e)
        {
            bool success = true;
            foreach (ITCapQuestion question in questionsArray)
            {
                if (question != null)
                {
                    if (!db.UpdateITCAP(client, question))
                    {
                        success = false;
                        break;
                    }
                }
            }

            if (success && db.SaveChanges())
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

        private void currentGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                ScoringEntity ent = row.DataBoundItem as ScoringEntity;
                if (ent.Type == "domain")
                {
                    row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    row.ReadOnly = true;
                    

                }
                else if (ent.Type == "capability")
                {
                    row.DefaultCellStyle.BackColor = Color.DimGray;
                    row.ReadOnly = true;
                    if (states == FormStates.Open)
                        row.Visible = false;
                }
                else if (ent.Type == "attribute")
                {
                    if (states == FormStates.SurveryMaker)
                        row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    if (states == FormStates.Open)
                        row.Visible = false;
                }
                
            }
            if (states == FormStates.SurveryMaker)
            {
                surveryMakerGrid.Columns["AsIsScore"].Visible = false;
                surveryMakerGrid.Columns["tobeStandardDeviation"].Visible = false;
                surveryMakerGrid.Columns["asisStandardDeviation"].Visible = false;
                surveryMakerGrid.Columns["ToBeScore"].Visible = false;
                surveryMakerGrid.Columns["CapabilityGapText"].Visible = false;
                surveryMakerGrid.Columns["PrioritizedGap"].Visible = false;
                
            }
            if (states == FormStates.Open)
            {
                //db.GetBOMS();

            }
           currentGrid.Columns["Name"].Width = 450;
           currentGrid.Refresh();
        }

        private void currentGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ITCapQuestion ent = currentGrid.Rows[e.RowIndex].DataBoundItem as ITCapQuestion;
            ent.CalculateCapabilityGap();
            if (e.ColumnIndex == 2)
            {
                ent.Owner.CalculateAsIsAverage();                
            }
            if (e.ColumnIndex == 3)
            {
                ent.Owner.CalculateToBeAverage();                
            }
            if (ent.CapabilityGap >= 1)
                currentGrid.Rows[e.RowIndex].Cells["CapabilityGapText"].Style.BackColor = Color.IndianRed;
            currentGrid.Refresh();
            
            
        }

        private void loadSurveyFromDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
            foreach(ScoringEntity ent in entities)
            {
                if (ent.Type == "attribute")
                {

                }
            }
        }

        private void loadSurveyFromDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        
                
        }

        private void loadSurveyFromDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addCapabilityButton_Click(object sender, EventArgs e)
        {

        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: Creating a new survey will overwrite the existing ITCAP Survey for this client. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetSurveyGrid();
                LoadDomains();
                if (db.RewriteITCAP(this))
                {
                    ChangeStates(FormStates.SurveryMaker);
                }
            }
        }
    }// end class
}
