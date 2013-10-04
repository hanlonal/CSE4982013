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
            this.initiativeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.initiativeChart)).BeginInit();
            this.SuspendLayout();
            // 
            // initiativeChart
            // 
            chartArea1.Name = "ChartArea1";
            this.initiativeChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.initiativeChart.Legends.Add(legend1);
            this.initiativeChart.Location = new System.Drawing.Point(12, 12);
            this.initiativeChart.Name = "initiativeChart";
            this.initiativeChart.Size = new System.Drawing.Size(670, 362);
            this.initiativeChart.TabIndex = 0;
            this.initiativeChart.Text = "BOM Initiative Chart";
            this.initiativeChart.Click += new System.EventHandler(this.initiativeChart_Click);
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Location = new System.Drawing.Point(12, 396);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(184, 48);
            this.btnLoadChart.TabIndex = 1;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(263, 396);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(184, 48);
            this.btnSaveImage.TabIndex = 2;
            this.btnSaveImage.Text = "Save To Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(498, 396);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(184, 48);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BOMInitiativeBubbleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnLoadChart);
            this.Controls.Add(this.initiativeChart);
            this.Name = "BOMInitiativeBubbleChart";
            this.Text = "BOM Initiative Bubble Chart";
            ((System.ComponentModel.ISupportInitialize)(this.initiativeChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart initiativeChart;
        private System.Windows.Forms.Button btnLoadChart;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnClose;
    }
}