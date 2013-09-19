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
            this.categoryAddButton = new System.Windows.Forms.Button();
            this.initiativeAddButton = new System.Windows.Forms.Button();
            this.objectiveAddButton = new System.Windows.Forms.Button();
            this.categoryNames = new System.Windows.Forms.ComboBox();
            this.objectiveNames = new System.Windows.Forms.ComboBox();
            this.initiativeNames = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataInputButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // categoryAddButton
            // 
            this.categoryAddButton.Location = new System.Drawing.Point(691, 12);
            this.categoryAddButton.Name = "categoryAddButton";
            this.categoryAddButton.Size = new System.Drawing.Size(94, 23);
            this.categoryAddButton.TabIndex = 0;
            this.categoryAddButton.Text = "Add Category";
            this.categoryAddButton.UseVisualStyleBackColor = true;
            this.categoryAddButton.Click += new System.EventHandler(this.categoryAddButton_Click);
            // 
            // initiativeAddButton
            // 
            this.initiativeAddButton.Location = new System.Drawing.Point(691, 214);
            this.initiativeAddButton.Name = "initiativeAddButton";
            this.initiativeAddButton.Size = new System.Drawing.Size(94, 23);
            this.initiativeAddButton.TabIndex = 1;
            this.initiativeAddButton.Text = "Add Initiative";
            this.initiativeAddButton.UseVisualStyleBackColor = true;
            this.initiativeAddButton.Click += new System.EventHandler(this.initiativeAddButton_Click);
            // 
            // objectiveAddButton
            // 
            this.objectiveAddButton.Location = new System.Drawing.Point(691, 101);
            this.objectiveAddButton.Name = "objectiveAddButton";
            this.objectiveAddButton.Size = new System.Drawing.Size(94, 23);
            this.objectiveAddButton.TabIndex = 2;
            this.objectiveAddButton.Text = "Add Objective";
            this.objectiveAddButton.UseVisualStyleBackColor = true;
            this.objectiveAddButton.Click += new System.EventHandler(this.objectiveAddButton_Click);
            // 
            // categoryNames
            // 
            this.categoryNames.FormattingEnabled = true;
            this.categoryNames.Items.AddRange(new object[] {
            "Category A ",
            "Category B",
            "Category C",
            "Category D",
            "Category E"});
            this.categoryNames.Location = new System.Drawing.Point(691, 57);
            this.categoryNames.Name = "categoryNames";
            this.categoryNames.Size = new System.Drawing.Size(121, 21);
            this.categoryNames.TabIndex = 3;
            this.categoryNames.Text = "Select Category";
            // 
            // objectiveNames
            // 
            this.objectiveNames.FormattingEnabled = true;
            this.objectiveNames.Items.AddRange(new object[] {
            "Objective A",
            "Objective B",
            "Objective C",
            "Objective D",
            "Objective E"});
            this.objectiveNames.Location = new System.Drawing.Point(691, 153);
            this.objectiveNames.Name = "objectiveNames";
            this.objectiveNames.Size = new System.Drawing.Size(121, 21);
            this.objectiveNames.TabIndex = 4;
            this.objectiveNames.Text = "Select Objective";
            // 
            // initiativeNames
            // 
            this.initiativeNames.FormattingEnabled = true;
            this.initiativeNames.Items.AddRange(new object[] {
            "Initiative A",
            "Initiative B",
            "Initiative C",
            "Initiative D",
            "Initiative E"});
            this.initiativeNames.Location = new System.Drawing.Point(691, 267);
            this.initiativeNames.Name = "initiativeNames";
            this.initiativeNames.Size = new System.Drawing.Size(121, 21);
            this.initiativeNames.TabIndex = 5;
            this.initiativeNames.Text = "Select Initiatives";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(741, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataInputButton
            // 
            this.dataInputButton.Location = new System.Drawing.Point(643, 324);
            this.dataInputButton.Name = "dataInputButton";
            this.dataInputButton.Size = new System.Drawing.Size(75, 23);
            this.dataInputButton.TabIndex = 7;
            this.dataInputButton.Text = "Input Data";
            this.dataInputButton.UseVisualStyleBackColor = true;
            this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
            // 
            // BOMRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 375);
            this.Controls.Add(this.dataInputButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.initiativeNames);
            this.Controls.Add(this.objectiveNames);
            this.Controls.Add(this.categoryNames);
            this.Controls.Add(this.objectiveAddButton);
            this.Controls.Add(this.initiativeAddButton);
            this.Controls.Add(this.categoryAddButton);
            this.Name = "BOMRedesign";
            this.Text = "BOMRedesign";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button categoryAddButton;
        private System.Windows.Forms.Button initiativeAddButton;
        private System.Windows.Forms.Button objectiveAddButton;
        private System.Windows.Forms.ComboBox categoryNames;
        private System.Windows.Forms.ComboBox objectiveNames;
        private System.Windows.Forms.ComboBox initiativeNames;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button dataInputButton;


    }
}