﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Threading;

namespace IBMConsultantTool
{

    public partial class CUPETool : Form
    {
        List<DataGridView> grids = new List<DataGridView>();
        List<Chart> charts = new List<Chart>();
        DataGridView currentGrid;
        DataGridView toRemove;
        Chart currentChart;

        int personCount = 0;
        bool isAnonymous = true;
        public bool is20Question = true;
        bool changesMade = false;
        string oldCellValue = String.Empty;

        int totalAIndex = 1;
        int totalBIndex = 2;
        int totalCIndex = 3;
        int totalDIndex = 4;
        int averageIndex = 6;
        int totalAnswers = 5;

        public delegate void UpdateUIDelegate(bool IsDataLoaded);
        //indexes of the row items. 
        //there are 20 questions, plus 1 blank row, then these are the resulting indexes.
        //When there is only 10 questions, the rows are hidden, but still technically indexed, so these values do not change
        int totalARowIndex = 21;
        int totalBRowIndex = 22;
        int totalCRowIndex = 23;
        int totalDRowIndex = 24;
        int averageRowIndex = 26;
        int totalAnswerRowIndex = 25;

        List<string> questions = new List<string>();
        // part of our help tutorial Not completed, but still functional
        bool HelpEnabled = false;
        int HelpCurrentStep = 0;

        string chartName;
        //variable that says whether the tool was closed by the "X" or if we are just switching in between tools
        // close means x was hit
        string closeState = "close";

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

            foreach (DataGridView grid in grids)
            {
                grid.RowHeadersVisible = false;           
            }

