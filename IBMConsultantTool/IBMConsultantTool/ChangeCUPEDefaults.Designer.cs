﻿namespace IBMConsultantTool
{
    partial class ChangeCUPEDefaults
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
            this.CUPEQuestionDataGridView = new System.Windows.Forms.DataGridView();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.Default20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Default15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Default10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CUPEQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Utility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enabler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CUPEQuestionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CUPEQuestionDataGridView
            // 
            this.CUPEQuestionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CUPEQuestionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Default20,
            this.Default15,
            this.Default10,
            this.CUPEQuestion,
            this.Commodity,
            this.Utility,
            this.Partner,
            this.Enabler});
            this.CUPEQuestionDataGridView.Location = new System.Drawing.Point(12, 12);
            this.CUPEQuestionDataGridView.Name = "CUPEQuestionDataGridView";
            this.CUPEQuestionDataGridView.Size = new System.Drawing.Size(767, 440);
            this.CUPEQuestionDataGridView.TabIndex = 0;
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(343, 495);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(118, 23);
            this.SaveChangesButton.TabIndex = 1;
            this.SaveChangesButton.Text = "Save Changes";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // Default20
            // 
            this.Default20.HeaderText = "Default20";
            this.Default20.Name = "Default20";
            this.Default20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Default20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Default15
            // 
            this.Default15.HeaderText = "Default15";
            this.Default15.Name = "Default15";
            this.Default15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Default15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Default10
            // 
            this.Default10.HeaderText = "Default10";
            this.Default10.Name = "Default10";
            this.Default10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Default10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CUPEQuestion
            // 
            this.CUPEQuestion.HeaderText = "Question";
            this.CUPEQuestion.MinimumWidth = 50;
            this.CUPEQuestion.Name = "CUPEQuestion";
            this.CUPEQuestion.Width = 600;
            // 
            // Commodity
            // 
            this.Commodity.HeaderText = "Commodity";
            this.Commodity.MinimumWidth = 50;
            this.Commodity.Name = "Commodity";
            this.Commodity.Width = 600;
            // 
            // Utility
            // 
            this.Utility.HeaderText = "Utility";
            this.Utility.MinimumWidth = 50;
            this.Utility.Name = "Utility";
            this.Utility.Width = 600;
            // 
            // Partner
            // 
            this.Partner.HeaderText = "Partner";
            this.Partner.MinimumWidth = 50;
            this.Partner.Name = "Partner";
            this.Partner.Width = 600;
            // 
            // Enabler
            // 
            this.Enabler.HeaderText = "Enabler";
            this.Enabler.MinimumWidth = 50;
            this.Enabler.Name = "Enabler";
            this.Enabler.Width = 600;
            // 
            // ChangeCUPEDefaults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.CUPEQuestionDataGridView);
            this.Name = "ChangeCUPEDefaults";
            this.Text = "ChangeCUPEDefaults";
            ((System.ComponentModel.ISupportInitialize)(this.CUPEQuestionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CUPEQuestionDataGridView;
        private System.Windows.Forms.Button SaveChangesButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Default20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Default15;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Default10;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUPEQuestion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Commodity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Utility;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Enabler;
    }
}