namespace IBMConsultantTool
{
    partial class ChooseBOMClient
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
            this.ChooseClientComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseClientLabel = new System.Windows.Forms.Label();
            this.OpenBOMButton = new System.Windows.Forms.Button();
            this.NewBOMButton = new System.Windows.Forms.Button();
            this.NewClientLabel = new System.Windows.Forms.Label();
            this.NewClientTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseClientComboBox
            // 
            this.ChooseClientComboBox.FormattingEnabled = true;
            this.ChooseClientComboBox.Location = new System.Drawing.Point(51, 88);
            this.ChooseClientComboBox.Name = "ChooseClientComboBox";
            this.ChooseClientComboBox.Size = new System.Drawing.Size(121, 21);
            this.ChooseClientComboBox.TabIndex = 0;
            // 
            // ChooseClientLabel
            // 
            this.ChooseClientLabel.AutoSize = true;
            this.ChooseClientLabel.Location = new System.Drawing.Point(51, 56);
            this.ChooseClientLabel.Name = "ChooseClientLabel";
            this.ChooseClientLabel.Size = new System.Drawing.Size(81, 13);
            this.ChooseClientLabel.TabIndex = 1;
            this.ChooseClientLabel.Text = "Choose a Client";
            // 
            // OpenBOMButton
            // 
            this.OpenBOMButton.Location = new System.Drawing.Point(51, 136);
            this.OpenBOMButton.Name = "OpenBOMButton";
            this.OpenBOMButton.Size = new System.Drawing.Size(75, 23);
            this.OpenBOMButton.TabIndex = 2;
            this.OpenBOMButton.Text = "Open BOM";
            this.OpenBOMButton.UseVisualStyleBackColor = true;
            this.OpenBOMButton.Click += new System.EventHandler(this.OpenBOMButton_Click);
            // 
            // NewBOMButton
            // 
            this.NewBOMButton.Location = new System.Drawing.Point(332, 136);
            this.NewBOMButton.Name = "NewBOMButton";
            this.NewBOMButton.Size = new System.Drawing.Size(75, 23);
            this.NewBOMButton.TabIndex = 5;
            this.NewBOMButton.Text = "New BOM";
            this.NewBOMButton.UseVisualStyleBackColor = true;
            this.NewBOMButton.Click += new System.EventHandler(this.NewBOMButton_Click);
            // 
            // NewClientLabel
            // 
            this.NewClientLabel.AutoSize = true;
            this.NewClientLabel.Location = new System.Drawing.Point(332, 56);
            this.NewClientLabel.Name = "NewClientLabel";
            this.NewClientLabel.Size = new System.Drawing.Size(101, 13);
            this.NewClientLabel.TabIndex = 4;
            this.NewClientLabel.Text = "Create a New Client";
            // 
            // NewClientTextBox
            // 
            this.NewClientTextBox.Location = new System.Drawing.Point(332, 89);
            this.NewClientTextBox.Name = "NewClientTextBox";
            this.NewClientTextBox.Size = new System.Drawing.Size(100, 20);
            this.NewClientTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "--OR--";
            // 
            // ChooseClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 248);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NewClientTextBox);
            this.Controls.Add(this.NewBOMButton);
            this.Controls.Add(this.NewClientLabel);
            this.Controls.Add(this.OpenBOMButton);
            this.Controls.Add(this.ChooseClientLabel);
            this.Controls.Add(this.ChooseClientComboBox);
            this.Name = "ChooseClient";
            this.Text = "ChooseClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ChooseClientComboBox;
        private System.Windows.Forms.Label ChooseClientLabel;
        private System.Windows.Forms.Button OpenBOMButton;
        private System.Windows.Forms.Button NewBOMButton;
        private System.Windows.Forms.Label NewClientLabel;
        private System.Windows.Forms.TextBox NewClientTextBox;
        private System.Windows.Forms.Label label1;
    }
}