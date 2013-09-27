namespace IBMConsultantTool
{
    partial class OpenBOM
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
            this.ClientComboBox = new System.Windows.Forms.ComboBox();
            this.OpenBOMButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChooseAClientLabel
            // 
            this.ChooseAClientLabel.AutoSize = true;
            this.ChooseAClientLabel.Location = new System.Drawing.Point(104, 34);
            this.ChooseAClientLabel.Name = "ChooseAClientLabel";
            this.ChooseAClientLabel.Size = new System.Drawing.Size(81, 13);
            this.ChooseAClientLabel.TabIndex = 0;
            this.ChooseAClientLabel.Text = "Choose a Client";
            // 
            // ClientComboBox
            // 
            this.ClientComboBox.FormattingEnabled = true;
            this.ClientComboBox.Location = new System.Drawing.Point(87, 103);
            this.ClientComboBox.Name = "ClientComboBox";
            this.ClientComboBox.Size = new System.Drawing.Size(121, 21);
            this.ClientComboBox.TabIndex = 1;
            // 
            // OpenBOMButton
            // 
            this.OpenBOMButton.Location = new System.Drawing.Point(107, 186);
            this.OpenBOMButton.Name = "OpenBOMButton";
            this.OpenBOMButton.Size = new System.Drawing.Size(75, 23);
            this.OpenBOMButton.TabIndex = 2;
            this.OpenBOMButton.Text = "Open BOM";
            this.OpenBOMButton.UseVisualStyleBackColor = true;
            this.OpenBOMButton.Click += new System.EventHandler(this.OpenBOMButton_Click);
            // 
            // OpenBOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.OpenBOMButton);
            this.Controls.Add(this.ClientComboBox);
            this.Controls.Add(this.ChooseAClientLabel);
            this.Name = "OpenBOM";
            this.Text = "OpenBOM";
            this.Load += new System.EventHandler(this.OpenBOM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ChooseAClientLabel;
        private System.Windows.Forms.ComboBox ClientComboBox;
        private System.Windows.Forms.Button OpenBOMButton;
    }
}