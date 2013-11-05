namespace IBMConsultantTool
{
    partial class AnalyticsForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fromDateText = new System.Windows.Forms.TextBox();
            this.analyticsListBox = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.cupeTimeFrameComboBox = new System.Windows.Forms.ComboBox();
            this.cupeAnswerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.capabilitiesComboBox = new System.Windows.Forms.ComboBox();
            this.cupeQuestionsComboBox = new System.Windows.Forms.ComboBox();
            this.objectiveNamesComboBox = new System.Windows.Forms.ComboBox();
            this.showResultsButton = new System.Windows.Forms.Button();
            this.domainsComboBox = new System.Windows.Forms.ComboBox();
            this.fromDateCheckBox = new System.Windows.Forms.CheckBox();
            this.toDateCheckBox = new System.Windows.Forms.CheckBox();
            this.regionCheckBox = new System.Windows.Forms.CheckBox();
            this.typeCheckBox = new System.Windows.Forms.CheckBox();
            this.metricCheckBox = new System.Windows.Forms.CheckBox();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.businessTypeComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.trendGridView = new System.Windows.Forms.DataGridView();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.filterPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trendGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Location = new System.Drawing.Point(44, 614);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(184, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "<To Date>";
            this.textBox1.Click += new System.EventHandler(this.DateText_Click);
            // 
            // fromDateText
            // 
            this.fromDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fromDateText.Location = new System.Drawing.Point(44, 557);
            this.fromDateText.Name = "fromDateText";
            this.fromDateText.ReadOnly = true;
            this.fromDateText.Size = new System.Drawing.Size(184, 26);
            this.fromDateText.TabIndex = 1;
            this.fromDateText.Text = "<From Date>";
            this.fromDateText.Click += new System.EventHandler(this.DateText_Click);
            // 
            // analyticsListBox
            // 
            this.analyticsListBox.FormattingEnabled = true;
            this.analyticsListBox.ItemHeight = 20;
            this.analyticsListBox.Items.AddRange(new object[] {
            "Capabilities",
            "Initiatives",
            "CUPE Questions",
            "Objectives",
            "IT Attribues"});
            this.analyticsListBox.Location = new System.Drawing.Point(7, 3);
            this.analyticsListBox.Name = "analyticsListBox";
            this.analyticsListBox.Size = new System.Drawing.Size(130, 144);
            this.analyticsListBox.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(44, 356);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 28);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "<Metric>";
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.filterPanel.Controls.Add(this.cupeTimeFrameComboBox);
            this.filterPanel.Controls.Add(this.cupeAnswerTypeComboBox);
            this.filterPanel.Controls.Add(this.capabilitiesComboBox);
            this.filterPanel.Controls.Add(this.cupeQuestionsComboBox);
            this.filterPanel.Controls.Add(this.objectiveNamesComboBox);
            this.filterPanel.Controls.Add(this.showResultsButton);
            this.filterPanel.Controls.Add(this.domainsComboBox);
            this.filterPanel.Controls.Add(this.fromDateCheckBox);
            this.filterPanel.Controls.Add(this.toDateCheckBox);
            this.filterPanel.Controls.Add(this.regionCheckBox);
            this.filterPanel.Controls.Add(this.typeCheckBox);
            this.filterPanel.Controls.Add(this.metricCheckBox);
            this.filterPanel.Controls.Add(this.regionComboBox);
            this.filterPanel.Controls.Add(this.businessTypeComboBox);
            this.filterPanel.Controls.Add(this.analyticsListBox);
            this.filterPanel.Controls.Add(this.textBox1);
            this.filterPanel.Controls.Add(this.comboBox1);
            this.filterPanel.Controls.Add(this.fromDateText);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterPanel.Location = new System.Drawing.Point(0, 24);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(231, 705);
            this.filterPanel.TabIndex = 6;
            // 
            // cupeTimeFrameComboBox
            // 
            this.cupeTimeFrameComboBox.FormattingEnabled = true;
            this.cupeTimeFrameComboBox.Location = new System.Drawing.Point(44, 323);
            this.cupeTimeFrameComboBox.Name = "cupeTimeFrameComboBox";
            this.cupeTimeFrameComboBox.Size = new System.Drawing.Size(184, 28);
            this.cupeTimeFrameComboBox.TabIndex = 19;
            this.cupeTimeFrameComboBox.Text = "<CUPE Time Frame>";
            // 
            // cupeAnswerTypeComboBox
            // 
            this.cupeAnswerTypeComboBox.FormattingEnabled = true;
            this.cupeAnswerTypeComboBox.Location = new System.Drawing.Point(44, 289);
            this.cupeAnswerTypeComboBox.Name = "cupeAnswerTypeComboBox";
            this.cupeAnswerTypeComboBox.Size = new System.Drawing.Size(184, 28);
            this.cupeAnswerTypeComboBox.TabIndex = 18;
            this.cupeAnswerTypeComboBox.Text = "<CUPE Answer Type>";
            // 
            // capabilitiesComboBox
            // 
            this.capabilitiesComboBox.FormattingEnabled = true;
            this.capabilitiesComboBox.Location = new System.Drawing.Point(12, 255);
            this.capabilitiesComboBox.Name = "capabilitiesComboBox";
            this.capabilitiesComboBox.Size = new System.Drawing.Size(216, 28);
            this.capabilitiesComboBox.TabIndex = 17;
            this.capabilitiesComboBox.Text = "<Capabilities>";
            // 
            // cupeQuestionsComboBox
            // 
            this.cupeQuestionsComboBox.FormattingEnabled = true;
            this.cupeQuestionsComboBox.Location = new System.Drawing.Point(12, 153);
            this.cupeQuestionsComboBox.Name = "cupeQuestionsComboBox";
            this.cupeQuestionsComboBox.Size = new System.Drawing.Size(216, 28);
            this.cupeQuestionsComboBox.TabIndex = 16;
            this.cupeQuestionsComboBox.Text = "<CUPE Questions>";
            // 
            // objectiveNamesComboBox
            // 
            this.objectiveNamesComboBox.FormattingEnabled = true;
            this.objectiveNamesComboBox.Location = new System.Drawing.Point(12, 187);
            this.objectiveNamesComboBox.Name = "objectiveNamesComboBox";
            this.objectiveNamesComboBox.Size = new System.Drawing.Size(216, 28);
            this.objectiveNamesComboBox.TabIndex = 15;
            this.objectiveNamesComboBox.Text = "<Objectives>";
            // 
            // showResultsButton
            // 
            this.showResultsButton.Location = new System.Drawing.Point(44, 662);
            this.showResultsButton.Name = "showResultsButton";
            this.showResultsButton.Size = new System.Drawing.Size(139, 31);
            this.showResultsButton.TabIndex = 14;
            this.showResultsButton.Text = "Show Results";
            this.showResultsButton.UseVisualStyleBackColor = true;
            this.showResultsButton.Click += new System.EventHandler(this.showResultsButton_Click);
            // 
            // domainsComboBox
            // 
            this.domainsComboBox.FormattingEnabled = true;
            this.domainsComboBox.Location = new System.Drawing.Point(12, 221);
            this.domainsComboBox.Name = "domainsComboBox";
            this.domainsComboBox.Size = new System.Drawing.Size(216, 28);
            this.domainsComboBox.TabIndex = 12;
            this.domainsComboBox.Text = "<Domains>";
            // 
            // fromDateCheckBox
            // 
            this.fromDateCheckBox.AutoSize = true;
            this.fromDateCheckBox.Location = new System.Drawing.Point(12, 557);
            this.fromDateCheckBox.Name = "fromDateCheckBox";
            this.fromDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.fromDateCheckBox.TabIndex = 11;
            this.fromDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // toDateCheckBox
            // 
            this.toDateCheckBox.AutoSize = true;
            this.toDateCheckBox.Location = new System.Drawing.Point(12, 614);
            this.toDateCheckBox.Name = "toDateCheckBox";
            this.toDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.toDateCheckBox.TabIndex = 10;
            this.toDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionCheckBox
            // 
            this.regionCheckBox.AutoSize = true;
            this.regionCheckBox.Location = new System.Drawing.Point(12, 433);
            this.regionCheckBox.Name = "regionCheckBox";
            this.regionCheckBox.Size = new System.Drawing.Size(15, 14);
            this.regionCheckBox.TabIndex = 9;
            this.regionCheckBox.UseVisualStyleBackColor = true;
            // 
            // typeCheckBox
            // 
            this.typeCheckBox.AutoSize = true;
            this.typeCheckBox.Location = new System.Drawing.Point(12, 499);
            this.typeCheckBox.Name = "typeCheckBox";
            this.typeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.typeCheckBox.TabIndex = 8;
            this.typeCheckBox.UseVisualStyleBackColor = true;
            // 
            // metricCheckBox
            // 
            this.metricCheckBox.AutoSize = true;
            this.metricCheckBox.Location = new System.Drawing.Point(12, 356);
            this.metricCheckBox.Name = "metricCheckBox";
            this.metricCheckBox.Size = new System.Drawing.Size(15, 14);
            this.metricCheckBox.TabIndex = 0;
            this.metricCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionComboBox
            // 
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(44, 433);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(184, 28);
            this.regionComboBox.TabIndex = 7;
            this.regionComboBox.Text = "<Region>";
            // 
            // businessTypeComboBox
            // 
            this.businessTypeComboBox.FormattingEnabled = true;
            this.businessTypeComboBox.Location = new System.Drawing.Point(44, 499);
            this.businessTypeComboBox.Name = "businessTypeComboBox";
            this.businessTypeComboBox.Size = new System.Drawing.Size(184, 28);
            this.businessTypeComboBox.TabIndex = 6;
            this.businessTypeComboBox.Text = "<Business Type>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // dataPanel
            // 
            this.dataPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataPanel.Controls.Add(this.trendGridView);
            this.dataPanel.Location = new System.Drawing.Point(237, 27);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(737, 298);
            this.dataPanel.TabIndex = 8;
            // 
            // trendGridView
            // 
            this.trendGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trendGridView.Location = new System.Drawing.Point(3, 3);
            this.trendGridView.Name = "trendGridView";
            this.trendGridView.ReadOnly = true;
            this.trendGridView.Size = new System.Drawing.Size(727, 275);
            this.trendGridView.TabIndex = 0;
            // 
            // chartPanel
            // 
            this.chartPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chartPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chartPanel.Location = new System.Drawing.Point(237, 331);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(737, 386);
            this.chartPanel.TabIndex = 9;
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.dataPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AnalyticsForm";
            this.Text = "AnalyticsForm";
            this.Load += new System.EventHandler(this.AnalyticsForm_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.dataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trendGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox fromDateText;
        private System.Windows.Forms.ListBox analyticsListBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.CheckBox fromDateCheckBox;
        private System.Windows.Forms.CheckBox toDateCheckBox;
        private System.Windows.Forms.CheckBox regionCheckBox;
        private System.Windows.Forms.CheckBox typeCheckBox;
        private System.Windows.Forms.CheckBox metricCheckBox;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox businessTypeComboBox;
        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.Button showResultsButton;
        private System.Windows.Forms.ComboBox domainsComboBox;
        private System.Windows.Forms.ComboBox cupeQuestionsComboBox;
        private System.Windows.Forms.ComboBox objectiveNamesComboBox;
        private System.Windows.Forms.ComboBox capabilitiesComboBox;
        private System.Windows.Forms.ComboBox cupeTimeFrameComboBox;
        private System.Windows.Forms.ComboBox cupeAnswerTypeComboBox;
        private System.Windows.Forms.DataGridView trendGridView;
    }
}