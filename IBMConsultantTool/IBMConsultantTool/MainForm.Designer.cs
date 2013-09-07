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
            this.CUPENOQUpdateButton = new System.Windows.Forms.Button();
            this.NumberOfQuestionsTextBox = new System.Windows.Forms.TextBox();
            this.NumberOfQuestionsLabel = new System.Windows.Forms.Label();
            this.CUPETable = new System.Windows.Forms.DataGridView();
            this.Participant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenCUPEButton = new System.Windows.Forms.Button();
            this.NewCUPEButton = new System.Windows.Forms.Button();
            this.ITCAP = new System.Windows.Forms.TabPage();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl.SuspendLayout();
            this.BOM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BOMTable)).BeginInit();
            this.CUPE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CUPETable)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.BOM);
            this.TabControl.Controls.Add(this.CUPE);
            this.TabControl.Controls.Add(this.ITCAP);
            this.TabControl.Location = new System.Drawing.Point(12, 25);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(765, 392);
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
            this.BOM.Location = new System.Drawing.Point(4, 22);
            this.BOM.Name = "BOM";
            this.BOM.Size = new System.Drawing.Size(757, 366);
            this.BOM.TabIndex = 0;
            this.BOM.Text = "BOM";
            this.BOM.UseVisualStyleBackColor = true;
            // 
            // SendBOMButton
            // 
            this.SendBOMButton.Location = new System.Drawing.Point(151, 332);
            this.SendBOMButton.Name = "SendBOMButton";
            this.SendBOMButton.Size = new System.Drawing.Size(107, 29);
            this.SendBOMButton.TabIndex = 6;
            this.SendBOMButton.Text = "Send";
            this.SendBOMButton.UseVisualStyleBackColor = true;
            // 
            // BOMAddInitiativeButton
            // 
            this.BOMAddInitiativeButton.Location = new System.Drawing.Point(502, 22);
            this.BOMAddInitiativeButton.Name = "BOMAddInitiativeButton";
            this.BOMAddInitiativeButton.Size = new System.Drawing.Size(108, 29);
            this.BOMAddInitiativeButton.TabIndex = 5;
            this.BOMAddInitiativeButton.Text = "Add Initiative";
            this.BOMAddInitiativeButton.UseVisualStyleBackColor = true;
            this.BOMAddInitiativeButton.Click += new System.EventHandler(this.BOMAddInitiativeButton_Click);
            // 
            // BOMBubbleChartButton
            // 
            this.BOMBubbleChartButton.Location = new System.Drawing.Point(626, 332);
            this.BOMBubbleChartButton.Name = "BOMBubbleChartButton";
            this.BOMBubbleChartButton.Size = new System.Drawing.Size(107, 29);
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
            this.BOMTable.Location = new System.Drawing.Point(3, 66);
            this.BOMTable.Name = "BOMTable";
            this.BOMTable.Size = new System.Drawing.Size(751, 239);
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
            this.SaveBOMButton.Location = new System.Drawing.Point(27, 332);
            this.SaveBOMButton.Name = "SaveBOMButton";
            this.SaveBOMButton.Size = new System.Drawing.Size(107, 29);
            this.SaveBOMButton.TabIndex = 2;
            this.SaveBOMButton.Text = "Save BOM";
            this.SaveBOMButton.UseVisualStyleBackColor = true;
            // 
            // OpenBOMButton
            // 
            this.OpenBOMButton.Location = new System.Drawing.Point(165, 22);
            this.OpenBOMButton.Name = "OpenBOMButton";
            this.OpenBOMButton.Size = new System.Drawing.Size(107, 29);
            this.OpenBOMButton.TabIndex = 1;
            this.OpenBOMButton.Text = "Open BOM";
            this.OpenBOMButton.UseVisualStyleBackColor = true;
            // 
            // NewBOMButton
            // 
            this.NewBOMButton.Location = new System.Drawing.Point(27, 22);
            this.NewBOMButton.Name = "NewBOMButton";
            this.NewBOMButton.Size = new System.Drawing.Size(107, 29);
            this.NewBOMButton.TabIndex = 0;
            this.NewBOMButton.Text = "New BOM";
            this.NewBOMButton.UseVisualStyleBackColor = true;
            // 
            // CUPE
            // 
            this.CUPE.Controls.Add(this.CUPENOQUpdateButton);
            this.CUPE.Controls.Add(this.NumberOfQuestionsTextBox);
            this.CUPE.Controls.Add(this.NumberOfQuestionsLabel);
            this.CUPE.Controls.Add(this.CUPETable);
            this.CUPE.Controls.Add(this.OpenCUPEButton);
            this.CUPE.Controls.Add(this.NewCUPEButton);
            this.CUPE.Location = new System.Drawing.Point(4, 22);
            this.CUPE.Name = "CUPE";
            this.CUPE.Size = new System.Drawing.Size(757, 366);
            this.CUPE.TabIndex = 1;
            this.CUPE.Text = "CUPE";
            this.CUPE.UseVisualStyleBackColor = true;
            // 
            // CUPENOQUpdateButton
            // 
            this.CUPENOQUpdateButton.Location = new System.Drawing.Point(587, 27);
            this.CUPENOQUpdateButton.Name = "CUPENOQUpdateButton";
            this.CUPENOQUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.CUPENOQUpdateButton.TabIndex = 7;
            this.CUPENOQUpdateButton.Text = "Update";
            this.CUPENOQUpdateButton.UseVisualStyleBackColor = true;
            this.CUPENOQUpdateButton.Click += new System.EventHandler(this.CUPENOQUpdateButton_Click);
            // 
            // NumberOfQuestionsTextBox
            // 
            this.NumberOfQuestionsTextBox.HideSelection = false;
            this.NumberOfQuestionsTextBox.Location = new System.Drawing.Point(543, 29);
            this.NumberOfQuestionsTextBox.Name = "NumberOfQuestionsTextBox";
            this.NumberOfQuestionsTextBox.Size = new System.Drawing.Size(38, 20);
            this.NumberOfQuestionsTextBox.TabIndex = 6;
            this.NumberOfQuestionsTextBox.Text = "1";
            // 
            // NumberOfQuestionsLabel
            // 
            this.NumberOfQuestionsLabel.AutoSize = true;
            this.NumberOfQuestionsLabel.Location = new System.Drawing.Point(431, 32);
            this.NumberOfQuestionsLabel.Name = "NumberOfQuestionsLabel";
            this.NumberOfQuestionsLabel.Size = new System.Drawing.Size(106, 13);
            this.NumberOfQuestionsLabel.TabIndex = 5;
            this.NumberOfQuestionsLabel.Text = "Number of Questions";
            // 
            // CUPETable
            // 
            this.CUPETable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CUPETable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Participant,
            this.Q1});
            this.CUPETable.Location = new System.Drawing.Point(3, 69);
            this.CUPETable.Name = "CUPETable";
            this.CUPETable.Size = new System.Drawing.Size(751, 239);
            this.CUPETable.TabIndex = 4;
            // 
            // Participant
            // 
            this.Participant.HeaderText = "Participant";
            this.Participant.Name = "Participant";
            // 
            // Q1
            // 
            this.Q1.HeaderText = "Q1";
            this.Q1.Name = "Q1";
            // 
            // OpenCUPEButton
            // 
            this.OpenCUPEButton.Location = new System.Drawing.Point(167, 24);
            this.OpenCUPEButton.Name = "OpenCUPEButton";
            this.OpenCUPEButton.Size = new System.Drawing.Size(107, 29);
            this.OpenCUPEButton.TabIndex = 2;
            this.OpenCUPEButton.Text = "Open CUPE";
            this.OpenCUPEButton.UseVisualStyleBackColor = true;
            // 
            // NewCUPEButton
            // 
            this.NewCUPEButton.Location = new System.Drawing.Point(26, 24);
            this.NewCUPEButton.Name = "NewCUPEButton";
            this.NewCUPEButton.Size = new System.Drawing.Size(107, 29);
            this.NewCUPEButton.TabIndex = 1;
            this.NewCUPEButton.Text = "New CUPE";
            this.NewCUPEButton.UseVisualStyleBackColor = true;
            // 
            // ITCAP
            // 
            this.ITCAP.Location = new System.Drawing.Point(4, 22);
            this.ITCAP.Name = "ITCAP";
            this.ITCAP.Size = new System.Drawing.Size(757, 366);
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
            this.MainMenu.Size = new System.Drawing.Size(789, 24);
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
            this.Menu.Size = new System.Drawing.Size(37, 20);
            this.Menu.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // New
            // 
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(152, 22);
            this.New.Text = "New";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 429);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "IBMConsultantTool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl.ResumeLayout(false);
            this.BOM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BOMTable)).EndInit();
            this.CUPE.ResumeLayout(false);
            this.CUPE.PerformLayout();
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
        private System.Windows.Forms.TextBox NumberOfQuestionsTextBox;
        private System.Windows.Forms.Label NumberOfQuestionsLabel;
        private System.Windows.Forms.DataGridView CUPETable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Participant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q1;
        private System.Windows.Forms.Button OpenCUPEButton;
        private System.Windows.Forms.Button NewCUPEButton;
        private System.Windows.Forms.Button CUPENOQUpdateButton;
        private System.Windows.Forms.Button BOMAddInitiativeButton;
        private System.Windows.Forms.Button SendBOMButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem Menu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    }
}

