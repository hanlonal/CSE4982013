namespace IBMConsultantTool
{
    partial class CrossClientForm
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
            this.AnalyzeInitiativeButton = new System.Windows.Forms.Button();
            this.CrossClientInitiativeLabel = new System.Windows.Forms.Label();
            this.InitiativeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AnalyzeInitiativeButton
            // 
            this.AnalyzeInitiativeButton.Location = new System.Drawing.Point(11, 63);
            this.AnalyzeInitiativeButton.Name = "AnalyzeInitiativeButton";
            this.AnalyzeInitiativeButton.Size = new System.Drawing.Size(179, 23);
            this.AnalyzeInitiativeButton.TabIndex = 8;
            this.AnalyzeInitiativeButton.Text = "Analyze Initiative";
            this.AnalyzeInitiativeButton.UseVisualStyleBackColor = true;
            this.AnalyzeInitiativeButton.Click += new System.EventHandler(this.AnalyzeInitiativeButton_Click);
            // 
            // CrossClientInitiativeLabel
            // 
            this.CrossClientInitiativeLabel.AutoSize = true;
            this.CrossClientInitiativeLabel.Location = new System.Drawing.Point(71, 9);
            this.CrossClientInitiativeLabel.Name = "CrossClientInitiativeLabel";
            this.CrossClientInitiativeLabel.Size = new System.Drawing.Size(46, 13);
            this.CrossClientInitiativeLabel.TabIndex = 7;
            this.CrossClientInitiativeLabel.Text = "Initiative";
            // 
            // InitiativeComboBox
            // 
            this.InitiativeComboBox.FormattingEnabled = true;
            this.InitiativeComboBox.Location = new System.Drawing.Point(10, 35);
            this.InitiativeComboBox.Name = "InitiativeComboBox";
            this.InitiativeComboBox.Size = new System.Drawing.Size(180, 21);
            this.InitiativeComboBox.TabIndex = 6;
            // 
            // CrossClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 414);
            this.Controls.Add(this.AnalyzeInitiativeButton);
            this.Controls.Add(this.CrossClientInitiativeLabel);
            this.Controls.Add(this.InitiativeComboBox);
            this.Name = "CrossClientForm";
            this.Text = "CrossClient";
            this.Load += new System.EventHandler(this.CrossClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AnalyzeInitiativeButton;
        private System.Windows.Forms.Label CrossClientInitiativeLabel;
        private System.Windows.Forms.ComboBox InitiativeComboBox;
    }
}