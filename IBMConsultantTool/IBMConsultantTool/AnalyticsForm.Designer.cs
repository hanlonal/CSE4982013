namespace IBMConsultantTool
{
    partial class AnalyticsForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fromDateText = new System.Windows.Forms.TextBox();
            this.analyticsListBox = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.firstLevelComboBox = new System.Windows.Forms.ComboBox();
            this.secondLevelComboBox = new System.Windows.Forms.ComboBox();
            this.showResultsButton = new System.Windows.Forms.Button();
            this.domainsComboBox = new System.Windows.Forms.ComboBox();
            this.fromDateCheckBox = new System.Windows.Forms.CheckBox();
            this.toDateCheckBox = new System.Windows.Forms.CheckBox();
            this.regionCheckBox = new System.Windows.Forms.CheckBox();
            this.typeCheckBox = new System.Windows.Forms.CheckBox();
            this.metricCheckBox = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.capabilitiesComboBox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Location = new System.Drawing.Point(44, 614);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(184, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "<To Date>";
            // 
            // fromDateText
            // 
            this.fromDateText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fromDateText.Location = new System.Drawing.Point(44, 557);
            this.fromDateText.Name = "fromDateText";
            this.fromDateText.ReadOnly = true;
            this.fromDateText.Size = new System.Drawing.Size(184, 26);
            this.fromDateText.TabIndex = 1;
            this.fromDateText.Text = "<From Date>";
            // 
            // analyticsListBox
            // 
            this.analyticsListBox.FormattingEnabled = true;
            this.analyticsListBox.ItemHeight = 20;
            this.analyticsListBox.Items.AddRange(new object[] {
            "Capabilities",
            "Initiatives",
            "CUPE Questions",
            "Objectives",
            "IT Attribues"});
            this.analyticsListBox.Location = new System.Drawing.Point(7, 3);
            this.analyticsListBox.Name = "analyticsListBox";
            this.analyticsListBox.Size = new System.Drawing.Size(130, 144);
            this.analyticsListBox.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(44, 356);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 28);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "<Metric>";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.capabilitiesComboBox);
            this.panel1.Controls.Add(this.firstLevelComboBox);
            this.panel1.Controls.Add(this.secondLevelComboBox);
            this.panel1.Controls.Add(this.showResultsButton);
            this.panel1.Controls.Add(this.domainsComboBox);
            this.panel1.Controls.Add(this.fromDateCheckBox);
            this.panel1.Controls.Add(this.toDateCheckBox);
            this.panel1.Controls.Add(this.regionCheckBox);
            this.panel1.Controls.Add(this.typeCheckBox);
            this.panel1.Controls.Add(this.metricCheckBox);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.analyticsListBox);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.fromDateText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 705);
            this.panel1.TabIndex = 6;
            // 
            // firstLevelComboBox
            // 
            this.firstLevelComboBox.FormattingEnabled = true;
            this.firstLevelComboBox.Location = new System.Drawing.Point(44, 168);
            this.firstLevelComboBox.Name = "firstLevelComboBox";
            this.firstLevelComboBox.Size = new System.Drawing.Size(184, 28);
            this.firstLevelComboBox.TabIndex = 16;
            this.firstLevelComboBox.Text = "<Choose>";
            // 
            // secondLevelComboBox
            // 
            this.secondLevelComboBox.FormattingEnabled = true;
            this.secondLevelComboBox.Location = new System.Drawing.Point(44, 202);
            this.secondLevelComboBox.Name = "secondLevelComboBox";
            this.secondLevelComboBox.Size = new System.Drawing.Size(184, 28);
            this.secondLevelComboBox.TabIndex = 15;
            this.secondLevelComboBox.Text = "<Choose>";
            // 
            // showResultsButton
            // 
            this.showResultsButton.Location = new System.Drawing.Point(44, 662);
            this.showResultsButton.Name = "showResultsButton";
            this.showResultsButton.Size = new System.Drawing.Size(139, 31);
            this.showResultsButton.TabIndex = 14;
            this.showResultsButton.Text = "Show Results";
            this.showResultsButton.UseVisualStyleBackColor = true;
            // 
            // domainsComboBox
            // 
            this.domainsComboBox.FormattingEnabled = true;
            this.domainsComboBox.Location = new System.Drawing.Point(44, 236);
            this.domainsComboBox.Name = "domainsComboBox";
            this.domainsComboBox.Size = new System.Drawing.Size(184, 28);
            this.domainsComboBox.TabIndex = 12;
            this.domainsComboBox.Text = "<Choose>";
            // 
            // fromDateCheckBox
            // 
            this.fromDateCheckBox.AutoSize = true;
            this.fromDateCheckBox.Location = new System.Drawing.Point(12, 557);
            this.fromDateCheckBox.Name = "fromDateCheckBox";
            this.fromDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.fromDateCheckBox.TabIndex = 11;
            this.fromDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // toDateCheckBox
            // 
            this.toDateCheckBox.AutoSize = true;
            this.toDateCheckBox.Location = new System.Drawing.Point(12, 614);
            this.toDateCheckBox.Name = "toDateCheckBox";
            this.toDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.toDateCheckBox.TabIndex = 10;
            this.toDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // regionCheckBox
            // 
            this.regionCheckBox.AutoSize = true;
            this.regionCheckBox.Location = new System.Drawing.Point(12, 433);
            this.regionCheckBox.Name = "regionCheckBox";
            this.regionCheckBox.Size = new System.Drawing.Size(15, 14);
            this.regionCheckBox.TabIndex = 9;
            this.regionCheckBox.UseVisualStyleBackColor = true;
            // 
            // typeCheckBox
            // 
            this.typeCheckBox.AutoSize = true;
            this.typeCheckBox.Location = new System.Drawing.Point(12, 499);
            this.typeCheckBox.Name = "typeCheckBox";
            this.typeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.typeCheckBox.TabIndex = 8;
            this.typeCheckBox.UseVisualStyleBackColor = true;
            // 
            // metricCheckBox
            // 
            this.metricCheckBox.AutoSize = true;
            this.metricCheckBox.Location = new System.Drawing.Point(12, 356);
            this.metricCheckBox.Name = "metricCheckBox";
            this.metricCheckBox.Size = new System.Drawing.Size(15, 14);
            this.metricCheckBox.TabIndex = 0;
            this.metricCheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(44, 433);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(184, 28);
            this.comboBox3.TabIndex = 7;
            this.comboBox3.Text = "<Region>";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(44, 499);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(184, 28);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.Text = "<Business Type>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // dataPanel
            // 
            this.dataPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataPanel.Location = new System.Drawing.Point(237, 27);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(737, 298);
            this.dataPanel.TabIndex = 8;
            // 
            // chartPanel
            // 
            this.chartPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chartPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chartPanel.Location = new System.Drawing.Point(237, 331);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(737, 386);
            this.chartPanel.TabIndex = 9;
            // 
            // capabilitiesComboBox
            // 
            this.capabilitiesComboBox.FormattingEnabled = true;
            this.capabilitiesComboBox.Location = new System.Drawing.Point(44, 273);
            this.capabilitiesComboBox.Name = "capabilitiesComboBox";
            this.capabilitiesComboBox.Size = new System.Drawing.Size(184, 28);
            this.capabilitiesComboBox.TabIndex = 17;
            this.capabilitiesComboBox.Text = "<Choose>";
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.dataPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AnalyticsForm";
            this.Text = "AnalyticsForm";
            this.Load += new System.EventHandler(this.AnalyticsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox fromDateText;
        private System.Windows.Forms.ListBox analyticsListBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.CheckBox fromDateCheckBox;
        private System.Windows.Forms.CheckBox toDateCheckBox;
        private System.Windows.Forms.CheckBox regionCheckBox;
        private System.Windows.Forms.CheckBox typeCheckBox;
        private System.Windows.Forms.CheckBox metricCheckBox;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.Button showResultsButton;
        private System.Windows.Forms.ComboBox domainsComboBox;
        private System.Windows.Forms.ComboBox firstLevelComboBox;
        private System.Windows.Forms.ComboBox secondLevelComboBox;
        private System.Windows.Forms.ComboBox capabilitiesComboBox;
    }
}