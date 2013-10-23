namespace IBMConsultantTool
{
    partial class ITCapTool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addDomainButton = new System.Windows.Forms.Button();
            this.surveryMakerGrid = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Capability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AsIs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToBe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCapabilityButton = new System.Windows.Forms.Button();
            this.addQuestionButton = new System.Windows.Forms.Button();
            this.domainNameTextBox = new System.Windows.Forms.TextBox();
            this.mainMenuToolBar = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.View = new System.Windows.Forms.ToolStripMenuItem();
            this.systemsAgendaCapabilityAssesmentResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capabilityAssesmentSummaryScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capabilityGapHeatmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prioritizedCapabilityGapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Insert = new System.Windows.Forms.ToolStripMenuItem();
            this.domainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeDefaults = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Workshop = new System.Windows.Forms.ToolStripMenuItem();
            this.LiveDataEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.Prioritization = new System.Windows.Forms.ToolStripMenuItem();
            this.SurveryMaker = new System.Windows.Forms.ToolStripMenuItem();
            this.capabilityNameTextBox = new System.Windows.Forms.TextBox();
            this.questionNameTextBox = new System.Windows.Forms.TextBox();
            this.removeEntityButton = new System.Windows.Forms.Button();
            this.domainList = new System.Windows.Forms.ComboBox();
            this.capabilitiesList = new System.Windows.Forms.ComboBox();
            this.questionList = new System.Windows.Forms.ComboBox();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.liveDataEntryGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddComment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prioritizationGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapabilityGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriorityGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.surveryMakerGrid)).BeginInit();
            this.mainMenuToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liveDataEntryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prioritizationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // addDomainButton
            // 
            this.addDomainButton.Location = new System.Drawing.Point(1462, 88);
            this.addDomainButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addDomainButton.Name = "addDomainButton";
            this.addDomainButton.Size = new System.Drawing.Size(112, 35);
            this.addDomainButton.TabIndex = 0;
            this.addDomainButton.Text = "+ Domain";
            this.addDomainButton.UseVisualStyleBackColor = true;
            this.addDomainButton.Visible = false;
            this.addDomainButton.Click += new System.EventHandler(this.addDomainButton_Click);
            // 
            // surveryMakerGrid
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.surveryMakerGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.surveryMakerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.surveryMakerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Capability,
            this.AsIs,
            this.ToBe,
            this.Type});
            this.surveryMakerGrid.EnableHeadersVisualStyles = false;
            this.surveryMakerGrid.Location = new System.Drawing.Point(13, 87);
            this.surveryMakerGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.surveryMakerGrid.MultiSelect = false;
            this.surveryMakerGrid.Name = "surveryMakerGrid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.surveryMakerGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.surveryMakerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.surveryMakerGrid.Size = new System.Drawing.Size(717, 567);
            this.surveryMakerGrid.TabIndex = 3;
            this.surveryMakerGrid.Visible = false;
            this.surveryMakerGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.surveryMakerGrid_CellClick);
            this.surveryMakerGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.surveryMakerGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.surveyMakerGrid_MouseDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 60;
            // 
            // Capability
            // 
            this.Capability.HeaderText = "Capability";
            this.Capability.Name = "Capability";
            this.Capability.ReadOnly = true;
            this.Capability.Width = 400;
            // 
            // AsIs
            // 
            this.AsIs.HeaderText = "As Is";
            this.AsIs.Name = "AsIs";
            this.AsIs.Visible = false;
            // 
            // ToBe
            // 
            this.ToBe.HeaderText = "To Be";
            this.ToBe.Name = "ToBe";
            this.ToBe.Visible = false;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.Visible = false;
            // 
            // addCapabilityButton
            // 
            this.addCapabilityButton.Location = new System.Drawing.Point(1462, 186);
            this.addCapabilityButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addCapabilityButton.Name = "addCapabilityButton";
            this.addCapabilityButton.Size = new System.Drawing.Size(112, 35);
            this.addCapabilityButton.TabIndex = 5;
            this.addCapabilityButton.Text = "+Cap";
            this.addCapabilityButton.UseVisualStyleBackColor = true;
            this.addCapabilityButton.Visible = false;
            // 
            // addQuestionButton
            // 
            this.addQuestionButton.Location = new System.Drawing.Point(1462, 286);
            this.addQuestionButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addQuestionButton.Name = "addQuestionButton";
            this.addQuestionButton.Size = new System.Drawing.Size(112, 35);
            this.addQuestionButton.TabIndex = 6;
            this.addQuestionButton.Text = "+Question";
            this.addQuestionButton.UseVisualStyleBackColor = true;
            this.addQuestionButton.Visible = false;
            // 
            // domainNameTextBox
            // 
            this.domainNameTextBox.Location = new System.Drawing.Point(1425, 132);
            this.domainNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.domainNameTextBox.Name = "domainNameTextBox";
            this.domainNameTextBox.Size = new System.Drawing.Size(148, 26);
            this.domainNameTextBox.TabIndex = 7;
            this.domainNameTextBox.Visible = false;
            // 
            // mainMenuToolBar
            // 
            this.mainMenuToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.View,
            this.Insert,
            this.ChangeDefaults,
            this.Workshop});
            this.mainMenuToolBar.Location = new System.Drawing.Point(0, 0);
            this.mainMenuToolBar.Name = "mainMenuToolBar";
            this.mainMenuToolBar.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.mainMenuToolBar.Size = new System.Drawing.Size(1370, 25);
            this.mainMenuToolBar.TabIndex = 8;
            this.mainMenuToolBar.Text = "menuStrip1";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(37, 19);
            this.File.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newToolStripMenuItem.Text = "New Survey";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // View
            // 
            this.View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemsAgendaCapabilityAssesmentResultsToolStripMenuItem,
            this.capabilityAssesmentSummaryScoresToolStripMenuItem,
            this.capabilityGapHeatmapToolStripMenuItem,
            this.prioritizedCapabilityGapsToolStripMenuItem});
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(44, 19);
            this.View.Text = "View";
            // 
            // systemsAgendaCapabilityAssesmentResultsToolStripMenuItem
            // 
            this.systemsAgendaCapabilityAssesmentResultsToolStripMenuItem.Name = "systemsAgendaCapabilityAssesmentResultsToolStripMenuItem";
            this.systemsAgendaCapabilityAssesmentResultsToolStripMenuItem.Size = new System.Drawing.Size(317, 22);
            this.systemsAgendaCapabilityAssesmentResultsToolStripMenuItem.Text = "Systems Agenda Capability Assesment Results";
            // 
            // capabilityAssesmentSummaryScoresToolStripMenuItem
            // 
            this.capabilityAssesmentSummaryScoresToolStripMenuItem.Name = "capabilityAssesmentSummaryScoresToolStripMenuItem";
            this.capabilityAssesmentSummaryScoresToolStripMenuItem.Size = new System.Drawing.Size(317, 22);
            this.capabilityAssesmentSummaryScoresToolStripMenuItem.Text = "Capability Assesment Summary Scores";
            // 
            // capabilityGapHeatmapToolStripMenuItem
            // 
            this.capabilityGapHeatmapToolStripMenuItem.Name = "capabilityGapHeatmapToolStripMenuItem";
            this.capabilityGapHeatmapToolStripMenuItem.Size = new System.Drawing.Size(317, 22);
            this.capabilityGapHeatmapToolStripMenuItem.Text = "Capability Gap Heatmap";
            // 
            // prioritizedCapabilityGapsToolStripMenuItem
            // 
            this.prioritizedCapabilityGapsToolStripMenuItem.Name = "prioritizedCapabilityGapsToolStripMenuItem";
            this.prioritizedCapabilityGapsToolStripMenuItem.Size = new System.Drawing.Size(317, 22);
            this.prioritizedCapabilityGapsToolStripMenuItem.Text = "Prioritized Capability Gaps";
            // 
            // Insert
            // 
            this.Insert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.domainToolStripMenuItem,
            this.capabilityToolStripMenuItem,
            this.questionToolStripMenuItem});
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(48, 19);
            this.Insert.Text = "Insert";
            // 
            // domainToolStripMenuItem
            // 
            this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
            this.domainToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.domainToolStripMenuItem.Text = "Domain";
            // 
            // capabilityToolStripMenuItem
            // 
            this.capabilityToolStripMenuItem.Name = "capabilityToolStripMenuItem";
            this.capabilityToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.capabilityToolStripMenuItem.Text = "Capability";
            // 
            // questionToolStripMenuItem
            // 
            this.questionToolStripMenuItem.Name = "questionToolStripMenuItem";
            this.questionToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.questionToolStripMenuItem.Text = "Question";
            // 
            // ChangeDefaults
            // 
            this.ChangeDefaults.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDefaultsToolStripMenuItem,
            this.preferencesToolStripMenuItem});
            this.ChangeDefaults.Name = "ChangeDefaults";
            this.ChangeDefaults.Size = new System.Drawing.Size(61, 19);
            this.ChangeDefaults.Text = "Settings";
            // 
            // changeDefaultsToolStripMenuItem
            // 
            this.changeDefaultsToolStripMenuItem.Name = "changeDefaultsToolStripMenuItem";
            this.changeDefaultsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.changeDefaultsToolStripMenuItem.Text = "ChangeDefaults";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // Workshop
            // 
            this.Workshop.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LiveDataEntry,
            this.Prioritization,
            this.SurveryMaker});
            this.Workshop.Name = "Workshop";
            this.Workshop.Size = new System.Drawing.Size(73, 19);
            this.Workshop.Text = "Workshop";
            // 
            // LiveDataEntry
            // 
            this.LiveDataEntry.Name = "LiveDataEntry";
            this.LiveDataEntry.Size = new System.Drawing.Size(152, 22);
            this.LiveDataEntry.Text = "Live Data Entry";
            this.LiveDataEntry.Click += new System.EventHandler(this.LiveDataEntry_Click);
            // 
            // Prioritization
            // 
            this.Prioritization.Name = "Prioritization";
            this.Prioritization.Size = new System.Drawing.Size(152, 22);
            this.Prioritization.Text = "Prioritization";
            this.Prioritization.Click += new System.EventHandler(this.Prioritization_Click);
            // 
            // SurveryMaker
            // 
            this.SurveryMaker.Name = "SurveryMaker";
            this.SurveryMaker.Size = new System.Drawing.Size(152, 22);
            this.SurveryMaker.Text = "Survery Maker";
            this.SurveryMaker.Click += new System.EventHandler(this.SurveryMaker_Click);
            // 
            // capabilityNameTextBox
            // 
            this.capabilityNameTextBox.Location = new System.Drawing.Point(1425, 246);
            this.capabilityNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.capabilityNameTextBox.Name = "capabilityNameTextBox";
            this.capabilityNameTextBox.Size = new System.Drawing.Size(148, 26);
            this.capabilityNameTextBox.TabIndex = 9;
            this.capabilityNameTextBox.Visible = false;
            // 
            // questionNameTextBox
            // 
            this.questionNameTextBox.Location = new System.Drawing.Point(1425, 346);
            this.questionNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.questionNameTextBox.Name = "questionNameTextBox";
            this.questionNameTextBox.Size = new System.Drawing.Size(148, 26);
            this.questionNameTextBox.TabIndex = 11;
            this.questionNameTextBox.Visible = false;
            // 
            // removeEntityButton
            // 
            this.removeEntityButton.Location = new System.Drawing.Point(1262, 311);
            this.removeEntityButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeEntityButton.Name = "removeEntityButton";
            this.removeEntityButton.Size = new System.Drawing.Size(112, 35);
            this.removeEntityButton.TabIndex = 13;
            this.removeEntityButton.Text = "Remove";
            this.removeEntityButton.UseVisualStyleBackColor = true;
            this.removeEntityButton.Visible = false;
            // 
            // domainList
            // 
            this.domainList.FormattingEnabled = true;
            this.domainList.Location = new System.Drawing.Point(1089, 91);
            this.domainList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.domainList.Name = "domainList";
            this.domainList.Size = new System.Drawing.Size(253, 28);
            this.domainList.TabIndex = 14;
            this.domainList.Text = "<Select Domain>";
            this.domainList.Visible = false;
            this.domainList.SelectedIndexChanged += new System.EventHandler(this.domainList_SelectedIndexChanged);
            this.domainList.LostFocus += new System.EventHandler(this.domainList_LostFocus);
            // 
            // capabilitiesList
            // 
            this.capabilitiesList.FormattingEnabled = true;
            this.capabilitiesList.Location = new System.Drawing.Point(1089, 157);
            this.capabilitiesList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.capabilitiesList.Name = "capabilitiesList";
            this.capabilitiesList.Size = new System.Drawing.Size(253, 28);
            this.capabilitiesList.TabIndex = 15;
            this.capabilitiesList.Visible = false;
            this.capabilitiesList.SelectedIndexChanged += new System.EventHandler(this.capabilitiesList_SelectedIndexChanged);
            this.capabilitiesList.LostFocus += new System.EventHandler(this.capabilitiesList_LostFocus);
            // 
            // questionList
            // 
            this.questionList.FormattingEnabled = true;
            this.questionList.Location = new System.Drawing.Point(1089, 220);
            this.questionList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.questionList.Name = "questionList";
            this.questionList.Size = new System.Drawing.Size(253, 28);
            this.questionList.TabIndex = 16;
            this.questionList.Visible = false;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // liveDataEntryGrid
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.liveDataEntryGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.liveDataEntryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.liveDataEntryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Comments,
            this.AddComment,
            this.dataGridViewTextBoxColumn5});
            this.liveDataEntryGrid.EnableHeadersVisualStyles = false;
            this.liveDataEntryGrid.Location = new System.Drawing.Point(13, 51);
            this.liveDataEntryGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.liveDataEntryGrid.MultiSelect = false;
            this.liveDataEntryGrid.Name = "liveDataEntryGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.liveDataEntryGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.liveDataEntryGrid.RowHeadersVisible = false;
            this.liveDataEntryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.liveDataEntryGrid.Size = new System.Drawing.Size(1031, 567);
            this.liveDataEntryGrid.TabIndex = 17;
            this.liveDataEntryGrid.Visible = false;
            this.liveDataEntryGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.liveDataEntryGrid_CellContentClick);
            this.liveDataEntryGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.liveDataEntryGrid_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Capability";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "As Is";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "To Be";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Comments
            // 
            this.Comments.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Comments.DividerWidth = 1;
            this.Comments.HeaderText = "Comments";
            this.Comments.Name = "Comments";
            this.Comments.Width = 250;
            // 
            // AddComment
            // 
            this.AddComment.HeaderText = "";
            this.AddComment.Name = "AddComment";
            this.AddComment.Text = "+";
            this.AddComment.UseColumnTextForButtonValue = true;
            this.AddComment.Width = 30;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Type";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // prioritizationGrid
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.prioritizationGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.prioritizationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prioritizationGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.CapabilityGap,
            this.PriorityGap,
            this.dataGridViewTextBoxColumn10});
            this.prioritizationGrid.EnableHeadersVisualStyles = false;
            this.prioritizationGrid.Location = new System.Drawing.Point(0, 122);
            this.prioritizationGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prioritizationGrid.MultiSelect = false;
            this.prioritizationGrid.Name = "prioritizationGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.prioritizationGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.prioritizationGrid.RowHeadersVisible = false;
            this.prioritizationGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.prioritizationGrid.Size = new System.Drawing.Size(1031, 567);
            this.prioritizationGrid.TabIndex = 18;
            this.prioritizationGrid.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Capability";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 300;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "As Is";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "To Be";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CapabilityGap
            // 
            this.CapabilityGap.HeaderText = "Capability Gap";
            this.CapabilityGap.Name = "CapabilityGap";
            this.CapabilityGap.ReadOnly = true;
            // 
            // PriorityGap
            // 
            this.PriorityGap.HeaderText = "Priority to Fill Gap";
            this.PriorityGap.Name = "PriorityGap";
            this.PriorityGap.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Type";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(1262, 376);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(112, 35);
            this.AddButton.TabIndex = 19;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Visible = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ITCapTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.prioritizationGrid);
            this.Controls.Add(this.liveDataEntryGrid);
            this.Controls.Add(this.questionList);
            this.Controls.Add(this.capabilitiesList);
            this.Controls.Add(this.domainList);
            this.Controls.Add(this.removeEntityButton);
            this.Controls.Add(this.questionNameTextBox);
            this.Controls.Add(this.capabilityNameTextBox);
            this.Controls.Add(this.domainNameTextBox);
            this.Controls.Add(this.addQuestionButton);
            this.Controls.Add(this.addCapabilityButton);
            this.Controls.Add(this.surveryMakerGrid);
            this.Controls.Add(this.addDomainButton);
            this.Controls.Add(this.mainMenuToolBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mainMenuToolBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ITCapTool";
            this.Text = "ITCapTool";
            this.Load += new System.EventHandler(this.ITCapTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.surveryMakerGrid)).EndInit();
            this.mainMenuToolBar.ResumeLayout(false);
            this.mainMenuToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liveDataEntryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prioritizationGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addDomainButton;
        private System.Windows.Forms.DataGridView surveryMakerGrid;
        private System.Windows.Forms.Button addCapabilityButton;
        private System.Windows.Forms.Button addQuestionButton;
        private System.Windows.Forms.TextBox domainNameTextBox;
        private System.Windows.Forms.MenuStrip mainMenuToolBar;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem View;
        private System.Windows.Forms.ToolStripMenuItem Insert;
        private System.Windows.Forms.ToolStripMenuItem domainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questionToolStripMenuItem;
        private System.Windows.Forms.TextBox capabilityNameTextBox;
        private System.Windows.Forms.TextBox questionNameTextBox;
        private System.Windows.Forms.ToolStripMenuItem ChangeDefaults;
        private System.Windows.Forms.Button removeEntityButton;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        public System.Windows.Forms.ComboBox domainList;
        public System.Windows.Forms.ComboBox capabilitiesList;
        public System.Windows.Forms.ComboBox questionList;
        private System.Windows.Forms.ToolStripMenuItem systemsAgendaCapabilityAssesmentResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capabilityAssesmentSummaryScoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capabilityGapHeatmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prioritizedCapabilityGapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Workshop;
        private System.Windows.Forms.ToolStripMenuItem LiveDataEntry;
        private System.Windows.Forms.ToolStripMenuItem Prioritization;
        private System.Windows.Forms.ToolStripMenuItem SurveryMaker;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.ToolStripMenuItem changeDefaultsToolStripMenuItem;
        private System.Windows.Forms.DataGridView liveDataEntryGrid;
        private System.Windows.Forms.DataGridView prioritizationGrid;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Capability;
        private System.Windows.Forms.DataGridViewTextBoxColumn AsIs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToBe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapabilityGap;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriorityGap;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Comments;
        private System.Windows.Forms.DataGridViewButtonColumn AddComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button AddButton;

       
    }
}