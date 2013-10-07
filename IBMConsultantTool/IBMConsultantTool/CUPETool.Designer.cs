namespace IBMConsultantTool
{
    partial class CUPETool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.questionGridBusinessCurrent = new System.Windows.Forms.DataGridView();
            this.Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numAnswers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.busiRadioButton = new System.Windows.Forms.RadioButton();
            this.itRadioButton = new System.Windows.Forms.RadioButton();
            this.questionGridITCurrent = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.busiCurrentGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sAMPLEEntitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.questionGridBusinessCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionGridITCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.busiCurrentGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sAMPLEEntitiesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // questionGridBusinessCurrent
            // 
            this.questionGridBusinessCurrent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.questionGridBusinessCurrent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionGridBusinessCurrent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Header,
            this.TotalA,
            this.TotalB,
            this.TotalC,
            this.TotalD,
            this.numAnswers,
            this.avgScore});
            this.questionGridBusinessCurrent.Location = new System.Drawing.Point(13, 50);
            this.questionGridBusinessCurrent.Margin = new System.Windows.Forms.Padding(4);
            this.questionGridBusinessCurrent.Name = "questionGridBusinessCurrent";
            this.questionGridBusinessCurrent.Size = new System.Drawing.Size(727, 507);
            this.questionGridBusinessCurrent.TabIndex = 0;
            this.questionGridBusinessCurrent.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionGrid_CellEndEdit);
            // 
            // Header
            // 
            this.Header.Frozen = true;
            this.Header.HeaderText = "I =IT  B=Business";
            this.Header.Name = "Header";
            this.Header.ReadOnly = true;
            // 
            // TotalA
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalA.DefaultCellStyle = dataGridViewCellStyle1;
            this.TotalA.HeaderText = "Total A";
            this.TotalA.Name = "TotalA";
            this.TotalA.ReadOnly = true;
            this.TotalA.Width = 50;
            // 
            // TotalB
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalB.DefaultCellStyle = dataGridViewCellStyle2;
            this.TotalB.HeaderText = "Total B";
            this.TotalB.Name = "TotalB";
            this.TotalB.ReadOnly = true;
            this.TotalB.Width = 50;
            // 
            // TotalC
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalC.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalC.HeaderText = "Total C";
            this.TotalC.Name = "TotalC";
            this.TotalC.ReadOnly = true;
            this.TotalC.Width = 50;
            // 
            // TotalD
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalD.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalD.HeaderText = "Total D";
            this.TotalD.Name = "TotalD";
            this.TotalD.ReadOnly = true;
            this.TotalD.Width = 50;
            // 
            // numAnswers
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAnswers.DefaultCellStyle = dataGridViewCellStyle5;
            this.numAnswers.HeaderText = "Total Answers";
            this.numAnswers.Name = "numAnswers";
            this.numAnswers.ReadOnly = true;
            this.numAnswers.Width = 60;
            // 
            // avgScore
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgScore.DefaultCellStyle = dataGridViewCellStyle6;
            this.avgScore.HeaderText = "Average Score";
            this.avgScore.Name = "avgScore";
            this.avgScore.ReadOnly = true;
            this.avgScore.Width = 70;
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(776, 529);
            this.addPersonButton.Margin = new System.Windows.Forms.Padding(4);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(112, 28);
            this.addPersonButton.TabIndex = 1;
            this.addPersonButton.Text = "Add Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // busiRadioButton
            // 
            this.busiRadioButton.AutoSize = true;
            this.busiRadioButton.Checked = true;
            this.busiRadioButton.Location = new System.Drawing.Point(776, 445);
            this.busiRadioButton.Name = "busiRadioButton";
            this.busiRadioButton.Size = new System.Drawing.Size(89, 20);
            this.busiRadioButton.TabIndex = 2;
            this.busiRadioButton.TabStop = true;
            this.busiRadioButton.Text = "Business";
            this.busiRadioButton.UseVisualStyleBackColor = true;
            this.busiRadioButton.Click += new System.EventHandler(this.busiRadioButton_Click);
            // 
            // itRadioButton
            // 
            this.itRadioButton.AutoSize = true;
            this.itRadioButton.Location = new System.Drawing.Point(776, 489);
            this.itRadioButton.Name = "itRadioButton";
            this.itRadioButton.Size = new System.Drawing.Size(131, 20);
            this.itRadioButton.TabIndex = 3;
            this.itRadioButton.Text = "IT Professional";
            this.itRadioButton.UseVisualStyleBackColor = true;
            this.itRadioButton.Click += new System.EventHandler(this.itRadioButton_Click);
            // 
            // questionGridITCurrent
            // 
            this.questionGridITCurrent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.questionGridITCurrent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionGridITCurrent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.questionGridITCurrent.Location = new System.Drawing.Point(13, 50);
            this.questionGridITCurrent.Margin = new System.Windows.Forms.Padding(4);
            this.questionGridITCurrent.Name = "questionGridITCurrent";
            this.questionGridITCurrent.Size = new System.Drawing.Size(727, 507);
            this.questionGridITCurrent.TabIndex = 4;
            this.questionGridITCurrent.Visible = false;
            this.questionGridITCurrent.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionGrid_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "I =IT  B=Business";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Total A";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.HeaderText = "Total B";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.HeaderText = "Total C";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn5.HeaderText = "Total D";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn6.HeaderText = "Total Answers";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn7.HeaderText = "Average Score";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // busiCurrentGraph
            // 
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 45;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 40;
            chartArea1.Area3DStyle.Rotation = 50;
            chartArea1.BackColor = System.Drawing.SystemColors.ScrollBar;
            chartArea1.BorderWidth = 4;
            chartArea1.Name = "ChartArea1";
            this.busiCurrentGraph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.busiCurrentGraph.Legends.Add(legend1);
            this.busiCurrentGraph.Location = new System.Drawing.Point(747, 50);
            this.busiCurrentGraph.Name = "busiCurrentGraph";
            this.busiCurrentGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "BusiCurrent";
            this.busiCurrentGraph.Series.Add(series1);
            this.busiCurrentGraph.Size = new System.Drawing.Size(407, 358);
            this.busiCurrentGraph.TabIndex = 5;
            this.busiCurrentGraph.Text = "chart1";
            // 
            // sAMPLEEntitiesBindingSource
            // 
            this.sAMPLEEntitiesBindingSource.DataSource = typeof(IBMConsultantTool.SAMPLEEntities);
            // 
            // CUPETool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 662);
            this.Controls.Add(this.busiCurrentGraph);
            this.Controls.Add(this.questionGridITCurrent);
            this.Controls.Add(this.itRadioButton);
            this.Controls.Add(this.busiRadioButton);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.questionGridBusinessCurrent);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CUPETool";
            this.Text = "CUPETool";
            this.Load += new System.EventHandler(this.CUPETool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionGridBusinessCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionGridITCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.busiCurrentGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sAMPLEEntitiesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView questionGridBusinessCurrent;
        private System.Windows.Forms.Button addPersonButton;
        private System.Windows.Forms.RadioButton busiRadioButton;
        private System.Windows.Forms.RadioButton itRadioButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalD;
        private System.Windows.Forms.DataGridViewTextBoxColumn numAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgScore;
        private System.Windows.Forms.DataGridView questionGridITCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataVisualization.Charting.Chart busiCurrentGraph;
        private System.Windows.Forms.BindingSource sAMPLEEntitiesBindingSource;
    }
}