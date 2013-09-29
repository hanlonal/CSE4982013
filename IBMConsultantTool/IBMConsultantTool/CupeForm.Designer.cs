namespace IBMConsultantTool
{
    partial class CupeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.personNameLabel = new System.Windows.Forms.Label();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(70, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 224);
            this.panel1.TabIndex = 0;
            // 
            // personNameLabel
            // 
            this.personNameLabel.AutoSize = true;
            this.personNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.personNameLabel.Location = new System.Drawing.Point(352, 44);
            this.personNameLabel.Name = "personNameLabel";
            this.personNameLabel.Size = new System.Drawing.Size(37, 15);
            this.personNameLabel.TabIndex = 1;
            this.personNameLabel.Text = "Name";
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(427, 39);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(75, 23);
            this.addPersonButton.TabIndex = 2;
            this.addPersonButton.Text = "New Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // CupeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 406);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.personNameLabel);
            this.Controls.Add(this.panel1);
            this.Name = "CupeForm";
            this.Text = "CupeForm";
            this.Load += new System.EventHandler(this.CupeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label personNameLabel;
        private System.Windows.Forms.Button addPersonButton;
    }
}