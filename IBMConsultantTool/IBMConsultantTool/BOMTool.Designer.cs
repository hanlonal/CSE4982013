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
            this.dataInputButton = new System.Windows.Forms.Button();
            this.critRadio = new System.Windows.Forms.RadioButton();
            this.effectRadio = new System.Windows.Forms.RadioButton();
            this.diffRadio = new System.Windows.Forms.RadioButton();
            this.categoryNames = new System.Windows.Forms.ComboBox();
            this.objectiveNames = new System.Windows.Forms.ComboBox();
            this.initiativeNames = new System.Windows.Forms.ComboBox();
            this.AddInitiativeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.participantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.detailInfoPanel = new System.Windows.Forms.Panel();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.detailInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // catWorkspace
            // 
            this.catWorkspace.Location = new System.Drawing.Point(18, 48);
            this.catWorkspace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.catWorkspace.Name = "catWorkspace";
            this.catWorkspace.SelectedIndex = 0;
            this.catWorkspace.Size = new System.Drawing.Size(831, 516);
            this.catWorkspace.TabIndex = 0;
            // 
            // dataInputButton
            // 
            this.dataInputButton.Location = new System.Drawing.Point(991, 529);
            this.dataInputButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataInputButton.Name = "dataInputButton";
            this.dataInputButton.Size = new System.Drawing.Size(112, 35);
            this.dataInputButton.TabIndex = 7;
            this.dataInputButton.Text = "Input Data";
            this.dataInputButton.UseVisualStyleBackColor = true;
            this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
            // 
            // critRadio
            // 
            this.critRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.critRadio.AutoSize = true;
            this.critRadio.Location = new System.Drawing.Point(856, 82);
            this.critRadio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.critRadio.Name = "critRadio";
            this.critRadio.Size = new System.Drawing.Size(127, 24);
            this.critRadio.TabIndex = 8;
            this.critRadio.TabStop = true;
            this.critRadio.Text = "View Criticality";
            this.critRadio.UseVisualStyleBackColor = true;
            this.critRadio.Click += new System.EventHandler(this.critRadio_Click);
            // 
            // effectRadio
            // 
            this.effectRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effectRadio.AutoSize = true;
            this.effectRadio.Location = new System.Drawing.Point(856, 48);
            this.effectRadio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.effectRadio.Name = "effectRadio";
            this.effectRadio.Size = new System.Drawing.Size(161, 24);
            this.effectRadio.TabIndex = 9;
            this.effectRadio.TabStop = true;
            this.effectRadio.Text = "View Effectiveness";
            this.effectRadio.UseVisualStyleBackColor = true;
            this.effectRadio.Click += new System.EventHandler(this.effectRadio_Click);
            // 
            // diffRadio
            // 
            this.diffRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.diffRadio.AutoSize = true;
            this.diffRadio.Location = new System.Drawing.Point(856, 116);
            this.diffRadio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.diffRadio.Name = "diffRadio";
            this.diffRadio.Size = new System.Drawing.Size(165, 24);
            this.diffRadio.TabIndex = 10;
            this.diffRadio.TabStop = true;
            this.diffRadio.Text = "View Differentiation";
            this.diffRadio.UseVisualStyleBackColor = true;
            this.diffRadio.Click += new System.EventHandler(this.diffRadio_Click);
            // 
            // categoryNames
            // 
            this.categoryNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(857, 165);
            this.categoryNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(180, 28);
            this.categoryNames.TabIndex = 11;
            this.categoryNames.Text = "<Select Category>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(856, 220);
            this.objectiveNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(180, 28);
            this.objectiveNames.TabIndex = 12;
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // initiativeNames
            // 
            this.initiativeNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Location = new System.Drawing.Point(856, 275);
            this.initiativeNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(180, 28);
            this.initiativeNames.TabIndex = 13;
            // 
            // AddInitiativeButton
            // 
            this.AddInitiativeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddInitiativeButton.Location = new System.Drawing.Point(857, 339);
            this.AddInitiativeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddInitiativeButton.Name = "AddInitiativeButton";
            this.AddInitiativeButton.Size = new System.Drawing.Size(112, 35);
            this.AddInitiativeButton.TabIndex = 14;
            this.AddInitiativeButton.Text = "Add Initiative";
            this.AddInitiativeButton.UseVisualStyleBackColor = true;
            this.AddInitiativeButton.Click += new System.EventHandler(this.AddInitiativeButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(991, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 15;
            this.button1.Text = "Create Survey";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BomSurveyButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(856, 527);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 37);
            this.button3.TabIndex = 17;
            this.button3.Text = "Open Surveys";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OpenSurvey_Clicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.vieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1116, 25);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendEmailToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            this.sendEmailToolStripMenuItem.Click += new System.EventHandler(this.SendEmailButton_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.participantsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // participantsToolStripMenuItem
            // 
            this.participantsToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.participantsToolStripMenuItem.Name = "participantsToolStripMenuItem";
            this.participantsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.participantsToolStripMenuItem.Text = "Participants";
            this.participantsToolStripMenuItem.Click += new System.EventHandler(this.participantsToolStripMenuItem_Click);
            // 
            // vieToolStripMenuItem
            // 
            this.vieToolStripMenuItem.Name = "vieToolStripMenuItem";
            this.vieToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.vieToolStripMenuItem.Text = "View";
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Location = new System.Drawing.Point(856, 463);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(120, 37);
            this.btnLoadChart.TabIndex = 19;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // detailInfoPanel
            // 
            this.detailInfoPanel.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.detailInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detailInfoPanel.Controls.Add(this.seperatorLabel);
            this.detailInfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.detailInfoPanel.Location = new System.Drawing.Point(0, 572);
            this.detailInfoPanel.Name = "detailInfoPanel";
            this.detailInfoPanel.Size = new System.Drawing.Size(1116, 177);
            this.detailInfoPanel.TabIndex = 20;
            // 
            // seperatorLabel
            // 
            this.seperatorLabel.AutoSize = true;
            this.seperatorLabel.BackColor = System.Drawing.Color.Silver;
            this.seperatorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.seperatorLabel.Location = new System.Drawing.Point(0, 0);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(58, 20);
            this.seperatorLabel.TabIndex = 0;
            this.seperatorLabel.Text = "Details";
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 749);
            this.Controls.Add(this.detailInfoPanel);
            this.Controls.Add(this.btnLoadChart);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AddInitiativeButton);
            this.Controls.Add(this.initiativeNames);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.categoryNames);
            this.Controls.Add(this.diffRadio);
            this.Controls.Add(this.effectRadio);
            this.Controls.Add(this.critRadio);
            this.Controls.Add(this.dataInputButton);
            this.Controls.Add(this.catWorkspace);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.Button dataInputButton;
        private System.Windows.Forms.RadioButton critRadio;
        private System.Windows.Forms.RadioButton effectRadio;
        private System.Windows.Forms.RadioButton diffRadio;
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

    }
}