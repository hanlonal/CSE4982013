namespace IBMConsultantTool
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.clientTypeLabel = new System.Windows.Forms.Label();
            this.dateStartedLabel = new System.Windows.Forms.Label();
            this.runITCapButton = new System.Windows.Forms.Button();
            this.runBomButton = new System.Windows.Forms.Button();
            this.runCupeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.clientLocationLabel = new System.Windows.Forms.Label();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.textFilePathInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.clientNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientNameLabel.Location = new System.Drawing.Point(264, 183);
            this.clientNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(133, 26);
            this.clientNameLabel.TabIndex = 1;
            this.clientNameLabel.Text = "Client Name";
            // 
            // clientTypeLabel
            // 
            this.clientTypeLabel.AutoSize = true;
            this.clientTypeLabel.BackColor = System.Drawing.Color.Transparent;
            this.clientTypeLabel.Location = new System.Drawing.Point(264, 234);
            this.clientTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientTypeLabel.Name = "clientTypeLabel";
            this.clientTypeLabel.Size = new System.Drawing.Size(87, 20);
            this.clientTypeLabel.TabIndex = 3;
            this.clientTypeLabel.Text = "Client Type";
            // 
            // dateStartedLabel
            // 
            this.dateStartedLabel.AutoSize = true;
            this.dateStartedLabel.BackColor = System.Drawing.Color.Transparent;
            this.dateStartedLabel.Location = new System.Drawing.Point(853, 194);
            this.dateStartedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateStartedLabel.Name = "dateStartedLabel";
            this.dateStartedLabel.Size = new System.Drawing.Size(101, 20);
            this.dateStartedLabel.TabIndex = 4;
            this.dateStartedLabel.Text = "Date Started";
            // 
            // runITCapButton
            // 
            this.runITCapButton.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            this.runITCapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runITCapButton.Location = new System.Drawing.Point(132, 430);
            this.runITCapButton.Name = "runITCapButton";
            this.runITCapButton.Size = new System.Drawing.Size(186, 62);
            this.runITCapButton.TabIndex = 7;
            this.runITCapButton.Text = "IT Capability Assessment Tool";
            this.runITCapButton.UseVisualStyleBackColor = true;
            this.runITCapButton.Click += new System.EventHandler(this.runITCapButton_Click);
            // 
            // runBomButton
            // 
            this.runBomButton.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            this.runBomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBomButton.Location = new System.Drawing.Point(132, 331);
            this.runBomButton.Name = "runBomButton";
            this.runBomButton.Size = new System.Drawing.Size(186, 64);
            this.runBomButton.TabIndex = 8;
            this.runBomButton.Text = "Business Objective Management Tool";
            this.runBomButton.UseVisualStyleBackColor = true;
            this.runBomButton.Click += new System.EventHandler(this.runBomButton_Click);
            // 
            // runCupeButton
            // 
            this.runCupeButton.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            this.runCupeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runCupeButton.Location = new System.Drawing.Point(132, 534);
            this.runCupeButton.Name = "runCupeButton";
            this.runCupeButton.Size = new System.Drawing.Size(186, 60);
            this.runCupeButton.TabIndex = 9;
            this.runCupeButton.Text = "IT Provider Relationship Tool";
            this.runCupeButton.UseVisualStyleBackColor = true;
            this.runCupeButton.Click += new System.EventHandler(this.runCupeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::IBMConsultantTool.Properties.Resources.startpage_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1006, 150);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(409, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 34);
            this.label1.TabIndex = 12;
            this.label1.Text = "Organize the business objectives and determine the \r\nactions needed to complete t" +
                "hose objectives. (BOM)\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(409, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 51);
            this.label2.TabIndex = 13;
            this.label2.Text = "Utilize the questionairre to determine the client\'s\r\nas-is and to-be state of var" +
                "ious attributes\r\nthat contribute towards the IT factor. (ITCAP)\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(409, 545);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 51);
            this.label3.TabIndex = 14;
            this.label3.Text = "Use hueristics and questionairre replies to \r\ndetermine the state between the IT " +
                "and \r\nbusiness branches of the client. (CUPE)\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 183);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 31);
            this.label4.TabIndex = 15;
            this.label4.Text = "Welcome!";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(743, 194);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Start date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(752, 234);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Location:";
            // 
            // clientLocationLabel
            // 
            this.clientLocationLabel.AutoSize = true;
            this.clientLocationLabel.BackColor = System.Drawing.Color.Transparent;
            this.clientLocationLabel.Location = new System.Drawing.Point(862, 234);
            this.clientLocationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientLocationLabel.Name = "clientLocationLabel";
            this.clientLocationLabel.Size = new System.Drawing.Size(70, 20);
            this.clientLocationLabel.TabIndex = 18;
            this.clientLocationLabel.Text = "Location";
            // 
            // btnFilePath
            // 
            this.btnFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePath.Location = new System.Drawing.Point(845, 637);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(109, 27);
            this.btnFilePath.TabIndex = 19;
            this.btnFilePath.Text = "Browse...";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.BackColor = System.Drawing.Color.Transparent;
            this.labelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilePath.Location = new System.Drawing.Point(129, 644);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(172, 17);
            this.labelFilePath.TabIndex = 20;
            this.labelFilePath.Text = "This is where charts save:";
            // 
            // textFilePathInfo
            // 
            this.textFilePathInfo.Location = new System.Drawing.Point(412, 638);
            this.textFilePathInfo.Name = "textFilePathInfo";
            this.textFilePathInfo.ReadOnly = true;
            this.textFilePathInfo.Size = new System.Drawing.Size(337, 26);
            this.textFilePathInfo.TabIndex = 21;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.textFilePathInfo);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.btnFilePath);
            this.Controls.Add(this.clientLocationLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.runCupeButton);
            this.Controls.Add(this.runBomButton);
            this.Controls.Add(this.runITCapButton);
            this.Controls.Add(this.dateStartedLabel);
            this.Controls.Add(this.clientTypeLabel);
            this.Controls.Add(this.clientNameLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "StartPage";
            this.Text = "StartPage";
            this.Load += new System.EventHandler(this.StartPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.Label clientTypeLabel;
        private System.Windows.Forms.Label dateStartedLabel;
        private System.Windows.Forms.Button runITCapButton;
        private System.Windows.Forms.Button runBomButton;
        private System.Windows.Forms.Button runCupeButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label clientLocationLabel;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox textFilePathInfo;
    }
}