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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingDatabase));
            this.LoadingTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadingTextLabel
            // 
            this.LoadingTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadingTextLabel.AutoSize = true;
            this.LoadingTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadingTextLabel.Location = new System.Drawing.Point(13, 60);
            this.LoadingTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoadingTextLabel.Name = "LoadingTextLabel";
            this.LoadingTextLabel.Size = new System.Drawing.Size(154, 13);
            this.LoadingTextLabel.TabIndex = 0;
            this.LoadingTextLabel.Text = "Connecting to database...";
            this.LoadingTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(406, 234);
            this.Controls.Add(this.LoadingTextLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadingDatabase";
            this.Text = "Loading Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label LoadingTextLabel;
    }
}