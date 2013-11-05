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
        List<TrendAnalysisEntity> entities = new List<TrendAnalysisEntity>();
        DBManager db = new DBManager();
        TextBox currentBox;
        public AnalyticsForm()
        {
            InitializeComponent();
            

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


        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ClearControls()
        {
            foreach (Control con in comboBoxControls)
            {
                ComboBox box = (ComboBox)con;
                box.DataSource = null;
                box.Items.Clear();
            }

        }


        #region Event Handlers

        private void analyticsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)analyticsListBox.SelectedItem;
            List<BUSINESSOBJECTIVE> values = new List<BUSINESSOBJECTIVE>();
            List<string> names = new List<string>();

            if (value == "Objectives")
            {
                values = db.GetObjectives().ToList();

                foreach (BUSINESSOBJECTIVE obj in values)
                {
                    names.Add(obj.NAME.Trim());
                }
                objectiveNamesComboBox.DataSource = names;
                objectiveNamesComboBox.SelectedValueChanged += new EventHandler(objectiveNamesComboBox_SelectedValueChanged);
            }

            if (value == "Capabilities")
            {
                //ClearControls();
                names = db.GetDomainNames().ToList();
                domainsComboBox.DataSource = names;

                domainsComboBox.SelectedValueChanged +=new EventHandler(domainsComboBox_SelectedValueChanged);

            }

            if (value == "CUPE Questions")
            {
                //ClearControls();
                List<CupeQuestionStringData> stringData = new List<CupeQuestionStringData>();
                
                stringData = db.GetCUPEQuestionStringData();
                foreach (CupeQuestionStringData x in stringData)
                {
                    names.Add(x.OriginalQuestionText);
                }

                cupeQuestionsComboBox.DataSource = names;

            }
        }

        private void objectiveNamesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string value = (string)cupeQuestionsComboBox.SelectedText;
        }

        private void domainsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //ClearControls();

            List<String> names = new List<string>();

            names = db.GetCapabilitiesFromDomain(domainsComboBox.Text);
           
            capabilitiesComboBox.DataSource = names;
           
        }

        private void date_DateSelected(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = (MonthCalendar)sender;

            currentBox.Text = e.Start.Date.ToShortDateString();
            //selectedTime = e.Start.Date;
            cal.Visible = false;
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

        private void showResultsButton_Click(object sender, EventArgs e)
        {
            TrendAnalysisEntity ent = new TrendAnalysisEntity();
            entities.Add(ent);
            trendGridView.DataSource = null;
            trendGridView.DataSource = entities;
            trendGridView.Refresh();
            //db.getin
        }






    }
}
