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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // catWorkspace
            // 
            this.catWorkspace.Location = new System.Drawing.Point(16, 38);
            this.catWorkspace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.catWorkspace.Name = "catWorkspace";
            this.catWorkspace.SelectedIndex = 0;
            this.catWorkspace.Size = new System.Drawing.Size(995, 482);
            this.catWorkspace.TabIndex = 0;
            // 
            // dataInputButton
            // 
            this.dataInputButton.Location = new System.Drawing.Point(1161, 492);
            this.dataInputButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataInputButton.Name = "dataInputButton";
            this.dataInputButton.Size = new System.Drawing.Size(100, 28);
            this.dataInputButton.TabIndex = 7;
            this.dataInputButton.Text = "Input Data";
            this.dataInputButton.UseVisualStyleBackColor = true;
            this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
            // 
            // critRadio
            // 
            this.critRadio.AutoSize = true;
            this.critRadio.Location = new System.Drawing.Point(1019, 78);
            this.critRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.critRadio.Name = "critRadio";
            this.critRadio.Size = new System.Drawing.Size(118, 21);
            this.critRadio.TabIndex = 8;
            this.critRadio.TabStop = true;
            this.critRadio.Text = "View Criticality";
            this.critRadio.UseVisualStyleBackColor = true;
            this.critRadio.Click += new System.EventHandler(this.critRadio_Click);
            // 
            // effectRadio
            // 
            this.effectRadio.AutoSize = true;
            this.effectRadio.Location = new System.Drawing.Point(1019, 38);
            this.effectRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.effectRadio.Name = "effectRadio";
            this.effectRadio.Size = new System.Drawing.Size(146, 21);
            this.effectRadio.TabIndex = 9;
            this.effectRadio.TabStop = true;
            this.effectRadio.Text = "View Effectiveness";
            this.effectRadio.UseVisualStyleBackColor = true;
            this.effectRadio.Click += new System.EventHandler(this.effectRadio_Click);
            // 
            // diffRadio
            // 
            this.diffRadio.AutoSize = true;
            this.diffRadio.Location = new System.Drawing.Point(1017, 117);
            this.diffRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.diffRadio.Name = "diffRadio";
            this.diffRadio.Size = new System.Drawing.Size(150, 21);
            this.diffRadio.TabIndex = 10;
            this.diffRadio.TabStop = true;
            this.diffRadio.Text = "View Differentiation";
            this.diffRadio.UseVisualStyleBackColor = true;
            this.diffRadio.Click += new System.EventHandler(this.diffRadio_Click);
            // 
            // categoryNames
            // 
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(1100, 238);
            this.categoryNames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(160, 24);
            this.categoryNames.TabIndex = 11;
            this.categoryNames.Text = "<Select Category>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(1100, 297);
            this.objectiveNames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(160, 24);
            this.objectiveNames.TabIndex = 12;
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // initiativeNames
            // 
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Location = new System.Drawing.Point(1100, 357);
            this.initiativeNames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(160, 24);
            this.initiativeNames.TabIndex = 13;
            // 
            // AddInitiativeButton
            // 
            this.AddInitiativeButton.Location = new System.Drawing.Point(1160, 430);
            this.AddInitiativeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddInitiativeButton.Name = "AddInitiativeButton";
            this.AddInitiativeButton.Size = new System.Drawing.Size(100, 28);
            this.AddInitiativeButton.TabIndex = 14;
            this.AddInitiativeButton.Text = "Add Initiative";
            this.AddInitiativeButton.UseVisualStyleBackColor = true;
            this.AddInitiativeButton.Click += new System.EventHandler(this.AddInitiativeButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1047, 492);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 28);
            this.button1.TabIndex = 15;
            this.button1.Text = "Create Survey";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BomSurveyButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1047, 526);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 30);
            this.button2.TabIndex = 16;
            this.button2.Text = "Email Surveys";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SendEmailButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1047, 562);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 29);
            this.button3.TabIndex = 17;
            this.button3.Text = "Open Surveys";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OpenSurvey_Clicked);
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 710);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "BOMTool";
            this.Text = "BOMTool";
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

    }
}