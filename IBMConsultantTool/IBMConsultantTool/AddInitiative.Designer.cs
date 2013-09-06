namespace IBMConsultantTool
{
    partial class AddInitiative
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
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.CategoryComboBox = new System.Windows.Forms.ComboBox();
            this.NewCategoryButton = new System.Windows.Forms.Button();
            this.NewBusinessObjectiveButton = new System.Windows.Forms.Button();
            this.BusinessObjectiveComboBox = new System.Windows.Forms.ComboBox();
            this.BusinessObjectiveLabel = new System.Windows.Forms.Label();
            this.NewInitiativeButton = new System.Windows.Forms.Button();
            this.InitiativeComboBox = new System.Windows.Forms.ComboBox();
            this.InitiativeLabel = new System.Windows.Forms.Label();
            this.AddInitiativeToTableButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Location = new System.Drawing.Point(67, 38);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(49, 13);
            this.CategoryLabel.TabIndex = 0;
            this.CategoryLabel.Text = "Category";
            // 
            // CategoryComboBox
            // 
            this.CategoryComboBox.FormattingEnabled = true;
            this.CategoryComboBox.Location = new System.Drawing.Point(161, 38);
            this.CategoryComboBox.Name = "CategoryComboBox";
            this.CategoryComboBox.Size = new System.Drawing.Size(348, 21);
            this.CategoryComboBox.TabIndex = 1;
            // 
            // NewCategoryButton
            // 
            this.NewCategoryButton.Location = new System.Drawing.Point(548, 35);
            this.NewCategoryButton.Name = "NewCategoryButton";
            this.NewCategoryButton.Size = new System.Drawing.Size(118, 24);
            this.NewCategoryButton.TabIndex = 2;
            this.NewCategoryButton.Text = "New Category";
            this.NewCategoryButton.UseVisualStyleBackColor = true;
            // 
            // NewBusinessObjectiveButton
            // 
            this.NewBusinessObjectiveButton.Location = new System.Drawing.Point(548, 93);
            this.NewBusinessObjectiveButton.Name = "NewBusinessObjectiveButton";
            this.NewBusinessObjectiveButton.Size = new System.Drawing.Size(141, 24);
            this.NewBusinessObjectiveButton.TabIndex = 5;
            this.NewBusinessObjectiveButton.Text = "New Business Objective";
            this.NewBusinessObjectiveButton.UseVisualStyleBackColor = true;
            // 
            // BusinessObjectiveComboBox
            // 
            this.BusinessObjectiveComboBox.FormattingEnabled = true;
            this.BusinessObjectiveComboBox.Location = new System.Drawing.Point(161, 96);
            this.BusinessObjectiveComboBox.Name = "BusinessObjectiveComboBox";
            this.BusinessObjectiveComboBox.Size = new System.Drawing.Size(348, 21);
            this.BusinessObjectiveComboBox.TabIndex = 4;
            // 
            // BusinessObjectiveLabel
            // 
            this.BusinessObjectiveLabel.AutoSize = true;
            this.BusinessObjectiveLabel.Location = new System.Drawing.Point(19, 99);
            this.BusinessObjectiveLabel.Name = "BusinessObjectiveLabel";
            this.BusinessObjectiveLabel.Size = new System.Drawing.Size(97, 13);
            this.BusinessObjectiveLabel.TabIndex = 3;
            this.BusinessObjectiveLabel.Text = "Business Objective";
            // 
            // NewInitiativeButton
            // 
            this.NewInitiativeButton.Location = new System.Drawing.Point(548, 156);
            this.NewInitiativeButton.Name = "NewInitiativeButton";
            this.NewInitiativeButton.Size = new System.Drawing.Size(118, 24);
            this.NewInitiativeButton.TabIndex = 8;
            this.NewInitiativeButton.Text = "New Initiative";
            this.NewInitiativeButton.UseVisualStyleBackColor = true;
            // 
            // InitiativeComboBox
            // 
            this.InitiativeComboBox.FormattingEnabled = true;
            this.InitiativeComboBox.Location = new System.Drawing.Point(161, 159);
            this.InitiativeComboBox.Name = "InitiativeComboBox";
            this.InitiativeComboBox.Size = new System.Drawing.Size(348, 21);
            this.InitiativeComboBox.TabIndex = 7;
            // 
            // InitiativeLabel
            // 
            this.InitiativeLabel.AutoSize = true;
            this.InitiativeLabel.Location = new System.Drawing.Point(70, 159);
            this.InitiativeLabel.Name = "InitiativeLabel";
            this.InitiativeLabel.Size = new System.Drawing.Size(46, 13);
            this.InitiativeLabel.TabIndex = 6;
            this.InitiativeLabel.Text = "Initiative";
            // 
            // AddInitiativeToTableButton
            // 
            this.AddInitiativeToTableButton.Location = new System.Drawing.Point(297, 257);
            this.AddInitiativeToTableButton.Name = "AddInitiativeToTableButton";
            this.AddInitiativeToTableButton.Size = new System.Drawing.Size(134, 36);
            this.AddInitiativeToTableButton.TabIndex = 9;
            this.AddInitiativeToTableButton.Text = "Add Initiative to Table";
            this.AddInitiativeToTableButton.UseVisualStyleBackColor = true;
            // 
            // AddInitiative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 342);
            this.Controls.Add(this.AddInitiativeToTableButton);
            this.Controls.Add(this.NewInitiativeButton);
            this.Controls.Add(this.InitiativeComboBox);
            this.Controls.Add(this.InitiativeLabel);
            this.Controls.Add(this.NewBusinessObjectiveButton);
            this.Controls.Add(this.BusinessObjectiveComboBox);
            this.Controls.Add(this.BusinessObjectiveLabel);
            this.Controls.Add(this.NewCategoryButton);
            this.Controls.Add(this.CategoryComboBox);
            this.Controls.Add(this.CategoryLabel);
            this.Name = "AddInitiative";
            this.Text = "AddInitiative";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.ComboBox CategoryComboBox;
        private System.Windows.Forms.Button NewCategoryButton;
        private System.Windows.Forms.Button NewBusinessObjectiveButton;
        private System.Windows.Forms.ComboBox BusinessObjectiveComboBox;
        private System.Windows.Forms.Label BusinessObjectiveLabel;
        private System.Windows.Forms.Button NewInitiativeButton;
        private System.Windows.Forms.ComboBox InitiativeComboBox;
        private System.Windows.Forms.Label InitiativeLabel;
        private System.Windows.Forms.Button AddInitiativeToTableButton;
    }
}