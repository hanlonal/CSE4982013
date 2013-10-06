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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.questionGrid = new System.Windows.Forms.DataGridView();
            this.addPersonButton = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numAnswers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.questionGrid.Location = new System.Drawing.Point(45, 90);
            this.questionGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.questionGrid.Name = "questionGrid";
            this.questionGrid.Size = new System.Drawing.Size(768, 244);
            this.questionGrid.TabIndex = 0;
            this.questionGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionGrid_CellEndEdit);
            // 
            // addPersonButton
            // 
            this.addPersonButton.Location = new System.Drawing.Point(701, 363);
            this.addPersonButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addPersonButton.Name = "addPersonButton";
            this.addPersonButton.Size = new System.Drawing.Size(112, 28);
            this.addPersonButton.TabIndex = 1;
            this.addPersonButton.Text = "Add Person";
            this.addPersonButton.UseVisualStyleBackColor = true;
            this.addPersonButton.Click += new System.EventHandler(this.addPersonButton_Click);
            // 
            // Header
            // 
            this.Header.Frozen = true;
            this.Header.HeaderText = "I =IT  B=Business";
            this.Header.Name = "Header";
            this.Header.ReadOnly = true;
            // 
            // TotalA
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalA.DefaultCellStyle = dataGridViewCellStyle1;
            this.TotalA.HeaderText = "Total A";
            this.TotalA.Name = "TotalA";
            this.TotalA.ReadOnly = true;
            this.TotalA.Width = 50;
            // 
            // TotalB
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalB.DefaultCellStyle = dataGridViewCellStyle2;
            this.TotalB.HeaderText = "Total B";
            this.TotalB.Name = "TotalB";
            this.TotalB.ReadOnly = true;
            this.TotalB.Width = 50;
            // 
            // TotalC
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalC.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalC.HeaderText = "Total C";
            this.TotalC.Name = "TotalC";
            this.TotalC.ReadOnly = true;
            this.TotalC.Width = 50;
            // 
            // TotalD
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalD.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalD.HeaderText = "Total D";
            this.TotalD.Name = "TotalD";
            this.TotalD.ReadOnly = true;
            this.TotalD.Width = 50;
            // 
            // numAnswers
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAnswers.DefaultCellStyle = dataGridViewCellStyle5;
            this.numAnswers.HeaderText = "Total Answers";
            this.numAnswers.Name = "numAnswers";
            this.numAnswers.ReadOnly = true;
            this.numAnswers.Width = 60;
            // 
            // avgScore
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgScore.DefaultCellStyle = dataGridViewCellStyle6;
            this.avgScore.HeaderText = "Average Score";
            this.avgScore.Name = "avgScore";
            this.avgScore.ReadOnly = true;
            this.avgScore.Width = 70;
            // 
            // CUPETool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 554);
            this.Controls.Add(this.addPersonButton);
            this.Controls.Add(this.questionGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CUPETool";
            this.Text = "CUPETool";
            this.Load += new System.EventHandler(this.CUPETool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView questionGrid;
        private System.Windows.Forms.Button addPersonButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalD;
        private System.Windows.Forms.DataGridViewTextBoxColumn numAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgScore;
    }
}