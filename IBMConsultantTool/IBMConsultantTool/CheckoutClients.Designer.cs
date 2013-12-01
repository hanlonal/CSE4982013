namespace IBMConsultantTool
{
    partial class CheckoutClients
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
            this.ClientDataGridView = new System.Windows.Forms.DataGridView();
            this.SaveChanges = new System.Windows.Forms.Button();
            this.CheckedOut = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ClientDataGridView
            // 
            this.ClientDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckedOut,
            this.Client});
            this.ClientDataGridView.Location = new System.Drawing.Point(12, 12);
            this.ClientDataGridView.Name = "ClientDataGridView";
            this.ClientDataGridView.Size = new System.Drawing.Size(593, 372);
            this.ClientDataGridView.TabIndex = 0;
            // 
            // SaveChanges
            // 
            this.SaveChanges.Location = new System.Drawing.Point(259, 410);
            this.SaveChanges.Name = "SaveChanges";
            this.SaveChanges.Size = new System.Drawing.Size(89, 23);
            this.SaveChanges.TabIndex = 1;
            this.SaveChanges.Text = "Save Changes";
            this.SaveChanges.UseVisualStyleBackColor = true;
            this.SaveChanges.Click += new System.EventHandler(this.SaveChanges_Click);
            // 
            // CheckedOut
            // 
            this.CheckedOut.HeaderText = "CheckedOut";
            this.CheckedOut.Name = "CheckedOut";
            // 
            // Client
            // 
            this.Client.HeaderText = "Client";
            this.Client.Name = "Client";
            this.Client.ReadOnly = true;
            this.Client.Width = 450;
            // 
            // CheckoutClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 445);
            this.Controls.Add(this.SaveChanges);
            this.Controls.Add(this.ClientDataGridView);
            this.Name = "CheckoutClients";
            this.Text = "CheckoutClients";
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ClientDataGridView;
        private System.Windows.Forms.Button SaveChanges;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckedOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
    }
}