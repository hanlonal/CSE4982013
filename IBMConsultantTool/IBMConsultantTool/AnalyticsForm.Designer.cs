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
            this.metricCheckBox = new System.Windows.Forms.CheckBox();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.businessTypeComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.trendGridView = new System.Windows.Forms.DataGridView();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.clearGridButton = new System.Windows.Forms.Button();
            this.filterPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trendGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toDateText
            // 
            this.toDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toDateText.Enabled = false;
            this.toDateText.Location = new System.Drawing.Point(44, 577);
            this.toDateText.Name = "toDateText";
            this.toDateText.ReadOnly = true;
            this.toDateText.Size = new System.Drawing.Size(184, 26);
            this.toDateText.TabIndex = 0;
            this.toDateText.Tag = "All";
            this.toDateText.Text = "<To Date>";
            this.toDateText.Click += new System.EventHandler(this.DateText_Click);
            // 
            // fromDateText
            // 
            this.fromDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fromDateText.Enabled = false;
            this.fromDateText.Location = new System.Drawing.Point(44, 520);
            this.fromDateText.Name = "fromDateText";
            this.fromDateText.ReadOnly = true;
            this.fromDateText.Size = new System.Drawing.Size(184, 26);
            this.fromDateText.TabIndex = 1;
            this.fromDateText.Tag = "All";
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
            this.analyticsListBox.Tag = "All";
            // 
            // metricsComboBox
            // 
            this.metricsComboBox.FormattingEnabled = true;
            this.metricsComboBox.Location = new System.Drawing.Point(44, 319);
            this.metricsComboBox.Name = "metricsComboBox";
            this.metricsComboBox.Size = new System.Drawing.Size(184, 28);
            this.metricsComboBox.TabIndex = 5;
            this.metricsComboBox.Tag = "All";
            this.metricsComboBox.Text = "<Metric>";
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
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
            this.filterPanel.Controls.Add(this.metricCheckBox);
            this.filterPanel.Controls.Add(this.regionComboBox);
            this.filterPanel.Controls.Add(this.businessTypeComboBox);
            this.filterPanel.Controls.Add(this.analyticsListBox);
            this.filterPanel.Controls.Add(this.toDateText);
            this.filterPanel.Controls.Add(this.metricsComboBox);
            this.filterPanel.Controls.Add(this.fromDateText);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterPanel.Location = new System.Drawing.Point(0, 24);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(231, 705);
            this.filterPanel.TabIndex = 6;
            // 
            // itAttributesComboBox
            // 
            this.itAttributesComboBox.FormattingEnabled = true;
            this.itAttributesComboBox.Location = new System.Drawing.Point(12, 255);
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
            this.cupeTimeFrameComboBox.Location = new System.Drawing.Point(44, 255);
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
            this.cupeAnswerTypeComboBox.Location = new System.Drawing.Point(44, 203);
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
            this.capabilitiesComboBox.Location = new System.Drawing.Point(12, 203);
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
            this.cupeQuestionsComboBox.Location = new System.Drawing.Point(12, 153);
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
            this.objectiveNamesComboBox.Location = new System.Drawing.Point(12, 153);
            this.objectiveNamesComboBox.Name = "objectiveNamesComboBox";
            this.objectiveNamesComboBox.Size = new System.Drawing.Size(216, 28);
            this.objectiveNamesComboBox.TabIndex = 15;
            this.objectiveNamesComboBox.Tag = "Objectives";
            this.objectiveNamesComboBox.Text = "<Objectives>";
            this.objectiveNamesComboBox.Visible = false;
            // 
            // showResultsButton
            // 
            this.showResultsButton.Location = new System.Drawing.Point(44, 609);
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
            this.domainsComboBox.Location = new System.Drawing.Point(12, 153);
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
            this.fromDateCheckBox.Location = new System.Drawing.Point(12, 520);
            this.fromDateCheckBox.Name = "fromDateCheckBox";
            this.fromDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.fromDateCheckBox.TabIndex = 11;
            this.fromDateCheckBox.Tag = "All";
            this.fromDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // toDateCheckBox
            // 
            this.toDateCheckBox.AutoSize = true;
            this.toDateCheckBox.Location = new System.Drawing.Point(12, 577);
            this.toDateCheckBox.Name = "toDateCheckBox";
            this.toDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.toDateCheckBox.TabIndex = 10;
            this.toDateCheckBox.Tag = "All";
            this.toDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionCheckBox
            // 
            this.regionCheckBox.AutoSize = true;
            this.regionCheckBox.Location = new System.Drawing.Point(12, 396);
            this.regionCheckBox.Name = "regionCheckBox";
            this.regionCheckBox.Size = new System.Drawing.Size(15, 14);
            this.regionCheckBox.TabIndex = 9;
            this.regionCheckBox.Tag = "All";
            this.regionCheckBox.UseVisualStyleBackColor = true;
            // 
            // typeCheckBox
            // 
            this.typeCheckBox.AutoSize = true;
            this.typeCheckBox.Location = new System.Drawing.Point(12, 462);
            this.typeCheckBox.Name = "typeCheckBox";
            this.typeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.typeCheckBox.TabIndex = 8;
            this.typeCheckBox.Tag = "All";
            this.typeCheckBox.UseVisualStyleBackColor = true;
            // 
            // metricCheckBox
            // 
            this.metricCheckBox.AutoSize = true;
            this.metricCheckBox.Location = new System.Drawing.Point(12, 319);
            this.metricCheckBox.Name = "metricCheckBox";
            this.metricCheckBox.Size = new System.Drawing.Size(15, 14);
            this.metricCheckBox.TabIndex = 0;
            this.metricCheckBox.Tag = "All";
            this.metricCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionComboBox
            // 
            this.regionComboBox.Enabled = false;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(44, 396);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(184, 28);
            this.regionComboBox.TabIndex = 7;
            this.regionComboBox.Tag = "All";
            this.regionComboBox.Text = "<Region>";
            // 
            // businessTypeComboBox
            // 
            this.businessTypeComboBox.Enabled = false;
            this.businessTypeComboBox.FormattingEnabled = true;
            this.businessTypeComboBox.Location = new System.Drawing.Point(44, 462);
            this.businessTypeComboBox.Name = "businessTypeComboBox";
            this.businessTypeComboBox.Size = new System.Drawing.Size(184, 28);
            this.businessTypeComboBox.TabIndex = 6;
            this.businessTypeComboBox.Tag = "All";
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
    }
}