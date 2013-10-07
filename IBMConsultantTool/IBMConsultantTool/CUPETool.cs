using System;
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
        DataGridView currentGrid;
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
            DataPoint point = new DataPoint();
            point.Color = Color.Fuchsia;
            point.BackGradientStyle = GradientStyle.Center;
            point.Name = "Commodity";
            point.Label = "Commodity";
            point.SetValueY(30);
            busiCurrentGraph.Series["BusiCurrent"].Points.Add(point);
            DataPoint point2 = new DataPoint();
            point2.Color = Color.Blue;
            point2.BackGradientStyle = GradientStyle.Center;
            point2.Name = "Utility";
            point2.Label = "Utility";
            point2.SetValueY(10);
            busiCurrentGraph.Series["BusiCurrent"].Points.Add(point2);
            DataPoint point3 = new DataPoint();
            point3.Color = Color.Orange;
            point3.BackGradientStyle = GradientStyle.Center;
            point3.Name = "Partner";
            point3.Label = "Partner";
            point3.SetValueY(5);
            busiCurrentGraph.Series["BusiCurrent"].Points.Add(point3);
            DataPoint point4 = new DataPoint();
            point4.Color = Color.Green;
            point4.BackGradientStyle = GradientStyle.Center;
            point4.Name = "Enabler";
            point4.Label = "Enabler";
            point4.SetValueY(25);
            busiCurrentGraph.Series["BusiCurrent"].Points.Add(point4);
            //busiCurrentGraph.Series["BusiCurrent"].Points[0].SetValueY(10);
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
            busiCurrentGraph.Series["BusiCurrent"].Points[0].SetValueXY("Commodity", numA);
            busiCurrentGraph.Series["BusiCurrent"].Points[1].SetValueXY("Utility", numB);
            busiCurrentGraph.Series["BusiCurrent"].Points[2].SetValueXY("Parter", numC);
            busiCurrentGraph.Series["BusiCurrent"].Points[3].SetValueXY("Enabler", numD);
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
            questionGridBusinessCurrent.Visible = false;
            questionGridITCurrent.Visible = true;
            currentGrid = questionGridITCurrent;

        }

        private void busiRadioButton_Click(object sender, EventArgs e)
        {
            questionGridITCurrent.Visible = false;
            questionGridBusinessCurrent.Visible = true;
            currentGrid = questionGridBusinessCurrent;
        }

    }


}
