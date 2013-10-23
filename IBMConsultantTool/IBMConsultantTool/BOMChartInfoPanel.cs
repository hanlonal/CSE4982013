using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class BOMChartInfoPanel : Panel
    {
        Label critLabel = new Label();
        Label effectLabel = new Label();
        Label diffLabel = new Label();
        Label nameLabel = new Label();

        Label critValueLabel = new Label();
        Label diffValueLabel = new Label();
        Label effectValueLabel = new Label();
        public BOMChartInfoPanel()
        {

            critLabel.Text = "Criticality: ";
            effectLabel.Text = "Effectivness: ";
            diffLabel.Text = "Differentiation: ";
            critLabel.Width = 120;
            effectLabel.Width = 120;
            diffLabel.Width = 130;
            this.BackColor = Color.LightGray;
            this.Width = 200;
            this.Height = 200;
            this.Controls.Add(critLabel);
            this.Controls.Add(diffLabel);
            this.Controls.Add(effectLabel);
            this.Controls.Add(critValueLabel);
            this.Controls.Add(effectValueLabel);
            this.Controls.Add(diffValueLabel);
            diffLabel.Location = new Point(10, 30);
            critLabel.Location = new Point(10, 50);
            effectLabel.Location = new Point(10, 70);
            diffValueLabel.Location = new Point(160, 30);
            critValueLabel.Location = new Point(160, 50);
            effectValueLabel.Location = new Point(160, 70);

        }


        public string SetDiffValue
        {
            set { diffValueLabel.Text = value; }
        }
        public string SetCritValue
        {
            set { critValueLabel.Text = value; }
        }
        public string SetEffectValue
        {
            set { effectValueLabel.Text = value; }
        }
    }
}
