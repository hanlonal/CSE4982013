namespace IBMConsultantTool
{
    partial class LoadingDatabase
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
            this.LoadingTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadingTextLabel
            // 
            this.LoadingTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadingTextLabel.AutoSize = true;
            this.LoadingTextLabel.Location = new System.Drawing.Point(12, 73);
            this.LoadingTextLabel.Name = "LoadingTextLabel";
            this.LoadingTextLabel.Size = new System.Drawing.Size(129, 13);
            this.LoadingTextLabel.TabIndex = 0;
            this.LoadingTextLabel.Text = "Connecting to database...";
            this.LoadingTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 168);
            this.Controls.Add(this.LoadingTextLabel);
            this.Name = "LoadingDatabase";
            this.Text = "Loading Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label LoadingTextLabel;
    }
}