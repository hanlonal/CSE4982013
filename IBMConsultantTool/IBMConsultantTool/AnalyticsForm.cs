using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic.PowerPacks;

namespace IBMConsultantTool
{
    public partial class AnalyticsForm : Form
    {
        List<Control> comboBoxControls = new List<Control>();
        List<CapabilityTrendAnalysis> capabilitiesToTrack = new List<CapabilityTrendAnalysis>();
        List<CUPEQuestionTrendAnalysis> cupeToTrack = new List<CUPEQuestionTrendAnalysis>();
        List<ITAttributeTrendAnalysis> attributesToTrack = new List<ITAttributeTrendAnalysis>();
        List<ImperativeTrendAnalysis> imperativesToTrack = new List<ImperativeTrendAnalysis>();
        enum TrackingState { Capabilities, ITAttributes, Objectives, CUPEQuestions, Imperatives, None };
        TrackingState state;
        private DateTime fromTime;
        private DateTime toTime;
        private string currentlyBeingTracked = "";
        DBManager db = new DBManager();
        TextBox currentBox;
        Chart lineChart = new Chart();

        public AnalyticsForm()
        {
            lineChart.Parent = this.chartPanel;
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
                    metricsComboBox.Items.Add("Capability Gap Type");
                    metricsComboBox.Items.Add("Prioritized Capability Gap Type");
                    metricsComboBox.Items.Add("Capability Gap Amount");
                    metricsComboBox.Items.Add("Prioritized Capability Gap Amount");
                    break;
                case TrackingState.CUPEQuestions:
                    ClearControls("CUPE");
                    metricsComboBox.Items.Add("Business Future");
                    metricsComboBox.Items.Add("Business Current");
                    metricsComboBox.Items.Add("IT Future");
                    metricsComboBox.Items.Add("IT Current");                    
                    break;
                case TrackingState.Objectives:
                    ClearControls("Objectives");
                    metricsComboBox.Items.Add("Total Priority");
                    break;
                case TrackingState.ITAttributes:
                    ClearControls("Capabilities");                    
                    metricsComboBox.Items.Add("Average As Is Score");
                    metricsComboBox.Items.Add("Average To Be Score");
                    break;
                case TrackingState.Imperatives:
                    ClearControls("Imperatives");                    
                    metricsComboBox.Items.Add("Differentiation");
                    metricsComboBox.Items.Add("Criticality");
                    metricsComboBox.Items.Add("Effectiveness");

                    graphTypeComboBox.Items.Clear();
                    graphTypeComboBox.Items.Add("Line Graph");
                    graphTypeComboBox.Items.Add("Bar Graph");
                    break;
            }
        }


        #region Event Handlers

        private void metricsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // TODO
            ComboBox comboBox = (ComboBox)sender;

            string selectedInfo = (string)metricsComboBox.SelectedItem;

            if (state == TrackingState.Capabilities)
            {
            }
            else if (state == TrackingState.CUPEQuestions)
            {
                CreateCUPEGraph(cupeToTrack, "CUPE Question", selectedInfo);
            }
            else if (state == TrackingState.ITAttributes)
            {
                CreateITAttributeGraph(attributesToTrack, "IT Attributes", selectedInfo);
            }
            else if (state == TrackingState.Imperatives)
            {
                CreateLineGraph(imperativesToTrack, "Imperatives", selectedInfo);
            }
            else if (state == TrackingState.Objectives)
            {
                CreateObjectivesGraph(attributesToTrack, "Objectives", selectedInfo);
            }

            int resultIndex = -1;

