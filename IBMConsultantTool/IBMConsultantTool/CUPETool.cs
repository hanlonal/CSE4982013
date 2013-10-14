﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IBMConsultantTool
{
    public partial class CUPETool : Form
    {
        List<DataGridView> grids = new List<DataGridView>();
        List<Chart> charts = new List<Chart>();
        DataGridView currentGrid;
        DataGridView toRemove;
       // DataGridView view = new DataGridView();
        Chart currentChart;
        int personCount = 0;

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
            
            foreach (DataGridView view in grids)
            {
                currentGrid = view;
                for (int i = 1; i < 21; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)currentGrid.Rows[0].Clone();
                    row.Cells[0].Value = "Question " + i;
                    row.Visible = true;
                    currentGrid.Rows.Add(row);

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
            CreateLabel();
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
                if ((string)(cell.Value) == "a" || (string)(cell.Value) == "A")
                {
                    totalA++;
                    count++;
                    total +=1;
                }
                if ((string)(cell.Value) == "b" || (string)(cell.Value) == "B")
                {
                    totalB++;
                    count++;
                    total+=2;
                }
                if ((string)cell.Value == "c" || (string)cell.Value == "C")
                {
                    totalC++;
                    count++;
                    total += 3;
                }
                if ((string)cell.Value == "d" || (string)cell.Value == "D")
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
            for (int i = 0; i < 20; i++)
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
                if ((string)row.Cells[colIndex].Value == "a" || (string)row.Cells[colIndex].Value == "A")
                {
                    totalA++;
                    count++;
                    total += 1;
                }
                if ((string)row.Cells[colIndex].Value == "b" || (string)row.Cells[colIndex].Value == "B")
                {
                    totalB++;
                    count++;
                    total += 2;
                }
                if ((string)row.Cells[colIndex].Value == "c" || (string)row.Cells[colIndex].Value == "C")
                {
                    totalC++;
                    count++;
                    total += 3;
                }
                if ((string)row.Cells[colIndex].Value == "d" || (string)row.Cells[colIndex].Value == "D")
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
            if (questionFilter.Text == "Highest Cupe Score")
            {
                FilterQuestionByHighestScore(questionFilterAmount.Text, averageIndex);
            }
            if (questionFilter.Text == "Lowest Cupe Score")
            {
                FilterQuestionByLowestScore(questionFilterAmount.Text, averageIndex);
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
                FilterQuestionByLowestScore(questionFilterAmount.Text, totalAIndex);
            }
            if (questionFilter.Text == "Least Utility")
            {
                FilterQuestionByLowestScore(questionFilterAmount.Text, totalBIndex);
            }
            if (questionFilter.Text == "Least Partner")
            {
                FilterQuestionByLowestScore(questionFilterAmount.Text, totalCIndex);
            }
            if (questionFilter.Text == "Least Enabler")
            {
                FilterQuestionByLowestScore(questionFilterAmount.Text, totalDIndex);
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
                //col.Width = 80;

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
            view.Location = new Point(10, 70);
            view.Width = 727;
            view.Height = 120;

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
            view.Location = new Point(10, 70);
            view.Width = 727;
            view.Height = 120;
            
         }

        private void iTStakeHoldersCurrentFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentFloats = new List<float>();
            List<float> futureFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                currentFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITCurrent.ColumnCount - 7].Value.ToString()));
            }
            count = 0;
            foreach(DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                 futureFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value.ToString()));
            }

            CreateChart(currentFloats, futureFloats, "IT StakeHolders Current/Future Comparison");
        }

        public void CreateChart(List<float> current, List<float> future, string name)
        {

        }

        private void businessLeadersCurrentFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> currentFloats = new List<float>();
            List<float> futureFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                currentFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value.ToString()));
            }
            count = 0;
            foreach(DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                 futureFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusiFuture.ColumnCount - 7].Value.ToString()));
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
                if (count == 0)
                    continue;
                count++;
                currentBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusinessCurrent.ColumnCount - 7].Value.ToString()));
            }
            count = 0;
            foreach(DataGridViewRow row in questionGridITCurrent.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                 currentITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITCurrent.ColumnCount - 7].Value.ToString()));
            }

            CreateChart(currentBusinessFloats, currentITFloats, "IT vs Business Leaders Current Comparison");
        }

        private void iTVsBusinessFutureComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<float> futureBusinessFloats = new List<float>();
            List<float> futureITFloats = new List<float>();
            int count = 0;
            foreach (DataGridViewRow row in questionGridBusiFuture.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                futureBusinessFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridBusiFuture.ColumnCount - 7].Value.ToString()));
            }
            count = 0;
            foreach(DataGridViewRow row in questionGridITFuture.Rows)
            {
                if (count == 0)
                    continue;
                count++;
                 futureITFloats.Add((float)Convert.ToDouble(row.Cells[averageIndex + questionGridITFuture.ColumnCount - 7].Value.ToString()));
            }

            CreateChart(futureBusinessFloats, futureITFloats, "IT vs Business Leaders Future Comparison");
        }

    }// end class


}