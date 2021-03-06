﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class NewObjective : Panel
    {

        public NewCategory owner;
        
        string name;
        private int labelHeight = 30;
        private int baseHeight = 90;
        private int baseWidth = 250;
        NewImperative currentImperative;

        

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
            imperativeLabel.Click +=new EventHandler(imperativeLabel_Click);
            imperativeToLabelDict[init] = imperativeLabel;
            imperativeLabel.MouseDown +=new MouseEventHandler(imperativeLabel_MouseDown);
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

        private void imperativeLabel_Click(object sender, EventArgs e)
        {
            owner.LastClicked = this;
            foreach (NewObjective o in owner.Objectives)
            {
                o.Refresh();
            }
        }

        private void imperativeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Label label = (Label)sender;
                foreach (NewImperative imp in this.imperatives)
                {
                    if (imp.Name == label.Text)
                        currentImperative = imp;
                }

                ContextMenuStrip strip = new ContextMenuStrip();
                ToolStripMenuItem delete = new ToolStripMenuItem();
                delete.Text = "Delete Imperative";
                delete.Click += new EventHandler(delete_Click);
                strip.Items.Add(delete);
                strip.Show(label, e.Location, ToolStripDropDownDirection.BelowRight);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(imperativeToLabelDict[currentImperative]);
            this.imperatives.Remove(currentImperative);
            ClientDataControl.db.RemoveBOM(currentImperative.Name, ClientDataControl.Client.EntityObject);
            if (imperatives.Count == 0)
            {
                owner.bomTool.RemoveObjective(this);
            }
            ReDrawImperatives(currentImperative);
            
        }

        private Point FindImperativeLocation()
        {

            Point p = new Point();

            p.Y = 0;
            bool isFound = true;
            while( isFound)
            {
                isFound = false;
                p.Y += 30;
                foreach (Label cont in Controls)
                {
                    if (p.Y == cont.Location.Y)
                    {
                        isFound = true;
                        break;
                    }
                }

            }
            p.X = 10;
            //p.Y = 30 + (Controls.Count - 1) * 30;
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
            int index = (owner.Objectives.Count % 3);
            p.X = baseWidth * (owner.Objectives.Count % 3);
            if (owner.Objectives.Count > 2)
            {
                owner.Heights[index] = owner.Objectives[owner.Objectives.Count - 3].Location.Y + owner.Objectives[owner.Objectives.Count - 3].Height;
                p.Y = (int)owner.Heights[index];
            }
            else
            {
                p.Y = 0;
                owner.Heights[index] = baseHeight;
            }


            return p;
        }

        public void ReDrawImperatives(NewImperative removedImp)
        {
            Point p = new Point(10, 30);
            foreach(Label lab in Controls)
            {
                if (lab.Location.Y == 0)
                {
                    continue;
                }
                lab.Location = p;
                p.Y += 30;
            }
            UpdateHeight();
            this.Refresh();
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
            label.Width = 250;
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
            //imperatives.Clear();
            //this.Controls.Clear();
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
                if(ConfigurationSettings.Instance.BOMSortModeStatic1 == true)
                    init.ChangeColor("differentiation");
                else
                    Console.WriteLine("dynamic mode");

                CheckColor(init);
            }
        }

        public void ColorByEffectiveness()
        {
            foreach (NewImperative init in imperatives)
            {
                if (ConfigurationSettings.Instance.BOMSortModeStatic1 == true)                
                    init.ChangeColor("effectiveness");
                else
                    Console.WriteLine("dynamic mode");

                CheckColor(init);
            }
        }

        public void ColorByCriticality()
        {
            foreach (NewImperative init in imperatives)
            {
                if (ConfigurationSettings.Instance.BOMSortModeStatic1 == true)
                    init.ChangeColor("criticality");
                else
                    Console.WriteLine("dynamic mode");

                CheckColor(init);
            }
        }

        public void ColorByBOMScore()
        {
            foreach (NewImperative init in imperatives)
            {
                if (ConfigurationSettings.Instance.BOMSortModeStatic1 == true)
                    init.ChangeColor("bomscore");
                else
                    Console.WriteLine("dynamic mode");

                CheckColor(init);
            }
        }

        private void CheckColor(NewImperative init)
        {
            if (init.ScoreState1 == NewImperative.ScoreState.None)
                imperativeToLabelDict[init].BackColor = Color.LightSlateGray;
            else if (init.ScoreState1 == NewImperative.ScoreState.High)
                imperativeToLabelDict[init].BackColor = Color.IndianRed;
            else if (init.ScoreState1 == NewImperative.ScoreState.Medium)
                imperativeToLabelDict[init].BackColor = Color.Yellow;
            else if (init.ScoreState1 == NewImperative.ScoreState.Low)
                imperativeToLabelDict[init].BackColor = Color.LawnGreen;
        }

        public List<NewImperative> Imperatives
        {
            get
            {
                return imperatives;
            }
        }

        public string ObjName
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
