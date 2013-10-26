namespace IBMConsultantTool
{
    partial class ChangeITCAPDefaults
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
            this.ITCAPQuestionDataGridView = new System.Windows.Forms.DataGridView();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.ITCAPQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Default = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ITCAPQuestionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ITCAPQuestionDataGridView
            // 
            this.ITCAPQuestionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ITCAPQuestionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITCAPQuestion,
            this.Default});
            this.ITCAPQuestionDataGridView.Location = new System.Drawing.Point(5, 59);
            this.ITCAPQuestionDataGridView.Name = "ITCAPQuestionDataGridView";
            this.ITCAPQuestionDataGridView.Size = new System.Drawing.Size(767, 399);
            this.ITCAPQuestionDataGridView.TabIndex = 0;
            this.ITCAPQuestionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ITCAPQuestionDataGridView_CellContentClick);
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(316, 498);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(118, 23);
            this.SaveChangesButton.TabIndex = 1;
            this.SaveChangesButton.Text = "Save Changes";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // ITCAPQuestion
            // 
            this.ITCAPQuestion.HeaderText = "Name";
            this.ITCAPQuestion.MinimumWidth = 50;
            this.ITCAPQuestion.Name = "ITCAPQuestion";
            this.ITCAPQuestion.Width = 600;
            // 
            // Default
            // 
            this.Default.HeaderText = "Default";
            this.Default.Name = "Default";
            // 
            // ChangeITCAPDefaults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.ITCAPQuestionDataGridView);
            this.Name = "ChangeITCAPDefaults";
            this.Text = "ChangeITCAPDefaults";
            ((System.ComponentModel.ISupportInitialize)(this.ITCAPQuestionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ITCAPQuestionDataGridView;
        private System.Windows.Forms.Button SaveChangesButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITCAPQuestion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Default;
    }
}