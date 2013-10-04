namespace IBMConsultantTool
{
    partial class BOMBubbleChartRedesign
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.panelChart)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            chartArea1.Name = "ChartArea1";
            this.panelChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.panelChart.Legends.Add(legend1);
            this.panelChart.Location = new System.Drawing.Point(138, 42);
            this.panelChart.Name = "panelChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.panelChart.Series.Add(series1);
            this.panelChart.Size = new System.Drawing.Size(300, 300);
            this.panelChart.TabIndex = 0;
            this.panelChart.Text = "chart1";
            // 
            // BOMBubbleChartRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 338);
            this.Controls.Add(this.panelChart);
            this.Name = "BOMBubbleChartRedesign";
            this.Text = "BOMBubbleChartRedesign";
            ((System.ComponentModel.ISupportInitialize)(this.panelChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart panelChart;
    }
}