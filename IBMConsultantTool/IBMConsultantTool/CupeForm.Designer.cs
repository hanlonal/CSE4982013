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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.new5Question = new System.Windows.Forms.ToolStripMenuItem();
            this.new10Question = new System.Windows.Forms.ToolStripMenuItem();
            this.new20Question = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPersonButton = new System.Windows.Forms.Button();
            this.nextPersonButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
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
            this.personNameLabel.Location = new System.Drawing.Point(394, 24);
            this.personNameLabel.Name = "personNameLabel";
            this.personNameLabel.Size = new System.Drawing.Size(37, 15);
            this.personNameLabel.TabIndex = 1;
            this.personNameLabel.Text = "Name";
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(441, 115);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(75, 23);
            this.addPersonButton.TabIndex = 2;
            this.addPersonButton.Text = "New Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(558, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new5Question,
            this.new10Question,
            this.new20Question});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // new5Question
            // 
            this.new5Question.Name = "new5Question";
            this.new5Question.Size = new System.Drawing.Size(152, 22);
            this.new5Question.Text = "5";
            // 
            // new10Question
            // 
            this.new10Question.Name = "new10Question";
            this.new10Question.Size = new System.Drawing.Size(152, 22);
            this.new10Question.Text = "10";
            // 
            // new20Question
            // 
            this.new20Question.Name = "new20Question";
            this.new20Question.Size = new System.Drawing.Size(152, 22);
            this.new20Question.Text = "20";
            this.new20Question.Click += new System.EventHandler(this.new20Question_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // previousPersonButton
            // 
            this.previousPersonButton.Location = new System.Drawing.Point(87, 56);
            this.previousPersonButton.Name = "previousPersonButton";
            this.previousPersonButton.Size = new System.Drawing.Size(75, 23);
            this.previousPersonButton.TabIndex = 4;
            this.previousPersonButton.Text = "< --";
            this.previousPersonButton.UseVisualStyleBackColor = true;
            this.previousPersonButton.Click += new System.EventHandler(this.previousPersonButton_Click);
            // 
            // nextPersonButton
            // 
            this.nextPersonButton.Location = new System.Drawing.Point(290, 55);
            this.nextPersonButton.Name = "nextPersonButton";
            this.nextPersonButton.Size = new System.Drawing.Size(75, 23);
            this.nextPersonButton.TabIndex = 5;
            this.nextPersonButton.Text = "-- >";
            this.nextPersonButton.UseVisualStyleBackColor = true;
            this.nextPersonButton.Click += new System.EventHandler(this.nextPersonButton_Click);
            // 
            // CupeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 406);
            this.Controls.Add(this.nextPersonButton);
            this.Controls.Add(this.previousPersonButton);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.personNameLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CupeForm";
            this.Text = "CupeForm";
            this.Load += new System.EventHandler(this.CupeForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label personNameLabel;
        private System.Windows.Forms.Button addPersonButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem new5Question;
        private System.Windows.Forms.ToolStripMenuItem new10Question;
        private System.Windows.Forms.ToolStripMenuItem new20Question;
        private System.Windows.Forms.Button previousPersonButton;
        private System.Windows.Forms.Button nextPersonButton;
    }
}