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
        List<CapabilityTrendAnalysis> capabilities = new List<CapabilityTrendAnalysis>();
        List<CUPEQuestionTrendAnalysis> cupes = new List<CUPEQuestionTrendAnalysis>();
        List<ITAttributeTrendAnalysis> attributes = new List<ITAttributeTrendAnalysis>();
        List<InitiativeTrendAnalysis> initiatives = new List<InitiativeTrendAnalysis>();
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
            date.BringToFront();
        }

        private void CreateCapabilityToTrack()
        {

            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Capability")
            {
                CapabilityTrendAnalysis ent = new CapabilityTrendAnalysis();
                capabilities.Add(ent);
                trendGridView.DataSource = null;
                trendGridView.DataSource = capabilities;
                trendGridView.Refresh();
                currentlyBeingTracked = "Capability";
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void CreateCUPEQuestionToTrack()
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "CUPE")
            {
                CUPEQuestionTrendAnalysis ent = new CUPEQuestionTrendAnalysis();
                cupes.Add(ent);
                trendGridView.DataSource = null;
                trendGridView.DataSource = cupes;
                trendGridView.Refresh();
                currentlyBeingTracked = "CUPE";
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void CreateITAttributeToTrack()
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Attribute")
            {
                ITAttributeTrendAnalysis ent = new ITAttributeTrendAnalysis();
                attributes.Add(ent);
                trendGridView.DataSource = null;
                trendGridView.DataSource = attributes;
                trendGridView.Refresh();
                currentlyBeingTracked = "Attribute";
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }
        }

        private void CreateInitiativeToTrack()
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Initiative")
            {
                InitiativeTrendAnalysis ent = new InitiativeTrendAnalysis();
                initiatives.Add(ent);
                trendGridView.DataSource = null;
                trendGridView.DataSource = initiatives;
                trendGridView.Refresh();
                currentlyBeingTracked = "Initiative";
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void showResultsButton_Click(object sender, EventArgs e)
        {
            if (state == TrackingState.Capabilities)
                CreateCapabilityToTrack();
            else if (state == TrackingState.CUPEQuestions)
                CreateCUPEQuestionToTrack();
            else if (state == TrackingState.ITAttributes)
                CreateITAttributeToTrack();
            else if (state == TrackingState.Initiatives)
                CreateInitiativeToTrack();

            
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
            attributes.Clear();
            cupes.Clear();
            capabilities.Clear();
        }

        private void dataPanel_Paint(object sender, PaintEventArgs e)
        {

        }









    }
}
