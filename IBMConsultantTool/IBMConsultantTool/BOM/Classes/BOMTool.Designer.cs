﻿namespace IBMConsultantTool
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
            this.initiativeNames = new System.Windows.Forms.ComboBox();
            this.AddInitiativeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.participantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.staticMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ratingThresholdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectivenessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.criticalityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.differentiationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOMScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cUPEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iTCapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.detailInfoPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.detailInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // catWorkspace
            // 
            this.catWorkspace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catWorkspace.Location = new System.Drawing.Point(11, 34);
            this.catWorkspace.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.catWorkspace.Name = "catWorkspace";
            this.catWorkspace.SelectedIndex = 0;
            this.catWorkspace.Size = new System.Drawing.Size(814, 515);
            this.catWorkspace.TabIndex = 0;
            this.catWorkspace.SelectedIndexChanged += new System.EventHandler(this.catWorkspace_SelectedIndexChanged);
            this.catWorkspace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.catWorkspace_MouseClick);
            // 
            // categoryNames
            // 
            this.categoryNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(829, 49);
            this.categoryNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(182, 28);
            this.categoryNames.TabIndex = 11;
            this.categoryNames.Text = "<Organizational Unit>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectiveNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(829, 83);
            this.objectiveNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(182, 28);
            this.objectiveNames.TabIndex = 12;
            this.objectiveNames.Text = "<Objective>";
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // initiativeNames
            // 
            this.initiativeNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.initiativeNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Location = new System.Drawing.Point(829, 117);
            this.initiativeNames.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(182, 28);
            this.initiativeNames.TabIndex = 13;
            this.initiativeNames.Text = "<Imperative>";
            // 
            // AddInitiativeButton
            // 
            this.AddInitiativeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddInitiativeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddInitiativeButton.Location = new System.Drawing.Point(859, 155);
            this.AddInitiativeButton.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.AddInitiativeButton.Name = "AddInitiativeButton";
            this.AddInitiativeButton.Size = new System.Drawing.Size(128, 35);
            this.AddInitiativeButton.TabIndex = 14;
            this.AddInitiativeButton.Text = "Add Imperative";
            this.AddInitiativeButton.UseVisualStyleBackColor = true;
            this.AddInitiativeButton.Click += new System.EventHandler(this.AddInitiativeButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(854, 362);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 15;
            this.button1.Text = "Create Survey";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BomSurveyButton_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(854, 469);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 38);
            this.button3.TabIndex = 17;
            this.button3.Text = "Open Surveys";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.OpenSurvey_Clicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.vieToolStripMenuItem,
            this.workshopToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 29);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendEmailToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            this.sendEmailToolStripMenuItem.Click += new System.EventHandler(this.SendEmailButton_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.participantsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ratingThresholdsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // participantsToolStripMenuItem
            // 
            this.participantsToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.participantsToolStripMenuItem.Name = "participantsToolStripMenuItem";
            this.participantsToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.participantsToolStripMenuItem.Text = "Participants";
            this.participantsToolStripMenuItem.Click += new System.EventHandler(this.participantsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staticMenuItem,
            this.dynamicMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 26);
            this.toolStripMenuItem1.Text = "Rating Type";
            // 
            // staticMenuItem
            // 
            this.staticMenuItem.Checked = true;
            this.staticMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.staticMenuItem.Name = "staticMenuItem";
            this.staticMenuItem.Size = new System.Drawing.Size(141, 26);
            this.staticMenuItem.Text = "Static";
            // 
            // dynamicMenuItem
            // 
            this.dynamicMenuItem.Name = "dynamicMenuItem";
            this.dynamicMenuItem.Size = new System.Drawing.Size(141, 26);
            this.dynamicMenuItem.Text = "Dynamic";
            // 
            // ratingThresholdsToolStripMenuItem
            // 
            this.ratingThresholdsToolStripMenuItem.Name = "ratingThresholdsToolStripMenuItem";
            this.ratingThresholdsToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.ratingThresholdsToolStripMenuItem.Text = "Rating Thresholds";
            this.ratingThresholdsToolStripMenuItem.Click += new System.EventHandler(this.ratingThresholdsToolStripMenuItem_Click);
            // 
            // vieToolStripMenuItem
            // 
            this.vieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effectivenessToolStripMenuItem,
            this.criticalityToolStripMenuItem,
            this.differentiationToolStripMenuItem,
            this.bOMScoreToolStripMenuItem});
            this.vieToolStripMenuItem.Name = "vieToolStripMenuItem";
            this.vieToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.vieToolStripMenuItem.Text = "View";
            // 
            // effectivenessToolStripMenuItem
            // 
            this.effectivenessToolStripMenuItem.Name = "effectivenessToolStripMenuItem";
            this.effectivenessToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.effectivenessToolStripMenuItem.Text = "Effectiveness";
            this.effectivenessToolStripMenuItem.Click += new System.EventHandler(this.effectivenessToolStripMenuItem_Click);
            // 
            // criticalityToolStripMenuItem
            // 
            this.criticalityToolStripMenuItem.Name = "criticalityToolStripMenuItem";
            this.criticalityToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.criticalityToolStripMenuItem.Text = "Criticality";
            this.criticalityToolStripMenuItem.Click += new System.EventHandler(this.criticalityToolStripMenuItem_Click);
            // 
            // differentiationToolStripMenuItem
            // 
            this.differentiationToolStripMenuItem.Name = "differentiationToolStripMenuItem";
            this.differentiationToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.differentiationToolStripMenuItem.Text = "Differentiation";
            this.differentiationToolStripMenuItem.Click += new System.EventHandler(this.differentiationToolStripMenuItem_Click);
            // 
            // bOMScoreToolStripMenuItem
            // 
            this.bOMScoreToolStripMenuItem.Name = "bOMScoreToolStripMenuItem";
            this.bOMScoreToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.bOMScoreToolStripMenuItem.Text = "BOM Score";
            this.bOMScoreToolStripMenuItem.Click += new System.EventHandler(this.bOMScoreToolStripMenuItem_Click);
            // 
            // workshopToolStripMenuItem
            // 
            this.workshopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cUPEToolStripMenuItem,
            this.bOMToolStripMenuItem,
            this.iTCapToolStripMenuItem});
            this.workshopToolStripMenuItem.Name = "workshopToolStripMenuItem";
            this.workshopToolStripMenuItem.Size = new System.Drawing.Size(59, 25);
            this.workshopToolStripMenuItem.Text = "Tools";
            // 
            // cUPEToolStripMenuItem
            // 
            this.cUPEToolStripMenuItem.Name = "cUPEToolStripMenuItem";
            this.cUPEToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.cUPEToolStripMenuItem.Text = "CUPE";
            this.cUPEToolStripMenuItem.Click += new System.EventHandler(this.cUPEToolStripMenuItem_Click);
            // 
            // bOMToolStripMenuItem
            // 
            this.bOMToolStripMenuItem.Name = "bOMToolStripMenuItem";
            this.bOMToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.bOMToolStripMenuItem.Text = "BOM";
            // 
            // iTCapToolStripMenuItem
            // 
            this.iTCapToolStripMenuItem.Name = "iTCapToolStripMenuItem";
            this.iTCapToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.iTCapToolStripMenuItem.Text = "ITCap";
            this.iTCapToolStripMenuItem.Click += new System.EventHandler(this.iTCapToolStripMenuItem_Click);
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadChart.Location = new System.Drawing.Point(854, 415);
            this.btnLoadChart.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(120, 38);
            this.btnLoadChart.TabIndex = 19;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // detailInfoPanel
            // 
            this.detailInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailInfoPanel.AutoScroll = true;
            this.detailInfoPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.detailInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detailInfoPanel.Controls.Add(this.label5);
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
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(791, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 5;
            this.label5.Tag = "permanent";
            this.label5.Text = "BOM Score";
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
            this.seperatorLabel.Location = new System.Drawing.Point(0, 0);
            this.seperatorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(1004, 20);
            this.seperatorLabel.TabIndex = 0;
            this.seperatorLabel.Tag = "permanent";
            this.seperatorLabel.Text = "View Data";
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.detailInfoPanel);
            this.Controls.Add(this.btnLoadChart);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AddInitiativeButton);
            this.Controls.Add(this.initiativeNames);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.categoryNames);
            this.Controls.Add(this.catWorkspace);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.Name = "BOMTool";
            this.Text = "BOMTool";
            this.Load += new System.EventHandler(this.BOMTool_Load);
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
        public System.Windows.Forms.ComboBox initiativeNames;
        private System.Windows.Forms.Button AddInitiativeButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem participantsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.Button btnLoadChart;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem bOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iTCapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bOMScoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem staticMenuItem;
        public System.Windows.Forms.ToolStripMenuItem dynamicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ratingThresholdsToolStripMenuItem;

    }
}