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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.clientLocationLabel = new System.Windows.Forms.Label();
            this.clientTypeLabel = new System.Windows.Forms.Label();
            this.dateStartedLabel = new System.Windows.Forms.Label();
            this.runITCapButton = new System.Windows.Forms.Button();
            this.runBomButton = new System.Windows.Forms.Button();
            this.runCupeButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1006, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newClientToolStripMenuItem,
            this.loadClientToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newClientToolStripMenuItem
            // 
            this.newClientToolStripMenuItem.Name = "newClientToolStripMenuItem";
            this.newClientToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.newClientToolStripMenuItem.Text = "New Client";
            // 
            // loadClientToolStripMenuItem
            // 
            this.loadClientToolStripMenuItem.Name = "loadClientToolStripMenuItem";
            this.loadClientToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.loadClientToolStripMenuItem.Text = "Load Client";
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Location = new System.Drawing.Point(379, 232);
            this.clientNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(119, 25);
            this.clientNameLabel.TabIndex = 1;
            this.clientNameLabel.Text = "Client Name";
            // 
            // clientLocationLabel
            // 
            this.clientLocationLabel.AutoSize = true;
            this.clientLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientLocationLabel.Location = new System.Drawing.Point(131, 221);
            this.clientLocationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientLocationLabel.Name = "clientLocationLabel";
            this.clientLocationLabel.Size = new System.Drawing.Size(159, 39);
            this.clientLocationLabel.TabIndex = 2;
            this.clientLocationLabel.Text = "Welcome";
            // 
            // clientTypeLabel
            // 
            this.clientTypeLabel.AutoSize = true;
            this.clientTypeLabel.Location = new System.Drawing.Point(596, 402);
            this.clientTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientTypeLabel.Name = "clientTypeLabel";
            this.clientTypeLabel.Size = new System.Drawing.Size(112, 25);
            this.clientTypeLabel.TabIndex = 3;
            this.clientTypeLabel.Text = "Client Type";
            // 
            // dateStartedLabel
            // 
            this.dateStartedLabel.AutoSize = true;
            this.dateStartedLabel.Location = new System.Drawing.Point(596, 322);
            this.dateStartedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateStartedLabel.Name = "dateStartedLabel";
            this.dateStartedLabel.Size = new System.Drawing.Size(121, 25);
            this.dateStartedLabel.TabIndex = 4;
            this.dateStartedLabel.Text = "Date Started";
            // 
            // runITCapButton
            // 
            this.runITCapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runITCapButton.Location = new System.Drawing.Point(123, 402);
            this.runITCapButton.Name = "runITCapButton";
            this.runITCapButton.Size = new System.Drawing.Size(186, 62);
            this.runITCapButton.TabIndex = 7;
            this.runITCapButton.Text = "IT Capability Assessment Tool";
            this.runITCapButton.UseVisualStyleBackColor = true;
            this.runITCapButton.Click += new System.EventHandler(this.runITCapButton_Click);
            // 
            // runBomButton
            // 
            this.runBomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBomButton.Location = new System.Drawing.Point(123, 304);
            this.runBomButton.Name = "runBomButton";
            this.runBomButton.Size = new System.Drawing.Size(186, 64);
            this.runBomButton.TabIndex = 8;
            this.runBomButton.Text = "Business Objective Management Tool";
            this.runBomButton.UseVisualStyleBackColor = true;
            this.runBomButton.Click += new System.EventHandler(this.runBomButton_Click);
            // 
            // runCupeButton
            // 
            this.runCupeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runCupeButton.Location = new System.Drawing.Point(123, 502);
            this.runCupeButton.Name = "runCupeButton";
            this.runCupeButton.Size = new System.Drawing.Size(186, 60);
            this.runCupeButton.TabIndex = 9;
            this.runCupeButton.Text = "IT Provider Relationship Tool";
            this.runCupeButton.UseVisualStyleBackColor = true;
            this.runCupeButton.Click += new System.EventHandler(this.runCupeButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 696);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1006, 25);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::IBMConsultantTool.Properties.Resources.startpage_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1006, 150);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.runCupeButton);
            this.Controls.Add(this.runBomButton);
            this.Controls.Add(this.runITCapButton);
            this.Controls.Add(this.dateStartedLabel);
            this.Controls.Add(this.clientTypeLabel);
            this.Controls.Add(this.clientLocationLabel);
            this.Controls.Add(this.clientNameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StartPage";
            this.Text = "StartPage";
            this.Load += new System.EventHandler(this.StartPage_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadClientToolStripMenuItem;
        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.Label clientLocationLabel;
        private System.Windows.Forms.Label clientTypeLabel;
        private System.Windows.Forms.Label dateStartedLabel;
        private System.Windows.Forms.Button runITCapButton;
        private System.Windows.Forms.Button runBomButton;
        private System.Windows.Forms.Button runCupeButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}