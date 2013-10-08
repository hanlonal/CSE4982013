namespace IBMConsultantTool
{
    partial class BOMRedesign
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
            this.initiativeAddButton = new System.Windows.Forms.Button();
            this.categoryNames = new System.Windows.Forms.ComboBox();
            this.objectiveNames = new System.Windows.Forms.ComboBox();
            this.initiativeNames = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataInputButton = new System.Windows.Forms.Button();
            this.CreateBomSurveyButton = new System.Windows.Forms.Button();
            this.mainWorkspace = new System.Windows.Forms.Panel();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.ObjectiveLabel = new System.Windows.Forms.Label();
            this.InitiativeLabel = new System.Windows.Forms.Label();
            this.btnLoadChart = new System.Windows.Forms.Button();
            this.btnLoadRedesignChart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // initiativeAddButton
            // 
            this.initiativeAddButton.Location = new System.Drawing.Point(865, 461);
            this.initiativeAddButton.Name = "initiativeAddButton";
            this.initiativeAddButton.Size = new System.Drawing.Size(94, 23);
            this.initiativeAddButton.TabIndex = 1;
            this.initiativeAddButton.Text = "Add Initiative";
            this.initiativeAddButton.UseVisualStyleBackColor = true;
            this.initiativeAddButton.Click += new System.EventHandler(this.initiativeAddButton_Click);
            // 
            // categoryNames
            // 
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(865, 327);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(121, 21);
            this.categoryNames.TabIndex = 3;
            this.categoryNames.Text = "<Select Category>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(865, 373);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(121, 21);
            this.objectiveNames.TabIndex = 4;
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // initiativeNames
            // 
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Location = new System.Drawing.Point(865, 420);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(121, 21);
            this.initiativeNames.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(858, 510);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Send Email";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataInputButton
            // 
            this.dataInputButton.Location = new System.Drawing.Point(939, 510);
            this.dataInputButton.Name = "dataInputButton";
            this.dataInputButton.Size = new System.Drawing.Size(75, 23);
            this.dataInputButton.TabIndex = 7;
            this.dataInputButton.Text = "Input Data";
            this.dataInputButton.UseVisualStyleBackColor = true;
            this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
            // 
            // CreateBomSurveyButton
            // 
            this.CreateBomSurveyButton.Location = new System.Drawing.Point(773, 510);
            this.CreateBomSurveyButton.Margin = new System.Windows.Forms.Padding(2);
            this.CreateBomSurveyButton.Name = "CreateBomSurveyButton";
            this.CreateBomSurveyButton.Size = new System.Drawing.Size(80, 23);
            this.CreateBomSurveyButton.TabIndex = 8;
            this.CreateBomSurveyButton.Text = "Create Survey";
            this.CreateBomSurveyButton.UseVisualStyleBackColor = true;
            this.CreateBomSurveyButton.Click += new System.EventHandler(this.BomSurveyButton_Click);
            // 
            // mainWorkspace
            // 
            this.mainWorkspace.AutoScroll = true;
            this.mainWorkspace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainWorkspace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWorkspace.Location = new System.Drawing.Point(27, 12);
            this.mainWorkspace.Name = "mainWorkspace";
            this.mainWorkspace.Size = new System.Drawing.Size(702, 521);
            this.mainWorkspace.TabIndex = 9;
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Location = new System.Drawing.Point(804, 330);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(49, 13);
            this.CategoryLabel.TabIndex = 10;
            this.CategoryLabel.Text = "Category";
            // 
            // ObjectiveLabel
            // 
            this.ObjectiveLabel.AutoSize = true;
            this.ObjectiveLabel.Location = new System.Drawing.Point(756, 376);
            this.ObjectiveLabel.Name = "ObjectiveLabel";
            this.ObjectiveLabel.Size = new System.Drawing.Size(97, 13);
            this.ObjectiveLabel.TabIndex = 11;
            this.ObjectiveLabel.Text = "Business Objective";
            // 
            // InitiativeLabel
            // 
            this.InitiativeLabel.AutoSize = true;
            this.InitiativeLabel.Location = new System.Drawing.Point(807, 423);
            this.InitiativeLabel.Name = "InitiativeLabel";
            this.InitiativeLabel.Size = new System.Drawing.Size(46, 13);
            this.InitiativeLabel.TabIndex = 12;
            this.InitiativeLabel.Text = "Initiative";
            // 
            // btnLoadChart
            // 
            this.btnLoadChart.Location = new System.Drawing.Point(778, 551);
            this.btnLoadChart.Name = "btnLoadChart";
            this.btnLoadChart.Size = new System.Drawing.Size(75, 23);
            this.btnLoadChart.TabIndex = 13;
            this.btnLoadChart.Text = "Load Chart";
            this.btnLoadChart.UseVisualStyleBackColor = true;
            this.btnLoadChart.Click += new System.EventHandler(this.btnLoadChart_Click);
            // 
            // btnLoadRedesignChart
            // 
            this.btnLoadRedesignChart.Location = new System.Drawing.Point(865, 551);
            this.btnLoadRedesignChart.Name = "btnLoadRedesignChart";
            this.btnLoadRedesignChart.Size = new System.Drawing.Size(121, 23);
            this.btnLoadRedesignChart.TabIndex = 14;
            this.btnLoadRedesignChart.Text = "Load Chart Redesign";
            this.btnLoadRedesignChart.UseVisualStyleBackColor = true;
            this.btnLoadRedesignChart.Click += new System.EventHandler(this.btnLoadRedesignChart_Click);
            // 
            // BOMRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 703);
            this.Controls.Add(this.btnLoadRedesignChart);
            this.Controls.Add(this.btnLoadChart);
            this.Controls.Add(this.InitiativeLabel);
            this.Controls.Add(this.ObjectiveLabel);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.mainWorkspace);
            this.Controls.Add(this.CreateBomSurveyButton);
            this.Controls.Add(this.dataInputButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.initiativeNames);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.categoryNames);
            this.Controls.Add(this.initiativeAddButton);
            this.Name = "BOMRedesign";
            this.Text = "BOMRedesign";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button initiativeAddButton;
        private System.Windows.Forms.ComboBox categoryNames;
        private System.Windows.Forms.ComboBox objectiveNames;
        private System.Windows.Forms.ComboBox initiativeNames;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button dataInputButton;
        private System.Windows.Forms.Button CreateBomSurveyButton;
        private System.Windows.Forms.Panel mainWorkspace;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Label ObjectiveLabel;
        private System.Windows.Forms.Label InitiativeLabel;
        private System.Windows.Forms.Button btnLoadChart;
        private System.Windows.Forms.Button btnLoadRedesignChart;


    }
}