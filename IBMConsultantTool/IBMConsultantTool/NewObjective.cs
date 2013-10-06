using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class NewObjective : Panel
    {

        NewCategory owner;
        int ID;
        string name;
        private int labelHeight = 20;
        private int labelWidth = 50;
        private int baseHeight = 70;
        private int baseWidth = 150;

        public NewObjective(NewCategory owner, string name)
        {
            this.owner = owner;
            this.Location = FindLocation();
            this.name = name;
            owner.Owner.Controls.Add(this);
            MakeLabel();
            this.BackColor = Color.Teal;
            this.Height = baseHeight;
            this.Width = baseWidth;

        }


        private Point FindLocation()
        {
            Point p = new Point();
            // Console.WriteLine(owner.Width.ToString());

            p.X = baseWidth * (owner.Objectives.Count % 4);
            if (owner.Objectives.Count > 3)
                p.Y = owner.Objectives[owner.Objectives.Count - 4].Location.Y + baseHeight;
            else
                p.Y = 0;

            return p;
        }


        private void MakeLabel()
        {
            Label label = new Label();
            label.Text = name;
            label.BackColor = Color.Pink;
            label.ForeColor = Color.Black;
            label.Height = labelHeight;
            this.Controls.Add(label);
            label.Width = Parent.Width;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Location = new Point(0, 0);
            //label.BackColor = Color.Teal;

        }
    }
}
