namespace IBMConsultantTool
{
    partial class NewCategory
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
            this.NewCategoryTextBox = new System.Windows.Forms.TextBox();
            this.NewCategoryCreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NewCategoryTextBox
            // 
            this.NewCategoryTextBox.Location = new System.Drawing.Point(36, 80);
            this.NewCategoryTextBox.Name = "NewCategoryTextBox";
            this.NewCategoryTextBox.Size = new System.Drawing.Size(210, 20);
            this.NewCategoryTextBox.TabIndex = 0;
            // 
            // NewCategoryCreateButton
            // 
            this.NewCategoryCreateButton.Location = new System.Drawing.Point(98, 153);
            this.NewCategoryCreateButton.Name = "NewCategoryCreateButton";
            this.NewCategoryCreateButton.Size = new System.Drawing.Size(75, 23);
            this.NewCategoryCreateButton.TabIndex = 1;
            this.NewCategoryCreateButton.Text = "Create";
            this.NewCategoryCreateButton.UseVisualStyleBackColor = true;
            this.NewCategoryCreateButton.Click += new System.EventHandler(this.NewCategoryCreateButton_Click);
            // 
            // NewCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.NewCategoryCreateButton);
            this.Controls.Add(this.NewCategoryTextBox);
            this.Name = "NewCategory";
            this.Text = "NewCategory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NewCategoryTextBox;
        private System.Windows.Forms.Button NewCategoryCreateButton;
    }
}