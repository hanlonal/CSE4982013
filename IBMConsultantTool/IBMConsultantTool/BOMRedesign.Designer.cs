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
            this.SuspendLayout();
            // 
            // AddBox
            // 
            this.AddBox.Location = new System.Drawing.Point(626, 30);
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
            this.CategoryBox.SelectedIndexChanged += new System.EventHandler(this.CategoryBox_SelectedIndexChanged);
            // 
            // mainWorkspace
            // 
            this.mainWorkspace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainWorkspace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWorkspace.Location = new System.Drawing.Point(12, 30);
            this.mainWorkspace.Name = "mainWorkspace";
            this.mainWorkspace.Size = new System.Drawing.Size(563, 204);
            this.mainWorkspace.TabIndex = 2;
            // 
            // BOMRedesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 261);
            this.Controls.Add(this.mainWorkspace);
            this.Controls.Add(this.CategoryBox);
            this.Controls.Add(this.AddBox);
            this.Name = "BOMRedesign";
            this.Text = "BOMRedesign";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddBox;
        private System.Windows.Forms.ComboBox CategoryBox;
        private System.Windows.Forms.Panel mainWorkspace;

    }
}