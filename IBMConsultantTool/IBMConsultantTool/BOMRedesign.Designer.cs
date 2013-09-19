namespace IBMConsultantTool
{
    partial class BOMRedesign
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
            this.AddBox = new System.Windows.Forms.Button();
            this.CategoryBox = new System.Windows.Forms.ComboBox();
            this.mainWorkspace = new System.Windows.Forms.Panel();
            this.addData = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downladToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addObjective = new System.Windows.Forms.Button();
            this.addInitivative = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.objectiveAdd = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainWorkspace.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddBox
            // 
            this.AddBox.Location = new System.Drawing.Point(737, 71);
            this.AddBox.Name = "AddBox";
            this.AddBox.Size = new System.Drawing.Size(75, 23);
            this.AddBox.TabIndex = 0;
            this.AddBox.Text = "Add ";
            this.AddBox.UseVisualStyleBackColor = true;
            this.AddBox.Click += new System.EventHandler(this.AddBox_Click);
            // 
            // CategoryBox
            // 
            this.CategoryBox.FormattingEnabled = true;
            this.CategoryBox.Items.AddRange(new object[] {
            "Products",
            "Catergory A",
            "Category B",
            "Category C"});
            this.CategoryBox.Location = new System.Drawing.Point(589, 73);
            this.CategoryBox.Name = "CategoryBox";
            this.CategoryBox.Size = new System.Drawing.Size(121, 21);
            this.CategoryBox.TabIndex = 1;
            this.CategoryBox.Text = "Select Category";
            // 
            // mainWorkspace
            // 
            this.mainWorkspace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainWorkspace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWorkspace.Controls.Add(this.panel1);
            this.mainWorkspace.Location = new System.Drawing.Point(12, 30);
            this.mainWorkspace.Name = "mainWorkspace";
            this.mainWorkspace.Size = new System.Drawing.Size(571, 281);
            this.mainWorkspace.TabIndex = 2;
            // 
            // addData
            // 
            this.addData.Location = new System.Drawing.Point(626, 123);
            this.addData.Name = "addData";
            this.addData.Size = new System.Drawing.Size(98, 23);
            this.addData.TabIndex = 3;
            this.addData.Text = "Add Data";
            this.addData.UseVisualStyleBackColor = true;
            this.addData.Click += new System.EventHandler(this.addData_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(824, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.tableToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(824, 24);
            this.menuStrip2.TabIndex = 5;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tableToolStripMenuItem
            // 
            this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            this.tableToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.tableToolStripMenuItem.Text = "Table";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.downladToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            // 
            // downladToolStripMenuItem
            // 
            this.downladToolStripMenuItem.Name = "downladToolStripMenuItem";
            this.downladToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downladToolStripMenuItem.Text = "Download";
            // 
            // addObjective
            // 
            this.addObjective.Location = new System.Drawing.Point(589, 169);
            this.addObjective.Name = "addObjective";
            this.addObjective.Size = new System.Drawing.Size(98, 23);
            this.addObjective.TabIndex = 6;
            this.addObjective.Text = "Add Objective";
            this.addObjective.UseVisualStyleBackColor = true;
            this.addObjective.Click += new System.EventHandler(this.addObjective_Click);
            // 
            // addInitivative
            // 
            this.addInitivative.Location = new System.Drawing.Point(626, 265);
            this.addInitivative.Name = "addInitivative";
            this.addInitivative.Size = new System.Drawing.Size(98, 23);
            this.addInitivative.TabIndex = 7;
            this.addInitivative.Text = "Add Initiative";
            this.addInitivative.UseVisualStyleBackColor = true;
            this.addInitivative.Click += new System.EventHandler(this.addInitivative_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(626, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 51);
            this.button1.TabIndex = 8;
            this.button1.Text = "Create Presentation Slide";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // objectiveAdd
            // 
            this.objectiveAdd.FormattingEnabled = true;
            this.objectiveAdd.Items.AddRange(new object[] {
            "Objective 1",
            "Objective 2",
            "Objective 3",
            "Objective 4"});
            this.objectiveAdd.Location = new System.Drawing.Point(703, 169);
            this.objectiveAdd.Name = "objectiveAdd";
            this.objectiveAdd.Size = new System.Drawing.Size(121, 21);
            this.objectiveAdd.TabIndex = 9;
            this.objectiveAdd.Text = "Add Objective";
            this.objectiveAdd.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Location = new System.Drawing.Point(272, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // BOMRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 375);
            this.Controls.Add(this.objectiveAdd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addInitivative);
            this.Controls.Add(this.addObjective);
            this.Controls.Add(this.addData);
            this.Controls.Add(this.mainWorkspace);
            this.Controls.Add(this.CategoryBox);
            this.Controls.Add(this.AddBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BOMRedesign";
            this.Text = "BOMRedesign";
            this.mainWorkspace.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddBox;
        private System.Windows.Forms.ComboBox CategoryBox;
        private System.Windows.Forms.Panel mainWorkspace;
        private System.Windows.Forms.Button addData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downladToolStripMenuItem;
        private System.Windows.Forms.Button addObjective;
        private System.Windows.Forms.Button addInitivative;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox objectiveAdd;
        private System.Windows.Forms.Panel panel1;

    }
}