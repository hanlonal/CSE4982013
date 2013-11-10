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
            this.AnalyzeImperativeButton = new System.Windows.Forms.Button();
            this.CrossClientImperativeLabel = new System.Windows.Forms.Label();
            this.ImperativeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AnalyzeImperativeButton
            // 
            this.AnalyzeImperativeButton.Location = new System.Drawing.Point(11, 63);
            this.AnalyzeImperativeButton.Name = "AnalyzeImperativeButton";
            this.AnalyzeImperativeButton.Size = new System.Drawing.Size(179, 23);
            this.AnalyzeImperativeButton.TabIndex = 8;
            this.AnalyzeImperativeButton.Text = "Analyze Imperative";
            this.AnalyzeImperativeButton.UseVisualStyleBackColor = true;
            this.AnalyzeImperativeButton.Click += new System.EventHandler(this.AnalyzeImperativeButton_Click);
            // 
            // CrossClientImperativeLabel
            // 
            this.CrossClientImperativeLabel.AutoSize = true;
            this.CrossClientImperativeLabel.Location = new System.Drawing.Point(71, 9);
            this.CrossClientImperativeLabel.Name = "CrossClientImperativeLabel";
            this.CrossClientImperativeLabel.Size = new System.Drawing.Size(46, 13);
            this.CrossClientImperativeLabel.TabIndex = 7;
            this.CrossClientImperativeLabel.Text = "Imperative";
            // 
            // ImperativeComboBox
            // 
            this.ImperativeComboBox.FormattingEnabled = true;
            this.ImperativeComboBox.Location = new System.Drawing.Point(10, 35);
            this.ImperativeComboBox.Name = "ImperativeComboBox";
            this.ImperativeComboBox.Size = new System.Drawing.Size(180, 21);
            this.ImperativeComboBox.TabIndex = 6;
            // 
            // CrossClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 414);
            this.Controls.Add(this.AnalyzeImperativeButton);
            this.Controls.Add(this.CrossClientImperativeLabel);
            this.Controls.Add(this.ImperativeComboBox);
            this.Name = "CrossClientForm";
            this.Text = "CrossClient";
            this.Load += new System.EventHandler(this.CrossClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AnalyzeImperativeButton;
        private System.Windows.Forms.Label CrossClientImperativeLabel;
        private System.Windows.Forms.ComboBox ImperativeComboBox;
    }
}