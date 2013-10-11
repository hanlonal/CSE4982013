using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Drawing2D;

namespace IBMConsultantTool
{
    public partial class BOMBubbleChartRedesign : Form
    {
        //private Initiative initiative;
        private string name;
        private float criticality = 0;
        private float differentiation =0;
        private float effectiveness = 0;
        //private DataEntryForm mainForm;
        private BOMTool mainForm;

        private Random random = new Random();
        //private string color[rowCount];

        //ShapeContainer canvas = new ShapeContainer();
        
        private int circleCount = 0;
        private int MaxCount = 0;
        OvalShape[] circle = new OvalShape[1000];

        public BOMBubbleChartRedesign(BOMTool info)
        {
            mainForm = info;
            //canvas.Parent = this.panelChart;
            //CreateMyLabel();
            //VerticalLabel_SizeChanged(
            //GenerateText();
            MaxCount = Count();

            for (int i = 0; i < MaxCount; i++)
            {
                circle[i] = new OvalShape();
            }
            
            InitializeComponent();
            //GenerateText();
        }

        public int Count()
        {
            int count = 0;
            for (int i = 0; i < mainForm.Categories.Count; i++)
                for (int j = 0; j < mainForm.Categories[i].Objectives.Count; j++)
                    for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                        count++;
            return count;
        }

        /*private Label VerticalLabel = new Label();

        private void VerticalLabel_SizeChanged(object sender, PaintEventArgs e)
        {
            //GenerateText();

            var g = e.Graphics;
            g.DrawString("Ciriticality", new Font("Arial", 10), Brushes.Black, 0, 0, new StringFormat(StringFormatFlags.DirectionVertical));
            
        }

        public void CreateMyLabel()
        {
            Label labelCriticality = new Label();

            //labelCriticality.Parent = panelList;

            labelCriticality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //labelCriticality.ImageList = imageList1;
            labelCriticality.ImageIndex = 1;
            labelCriticality.ImageAlign = ContentAlignment.TopLeft;

            labelCriticality.UseMnemonic = true;
            labelCriticality.Text = "C\nr\ni\nt\ni\nc\na\nl\ni\nt\ny\n";
            labelCriticality.ForeColor = Color.Black;

            labelCriticality.Location = new Point(0, 0);
            labelCriticality.Size = new Size(labelCriticality.PreferredWidth, labelCriticality.PreferredHeight);
        }

        private void GenerateText()
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            Bitmap img = new Bitmap(VerticalLabel.Height, VerticalLabel.Width);
            //Bitmap img = new Bitmap(this.VerticalLabel.Height, this.VerticalLabel.Width);
            Graphics G = Graphics.FromImage(img);

            G.Clear(VerticalLabel.BackColor);

            SolidBrush brush_text = new SolidBrush(VerticalLabel.ForeColor);
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            G.DrawString(this.VerticalLabel.Text, VerticalLabel.Font, brush_text, new Rectangle(0, 0, img.Width, img.Height), format);
            brush_text.Dispose();

            img.RotateFlip(RotateFlipType.Rotate270FlipNone);

            
            VerticalLabel.BackgroundImage = img;
           // VerticalLabel.ResetText();
            VerticalLabel.Name = "Criticality";// VerticalLabel.ResetText();
            //img.Dispose();
            //VerticalLabel.Refresh();
        }*/

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            int cirCount = 0;

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = this.panelChart;
            
            if (mainForm == null)
            {
                this.Hide();
            }

            for (int i = 0; i < mainForm.Categories.Count; i++)
            {
                rowCount++;
                for (int j = 0; j < mainForm.Categories[i].Objectives.Count; j++)
                {
                    rowCount++;
                    for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                    {
                        name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                        criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                        differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                        effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                        int effectivenessBig = (int) (effectiveness * 2);
                        int newCriticality = (int)(criticality * 20);
                        int newDifferentiation = (int)(differentiation * 30);

                        circle[cirCount].Parent = canvas;

                        circle[cirCount].FillStyle = FillStyle.Solid;

                        circle[cirCount].FillColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));

                        circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                        circle[cirCount].AccessibleName = "(" + differentiation.ToString() + "," + criticality.ToString() + ",";

                        circle[cirCount].Visible = true;
                        circle[cirCount].Location = new System.Drawing.Point((60 + (int)(newDifferentiation) - (int)effectiveness), (320 - (int)(newCriticality) - (int)(effectiveness)));
                        circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);

                        circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseDown);
                        rowCount++;
                        circleCount++;
                        cirCount++;
                    }
                }
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save File";
            save.Filter = "Image files (*.jpeg)|*.jpeg| All Files (*.*)|*.*";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               // this.initiativeChart.SaveImage(File.Create(save.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void circle_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Click");
            for (int i = 0; i < circleCount; i++)
            {
                System.Diagnostics.Trace.WriteLine("In For Loop Click " + i);
                
                ///
                /// 
                ///
                if (circle[i].HitTest(MousePosition.X, MousePosition.Y))
                {
                    //System.Diagnostics.Trace.WriteLine("click circle!!");
                    //circle[i].AccessibleName = circle[i].Name;
                    //circle[i].ToString(circle[i].Width, circle[i].Height, circle[i].Size);

                    //string width = circle[i].Width.ToString();
                    circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName + (circle[i].Size.Height/2).ToString() + ")";
                
                    // Need to display the AccessibleDescription //

                    //MessageBox.Show(circle[i].AccessibleDescription);

                    System.Diagnostics.Trace.WriteLine(circle[i].AccessibleDescription);
                }
            }
        }
    }
}
