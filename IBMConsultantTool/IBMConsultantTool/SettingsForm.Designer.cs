namespace IBMConsultantTool
{
    partial class SettingsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bomhighTextbox = new System.Windows.Forms.TextBox();
            this.bomlowTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.stdDeviationFlgAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dynamicSortRadio = new System.Windows.Forms.RadioButton();
            this.staticSortRadio = new System.Windows.Forms.RadioButton();
            this.dynamicAutoLowGaText = new System.Windows.Forms.TextBox();
            this.dynamicAutoHighGapText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.highGapThresholdText = new System.Windows.Forms.TextBox();
            this.lowGapThresholdText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(18, 18);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(548, 439);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bomhighTextbox);
            this.tabPage1.Controls.Add(this.bomlowTextbox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(540, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "BOM";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bomhighTextbox
            // 
            this.bomhighTextbox.Location = new System.Drawing.Point(301, 112);
            this.bomhighTextbox.Name = "bomhighTextbox";
            this.bomhighTextbox.Size = new System.Drawing.Size(70, 26);
            this.bomhighTextbox.TabIndex = 3;
            // 
            // bomlowTextbox
            // 
            this.bomlowTextbox.Location = new System.Drawing.Point(301, 38);
            this.bomlowTextbox.Name = "bomlowTextbox";
            this.bomlowTextbox.Size = new System.Drawing.Size(70, 26);
            this.bomlowTextbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "High Threshold";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Low Threshold";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(540, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CUPE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(316, 91);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(316, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 26);
            this.textBox3.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Auto High Gap Threshold";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Auto Low Gap Threshold";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.stdDeviationFlgAmount);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.dynamicSortRadio);
            this.tabPage3.Controls.Add(this.staticSortRadio);
            this.tabPage3.Controls.Add(this.dynamicAutoLowGaText);
            this.tabPage3.Controls.Add(this.dynamicAutoHighGapText);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.highGapThresholdText);
            this.tabPage3.Controls.Add(this.lowGapThresholdText);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(540, 406);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "IT Cap";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // stdDeviationFlgAmount
            // 
            this.stdDeviationFlgAmount.Location = new System.Drawing.Point(290, 338);
            this.stdDeviationFlgAmount.Name = "stdDeviationFlgAmount";
            this.stdDeviationFlgAmount.Size = new System.Drawing.Size(59, 26);
            this.stdDeviationFlgAmount.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 338);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(240, 20);
            this.label11.TabIndex = 17;
            this.label11.Text = "Standard Deviation Flag Amount";
            // 
            // label10
            // 
            this.label10.Image = global::IBMConsultantTool.Properties.Resources.blue_line;
            this.label10.Location = new System.Drawing.Point(3, 289);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(577, 20);
            this.label10.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.Image = global::IBMConsultantTool.Properties.Resources.blue_line;
            this.label9.Location = new System.Drawing.Point(-8, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(577, 20);
            this.label9.TabIndex = 15;
            // 
            // dynamicSortRadio
            // 
            this.dynamicSortRadio.AutoSize = true;
            this.dynamicSortRadio.Location = new System.Drawing.Point(33, 160);
            this.dynamicSortRadio.Name = "dynamicSortRadio";
            this.dynamicSortRadio.Size = new System.Drawing.Size(115, 17);
            this.dynamicSortRadio.TabIndex = 14;
            this.dynamicSortRadio.TabStop = true;
            this.dynamicSortRadio.Text = "Dynamic sort mode";
            this.dynamicSortRadio.UseVisualStyleBackColor = true;
            // 
            // staticSortRadio
            // 
            this.staticSortRadio.AutoSize = true;
            this.staticSortRadio.Location = new System.Drawing.Point(33, 14);
            this.staticSortRadio.Name = "staticSortRadio";
            this.staticSortRadio.Size = new System.Drawing.Size(101, 17);
            this.staticSortRadio.TabIndex = 13;
            this.staticSortRadio.TabStop = true;
            this.staticSortRadio.Text = "Static sort mode";
            this.staticSortRadio.UseVisualStyleBackColor = true;
            // 
            // dynamicAutoLowGaText
            // 
            this.dynamicAutoLowGaText.Location = new System.Drawing.Point(239, 242);
            this.dynamicAutoLowGaText.Name = "dynamicAutoLowGaText";
            this.dynamicAutoLowGaText.Size = new System.Drawing.Size(59, 26);
            this.dynamicAutoLowGaText.TabIndex = 12;
            // 
            // dynamicAutoHighGapText
            // 
            this.dynamicAutoHighGapText.Location = new System.Drawing.Point(239, 203);
            this.dynamicAutoHighGapText.Name = "dynamicAutoHighGapText";
            this.dynamicAutoHighGapText.Size = new System.Drawing.Size(59, 26);
            this.dynamicAutoHighGapText.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 203);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Auto High Gap Threshold";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 242);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Auto Low Gap Threshold";
            // 
            // highGapThresholdText
            // 
            this.highGapThresholdText.Location = new System.Drawing.Point(239, 96);
            this.highGapThresholdText.Name = "highGapThresholdText";
            this.highGapThresholdText.Size = new System.Drawing.Size(70, 26);
            this.highGapThresholdText.TabIndex = 7;
            // 
            // lowGapThresholdText
            // 
            this.lowGapThresholdText.Location = new System.Drawing.Point(239, 50);
            this.lowGapThresholdText.Name = "lowGapThresholdText";
            this.lowGapThresholdText.Size = new System.Drawing.Size(70, 26);
            this.lowGapThresholdText.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "High Gap Threshold";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 50);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Low  Gap Threshold";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(224, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 509);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox bomhighTextbox;
        private System.Windows.Forms.TextBox bomlowTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox dynamicAutoLowGaText;
        private System.Windows.Forms.TextBox dynamicAutoHighGapText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox highGapThresholdText;
        private System.Windows.Forms.TextBox lowGapThresholdText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton dynamicSortRadio;
        private System.Windows.Forms.RadioButton staticSortRadio;
        private System.Windows.Forms.TextBox stdDeviationFlgAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
    }
}