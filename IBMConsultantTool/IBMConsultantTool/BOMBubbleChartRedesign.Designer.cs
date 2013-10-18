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
            Microsoft.VisualBasic.PowerPacks.LineShape lineAxisY;
            Microsoft.VisualBasic.PowerPacks.LineShape lineAxisX;
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.VerticalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panelList = new System.Windows.Forms.Panel();
            this.btnLoadObjectives = new System.Windows.Forms.Button();
            lineAxisY = new Microsoft.VisualBasic.PowerPacks.LineShape();
            lineAxisX = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // lineAxisY
            // 
            lineAxisY.Enabled = false;
            lineAxisY.Name = "lineAxisY";
            lineAxisY.X1 = 60;
            lineAxisY.X2 = 60;
            lineAxisY.Y1 = 850;
            lineAxisY.Y2 = 50;
            // 
            // lineAxisX
            // 
            lineAxisX.Enabled = false;
            lineAxisX.Name = "lineAxisX";
            lineAxisX.X1 = 60;
            lineAxisX.X2 = 960;
            lineAxisX.Y1 = 850;
            lineAxisX.Y2 = 850;
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadChart.Location = new System.Drawing.Point(1080, 765);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(242, 48);
            this.btnLoadChart.TabIndex = 1;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveImage.Location = new System.Drawing.Point(1080, 819);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(242, 48);
            this.btnSaveImage.TabIndex = 2;
            this.btnSaveImage.Text = "Save To Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1080, 873);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(242, 48);
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
            this.panelChart.Size = new System.Drawing.Size(1050, 900);
            this.panelChart.TabIndex = 4;
            // 
            // VerticalLabel
            // 
            this.VerticalLabel.AutoSize = true;
            this.VerticalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerticalLabel.Location = new System.Drawing.Point(17, 300);
            this.VerticalLabel.Name = "VerticalLabel";
            this.VerticalLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VerticalLabel.Size = new System.Drawing.Size(23, 264);
            this.VerticalLabel.TabIndex = 2;
            this.VerticalLabel.Text = "C\r\nr\r\ni\r\nt\r\ni\r\nc\r\na\r\nl\r\ni\r\nt\r\ny";
            this.VerticalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(400, 860);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Differentiation";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            lineAxisY,
            lineAxisX});
            this.shapeContainer1.Size = new System.Drawing.Size(1050, 900);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // panelList
            // 
            this.panelList.AutoScroll = true;
            this.panelList.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelList.Location = new System.Drawing.Point(1080, 12);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(242, 684);
            this.panelList.TabIndex = 5;
            this.panelList.Paint += new System.Windows.Forms.PaintEventHandler(this.panelList_Paint);
            // 
            // btnLoadObjectives
            // 
            this.btnLoadObjectives.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadObjectives.Location = new System.Drawing.Point(1080, 711);
            this.btnLoadObjectives.Name = "btnLoadObjectives";
            this.btnLoadObjectives.Size = new System.Drawing.Size(242, 48);
            this.btnLoadObjectives.TabIndex = 6;
            this.btnLoadObjectives.Text = "Load Objectives";
            this.btnLoadObjectives.UseVisualStyleBackColor = true;
            this.btnLoadObjectives.Click += new System.EventHandler(this.btnLoadObjectives_Click);
            // 
            // BOMBubbleChartRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 932);
            this.Controls.Add(this.btnLoadObjectives);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnLoadChart);
            this.MaximizeBox = false;
            this.Name = "BOMBubbleChartRedesign";
            this.Text = "BOM Bubble Chart";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label VerticalLabel;
        private System.Windows.Forms.Button btnLoadObjectives;
    }
}