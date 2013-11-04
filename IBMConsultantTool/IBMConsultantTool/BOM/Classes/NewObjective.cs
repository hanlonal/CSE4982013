using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class NewObjective : Panel
    {

        NewCategory owner;
        
        int ID;
        string name;
        private int labelHeight = 30;
        private int labelWidth = 50;
        private int baseHeight = 90;
        private int baseWidth = 200;

        List<NewInitiative> initiatives = new List<NewInitiative>();

        public NewObjective(NewCategory owner, string name)
        {
            this.owner = owner;
            this.Location = FindLocation();
            this.name = name;
            owner.Owner.Controls.Add(this);
            MakeLabel();
            this.Click += new EventHandler(NewObjective_Click);
            this.BackColor = Color.LightBlue;
            this.Height = baseHeight;
            this.Width = baseWidth;
            this.BorderStyle = BorderStyle.FixedSingle;

        }

        public void NewObjective_Click(object sender, EventArgs e)
        {
            NewObjective obj = (NewObjective)sender;
            //Console.WriteLine(obj.name);
            owner.LastClicked = obj;
            //owner.ObjectiveClicked(obj);
        }
        public NewInitiative AddInitiative(string name)
        {
            NewInitiative init = new NewInitiative(this, name);
            initiatives.Add(init);
            return init;
           // init.Name = name;
        }

        public void UpdateHeight()
        {
            this.Height = baseHeight + 30*initiatives.Count;
        }

        private Point FindLocation()
        {
            Point p = new Point();
            // Console.WriteLine(owner.Width.ToString());
            int index = (owner.Objectives.Count % 4);
            p.X = baseWidth * (owner.Objectives.Count % 4);
            if (owner.Objectives.Count > 3)
            {
                owner.Heights[index] = owner.Objectives[owner.Objectives.Count - 4].Location.Y + owner.Objectives[owner.Objectives.Count - 4].Height;
                p.Y = (int)owner.Heights[index];
            }
            else
            {
                p.Y = 0;
                owner.Heights[index] = baseHeight;
            }


            return p;
        }


        private void MakeLabel()
        {
            Label label = new Label();
            label.MouseDown +=new MouseEventHandler(label_MouseDown);
            label.Text = name;
            label.BackColor = Color.LightGray;
            label.ForeColor = Color.Black;
            label.Height = labelHeight;
            this.Controls.Add(label);
            label.Width = label.Parent.Width;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Location = new Point(0, 0);
            //label.BackColor = Color.Teal;
            

        }

        private void label_MouseDown(object sender, MouseEventArgs e)           
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip strip = new ContextMenuStrip();
                ToolStripMenuItem deleteObj = new ToolStripMenuItem();
                deleteObj.Click += new EventHandler(deleteObj_Click);
                deleteObj.Text = "Remove Objective";
                strip.Items.Add(deleteObj);

                ToolStripMenuItem colorObj = new ToolStripMenuItem();
                colorObj.Click += new EventHandler(colorObj_Click);
                colorObj.Text = "Objective Color";
                strip.Items.Add(colorObj);
                strip.Show(this, e.Location, ToolStripDropDownDirection.BelowRight);
            }
        }

        private void deleteObj_Click(object sender, EventArgs e)
        {
            initiatives.Clear();
            this.Controls.Clear();
            owner.RemoveObjective(this);
        
        
        }

        private void colorObj_Click(object sender, EventArgs e)
        {
            owner.ChooseColor(this);
        }

        public void ColorByDifferentiation()
        {
            foreach (NewInitiative init in initiatives)
            {
                init.ChangeColor("differentiation");
            }
        }

        public void ColorByEffectiveness()
        {
            foreach (NewInitiative init in initiatives)
            {
                init.ChangeColor("effectiveness");
            }
        }

        public void ColorByCriticality()
        {
            foreach (NewInitiative init in initiatives)
            {
                init.ChangeColor("criticality");
            }
        }

        public void ColorByBOMScore()
        {
            foreach (NewInitiative init in initiatives)
            {
                init.ChangeColor("bomscore");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<NewInitiative> Initiatives
        {
            get
            {
                return initiatives;
            }
        }
    }
}
