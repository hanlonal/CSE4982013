namespace IBMConsultantTool
{
    partial class DataEntryForm
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.Initiative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Criticality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Differentiality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Effectiveness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Initiative,
            this.Criticality,
            this.Differentiality,
            this.Effectiveness});
            this.dataGrid.Location = new System.Drawing.Point(21, 49);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(460, 150);
            this.dataGrid.TabIndex = 0;
            // 
            // Initiative
            // 
            this.Initiative.HeaderText = "Initiative";
            this.Initiative.Name = "Initiative";
            // 
            // Criticality
            // 
            this.Criticality.HeaderText = "Criticality";
            this.Criticality.Name = "Criticality";
            // 
            // Differentiality
            // 
            this.Differentiality.HeaderText = "Differentiality";
            this.Differentiality.Name = "Differentiality";
            // 
            // Effectiveness
            // 
            this.Effectiveness.HeaderText = "Effectiveness";
            this.Effectiveness.Name = "Effectiveness";
            // 
            // DataEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 261);
            this.Controls.Add(this.dataGrid);
            this.Name = "DataEntryForm";
            this.Text = "DataEntryForm";
            this.Load += new System.EventHandler(this.DataEntryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Initiative;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Differentiality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Effectiveness;
    }
}