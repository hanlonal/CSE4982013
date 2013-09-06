namespace IBMConsultantTool
{
    partial class BOMBubbleChart
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
            this.BubbleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleChart)).BeginInit();
            this.SuspendLayout();
            // 
            // BubbleChart
            // 
            this.BubbleChart.BackColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this.BubbleChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.BubbleChart.Legends.Add(legend1);
            this.BubbleChart.Location = new System.Drawing.Point(21, 12);
            this.BubbleChart.Name = "BubbleChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.BubbleChart.Series.Add(series1);
            this.BubbleChart.Size = new System.Drawing.Size(546, 455);
            this.BubbleChart.TabIndex = 0;
            this.BubbleChart.Text = "Bubble Chart";
            // 
            // BOMBubbleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 479);
            this.Controls.Add(this.BubbleChart);
            this.Name = "BOMBubbleChart";
            this.Text = "BOMBubbleChart";
            ((System.ComponentModel.ISupportInitialize)(this.BubbleChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart BubbleChart;
    }
}