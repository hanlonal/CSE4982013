namespace IBMConsultantTool
{
    partial class ChooseCUPEClient
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
            this.OpenCUPEButton = new System.Windows.Forms.Button();
            this.NewCUPEButton = new System.Windows.Forms.Button();
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
            // OpenCUPEButton
            // 
            this.OpenCUPEButton.Location = new System.Drawing.Point(51, 136);
            this.OpenCUPEButton.Name = "OpenCUPEButton";
            this.OpenCUPEButton.Size = new System.Drawing.Size(75, 23);
            this.OpenCUPEButton.TabIndex = 2;
            this.OpenCUPEButton.Text = "Open CUPE";
            this.OpenCUPEButton.UseVisualStyleBackColor = true;
            this.OpenCUPEButton.Click += new System.EventHandler(this.OpenCUPEButton_Click);
            // 
            // NewCUPEButton
            // 
            this.NewCUPEButton.Location = new System.Drawing.Point(332, 136);
            this.NewCUPEButton.Name = "NewCUPEButton";
            this.NewCUPEButton.Size = new System.Drawing.Size(75, 23);
            this.NewCUPEButton.TabIndex = 5;
            this.NewCUPEButton.Text = "New CUPE";
            this.NewCUPEButton.UseVisualStyleBackColor = true;
            this.NewCUPEButton.Click += new System.EventHandler(this.NewCUPEButton_Click);
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
            // ChooseCUPEClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 248);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NewClientTextBox);
            this.Controls.Add(this.NewCUPEButton);
            this.Controls.Add(this.NewClientLabel);
            this.Controls.Add(this.OpenCUPEButton);
            this.Controls.Add(this.ChooseClientLabel);
            this.Controls.Add(this.ChooseClientComboBox);
            this.Name = "ChooseCUPEClient";
            this.Text = "ChooseClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ChooseClientComboBox;
        private System.Windows.Forms.Label ChooseClientLabel;
        private System.Windows.Forms.Button OpenCUPEButton;
        private System.Windows.Forms.Button NewCUPEButton;
        private System.Windows.Forms.Label NewClientLabel;
        private System.Windows.Forms.TextBox NewClientTextBox;
        private System.Windows.Forms.Label label1;
    }
}