            resultIndex = metricsComboBox.FindStringExact(selectedInfo);
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
            if (value == "Imperatives")
            {
                ChangeState(TrackingState.Imperatives);
                names = db.GetImperativeNames().ToList();
                imperativesComboBox.DataSource = names;
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

        private void CreateCapabilityToTrack(string region, string country, string busi, string from, string to)
        {

            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Capability")
            {
                List<CapabilityTrendAnalysis> capabilities = new List<CapabilityTrendAnalysis>();
                capabilities = db.GetCapabilityTrendAnalysis(capabilitiesComboBox.Text, region, country, busi, from, to);
                CapabilityTrendAnalysis ent = new CapabilityTrendAnalysis();
                if (capabilities.Count > 0)
                {
                    capabilities.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
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

        private void CreateCUPEQuestionToTrack(string region, string country, string busi, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "CUPE")
            {
                List<CUPEQuestionTrendAnalysis> cupes = new List<CUPEQuestionTrendAnalysis>();

                cupes = db.GetCUPEQuestionTrendAnalysis(cupeQuestionsComboBox.Text, region, country, busi, from, to);
                if (cupes.Count < 0)
                {
                    cupes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
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

        private void CreateITAttributeToTrack(string region, string country, string busi, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Attribute")
            {
                List<ITAttributeTrendAnalysis> attributes = new List<ITAttributeTrendAnalysis>();
                attributes = db.GetITAttributeTrendAnalysis(itAttributesComboBox.Text, region, country, busi, from, to);
                ITAttributeTrendAnalysis ent = new ITAttributeTrendAnalysis();
                if (attributes.Count > 0)
                {
                    attributes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
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

        private void CreateImperativeToTrack(string region, string country, string business, string from, string to)
        {
            if (currentlyBeingTracked == "" || currentlyBeingTracked == "Imperative")
            {
                List<ImperativeTrendAnalysis> imperatives = new List<ImperativeTrendAnalysis>();
                imperatives = db.GetImperativeTrendAnalysis(imperativesComboBox.Text, region, country, business, from, to);
                ImperativeTrendAnalysis init = new ImperativeTrendAnalysis();


                if (imperatives.Count > 0)
                {
                    imperatives.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                    float diff = imperatives.Average(d => d.Differentiation);
                    float crit = imperatives.Average(d => d.Criticality);
                    float effect = imperatives.Average(d => d.Effectiveness);

                    init.Effectiveness = effect;
                    init.Criticality = crit;
                    init.Country = "All";
                    init.Differentiation = diff;
                    init.BusinessType = business;
                    init.Region = region;
                    init.Name = imperativesComboBox.Text;
                    init.Type1 = TrendAnalysisEntity.Type.Master;
                    imperativesToTrack.Add(init);
                    foreach (ImperativeTrendAnalysis i in imperatives)
                    {
                        init.Children++;
                        i.Type1 = TrendAnalysisEntity.Type.Child;
                        imperativesToTrack.Add(i);
                    }

                   
                    trendGridView.Columns["Collapse"].Visible = true;
                }
                else
                    MessageBox.Show("Query did not return any results");
                trendGridView.DataSource = null;
                trendGridView.DataSource = imperativesToTrack;
                trendGridView.Refresh();
                currentlyBeingTracked = "Imperative";

                if (metricsComboBox.Enabled)
                {
                    testingLabel.Text = metricsComboBox.Text;
                    CreateLineGraph(imperativesToTrack, "Imperatives", metricsComboBox.Text);
                }

                CreateImperativeGraph(imperatives);
                CreateLineGraph(imperativesToTrack, "Imperatives", "");
            }
            else
            {
                MessageBox.Show("You can only track one entity type at a time. Please clear grid and try again.");
            }

        }

        private void showResultsButton_Click(object sender, EventArgs e)
        {
            string regionToSearch;
            string countryToSearch;
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
            if (regionComboBox.Enabled)
            {
                regionToSearch = regionComboBox.Text.Trim();
                if (countryCheckBox.Enabled)
                    countryToSearch = countryComboBox.Text.Trim();
                else
                    countryToSearch = "All";
            }
            else
            {
                regionToSearch = "All";
                countryToSearch = "All";
            }

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
                CreateCapabilityToTrack(regionToSearch, countryToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.CUPEQuestions)
                CreateCUPEQuestionToTrack(regionToSearch, countryToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.ITAttributes)
                CreateITAttributeToTrack(regionToSearch, countryToSearch, businessTypeToSearch, fromDate, toDate);
            else if (state == TrackingState.Imperatives)
                CreateImperativeToTrack(regionToSearch, countryToSearch, businessTypeToSearch, fromDate, toDate);            
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
            imperativesToTrack.Clear();
        }


        public void CreateImperativeGraph(List<ImperativeTrendAnalysis> init)
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
            if (e.ColumnIndex == 0 && e.RowIndex >=0)
            {
                TrendAnalysisEntity ent = trendGridView.Rows[e.RowIndex].DataBoundItem as TrendAnalysisEntity;
                if (ent.Type1 == TrendAnalysisEntity.Type.Master)
                {
                    for (int i = 1; i < ent.Children + 1; i++)
                    {                       
                        trendGridView.Rows[e.RowIndex + i].Visible = !trendGridView.Rows[e.RowIndex + i].Visible;
                    }
                }
            }
        }
        private int numberOfGraph = 0;
        public void CreateLineGraph(List<ImperativeTrendAnalysis> init, string title, string boxText)
        {
            testingLabel.Visible = false;

            foreach (ImperativeTrendAnalysis ana in init)
            {
                Console.WriteLine(numberOfGraph.ToString() + ", Date: " + ana.Date.ToString() + ", diff: " + ana.Differentiation.ToString() +
                    ", crit: " + ana.Criticality.ToString() + ", eff: " + ana.Effectiveness.ToString());
            }

            numberOfGraph = 0;
            if (lineChart != null)
            {
                lineChart.ChartAreas.Clear();
                lineChart.Series.Clear();
                lineChart.Legends.Clear();
                lineChart.Titles.Clear();
            }
            lineChart.Parent = this.chartPanel;
            lineChart.Size = this.chartPanel.Size;
            lineChart.Visible = true;

            lineChart.ChartAreas.Add(title);
            lineChart.ChartAreas[title].Visible = true;

            string saveName = boxText;

            string seriesName = "";

            int numberOfSeries = 0;

            for (int cnt = 0; cnt < init.Count; cnt++)
            {
                if (lineChart.Series.FindByName(init[cnt].Name) == null)
                    numberOfSeries++;
            }

            int eachClients = 1;

            if (numberOfSeries > 0)
                eachClients = init.Count / numberOfSeries;

            int cntNum = 0;
            int[] sameNum = new int[100];

            #region Differentiation Line Graph

            if (boxText == "Differentiation")
            {
                int newCount = 0;
                int childrenCount = 0;
                for (int cnt = 0; cnt < init.Count; cnt++)
                {
                    string name = init[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        seriesName = name;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount == -1)
                    {
                        lineChart.Series.Add(numberOfGraph.ToString());
                        seriesName = numberOfGraph.ToString();
                        name = numberOfGraph.ToString();

                        numberOfGraph++;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount >= 0 && lineChart.Series.FindByName(name) != null)
                    {
                        name = seriesName;
                    }

                    if (init[cnt].Children > 0)
                    {
                        childrenCount = init[cnt].Children;
                        eachClients = init[cnt].Children;
                    }

                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }

                    if (newCount != sameNum[cntNum])
                    {
                        double differentiation = init[cnt].Differentiation;

                        double[] diff = new double[100];
                        diff[cnt] = init[cnt].Differentiation;
                        int count = 1;

                        DateTime date = init[cnt].Date;
                        for (int num = 1; num < childrenCount; num++)
                        {
                            if (date == init[num + cnt].Date)
                            {
                                differentiation += init[num + cnt].Differentiation;
                                sameNum[cntNum] = num + cnt;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            differentiation /= count;

                            double temp = Convert.ToDouble(differentiation);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                        else
                        {
                            double temp = Convert.ToDouble(init[cnt].Differentiation);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                    }
                    newCount++;
                    childrenCount--;
                }
            }

            #endregion

            #region Criticality Line Graph

            else if (boxText == "Criticality")
            {
                int newCount = 0;
                int childrenCount = 0;
                for (int cnt = 0; cnt < init.Count; cnt++)
                {
                    string name = init[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        seriesName = name;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount == -1)
                    {
                        lineChart.Series.Add(numberOfGraph.ToString());
                        seriesName = numberOfGraph.ToString();
                        name = numberOfGraph.ToString();

                        numberOfGraph++;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount >= 0 && lineChart.Series.FindByName(name) != null)
                    {
                        name = seriesName;
                    }

                    if (init[cnt].Children > 0)
                    {
                        childrenCount = init[cnt].Children;
                        eachClients = init[cnt].Children;
                    }

                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }

                    if (newCount != sameNum[cntNum])
                    {
                        double criticality = init[cnt].Criticality;

                        double[] diff = new double[100];
                        diff[cnt] = init[cnt].Criticality;
                        int count = 1;

                        DateTime date = init[cnt].Date;
                        for (int num = 1; num < childrenCount; num++)
                        {
                            if (date == init[num + cnt].Date)
                            {
                                criticality += init[num + cnt].Criticality;
                                sameNum[cntNum] = num + cnt;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            criticality /= count;

                            double temp = Convert.ToDouble(criticality);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                        else
                        {
                            double temp = Convert.ToDouble(init[cnt].Criticality);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                    }
                    newCount++;
                    childrenCount--;
                }
            }

            #endregion

            #region Effectiveness Line Graph

            else if (boxText == "Effectiveness")
            {
                int newCount = 0;
                int childrenCount = 0;
                for (int cnt = 0; cnt < init.Count; cnt++)
                {
                    string name = init[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        seriesName = name;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount == -1)
                    {
                        lineChart.Series.Add(numberOfGraph.ToString());
                        seriesName = numberOfGraph.ToString();
                        name = numberOfGraph.ToString();

                        numberOfGraph++;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount >= 0 && lineChart.Series.FindByName(name) != null)
                    {
                        name = seriesName;
                    }

                    if (init[cnt].Children > 0)
                    {
                        childrenCount = init[cnt].Children;
                        eachClients = init[cnt].Children;
                    }

                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }
                    if (newCount != sameNum[cntNum])
                    {
                        double effectiveness = init[cnt].Effectiveness;

                        double[] diff = new double[100];
                        diff[cnt] = init[cnt].Effectiveness;
                        int count = 1;

                        DateTime date = init[cnt].Date;
                        for (int num = 1; num < childrenCount; num++)
                        {
                            if (date == init[num + cnt].Date)
                            {
                                effectiveness += init[num + cnt].Effectiveness;
                                sameNum[cntNum] = num + cnt;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            effectiveness /= count;

                            double temp = Convert.ToDouble(effectiveness);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                        else
                        {
                            double temp = Convert.ToDouble(init[cnt].Effectiveness);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                    }
                    newCount++;
                    childrenCount--;
                }
            }

            #endregion

            #region Default Line Graph

            else
            {
                saveName = "Default Differentiation";
                int newCount = 0;
                int childrenCount = 0;
                for (int cnt = 0; cnt < init.Count; cnt++)
                {
                    string name = init[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        seriesName = name;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount == -1)
                    {
                        lineChart.Series.Add(numberOfGraph.ToString());
                        seriesName = numberOfGraph.ToString();
                        name = numberOfGraph.ToString();

                        numberOfGraph++;
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }

                    else if (childrenCount >= 0 && lineChart.Series.FindByName(name) != null)
                    {
                        name = seriesName;
                    }

                    if (init[cnt].Children > 0)
                    {
                        childrenCount = init[cnt].Children;
                        eachClients = init[cnt].Children;
                    }

                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }
                    if (newCount != sameNum[cntNum])
                    {
                        System.Diagnostics.Trace.WriteLine(init[cnt].Date + ", diff: " + init[cnt].Differentiation.ToString());
                        double differentiation = init[cnt].Differentiation;

                        double[] diff = new double[100];
                        diff[cnt] = init[cnt].Differentiation;
                        int count = 1;

                        DateTime date = init[cnt].Date;
                        for (int num = 1; num < childrenCount; num++)
                        {
                            if (cnt > num + childrenCount)
                                break;
                            if (date == init[num + cnt].Date)
                            {
                                differentiation += init[num + cnt].Differentiation;
                                sameNum[cntNum] = num + cnt;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            differentiation /= count;

                            double temp = Convert.ToDouble(differentiation);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                        else
                        {
                            double temp = Convert.ToDouble(init[cnt].Differentiation);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(init[cnt].Date, temp);
                        }
                    }
                    newCount++;
                    childrenCount--;
                }
            }

            #endregion

            lineChart.Titles.Add("title");
            lineChart.Titles[0].Name = "title";
            lineChart.Titles["title"].Visible = true;
            lineChart.Titles["title"].Text = title + " - " + saveName;
            lineChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            //lineChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + title + " " +
            //saveName + ".jpg", ChartImageFormat.Jpeg);
        }

        private void regionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            countryComboBox.Items.Clear();
            countryComboBox.Text = "<All Countries>";
            countryComboBox.Items.AddRange(db.GetCountryNames(regionComboBox.Text).ToArray());
        }

        private void regionComboBox_LostFocus(object sender, EventArgs e)
        {
            countryComboBox.Items.Clear();
            countryComboBox.Text = "<All Countries>";
            countryComboBox.Items.AddRange(db.GetCountryNames(regionComboBox.Text).ToArray());
        }

        public void CreateITAttributeGraph(List<ITAttributeTrendAnalysis> itAtt, string title, string boxText)
        {
            testingLabel.Visible = false;

            foreach (ITAttributeTrendAnalysis ana in itAtt)
            {
                Console.WriteLine("Date: " + ana.Date.ToString() + ", as is: " + ana.AsisScore.ToString()
                    + ", to be: " + ana.TobeScore.ToString() + ", " + ana.BusinessType.ToString());
            }

            string saveName = boxText;

            if (lineChart != null)
            {
                lineChart.ChartAreas.Clear();
                lineChart.Series.Clear();
            }
            lineChart.Parent = this.chartPanel;
            lineChart.Size = this.chartPanel.Size;
            lineChart.Visible = true;

            lineChart.ChartAreas.Add(title);
            lineChart.ChartAreas[title].Visible = true;
            int cntNum = 0;
            int[] sameNum = new int[100];

            #region All Line Graph

            if (boxText == "All")
            {
            }

            #endregion

            #region Average Current Score Line Graph

            else if (boxText == "Average As Is Score")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < itAtt.Count; cnt++)
                {
                    string name = itAtt[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }
                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }
                    if (newCount != sameNum[cntNum])
                    {
                        double asIsScore = itAtt[cnt].AsisScore;

                        double[] asIs = new double[100];
                        asIs[cnt] = itAtt[cnt].AsisScore;
                        int count = 1;

                        DateTime date = itAtt[cnt].Date;
                        for (int num = (cnt + 1); num < itAtt.Count; num++)
                        {
                            if (date == itAtt[num].Date)
                            {
                                asIsScore += itAtt[num].AsisScore;
                                sameNum[cntNum] = num;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            asIsScore /= count;

                            double temp = Convert.ToDouble(asIsScore);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(itAtt[cnt].Date, temp);
                        }
                        else
                        {
                            lineChart.Series[name].Points.AddXY(itAtt[cnt].Date, itAtt[cnt].AsisScore);
                        }
                    }
                    newCount++;
                }
            }

            #endregion

            #region Average Future Score Line Graph

            else if (boxText == "Average To Be Score")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < itAtt.Count; cnt++)
                {
                    string name = itAtt[cnt].Name;

                    if (lineChart.Series.FindByName(name) == null)
                    {
                        lineChart.Series.Add(name);
                        for (int i = 0; i < cntNum; i++)
                        {
                            sameNum[i] = new int();
                        }
                        cntNum = 0;
                        newCount = 0;
                    }
                    lineChart.Series[name].ChartArea = title;
                    lineChart.Series[name].ChartType = SeriesChartType.Line;
                    lineChart.Series[name].XValueType = ChartValueType.DateTime;
                    lineChart.Series[name].YValueType = ChartValueType.Double;
                    lineChart.Series[name].BorderWidth = 5;

                    for (int i = 0; i < cntNum; i++)
                    {
                        if (cnt == sameNum[i])
                        {
                            cntNum = i;
                            newCount = cnt;
                            break;
                        }
                    }
                    if (newCount != sameNum[cntNum])
                    {
                        double toBeScore = itAtt[cnt].TobeScore;

                        double[] toBe = new double[100];
                        toBe[cnt] = itAtt[cnt].TobeScore;
                        int count = 1;

                        DateTime date = itAtt[cnt].Date;
                        for (int num = (cnt + 1); num < itAtt.Count; num++)
                        {
                            if (date == itAtt[num].Date)
                            {
                                toBeScore += itAtt[num].AsisScore;
                                sameNum[cntNum] = num;
                                cntNum++;
                                count++;
                            }
                        }

                        if (count > 1)
                        {
                            toBeScore /= count;

                            double temp = Convert.ToDouble(toBeScore);
                            decimal tmp = Convert.ToDecimal(temp);
                            tmp = Math.Round(tmp, 2);
                            temp = (double)tmp;

                            lineChart.Series[name].Points.AddXY(itAtt[cnt].Date, temp);
                        }
                        else
                        {
                            lineChart.Series[name].Points.AddXY(itAtt[cnt].Date, itAtt[cnt].TobeScore);
                        }
                    }
                    newCount++;
                }
            }

            #endregion

            #region Default Line Graph

            else
            {
                saveName = "Default";
            }

            #endregion

            lineChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + title + " " +
                saveName + ".jpg", ChartImageFormat.Jpeg);
        }

        public void CreateObjectivesGraph(List<ITAttributeTrendAnalysis> itAtt, string title, string boxText)
        {
            testingLabel.Visible = false;

            /*foreach (ITAttributeTrendAnalysis ana in itAtt)
            {
                Console.WriteLine(numberOfGraph.ToString() + ", Date: " + ana.Date.ToString() + ", as is: " + ana.AsisScore.ToString()
                    + ", to be: " + ana.TobeScore.ToString() + ", " + ana.BusinessType.ToString());
            }*/

            string saveName = "Total Priority";

            if (lineChart != null)
            {
                lineChart.ChartAreas.Clear();
                lineChart.Series.Clear();
            }
            lineChart.Parent = this.chartPanel;
            lineChart.Size = this.chartPanel.Size;
            lineChart.Visible = true;

            lineChart.ChartAreas.Add(title);
            lineChart.ChartAreas[title].Visible = true;
            int cntNum = 0;
            int[] sameNum = new int[100];

            lineChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + title + " " +
                saveName + ".jpg", ChartImageFormat.Jpeg);
        }

        public void CreateCUPEGraph(List<CUPEQuestionTrendAnalysis> cupe, string title, string boxText)
        {
            testingLabel.Visible = false;

            string saveName = boxText;

            if (lineChart != null)
            {
                lineChart.ChartAreas.Clear();
                lineChart.Series.Clear();
            }

            lineChart.Parent = this.chartPanel;
            lineChart.Size = this.chartPanel.Size;
            lineChart.Visible = true;

            lineChart.ChartAreas.Add(title);
            lineChart.ChartAreas[title].Visible = true;

            

            int cntNum = 0;
            int[] sameNum = new int[100];

            #region Business Future Line Graph

            if (boxText == "Business Future")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < cupe.Count; cnt++)
                {
                    string name = cupe[cnt].Name;

                    if (cupe[cnt].BusinessType == "Business")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double futureScore = cupe[cnt].FutureAnswer;

                            double[] future = new double[100];
                            future[cnt] = cupe[cnt].FutureAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    futureScore += cupe[num].FutureAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                futureScore /= count;

                                double temp = Convert.ToDouble(futureScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].FutureAnswer);
                            }
                        }
                        newCount++;
                    }
                }
            }

            #endregion

            #region Business Current Line Graph

            else if (boxText == "Business Current")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < cupe.Count; cnt++)
                {
                    string name = cupe[cnt].Name;

                    if (cupe[cnt].BusinessType == "Business")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double currentScore = cupe[cnt].CurrentAnswer;

                            double[] current = new double[100];
                            current[cnt] = cupe[cnt].CurrentAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    currentScore += cupe[num].CurrentAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                currentScore /= count;

                                double temp = Convert.ToDouble(currentScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].CurrentAnswer);
                            }
                        }
                        newCount++;
                    }
                }
            }

            #endregion

            #region IT Future Line Graph

            else if (boxText == "IT Future")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < cupe.Count; cnt++)
                {
                    string name = cupe[cnt].Name;

                    if (cupe[cnt].BusinessType == "IT")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double futureScore = cupe[cnt].FutureAnswer;

                            double[] future = new double[100];
                            future[cnt] = cupe[cnt].FutureAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    futureScore += cupe[num].FutureAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                futureScore /= count;

                                double temp = Convert.ToDouble(futureScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].FutureAnswer);
                            }
                        }
                        newCount++;
                    }
                }
            }

            #endregion

            #region IT Current Line Graph

            else if (boxText == "IT Current")
            {
                int newCount = 0;
                for (int cnt = 0; cnt < cupe.Count; cnt++)
                {
                    string name = cupe[cnt].Name;

                    if (cupe[cnt].BusinessType == "IT")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double currentScore = cupe[cnt].CurrentAnswer;

                            double[] current = new double[100];
                            current[cnt] = cupe[cnt].CurrentAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    currentScore += cupe[num].CurrentAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                currentScore /= count;

                                double temp = Convert.ToDouble(currentScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].CurrentAnswer);
                            }
                        }
                        newCount++;
                    }
                }
            }

            #endregion

            #region All Line Graph

            else if (boxText == "All")
            {
            }

            #endregion

            #region Default Line Graph

            else
            {
                saveName = "Default";
                int newCount = 0;
                for (int cnt = 0; cnt < cupe.Count; cnt++)
                {
                    string name = cupe[cnt].Name;

                    if (cupe[cnt].BusinessType == "Business")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double futureScore = cupe[cnt].FutureAnswer;

                            double[] future = new double[100];
                            future[cnt] = cupe[cnt].FutureAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    futureScore += cupe[num].FutureAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                futureScore /= count;

                                double temp = Convert.ToDouble(futureScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].FutureAnswer);
                            }
                        }
                        newCount++;
                    }

                    if (cupe[cnt].BusinessType == "IT")
                    {

                        if (lineChart.Series.FindByName(name) == null)
                        {
                            lineChart.Series.Add(name);
                            for (int i = 0; i < cntNum; i++)
                            {
                                sameNum[i] = new int();
                            }
                            cntNum = 0;
                            newCount = 0;
                        }
                        lineChart.Series[name].ChartArea = title;
                        lineChart.Series[name].ChartType = SeriesChartType.Line;
                        lineChart.Series[name].XValueType = ChartValueType.DateTime;
                        lineChart.Series[name].YValueType = ChartValueType.Double;
                        lineChart.Series[name].BorderWidth = 5;

                        for (int i = 0; i < cntNum; i++)
                        {
                            if (cnt == sameNum[i])
                            {
                                cntNum = i;
                                newCount = cnt;
                                break;
                            }
                        }
                        if (newCount != sameNum[cntNum])
                        {
                            double futureScore = cupe[cnt].FutureAnswer;

                            double[] future = new double[100];
                            future[cnt] = cupe[cnt].FutureAnswer;
                            int count = 1;

                            DateTime date = cupe[cnt].Date;
                            for (int num = (cnt + 1); num < cupe.Count; num++)
                            {
                                if (date == cupe[num].Date)
                                {
                                    futureScore += cupe[num].FutureAnswer;
                                    sameNum[cntNum] = num;
                                    cntNum++;
                                    count++;
                                }
                            }

                            if (count > 1)
                            {
                                futureScore /= count;

                                double temp = Convert.ToDouble(futureScore);
                                decimal tmp = Convert.ToDecimal(temp);
                                tmp = Math.Round(tmp, 2);
                                temp = (double)tmp;

                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, temp);
                            }
                            else
                            {
                                lineChart.Series[name].Points.AddXY(cupe[cnt].Date, cupe[cnt].FutureAnswer);
                            }
                        }
                        newCount++;
                    }
                }
            }

            #endregion

            lineChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + title + " " +
                saveName + ".jpg", ChartImageFormat.Jpeg);
        }

        private void trendGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void color_Click(object sender, EventArgs e)
        {
            ColorDialog clrDialog = new ColorDialog();

            clrDialog.AllowFullOpen = false;

            clrDialog.ShowHelp = true;

            clrDialog.Color = trendGridView.SelectedRows[0].DefaultCellStyle.BackColor;

            if (clrDialog.ShowDialog() == DialogResult.OK)
            {
                trendGridView.SelectedRows[0].DefaultCellStyle.BackColor = clrDialog.Color;
            }

        }

        private void trendGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverColumn = trendGridView.HitTest(e.X, e.Y).ColumnIndex;
                int currentMouseOverRow = trendGridView.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverColumn > 0 && currentMouseOverRow >= 0)
                {
                    TrendAnalysisEntity ent = trendGridView.Rows[currentMouseOverRow].DataBoundItem as TrendAnalysisEntity;
                    trendGridView.Rows[currentMouseOverRow].Selected = true;
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem color = new ToolStripMenuItem();
                    strip.Items.Add(color);
                    color.Click += new EventHandler(color_Click);
                    color.Text = "Change Color";


                    strip.Show(trendGridView, e.Location, ToolStripDropDownDirection.BelowRight);

                }

            }
        }
    }
}
