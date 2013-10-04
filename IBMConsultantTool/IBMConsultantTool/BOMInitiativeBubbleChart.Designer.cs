namespace IBMConsultantTool
{
    partial class BOMInitiativeBubbleChart
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
            this.initiativeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.initiativeChart)).BeginInit();
            this.SuspendLayout();
            // 
            // initiativeChart
            // 
            chartArea1.Name = "ChartArea1";
            this.initiativeChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.initiativeChart.Legends.Add(legend1);
            this.initiativeChart.Location = new System.Drawing.Point(79, 41);
            this.initiativeChart.Name = "initiativeChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.initiativeChart.Series.Add(series1);
            this.initiativeChart.Size = new System.Drawing.Size(300, 300);
            this.initiativeChart.TabIndex = 0;
            this.initiativeChart.Text = "chart1";
            // 
            // BOMInitiativeBubbleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 353);
            this.Controls.Add(this.initiativeChart);
            this.Name = "BOMInitiativeBubbleChart";
            this.Text = "BOMInitiativeBubbleChart";
            ((System.ComponentModel.ISupportInitialize)(this.initiativeChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart initiativeChart;
    }
}