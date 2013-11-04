using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace IBMConsultantTool
{
    //TODO: Save changes to questions in the question view
    //TODO: Load survey data
    //TODO: Load any user data when tool starts up

    public partial class CUPETool : Form
    {
        List<DataGridView> grids = new List<DataGridView>();
        List<Chart> charts = new List<Chart>();
        DataGridView currentGrid;
        DataGridView toRemove;
       // DataGridView view = new DataGridView();
        Chart currentChart;
        int personCount = 0;
        bool isAnonymous = true;
        public bool is20Question = true;
        bool changesMade = false;

        int totalAIndex = 1;
        int totalBIndex = 2;
        int totalCIndex = 3;
        int totalDIndex = 4;
        int averageIndex = 6;
        int totalAnswers = 5;

        int numberOfIT = 0;
        int numerOfBusi = 0;


        int totalARowIndex = 21;
        int totalBRowIndex = 22;
        int totalCRowIndex = 23;
        int totalDRowIndex = 24;
        int averageRowIndex = 26;
        int totalAnswerRowIndex = 25;

        List<string> questions = new List<string>();

        bool HelpEnabled = false;
        int HelpCurrentStep = 0;

        public CUPETool()
        {
            InitializeComponent();
            grids.Add(questionGridITCurrent);
            grids.Add(questionGridBusinessCurrent);
            grids.Add(questionGridITFuture);
            grids.Add(questionGridBusiFuture);
            charts.Add(busiCurrentGraph);
            charts.Add(busiFutureGraph);
            charts.Add(itCurrentGraph);
            charts.Add(itFutureGraph);
        }

        private void CUPETool_Load(object sender, EventArgs e)
        {
            //questionGrid.CellValueChanged +=new DataGridViewCellEventHandler(questionGrid_CellValueChanged);
            //CreatePerson();
            ClientDataControl.LoadCUPEQuestions(this);
            foreach (DataGridView view in grids)
            {
                currentGrid = view;
                for (int i = 1; i < ClientDataControl.GetCupeQuestions().Count + 1; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[0].Clone();
                    row.Cells[0].Value = "Question " + i;
                    row.Visible = true;
                    currentGrid.Rows.Add(row);

                    //Change this if the number of questions changes
                    if(questions.Count < 20)
                    {
                        questions.Add(row.Cells[0].Value.ToString());
                    }
                }
                CreateStatsRows();
            }
            currentGrid = questionGridBusinessCurrent;
            currentChart = busiCurrentGraph;
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            currentChart.Visible = true;
            CreateGraphs();
            //CreateLabel();

        }

        private void CreateGraphs()
        {
            foreach (Chart chart in charts)
            {
                currentChart = chart;
                
                DataPoint point = new DataPoint();
                point.Color = Color.Fuchsia;
                point.BackGradientStyle = GradientStyle.Center;
                point.Name = "Commodity";
                point.Label = "Commodity";
                point.SetValueY(30);
                currentChart.Series["BusiCurrent"].Points.Add(point);
                DataPoint point2 = new DataPoint();
                point2.Color = Color.Blue;
                point2.BackGradientStyle = GradientStyle.Center;
                point2.Name = "Utility";
                point2.Label = "Utility";
                point2.SetValueY(10);
                currentChart.Series["BusiCurrent"].Points.Add(point2);
                DataPoint point3 = new DataPoint();
                point3.Color = Color.Orange;
                point3.BackGradientStyle = GradientStyle.Center;
                point3.Name = "Partner";
                point3.Label = "Partner";
                point3.SetValueY(5);
                currentChart.Series["BusiCurrent"].Points.Add(point3);
                DataPoint point4 = new DataPoint();
                point4.Color = Color.Green;
                point4.BackGradientStyle = GradientStyle.Center;
                point4.Name = "Enabler";
                point4.Label = "Enabler";
                point4.SetValueY(25);
                currentChart.Series["BusiCurrent"].Points.Add(point4);
            }
        }

        private void CreateLabel()
        {
            //Console.WriteLine("yo");
            Label label = new Label();
            questionInfoPanel.Controls.Add(label);
            label.Width = questionInfoPanel.Width;
            label.BackColor = Color.DeepSkyBlue;
            label.Location = new Point(0, 0);
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Visible = true;
            label.Text = "Question Info";
            //Console.WriteLine(label.Text);
        }

        private void CreateStatsRows()
        {
            DataGridViewRow row0 = (DataGridViewRow)currentGrid.Rows[0].Clone();
           // row0.Cells[0].Value = "Total Commodity";
            row0.Visible = true;
            row0.ReadOnly = true;
            currentGrid.Rows.Add(row0);

            DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row.Cells[0].Value = "Total Commodity";
            row.Visible = true;
            row.ReadOnly = true;
            currentGrid.Rows.Add(row);

            DataGridViewRow row2 = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row2.Cells[0].Value = "Total Utility";
            row2.Visible = true;
            row2.ReadOnly = true;
            currentGrid.Rows.Add(row2);

            DataGridViewRow row3 = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row3.Cells[0].Value = "Total Partner";
            row3.Visible = true;
            row3.ReadOnly = true;
            currentGrid.Rows.Add(row3);

            DataGridViewRow row4 = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row4.Cells[0].Value = "Total Enabler";
            row4.Visible = true;
            row4.ReadOnly = true;
            currentGrid.Rows.Add(row4);

            DataGridViewRow row5 = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row5.Cells[0].Value = "Total Answer";
            row5.Visible = true;
            row5.ReadOnly = true;
            currentGrid.Rows.Add(row5);

            DataGridViewRow row6 = (DataGridViewRow)currentGrid.Rows[0].Clone();
            row6.Cells[0].Value = "Average Score";
            row6.Visible = true;
            row6.ReadOnly = true;
            currentGrid.Rows.Add(row6);



        }

        public void CreatePerson()
        {
            personCount++;
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            Person person = new Person();

            //person.Type = Person.EmployeeType.Business;
            col.HeaderText = "Person  " + (currentGrid.ColumnCount - 6).ToString();            
           
            col.Name = "Person" + (currentGrid.ColumnCount -6).ToString(); 
            
            col.Width = 60;
         
            currentGrid.Columns.Insert(currentGrid.ColumnCount -6, col);
            
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            CreatePerson();
        }



        private void questionGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ChangeTotalsByRow(e.RowIndex);
            ChangeTotalsByColumn(e.ColumnIndex, e.RowIndex);
            LoadChartData();
            UpdateCupeScore();
        }

        public void ChangeTotalsByRow(int index)
        {
            int totalA=0;
            int totalB=0;
            int totalC=0;
            int totalD=0;
            int count = 0;
            float total = 0;
            foreach (DataGridViewCell cell in currentGrid.Rows[index].Cells)
            {
                if (cell.Value == null)
                {
                    continue;
                }
                if ((cell.Value).ToString() == "a" || (cell.Value).ToString() == "A")
                {
                    totalA++;
                    count++;
                    total +=1;
                }
                if ((cell.Value).ToString() == "b" || (cell.Value).ToString() == "B")
                {
                    totalB++;
                    count++;
                    total+=2;
                }
                if ((cell.Value).ToString() == "c" || (cell.Value).ToString() == "C")
                {
                    totalC++;
                    count++;
                    total += 3;
                }
                if ((cell.Value).ToString() == "d" || (cell.Value).ToString() == "D")
                {
                    totalD++;
                    count++;
                    total += 4;

                }
            }
            currentGrid.Rows[index].Cells[totalAIndex + (currentGrid.ColumnCount -7)].Value = totalA.ToString();
            currentGrid.Rows[index].Cells[totalBIndex + (currentGrid.ColumnCount - 7)].Value = totalB.ToString();
            currentGrid.Rows[index].Cells[totalCIndex + (currentGrid.ColumnCount - 7)].Value = totalC.ToString();
            currentGrid.Rows[index].Cells[totalDIndex + (currentGrid.ColumnCount - 7)].Value = totalD.ToString();
            currentGrid.Rows[index].Cells[totalAnswers + (currentGrid.ColumnCount - 7)].Value = count.ToString();
            currentGrid.Rows[index].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value = (total / (currentGrid.ColumnCount - 7)).ToString();

            QuestionCellFormatting(index);
            
        }

        private void UpdateCupeScore()
        {
            int count = 0;
            string total = "";
            float num = 0;
            for (int i = 0; i < 21; i++)
            {
                if (currentGrid.Rows[i].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value != null)
                {
                    total = currentGrid.Rows[i].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value.ToString();
                    num += (float)Convert.ToDouble(total);
                    count++;
                }
            }
            if (count > 0)
                cupeScoreLabel.Text = (num / count).ToString();
            else
                cupeScoreLabel.Text = " ";
        }



        private void ChangeTotalsByColumn(int colIndex, int rowIndex)
        {
            int totalA = 0;
            int totalB = 0;
            int totalC = 0;
            int totalD = 0;
            int count = 0;
            float total = 0;

            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                if (row.Cells[colIndex].Value == null)
                { 
                    continue;
                }
                if (row.Cells[colIndex].Value.ToString() == "a" || row.Cells[colIndex].Value.ToString() == "A")
                {
                    totalA++;
                    count++;
                    total += 1;
                }
                if (row.Cells[colIndex].Value.ToString() == "b" || row.Cells[colIndex].Value.ToString() == "B")
                {
                    totalB++;
                    count++;
                    total += 2;
                }
                if (row.Cells[colIndex].Value.ToString() == "c" || row.Cells[colIndex].Value.ToString() == "C")
                {
                    totalC++;
                    count++;
                    total += 3;
                }
                if (row.Cells[colIndex].Value.ToString() == "d" || row.Cells[colIndex].Value.ToString() == "D")
                {
                    totalD++;
                    count++;
                    total += 4;
                }
            }

            currentGrid.Rows[totalARowIndex].Cells[colIndex].Value = totalA.ToString();
            currentGrid.Rows[totalBRowIndex].Cells[colIndex].Value = totalB.ToString();
            currentGrid.Rows[totalCRowIndex].Cells[colIndex].Value = totalC.ToString();
            currentGrid.Rows[totalDRowIndex].Cells[colIndex].Value = totalD.ToString();
            currentGrid.Rows[totalAnswerRowIndex].Cells[colIndex].Value = count.ToString();
            currentGrid.Rows[averageRowIndex].Cells[colIndex].Value = (total / count).ToString();

            //questionGrid[colIndex, averageRowIndex].Style.BackColor = Color.Red;
           

            

            PersonCellFormatting(colIndex);
            
        }

        public void LoadChartData()
        {
            double numA = 0;
            double numB = 0;
            double numC = 0;
            double numD = 0;
            string temp = "";
            
            for (int i = 1; i <= (currentGrid.ColumnCount - 7); i++)
            {
                if (currentGrid.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = currentGrid.Rows[totalARowIndex].Cells[i].Value.ToString();
                    numA += Convert.ToDouble(temp);
                    temp = currentGrid.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    numB += Convert.ToDouble(temp);
                    temp = currentGrid.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    numC += Convert.ToDouble(temp);
                    temp = currentGrid.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    numD += Convert.ToDouble(temp);
                }
            }
            
              
              // int temp = (int)Convert.ToInt32(totalComm);
              // num += temp;
            //Console.WriteLine(numA.ToString());
            currentChart.Series["BusiCurrent"].Points[0].SetValueXY("Commodity", numA);
            currentChart.Series["BusiCurrent"].Points[1].SetValueXY("Utility", numB);
            currentChart.Series["BusiCurrent"].Points[2].SetValueXY("Parter", numC);
            currentChart.Series["BusiCurrent"].Points[3].SetValueXY("Enabler", numD);
        }

        public void PersonCellFormatting(int index)
        {
            string tempAvg = (string)currentGrid.Rows[averageRowIndex].Cells[index].Value;
            float average = (float)Convert.ToDouble(tempAvg);
            if(average < 3)
                currentGrid[index, averageRowIndex].Style.BackColor = Color.IndianRed;
            else
                currentGrid[index, averageRowIndex].Style.BackColor = Color.SeaGreen;
        }
        public void QuestionCellFormatting(int index)
        {
            string tempAvg = (string)currentGrid.Rows[index].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value;
            float average = (float)Convert.ToDouble(tempAvg);
            if (average < 3)
                currentGrid[averageIndex + (currentGrid.ColumnCount - 7), index].Style.BackColor = Color.IndianRed;
            else
                currentGrid[averageIndex + (currentGrid.ColumnCount - 7), index].Style.BackColor = Color.SeaGreen;
        }

        private void itRadioButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridView view in grids)
            {
                view.Visible = false;
            }
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            itCurrentGraph.Visible = true;
            currentChart = itCurrentGraph;
            questionGridITCurrent.Visible = true;
            currentGrid = questionGridITCurrent;
            UpdateCupeScore();

        }

        private void busiRadioButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridView view in grids)
            {
                view.Visible = false;
            }
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            busiCurrentGraph.Visible = true;
            currentChart = busiCurrentGraph;
            questionGridBusinessCurrent.Visible = true;
            currentGrid = questionGridBusinessCurrent;
            UpdateCupeScore();
        }



        private void cellClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = currentGrid.HitTest(e.X, e.Y);

                Console.WriteLine((hit.ColumnIndex + "   " + hit.RowIndex).ToString());
                
            }
            
        }

        private void busiFutureRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridView view in grids)
            {
                view.Visible = false;
            }
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            busiFutureGraph.Visible = true;
            currentChart = busiFutureGraph;
            questionGridBusiFuture.Visible = true;
            currentGrid = questionGridBusiFuture;
            UpdateCupeScore();
        }

        private void itFutureRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridView view in grids)
            {
                view.Visible = false;
            }
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            itFutureGraph.Visible = true;
            currentChart = itFutureGraph;
            questionGridITFuture.Visible = true;
            currentGrid = questionGridITFuture;
            UpdateCupeScore();
        }

        private void questionInfoPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void filterQuestionsGo_Click(object sender, EventArgs e)
        {
            try
            {

                if (questionFilter.Text == "Highest Cupe Score")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, averageIndex);
                }
                if (questionFilter.Text == "Lowest Cupe Score")
                {
                    FilterQuestionByLowestAnswer(questionFilterAmount.Text, averageIndex);
                }
                if (questionFilter.Text == "Most Commodity")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, totalAIndex);
                }
                if (questionFilter.Text == "Most Utility")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, totalBIndex);
                }
                if (questionFilter.Text == "Most Partner")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, totalCIndex);
                }
                if (questionFilter.Text == "Most Enabler")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, totalDIndex);
                }
                if (questionFilter.Text == "Least Commodity")
                {
                    FilterQuestionByLowestAnswer(questionFilterAmount.Text, totalAIndex);
                }
                if (questionFilter.Text == "Least Utility")
                {
                    FilterQuestionByLowestAnswer(questionFilterAmount.Text, totalBIndex);
                }
                if (questionFilter.Text == "Least Partner")
                {
                    FilterQuestionByLowestAnswer(questionFilterAmount.Text, totalCIndex);
                }
                if (questionFilter.Text == "Least Enabler")
                {
                    FilterQuestionByLowestAnswer(questionFilterAmount.Text, totalDIndex);
                }
            }
            catch
            {
            }
        }

        public void FilterQuestionByHighestScore(string amount, int index)
        {
            if(toRemove != null)
                questionInfoPanel.Controls.Remove(toRemove);
            int num = Convert.ToInt32(amount);
            List<Tuple<float, int>> values = new List<Tuple<float, int>>();
            string value;

            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                if (row.Cells[index + currentGrid.ColumnCount - 7].Value != null)
                {
                    value = row.Cells[index + currentGrid.ColumnCount - 7].Value.ToString();
                    float floatValue = (float)Convert.ToDouble(value);
                    values.Add(new Tuple<float, int>(floatValue, row.Index));
                    //row.
                }
            }

            DataGridView view = new DataGridView();
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
                //col.Width = 50;

            }

            values.Sort();

            values.Reverse();

           // DataGridView grid = new DataGridView();
            
            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
            }
            //currentGrid.Columns[0].cl

            
            questionInfoPanel.Controls.Add(view);
            view.Location = new Point(10, 35);
            view.Width = 550;
            view.Height = 130;
            

        }
        public void FilterQuestionByLowestScore(string amount, int index)
        {

            if(toRemove != null)
                questionInfoPanel.Controls.Remove(toRemove);
            int num = Convert.ToInt32(amount);
            List<Tuple<float, int>> values = new List<Tuple<float, int>>();
            string value;

            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                value = row.Cells[index + currentGrid.ColumnCount - 7].Value.ToString();
                float floatValue = (float)Convert.ToDouble(value);
                values.Add(new Tuple<float, int>(floatValue, row.Index));
                //row.
            }

            DataGridView view = new DataGridView();
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
                //col.Width = 80;

            }

            values.Sort();

           // values.Reverse();

           // DataGridView grid = new DataGridView();
            
            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
            }
            //currentGrid.Columns[0].cl

            questionInfoPanel.Controls.Add(view);
            view.Location = new Point(10, 35);
            view.Width = 550;
            view.Height = 130;
            
         }

        public void FilterQuestionByLowestAnswer(string amount, int index)
        {
            if (toRemove != null)
                questionInfoPanel.Controls.Remove(toRemove);
            int num = Convert.ToInt32(amount);
            List<Tuple<float, int>> values = new List<Tuple<float, int>>();
            string value;

            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                if (row.Cells[index + currentGrid.ColumnCount - 7].Value != null)
                {
                    value = row.Cells[index + currentGrid.ColumnCount - 7].Value.ToString();
                    float floatValue = (float)Convert.ToDouble(value);
                    values.Add(new Tuple<float, int>(floatValue, row.Index));
                    //row.
                }
            }

            DataGridView view = new DataGridView();
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
                //col.Width = 50;

            }

            values.Sort();

            //values.Reverse();

            // DataGridView grid = new DataGridView();

            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
            }
            //currentGrid.Columns[0].cl


            questionInfoPanel.Controls.Add(view);
            view.Location = new Point(10, 35);
            view.Width = 550;
            view.Height = 130;


        }

        private void iTStakeHoldersCurrentFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentFloats = new List<float>();
            List<float> futureFloats = new List<float>();
            int count = 0;
            //System.Diagnostics.Trace.WriteLine("count: " + questionGridITCurrent.RowCount);
            System.Diagnostics.Trace.WriteLine("column: " + questionGridITCurrent.ColumnCount);
            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                //currentFloats.Add((float)row.Cells[averageIndex + (questionGridITCurrent.ColumnCount - 7)].Value);
                //if (row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value == null)
                //if (count >= 20)
                if (row.Cells[0].Value == null)
                    break;
                currentFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + (questionGridITCurrent.ColumnCount - 7)].Value));
                count++;
                
            }
            count = 0;
            foreach (DataGridViewRow row in questionGridITFuture.Rows)
            {
                //if (row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value == null)
                //if (count >= 20)
                
                if (row.Cells[0].Value == null)
                {
                    break;
                }
                    //break;
                futureFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value));
                count++;
                //System.Diagnostics.Trace.WriteLine("count: " + count.ToString());
            }

            CreateChart(currentFloats, futureFloats, "IT StakeHolders Current/Future Comparison");
        }

        public void CreateChart(List<float> current, List<float> future, string name)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = name;
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
            newChart.Text = name;
            char[] slashSeparator = new char[] { '/' };
            string[] result;
            result = name.Split(slashSeparator, StringSplitOptions.None);
            string newStr = "";
            foreach (string s in result)
            {
                System.Console.WriteLine(s);
                newStr += s;
            }
            newChart.Name = newStr;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisX.Title = "Question";
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;
            newChart.ChartAreas["chart1"].AxisX.Interval = 1;
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;
            //newChart.Legends["legend"].LegendStyle = LegendStyle.Table;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = name;
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("Current");
            newChart.Series["Current"].ChartArea = "chart1";
            newChart.Series["Current"].ChartType = SeriesChartType.Bar;
            newChart.Series["Current"].IsValueShownAsLabel = true;
            newChart.Series["Current"].IsVisibleInLegend = true;
            newChart.Series["Current"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Future");
            newChart.Series["Future"].ChartArea = "chart1";
            newChart.Series["Future"].ChartType = SeriesChartType.Bar;
            newChart.Series["Future"].IsValueShownAsLabel = true;
            newChart.Series["Future"].IsVisibleInLegend = true;
            newChart.Series["Future"].YValueType = ChartValueType.Double;

            int currentCount = current.Count;
            int futureCount = future.Count;

            //System.Diagnostics.Trace.WriteLine("current: " + currentCount.ToString() + "  future: " + futureCount.ToString());

            for (int i = 0; i < currentCount; i++)
            {
                newChart.Series["Current"].Points.AddXY(ClientDataControl.cupeQuestions[i].QuestionText.ToString(), current[i]);

            }

            for (int i = 0; i < futureCount; i++)
            {
                newChart.Series["Future"].Points.AddXY(ClientDataControl.cupeQuestions[i].QuestionText.ToString(), future[i]);
            }

            //newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            //newChart.SaveImage(Application.StartupPath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }

        private void businessLeadersCurrentFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentFloats = new List<float>();
            List<float> futureFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridBusinessCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                count++;
                currentFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value));
            }
            count = 0;
            foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                count++;
                futureFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusiFuture.ColumnCount - 7].Value));
            }

            CreateChart(currentFloats, futureFloats, "Business Leaders Current/Future Comparison");
        }

        private void iTVsBusinessLeadersCurrentComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentBusinessFloats = new List<float>();
            List<float> currentITFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridBusinessCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                count++;
                currentBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value));
            }
            count = 0;
            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                count++;
                currentITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITCurrent.ColumnCount - 7].Value));
            }

            CreateChartITVsBussiness(currentBusinessFloats, currentITFloats, "IT vs Business Leaders Current Comparison");
        }

        private void iTVsBusinessFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> futureBusinessFloats = new List<float>();
            List<float> futureITFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                count++;
                futureBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusiFuture.ColumnCount - 7].Value));
            }
            int countB = 0;
            foreach (DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                countB++;
                futureITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value));
            }

            if (count > 0 || countB > 0)
                CreateChartITVsBussiness(futureBusinessFloats, futureITFloats, "IT vs Business Leaders Future Comparison");
        }

        public void CreateChartITVsBussiness(List<float> current, List<float> future, string name)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = name;
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
            newChart.Text = name;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisX.Title = "Question";
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;
            newChart.ChartAreas["chart1"].AxisX.Interval = 1;
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;
            //newChart.Legends["legend"].LegendStyle = LegendStyle.Table;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = name;
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("Business");
            newChart.Series["Business"].ChartArea = "chart1";
            newChart.Series["Business"].ChartType = SeriesChartType.Bar;
            newChart.Series["Business"].IsValueShownAsLabel = true;
            newChart.Series["Business"].IsVisibleInLegend = true;
            newChart.Series["Business"].YValueType = ChartValueType.Double;
            newChart.Series.Add("IT");
            newChart.Series["IT"].ChartArea = "chart1";
            newChart.Series["IT"].ChartType = SeriesChartType.Bar;
            newChart.Series["IT"].IsValueShownAsLabel = true;
            newChart.Series["IT"].IsVisibleInLegend = true;
            newChart.Series["IT"].YValueType = ChartValueType.Double;

            int currentCount = current.Count;
            int futureCount = future.Count;

            for (int i = 0; i < currentCount; i++)
            {
                newChart.Series["Business"].Points.AddXY(ClientDataControl.cupeQuestions[i].QuestionText.ToString(), current[i]);
            }

            for (int i = 0; i < futureCount; i++)
            {
                newChart.Series["IT"].Points.AddXY(ClientDataControl.cupeQuestions[i].QuestionText.ToString(), future[i]);
            }

            //newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            //newChart.SaveImage(Application.StartupPath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }

        private void participantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changesMade = true;
            saveCupeAnswersToClientDataControl();
            var FD = new EditParticipants();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                removePersonColumns();
                loadColumnNames();
                LoadAnswersFromDataControl();
                
                if(HelpEnabled && HelpCurrentStep == 1)
                {
                    HelpCurrentStep = 2;
                    StartTutorialMode();
                }
                return;
            }

        }

        private void loadColumnNames()
        {

            //grids.Add(questionGridITCurrent);
            //grids.Add(questionGridBusinessCurrent);
            //grids.Add(questionGridITFuture);
            //grids.Add(questionGridBusiFuture);

            
            foreach(Person person in ClientDataControl.GetParticipants())
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();

                col.HeaderText =  person.Name;                   
                col.Name = person.Name;
                col.Width = 100;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = person.Name;
                col2.Name = person.Name;
                col2.Width = 100;
                col2.SortMode = DataGridViewColumnSortMode.NotSortable;



                if (person.Type == Person.EmployeeType.IT)
                {

                    if (isAnonymous)
                    {
                        col.HeaderText = "Person " + (questionGridITCurrent.Columns.Count - 6).ToString();
                        col2.HeaderText = "Person " + (questionGridITCurrent.Columns.Count - 6).ToString();
                    }

                    questionGridITCurrent.Columns.Insert(questionGridITCurrent.ColumnCount - 6, col);
                    questionGridITFuture.Columns.Insert(questionGridITFuture.ColumnCount - 6, col2);
                }
                if (person.Type == Person.EmployeeType.Business)
                {

                    if (isAnonymous)
                    {
                        col.HeaderText = "Person " + (questionGridBusinessCurrent.Columns.Count - 6).ToString();
                        col2.HeaderText = "Person " + (questionGridBusinessCurrent.Columns.Count - 6).ToString();
                    }

                    questionGridBusinessCurrent.Columns.Insert(questionGridBusinessCurrent.ColumnCount - 6, col);
                    questionGridBusiFuture.Columns.Insert(questionGridBusiFuture.ColumnCount - 6, col2);
                }
            }
        }
        
        //load the data in the cupe Charts to the ClientDataControl
        private void loadCupeDataValues()
        {


        }

        //Saves the table values to the clientdatacontrol
        private void saveCupeDataValues()
        {

            //ITCurrent
            foreach( DataGridViewColumn column in questionGridITCurrent.Columns)
            {
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }
                
                if( currentPerson != null)
                {
                    currentPerson.cupeDataHolder.CurrentAnswers.Clear();
                    foreach (DataGridViewRow row in questionGridITCurrent.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[column.Index].Value != null)
                        {
                            currentPerson.cupeDataHolder.CurrentAnswers.Add(
                                row.Cells[0].Value.ToString(), 
                                 row.Cells[column.Index].Value.ToString()[0]);
                        }
                        else if (row.Cells[0].Value == null)
                        {
                            break;
                        }
                    }
                }
            }

            //ITFuture
            foreach (DataGridViewColumn column in questionGridITFuture.Columns)
            {
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }

                if (currentPerson != null)
                {
                    currentPerson.cupeDataHolder.FutureAnswers.Clear();
                    foreach (DataGridViewRow row in questionGridITFuture.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[column.Index].Value != null)
                        {
                            currentPerson.cupeDataHolder.FutureAnswers.Add(
                                row.Cells[0].Value.ToString(),
                                 row.Cells[column.Index].Value.ToString()[0]);
                        }
                        else if (row.Cells[0].Value == null)
                        {
                            break;
                        }
                    }
                }
            }

            //BusiCurrent
            foreach (DataGridViewColumn column in questionGridBusinessCurrent.Columns)
            {
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }

                if (currentPerson != null)
                {
                    currentPerson.cupeDataHolder.CurrentAnswers.Clear();
                    foreach (DataGridViewRow row in questionGridBusinessCurrent.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[column.Index].Value != null)
                        {
                            currentPerson.cupeDataHolder.CurrentAnswers.Add(
                                row.Cells[0].Value.ToString(),
                                 row.Cells[column.Index].Value.ToString()[0]);
                        }
                        else if (row.Cells[0].Value == null)
                        {
                            break;
                        }
                    }
                }
            }
            //BusiFuture
            foreach (DataGridViewColumn column in questionGridBusiFuture.Columns)
            {
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }

                if (currentPerson != null)
                {
                    currentPerson.cupeDataHolder.FutureAnswers.Clear();
                    foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[column.Index].Value != null)
                        {
                            currentPerson.cupeDataHolder.FutureAnswers.Add(
                                row.Cells[0].Value.ToString(),
                                 row.Cells[column.Index].Value.ToString()[0]);
                        }
                        else if (row.Cells[0].Value == null)
                        {
                            break;
                        }
                    }
                }
            }

        }
        private void removePersonColumns()
        {
            for( int i=1; i<= questionGridITCurrent.ColumnCount - 7; i=1)
            {
                questionGridITCurrent.Columns.RemoveAt(i);
            }
            for (int i = 1; i <= questionGridITFuture.ColumnCount - 7; i=1)
            {
                questionGridITFuture.Columns.RemoveAt(i);
            }
            for (int i = 1; i <= questionGridBusinessCurrent.ColumnCount - 7; i=1)
            {
                questionGridBusinessCurrent.Columns.RemoveAt(i);
            }
            for (int i = 1; i <= questionGridBusiFuture.ColumnCount - 7; i=1)
            {
                questionGridBusiFuture.Columns.RemoveAt(i);
            }

        }

        private void saveCupeAnswersToClientDataControl()
        {
            ClientDataControl.SetCupeAnswers(new List<CupeData>());
            foreach ( Person currentPerson in ClientDataControl.GetParticipants())
            {
                ClientDataControl.AddCupeAnswers(currentPerson.cupeDataHolder);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Store the cupe answeres in each person's cupe data holder
            saveCupeDataValues();
            //Store everyone's answers in the clientdatacontroller
            saveCupeAnswersToClientDataControl();
        }

        private void createSurveyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyGenerator generator = new SurveyGenerator();
            generator.CreateCupeSurvey(ClientDataControl.GetParticipants(), questions);
        }

        private void openSurveysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SurveyReader = new SurveyReader();

            SurveyReader.ReadSurveyCUPE(ClientDataControl.GetParticipants());
            LoadAnswersFromDataControl();
        }

        private void iTCapabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RunITCap));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RunITCap()
        {
            Application.Run(new ITCapTool());
        }


        /* Get the tree node under the mouse pointer and 
   save it in the mySelectedNode variable. */

        TreeNode mySelectedNode;
        int indexCurrentQuestionNode;

        private void QuestionView_MouseDown(object sender,
  System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                mySelectedNode = QuestionView.GetNodeAt(e.X, e.Y);
                QuestionView.SelectedNode = mySelectedNode;
                QuestionView.LabelEdit = true;
                if (!mySelectedNode.IsEditing)
                {
                    mySelectedNode.BeginEdit();
                }
            }
            catch
            {

            }
        }


        private void QuestionView_AfterLabelEdit(object sender,
         System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            changesMade = true;
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@' }) == -1)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);

                        CupeQuestionStringData data = new CupeQuestionStringData();

                        if ( ClientDataControl.cupeQuestions[indexCurrentQuestionNode].QuestionText == e.Node.Text)
                        {
                            ClientDataControl.cupeQuestions[indexCurrentQuestionNode].QuestionText = e.Label;
                        }
                        else if (ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceA == e.Node.Text )
                        {
                            ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceA = e.Label;
                        }
                        else if (ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceB == e.Node.Text)
                        {
                            ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceB = e.Label;
                        }
                        else if (ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceC == e.Node.Text)
                        {
                            ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceC = e.Label;
                        }
                        else if (ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceD == e.Node.Text)
                        {
                            ClientDataControl.cupeQuestions[indexCurrentQuestionNode].ChoiceD = e.Label;
                        }


                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show("Invalid multiple choice label.\n" +
                           "The invalid characters are: '@',",
                           "Node Label Edit");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    MessageBox.Show("Invalid multiple choice label.\nThe label cannot be blank",
                       "Node Label Edit");
                    e.Node.BeginEdit();
                }

            }
        }

        private void LoadCupeQuestionsFromDocument()
        {
            CupeQuestionStringData data = new CupeQuestionStringData();
            string path;
            if(is20Question)
            {
                path = @"Resources/Questions.txt";
            }
            else
            {
                path = @"Resources/Questions10.txt";
            }
            foreach (string line in File.ReadLines(path))
            {
                if (!String.IsNullOrEmpty(line))
                {
                    if (line[0] == 'A' && line[1] == '.')
                    {
                        data.ChoiceA = line;
                    }
                    else if (line[0] == 'B' && line[1] == '.')
                    {
                        data.ChoiceB = line;
                    }
                    else if (line[0] == 'C' && line[1] == '.')
                    {
                        data.ChoiceC = line;
                    }
                    else if (line[0] == 'D' && line[1] == '.')
                    {
                        data.ChoiceD = line;
                    }
                    else
                    {
                        data.QuestionText = line;
                    }
                }
                else if (String.IsNullOrEmpty(line))
                {
                    ClientDataControl.AddCupeQuestion(data);
                    data = new CupeQuestionStringData();
                }
            }

        }

        

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientDataControl.SendEmailButton_Click();
        }

        private void iTCapabilityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNITCap));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNITCap()
        {
            Application.Run(new ITCapTool());
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

        private int numberYouChoose = 0;
        private bool choose = false;
        private void questionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int countA = 0;
            int countB = 0;
            int countC = 0;
            int countD = 0;
            int count = 0;

            List<float> futureBusinessFloats = new List<float>();
            List<float> futureITFloats = new List<float>();
            List<float> currentBusinessFloats = new List<float>();
            List<float> currentITFloats = new List<float>();

            foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                countA++;
                futureBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusiFuture.ColumnCount - 7].Value));
            }
            count = countA;
            foreach (DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                countB++;
                futureITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value));
            }
            if (count < countB)
                count = countB;
            foreach (DataGridViewRow row in questionGridBusinessCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                countC++;
                currentBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value));
            }
            if (count < countC)
                count = countC;
            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                countD++;
                currentITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITCurrent.ColumnCount - 7].Value));
            }
            if (count < countD)
                count = countD;

            choose = false;

            if (questionGridBusiFuture.ColumnCount == 7 && questionGridBusinessCurrent.ColumnCount == 7 &&
                questionGridITCurrent.ColumnCount == 7 && questionGridITFuture.ColumnCount == 7)
            {
                countA = 0;
                countB = 0;
                countC = 0;
                countD = 0;
            }

            else if (questionGridBusiFuture.ColumnCount == 7 && questionGridBusinessCurrent.ColumnCount == 7)
            {
                countA = 0;
                countB = 0;
            }

            else if (questionGridITCurrent.ColumnCount == 7 && questionGridITFuture.ColumnCount == 7)
            {
                countC = 0;
                countD = 0;
            }

            Question(countA, countB, countC, countD, count, futureBusinessFloats, currentBusinessFloats, futureITFloats, currentITFloats);


            //QuestionsChart(futureBusinessFloats, currentBusinessFloats, futureITFloats, currentITFloats, numberYouChoose);
        }

        private Form form = new Form();
        private Button btnOpen = new Button();
        private Label la = new Label();
        private ComboBox combo = new ComboBox();

        List<float> FBF = new List<float>();
        List<float> FITF = new List<float>();
        List<float> CBF = new List<float>();
        List<float> CITF = new List<float>();

        public void Question(int a, int b, int c, int d, int cnt, List<float> fuBusiness, List<float> curBusiness, List<float> fuIT, List<float> curIT)
        {
            form = new Form();
            btnOpen = new Button();
            la = new Label();
            combo = new ComboBox();
            //System.Diagnostics.Trace.WriteLine("hello");
            
            var list = new List<int>() { a, b, c, d, cnt };
            var count = list.Max();


            if (a == 0 && b == 0 && c == 0 && d == 0)
            {
                MessageBox.Show("There are no participants");
            }
            else
            {
                if (a == 0 && b == 0)
                    MessageBox.Show("There are no Business participants");
                else if (c == 0 && d == 0)
                    MessageBox.Show("There are no IT participants");

                form.Size = new Size(250, 100);
                form.AutoSize = true;
                form.Text = "CUPE Question";

                form.Show();

                la.Parent = form;
                la.Text = "Question";
                la.AutoSize = true;
                la.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                la.Location = new Point(10, 10);

                btnOpen.Parent = form;
                btnOpen.Text = "Open";
                btnOpen.AutoSize = true;
                btnOpen.Font = new Font("Microsoft Sans Serif", 12);
                btnOpen.Location = new Point(form.Size.Width / 2 - btnOpen.Width / 2, form.Size.Height / 2 - 10);

                //combo.DropDownStyle = ComboBoxStyle.Simple;
                combo.Parent = form;
                combo.Size = new Size(50, la.Height);
                //combo.MaxLength = 10;
                combo.Location = new Point(form.Width - combo.Width - 30, 10);
                for (int i = 0; i < count; i++)
                {
                    combo.Items.Add((i + 1).ToString());
                }

                btnOpen.Click += new EventHandler(btnOpen_Click);
                FBF = fuBusiness;
                CBF = curBusiness;
                FITF = fuIT;
                CITF = curIT;
            }
        }

        public void ChartCall()
        {
            if (choose)
            {
                QuestionsChart(FBF, CBF, FITF, CITF, numberYouChoose);
            }
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("open clicked");
            numberYouChoose = (int)Convert.ToInt32(combo.SelectedItem);
            //System.Diagnostics.Trace.WriteLine("num: " + numberYouChoose.ToString());
            choose = true;
            ChartCall();
        }

        public void QuestionsChart(List<float> fuBusiness, List<float> curBusiness, List<float> fuIT, List<float> curIT, int index)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            newChart.Parent = formChart;

            newChart.Size = new Size(400, 400);
            newChart.Visible = true;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.Name = "Question" + index.ToString();

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].AxisX.Title = "Question";
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
            //newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "CUPE Profile Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;
            //newChart.Legends["legend"].LegendStyle = LegendStyle.Table;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = "Q" + index + ": IT and Business Responses";
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);


            newChart.Series.Add("Business Future");
            newChart.Series["Business Future"].ChartArea = "chart1";
            newChart.Series["Business Future"].ChartType = SeriesChartType.Bar;
            newChart.Series["Business Future"].IsValueShownAsLabel = true;
            newChart.Series["Business Future"].IsVisibleInLegend = true;
            newChart.Series["Business Future"].YValueType = ChartValueType.Double;

            newChart.Series.Add("Business Current");
            newChart.Series["Business Current"].ChartArea = "chart1";
            newChart.Series["Business Current"].ChartType = SeriesChartType.Bar;
            newChart.Series["Business Current"].IsValueShownAsLabel = true;
            newChart.Series["Business Current"].IsVisibleInLegend = true;
            newChart.Series["Business Current"].YValueType = ChartValueType.Double;

            newChart.Series.Add("IT Future");
            newChart.Series["IT Future"].ChartArea = "chart1";
            newChart.Series["IT Future"].ChartType = SeriesChartType.Bar;
            newChart.Series["IT Future"].IsValueShownAsLabel = true;
            newChart.Series["IT Future"].IsVisibleInLegend = true;
            newChart.Series["IT Future"].YValueType = ChartValueType.Double;

            newChart.Series.Add("IT Current");
            newChart.Series["IT Current"].ChartArea = "chart1";
            newChart.Series["IT Current"].ChartType = SeriesChartType.Bar;
            newChart.Series["IT Current"].IsValueShownAsLabel = true;
            newChart.Series["IT Current"].IsVisibleInLegend = true;
            newChart.Series["IT Current"].YValueType = ChartValueType.Double;

            index--;

            newChart.Series["Business Future"].Points.AddY(fuBusiness[index]);
            newChart.Series["Business Current"].Points.AddY(curBusiness[index]);
            newChart.Series["IT Future"].Points.AddY(fuIT[index]);
            newChart.Series["IT Current"].Points.AddY(curIT[index]);

            //newChart.SaveImage(Application.StartupPath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }

        private void LoadAnswersFromDataControl()
        {

            foreach (DataGridViewColumn column in questionGridITCurrent.Columns)
            {
                if (column.HeaderText == "Questions")
                {
                    continue;
                }
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }

                if (currentPerson != null)
                {
                    if (currentPerson.Type == Person.EmployeeType.IT)
                    {
                        currentGrid = questionGridITCurrent;
                        currentChart = itCurrentGraph;
                        foreach (DataGridViewRow row in questionGridITCurrent.Rows)
                        {
                            if (row.Cells[0].Value == null)
                            {
                                break;
                            }
                            if (!currentPerson.cupeDataHolder.CurrentAnswers.ContainsKey(row.Cells[0].Value.ToString()))
                            {
                                continue;
                            }
                            row.Cells[column.Index].Value = currentPerson.cupeDataHolder.CurrentAnswers[row.Cells[0].Value.ToString()];
                            ChangeTotalsByRow(row.Index);
                        }
                        ChangeTotalsByColumn(column.Index, 1);
                        LoadChartData();
                        UpdateCupeScore();

                        currentGrid = questionGridITFuture;
                        currentChart = itFutureGraph;
                        foreach (DataGridViewRow row in questionGridITFuture.Rows)
                        {
                            if (row.Cells[0].Value == null)
                            {
                                break;
                            }
                            if (!currentPerson.cupeDataHolder.FutureAnswers.ContainsKey(row.Cells[0].Value.ToString()))
                            {
                                continue;
                            }
                            row.Cells[column.Index].Value = currentPerson.cupeDataHolder.FutureAnswers[row.Cells[0].Value.ToString()];
                            ChangeTotalsByRow(row.Index);
                        }
                        ChangeTotalsByColumn(column.Index, 1);
                        LoadChartData();
                        UpdateCupeScore();
                    }
                }
            }

            foreach (DataGridViewColumn column in questionGridBusinessCurrent.Columns)
            {
                if (column.HeaderText == "Questions")
                {
                    continue;
                }
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.Name == column.Name).Single();
                }
                catch
                {

                }

                if (currentPerson != null)
                {
                    if (currentPerson.Type == Person.EmployeeType.Business)
                    {
                        currentGrid = questionGridBusinessCurrent;
                        currentChart = busiCurrentGraph;
                        foreach (DataGridViewRow row in questionGridBusinessCurrent.Rows)
                        {
                            if(row.Cells[0].Value == null)
                            { 
                                break;
                            }
                            if (!currentPerson.cupeDataHolder.CurrentAnswers.ContainsKey(row.Cells[0].Value.ToString()))
                            {
                                continue;
                            }
                            row.Cells[column.Index].Value = currentPerson.cupeDataHolder.CurrentAnswers[row.Cells[0].Value.ToString()];
                            ChangeTotalsByRow(row.Index);
                        }
                        ChangeTotalsByColumn(column.Index, 1);
                        LoadChartData();
                        UpdateCupeScore();


                        currentGrid = questionGridBusiFuture;
                        currentChart = busiFutureGraph;
                        foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
                        {
                            if (row.Cells[0].Value == null)
                            {
                                break;
                            }
                            if (!currentPerson.cupeDataHolder.FutureAnswers.ContainsKey(row.Cells[0].Value.ToString()))
                            {
                                continue;
                            }
                            row.Cells[column.Index].Value = currentPerson.cupeDataHolder.FutureAnswers[row.Cells[0].Value.ToString()];
                            ChangeTotalsByRow(row.Index);
                        }
                        ChangeTotalsByColumn(column.Index, 1);
                        LoadChartData();
                        UpdateCupeScore();
                    }
                }
            }

        }

        private void participantNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isAnonymous == true)
            {
                isAnonymous = false;
                foreach (DataGridView grid in grids)
                {
                    for (int i = 1; i < grid.Columns.Count - 6; i++)
                    {
                        grid.Columns[i].HeaderText = grid.Columns[i].Name;
                    }
                }
            }
            else
            {
                isAnonymous = true;
                foreach (DataGridView grid in grids)
                {
                    for (int i = 1; i < grid.Columns.Count - 6; i++)
                    {
                        grid.Columns[i].HeaderText = "Person " + i;
                    }
                }
            }



        }

        //Change the number of questions in the Cupe Form
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            changesMade = true;
            is20Question = false;
            ClientDataControl.cupeQuestions.Clear();
            LoadCupeQuestionsFromDocument();
            SetLastTenColumnVisibility(false);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            changesMade = true;
            is20Question = true;
            ClientDataControl.cupeQuestions.Clear();
            LoadCupeQuestionsFromDocument();
            SetLastTenColumnVisibility(true);
        }

        private void SetLastTenColumnVisibility(bool isShown)
        {
            foreach ( DataGridView grid in grids)
            {
                for(int index = 10; index < 20; index++)
                {
                    grid.Rows[index].Visible = isShown;
                }
            }
        }

        private void questionGridITFuture_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridITFuture;
            currentChart = itFutureGraph;
            GridClicked(sender, e);
        }

        private void questionGridBusiFuture_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridBusiFuture;
            currentChart = busiFutureGraph;
            GridClicked(sender, e);
        }

        private void questionGridITCurrent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridITCurrent;
            currentChart = itCurrentGraph;
            GridClicked(sender, e);
        }

        private void questionGridBusinessCurrent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridBusinessCurrent;
            currentChart = busiCurrentGraph;
            GridClicked(sender, e);
        }

        private void GridClicked(object sender, DataGridViewCellEventArgs e)
        {
             string RowName = "";

            try
            {
                RowName = this.currentGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch
            {

            }
            QuestionView.Nodes.Clear();
            var questionsText = ClientDataControl.GetCupeQuestions();
            if (RowName.Contains("Question"))
            {
                var index = e.RowIndex;
                indexCurrentQuestionNode = index;

                TreeNode node = new TreeNode();
                node.Text = ClientDataControl.GetCupeQuestions()[index].QuestionText;

                TreeNode nodeA = new TreeNode();
                nodeA.Text = ClientDataControl.GetCupeQuestions()[index].ChoiceA;

                TreeNode nodeB = new TreeNode();
                nodeB.Text = ClientDataControl.GetCupeQuestions()[index].ChoiceB;

                TreeNode nodeC = new TreeNode();
                nodeC.Text = ClientDataControl.GetCupeQuestions()[index].ChoiceC;

                TreeNode nodeD = new TreeNode();
                nodeD.Text = ClientDataControl.GetCupeQuestions()[index].ChoiceD;

                node.Nodes.Add(nodeA);
                node.Nodes.Add(nodeB);
                node.Nodes.Add(nodeC);
                node.Nodes.Add(nodeD);
                QuestionView.Nodes.Add(node);
                QuestionView.ExpandAll();


            }
        }

        private void ShowChangesMadeDialog()
        {
            Form dialog = new ChangesMadeForm();
            dialog.ShowDialog();

            if(dialog.DialogResult == DialogResult.Yes)
            {
                saveCupeDataValues();
                saveCupeAnswersToClientDataControl();
            }

        }

        private void CUPETool_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesMade)
            {
                ShowChangesMadeDialog();
            }
        }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double numA = 0;
            double numB = 0;
            double numC = 0;
            double numD = 0;
            double ITc = 0;
            double ITu = 0;
            double ITp = 0;
            double ITe = 0;
            double total = 0;
            double avgBusC, avgBusU, avgBusP, avgBusE;
            double avgITc, avgITu, avgITp, avgITe;
            //double avgC, avgU, avgP, avgE;
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Business & IT CUPE Responses Current";
            List<double> busCurInfo = new List<double>();
            List<double> ITCurInfo = new List<double>();
            List<double> totalCurInfo = new List<double>();

            for (int i = 1; i <= (questionGridBusinessCurrent.ColumnCount - 7); i++)
            {
                if (questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    numA += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    numB += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    numC += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    numD += Convert.ToDouble(temp);
                }
            }
            total = numA + numB + numC + numD;
            avgBusC = numA / total * 100;
            avgBusU = numB / total * 100;
            avgBusP = numC / total * 100;
            avgBusE = numD / total * 100;
            busCurInfo.Add(avgBusC);
            busCurInfo.Add(avgBusU);
            busCurInfo.Add(avgBusP);
            busCurInfo.Add(avgBusE);

            for (int i = 1; i <= (questionGridITCurrent.ColumnCount - 7); i++)
            {
                if (questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe += Convert.ToDouble(temp);
                }
            }

            total = ITc + ITu + ITp + ITe;
            avgITc = ITc / total * 100;
            avgITu = ITu / total * 100;
            avgITp = ITp / total * 100;
            avgITe = ITe / total * 100;
            ITCurInfo.Add(avgITc);
            ITCurInfo.Add(avgITu);
            ITCurInfo.Add(avgITp);
            ITCurInfo.Add(avgITe);

            /*total = numA + numB + numC + numD + ITc + ITu + ITp + ITe;
            avgC = (numA + ITc) / total * 100;
            avgU = (numB + ITu) / total * 100;
            avgP = (numC + ITp) / total * 100;
            avgE = (numD + ITe) / total * 100;

            totalCurInfo.Add(avgC);
            totalCurInfo.Add(avgU);
            totalCurInfo.Add(avgP);
            totalCurInfo.Add(avgE);*/

            BusITResponsesChart(busCurInfo, ITCurInfo, cupe, name);

            //CurrentBusITChart chart = new CurrentBusITChart();
        }


        private void futureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double numA = 0;
            double numB = 0;
            double numC = 0;
            double numD = 0;
            double ITc = 0;
            double ITu = 0;
            double ITp = 0;
            double ITe = 0;
            double total = 0;
            double avgBusC, avgBusU, avgBusP, avgBusE;
            double avgITc, avgITu, avgITp, avgITe;
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Business & IT CUPE Responses Future";
            List<double> busFutureInfo = new List<double>();
            List<double> ITFutureInfo = new List<double>();
            //List<double> totalFutureInfo = new List<double>();

            for (int i = 1; i <= (questionGridBusiFuture.ColumnCount - 7); i++)
            {
                if (questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    numA += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    numB += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    numC += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    numD += Convert.ToDouble(temp);
                }
            }
            total = numA + numB + numC + numD;
            avgBusC = numA / total * 100;
            avgBusU = numB / total * 100;
            avgBusP = numC / total * 100;
            avgBusE = numD / total * 100;
            busFutureInfo.Add(avgBusC);
            busFutureInfo.Add(avgBusU);
            busFutureInfo.Add(avgBusP);
            busFutureInfo.Add(avgBusE);

            for (int i = 1; i <= (questionGridITFuture.ColumnCount - 7); i++)
            {
                if (questionGridITFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe += Convert.ToDouble(temp);
                }
            }

            total = ITc + ITu + ITp + ITe;
            avgITc = ITc / total * 100;
            avgITu = ITu / total * 100;
            avgITp = ITp / total * 100;
            avgITe = ITe / total * 100;
            ITFutureInfo.Add(avgITc);
            ITFutureInfo.Add(avgITu);
            ITFutureInfo.Add(avgITp);
            ITFutureInfo.Add(avgITe);

            BusITResponsesChart(busFutureInfo, ITFutureInfo, cupe, name);
        }

        public void BusITResponsesChart(List<double> bus, List<double> it, List<string> cupe, string name)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = name;
            newChart.Parent = formChart;

            newChart.Size = new Size(800, 750);
            newChart.Visible = true;
            newChart.Text = name;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.BrightPastel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].Area3DStyle.Enable3D = true;
            newChart.ChartAreas["chart1"].Area3DStyle.IsClustered = true;
            newChart.ChartAreas["chart1"].Area3DStyle.LightStyle = LightStyle.None;
            newChart.ChartAreas["chart1"].Area3DStyle.WallWidth = 0;
            newChart.ChartAreas["chart1"].Area3DStyle.IsRightAngleAxes = true;
            newChart.ChartAreas["chart1"].Area3DStyle.Inclination = 30;
            newChart.ChartAreas["chart1"].AxisY.Interval = 5;

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = name;
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("Business");
            newChart.Series["Business"].ChartArea = "chart1";
            newChart.Series["Business"].ChartType = SeriesChartType.Column;
            newChart.Series["Business"].IsValueShownAsLabel = true;
            newChart.Series["Business"].IsVisibleInLegend = true;
            newChart.Series["Business"].YValueType = ChartValueType.Double;
            newChart.Series.Add("IT");
            newChart.Series["IT"].ChartArea = "chart1";
            newChart.Series["IT"].ChartType = SeriesChartType.Column;
            newChart.Series["IT"].IsValueShownAsLabel = true;
            newChart.Series["IT"].IsVisibleInLegend = true;
            newChart.Series["IT"].YValueType = ChartValueType.Double;

            int busCount = bus.Count;
            int itCount = it.Count;

            for (int i = 0; i < busCount; i++)
            {
                newChart.Series["Business"].Points.AddXY(cupe[i], bus[i]);
            }

            for (int i = 0; i < itCount; i++)
            {
                newChart.Series["IT"].Points.AddXY(cupe[i], it[i]);
            }

            //newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }

        private void currentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double numA = 0;
            double numB = 0;
            double numC = 0;
            double numD = 0;
            double ITc = 0;
            double ITu = 0;
            double ITp = 0;
            double ITe = 0;
            double total = 0;
            double avgBusC, avgBusU, avgBusP, avgBusE;
            double avgITc, avgITu, avgITp, avgITe;
            double avgC, avgU, avgP, avgE;
            string temp = "";
            List<string> id = new List<string> { "Business", "IT", "Total" };
            string name = "Current CUPE Score - Overall";
            List<double> busCurInfo = new List<double>();
            List<double> ITCurInfo = new List<double>();
            List<double> totalCurInfo = new List<double>();

            for (int i = 1; i <= (questionGridBusinessCurrent.ColumnCount - 7); i++)
            {
                if (questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    numA += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    numB += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    numC += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    numD += Convert.ToDouble(temp);
                }
            }
            total = numA + numB + numC + numD;
            avgBusC = numA;// / total * 100;
            avgBusU = numB;// / total * 100;
            avgBusP = numC;// / total * 100;
            avgBusE = numD;// / total * 100;
            busCurInfo.Add(avgBusC);
            busCurInfo.Add(avgBusU);
            busCurInfo.Add(avgBusP);
            busCurInfo.Add(avgBusE);

            for (int i = 1; i <= (questionGridITCurrent.ColumnCount - 7); i++)
            {
                if (questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe += Convert.ToDouble(temp);
                }
            }

            total = ITc + ITu + ITp + ITe;
            avgITc = ITc;// / total * 100;
            avgITu = ITu; // / total * 100;
            avgITp = ITp; // / total * 100;
            avgITe = ITe; // / total * 100;
            ITCurInfo.Add(avgITc);
            ITCurInfo.Add(avgITu);
            ITCurInfo.Add(avgITp);
            ITCurInfo.Add(avgITe);

            /*total = numA + numB + numC + numD + ITc + ITu + ITp + ITe;
            avgC = (numA + ITc) / total * 100;
            avgU = (numB + ITu) / total * 100;
            avgP = (numC + ITp) / total * 100;
            avgE = (numD + ITe) / total * 100;*/
            avgC = avgBusC + avgITc;
            avgU = avgBusU + avgITu;
            avgP = avgBusP + avgITp;
            avgE = avgBusE + avgITe;

            totalCurInfo.Add(avgC);
            totalCurInfo.Add(avgU);
            totalCurInfo.Add(avgP);
            totalCurInfo.Add(avgE);

            OverallChart(busCurInfo, ITCurInfo, totalCurInfo, id, name);

            //CurrentBusITChart chart = new CurrentBusITChart();
        }

        private void futureToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double numA = 0;
            double numB = 0;
            double numC = 0;
            double numD = 0;
            double ITc = 0;
            double ITu = 0;
            double ITp = 0;
            double ITe = 0;
            double total = 0;
            double avgBusC, avgBusU, avgBusP, avgBusE;
            double avgITc, avgITu, avgITp, avgITe;
            double avgC, avgU, avgP, avgE;
            string temp = "";
            List<string> id = new List<string> { "Business", "IT", "Total" };
            string name = "Future CUPE Score - Overall";
            List<double> busFutureInfo = new List<double>();
            List<double> ITFutureInfo = new List<double>();
            List<double> totalFutureInfo = new List<double>();

            for (int i = 1; i <= (questionGridBusiFuture.ColumnCount - 7); i++)
            {
                if (questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    numA += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    numB += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    numC += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    numD += Convert.ToDouble(temp);
                }
            }
            total = numA + numB + numC + numD;
            avgBusC = numA; // / total * 100;
            avgBusU = numB; // / total * 100;
            avgBusP = numC; // / total * 100;
            avgBusE = numD; // / total * 100;
            busFutureInfo.Add(avgBusC);
            busFutureInfo.Add(avgBusU);
            busFutureInfo.Add(avgBusP);
            busFutureInfo.Add(avgBusE);

            for (int i = 1; i <= (questionGridITFuture.ColumnCount - 7); i++)
            {
                if (questionGridITFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe += Convert.ToDouble(temp);
                }
            }

            total = ITc + ITu + ITp + ITe;
            avgITc = ITc; // / total * 100;
            avgITu = ITu; //  / total * 100;
            avgITp = ITp; // / total * 100;
            avgITe = ITe; // / total * 100;
            ITFutureInfo.Add(avgITc);
            ITFutureInfo.Add(avgITu);
            ITFutureInfo.Add(avgITp);
            ITFutureInfo.Add(avgITe);

            avgC = avgBusC + avgITc;
            avgU = avgBusU + avgITu;
            avgP = avgBusP + avgITp;
            avgE = avgBusE + avgITe;

            totalFutureInfo.Add(avgC);
            totalFutureInfo.Add(avgU);
            totalFutureInfo.Add(avgP);
            totalFutureInfo.Add(avgE);

            OverallChart(busFutureInfo, ITFutureInfo, totalFutureInfo, id, name);
        }

        public void OverallChart(List<double> bus, List<double> it, List<double> total, List<string> id, string name)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = name;
            newChart.Parent = formChart;

            newChart.Size = new Size(800, 750);
            newChart.Visible = true;
            newChart.Text = name;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.Excel;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].Area3DStyle.Enable3D = true;
            newChart.ChartAreas["chart1"].Area3DStyle.IsClustered = true;
            newChart.ChartAreas["chart1"].Area3DStyle.LightStyle = LightStyle.None;
            newChart.ChartAreas["chart1"].Area3DStyle.WallWidth = 0;
            newChart.ChartAreas["chart1"].Area3DStyle.IsRightAngleAxes = true;
            newChart.ChartAreas["chart1"].Area3DStyle.Inclination = 30;
            newChart.ChartAreas["chart1"].AxisY.Interval = 5;

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = name;
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("Total Commodity");
            newChart.Series["Total Commodity"].ChartArea = "chart1";
            newChart.Series["Total Commodity"].ChartType = SeriesChartType.StackedColumn;
            newChart.Series["Total Commodity"].IsValueShownAsLabel = true;
            newChart.Series["Total Commodity"].IsVisibleInLegend = true;
            newChart.Series["Total Commodity"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Utility");
            newChart.Series["Total Utility"].ChartArea = "chart1";
            newChart.Series["Total Utility"].ChartType = SeriesChartType.StackedColumn;
            newChart.Series["Total Utility"].IsValueShownAsLabel = true;
            newChart.Series["Total Utility"].IsVisibleInLegend = true;
            newChart.Series["Total Utility"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Partner");
            newChart.Series["Total Partner"].ChartArea = "chart1";
            newChart.Series["Total Partner"].ChartType = SeriesChartType.StackedColumn;
            newChart.Series["Total Partner"].IsValueShownAsLabel = true;
            newChart.Series["Total Partner"].IsVisibleInLegend = true;
            newChart.Series["Total Partner"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Enabler");
            newChart.Series["Total Enabler"].ChartArea = "chart1";
            newChart.Series["Total Enabler"].ChartType = SeriesChartType.StackedColumn;
            newChart.Series["Total Enabler"].IsValueShownAsLabel = true;
            newChart.Series["Total Enabler"].IsVisibleInLegend = true;
            newChart.Series["Total Enabler"].YValueType = ChartValueType.Double;

            newChart.Series["Total Commodity"].Points.AddXY(id[0], bus[0]);
            newChart.Series["Total Commodity"].Points.AddXY(id[1], it[0]);
            newChart.Series["Total Commodity"].Points.AddXY(id[2], total[0]);

            newChart.Series["Total Utility"].Points.AddXY(id[0], bus[1]);
            newChart.Series["Total Utility"].Points.AddXY(id[1], it[1]);
            newChart.Series["Total Utility"].Points.AddXY(id[2], total[1]);

            newChart.Series["Total Partner"].Points.AddXY(id[0], bus[2]);
            newChart.Series["Total Partner"].Points.AddXY(id[1], it[2]);
            newChart.Series["Total Partner"].Points.AddXY(id[2], total[2]);

            newChart.Series["Total Enabler"].Points.AddXY(id[0], bus[3]);
            newChart.Series["Total Enabler"].Points.AddXY(id[1], it[3]);
            newChart.Series["Total Enabler"].Points.AddXY(id[2], total[3]);

            //newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }

        private void iTStakeHoldersCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] ITc = new double[100];
            double[] ITu = new double[100];
            double[] ITp = new double[100];
            double[] ITe = new double[100];
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Current IT CUPE Score";
            string xName = "IT Stakeholders";
            List<double> commodity = new List<double>();
            List<double> utility = new List<double>();
            List<double> partner = new List<double>();
            List<double> enabler = new List<double>();

            for (int i = 1; i <= (questionGridITCurrent.ColumnCount - 7); i++)
            {
                if (questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc[i] += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu[i] += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp[i] += Convert.ToDouble(temp);
                    temp = questionGridITCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe[i] += Convert.ToDouble(temp);
                }
                commodity.Add(ITc[i]);
                utility.Add(ITu[i]);
                partner.Add(ITp[i]);
                enabler.Add(ITe[i]);
            }

            EachPersonChart(name, xName, commodity, utility, partner, enabler);
        }


        private void iTStakeHoldersFutureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] ITc = new double[100];
            double[] ITu = new double[100];
            double[] ITp = new double[100];
            double[] ITe = new double[100];
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Future (Desired) IT CUPE Score";
            string xName = "IT Stakeholders";
            List<double> commodity = new List<double>();
            List<double> utility = new List<double>();
            List<double> partner = new List<double>();
            List<double> enabler = new List<double>();

            for (int i = 1; i <= (questionGridITFuture.ColumnCount - 7); i++)
            {
                if (questionGridITFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridITFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    ITc[i] += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    ITu[i] += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    ITp[i] += Convert.ToDouble(temp);
                    temp = questionGridITFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    ITe[i] += Convert.ToDouble(temp);
                }
                commodity.Add(ITc[i]);
                utility.Add(ITu[i]);
                partner.Add(ITp[i]);
                enabler.Add(ITe[i]);
            }

            EachPersonChart(name, xName, commodity, utility, partner, enabler);
            
        }

        private void businessStakeHoldersCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            double[] busC = new double[100];
            double[] busU = new double[100];
            double[] busP = new double[100];
            double[] busE = new double[100];
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Current Business CUPE Score";
            string xName = "Business Stakeholders";
            List<double> commodity = new List<double>();
            List<double> utility = new List<double>();
            List<double> partner = new List<double>();
            List<double> enabler = new List<double>();

            for (int i = 1; i <= (questionGridBusinessCurrent.ColumnCount - 7); i++)
            {
                if (questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusinessCurrent.Rows[totalARowIndex].Cells[i].Value.ToString();
                    busC[i] += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    busU[i] += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    busP[i] += Convert.ToDouble(temp);
                    temp = questionGridBusinessCurrent.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    busE[i] += Convert.ToDouble(temp);
                }
                commodity.Add(busC[i]);
                utility.Add(busU[i]);
                partner.Add(busP[i]);
                enabler.Add(busE[i]);
            }

            EachPersonChart(name, xName, commodity, utility, partner, enabler);
        }

        private void businessStakeHoldersFutureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] busC = new double[100];
            double[] busU = new double[100];
            double[] busP = new double[100];
            double[] busE = new double[100];
            string temp = "";
            List<string> cupe = new List<string> { "Total Commodity", "Total Utility", "Total Partner", "Total Enabler" };
            string name = "Future (Desired) Business CUPE Score";
            string xName = "Business Stakeholders";
            List<double> commodity = new List<double>();
            List<double> utility = new List<double>();
            List<double> partner = new List<double>();
            List<double> enabler = new List<double>();

            for (int i = 1; i <= (questionGridBusiFuture.ColumnCount - 7); i++)
            {
                if (questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value != null)
                {
                    temp = questionGridBusiFuture.Rows[totalARowIndex].Cells[i].Value.ToString();
                    busC[i] += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalBRowIndex].Cells[i].Value.ToString();
                    busU[i] += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalCRowIndex].Cells[i].Value.ToString();
                    busP[i] += Convert.ToDouble(temp);
                    temp = questionGridBusiFuture.Rows[totalDRowIndex].Cells[i].Value.ToString();
                    busE[i] += Convert.ToDouble(temp);
                }
                commodity.Add(busC[i]);
                utility.Add(busU[i]);
                partner.Add(busP[i]);
                enabler.Add(busE[i]);
            }

            EachPersonChart(name, xName, commodity, utility, partner, enabler);
        }

        public void EachPersonChart(string name, string xName, List<double> commodity, List<double> utility, List<double> partner, List<double> enabler)
        {
            Form formChart = new Form();
            formChart.AutoSize = true;
            formChart.AutoScroll = true;

            formChart.Show();
            Chart newChart = new Chart();

            formChart.Text = name;
            newChart.Parent = formChart;

            newChart.Size = new Size(800, 750);
            newChart.Visible = true;
            newChart.Text = name;
            newChart.ChartAreas.Add("chart1");
            newChart.Palette = ChartColorPalette.None;

            newChart.ChartAreas["chart1"].Visible = true;
            newChart.ChartAreas["chart1"].AxisX.MajorGrid.Enabled = false;
            newChart.ChartAreas["chart1"].Area3DStyle.Enable3D = true;
            newChart.ChartAreas["chart1"].Area3DStyle.IsClustered = true;
            newChart.ChartAreas["chart1"].Area3DStyle.LightStyle = LightStyle.None;
            newChart.ChartAreas["chart1"].Area3DStyle.WallWidth = 0;
            newChart.ChartAreas["chart1"].Area3DStyle.IsRightAngleAxes = true;
            newChart.ChartAreas["chart1"].Area3DStyle.Inclination = 30;
            newChart.ChartAreas["chart1"].AxisY.Interval = 5;
            newChart.ChartAreas["chart1"].AxisY.Maximum = 20;
            newChart.ChartAreas["chart1"].AxisX.Title = xName;
            newChart.ChartAreas["chart1"].AxisX.TitleFont = new Font("Arial", 13);
            newChart.ChartAreas["chart1"].AxisY.Title = "CUPE Responses";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Arial", 13);

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = name;
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Series.Add("Total Commodity");
            newChart.Series["Total Commodity"].ChartArea = "chart1";
            newChart.Series["Total Commodity"].ChartType = SeriesChartType.StackedColumn;
            //newChart.Series["Total Commodity"].IsValueShownAsLabel = true;
            newChart.Series["Total Commodity"].IsVisibleInLegend = true;
            newChart.Series["Total Commodity"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Utility");
            newChart.Series["Total Utility"].ChartArea = "chart1";
            newChart.Series["Total Utility"].ChartType = SeriesChartType.StackedColumn;
            //newChart.Series["Total Utility"].IsValueShownAsLabel = true;
            newChart.Series["Total Utility"].IsVisibleInLegend = true;
            newChart.Series["Total Utility"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Partner");
            newChart.Series["Total Partner"].ChartArea = "chart1";
            newChart.Series["Total Partner"].ChartType = SeriesChartType.StackedColumn;
            //newChart.Series["Total Partner"].IsValueShownAsLabel = true;
            newChart.Series["Total Partner"].IsVisibleInLegend = true;
            newChart.Series["Total Partner"].YValueType = ChartValueType.Double;
            newChart.Series.Add("Total Enabler");
            newChart.Series["Total Enabler"].ChartArea = "chart1";
            newChart.Series["Total Enabler"].ChartType = SeriesChartType.StackedColumn;
            //newChart.Series["Total Enabler"].IsValueShownAsLabel = true;
            newChart.Series["Total Enabler"].IsVisibleInLegend = true;
            newChart.Series["Total Enabler"].YValueType = ChartValueType.Double;

            int count = commodity.Count;

            for (int cnt = 0; cnt < count; cnt++)
            {
                newChart.Series["Total Commodity"].Points.AddXY((cnt + 1).ToString(), commodity[cnt]);

                newChart.Series["Total Utility"].Points.AddXY((cnt + 1).ToString(), utility[cnt]);

                newChart.Series["Total Partner"].Points.AddXY((cnt + 1).ToString(), partner[cnt]);

                newChart.Series["Total Enabler"].Points.AddXY((cnt + 1).ToString(), enabler[cnt]);
            }
            //newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }

        private void StartTutorialMode()
        {
            if( HelpCurrentStep == 0 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("Begin by adding participants under the Edit->Participants menu",
                    this, 100, 5, 4000);
            }
            if (HelpCurrentStep == 1 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("Enter answers manually or go to File->Open Surveys",
                    this, -40, 5, 4000);
            }
            if (HelpCurrentStep == 2 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Begin by adding participants under the Edit->Participants menu",
                    this.menuStrip1, -40, 0, 4000);
            }


        }

        private void beginTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpEnabled = true;
            HelpCurrentStep = 0;
            StartTutorialMode();
        }

        private void continuteTutorialMode(object sender, EventArgs e)
        {

        }


        private void commodityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = charts[0].Series["BusiCurrent"].Points[0].Color;
            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                foreach(Chart ch in charts)
                {
                    ch.Series["BusiCurrent"].Points[0].Color = MyDialog.Color;
                }
                
            }
        }

        private void utilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = charts[0].Series["BusiCurrent"].Points[1].Color;
            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Chart ch in charts)
                {
                    ch.Series["BusiCurrent"].Points[1].Color = MyDialog.Color;
                }

            }
        }

        private void partnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = charts[0].Series["BusiCurrent"].Points[2].Color;
            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Chart ch in charts)
                {
                    ch.Series["BusiCurrent"].Points[2].Color = MyDialog.Color;
                }

            }
        }

        private void enablerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = charts[0].Series["BusiCurrent"].Points[3].Color;
            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Chart ch in charts)
                {
                    ch.Series["BusiCurrent"].Points[3].Color = MyDialog.Color;
                }

            }
        }

        private void defaultSurveysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeCUPEDefaults().Show();
        }

    }// end class


}
