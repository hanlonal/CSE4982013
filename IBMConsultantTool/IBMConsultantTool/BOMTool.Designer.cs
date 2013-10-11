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
            this.SuspendLayout();
            // 
            // catWorkspace
            // 
            this.catWorkspace.Location = new System.Drawing.Point(12, 31);
            this.catWorkspace.Name = "catWorkspace";
            this.catWorkspace.SelectedIndex = 0;
            this.catWorkspace.Size = new System.Drawing.Size(746, 392);
            this.catWorkspace.TabIndex = 0;
            // 
            // dataInputButton
            // 
            this.dataInputButton.Location = new System.Drawing.Point(871, 400);
            this.dataInputButton.Name = "dataInputButton";
            this.dataInputButton.Size = new System.Drawing.Size(75, 23);
            this.dataInputButton.TabIndex = 7;
            this.dataInputButton.Text = "Input Data";
            this.dataInputButton.UseVisualStyleBackColor = true;
            this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
            // 
            // critRadio
            // 
            this.critRadio.AutoSize = true;
            this.critRadio.Location = new System.Drawing.Point(764, 63);
            this.critRadio.Name = "critRadio";
            this.critRadio.Size = new System.Drawing.Size(92, 17);
            this.critRadio.TabIndex = 8;
            this.critRadio.TabStop = true;
            this.critRadio.Text = "View Criticality";
            this.critRadio.UseVisualStyleBackColor = true;
            this.critRadio.Click += new System.EventHandler(this.critRadio_Click);
            // 
            // effectRadio
            // 
            this.effectRadio.AutoSize = true;
            this.effectRadio.Location = new System.Drawing.Point(764, 31);
            this.effectRadio.Name = "effectRadio";
            this.effectRadio.Size = new System.Drawing.Size(115, 17);
            this.effectRadio.TabIndex = 9;
            this.effectRadio.TabStop = true;
            this.effectRadio.Text = "View Effectiveness";
            this.effectRadio.UseVisualStyleBackColor = true;
            this.effectRadio.Click += new System.EventHandler(this.effectRadio_Click);
            // 
            // diffRadio
            // 
            this.diffRadio.AutoSize = true;
            this.diffRadio.Location = new System.Drawing.Point(763, 95);
            this.diffRadio.Name = "diffRadio";
            this.diffRadio.Size = new System.Drawing.Size(116, 17);
            this.diffRadio.TabIndex = 10;
            this.diffRadio.TabStop = true;
            this.diffRadio.Text = "View Differentiation";
            this.diffRadio.UseVisualStyleBackColor = true;
            this.diffRadio.Click += new System.EventHandler(this.diffRadio_Click);
            // 
            // categoryNames
            // 
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Location = new System.Drawing.Point(825, 193);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(121, 21);
            this.categoryNames.TabIndex = 11;
            this.categoryNames.Text = "<Select Category>";
            this.categoryNames.SelectedIndexChanged += new System.EventHandler(this.categoryNames_SelectedIndexChanged);
            this.categoryNames.LostFocus += new System.EventHandler(this.categoryNames_LostFocus);
            // 
            // objectiveNames
            // 
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Location = new System.Drawing.Point(825, 241);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(121, 21);
            this.objectiveNames.TabIndex = 12;
            this.objectiveNames.SelectedIndexChanged += new System.EventHandler(this.objectiveNames_SelectedIndexChanged);
            this.objectiveNames.LostFocus += new System.EventHandler(this.objectiveNames_LostFocus);
            // 
            // initiativeNames
            // 
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Location = new System.Drawing.Point(825, 290);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(121, 21);
            this.initiativeNames.TabIndex = 13;
            // 
            // AddInitiativeButton
            // 
            this.AddInitiativeButton.Location = new System.Drawing.Point(870, 349);
            this.AddInitiativeButton.Name = "AddInitiativeButton";
            this.AddInitiativeButton.Size = new System.Drawing.Size(75, 23);
            this.AddInitiativeButton.TabIndex = 14;
            this.AddInitiativeButton.Text = "Add Initiative";
            this.AddInitiativeButton.UseVisualStyleBackColor = true;
            this.AddInitiativeButton.Click += new System.EventHandler(this.AddInitiativeButton_Click);
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 577);
            this.Controls.Add(this.AddInitiativeButton);
            this.Controls.Add(this.initiativeNames);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.categoryNames);
            this.Controls.Add(this.diffRadio);
            this.Controls.Add(this.effectRadio);
            this.Controls.Add(this.critRadio);
            this.Controls.Add(this.dataInputButton);
            this.Controls.Add(this.catWorkspace);
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

    }
}