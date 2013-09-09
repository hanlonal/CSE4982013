namespace IBMConsultantTool
{
    partial class MainForm
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.BOM = new System.Windows.Forms.TabPage();
            this.SendBOMButton = new System.Windows.Forms.Button();
            this.BOMAddInitiativeButton = new System.Windows.Forms.Button();
            this.BOMBubbleChartButton = new System.Windows.Forms.Button();
            this.BOMTable = new System.Windows.Forms.DataGridView();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusinessObjective = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imperative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Effectiveness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Criticality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Differential = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveBOMButton = new System.Windows.Forms.Button();
            this.OpenBOMButton = new System.Windows.Forms.Button();
            this.NewBOMButton = new System.Windows.Forms.Button();
            this.CUPE = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.CUPETable = new System.Windows.Forms.DataGridView();
            this.OpenAnalytics = new System.Windows.Forms.Button();
            this.NewCUPEButton = new System.Windows.Forms.Button();
            this.ITCAP = new System.Windows.Forms.TabPage();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Objective = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Participant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnswersSubmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.BOM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BOMTable)).BeginInit();
            this.CUPE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUPETable)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.BOM);
            this.TabControl.Controls.Add(this.CUPE);
            this.TabControl.Controls.Add(this.ITCAP);
            this.TabControl.Location = new System.Drawing.Point(16, 31);
            this.TabControl.Margin = new System.Windows.Forms.Padding(4);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1020, 482);
            this.TabControl.TabIndex = 0;
            // 
            // BOM
            // 
            this.BOM.Controls.Add(this.SendBOMButton);
            this.BOM.Controls.Add(this.BOMAddInitiativeButton);
            this.BOM.Controls.Add(this.BOMBubbleChartButton);
            this.BOM.Controls.Add(this.BOMTable);
            this.BOM.Controls.Add(this.SaveBOMButton);
            this.BOM.Controls.Add(this.OpenBOMButton);
            this.BOM.Controls.Add(this.NewBOMButton);
            this.BOM.Location = new System.Drawing.Point(4, 25);
            this.BOM.Margin = new System.Windows.Forms.Padding(4);
            this.BOM.Name = "BOM";
            this.BOM.Size = new System.Drawing.Size(1012, 453);
            this.BOM.TabIndex = 0;
            this.BOM.Text = "BOM";
            this.BOM.UseVisualStyleBackColor = true;
            // 
            // SendBOMButton
            // 
            this.SendBOMButton.Location = new System.Drawing.Point(201, 409);
            this.SendBOMButton.Margin = new System.Windows.Forms.Padding(4);
            this.SendBOMButton.Name = "SendBOMButton";
            this.SendBOMButton.Size = new System.Drawing.Size(143, 36);
            this.SendBOMButton.TabIndex = 6;
            this.SendBOMButton.Text = "Send";
            this.SendBOMButton.UseVisualStyleBackColor = true;
            // 
            // BOMAddInitiativeButton
            // 
            this.BOMAddInitiativeButton.Location = new System.Drawing.Point(669, 27);
            this.BOMAddInitiativeButton.Margin = new System.Windows.Forms.Padding(4);
            this.BOMAddInitiativeButton.Name = "BOMAddInitiativeButton";
            this.BOMAddInitiativeButton.Size = new System.Drawing.Size(144, 36);
            this.BOMAddInitiativeButton.TabIndex = 5;
            this.BOMAddInitiativeButton.Text = "Add Initiative";
            this.BOMAddInitiativeButton.UseVisualStyleBackColor = true;
            this.BOMAddInitiativeButton.Click += new System.EventHandler(this.BOMAddInitiativeButton_Click);
            // 
            // BOMBubbleChartButton
            // 
            this.BOMBubbleChartButton.Location = new System.Drawing.Point(835, 409);
            this.BOMBubbleChartButton.Margin = new System.Windows.Forms.Padding(4);
            this.BOMBubbleChartButton.Name = "BOMBubbleChartButton";
            this.BOMBubbleChartButton.Size = new System.Drawing.Size(143, 36);
            this.BOMBubbleChartButton.TabIndex = 4;
            this.BOMBubbleChartButton.Text = "Bubble Chart";
            this.BOMBubbleChartButton.UseVisualStyleBackColor = true;
            this.BOMBubbleChartButton.Click += new System.EventHandler(this.BOMBubbleChartButton_Click);
            // 
            // BOMTable
            // 
            this.BOMTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BOMTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.BusinessObjective,
            this.Imperative,
            this.Effectiveness,
            this.Criticality,
            this.Differential});
            this.BOMTable.Location = new System.Drawing.Point(4, 81);
            this.BOMTable.Margin = new System.Windows.Forms.Padding(4);
            this.BOMTable.Name = "BOMTable";
            this.BOMTable.Size = new System.Drawing.Size(1001, 294);
            this.BOMTable.TabIndex = 3;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            // 
            // BusinessObjective
            // 
            this.BusinessObjective.HeaderText = "Business Objectives";
            this.BusinessObjective.Name = "BusinessObjective";
            // 
            // Imperative
            // 
            this.Imperative.HeaderText = "Imperative";
            this.Imperative.Name = "Imperative";
            this.Imperative.Width = 200;
            // 
            // Effectiveness
            // 
            this.Effectiveness.HeaderText = "Effectiveness";
            this.Effectiveness.Name = "Effectiveness";
            // 
            // Criticality
            // 
            this.Criticality.HeaderText = "Criticality";
            this.Criticality.Name = "Criticality";
            // 
            // Differential
            // 
            this.Differential.HeaderText = "Differential";
            this.Differential.Name = "Differential";
            // 
            // SaveBOMButton
            // 
            this.SaveBOMButton.Location = new System.Drawing.Point(36, 409);
            this.SaveBOMButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveBOMButton.Name = "SaveBOMButton";
            this.SaveBOMButton.Size = new System.Drawing.Size(143, 36);
            this.SaveBOMButton.TabIndex = 2;
            this.SaveBOMButton.Text = "Save BOM";
            this.SaveBOMButton.UseVisualStyleBackColor = true;
            // 
            // OpenBOMButton
            // 
            this.OpenBOMButton.Location = new System.Drawing.Point(220, 27);
            this.OpenBOMButton.Margin = new System.Windows.Forms.Padding(4);
            this.OpenBOMButton.Name = "OpenBOMButton";
            this.OpenBOMButton.Size = new System.Drawing.Size(143, 36);
            this.OpenBOMButton.TabIndex = 1;
            this.OpenBOMButton.Text = "Open BOM";
            this.OpenBOMButton.UseVisualStyleBackColor = true;
            // 
            // NewBOMButton
            // 
            this.NewBOMButton.Location = new System.Drawing.Point(36, 27);
            this.NewBOMButton.Margin = new System.Windows.Forms.Padding(4);
            this.NewBOMButton.Name = "NewBOMButton";
            this.NewBOMButton.Size = new System.Drawing.Size(143, 36);
            this.NewBOMButton.TabIndex = 0;
            this.NewBOMButton.Text = "New BOM";
            this.NewBOMButton.UseVisualStyleBackColor = true;
            // 
            // CUPE
            // 
            this.CUPE.Controls.Add(this.button4);
            this.CUPE.Controls.Add(this.button3);
            this.CUPE.Controls.Add(this.button2);
            this.CUPE.Controls.Add(this.dataGridView1);
            this.CUPE.Controls.Add(this.button1);
            this.CUPE.Controls.Add(this.CUPETable);
            this.CUPE.Controls.Add(this.OpenAnalytics);
            this.CUPE.Controls.Add(this.NewCUPEButton);
            this.CUPE.Location = new System.Drawing.Point(4, 25);
            this.CUPE.Margin = new System.Windows.Forms.Padding(4);
            this.CUPE.Name = "CUPE";
            this.CUPE.Size = new System.Drawing.Size(1012, 453);
            this.CUPE.TabIndex = 1;
            this.CUPE.Text = "CUPE";
            this.CUPE.UseVisualStyleBackColor = true;
            this.CUPE.Click += new System.EventHandler(this.CUPE_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Objective});
            this.dataGridView1.Location = new System.Drawing.Point(388, 74);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(335, 362);
            this.dataGridView1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 29);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "Open Question List";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CUPETable
            // 
            this.CUPETable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CUPETable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Participant,
            this.AnswersSubmitted});
            this.CUPETable.Location = new System.Drawing.Point(17, 74);
            this.CUPETable.Margin = new System.Windows.Forms.Padding(4);
            this.CUPETable.Name = "CUPETable";
            this.CUPETable.Size = new System.Drawing.Size(297, 362);
            this.CUPETable.TabIndex = 4;
            // 
            // OpenAnalytics
            // 
            this.OpenAnalytics.Location = new System.Drawing.Point(771, 150);
            this.OpenAnalytics.Margin = new System.Windows.Forms.Padding(4);
            this.OpenAnalytics.Name = "OpenAnalytics";
            this.OpenAnalytics.Size = new System.Drawing.Size(143, 36);
            this.OpenAnalytics.TabIndex = 2;
            this.OpenAnalytics.Text = "Open Analytics";
            this.OpenAnalytics.UseVisualStyleBackColor = true;
            this.OpenAnalytics.Click += new System.EventHandler(this.OpenAnalytics_Click);
            // 
            // NewCUPEButton
            // 
            this.NewCUPEButton.Location = new System.Drawing.Point(17, 30);
            this.NewCUPEButton.Margin = new System.Windows.Forms.Padding(4);
            this.NewCUPEButton.Name = "NewCUPEButton";
            this.NewCUPEButton.Size = new System.Drawing.Size(143, 36);
            this.NewCUPEButton.TabIndex = 1;
            this.NewCUPEButton.Text = "Open User List";
            this.NewCUPEButton.UseVisualStyleBackColor = true;
            // 
            // ITCAP
            // 
            this.ITCAP.Location = new System.Drawing.Point(4, 25);
            this.ITCAP.Margin = new System.Windows.Forms.Padding(4);
            this.ITCAP.Name = "ITCAP";
            this.ITCAP.Size = new System.Drawing.Size(1012, 453);
            this.ITCAP.TabIndex = 2;
            this.ITCAP.Text = "ITCAP";
            this.ITCAP.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(1052, 28);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "Menu";
            // 
            // Menu
            // 
            this.Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.New,
            this.saveToolStripMenuItem});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(44, 24);
            this.Menu.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // New
            // 
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(114, 24);
            this.New.Text = "New";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(539, 29);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 36);
            this.button2.TabIndex = 10;
            this.button2.Text = "Save Current Questions";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(171, 29);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 36);
            this.button3.TabIndex = 11;
            this.button3.Text = "Save Current Users";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Question";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // Objective
            // 
            this.Objective.HeaderText = "Objective";
            this.Objective.Name = "Objective";
            this.Objective.Width = 150;
            // 
            // Participant
            // 
            this.Participant.HeaderText = "Participant";
            this.Participant.Name = "Participant";
            this.Participant.Width = 150;
            // 
            // AnswersSubmitted
            // 
            this.AnswersSubmitted.DataPropertyName = "AnswersSubmitted";
            this.AnswersSubmitted.HeaderText = "Answers Submitted";
            this.AnswersSubmitted.Name = "AnswersSubmitted";
            this.AnswersSubmitted.Width = 110;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(771, 223);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(143, 36);
            this.button4.TabIndex = 12;
            this.button4.Text = "Generate WebPage";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 528);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "IBMConsultantTool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl.ResumeLayout(false);
            this.BOM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BOMTable)).EndInit();
            this.CUPE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUPETable)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage BOM;
        private System.Windows.Forms.TabPage CUPE;
        private System.Windows.Forms.TabPage ITCAP;
        private System.Windows.Forms.Button NewBOMButton;
        private System.Windows.Forms.Button SaveBOMButton;
        private System.Windows.Forms.Button OpenBOMButton;
        private System.Windows.Forms.DataGridView BOMTable;
        private System.Windows.Forms.Button BOMBubbleChartButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinessObjective;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imperative;
        private System.Windows.Forms.DataGridViewTextBoxColumn Effectiveness;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Differential;
        private System.Windows.Forms.DataGridView CUPETable;
        private System.Windows.Forms.Button OpenAnalytics;
        private System.Windows.Forms.Button NewCUPEButton;
        private System.Windows.Forms.Button BOMAddInitiativeButton;
        private System.Windows.Forms.Button SendBOMButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem Menu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Objective;
        private System.Windows.Forms.DataGridViewTextBoxColumn Participant;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnswersSubmitted;
        private System.Windows.Forms.Button button4;
    }
}

