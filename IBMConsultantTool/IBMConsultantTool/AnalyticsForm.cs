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
    public partial class AnalyticsForm : Form
    {
        List<Control> comboBoxControls = new List<Control>();
        List<CapabilityTrendAnalysis> capabilitiesToTrack = new List<CapabilityTrendAnalysis>();
        List<CUPEQuestionTrendAnalysis> cupeToTrack = new List<CUPEQuestionTrendAnalysis>();
        List<ITAttributeTrendAnalysis> attributesToTrack = new List<ITAttributeTrendAnalysis>();
        List<InitiativeTrendAnalysis> initiativesToTrack = new List<InitiativeTrendAnalysis>();
        enum TrackingState { Capabilities, ITAttributes, Objectives, CUPEQuestions, Initiatives, None };
        TrackingState state;
        private DateTime fromTime;
        private DateTime toTime;
        private string currentlyBeingTracked = "";
        DBManager db = new DBManager();
        TextBox currentBox;
        public AnalyticsForm()
        {
            InitializeComponent();

            state = TrackingState.None;
            analyticsListBox.SelectedValueChanged +=new EventHandler(analyticsListBox_SelectedValueChanged);
        }

        private void AnalyticsForm_Load(object sender, EventArgs e)
        {
            comboBoxControls.Add(domainsComboBox);
            comboBoxControls.Add(capabilitiesComboBox);
            comboBoxControls.Add(cupeTimeFrameComboBox);
            comboBoxControls.Add(cupeAnswerTypeComboBox);
            comboBoxControls.Add(cupeQuestionsComboBox);
            cupeAnswerTypeComboBox.Items.Add("IT Professional");
            cupeAnswerTypeComboBox.Items.Add("Business Professional");
            cupeAnswerTypeComboBox.Items.Add("All");

            cupeTimeFrameComboBox.Items.Add("Current");
            cupeTimeFrameComboBox.Items.Add("Future");
            cupeTimeFrameComboBox.Items.Add("Both");
            List<string> names = new List<string>();
            names = db.GetRegionNames();

            regionComboBox.DataSource = names;

            names = db.GetBusinessTypeNames();

            businessTypeComboBox.DataSource = names;

            metricsComboBox.DataBindings.Add("Enabled", metricCheckBox, "Checked");
            regionComboBox.DataBindings.Add("Enabled", regionCheckBox, "Checked");
            businessTypeComboBox.DataBindings.Add("Enabled", typeCheckBox, "Checked");
            fromDateText.DataBindings.Add("Enabled", fromDateCheckBox, "Checked");
            toDateText.DataBindings.Add("Enabled", toDateCheckBox, "Checked");
            countryComboBox.DataBindings.Add("Enabled", countryCheckBox, "Checked");

            metricsComboBox.SelectedValueChanged +=new EventHandler(metricsComboBox_SelectedValueChanged);
            regionComboBox.SelectedValueChanged +=new EventHandler(regionComboBox_SelectedValueChanged);
            businessTypeComboBox.SelectedValueChanged +=new EventHandler(businessTypeComboBox_SelectedValueChanged);

            DataGridViewDisableButtonColumn cell = (DataGridViewDisableButtonColumn)trendGridView.Columns["Collapse"];
            cell.Visible = false;
            trendGridView.RowHeadersVisible = false;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ClearControls(string tag)
        {
            foreach (Control con in filterPanel.Controls)
            {
                if ((string)con.Tag == tag || con.Tag == "All")
                {
                    con.Visible = true;
                }
                else
                    con.Visible = false;
            }

        }

        private void ChangeState(TrackingState newState)
        {
            state = newState;
            metricsComboBox.Items.Clear();
            switch (state)
            {
                case TrackingState.Capabilities:
                    ClearControls("Capabilities");
                    metricsComboBox.Items.Add("All");
                    metricsComboBox.Items.Add("Capability Gap Type");
                    metricsComboBox.Items.Add("Prioritized Capability Gap Type");
                    metricsComboBox.Items.Add("Capability Gap Amount");
                    metricsComboBox.Items.Add("Prioritized Capability Gap Amount");
                    break;
                case TrackingState.CUPEQuestions:
                    metricsComboBox.Items.Add("All");
                    metricsComboBox.Items.Add("Business Future");
                    metricsComboBox.Items.Add("Business Current");
                    metricsComboBox.Items.Add("IT Future");
                    metricsComboBox.Items.Add("IT Current");
                    ClearControls("CUPE");
                    break;
                case TrackingState.Objectives:
                    ClearControls("Objectives");
                    metricsComboBox.Items.Add("Total Priority");
                    break;
                case TrackingState.ITAttributes:
                    ClearControls("Capabilities");
                    metricsComboBox.Items.Add("All");
                    metricsComboBox.Items.Add("Average As Is Score");
                    metricsComboBox.Items.Add("Average To Be Score");
                    break;
                case TrackingState.Initiatives:
                    ClearControls("Initiatives");
                    metricsComboBox.Items.Add("All");
                    metricsComboBox.Items.Add("Differentiation");
                    metricsComboBox.Items.Add("Criticality");
                    metricsComboBox.Items.Add("Effectiveness");
                    break;

            }

        }


        #region Event Handlers

        private void metricsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // TODO
        }

        private void regionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // TODO
        }
        private void businessTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //TODO
        }



        private void analyticsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)analyticsListBox.SelectedItem;
            
            List<string> names = new List<string>();


            if (value == "Objectives")
            {
                ChangeState(TrackingState.Objectives);
                List<BUSINESSOBJECTIVE> values = new List<BUSINESSOBJECTIVE>();
                values = db.GetObjectives().ToList();

                foreach (BUSINESSOBJECTIVE obj in values)
                {
                    names.Add(obj.NAME.Trim());
                }
                objectiveNamesComboBox.DataSource = names;
                objectiveNamesComboBox.Text = "<Objectives>";
                objectiveNamesComboBox.SelectedValueChanged += new EventHandler(objectiveNamesComboBox_SelectedValueChanged);
            }

            if (value == "Capabilities")
            {
                ChangeState(TrackingState.Capabilities);
                //ClearControls();
                names = db.GetDomainNames().ToList();
                domainsComboBox.DataSource = names;
                domainsComboBox.Text = "<Domains>";
                domainsComboBox.SelectedValueChanged += new EventHandler(domainsComboBox_SelectedValueChanged);

            }

            if (value == "CUPE Questions")
            {
                //ClearControls();
                ChangeState(TrackingState.CUPEQuestions);
                List<CupeQuestionStringData> stringData = new List<CupeQuestionStringData>();
                
                stringData = db.GetCUPEQuestionStringData();
                foreach (CupeQuestionStringData x in stringData)
                {
                    names.Add(x.OriginalQuestionText);
                }

                cupeQuestionsComboBox.DataSource = names;
                cupeQuestionsComboBox.Text = "<CUPE Questions";
            }
            if (value == "IT Attribues")
            {
                ChangeState(TrackingState.ITAttributes);
                names = db.GetDomainNames().ToList();
                domainsComboBox.DataSource = names;
                domainsComboBox.Text = "<Domains>";
                domainsComboBox.SelectedValueChanged += new EventHandler(domainsComboBox_SelectedValueChanged);
            }
            if (value == "Initiatives")
            {
                ChangeState(TrackingState.Initiatives);
                names = db.GetInitiativeNames().ToList();
                initiativesComboBox.DataSource = names;
            }

        }

        private void objectiveNamesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)cupeQuestionsComboBox.SelectedText;
        }

        private void capabilitiesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            List<string> attributes = new List<string>();
            attributes = db.GetITCAPQuestionNames(capabilitiesComboBox.Text, domainsComboBox.Text).ToList();
            itAttributesComboBox.DataSource = attributes;
            itAttributesComboBox.Text = "<Attributes>";
        
        }

        private void domainsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            List<String> names = new List<string>();
            
            names = db.GetCapabilitiesFromDomain(domainsComboBox.Text);
            capabilitiesComboBox.DataSource = names;
            capabilitiesComboBox.Text = "<Capabilities>";
            if (state == TrackingState.ITAttributes)
            {
                capabilitiesComboBox.SelectedValueChanged += new EventHandler(capabilitiesComboBox_SelectedValueChanged);
                itAttributesComboBox.Enabled = true;
            }
            else
                itAttributesComboBox.Enabled = false;
        }

        private void date_DateSelected(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = (MonthCalendar)sender;
            currentBox.Text = e.Start.Date.ToShortDateString();
            //selectedTime = e.Start.Date;
            cal.Visible = false;
            if (currentBox == toDateText)
            {
                toTime = e.Start.Date;
                if (fromTime == null)
                    return;
                else
                    if (fromTime > toTime)
                    {
                        MessageBox.Show("From data cannot be larger than to date");
                        toDateText.Clear();
                        fromDateText.Clear();

                    }
            }
            else if (currentBox == fromDateText)
            {
                fromTime = e.Start.Date;
            }
        }

        # endregion

        private void DateText_Click(object sender, EventArgs e)
        {
            
           
            TextBox box = (TextBox)sender;
            currentBox = box;
            MonthCalendar date = new MonthCalendar();
            filterPanel.Controls.Add(date);
            date.DateSelected += new DateRangeEventHandler(date_DateSelected);
            date.Visible = true;
            date.Location = new Point(currentBox.Location.X, currentBox.Location.Y - 50);
            date.BringToFront();
        }

        private void CreateCapabilityToTrack(string region, string busi, string from, string to)
        {

            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Capability")
            {
                List<CapabilityTrendAnalysis> capabilities = new List<CapabilityTrendAnalysis>();
                capabilities = db.GetCapabilityTrendAnalysis(capabilitiesComboBox.Text, region, busi, from, to);
                CapabilityTrendAnalysis ent = new CapabilityTrendAnalysis();
                if (capabilities.Count > 0)
                {
                    float gap = capabilities.Average(d => d.CapabilityGap);
                    float prior = capabilities.Average(d => d.PrioritizedCapabilityGap);

                    ent.CapabilityGap = gap;
                    ent.PrioritizedCapabilityGap = prior;
                    ent.GapType = "---";
                    ent.PrioritizedGapType = "---";
                    ent.Type1 = TrendAnalysisEntity.Type.Master;
                    capabilitiesToTrack.Add(ent);
                }
                else
                    MessageBox.Show("Query returned no results");

                foreach (CapabilityTrendAnalysis cap in capabilitiesToTrack)
                {
                    ent.Children++;
                    cap.Type1 = TrendAnalysisEntity.Type.Child;
                    capabilitiesToTrack.Add(cap);
                }

                trendGridView.DataSource = null;
                trendGridView.DataSource = capabilitiesToTrack ;
                trendGridView.Refresh();
                currentlyBeingTracked = "Capability";
                trendGridView.Columns["Collapse"].Visible = true;
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void CreateCUPEQuestionToTrack(string region, string busi, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "CUPE")
            {
                List<CUPEQuestionTrendAnalysis> cupes = new List<CUPEQuestionTrendAnalysis>();

                cupes = db.GetCUPEQuestionTrendAnalysis(cupeQuestionsComboBox.Text, region, busi, from, to);
                if (cupes.Count < 0)
                {
                    float asIsAaverage = cupes.Average(d => d.CurrentAnswer);
                    float futureAnswer = cupes.Average(d => d.FutureAnswer);

                    CUPEQuestionTrendAnalysis track = new CUPEQuestionTrendAnalysis();
                    track.CurrentAnswer = asIsAaverage;
                    track.FutureAnswer = futureAnswer;
                    track.Name = cupeQuestionsComboBox.Text;
                    track.BusinessType = busi;
                    track.CupeType = "---";
                    track.Type1 = TrendAnalysisEntity.Type.Master;
                    cupeToTrack.Add(track);

                    foreach (CUPEQuestionTrendAnalysis c in cupes)
                    {
                        track.Children++;
                        cupeToTrack.Add(c);
                        c.Type1 = TrendAnalysisEntity.Type.Child;
                    }
                }
                else
                    MessageBox.Show("Query returned no results.");



                trendGridView.DataSource = null;
                trendGridView.DataSource = cupeToTrack;
                trendGridView.Refresh();
                currentlyBeingTracked = "CUPE";

                trendGridView.Columns["Collapse"].Visible = true;
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void CreateITAttributeToTrack(string region, string busi, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Attribute")
            {
                List<ITAttributeTrendAnalysis> attributes = new List<ITAttributeTrendAnalysis>();
                attributes = db.GetITAttributeTrendAnalysis(itAttributesComboBox.Text, region, busi, from, to);
                ITAttributeTrendAnalysis ent = new ITAttributeTrendAnalysis();
                if (attributes.Count > 0)
                {
                    ent.AsisScore = attributes.Average(d => d.AsisScore);
                    ent.TobeScore = attributes.Average(d => d.TobeScore);
                    ent.Type1 = TrendAnalysisEntity.Type.Master;

                    attributesToTrack.Add(ent);
                    trendGridView.DataSource = null;
                    trendGridView.DataSource = attributes;
                    trendGridView.Refresh();
                    currentlyBeingTracked = "Attribute";

                    foreach (ITAttributeTrendAnalysis attr in attributes)
                    {
                        attr.Type1 = TrendAnalysisEntity.Type.Child;
                        ent.Children++;
                        attributesToTrack.Add(attr);
                    }
                }
                else
                    MessageBox.Show("Query returned no results.");
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }
        }

        private void CreateInitiativeToTrack(string region, string business, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Initiative")
            {
                List<InitiativeTrendAnalysis> initiatives = new List<InitiativeTrendAnalysis>();
                initiatives = db.GetInitiativeTrendAnalysis(initiativesComboBox.Text, region, business, from, to);
                InitiativeTrendAnalysis init = new InitiativeTrendAnalysis();


                if (initiatives.Count > 0)
                {
                    float diff = initiatives.Average(d => d.Differentiation);
                    float crit = initiatives.Average(d => d.Criticality);
                    float effect = initiatives.Average(d => d.Effectiveness);

                    init.Effectiveness = effect;
                    init.Criticality = crit;
                    init.Country = "All";
                    init.Differentiation = diff;
                    init.BusinessType = business;
                    init.Region = region;
                    init.Name = initiativesComboBox.Text;
                    init.Type1 = TrendAnalysisEntity.Type.Master;
                    initiativesToTrack.Add(init);
                    foreach (InitiativeTrendAnalysis i in initiatives)
                    {
                        init.Children++;
                        i.Type1 = TrendAnalysisEntity.Type.Child;
                        initiativesToTrack.Add(i);
                    }
                    trendGridView.Columns["Collapse"].Visible = true;
                }
                else
                    MessageBox.Show("Query did not return any results");
                trendGridView.DataSource = null;
                trendGridView.DataSource = initiativesToTrack;
                trendGridView.Refresh();
                currentlyBeingTracked = "Initiative";

                if (metricsComboBox.Enabled)
                {
                    testingLabel.Text = metricsComboBox.Text;
                }

                CreateInitiativeGraph(initiatives);
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void showResultsButton_Click(object sender, EventArgs e)
        {
            string regionToSearch;
            string businessTypeToSearch;
            string fromDate;
            string toDate;

            if (fromDateText.Enabled)
            {
                fromDate = fromDateText.Text;
            }
            else
                fromDate = "All";

            if (toDateText.Enabled)
            {
                toDate = toDateText.Text;
            }
            else
                toDate = "All";
            if(regionComboBox.Enabled)
                regionToSearch = regionComboBox.Text.Trim();
            else
                regionToSearch = "All";

            if(businessTypeComboBox.Enabled)
                businessTypeToSearch = businessTypeComboBox.Text.Trim();
            else
                businessTypeToSearch = "All";
            if (regionToSearch == "<All Regions>")
            {
                regionToSearch = "All";
            }
            if(businessTypeToSearch == "<All Business Types>")
            {
                businessTypeToSearch = "All";
            }

            

            if (state == TrackingState.Capabilities)
                CreateCapabilityToTrack(regionToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.CUPEQuestions)
                CreateCUPEQuestionToTrack(regionToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.ITAttributes)
                CreateITAttributeToTrack(regionToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.Initiatives)
                CreateInitiativeToTrack(regionToSearch, businessTypeToSearch, fromDate, toDate);            
        }

        private void clearGridButton_Click(object sender, EventArgs e)
        {
            ClearGridForm form = new ClearGridForm(this);
            form.Show();
        }

        public void ClearGrid()
        {
            trendGridView.DataSource = null;
            trendGridView.Refresh();
            currentlyBeingTracked = "";
            attributesToTrack.Clear();
            cupeToTrack.Clear();
            capabilitiesToTrack.Clear();
            initiativesToTrack.Clear();
        }


        public void CreateInitiativeGraph(List<InitiativeTrendAnalysis> init)
        {

        }

        private void trendGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in trendGridView.Rows)
            {
                TrendAnalysisEntity ent = row.DataBoundItem as TrendAnalysisEntity;

                if (ent.Type1 == TrendAnalysisEntity.Type.Child)
                {
                    trendGridView.CurrentCell = null;
                    row.Visible = false;
                }
                else
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void trendGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                TrendAnalysisEntity ent = trendGridView.Rows[e.RowIndex].DataBoundItem as TrendAnalysisEntity;
                if (ent.Type1 == TrendAnalysisEntity.Type.Master)
                {
                    for (int i = 1; i < ent.Children + 1; i++)
                    {
                        trendGridView.CurrentCell = null;
                        
                        trendGridView.Rows[e.RowIndex + i].Visible = !trendGridView.Rows[e.RowIndex + i].Visible;

                    }

                }

            }
        }







    }
}
