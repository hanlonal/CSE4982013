using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IBMConsultantTool
{
    public partial class BOMInitiativeBubbleChart : Form
    {
        //private Initiative initiative;
        private string name;
        private float criticality = 0;
        private float differentiation =0;
        private float effectiveness = 0;
        //private DataEntryForm mainForm;
        private BOMRedesign mainForm;

        public BOMInitiativeBubbleChart(BOMRedesign info)
        {
            mainForm = info;
            InitializeComponent();
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            int rowCount = 0;

            this.initiativeChart.ChartAreas["ChartArea1"].AxisX.Title = "Competitive Differenciation";
            this.initiativeChart.ChartAreas["ChartArea1"].AxisY.Title = "Criticality";
            this.initiativeChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this.initiativeChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            // Fixed graph value of the X and Y axies
            this.initiativeChart.ChartAreas["ChartArea1"].AxisX.Maximum = 15;
            this.initiativeChart.ChartAreas["ChartArea1"].AxisY.Maximum = 15;

            if (mainForm == null)
            {
                this.Hide();
            }

            //this.initiativeChart.


            
            for (int i = 0; i < mainForm.CategoryCount; i++)
            {
                rowCount++;
                for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                {
                    rowCount++;
                    for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                    {
                        name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                        criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                        differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                        effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                        

                        this.initiativeChart.Series.Add(name);
                        this.initiativeChart.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bubble;
                        this.initiativeChart.Series[name].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                        this.initiativeChart.Series[name].Points.AddXY(differentiation, criticality, effectiveness);
                        this.initiativeChart.Series[name].SmartLabelStyle.Enabled = true;
                        //name on the chart
                        //this.initiativeChart.Series[name].Label = name;
                        //this.initiativeChart.BackColor = Color.Transparent;
                        //this.initiativeChart.ChartAreas["ChartArea1"].BackColor = Color.Transparent;
                        //this.initiativeChart.Legends["Legend1"].BackColor = Color.Transparent;

                        rowCount++;
                    }
                }
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save File";
            save.Filter = "Image files (*.jpeg)|*.jpeg| All Files (*.*)|*.*";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.initiativeChart.SaveImage(File.Create(save.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
