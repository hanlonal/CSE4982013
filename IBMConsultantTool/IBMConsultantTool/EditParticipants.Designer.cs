namespace IBMConsultantTool
{
    partial class EditParticipants
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
            this.participantsGrid = new System.Windows.Forms.DataGridView();
            this.SaveParticipantButton = new System.Windows.Forms.Button();
            this.ParticipantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParticipantEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParticipantType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParticipantReceiveEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.participantsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // participantsGrid
            // 
            this.participantsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.participantsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.participantsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParticipantName,
            this.ParticipantEmail,
            this.ParticipantType,
            this.ParticipantReceiveEmail,
            this.Id});
            this.participantsGrid.Location = new System.Drawing.Point(13, 13);
            this.participantsGrid.Margin = new System.Windows.Forms.Padding(4);
            this.participantsGrid.Name = "participantsGrid";
            this.participantsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.participantsGrid.Size = new System.Drawing.Size(847, 405);
            this.participantsGrid.TabIndex = 13;
            this.participantsGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.participantsGrid_RowsAdded);
            // 
            // SaveParticipantButton
            // 
            this.SaveParticipantButton.Location = new System.Drawing.Point(737, 425);
            this.SaveParticipantButton.Name = "SaveParticipantButton";
            this.SaveParticipantButton.Size = new System.Drawing.Size(123, 34);
            this.SaveParticipantButton.TabIndex = 14;
            this.SaveParticipantButton.Text = "Save Changes";
            this.SaveParticipantButton.UseVisualStyleBackColor = true;
            this.SaveParticipantButton.Click += new System.EventHandler(this.SaveParticipantButton_Click);
            // 
            // ParticipantName
            // 
            this.ParticipantName.HeaderText = "Name";
            this.ParticipantName.Name = "ParticipantName";
            this.ParticipantName.Width = 150;
            // 
            // ParticipantEmail
            // 
            this.ParticipantEmail.HeaderText = "Email";
            this.ParticipantEmail.Name = "ParticipantEmail";
            this.ParticipantEmail.Width = 150;
            // 
            // ParticipantType
            // 
            this.ParticipantType.HeaderText = "Type";
            this.ParticipantType.Name = "ParticipantType";
            this.ParticipantType.Width = 50;
            // 
            // ParticipantReceiveEmail
            // 
            this.ParticipantReceiveEmail.HeaderText = "Include In Email";
            this.ParticipantReceiveEmail.Name = "ParticipantReceiveEmail";
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // EditParticipants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 471);
            this.Controls.Add(this.SaveParticipantButton);
            this.Controls.Add(this.participantsGrid);
            this.Name = "EditParticipants";
            this.Text = "EditParticipants";
            ((System.ComponentModel.ISupportInitialize)(this.participantsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView participantsGrid;
        private System.Windows.Forms.Button SaveParticipantButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantReceiveEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}