namespace IBMConsultantTool
{
    partial class CupeForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 7D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 5D);
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel1 = new System.Windows.Forms.Panel();
            this.personNameLabel = new System.Windows.Forms.Label();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.new5Question = new System.Windows.Forms.ToolStripMenuItem();
            this.new10Question = new System.Windows.Forms.ToolStripMenuItem();
            this.new20Question = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPersonButton = new System.Windows.Forms.Button();
            this.nextPersonButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalCommodityLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.totalUtilityLabel = new System.Windows.Forms.Label();
            this.totalPartnerLabel = new System.Windows.Forms.Label();
            this.totalEnablerLabel = new System.Windows.Forms.Label();
            this.totalFutureCommodityLabel = new System.Windows.Forms.Label();
            this.totalFutureUtilityLabel = new System.Windows.Forms.Label();
            this.totalFuturePartnerLabel = new System.Windows.Forms.Label();
            this.totalFutureEnablerLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.personListBox = new System.Windows.Forms.ListBox();
            this.questionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sAMPLEEntitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sAMPLEEntitiesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(153, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 224);
            this.panel1.TabIndex = 0;
            // 
            // personNameLabel
            // 
            this.personNameLabel.AutoSize = true;
            this.personNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.personNameLabel.Location = new System.Drawing.Point(290, 309);
            this.personNameLabel.Name = "personNameLabel";
            this.personNameLabel.Size = new System.Drawing.Size(37, 15);
            this.personNameLabel.TabIndex = 1;
            this.personNameLabel.Text = "Name";
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(274, 27);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(75, 23);
            this.addPersonButton.TabIndex = 2;
            this.addPersonButton.Text = "New Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new5Question,
            this.new10Question,
            this.new20Question});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // new5Question
            // 
            this.new5Question.Name = "new5Question";
            this.new5Question.Size = new System.Drawing.Size(86, 22);
            this.new5Question.Text = "5";
            // 
            // new10Question
            // 
            this.new10Question.Name = "new10Question";
            this.new10Question.Size = new System.Drawing.Size(86, 22);
            this.new10Question.Text = "10";
            // 
            // new20Question
            // 
            this.new20Question.Name = "new20Question";
            this.new20Question.Size = new System.Drawing.Size(86, 22);
            this.new20Question.Text = "20";
            this.new20Question.Click += new System.EventHandler(this.new20Question_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // previousPersonButton
            // 
            this.previousPersonButton.Location = new System.Drawing.Point(153, 27);
            this.previousPersonButton.Name = "previousPersonButton";
            this.previousPersonButton.Size = new System.Drawing.Size(75, 23);
            this.previousPersonButton.TabIndex = 4;
            this.previousPersonButton.Text = "< --";
            this.previousPersonButton.UseVisualStyleBackColor = true;
            this.previousPersonButton.Click += new System.EventHandler(this.previousPersonButton_Click);
            // 
            // nextPersonButton
            // 
            this.nextPersonButton.Location = new System.Drawing.Point(393, 27);
            this.nextPersonButton.Name = "nextPersonButton";
            this.nextPersonButton.Size = new System.Drawing.Size(75, 23);
            this.nextPersonButton.TabIndex = 5;
            this.nextPersonButton.Text = "-- >";
            this.nextPersonButton.UseVisualStyleBackColor = true;
            this.nextPersonButton.Click += new System.EventHandler(this.nextPersonButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Total Commodity";
            // 
            // totalCommodityLabel
            // 
            this.totalCommodityLabel.AutoSize = true;
            this.totalCommodityLabel.Location = new System.Drawing.Point(281, 391);
            this.totalCommodityLabel.Name = "totalCommodityLabel";
            this.totalCommodityLabel.Size = new System.Drawing.Size(0, 13);
            this.totalCommodityLabel.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 435);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Total Utility";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 479);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Total Partner";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(158, 530);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Total Enabler";
            // 
            // totalUtilityLabel
            // 
            this.totalUtilityLabel.AutoSize = true;
            this.totalUtilityLabel.Location = new System.Drawing.Point(281, 435);
            this.totalUtilityLabel.Name = "totalUtilityLabel";
            this.totalUtilityLabel.Size = new System.Drawing.Size(0, 13);
            this.totalUtilityLabel.TabIndex = 11;
            // 
            // totalPartnerLabel
            // 
            this.totalPartnerLabel.AutoSize = true;
            this.totalPartnerLabel.Location = new System.Drawing.Point(284, 479);
            this.totalPartnerLabel.Name = "totalPartnerLabel";
            this.totalPartnerLabel.Size = new System.Drawing.Size(0, 13);
            this.totalPartnerLabel.TabIndex = 12;
            // 
            // totalEnablerLabel
            // 
            this.totalEnablerLabel.AutoSize = true;
            this.totalEnablerLabel.Location = new System.Drawing.Point(287, 530);
            this.totalEnablerLabel.Name = "totalEnablerLabel";
            this.totalEnablerLabel.Size = new System.Drawing.Size(0, 13);
            this.totalEnablerLabel.TabIndex = 13;
            // 
            // totalFutureCommodityLabel
            // 
            this.totalFutureCommodityLabel.AutoSize = true;
            this.totalFutureCommodityLabel.Location = new System.Drawing.Point(348, 391);
            this.totalFutureCommodityLabel.Name = "totalFutureCommodityLabel";
            this.totalFutureCommodityLabel.Size = new System.Drawing.Size(0, 13);
            this.totalFutureCommodityLabel.TabIndex = 14;
            // 
            // totalFutureUtilityLabel
            // 
            this.totalFutureUtilityLabel.AutoSize = true;
            this.totalFutureUtilityLabel.Location = new System.Drawing.Point(351, 434);
            this.totalFutureUtilityLabel.Name = "totalFutureUtilityLabel";
            this.totalFutureUtilityLabel.Size = new System.Drawing.Size(0, 13);
            this.totalFutureUtilityLabel.TabIndex = 15;
            // 
            // totalFuturePartnerLabel
            // 
            this.totalFuturePartnerLabel.AutoSize = true;
            this.totalFuturePartnerLabel.Location = new System.Drawing.Point(351, 479);
            this.totalFuturePartnerLabel.Name = "totalFuturePartnerLabel";
            this.totalFuturePartnerLabel.Size = new System.Drawing.Size(0, 13);
            this.totalFuturePartnerLabel.TabIndex = 16;
            // 
            // totalFutureEnablerLabel
            // 
            this.totalFutureEnablerLabel.AutoSize = true;
            this.totalFutureEnablerLabel.Location = new System.Drawing.Point(351, 529);
            this.totalFutureEnablerLabel.Name = "totalFutureEnablerLabel";
            this.totalFutureEnablerLabel.Size = new System.Drawing.Size(0, 13);
            this.totalFutureEnablerLabel.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(245, 345);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Current";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(342, 345);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Future";
            // 
            // personListBox
            // 
            this.personListBox.FormattingEnabled = true;
            this.personListBox.Location = new System.Drawing.Point(12, 66);
            this.personListBox.Name = "personListBox";
            this.personListBox.Size = new System.Drawing.Size(69, 225);
            this.personListBox.TabIndex = 20;
            // 
            // questionChart
            // 
            chartArea1.Name = "ChartArea1";
            this.questionChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.questionChart.Legends.Add(legend1);
            this.questionChart.Location = new System.Drawing.Point(596, 24);
            this.questionChart.Name = "questionChart";
            this.questionChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            dataPoint1.Color = System.Drawing.Color.Lime;
            series1.Points.Add(dataPoint1);
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            dataPoint2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series2.Points.Add(dataPoint2);
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            dataPoint3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            series3.Points.Add(dataPoint3);
            series4.ChartArea = "ChartArea1";
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            series4.Legend = "Legend1";
            series4.Name = "Series4";
            series4.Points.Add(dataPoint4);
            this.questionChart.Series.Add(series1);
            this.questionChart.Series.Add(series2);
            this.questionChart.Series.Add(series3);
            this.questionChart.Series.Add(series4);
            this.questionChart.Size = new System.Drawing.Size(300, 300);
            this.questionChart.TabIndex = 21;
            this.questionChart.Text = "questionChart";
            title1.Name = "Title1";
            title1.Text = "Question Chart";
            this.questionChart.Titles.Add(title1);
            // 
            // sAMPLEEntitiesBindingSource
            // 
            this.sAMPLEEntitiesBindingSource.DataSource = typeof(IBMConsultantTool.SAMPLEEntities);
            // 
            // CupeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 566);
            this.Controls.Add(this.questionChart);
            this.Controls.Add(this.personListBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.totalFutureEnablerLabel);
            this.Controls.Add(this.totalFuturePartnerLabel);
            this.Controls.Add(this.totalFutureUtilityLabel);
            this.Controls.Add(this.totalFutureCommodityLabel);
            this.Controls.Add(this.totalEnablerLabel);
            this.Controls.Add(this.totalPartnerLabel);
            this.Controls.Add(this.totalUtilityLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.totalCommodityLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextPersonButton);
            this.Controls.Add(this.previousPersonButton);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.personNameLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CupeForm";
            this.Text = "CupeForm";
            this.Load += new System.EventHandler(this.CupeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CupeForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sAMPLEEntitiesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label personNameLabel;
        private System.Windows.Forms.Button addPersonButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem new5Question;
        private System.Windows.Forms.ToolStripMenuItem new10Question;
        private System.Windows.Forms.ToolStripMenuItem new20Question;
        private System.Windows.Forms.Button previousPersonButton;
        private System.Windows.Forms.Button nextPersonButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalCommodityLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label totalUtilityLabel;
        private System.Windows.Forms.Label totalPartnerLabel;
        private System.Windows.Forms.Label totalEnablerLabel;
        private System.Windows.Forms.Label totalFutureCommodityLabel;
        private System.Windows.Forms.Label totalFutureUtilityLabel;
        private System.Windows.Forms.Label totalFuturePartnerLabel;
        private System.Windows.Forms.Label totalFutureEnablerLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox personListBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart questionChart;
        private System.Windows.Forms.BindingSource sAMPLEEntitiesBindingSource;
    }
}