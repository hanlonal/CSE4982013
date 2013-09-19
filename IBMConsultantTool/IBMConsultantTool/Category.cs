using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
    public class Category : Panel
    {
        private TreeView treeView;
       // private this this;
        private List<BusinessObjective> objectives;
        private BOMRedesign owner;
        //constructor
        public Category(BOMRedesign form, string name, Color color )
        {
            MakePanel(name, color);
            MakeTreeView();
            //this.DisplayRectangle.
            owner = form;


        }

        private void MakePanel(string name, Color color)
        {

            Label categoryLabel = new Label();
            categoryLabel.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.WhiteSmoke;
            categoryLabel.BackColor = this.BackColor;
            categoryLabel.Location = new System.Drawing.Point(0, 0);
            categoryLabel.Text = name;
            categoryLabel.ForeColor = Color.White;
            this.Controls.Add(categoryLabel);
            categoryLabel.Width = this.Width;
            categoryLabel.Click += new EventHandler(categoryLabel_Click);
            categoryLabel.MouseHover += new EventHandler(categoryLabel_MouseHover);
            /*
           // this = new this();
            this.Location = new System.Drawing.Point(0, 0);

            this.Size = new System.Drawing.Size(100, 100);
            this.Name = name;

            this.Visible = true;


            
           
            //this.Controls.Add(treeView);*/
        }

        public void categoryLabel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked on the label");
        }
        public void categoryLabel_MouseHover(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.Cursor = Cursors.Hand;
        }

        private void MakeTreeView()
        {
            treeView = new TreeView();
            treeView.Height = 100;
            treeView.Width = 100;
            this.Controls.Add(treeView);
            treeView.Location = new System.Drawing.Point(50, 50);
        }

        public void AddObjective()
        {
            Console.WriteLine("hello");
        }

        //public this Category

        
        public TreeView CategoryTreeView
        {
            get
            {
                return treeView;
            }

        }
    }
}
