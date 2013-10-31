namespace IBMConsultantTool
{
    partial class NewClientForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.clientNameTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.startDateText = new System.Windows.Forms.TextBox();
            this.buisnessTypeComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create New Client";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Client Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Location:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Business Type:";
            // 
            // clientNameTextBox
            // 
            this.clientNameTextBox.Location = new System.Drawing.Point(236, 105);
            this.clientNameTextBox.Name = "clientNameTextBox";
            this.clientNameTextBox.Size = new System.Drawing.Size(179, 26);
            this.clientNameTextBox.TabIndex = 5;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(236, 164);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(179, 26);
            this.locationTextBox.TabIndex = 6;
            // 
            // startDateText
            // 
            this.startDateText.Location = new System.Drawing.Point(236, 218);
            this.startDateText.Name = "startDateText";
            this.startDateText.Size = new System.Drawing.Size(179, 26);
            this.startDateText.TabIndex = 7;
           // this.startDateText.TextChanged += new System.EventHandler(this.startDateText_TextChanged);
            // 
            // buisnessTypeComboBox
            // 
            this.buisnessTypeComboBox.FormattingEnabled = true;
            this.buisnessTypeComboBox.Location = new System.Drawing.Point(236, 276);
            this.buisnessTypeComboBox.Name = "buisnessTypeComboBox";
            this.buisnessTypeComboBox.Size = new System.Drawing.Size(179, 28);
            this.buisnessTypeComboBox.TabIndex = 8;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(283, 400);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(132, 41);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(86, 400);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(132, 41);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // NewClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 480);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.buisnessTypeComboBox);
            this.Controls.Add(this.startDateText);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.clientNameTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NewClientForm";
            this.Text = "NewClientForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NewClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox clientNameTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.TextBox startDateText;
        private System.Windows.Forms.ComboBox buisnessTypeComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}