            this.FormClosed += new FormClosedEventHandler(CUPETool_FormClosed);
        }
        // when switching tools, deciding what to do based on what we are switching to.
        private void CUPETool_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (closeState == "ITCap")
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNITCap));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
            else if (closeState == "BOM")
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNBOM));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
            else if (closeState == "close")
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNTEST));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
        }
        // Test form is the intial blue form with save load trend analysis
        private void RUNTEST()
        {
            Application.Run(new TestForm());
        }
        //builds the main grid view with the 20 questions, and any answers that were loading form surveys
        private void CUPETool_Load(object sender, EventArgs e)
        {
            ClientDataControl.cupeQuestions.Clear();
            ClientDataControl.SetParticipants(new List<Person>());
            ClientDataControl.SetCupeAnswers(new List<CupeData>());
            //questionGrid.CellValueChanged +=new DataGridViewCellEventHandler(questionGrid_CellValueChanged);
            //CreatePerson();
            ClientDataControl.LoadCUPEQuestions(this);
            ClientDataControl.LoadParticipants();
            int questionCount = ClientDataControl.GetCupeQuestions().Count;
            foreach (DataGridView view in grids)
            {
                currentGrid = view;
                for (int i = 1; i <= 20; i++)
                {
                    if (i <= questionCount)
                    {
                        DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[0].Clone();
                        row.Cells[0].Value = "Question " + i;
                        row.Visible = true;
                        currentGrid.Rows.Add(row);

                        //Change this if the number of questions changes
                        if (questions.Count < 20)
                        {
                            questions.Add(row.Cells[0].Value.ToString());
                        }
                    }
                    else
                    {
                        DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[0].Clone();
                        row.Cells[0].Value = "Question " + i;
                        row.Visible = false;
                        currentGrid.Rows.Add(row);
                    }
                }

                CreateStatsRows();
            }
            //sets the business current grid as the default and first to view on open
            currentGrid = questionGridBusinessCurrent;
            currentChart = busiCurrentGraph;
            //hides all other charts in the form
            foreach (Chart chart in charts)
            {
                chart.Visible = false;
            }
            currentChart.Visible = true;
            CreateGraphs();
            //CreateLabel();

            loadColumnNames();
            LoadAnswersFromDataControl();
        }

        private void CreateGraphs()
        {
            foreach (Chart chart in charts)
            {
                currentChart = chart;

                DataPoint point = new DataPoint();
                point.Color = Color.Fuchsia;
                point.BackGradientStyle = GradientStyle.Center;
                point.LegendText = "Commodity";
                //point.Name = "Commodity";
                //point.Label = "Commodity";
                //point.SetValueY(30);
                currentChart.Series["BusiCurrent"].Points.Add(point);
                DataPoint point2 = new DataPoint();
                point2.Color = Color.Blue;
                point2.BackGradientStyle = GradientStyle.Center;
                point2.LegendText = "Utility";
                //point2.Name = "Utility";
                //point2.Label = "Utility";
                //point2.SetValueY(10);
                currentChart.Series["BusiCurrent"].Points.Add(point2);
                DataPoint point3 = new DataPoint();
                point3.Color = Color.Orange;
                point3.BackGradientStyle = GradientStyle.Center;
                point3.LegendText = "Partner";

                currentChart.Series["BusiCurrent"].Points.Add(point3);
                DataPoint point4 = new DataPoint();
                point4.Color = Color.Green;
                point4.BackGradientStyle = GradientStyle.Center;
                point4.LegendText = "Enabler";

                currentChart.Series["BusiCurrent"].Points.Add(point4);
            }
        }

        private void CreateLabel()
        {
            
            Label label = new Label();
            questionInfoPanel.Controls.Add(label);
            label.Width = questionInfoPanel.Width;
            label.BackColor = Color.DeepSkyBlue;
            label.Location = new Point(0, 0);
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Visible = true;
            label.Text = "Question Info";

        }
        //adds these set rows after the initial rows have been created
        
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
        //creates a new person from the edit participants window
        public void CreatePerson()
        {
            personCount++;
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            Person person = new Person(personCount);

            //person.Type = Person.EmployeeType.Business;
            col.HeaderText = "Person  " + (currentGrid.ColumnCount - 6).ToString();

            col.Name = person.CodeName; 
            
            col.Width = 60;
         
            currentGrid.Columns.Insert(currentGrid.ColumnCount -6, col);
            
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            CreatePerson();
        }



        private void questionGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            changesMade = true;
            // which row was changed
            var currentCell = currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //check for null value, and then make sure it is also not blank. Double error check here
            if ( currentCell.Value!= null)
            {
                if (currentCell.Value.ToString().Length > 0)
                {
                    if ((currentCell.Value.ToString().IndexOfAny(new char[] { 'a','b','c','d', 'A', 'B', 'C', 'D' }) != -1)
                        && (currentCell.Value.ToString().Length < 2))
                    {
                        // Stop editing without canceling the label change.
                        ChangeTotalsByRow(e.RowIndex);
                        ChangeTotalsByColumn(e.ColumnIndex, e.RowIndex);
                        LoadChartData();
                        UpdateCupeScore();

                        //Help / Tutorial Step
                        if (HelpEnabled && HelpCurrentStep == 1)
                        {
                            HelpCurrentStep = 2;
                            StartTutorialMode();
                        }

                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        currentCell.Value = String.Empty;
                        System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                        myToolTip.IsBalloon = true;
                        myToolTip.Show(string.Empty, this, 0);
                        myToolTip.Show("Invalid answer. Must give answer as a, b, c, or d. Case insensitive",
                            currentGrid, 100, 5, 2000);
                        currentGrid.ShowCellToolTips = false;

                        if (oldCellValue != string.Empty)
                        {
                            currentCell.Value = oldCellValue;
                        }

                    }
                }
                else
                {

                }

            }

            oldCellValue = string.Empty;
        }
        //iterate through all of the rows
        //check the values, give them a score depending on the letter
        //A =1
        //B =2
        //C =3
        //D =4
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
            //the -7 here is the number of static columns that exist. The current count - the number of static rows is the person count
            // this is also a way to get the index when added to the index
            //If Another static row is to be added. (such as a row for standard deviation) these need to be updated to 8.
            currentGrid.Rows[index].Cells[totalAIndex + (currentGrid.ColumnCount -7)].Value = totalA.ToString();
            currentGrid.Rows[index].Cells[totalBIndex + (currentGrid.ColumnCount - 7)].Value = totalB.ToString();
            currentGrid.Rows[index].Cells[totalCIndex + (currentGrid.ColumnCount - 7)].Value = totalC.ToString();
            currentGrid.Rows[index].Cells[totalDIndex + (currentGrid.ColumnCount - 7)].Value = totalD.ToString();
            currentGrid.Rows[index].Cells[totalAnswers + (currentGrid.ColumnCount - 7)].Value = count.ToString();
            currentGrid.Rows[index].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value = currentGrid.ColumnCount > 7 ? Math.Round((total / (currentGrid.ColumnCount - 7)), 2).ToString() : "0";

            QuestionCellFormatting(index);
            
        }

        private void UpdateCupeScore()
        {
            int count = 0;
            string total = "";
            float num = 0;
            for (int i = 0; i < 21; i++) // 21 becuase if 10, rows are still indexed in the grid
            {
                if (currentGrid.Rows[i].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value != null)
                {
                    total = currentGrid.Rows[i].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value.ToString();
                    num += (float)Convert.ToDouble(total);
                    count++;
                }
            }
            // make sure there is a person, and calculate average
            if (count > 0)
                cupeScoreLabel.Text = Math.Round((num / count), 2).ToString();
            else
                cupeScoreLabel.Text = " ";
        }


        // happens after changetotalsbyrow
        //runs down the column of the cell last changed, and updates the bottom rows for averages per person
        private void ChangeTotalsByColumn(int colIndex, int rowIndex)
        {
            int totalA = 0;
            int totalB = 0;
            int totalC = 0;
            int totalD = 0;
            int count = 0;
            float total = 0;
            //check the answers in each of the cells
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
            //total row indexes are for each of the stats being tracked and are set 
            currentGrid.Rows[totalARowIndex].Cells[colIndex].Value = totalA.ToString();
            currentGrid.Rows[totalBRowIndex].Cells[colIndex].Value = totalB.ToString();
            currentGrid.Rows[totalCRowIndex].Cells[colIndex].Value = totalC.ToString();
            currentGrid.Rows[totalDRowIndex].Cells[colIndex].Value = totalD.ToString();
            currentGrid.Rows[totalAnswerRowIndex].Cells[colIndex].Value = count.ToString();
            currentGrid.Rows[averageRowIndex].Cells[colIndex].Value = currentGrid.ColumnCount > 7 ? Math.Round((total / (count)), 2).ToString() : "0";


           

            

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

            for (int cnt = 0; cnt < 4; cnt++)
            {
                currentChart.Series["BusiCurrent"].Points[cnt].SetDefault(true);
                
            }

            if (currentGrid == questionGridBusiFuture)
                chartName = "Business Future CUPE Responses";
            if (currentGrid == questionGridBusinessCurrent)
                chartName = "Business Current CUPE Responses";
            if (currentGrid == questionGridITCurrent)
                chartName = "IT Current CUPE Responses";
            if (currentGrid == questionGridITFuture)
                chartName = "IT Future CUPE Responses";

              

            if (numA != 0 && numA > 0)
                currentChart.Series["BusiCurrent"].Points[0].SetValueXY("Commodity", numA);
            if (numB != 0 && numB > 0)
                currentChart.Series["BusiCurrent"].Points[1].SetValueXY("Utility", numB);
            if (numC != 0 && numC > 0)
                currentChart.Series["BusiCurrent"].Points[2].SetValueXY("Partner", numC);
            if (numD != 0 && numD > 0)
                currentChart.Series["BusiCurrent"].Points[3].SetValueXY("Enabler", numD);

            // the beginning of the chart
            if (numA == 0)
            {
                currentChart.Series["BusiCurrent"].Points[0].LegendText = "Commodity";
                currentChart.Series["BusiCurrent"].Points[0].SetValueY(numA);
            }
            if (numB == 0)
            {
                currentChart.Series["BusiCurrent"].Points[1].LegendText = "Utility";
                currentChart.Series["BusiCurrent"].Points[1].SetValueY(numB);
            }
            if (numC == 0)
            {
                currentChart.Series["BusiCurrent"].Points[2].LegendText = "Partner";
                currentChart.Series["BusiCurrent"].Points[2].SetValueY(numC);
            }
            if (numD == 0)
            {
                currentChart.Series["BusiCurrent"].Points[3].LegendText = "Enabler";
                currentChart.Series["BusiCurrent"].Points[3].SetValueY(numD);
            }

            try
            {
                currentChart.SaveImage(ClientDataControl.Client.FilePath + "/" + chartName + ".jpg", ChartImageFormat.Jpeg);
                currentChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + chartName + ".jpg", ChartImageFormat.Jpeg);
            }
            catch
            {

            }
        }
        //check the the average of the last row updated, and set the color is loow
        public void PersonCellFormatting(int index)
        {
            string tempAvg = (string)currentGrid.Rows[averageRowIndex].Cells[index].Value;
            float average = (float)Convert.ToDouble(tempAvg);
            if (average < ConfigurationSettings.Instance.CupeHigh)
                currentGrid[index, averageRowIndex].Style.BackColor = Color.IndianRed;
            else
                currentGrid[index, averageRowIndex].Style.BackColor = Color.SeaGreen;
        }
        //update the column of the last column updated, and change color depending on the answer
        public void QuestionCellFormatting(int index)
        {
            string tempAvg = (string)currentGrid.Rows[index].Cells[averageIndex + (currentGrid.ColumnCount - 7)].Value;
            float average = (float)Convert.ToDouble(tempAvg);
            if (average < ConfigurationSettings.Instance.CupeHigh)
                currentGrid[averageIndex + (currentGrid.ColumnCount - 7), index].Style.BackColor = Color.IndianRed;
            else
                currentGrid[averageIndex + (currentGrid.ColumnCount - 7), index].Style.BackColor = Color.SeaGreen;
        }
        //determines which chart you currently would like to view, and hides the rest
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

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 2)
            {
                HelpCurrentStep = 3;
                StartTutorialMode();
            }
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

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 2)
            {
                HelpCurrentStep = 3;
                StartTutorialMode();
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

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 2)
            {
                HelpCurrentStep = 3;
                StartTutorialMode();
            }
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

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 2)
            {
                HelpCurrentStep = 3;
                StartTutorialMode();
            }
        }
        //hitting go button checks for what it needs to search, and then calls the appropriate function
        private void filterQuestionsGo_Click(object sender, EventArgs e)
        {
            try
            {

                if (questionFilter.Text == "Highest Cupe Score")
                {
                    FilterQuestionByHighestScore(questionFilterAmount.Text, averageIndex);

                    //Help / Tutorial Step
                    if (HelpEnabled && HelpCurrentStep == 5)
                    {
                        HelpCurrentStep = 6;
                        StartTutorialMode();
                    }

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
                if (questionFilter.Text == "Highest Standard Deviation")
                {
                    FilterQuestionsByHighestStandardDeviation(questionFilterAmount.Text,averageIndex);
                }
            }
            catch
            {
            }
        }

        public void FilterQuestionsByHighestStandardDeviation(string amount, int index)
        {
            if (toRemove != null)
                questionInfoPanel.Controls.Remove(toRemove);
            Console.WriteLine("here");
            int num = Convert.ToInt32(amount);
            List<Tuple<double, int>> values = new List<Tuple<double, int>>();
            int count = 0;
            foreach (DataGridViewRow row in currentGrid.Rows)
            {
                Console.WriteLine("beginning of function");
                double avg = Convert.ToDouble(row.Cells[averageIndex + currentGrid.ColumnCount - 7].Value.ToString());
                double sum = 0;
                double numAnswers = 0;
                for (int i = 1; i <= currentGrid.ColumnCount - 7; i++)
                {
                    if ((row.Cells[i].Value).ToString() == "a" || (row.Cells[i].Value).ToString() == "A")
                    {
                        double answer = 1;
                        sum += Math.Pow(answer - avg, 2);
                        numAnswers++;
                    }
                    else if ((row.Cells[i].Value).ToString() == "b" || (row.Cells[i].Value).ToString() == "B")
                    {
                        double answer = 2;
                        sum += Math.Pow(answer - avg, 2);
                        numAnswers++;
                    }
                    else if ((row.Cells[i].Value).ToString() == "c" || (row.Cells[i].Value).ToString() == "C")
                    {
                        double answer = 3;
                        sum += Math.Pow(answer - avg, 2);
                        numAnswers++;
                    }
                    else if ((row.Cells[i].Value).ToString() == "d" || (row.Cells[i].Value).ToString() == "D")
                    {
                        double answer = 4;
                        sum += Math.Pow(answer - avg, 2);
                        numAnswers++;
                    }
                    else
                    {
                        Console.WriteLine("count");
                        continue;
                    }

                }
                count++;
                if (count == 20)
                {
                  Console.WriteLine("about to break");
                    break;

                }
                Console.WriteLine("here before sqrt");
                double stdDev = Math.Sqrt(sum / numAnswers);
                Console.WriteLine(stdDev);
                values.Add(new Tuple<double,int>(stdDev, row.Index));
                Console.WriteLine("here right after sqrt " + row.Index.ToString());
            }
            Console.WriteLine("right before grid creation");
            DataGridView view = new DataGridView();
            view.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Console.WriteLine("created grid");
            view.AllowUserToAddRows = false;
            view.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
                Console.WriteLine("adding columns");
            }

            values.Sort();
            values.Reverse();
            Console.WriteLine("just sorted");
            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
                Console.WriteLine("added rows to new grid");
            }

            questionInfoPanel.Controls.Add(view);
            view.Location = new Point(10, 35);
            view.Width = 550;
            view.Height = 130;

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
            view.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
            }

            values.Sort();

            values.Reverse();

            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
            }
            
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
            view.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());
            }

            values.Sort();
            
            for (int i = 0; i < num; i++)
            {
                DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[values[i].Item2].Clone();
                for (int j = 0; j < currentGrid.ColumnCount; j++)
                {
                    row.Cells[j].Value = currentGrid.Rows[values[i].Item2].Cells[j].Value;
                }
                view.Rows.Add(row);
            }

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
            view.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toRemove = view;
            foreach (DataGridViewColumn col in currentGrid.Columns)
            {
                view.Columns.Add((DataGridViewColumn)col.Clone());                
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
        //create s view for chart
        private void iTStakeHoldersCurrentFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentFloats = new List<float>();
            List<float> futureFloats = new List<float>();
            int count = 0;

            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                currentFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + (questionGridITCurrent.ColumnCount - 7)].Value));
                count++;
                
            }
            count = 0;
            foreach (DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                futureFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value));
                count++;
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

            for (int i = 0; i < currentCount; i++)
            {
                double temp = Convert.ToDouble(current[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                string questionOfCupe = ClientDataControl.cupeQuestions[i].QuestionText;
                if (questionOfCupe.Length > length_label)
                {
                    questionOfCupe = questionOfCupe.Remove(length_label);
                    questionOfCupe += "...";
                }

                newChart.Series["Current"].Points.AddXY(questionOfCupe, temp);
            }

            for (int i = 0; i < futureCount; i++)
            {
                double temp = Convert.ToDouble(future[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                string questionOfCupe = ClientDataControl.cupeQuestions[i].QuestionText;
                if (questionOfCupe.Length > length_label)
                {
                    questionOfCupe = questionOfCupe.Remove(length_label);
                    questionOfCupe += "...";
                }

                newChart.Series["Future"].Points.AddXY(questionOfCupe, temp);
            }

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
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
        //creates view for chart
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
        //creates view for chhart
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

        private const int length_label = 30;

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
            newChart.ChartAreas["chart1"].AxisX.LabelStyle.Format = "^.{10}";
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
            //newChart.Series["IT"

            int currentCount = current.Count;
            int futureCount = future.Count;

            for (int i = 0; i < currentCount; i++)
            {
                double temp = Convert.ToDouble(current[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                string questionOfCupe = ClientDataControl.cupeQuestions[i].QuestionText;
                if (questionOfCupe.Length > length_label)
                {
                    questionOfCupe = questionOfCupe.Remove(length_label);
                    questionOfCupe += "...";
                }

                newChart.Series["Business"].Points.AddXY(questionOfCupe, temp);
            }

            for (int i = 0; i < futureCount; i++)
            {
                double temp = Convert.ToDouble(future[i]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                string questionOfCupe = ClientDataControl.cupeQuestions[i].QuestionText;
                if (questionOfCupe.Length > length_label)
                {
                    questionOfCupe = questionOfCupe.Remove(length_label);
                    questionOfCupe += "...";
                }

                newChart.Series["IT"].Points.AddXY(questionOfCupe, temp);
            }

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
            //newChart.SaveImage(Application.StartupPath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }
        //opens the particapants menu. Here you can change which people receive the survey, and how many answers to record.
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
                changeAllTotals();
                UpdateCupeScore();

                //Help / Tutorial Step
                if(HelpEnabled && HelpCurrentStep == 0)
                {
                    HelpCurrentStep = 1;
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

                //col.HeaderText = "Person " + (questionGridITCurrent.Columns.Count - 6).ToString();
                col.HeaderText = person.CodeName; 
                col.Name = person.CodeName; 
                col.Width = 100;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = person.CodeName; 
                col2.Name = person.CodeName;  
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

        //Saves the table values to the clientdatacontrol
        //runs through every cell in the column, and saves values to database
        private void saveCupeDataValues()
        {

            //ITCurrent
            foreach( DataGridViewColumn column in questionGridITCurrent.Columns)
            {
                Person currentPerson = null;
                try
                {
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
                            if (row.Cells[column.Index].Value.ToString().Length > 0)
                            {
                                currentPerson.cupeDataHolder.CurrentAnswers.Add(
                                    row.Cells[0].Value.ToString(),
                                     row.Cells[column.Index].Value.ToString()[0]);
                            }
                            else
                            {
                                currentPerson.cupeDataHolder.CurrentAnswers.Add(
                                    row.Cells[0].Value.ToString(), ' ');
                            }

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
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
        //remove s all people from all data grids
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
        //saves cupe answers to data base
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
            changesMade = false;

            ClientDataControl.SaveCUPE();
            changesMade = false;
        }
        //starts sending commands to microsoft word to build the surveys
        private void createSurveyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyGenerator generator = new SurveyGenerator();
            if(!generator.CreateCupeSurvey(ClientDataControl.GetParticipants(), questions, is20Question))
            {
                MessageBox.Show("Word encountered an error. Please retry");
            }
        }
        private void UpdateUI(bool IsDataLoaded)
        {

        }

        private void LoadSurveys()
        {
            
            
            
            
        }
        //opens surveys and answers into the tool
        private void openSurveysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientDataControl.SetParticipants(new List<Person>());
            ClientDataControl.SetCupeAnswers(new List<CupeData>());

            
            LoadingScreen loadingScreen = new LoadingScreen(questionGridBusiFuture.Location.X, questionGridBusiFuture.Location.Y, this );
            UpdateUI(false);
            
            Thread t = new Thread(new ThreadStart(LoadSurveys));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.IsBackground = true;
            t.Start();

            var SurveyReader = new SurveyReader();
            SurveyReader.ReadSurveyCUPE(ClientDataControl.GetParticipants());


            removePersonColumns();
            loadColumnNames();
            LoadAnswersFromDataControl();
            ClientDataControl.SaveCUPE();
            ClientDataControl.SaveParticipantsToDB();
            UpdateUI(true);

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 1)
            {
                HelpCurrentStep = 2;
                StartTutorialMode();
            }
        }

        /* Get the tree node under the mouse pointer and 
   save it in the mySelectedNode variable. */

        TreeNode mySelectedNode;
        int indexCurrentQuestionNode;

        private void QuestionView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
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

        //after the tree view in the bottom right hand corner is edited, it registers a new question with the database
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
                        //adds question to database along with all of its avilable answers
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

                        //Help / Tutorial Step
                        if (HelpEnabled && HelpCurrentStep == 4)
                        {
                            HelpCurrentStep = 5;
                            StartTutorialMode();
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
        // actual function to search through the word documents in the folder specified and loads them into the tool
        private void LoadCupeQuestionsFromDocument()
        {
            CupeQuestionStringData data = new CupeQuestionStringData();
            string path;
            if(is20Question)
            {
                path = @"Resources\Questions.txt";
            }
            else
            {
                path = @"Resources\Questions10.txt";
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
            closeState = "ITCap";
            this.Close();
            return;
        }

        private void RUNITCap()
        {
            Application.Run(new ITCapTool());
        }

        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = "BOM";
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
            newChart.ChartAreas["chart1"].AxisX.LabelStyle.IsEndLabelVisible = false;
            //newChart.ChartAreas["chart1"].AxisX.Maximum = maxQuestion + 1;
            newChart.ChartAreas["chart1"].AxisY.Title = "CUPE Profile Score";
            newChart.ChartAreas["chart1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
            newChart.ChartAreas["chart1"].AxisY.Maximum = 4;
            //newChart.ChartAreas["chart1"].AxisY.

            newChart.Legends.Add("legend");
            newChart.Legends["legend"].Enabled = true;
            newChart.Legends["legend"].LegendStyle = LegendStyle.Table;
            newChart.Legends["legend"].TableStyle = LegendTableStyle.Wide;
            newChart.Legends["legend"].Docking = Docking.Bottom;
            newChart.Legends["legend"].Alignment = StringAlignment.Center;

            newChart.Titles.Add("title");
            newChart.Titles[0].Name = "title";
            newChart.Titles["title"].Visible = true;
            newChart.Titles["title"].Text = "Q" + index + ": IT and Business Responses";
            newChart.Titles["title"].Font = new Font("Arial", 14, FontStyle.Bold);

            newChart.Titles.Add("subtitle");
            newChart.Titles[1].Name = "subtitle";
            newChart.Titles["subtitle"].Visible = true;
            newChart.Titles["subtitle"].Text = ClientDataControl.cupeQuestions[index].QuestionText;
            newChart.Titles["subtitle"].Font = new Font("Time New Roman", 11);

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

            double temp = Convert.ToDouble(fuBusiness[index]);
            decimal tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 2);
            temp = (double)tmp;

            newChart.Series["Business Future"].Points.AddY(temp);

            temp = Convert.ToDouble(curBusiness[index]);
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 2);
            temp = (double)tmp;

            newChart.Series["Business Current"].Points.AddY(temp);

            temp = Convert.ToDouble(fuIT[index]);
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 2);
            temp = (double)tmp;

            newChart.Series["IT Future"].Points.AddY(temp);

            temp = Convert.ToDouble(curIT[index]);
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 2);
            temp = (double)tmp;

            newChart.Series["IT Current"].Points.AddY(temp);

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Application.StartupPath + "/" + newChart.Name + ".jpg", ChartImageFormat.Jpeg);
        }
        // when loaded in from another tool, checks the client data to see if any already exists
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
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
                    currentPerson = ClientDataControl.GetParticipants().Where(x => x.CodeName == column.Name).Single();
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
            ClientDataControl.LoadNewCUPE10(this);
            SetLastTenColumnVisibility(false);
            UpdateCupeScore();
            for (int i = 0; i < currentGrid.Rows.Count; i++)
            {
                ChangeTotalsByRow(i);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            changesMade = true;
            is20Question = true;
            ClientDataControl.cupeQuestions.Clear();
            ClientDataControl.LoadNewCUPE20(this);
            SetLastTenColumnVisibility(true);
            UpdateCupeScore();
            for (int i = 0; i < currentGrid.Rows.Count; i++)
            {
                ChangeTotalsByRow(i);
            }
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
            if (e.RowIndex != -1)
            {
                if (currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    oldCellValue = currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            GridClicked(sender, e);

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 3)
            {
                HelpCurrentStep = 4;
                StartTutorialMode();
            }
        }

        private void questionGridBusiFuture_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridBusiFuture;
            currentChart = busiFutureGraph;
            if (e.RowIndex != -1)
            {
                if (currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    oldCellValue = currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            GridClicked(sender, e);

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 3)
            {
                HelpCurrentStep = 4;
                StartTutorialMode();
            }
        }

        private void questionGridITCurrent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridITCurrent;
            currentChart = itCurrentGraph;
            if (e.RowIndex != -1)
            {
                if (currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    oldCellValue = currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            GridClicked(sender, e);

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 3)
            {
                HelpCurrentStep = 4;
                StartTutorialMode();
            }
        }

        private void questionGridBusinessCurrent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentGrid = questionGridBusinessCurrent;
            currentChart = busiCurrentGraph;
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    oldCellValue = currentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            GridClicked(sender, e);

            //Help / Tutorial Step
            if (HelpEnabled && HelpCurrentStep == 3)
            {
                HelpCurrentStep = 4;
                StartTutorialMode();
            }
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
                nodeA.Text ="A " + ClientDataControl.GetCupeQuestions()[index].ChoiceA;

                TreeNode nodeB = new TreeNode();
                nodeB.Text = "B " + ClientDataControl.GetCupeQuestions()[index].ChoiceB;

                TreeNode nodeC = new TreeNode();
                nodeC.Text = "C " + ClientDataControl.GetCupeQuestions()[index].ChoiceC;

                TreeNode nodeD = new TreeNode();
                nodeD.Text = "D " + ClientDataControl.GetCupeQuestions()[index].ChoiceD;

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
                ClientDataControl.SaveCUPE();
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

            BusITResponsesChart(busCurInfo, ITCurInfo, cupe, name);
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
            double temp;

            for (int i = 0; i < busCount; i++)
            {
                temp = Convert.ToDouble(bus[i]);
                if (!Double.IsNaN(temp))
                {
                    decimal tmp = Convert.ToDecimal(temp);
                    tmp = Math.Round(tmp, 2);
                    temp = (double)tmp;

                    newChart.Series["Business"].Points.AddXY(cupe[i], temp);
                }
            }

            for (int i = 0; i < itCount; i++)
            {
                temp = Convert.ToDouble(it[i]);
                if (!Double.IsNaN(temp) )
                {
                    decimal tmp = Convert.ToDecimal(temp);
                    tmp = Math.Round(tmp, 2);
                    temp = (double)tmp;

                    newChart.Series["IT"].Points.AddXY(cupe[i], temp);
                }

            }

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
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

            avgC = avgBusC + avgITc;
            avgU = avgBusU + avgITu;
            avgP = avgBusP + avgITp;
            avgE = avgBusE + avgITe;

            totalCurInfo.Add(avgC);
            totalCurInfo.Add(avgU);
            totalCurInfo.Add(avgP);
            totalCurInfo.Add(avgE);

            OverallChart(busCurInfo, ITCurInfo, totalCurInfo, id, name);
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

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
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
            double temp;

            for (int cnt = 0; cnt < count; cnt++)
            {
                temp = Convert.ToDouble(commodity[cnt]);
                decimal tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;
 
                newChart.Series["Total Commodity"].Points.AddXY((cnt + 1).ToString(), tmp);

                temp = Convert.ToDouble(utility[cnt]);
                tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                newChart.Series["Total Utility"].Points.AddXY((cnt + 1).ToString(), temp);

                temp = Convert.ToDouble(partner[cnt]);
                tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                newChart.Series["Total Partner"].Points.AddXY((cnt + 1).ToString(), temp);

                temp = Convert.ToDouble(enabler[cnt]);
                tmp = Convert.ToDecimal(temp);
                tmp = Math.Round(tmp, 2);
                temp = (double)tmp;

                newChart.Series["Total Enabler"].Points.AddXY((cnt + 1).ToString(), temp);
            }

            newChart.SaveImage(ClientDataControl.Client.FilePath + "/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
            newChart.SaveImage(Directory.GetCurrentDirectory() + @"/Charts/" + newChart.Text + ".jpg", ChartImageFormat.Jpeg);
        }

        private void StartTutorialMode()
        {
            if( HelpCurrentStep == 0 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("Begin by adding participants under the Edit->Participants menu",
                    this, 100, 5, 8000);
            }
            if (HelpCurrentStep == 1 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("Enter answers manually or go to File->Open Surveys to open any completed surveys",
                    this, 100, 5, 8000);
            }
            if (HelpCurrentStep == 2 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Change the displayed grid by clicking on one of the radio buttons",
                    this, 820, 450, 8000);
            }
            if (HelpCurrentStep == 3 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Select one of the Questions on the grid.",
                    this, 100, 100, 8000);
            }
            if (HelpCurrentStep == 4 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("The questions can be viewed and altered here. Edit one now by clicking it once and changing its text.",
                    this, 600, 600, 8000);
            }
            if (HelpCurrentStep == 5 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show("Sorted responses can be viewed here. Try sorting the answers by Highest CUPE Score.",
                    this, 20, 600, 8000);
            }
            if (HelpCurrentStep == 6 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("The pie chart's colors can be changed by going to Edit->Pie Graph Colors->_____. Change the Commodity wedge's colors.",
                    this, 30, 5, 8000);
            }
            if (HelpCurrentStep == 7 && HelpEnabled)
            {
                System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();
                myToolTip.IsBalloon = true;
                myToolTip.Show(string.Empty, this, 0);
                myToolTip.Show("More graphs are listed under the View Menu.",
                    this, 30, 5, 8000);
            }
        }

        private void beginTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpEnabled = true;
            HelpCurrentStep = 0;
            StartTutorialMode();

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

                //Help / Tutorial Step
                if (HelpEnabled && HelpCurrentStep == 6)
                {
                    HelpCurrentStep = 7;
                    StartTutorialMode();
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

        private void generatePowerpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Powah = new PowerPointGenerator();

            Powah.ReplaceTemplatePowerpoint();
        }

        private void emailPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailSettingsForm form = new EmailSettingsForm();
            form.Show();
        }

        private void changeAllTotals()
        {
            var temp = currentGrid;
            for( int i=1; i< questionGridBusinessCurrent.Columns.Count - 7; i++)
            {
                for( int o=0; o< 20; o++)
                {
                    currentGrid = questionGridBusinessCurrent;
                    ChangeTotalsByColumn(i, o);
                    ChangeTotalsByRow(o);
                    currentGrid = questionGridBusiFuture;
                    ChangeTotalsByColumn(i, o);
                    ChangeTotalsByRow(o);
                }
            }
            for (int i = 1; i < questionGridITCurrent.Columns.Count - 7; i++)
            {
                for (int o = 0; o < 20; o++)
                {
                    currentGrid = questionGridITCurrent;
                    ChangeTotalsByColumn(i, o);
                    ChangeTotalsByRow(o);
                    currentGrid = questionGridITFuture;
                    ChangeTotalsByColumn(i, o);
                    ChangeTotalsByRow(o);
                }
            }
            currentGrid = temp;
        }

        private void iTProviderRelationshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (questionGridBusiFuture.ColumnCount > averageIndex && questionGridBusinessCurrent.ColumnCount > averageIndex &&
                questionGridITCurrent.ColumnCount > averageIndex && questionGridITFuture.ColumnCount > averageIndex)
            {
                double currentBusiness = 0;
                double futureBusiness = 0;
                double currentIT = 0;
                double futureIT = 0;
                int count = 0;
                string total = "";
                double num = 0;
                for (int i = 0; i < 21; i++)
                {
                    if (questionGridBusiFuture.Rows[i].Cells[averageIndex + (questionGridBusiFuture.ColumnCount - 7)].Value != null)
                    {
                        total = questionGridBusiFuture.Rows[i].Cells[averageIndex + (questionGridBusiFuture.ColumnCount - 7)].Value.ToString();
                        num += Convert.ToDouble(total);
                        count++;
                    }
                }
                if (count > 0)
                    futureBusiness = num / count;

                num = 0;
                count = 0;
                for (int i = 0; i < 21; i++)
                {
                    if (questionGridBusinessCurrent.Rows[i].Cells[averageIndex + (questionGridBusinessCurrent.ColumnCount - 7)].Value != null)
                    {
                        total = questionGridBusinessCurrent.Rows[i].Cells[averageIndex + (questionGridBusinessCurrent.ColumnCount - 7)].Value.ToString();
                        num += Convert.ToDouble(total);
                        count++;
                    }
                }
                if (count > 0)
                    currentBusiness = num / count;

                num = 0;
                count = 0;
                for (int i = 0; i < 21; i++)
                {
                    if (questionGridITFuture.Rows[i].Cells[averageIndex + (questionGridITFuture.ColumnCount - 7)].Value != null)
                    {
                        total = questionGridITFuture.Rows[i].Cells[averageIndex + (questionGridITFuture.ColumnCount - 7)].Value.ToString();
                        num += Convert.ToDouble(total);
                        count++;
                    }
                }
                if (count > 0)
                    futureIT = num / count;

                num = 0;
                count = 0;
                for (int i = 0; i < 21; i++)
                {
                    if (questionGridITCurrent.Rows[i].Cells[averageIndex + (questionGridITCurrent.ColumnCount - 7)].Value != null)
                    {
                        total = questionGridITCurrent.Rows[i].Cells[averageIndex + (questionGridITCurrent.ColumnCount - 7)].Value.ToString();
                        num += Convert.ToDouble(total);
                        count++;
                    }
                }
                if (count > 0)
                    currentIT = num / count;

                int countA = 0;
                int countB = 0;
                int countC = 0;
                int countD = 0;
                count = 0;

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

                ITProviderRelationshipGraph chart = new ITProviderRelationshipGraph(currentBusiness, futureBusiness, currentIT, futureIT);

                chart.Show();

                Bitmap bmp = new Bitmap(chart.Width, chart.Height);
                chart.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));

                bmp.Save(ClientDataControl.Client.FilePath + "/IT Provider Relationship.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                bmp.Save(Directory.GetCurrentDirectory() + @"/Charts/" + "IT Provider Relationship.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach( DataGridView grid in grids )
            {
                grid.Rows.Clear();
            }
            foreach( Chart ch in charts)
            {
                ch.Series["BusiCurrent"].Points.Clear();
            }
            CUPETool_Load(sender, e);
            loadColumnNames();
            changeAllTotals();
            ClientDataControl.SetParticipants(new List<Person>());
            removePersonColumns();
            UpdateCupeScore();
            for (int i = 0; i < currentGrid.Rows.Count; i++)
            {
                ChangeTotalsByRow(i);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(this);
            if(form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.changeAllTotals();
            }
            
        }

    }// end class


}
