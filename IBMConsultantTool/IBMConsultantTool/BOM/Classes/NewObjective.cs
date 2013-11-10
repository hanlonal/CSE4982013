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

        

        List<NewImperative> imperatives = new List<NewImperative>();
        private Dictionary<NewImperative, Label> imperativeToLabelDict = new Dictionary<NewImperative, Label>();
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
            //this.Paint +=new PaintEventHandler(NewObjective_Paint);
        }

        public void NewObjective_Click(object sender, EventArgs e)
        {
            NewObjective obj = (NewObjective)sender;
            //Console.WriteLine(obj.name);
            owner.LastClicked = obj;

            foreach (NewObjective o in owner.Objectives)
            {
                o.Refresh();
            }
            
           //owner.ObjectiveClicked(obj);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (owner.LastClicked == this)
            {

                e.Graphics.DrawRectangle(Pens.Yellow,
                e.ClipRectangle.Left,
                e.ClipRectangle.Top,
                e.ClipRectangle.Width - 1,
                e.ClipRectangle.Height - 1);
            }
            
            base.OnPaint(e);
        }

        public NewImperative AddImperative(string name)
        {
            NewImperative init = new NewImperative(this, name);

            MakeImperativeLabel(init);

            imperatives.Add(init);
            return init;
           // init.Name = name;
        }

        private void MakeImperativeLabel(NewImperative init)
        {
            Label imperativeLabel = new Label();
            imperativeToLabelDict[init] = imperativeLabel;
            imperativeLabel.Location = FindImperativeLocation();
            imperativeLabel.Height = baseHeight;
            imperativeLabel.BackColor = Color.White;
            imperativeLabel.AutoEllipsis = true;
            imperativeLabel.Text = init.Name;

            Controls.Add(imperativeLabel);

            imperativeLabel.Width = owner.Width;
            imperativeLabel.Height = 25;

            imperativeLabel.BorderStyle = BorderStyle.FixedSingle;

        }

        private Point FindImperativeLocation()
        {

            Point p = new Point();

            p.X = 10;
            p.Y = 30 + (Imperatives.Count) * 30;
            UpdateHeight();
            return p;
        }


        public void UpdateHeight()
        {
            this.Height = baseHeight + 30*imperatives.Count;
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

                ToolStripMenuItem detailView = new ToolStripMenuItem();
                detailView.Click += new EventHandler(detailView_Click);
                detailView.Text = "Detailed View";
                strip.Items.Add(detailView);
                strip.Show(this, e.Location, ToolStripDropDownDirection.BelowRight);


            }
        }


        private void detailView_Click(object sender, EventArgs e)
        {
            DetailedBOMViewForm form = new DetailedBOMViewForm(this);

            form.Show();
        }

        private void deleteObj_Click(object sender, EventArgs e)
        {
            imperatives.Clear();
            this.Controls.Clear();
            owner.RemoveObjective(this);
        
        
        }

        private void colorObj_Click(object sender, EventArgs e)
        {
            owner.ChooseColor(this);
        }

        public void ColorByDifferentiation()
        {
            foreach (NewImperative init in imperatives)
            {
                init.ChangeColor("differentiation");
                CheckColor(init);
            }
        }

        public void ColorByEffectiveness()
        {
            foreach (NewImperative init in imperatives)
            {
                init.ChangeColor("effectiveness");
                CheckColor(init);
            }
        }

        public void ColorByCriticality()
        {
            foreach (NewImperative init in imperatives)
            {
                init.ChangeColor("criticality");
                CheckColor(init);
            }
        }

        public void ColorByBOMScore()
        {
            foreach (NewImperative init in imperatives)
            {
                init.ChangeColor("bomscore");
                CheckColor(init);
            }
        }

        private void CheckColor(NewImperative init)
        {
            if (init.ScoreState1 == NewImperative.ScoreState.None)
                imperativeToLabelDict[init].BackColor = Color.LightSlateGray;
            else if (init.ScoreState1 == NewImperative.ScoreState.High)
                imperativeToLabelDict[init].BackColor = Color.LawnGreen;
            else if (init.ScoreState1 == NewImperative.ScoreState.Medium)
                imperativeToLabelDict[init].BackColor = Color.Yellow;
            else if (init.ScoreState1 == NewImperative.ScoreState.Low)
                imperativeToLabelDict[init].BackColor = Color.Red;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<NewImperative> Imperatives
        {
            get
            {
                return imperatives;
            }
        }
    }
}
