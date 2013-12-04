namespace IBMConsultantTool
{
    partial class BOMTool
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
            this.catWorkspace = new System.Windows.Forms.TabControl();
            this.categoryNames = new System.Windows.Forms.ComboBox();
            this.objectiveNames = new System.Windows.Forms.ComboBox();
            this.imperativeNames = new System.Windows.Forms.ComboBox();
            this.AddImperativeButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSurveyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openSurveysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectivenessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.criticalityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.differentiationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOMScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.workshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cUPEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iTCapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailInfoPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.sortedImperativesListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sortedComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.detailInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // catWorkspace
            // 
            this.catWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.catWorkspace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catWorkspace.ItemSize = new System.Drawing.Size(0, 35);
            this.catWorkspace.Location = new System.Drawing.Point(11, 34);
            this.catWorkspace.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.catWorkspace.Name = "catWorkspace";
            this.catWorkspace.SelectedIndex = 0;
            this.catWorkspace.Size = new System.Drawing.Size(779, 459);
            this.catWorkspace.TabIndex = 0;
            this.catWorkspace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.catWorkspace_MouseClick);
            // 
            // categoryNames
            // 
            this.categoryNames.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.categoryNames.DropDownWidth = 250;
            this.categoryNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(20, 512);
            this.categoryNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(261, 28);
            this.categoryNames.TabIndex = 11;
            this.categoryNames.Text = "<Organizational Unit>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.objectiveNames.DropDownWidth = 250;
            this.objectiveNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(299, 512);
            this.objectiveNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(261, 28);
            this.objectiveNames.TabIndex = 12;
            this.objectiveNames.Text = "<Business Objective>";
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // imperativeNames
            // 
            this.imperativeNames.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imperativeNames.DropDownWidth = 250;
            this.imperativeNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imperativeNames.FormattingEnabled = true;
            this.imperativeNames.Location = new System.Drawing.Point(577, 512);
            this.imperativeNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.imperativeNames.Name = "imperativeNames";
            this.imperativeNames.Size = new System.Drawing.Size(261, 28);
            this.imperativeNames.TabIndex = 13;
            this.imperativeNames.Text = "<Business Imperative>";
            // 
            // AddImperativeButton
            // 
            this.AddImperativeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddImperativeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddImperativeButton.Location = new System.Drawing.Point(869, 512);
            this.AddImperativeButton.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.AddImperativeButton.Name = "AddImperativeButton";
            this.AddImperativeButton.Size = new System.Drawing.Size(128, 35);
            this.AddImperativeButton.TabIndex = 14;
            this.AddImperativeButton.Text = "Add Imperative";
            this.AddImperativeButton.UseVisualStyleBackColor = true;
            this.AddImperativeButton.Click += new System.EventHandler(this.AddImperativeButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.vieToolStripMenuItem,
            this.workshopToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.createSurveyToolStripMenuItem1,
            this.openSurveysToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // createSurveyToolStripMenuItem1
            // 
            this.createSurveyToolStripMenuItem1.Name = "createSurveyToolStripMenuItem1";
            this.createSurveyToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.createSurveyToolStripMenuItem1.Text = "Create Survey";
            this.createSurveyToolStripMenuItem1.Click += new System.EventHandler(this.BomSurveyButton_Click);
            // 
            // openSurveysToolStripMenuItem1
            // 
            this.openSurveysToolStripMenuItem1.Name = "openSurveysToolStripMenuItem1";
            this.openSurveysToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.openSurveysToolStripMenuItem1.Text = "Open Surveys";
            this.openSurveysToolStripMenuItem1.Click += new System.EventHandler(this.OpenSurvey_Clicked);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // vieToolStripMenuItem
            // 
            this.vieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effectivenessToolStripMenuItem,
            this.criticalityToolStripMenuItem,
            this.differentiationToolStripMenuItem,
            this.bOMScoreToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.vieToolStripMenuItem.Name = "vieToolStripMenuItem";
            this.vieToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.vieToolStripMenuItem.Text = "View";
            // 
            // effectivenessToolStripMenuItem
            // 
            this.effectivenessToolStripMenuItem.Name = "effectivenessToolStripMenuItem";
            this.effectivenessToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.effectivenessToolStripMenuItem.Text = "Effectiveness";
            this.effectivenessToolStripMenuItem.Click += new System.EventHandler(this.effectivenessToolStripMenuItem_Click);
            // 
            // criticalityToolStripMenuItem
            // 
            this.criticalityToolStripMenuItem.Name = "criticalityToolStripMenuItem";
            this.criticalityToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.criticalityToolStripMenuItem.Text = "Criticality";
            this.criticalityToolStripMenuItem.Click += new System.EventHandler(this.criticalityToolStripMenuItem_Click);
            // 
            // differentiationToolStripMenuItem
            // 
            this.differentiationToolStripMenuItem.Name = "differentiationToolStripMenuItem";
            this.differentiationToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.differentiationToolStripMenuItem.Text = "Differentiation";
            this.differentiationToolStripMenuItem.Click += new System.EventHandler(this.differentiationToolStripMenuItem_Click);
            // 
            // bOMScoreToolStripMenuItem
            // 
            this.bOMScoreToolStripMenuItem.Name = "bOMScoreToolStripMenuItem";
            this.bOMScoreToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.bOMScoreToolStripMenuItem.Text = "BOM Score";
            this.bOMScoreToolStripMenuItem.Click += new System.EventHandler(this.bOMScoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem2.Text = "Bubble Chart";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // workshopToolStripMenuItem
            // 
            this.workshopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cUPEToolStripMenuItem,
            this.iTCapToolStripMenuItem});
            this.workshopToolStripMenuItem.Name = "workshopToolStripMenuItem";
            this.workshopToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.workshopToolStripMenuItem.Text = "Tools";
            // 
            // cUPEToolStripMenuItem
            // 
            this.cUPEToolStripMenuItem.Name = "cUPEToolStripMenuItem";
            this.cUPEToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cUPEToolStripMenuItem.Text = "CUPE";
            this.cUPEToolStripMenuItem.Click += new System.EventHandler(this.cUPEToolStripMenuItem_Click);
            // 
            // iTCapToolStripMenuItem
            // 
            this.iTCapToolStripMenuItem.Name = "iTCapToolStripMenuItem";
            this.iTCapToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.iTCapToolStripMenuItem.Text = "ITCAP";
            this.iTCapToolStripMenuItem.Click += new System.EventHandler(this.iTCapToolStripMenuItem_Click);
            // 
            // detailInfoPanel
            // 
            this.detailInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailInfoPanel.AutoScroll = true;
            this.detailInfoPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.detailInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detailInfoPanel.Controls.Add(this.label4);
            this.detailInfoPanel.Controls.Add(this.label3);
            this.detailInfoPanel.Controls.Add(this.label2);
            this.detailInfoPanel.Controls.Add(this.label1);
            this.detailInfoPanel.Controls.Add(this.seperatorLabel);
            this.detailInfoPanel.Location = new System.Drawing.Point(0, 556);
            this.detailInfoPanel.Margin = new System.Windows.Forms.Padding(2);
            this.detailInfoPanel.Name = "detailInfoPanel";
            this.detailInfoPanel.Size = new System.Drawing.Size(1008, 173);
            this.detailInfoPanel.TabIndex = 20;
            this.detailInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailInfoPanel_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(624, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 4;
            this.label4.Tag = "permanent";
            this.label4.Text = "Differentiation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(509, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 3;
            this.label3.Tag = "permanent";
            this.label3.Text = "Criticality";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(351, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 2;
            this.label2.Tag = "permanent";
            this.label2.Text = "Effectiveness";
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 1;
            this.label1.Tag = "permanent";
            this.label1.Text = "Imperative Name";
            // 
            // seperatorLabel
            // 
            this.seperatorLabel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.seperatorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.seperatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seperatorLabel.Location = new System.Drawing.Point(0, 0);
            this.seperatorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(1004, 21);
            this.seperatorLabel.TabIndex = 0;
            this.seperatorLabel.Tag = "permanent";
            this.seperatorLabel.Text = "View Data";
            // 
            // sortedImperativesListBox
            // 
            this.sortedImperativesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortedImperativesListBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.sortedImperativesListBox.FormattingEnabled = true;
            this.sortedImperativesListBox.ItemHeight = 20;
            this.sortedImperativesListBox.Location = new System.Drawing.Point(795, 109);
            this.sortedImperativesListBox.Name = "sortedImperativesListBox";
            this.sortedImperativesListBox.Size = new System.Drawing.Size(201, 384);
            this.sortedImperativesListBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(831, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Sort Imperatives";
            // 
            // sortedComboBox
            // 
            this.sortedComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortedComboBox.FormattingEnabled = true;
            this.sortedComboBox.Items.AddRange(new object[] {
            "Criticality",
            "Differentiation",
            "Effectiveness",
            "Weighted ECD"});
            this.sortedComboBox.Location = new System.Drawing.Point(795, 57);
            this.sortedComboBox.Name = "sortedComboBox";
            this.sortedComboBox.Size = new System.Drawing.Size(200, 28);
            this.sortedComboBox.TabIndex = 23;
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.sortedComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sortedImperativesListBox);
            this.Controls.Add(this.AddImperativeButton);
            this.Controls.Add(this.detailInfoPanel);
            this.Controls.Add(this.imperativeNames);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.catWorkspace);
            this.Controls.Add(this.categoryNames);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.Name = "BOMTool";
            this.Text = "Business Objective Mapping Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.detailInfoPanel.ResumeLayout(false);
            this.detailInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl catWorkspace;
        public System.Windows.Forms.ComboBox categoryNames;
        public System.Windows.Forms.ComboBox objectiveNames;
        public System.Windows.Forms.ComboBox imperativeNames;
        private System.Windows.Forms.Button AddImperativeButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Panel detailInfoPanel;
        private System.Windows.Forms.Label seperatorLabel;
        private System.Windows.Forms.ToolStripMenuItem workshopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cUPEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectivenessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem criticalityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem differentiationToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem iTCapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bOMScoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createSurveyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openSurveysToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ListBox sortedImperativesListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sortedComboBox;

    }
}