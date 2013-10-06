namespace IBMConsultantTool
{
    partial class CUPETool
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
            this.questionGrid = new System.Windows.Forms.DataGridView();
            this.Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numAnswers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addPersonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.questionGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // questionGrid
            // 
            this.questionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Header,
            this.TotalA,
            this.TotalB,
            this.TotalC,
            this.TotalD,
            this.numAnswers,
            this.avgScore});
            this.questionGrid.Location = new System.Drawing.Point(30, 73);
            this.questionGrid.Name = "questionGrid";
            this.questionGrid.Size = new System.Drawing.Size(744, 199);
            this.questionGrid.TabIndex = 0;
            // 
            // Header
            // 
            this.Header.Frozen = true;
            this.Header.HeaderText = "I =IT  B=Business";
            this.Header.Name = "Header";
            // 
            // TotalA
            // 
            this.TotalA.HeaderText = "Total A";
            this.TotalA.Name = "TotalA";
            this.TotalA.Width = 50;
            // 
            // TotalB
            // 
            this.TotalB.HeaderText = "Total B";
            this.TotalB.Name = "TotalB";
            this.TotalB.Width = 50;
            // 
            // TotalC
            // 
            this.TotalC.HeaderText = "Total C";
            this.TotalC.Name = "TotalC";
            this.TotalC.Width = 50;
            // 
            // TotalD
            // 
            this.TotalD.HeaderText = "Total D";
            this.TotalD.Name = "TotalD";
            this.TotalD.Width = 50;
            // 
            // numAnswers
            // 
            this.numAnswers.HeaderText = "Total Answers";
            this.numAnswers.Name = "numAnswers";
            this.numAnswers.Width = 60;
            // 
            // avgScore
            // 
            this.avgScore.HeaderText = "Average Score";
            this.avgScore.Name = "avgScore";
            this.avgScore.Width = 70;
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(623, 292);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(75, 23);
            this.addPersonButton.TabIndex = 1;
            this.addPersonButton.Text = "Add Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // CUPETool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 450);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.questionGrid);
            this.Name = "CUPETool";
            this.Text = "CUPETool";
            this.Load += new System.EventHandler(this.CUPETool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView questionGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalD;
        private System.Windows.Forms.DataGridViewTextBoxColumn numAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgScore;
        private System.Windows.Forms.Button addPersonButton;
    }
}