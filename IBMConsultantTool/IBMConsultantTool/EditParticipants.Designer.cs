﻿namespace IBMConsultantTool
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
            this.ParticipantEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeBusiness = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TypeIT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ParticipantReceiveEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.participantsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // participantsGrid
            // 
            this.participantsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.participantsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.participantsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.participantsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParticipantEmail,
            this.TypeBusiness,
            this.TypeIT,
            this.ParticipantReceiveEmail,
            this.Id});
            this.participantsGrid.Location = new System.Drawing.Point(10, 11);
            this.participantsGrid.Name = "participantsGrid";
            this.participantsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.participantsGrid.Size = new System.Drawing.Size(251, 322);
            this.participantsGrid.TabIndex = 13;
            this.participantsGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.participantsGrid_RowsAdded);
            // 
            // SaveParticipantButton
            // 
            this.SaveParticipantButton.Location = new System.Drawing.Point(88, 338);
            this.SaveParticipantButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveParticipantButton.Name = "SaveParticipantButton";
            this.SaveParticipantButton.Size = new System.Drawing.Size(92, 28);
            this.SaveParticipantButton.TabIndex = 14;
            this.SaveParticipantButton.Text = "Save and Exit";
            this.SaveParticipantButton.UseVisualStyleBackColor = true;
            this.SaveParticipantButton.Click += new System.EventHandler(this.SaveParticipantButton_Click);
            // 
            // ParticipantEmail
            // 
            this.ParticipantEmail.HeaderText = "Email";
            this.ParticipantEmail.Name = "ParticipantEmail";
            this.ParticipantEmail.Visible = false;
            this.ParticipantEmail.Width = 150;
            // 
            // TypeBusiness
            // 
            this.TypeBusiness.FalseValue = "False";
            this.TypeBusiness.HeaderText = "Add Business";
            this.TypeBusiness.IndeterminateValue = "False";
            this.TypeBusiness.Name = "TypeBusiness";
            this.TypeBusiness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeBusiness.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TypeBusiness.TrueValue = "True";
            // 
            // TypeIT
            // 
            this.TypeIT.FalseValue = "False";
            this.TypeIT.HeaderText = "Add IT";
            this.TypeIT.IndeterminateValue = "False";
            this.TypeIT.Name = "TypeIT";
            this.TypeIT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeIT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TypeIT.TrueValue = "True";
            // 
            // ParticipantReceiveEmail
            // 
            this.ParticipantReceiveEmail.HeaderText = "Include In Email";
            this.ParticipantReceiveEmail.Name = "ParticipantReceiveEmail";
            this.ParticipantReceiveEmail.Visible = false;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // EditParticipants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 376);
            this.Controls.Add(this.SaveParticipantButton);
            this.Controls.Add(this.participantsGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EditParticipants";
            this.Text = "EditParticipants";
            ((System.ComponentModel.ISupportInitialize)(this.participantsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView participantsGrid;
        private System.Windows.Forms.Button SaveParticipantButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantEmail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TypeBusiness;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TypeIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantReceiveEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}