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
            this.toDateText = new System.Windows.Forms.TextBox();
            this.fromDateText = new System.Windows.Forms.TextBox();
            this.analyticsListBox = new System.Windows.Forms.ListBox();
            this.metricsComboBox = new System.Windows.Forms.ComboBox();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.testingLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.countryCheckBox = new System.Windows.Forms.CheckBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.imperativesComboBox = new System.Windows.Forms.ComboBox();
            this.clearGridButton = new System.Windows.Forms.Button();
            this.itAttributesComboBox = new System.Windows.Forms.ComboBox();
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
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.businessTypeComboBox = new System.Windows.Forms.ComboBox();
            this.metricCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.trendGridView = new System.Windows.Forms.DataGridView();
            this.Collapse = new IBMConsultantTool.DataGridViewDisableButtonColumn();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.graphTypeComboBox = new System.Windows.Forms.ComboBox();
            this.filterPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trendGridView)).BeginInit();
            this.chartPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toDateText
            // 
            this.toDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toDateText.Enabled = false;
            this.toDateText.Location = new System.Drawing.Point(44, 563);
            this.toDateText.Name = "toDateText";
            this.toDateText.ReadOnly = true;
            this.toDateText.Size = new System.Drawing.Size(184, 26);
            this.toDateText.TabIndex = 0;
            this.toDateText.Tag = "All";
            this.toDateText.Text = "<End of Time>";
            this.toDateText.Click += new System.EventHandler(this.DateText_Click);
            // 
            // fromDateText
            // 
            this.fromDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fromDateText.Enabled = false;
            this.fromDateText.Location = new System.Drawing.Point(44, 505);
            this.fromDateText.Name = "fromDateText";
            this.fromDateText.ReadOnly = true;
            this.fromDateText.Size = new System.Drawing.Size(184, 26);
            this.fromDateText.TabIndex = 1;
            this.fromDateText.Tag = "All";
            this.fromDateText.Text = "<Beginning of Time>";
            this.fromDateText.Click += new System.EventHandler(this.DateText_Click);
            // 
            // analyticsListBox
            // 
            this.analyticsListBox.FormattingEnabled = true;
            this.analyticsListBox.ItemHeight = 20;
            this.analyticsListBox.Items.AddRange(new object[] {
            "Capabilities",
            "Imperatives",
            "CUPE Questions",
            "Objectives",
            "IT Attribues"});
            this.analyticsListBox.Location = new System.Drawing.Point(7, 3);
            this.analyticsListBox.Name = "analyticsListBox";
            this.analyticsListBox.Size = new System.Drawing.Size(130, 124);
            this.analyticsListBox.TabIndex = 2;
            this.analyticsListBox.Tag = "All";
            // 
            // metricsComboBox
            // 
            this.metricsComboBox.FormattingEnabled = true;
            this.metricsComboBox.Location = new System.Drawing.Point(592, 331);
            this.metricsComboBox.Name = "metricsComboBox";
            this.metricsComboBox.Size = new System.Drawing.Size(184, 28);
            this.metricsComboBox.TabIndex = 5;
            this.metricsComboBox.Tag = "All";
            this.metricsComboBox.Text = "<Choose Metric>";
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.filterPanel.Controls.Add(this.label6);
            this.filterPanel.Controls.Add(this.testingLabel);
            this.filterPanel.Controls.Add(this.label3);
            this.filterPanel.Controls.Add(this.label7);
            this.filterPanel.Controls.Add(this.label2);
            this.filterPanel.Controls.Add(this.label1);
            this.filterPanel.Controls.Add(this.label5);
            this.filterPanel.Controls.Add(this.countryCheckBox);
            this.filterPanel.Controls.Add(this.countryComboBox);
            this.filterPanel.Controls.Add(this.imperativesComboBox);
            this.filterPanel.Controls.Add(this.clearGridButton);
            this.filterPanel.Controls.Add(this.itAttributesComboBox);
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
            this.filterPanel.Controls.Add(this.regionComboBox);
            this.filterPanel.Controls.Add(this.businessTypeComboBox);
            this.filterPanel.Controls.Add(this.analyticsListBox);
            this.filterPanel.Controls.Add(this.toDateText);
            this.filterPanel.Controls.Add(this.fromDateText);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterPanel.Location = new System.Drawing.Point(0, 24);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(240, 705);
            this.filterPanel.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 4;
            this.label6.Tag = "All";
            this.label6.Text = "Business Type";
            // 
            // testingLabel
            // 
            this.testingLabel.AutoSize = true;
            this.testingLabel.Location = new System.Drawing.Point(40, 368);
            this.testingLabel.Name = "testingLabel";
            this.testingLabel.Size = new System.Drawing.Size(64, 20);
            this.testingLabel.TabIndex = 0;
            this.testingLabel.Tag = "All";
            this.testingLabel.Text = "Country";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 1;
            this.label3.Tag = "All";
            this.label3.Text = "Region";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 482);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 20);
            this.label7.TabIndex = 5;
            this.label7.Tag = "All";
            this.label7.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 20);
            this.label2.TabIndex = 27;
            this.label2.Tag = "All";
            this.label2.Text = "-------------------------------------------";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 26;
            this.label1.Tag = "All";
            this.label1.Text = "Filters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 540);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 3;
            this.label5.Tag = "All";
            this.label5.Text = "End Date";
            // 
            // countryCheckBox
            // 
            this.countryCheckBox.AutoSize = true;
            this.countryCheckBox.Location = new System.Drawing.Point(12, 394);
            this.countryCheckBox.Name = "countryCheckBox";
            this.countryCheckBox.Size = new System.Drawing.Size(15, 14);
            this.countryCheckBox.TabIndex = 25;
            this.countryCheckBox.Tag = "All";
            this.countryCheckBox.UseVisualStyleBackColor = true;
            // 
            // countryComboBox
            // 
            this.countryComboBox.Enabled = false;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(44, 394);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(184, 28);
            this.countryComboBox.TabIndex = 24;
            this.countryComboBox.Tag = "All";
            this.countryComboBox.Text = "<All Countries>";
            // 
            // imperativesComboBox
            // 
            this.imperativesComboBox.FormattingEnabled = true;
            this.imperativesComboBox.Location = new System.Drawing.Point(12, 133);
            this.imperativesComboBox.Name = "imperativesComboBox";
            this.imperativesComboBox.Size = new System.Drawing.Size(216, 28);
            this.imperativesComboBox.TabIndex = 22;
            this.imperativesComboBox.Tag = "Imperatives";
            this.imperativesComboBox.Text = "<Imperatives>";
            this.imperativesComboBox.Visible = false;
            // 
            // clearGridButton
            // 
            this.clearGridButton.Location = new System.Drawing.Point(44, 662);
            this.clearGridButton.Name = "clearGridButton";
            this.clearGridButton.Size = new System.Drawing.Size(139, 31);
            this.clearGridButton.TabIndex = 21;
            this.clearGridButton.Tag = "All";
            this.clearGridButton.Text = "Clear Grid";
            this.clearGridButton.UseVisualStyleBackColor = true;
            this.clearGridButton.Click += new System.EventHandler(this.clearGridButton_Click);
            // 
            // itAttributesComboBox
            // 
            this.itAttributesComboBox.FormattingEnabled = true;
            this.itAttributesComboBox.Location = new System.Drawing.Point(9, 235);
            this.itAttributesComboBox.Name = "itAttributesComboBox";
            this.itAttributesComboBox.Size = new System.Drawing.Size(216, 28);
            this.itAttributesComboBox.TabIndex = 20;
            this.itAttributesComboBox.Tag = "Capabilities";
            this.itAttributesComboBox.Text = "<Attributes>";
            this.itAttributesComboBox.Visible = false;
            // 
            // cupeTimeFrameComboBox
            // 
            this.cupeTimeFrameComboBox.FormattingEnabled = true;
            this.cupeTimeFrameComboBox.Location = new System.Drawing.Point(41, 235);
            this.cupeTimeFrameComboBox.Name = "cupeTimeFrameComboBox";
            this.cupeTimeFrameComboBox.Size = new System.Drawing.Size(184, 28);
            this.cupeTimeFrameComboBox.TabIndex = 19;
            this.cupeTimeFrameComboBox.Tag = "CUPE";
            this.cupeTimeFrameComboBox.Text = "<CUPE Time Frame>";
            this.cupeTimeFrameComboBox.Visible = false;
            // 
            // cupeAnswerTypeComboBox
            // 
            this.cupeAnswerTypeComboBox.FormattingEnabled = true;
            this.cupeAnswerTypeComboBox.Location = new System.Drawing.Point(41, 183);
            this.cupeAnswerTypeComboBox.Name = "cupeAnswerTypeComboBox";
            this.cupeAnswerTypeComboBox.Size = new System.Drawing.Size(184, 28);
            this.cupeAnswerTypeComboBox.TabIndex = 18;
            this.cupeAnswerTypeComboBox.Tag = "CUPE";
            this.cupeAnswerTypeComboBox.Text = "<CUPE Answer Type>";
            this.cupeAnswerTypeComboBox.Visible = false;
            // 
            // capabilitiesComboBox
            // 
            this.capabilitiesComboBox.FormattingEnabled = true;
            this.capabilitiesComboBox.Location = new System.Drawing.Point(9, 183);
            this.capabilitiesComboBox.Name = "capabilitiesComboBox";
            this.capabilitiesComboBox.Size = new System.Drawing.Size(216, 28);
            this.capabilitiesComboBox.TabIndex = 17;
            this.capabilitiesComboBox.Tag = "Capabilities";
            this.capabilitiesComboBox.Text = "<Capabilities>";
            this.capabilitiesComboBox.Visible = false;
            // 
            // cupeQuestionsComboBox
            // 
            this.cupeQuestionsComboBox.FormattingEnabled = true;
            this.cupeQuestionsComboBox.Location = new System.Drawing.Point(9, 133);
            this.cupeQuestionsComboBox.Name = "cupeQuestionsComboBox";
            this.cupeQuestionsComboBox.Size = new System.Drawing.Size(216, 28);
            this.cupeQuestionsComboBox.TabIndex = 16;
            this.cupeQuestionsComboBox.Tag = "CUPE";
            this.cupeQuestionsComboBox.Text = "<CUPE Questions>";
            this.cupeQuestionsComboBox.Visible = false;
            // 
            // objectiveNamesComboBox
            // 
            this.objectiveNamesComboBox.FormattingEnabled = true;
            this.objectiveNamesComboBox.Location = new System.Drawing.Point(9, 133);
            this.objectiveNamesComboBox.Name = "objectiveNamesComboBox";
            this.objectiveNamesComboBox.Size = new System.Drawing.Size(216, 28);
            this.objectiveNamesComboBox.TabIndex = 15;
            this.objectiveNamesComboBox.Tag = "Objectives";
            this.objectiveNamesComboBox.Text = "<Objectives>";
            this.objectiveNamesComboBox.Visible = false;
            // 
            // showResultsButton
            // 
            this.showResultsButton.Location = new System.Drawing.Point(44, 610);
            this.showResultsButton.Name = "showResultsButton";
            this.showResultsButton.Size = new System.Drawing.Size(139, 31);
            this.showResultsButton.TabIndex = 14;
            this.showResultsButton.Tag = "All";
            this.showResultsButton.Text = "Add to Grid";
            this.showResultsButton.UseVisualStyleBackColor = true;
            this.showResultsButton.Click += new System.EventHandler(this.showResultsButton_Click);
            // 
            // domainsComboBox
            // 
            this.domainsComboBox.FormattingEnabled = true;
            this.domainsComboBox.Location = new System.Drawing.Point(9, 133);
            this.domainsComboBox.Name = "domainsComboBox";
            this.domainsComboBox.Size = new System.Drawing.Size(216, 28);
            this.domainsComboBox.TabIndex = 12;
            this.domainsComboBox.Tag = "Capabilities";
            this.domainsComboBox.Text = "<Domains>";
            this.domainsComboBox.Visible = false;
            // 
            // fromDateCheckBox
            // 
            this.fromDateCheckBox.AutoSize = true;
            this.fromDateCheckBox.Location = new System.Drawing.Point(12, 505);
            this.fromDateCheckBox.Name = "fromDateCheckBox";
            this.fromDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.fromDateCheckBox.TabIndex = 11;
            this.fromDateCheckBox.Tag = "All";
            this.fromDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // toDateCheckBox
            // 
            this.toDateCheckBox.AutoSize = true;
            this.toDateCheckBox.Location = new System.Drawing.Point(12, 563);
            this.toDateCheckBox.Name = "toDateCheckBox";
            this.toDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.toDateCheckBox.TabIndex = 10;
            this.toDateCheckBox.Tag = "All";
            this.toDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionCheckBox
            // 
            this.regionCheckBox.AutoSize = true;
            this.regionCheckBox.Location = new System.Drawing.Point(12, 337);
            this.regionCheckBox.Name = "regionCheckBox";
            this.regionCheckBox.Size = new System.Drawing.Size(15, 14);
            this.regionCheckBox.TabIndex = 9;
            this.regionCheckBox.Tag = "All";
            this.regionCheckBox.UseVisualStyleBackColor = true;
            // 
            // typeCheckBox
            // 
            this.typeCheckBox.AutoSize = true;
            this.typeCheckBox.Location = new System.Drawing.Point(12, 451);
            this.typeCheckBox.Name = "typeCheckBox";
            this.typeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.typeCheckBox.TabIndex = 8;
            this.typeCheckBox.Tag = "All";
            this.typeCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionComboBox
            // 
            this.regionComboBox.Enabled = false;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(44, 337);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(184, 28);
            this.regionComboBox.TabIndex = 7;
            this.regionComboBox.Tag = "All";
            this.regionComboBox.Text = "<All Regions>";
            this.regionComboBox.SelectedIndexChanged += new System.EventHandler(this.regionComboBox_SelectedIndexChanged);
            this.regionComboBox.LostFocus += new System.EventHandler(this.regionComboBox_LostFocus);
            // 
            // businessTypeComboBox
            // 
            this.businessTypeComboBox.Enabled = false;
            this.businessTypeComboBox.FormattingEnabled = true;
            this.businessTypeComboBox.Location = new System.Drawing.Point(44, 451);
            this.businessTypeComboBox.Name = "businessTypeComboBox";
            this.businessTypeComboBox.Size = new System.Drawing.Size(184, 28);
            this.businessTypeComboBox.TabIndex = 6;
            this.businessTypeComboBox.Tag = "All";
            this.businessTypeComboBox.Text = "<All Business Types>";
            // 
            // metricCheckBox
            // 
            this.metricCheckBox.AutoSize = true;
            this.metricCheckBox.Checked = true;
            this.metricCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metricCheckBox.Location = new System.Drawing.Point(556, 331);
            this.metricCheckBox.Name = "metricCheckBox";
            this.metricCheckBox.Size = new System.Drawing.Size(15, 14);
            this.metricCheckBox.TabIndex = 0;
            this.metricCheckBox.Tag = "All";
            this.metricCheckBox.UseVisualStyleBackColor = true;
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
            this.dataPanel.Location = new System.Drawing.Point(246, 27);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(750, 298);
            this.dataPanel.TabIndex = 8;
            // 
            // trendGridView
            // 
            this.trendGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trendGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Collapse});
            this.trendGridView.Location = new System.Drawing.Point(3, 8);
            this.trendGridView.Name = "trendGridView";
            this.trendGridView.ReadOnly = true;
            this.trendGridView.Size = new System.Drawing.Size(740, 288);
            this.trendGridView.TabIndex = 0;
            this.trendGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.trendGridView_CellClick);
            this.trendGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.trendGridView_CellMouseDown);
            this.trendGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.trendGridView_DataBindingComplete);
            // 
            // Collapse
            // 
            this.Collapse.HeaderText = "";
            this.Collapse.Name = "Collapse";
            this.Collapse.ReadOnly = true;
            this.Collapse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Collapse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Collapse.Text = "+";
            this.Collapse.UseColumnTextForButtonValue = true;
            this.Collapse.Width = 30;
            // 
            // chartPanel
            // 
            this.chartPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chartPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chartPanel.Controls.Add(this.label4);
            this.chartPanel.Location = new System.Drawing.Point(246, 361);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(745, 356);
            this.chartPanel.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            // 
            // graphTypeComboBox
            // 
            this.graphTypeComboBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.graphTypeComboBox.FormattingEnabled = true;
            this.graphTypeComboBox.Location = new System.Drawing.Point(818, 331);
            this.graphTypeComboBox.Name = "graphTypeComboBox";
            this.graphTypeComboBox.Size = new System.Drawing.Size(173, 28);
            this.graphTypeComboBox.TabIndex = 10;
            this.graphTypeComboBox.Text = "<Graph Type>";
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.graphTypeComboBox);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.dataPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.metricsComboBox);
            this.Controls.Add(this.metricCheckBox);
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
            this.chartPanel.ResumeLayout(false);
            this.chartPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox toDateText;
        private System.Windows.Forms.TextBox fromDateText;
        private System.Windows.Forms.ListBox analyticsListBox;
        private System.Windows.Forms.ComboBox metricsComboBox;
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
        private System.Windows.Forms.ComboBox itAttributesComboBox;
        private System.Windows.Forms.Button clearGridButton;
        private System.Windows.Forms.ComboBox imperativesComboBox;
        private System.Windows.Forms.CheckBox countryCheckBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.ComboBox graphTypeComboBox;
        private System.Windows.Forms.Label testingLabel;
        private DataGridViewDisableButtonColumn Collapse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}