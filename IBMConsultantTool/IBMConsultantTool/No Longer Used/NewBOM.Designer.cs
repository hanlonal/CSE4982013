namespace IBMConsultantTool
{
    partial class NewBOM
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
            this.ChooseAClientLabel = new System.Windows.Forms.Label();
            this.NewBOMCreateBOMButton = new System.Windows.Forms.Button();
            this.NewClientTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ChooseAClientLabel
            // 
            this.ChooseAClientLabel.AutoSize = true;
            this.ChooseAClientLabel.Location = new System.Drawing.Point(93, 37);
            this.ChooseAClientLabel.Name = "ChooseAClientLabel";
            this.ChooseAClientLabel.Size = new System.Drawing.Size(106, 13);
            this.ChooseAClientLabel.TabIndex = 1;
            this.ChooseAClientLabel.Text = "Choose a New Client";
            // 
            // NewBOMCreateBOMButton
            // 
            this.NewBOMCreateBOMButton.Location = new System.Drawing.Point(105, 189);
            this.NewBOMCreateBOMButton.Name = "NewBOMCreateBOMButton";
            this.NewBOMCreateBOMButton.Size = new System.Drawing.Size(75, 23);
            this.NewBOMCreateBOMButton.TabIndex = 2;
            this.NewBOMCreateBOMButton.Text = "Create BOM";
            this.NewBOMCreateBOMButton.UseVisualStyleBackColor = true;
            this.NewBOMCreateBOMButton.Click += new System.EventHandler(this.NewBOMCreateBOMButton_Click);
            // 
            // NewClientTextBox
            // 
            this.NewClientTextBox.Location = new System.Drawing.Point(96, 101);
            this.NewClientTextBox.Name = "NewClientTextBox";
            this.NewClientTextBox.Size = new System.Drawing.Size(100, 20);
            this.NewClientTextBox.TabIndex = 3;
            // 
            // NewBOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.NewClientTextBox);
            this.Controls.Add(this.NewBOMCreateBOMButton);
            this.Controls.Add(this.ChooseAClientLabel);
            this.Name = "NewBOM";
            this.Text = "NewBOM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ChooseAClientLabel;
        private System.Windows.Forms.Button NewBOMCreateBOMButton;
        private System.Windows.Forms.TextBox NewClientTextBox;

    }
}