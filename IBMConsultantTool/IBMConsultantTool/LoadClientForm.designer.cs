namespace IBMConsultantTool
{
    partial class LoadClientForm
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
            this.ChooseClientComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseClientLabel = new System.Windows.Forms.Label();
            this.LoadClientButton = new System.Windows.Forms.Button();
            this.LoadClientCancelButton = new System.Windows.Forms.Button();
            this.clientsgridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.clientsgridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ChooseClientComboBox
            // 
            this.ChooseClientComboBox.FormattingEnabled = true;
            this.ChooseClientComboBox.Location = new System.Drawing.Point(64, 96);
            this.ChooseClientComboBox.Name = "ChooseClientComboBox";
            this.ChooseClientComboBox.Size = new System.Drawing.Size(215, 21);
            this.ChooseClientComboBox.TabIndex = 0;
            // 
            // ChooseClientLabel
            // 
            this.ChooseClientLabel.AutoSize = true;
            this.ChooseClientLabel.Location = new System.Drawing.Point(131, 55);
            this.ChooseClientLabel.Name = "ChooseClientLabel";
            this.ChooseClientLabel.Size = new System.Drawing.Size(81, 13);
            this.ChooseClientLabel.TabIndex = 1;
            this.ChooseClientLabel.Text = "Choose a Client";
            // 
            // LoadClientButton
            // 
            this.LoadClientButton.Location = new System.Drawing.Point(219, 213);
            this.LoadClientButton.Name = "LoadClientButton";
            this.LoadClientButton.Size = new System.Drawing.Size(75, 23);
            this.LoadClientButton.TabIndex = 2;
            this.LoadClientButton.Text = "Load";
            this.LoadClientButton.UseVisualStyleBackColor = true;
            this.LoadClientButton.Click += new System.EventHandler(this.LoadClientButton_Click);
            // 
            // LoadClientCancelButton
            // 
            this.LoadClientCancelButton.Location = new System.Drawing.Point(73, 213);
            this.LoadClientCancelButton.Name = "LoadClientCancelButton";
            this.LoadClientCancelButton.Size = new System.Drawing.Size(75, 23);
            this.LoadClientCancelButton.TabIndex = 3;
            this.LoadClientCancelButton.Text = "Cancel";
            this.LoadClientCancelButton.UseVisualStyleBackColor = true;
            this.LoadClientCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // clientsgridView
            // 
            this.clientsgridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsgridView.Location = new System.Drawing.Point(22, 12);
            this.clientsgridView.Name = "clientsgridView";
            this.clientsgridView.Size = new System.Drawing.Size(292, 174);
            this.clientsgridView.TabIndex = 4;
            this.clientsgridView.Visible = false;
            // 
            // LoadClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 248);
            this.Controls.Add(this.LoadClientCancelButton);
            this.Controls.Add(this.LoadClientButton);
            this.Controls.Add(this.ChooseClientLabel);
            this.Controls.Add(this.ChooseClientComboBox);
            this.Controls.Add(this.clientsgridView);
            this.Name = "LoadClientForm";
            this.Text = "ChooseClient";
            ((System.ComponentModel.ISupportInitialize)(this.clientsgridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ChooseClientComboBox;
        private System.Windows.Forms.Label ChooseClientLabel;
        private System.Windows.Forms.Button LoadClientButton;
        private System.Windows.Forms.Button LoadClientCancelButton;
        private System.Windows.Forms.DataGridView clientsgridView;
    }
}