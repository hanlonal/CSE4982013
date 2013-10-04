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
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.VerticalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineAxisY = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineAxisX = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panelList = new System.Windows.Forms.Panel();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Location = new System.Drawing.Point(554, 236);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(128, 48);
            this.btnLoadChart.TabIndex = 1;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(554, 290);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(128, 48);
            this.btnSaveImage.TabIndex = 2;
            this.btnSaveImage.Text = "Save To Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(554, 344);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 48);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.SystemColors.Window;
            this.panelChart.Controls.Add(this.VerticalLabel);
            this.panelChart.Controls.Add(this.label1);
            this.panelChart.Controls.Add(this.shapeContainer1);
            this.panelChart.Location = new System.Drawing.Point(12, 12);
            this.panelChart.Name = "panelChart";
            this.panelChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelChart.Size = new System.Drawing.Size(530, 380);
            this.panelChart.TabIndex = 4;
            // 
            // VerticalLabel
            // 
            this.VerticalLabel.AutoSize = true;
            this.VerticalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerticalLabel.Location = new System.Drawing.Point(15, 97);
            this.VerticalLabel.Name = "VerticalLabel";
            this.VerticalLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VerticalLabel.Size = new System.Drawing.Size(14, 143);
            this.VerticalLabel.TabIndex = 2;
            this.VerticalLabel.Text = "C\r\nr\r\ni\r\nt\r\ni\r\nc\r\na\r\nl\r\ni\r\nt\r\ny";
            this.VerticalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Differentiation";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineAxisY,
            this.lineAxisX});
            this.shapeContainer1.Size = new System.Drawing.Size(530, 380);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // lineAxisY
            // 
            this.lineAxisY.Name = "lineAxisY";
            this.lineAxisY.X1 = 60;
            this.lineAxisY.X2 = 60;
            this.lineAxisY.Y1 = 320;
            this.lineAxisY.Y2 = 20;
            // 
            // lineAxisX
            // 
            this.lineAxisX.Name = "lineAxisX";
            this.lineAxisX.X1 = 60;
            this.lineAxisX.X2 = 510;
            this.lineAxisX.Y1 = 320;
            this.lineAxisX.Y2 = 320;
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelList.Location = new System.Drawing.Point(554, 12);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(128, 218);
            this.panelList.TabIndex = 5;
            // 
            // BOMBubbleChartRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 404);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnLoadChart);
            this.Name = "BOMBubbleChartRedesign";
            this.Text = "BOM Initiative Bubble Chart";
            this.panelChart.ResumeLayout(false);
            this.panelChart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadChart;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Panel panelList;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineAxisX;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineAxisY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label VerticalLabel;
    }
}