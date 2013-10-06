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
            this.newCategoryButton = new System.Windows.Forms.Button();
            this.catNameTextBox = new System.Windows.Forms.TextBox();
            this.newObjectiveButton = new System.Windows.Forms.Button();
            this.newInitiativeButton = new System.Windows.Forms.Button();
            this.objNameTextBox = new System.Windows.Forms.TextBox();
            this.initNameTextBox = new System.Windows.Forms.TextBox();
            this.dataInputButton = new System.Windows.Forms.Button();
            this.critRadio = new System.Windows.Forms.RadioButton();
            this.effectRadio = new System.Windows.Forms.RadioButton();
            this.diffRadio = new System.Windows.Forms.RadioButton();
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
            // newCategoryButton
            // 
            this.newCategoryButton.Location = new System.Drawing.Point(871, 31);
            this.newCategoryButton.Name = "newCategoryButton";
            this.newCategoryButton.Size = new System.Drawing.Size(85, 23);
            this.newCategoryButton.TabIndex = 1;
            this.newCategoryButton.Text = "New Category";
            this.newCategoryButton.UseVisualStyleBackColor = true;
            this.newCategoryButton.Click += new System.EventHandler(this.newCategoryButton_Click);
            // 
            // catNameTextBox
            // 
            this.catNameTextBox.Location = new System.Drawing.Point(871, 79);
            this.catNameTextBox.Name = "catNameTextBox";
            this.catNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.catNameTextBox.TabIndex = 2;
            // 
            // newObjectiveButton
            // 
            this.newObjectiveButton.Location = new System.Drawing.Point(871, 148);
            this.newObjectiveButton.Name = "newObjectiveButton";
            this.newObjectiveButton.Size = new System.Drawing.Size(85, 23);
            this.newObjectiveButton.TabIndex = 3;
            this.newObjectiveButton.Text = "Add Objective";
            this.newObjectiveButton.UseVisualStyleBackColor = true;
            this.newObjectiveButton.Click += new System.EventHandler(this.newObjectiveButton_Click);
            // 
            // newInitiativeButton
            // 
            this.newInitiativeButton.Location = new System.Drawing.Point(871, 281);
            this.newInitiativeButton.Name = "newInitiativeButton";
            this.newInitiativeButton.Size = new System.Drawing.Size(85, 23);
            this.newInitiativeButton.TabIndex = 4;
            this.newInitiativeButton.Text = "New Initiative";
            this.newInitiativeButton.UseVisualStyleBackColor = true;
            this.newInitiativeButton.Click += new System.EventHandler(this.newInitiativeButton_Click);
            // 
            // objNameTextBox
            // 
            this.objNameTextBox.Location = new System.Drawing.Point(871, 205);
            this.objNameTextBox.Name = "objNameTextBox";
            this.objNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.objNameTextBox.TabIndex = 5;
            // 
            // initNameTextBox
            // 
            this.initNameTextBox.Location = new System.Drawing.Point(871, 347);
            this.initNameTextBox.Name = "initNameTextBox";
            this.initNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.initNameTextBox.TabIndex = 6;
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
            this.critRadio.Location = new System.Drawing.Point(764, 64);
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
            this.effectRadio.Location = new System.Drawing.Point(764, 98);
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
            this.diffRadio.Location = new System.Drawing.Point(764, 131);
            this.diffRadio.Name = "diffRadio";
            this.diffRadio.Size = new System.Drawing.Size(112, 17);
            this.diffRadio.TabIndex = 10;
            this.diffRadio.TabStop = true;
            this.diffRadio.Text = "viewDifferentiation";
            this.diffRadio.UseVisualStyleBackColor = true;
            this.diffRadio.Click += new System.EventHandler(this.diffRadio_Click);
            // 
            // BOMTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 577);
            this.Controls.Add(this.diffRadio);
            this.Controls.Add(this.effectRadio);
            this.Controls.Add(this.critRadio);
            this.Controls.Add(this.dataInputButton);
            this.Controls.Add(this.initNameTextBox);
            this.Controls.Add(this.objNameTextBox);
            this.Controls.Add(this.newInitiativeButton);
            this.Controls.Add(this.newObjectiveButton);
            this.Controls.Add(this.catNameTextBox);
            this.Controls.Add(this.newCategoryButton);
            this.Controls.Add(this.catWorkspace);
            this.Name = "BOMTool";
            this.Text = "BOMTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl catWorkspace;
        private System.Windows.Forms.Button newCategoryButton;
        private System.Windows.Forms.TextBox catNameTextBox;
        private System.Windows.Forms.Button newObjectiveButton;
        private System.Windows.Forms.Button newInitiativeButton;
        private System.Windows.Forms.TextBox objNameTextBox;
        private System.Windows.Forms.TextBox initNameTextBox;
        private System.Windows.Forms.Button dataInputButton;
        private System.Windows.Forms.RadioButton critRadio;
        private System.Windows.Forms.RadioButton effectRadio;
        private System.Windows.Forms.RadioButton diffRadio;

    }
}