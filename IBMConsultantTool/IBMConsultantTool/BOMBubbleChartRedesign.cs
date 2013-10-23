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
        //private BOMChartInfoPanel panel;

        private Random random = new Random();
        //private string color[rowCount];

        //ShapeContainer canvas = new ShapeContainer();
        
        private int circleCount = 0;
        private int MaxCount = 0;
        private int labelCount = 0;
        OvalShape[] circle = new OvalShape[1000];
        Label[] labelInfo = new Label[1000];
        CheckBox[] objectivesCheckBox = new CheckBox[1000];
        Label labelDescription = new Label();
        private int objCount = 0;
        Label[] categoryLabel = new Label[1000];
        //Color[] color = "Color.AliceBlue;
        
        private bool mouseDown = false;
        private bool[] labelHide = new bool[1000];

        public BOMBubbleChartRedesign(BOMTool info)
        {
            //panel = new BOMChartInfoPanel();
            /*Size max = SystemInformation.MaxWindowTrackSize;

            if (height > max.Height)
                height = max.Height;
            if (width > max.Width)
                width = max.Width;*/

            if (this.WindowState == FormWindowState.Normal && (base.Height != Height || base.Width != Width))
            {
                Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
                if (Height > maxWindowTrackSize.Height)
                    Height = maxWindowTrackSize.Height;
                if (Width > maxWindowTrackSize.Width)
                    Width = maxWindowTrackSize.Width;
            }
            mainForm = info;
            //canvas.Parent = this.panelChart;
            //CreateMyLabel();
            //VerticalLabel_SizeChanged(
            //GenerateText();
            MaxCount = Count();
            
            for (int i = 0; i < MaxCount; i++)
            {
                circle[i] = new OvalShape();
                labelInfo[i] = new Label();
                objectivesCheckBox[i] = new CheckBox();
                categoryLabel[i] = new Label();
                labelHide[i] = false;
            }

           /* for (int j = 0; j < MaxCount; j++)
            {
                labelInfo[j] = new Label();

              //  labelInfo[j].Parent = this.panelChart;
            }

            for (int k = 0; k < MaxCount; k++)
            {
                objectivesCheckBox[k] = new CheckBox();
            }*/
            //this.lineAxisX.BringToFront();
            //this.lineAxisY.BringToFront();
            
            InitializeComponent();
            //GenerateText();
        }

        public void Initialize()
        {
            MaxCount = Count();

            for (int i = 0; i < MaxCount; i++)
            {
                circle[i] = new OvalShape();
                labelInfo[i] = new Label();
                objectivesCheckBox[i] = new CheckBox();
                categoryLabel[i] = new Label();
                labelHide[i] = false;
            }
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

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            int cirCount = 0;
            int objectivesCount = 0;
            mouseDown = false;
            
            //System.Diagnostics.Trace.WriteLine("Load chart: " + labelCount.ToString());

            //labelDescription.ResetText();
            for (int i=0; i<circleCount; i++)
            {
                if(labelInfo[i] != null)
                    labelInfo[i].ResetText();
            }

            for (int i = 0; i < circleCount; i++)
            {
                if (circle[i] != null)
                {
                    circle[i].Hide();
                    circle[i] = new OvalShape();
                }
            }
            //Initialize();
            circleCount = 0;
            labelCount = 0;

            //this.panelChart.

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
                    //objectivesCheckBox[objectivesCount].CheckedChanged += new EventHandler(BOMBubbleChartRedesign_CheckedChanged);
                    //objectivesCheckBox[objectivesCount].CheckedChanged
                    //BOMBubbleChartRedesign_CheckedChanged(sender, e);
                    if (objectivesCheckBox[objectivesCount].Checked)
                    {
                        for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                        {
                            name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                            criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                            differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                            effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                            float effective = effectiveness * 20;
                            int effectivenessBig = (int)(effective);

                            float critical = criticality * 80;
                            int newCriticality = (int)(critical);

                            float different = differentiation * 90;
                            int newDifferentiation = (int)(different);

                            //System.Diagnostics.Trace.WriteLine("Effectiveness: " + effectiveness.ToString() + "    effectiveness * 20 : " + (effectiveness * 20).ToString() + "   effectivenessBig: " + effectivenessBig.ToString());

                            circle[cirCount].Parent = canvas;

                            circle[cirCount].FillStyle = FillStyle.Solid;
                            
                            //circle[cirCount].FillColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));
                            //circle[cirCount].FillColor = mainForm.Categories[i].Objectives[j].BackColor;
                            circle[cirCount].FillColor = objectivesCheckBox[objectivesCount].BackColor;

                            circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                            circle[cirCount].AccessibleName = "Differentiation: " + differentiation.ToString() + "\nCriticality: " + criticality.ToString() + "\nEffectiveness: " + effectiveness.ToString();
                            //circle[cirCount].AccessibleName = "(" + differentiation.ToString() + "," + criticality.ToString() + "," + effectiveness.ToString() + ")";

                            /*panel.Parent = this.panelChart;
                            panel.SetDiffValue = differentiation.ToString();
                            panel.SetCritValue = criticality.ToString();
                            panel.SetEffectValue = effectiveness.ToString();*/

                            circle[cirCount].Visible = true;
                            circle[cirCount].Location = new System.Drawing.Point((60 + (int)(newDifferentiation) - (int)effectivenessBig / 2), (850 - (int)(newCriticality) - (int)(effectivenessBig / 2)));
                            circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);
                            //circle[cirCount].SendToBack();
                            //circle[cirCount].Enabled = false;

                            circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseClick);
                            circle[cirCount].MouseDown +=new MouseEventHandler(circle_MouseDown);
                            circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            circle[cirCount].MouseUp += new MouseEventHandler(circle_MouseUp);

                            System.Diagnostics.Trace.WriteLine(" circle height: " + effectivenessBig.ToString());

                            //System.Diagnostics.Trace.WriteLine("circle number is: " + cirCount.ToString());

                            //circle[cirCount].Refresh();
                            //System.Diagnostics.Trace.WriteLine("Load chart after mouse click event: " + labelCount.ToString());
                            //circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            rowCount++;
                            circleCount++;
                            cirCount++;
                            //System.Diagnostics.Trace.WriteLine("circle count: " + circleCount.ToString());

                        }
                    }
                    objectivesCount++;
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
                Bitmap bmp = new Bitmap(this.panelChart.Width, this.panelChart.Height);
                    //new Bitmap(this.panelChart.Width,this.panelChart.Height);
                this.panelChart.DrawToBitmap(bmp, this.panelChart.Bounds);
                bmp.Save(File.Create(save.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);

                //Image img = 
                //bmp.Save(@"C:\Temp\Test.bmp");
                //Image image = this.panelChart.
                //this.panelChart.SaveImage(File.Create(save.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
               // this.initiativeChart.SaveImage(File.Create(save.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void circle_MouseClick(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("Click " + circleCount.ToString());
            /*for (int i = 0; i < circleCount; i++)
            {
                //System.Diagnostics.Trace.WriteLine("In For Loop Click " + i);
                
                ///
                /// 
                ///
                if (circle[i].HitTest(MousePosition.X, MousePosition.Y))
                {
                    //System.Diagnostics.Trace.WriteLine("click circle!!");
                    //circle[i].AccessibleName = circle[i].Name;
                    //circle[i].ToString(circle[i].Width, circle[i].Height, circle[i].Size);

                    //int new_count = 0;

                    //string width = circle[i].Width.ToString();
                    for (int j = (i + 1); j < circleCount; j++)
                    {
                        //System.Diagnostics.Trace.WriteLine("Second for loop and j is: " + j.ToString());
                        if (circle[j].HitTest(MousePosition.X, MousePosition.Y))
                        {
                            if (circle[i].Height > circle[j].Height)
                            {
                                i = j;
                                break;
                            }
                            //if (new_count > 0 )
                        }
                    }

                    circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;

                    /*labelDescription.Parent = this.panelChart;

                    labelDescription.AutoSize = true;
                    labelDescription.Text = circle[i].AccessibleDescription;
                    //labelInfo[i].Location = new Point(circle[i].Location.X, circle[i].Location.Y);
                    labelDescription.Location = new Point(circle[i].Location.X, circle[i].Location.Y);
                    labelDescription.Name = circle[i].Name;
                    labelDescription.Visible = true;
                    labelDescription.Font = new Font("Arial", 12);
                    labelDescription.BackColor = Color.Transparent;*/

                    /*if (labelHide[i])
                    {
                        labelInfo[i].Show();
                        labelHide[i] = false;
                    }
                    else*/ 
                    /*if (labelInfo[i].Text == circle[i].AccessibleDescription)
                    {
                        System.Diagnostics.Trace.WriteLine("Hello! reset text! ");
                        System.Diagnostics.Trace.WriteLine("Label: " + labelInfo[i].Text);
                        System.Diagnostics.Trace.WriteLine("Circle: " + circle[i].AccessibleDescription);
                        labelInfo[i].ResetText();
                        System.Diagnostics.Trace.WriteLine("After reset Label: " + labelInfo[i].Text);
                        //labelInfo[i].Hide();
                        //labelHide[i] = true;
                    }
                    else
                    {
                        labelInfo[i].Parent = this.panelChart;

                        labelInfo[i].AutoSize = true;
                        labelInfo[i].Text = circle[i].AccessibleDescription;
                        //labelInfo[i].Location = new Point(circle[i].Location.X, circle[i].Location.Y);
                        labelInfo[i].Location = new Point(circle[i].Location.X + circle[i].Height, circle[i].Location.Y + circle[i].Height/2);
                        labelInfo[i].Name = circle[i].Name;
                        labelInfo[i].Visible = true;
                        labelInfo[i].Font = new Font("Arial", 12);
                        labelInfo[i].BackColor = circle[i].BackColor;
                        //labelInfo[i].

                        labelCount++;
                    }

                    return;
                    //System.Diagnostics.Trace.WriteLine("In HitTest: " + labelCount.ToString());
                }
            }*/
            circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;

            if (labelInfo[currentCircle].Text == circle[currentCircle].AccessibleDescription)
            {
                /*System.Diagnostics.Trace.WriteLine("Hello! reset text! ");
                System.Diagnostics.Trace.WriteLine("Label: " + labelInfo[i].Text);
                System.Diagnostics.Trace.WriteLine("Circle: " + circle[i].AccessibleDescription);*/
                labelInfo[currentCircle].ResetText();
                //System.Diagnostics.Trace.WriteLine("After reset Label: " + labelInfo[i].Text);
                //labelInfo[i].Hide();
                //labelHide[i] = true;
            }
            else
            {
                labelInfo[currentCircle].Parent = this.panelChart;

                labelInfo[currentCircle].AutoSize = true;
                labelInfo[currentCircle].Text = circle[currentCircle].AccessibleDescription;
                labelInfo[currentCircle].Name = circle[currentCircle].Name;
                labelInfo[currentCircle].Visible = true;
                labelInfo[currentCircle].Font = new Font("Arial", 12);
                labelInfo[currentCircle].BackColor = circle[currentCircle].BackColor;
                //labelInfo[i].Location = new Point(circle[i].Location.X, circle[i].Location.Y);
                if (circle[currentCircle].Location.X > 510)
                {
                    if (circle[currentCircle].Location.Y < 450)
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X - circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);// + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X, 
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);//circle[currentCircle].Height / 2);
                }
                else if (circle[currentCircle].Location.X < 150)
                {
                    if (circle[currentCircle].Location.Y < 450)
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height /2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height /2,
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);
                }
                else
                {
                    //labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height, circle[currentCircle].Location.Y + circle[currentCircle].Height / 2);
                    if (circle[currentCircle].Location.Y < 450)
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X - circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);//- labelInfo[currentCircle].Height);// + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X,// + labelInfo[currentCircle].Width / 2,//circle[currentCircle].Height,
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);// - circle[currentCircle].Height / 2);

                }
                //labelInfo[i].

                labelCount++;
            }

            /*panel.Parent = this.panelChart;

            panel.Show();*/
        }

        private void btnLoadObjectives_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            objCount = 0;

            int x = 5;
            int y = 5;

            if (mainForm == null)
            {
                this.Hide();
            }

            for (int i = 0; i < mainForm.Categories.Count; i++)
            {
                rowCount++;
                categoryLabel[i].Parent = this.panelList;
                categoryLabel[i].Text = mainForm.Categories[i].name;
                categoryLabel[i].AutoSize = true;
                categoryLabel[i].Font = new Font("Arial", 12, FontStyle.Bold);
                categoryLabel[i].BackColor = Color.Orange;
                y += i * 24 + objCount * 24;
                categoryLabel[i].Location = new Point(x, y);
                for (int j = 0; j < mainForm.Categories[i].Objectives.Count; j++)
                {
                    rowCount++;
                    objectivesCheckBox[objCount].Parent = this.panelList;
                    System.Diagnostics.Trace.WriteLine(mainForm.Categories[i].Objectives[j].Name);
                    objectivesCheckBox[objCount].Text = mainForm.Categories[i].Objectives[j].Name;
                    objectivesCheckBox[objCount].Name = objectivesCheckBox[j].Text;
                    y += j * 24;
                    objectivesCheckBox[objCount].Location = new Point((x + 10), (y+24));
                    objectivesCheckBox[objCount].Font = new Font("Arial", 12);
                    objectivesCheckBox[objCount].AutoSize = true;
                    objectivesCheckBox[objCount].BackColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));
                    //objectivesCheckBox[objCount].CheckedChanged += new EventHandler(BOMBubbleChartRedesign_CheckedChanged);
                    objCount++;
                }
            }
        }

        private int currentCircle;
        private bool mouseMove = false;
        private void circle_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            if (!mouseMove)
            {
                for (int i = 0; i < circleCount; i++)
                {
                    if (circle[i].HitTest(MousePosition.X, MousePosition.Y))
                    {
                        //System.Diagnostics.Trace.WriteLine("click circle!!");
                        //circle[i].AccessibleName = circle[i].Name;
                        //circle[i].ToString(circle[i].Width, circle[i].Height, circle[i].Size);

                        //int new_count = 0;

                        //string width = circle[i].Width.ToString();
                        for (int j = (i + 1); j < circleCount; j++)
                        {
                            //System.Diagnostics.Trace.WriteLine("Second for loop and j is: " + j.ToString());
                            if (circle[j].HitTest(MousePosition.X, MousePosition.Y))
                            {
                                if (circle[i].Height > circle[j].Height)
                                {
                                    i = j;
                                    break;
                                }
                                //if (new_count > 0 )
                            }
                        }

                        //System.Diagnostics.Trace.WriteLine("e.X: " + e.X.ToString() + ",   e.Y: " + e.Y.ToString());
                        //circle[i].Enabled = true;
                        //circle[i].E
                        for (int k = 0; k < circleCount; k++)
                        {
                            if (k == i)
                                circle[k].Enabled = true;
                            else
                                circle[k].Enabled = false;
                        }
                        currentCircle = i;
                        circle[i].BringToFront();
                        break;
                    }
                }
            }
        }

        private void circle_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseMove = true;
                int newX = circle[currentCircle].Left + e.X;
                int newY = circle[currentCircle].Top + e.Y;

                System.Diagnostics.Trace.WriteLine("newX: " + newX.ToString() + ",   newY: " + newY.ToString());

                if ((newX >= 60 && newX <= 960) && (newY >= 50 && newY <= 850))
                {
                    //System.Diagnostics.Trace.WriteLine("Congratulation!");

                    // Change the values to think move it x-axis 9, and y-axis 8
                    circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;

                    //circle[i].Left = circle[i].Left 
                    System.Diagnostics.Trace.WriteLine("left: " + circle[currentCircle].Left.ToString() + ",    top: " + circle[currentCircle].Top.ToString());
                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (double)90;
                    float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / 20;

                    //System.Diagnostics.Trace.WriteLine("circle height: " + circle[i].Height.ToString());

                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: " 
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();

                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
                else if (newX < 60)
                {
                    System.Diagnostics.Trace.WriteLine("X value less");
                    circle[currentCircle].Left = 60;
                    circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    //circle[i].Left += 60 - circle[i].Height / 2;
                    if ((newY >= 50 && newY <= 850))
                    {
                        circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;
                    }
                    else if (newY < 50)
                    {
                        circle[currentCircle].Top = 50;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 50 - circle[i].Height / 2;
                    }
                    else
                    {
                        //System.Diagnostics.Trace.WriteLine("Y less");
                        circle[currentCircle].Top = 850;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //System.Diagnostics.Trace.WriteLine("new circle: " + circle[i].Top.ToString());

                    }
                    float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    /*circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString() 
                        + "\nNew Effectiveness: " + newEffectiveness.ToString();*/
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
                else if (newX > 960)
                {
                    System.Diagnostics.Trace.WriteLine("X value more!");
                    circle[currentCircle].Left = 960;
                    circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    //circle[i].Left += 860 - circle[i].Height / 2;
                    if ((newY >= 50 && newY <= 850))
                    {
                        circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;
                    }
                    else if (newY < 50)
                    {
                        circle[currentCircle].Top = 50;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 50 - circle[i].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Top = 850;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 850 - circle[i].Height / 2;
                    }
                    float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString() + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }

                else if (newY > 850)
                {
                    circle[currentCircle].Top = 850;
                    circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                    //circle[i].Top += 860 - circle[i].Height / 2;
                    if ((newX >= 60 && newX <= 860))
                    {
                        circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    }
                    else if (newX < 60)
                    {
                        circle[currentCircle].Left = 60;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Left = 860;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString()
                    //    + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }

                else if (newY < 50)
                {
                    circle[currentCircle].Top = 50;
                    circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                    //circle[i].Top += 860 - circle[i].Height / 2;
                    if ((newX >= 60 && newX <= 860))
                    {
                        circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    }
                    else if (newX < 60)
                    {
                        circle[currentCircle].Left = 60;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Left = 960;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString()
                    //    + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
                /*for (int i = 0; i < circleCount; i++)
                {
                    if (circle[i].HitTest(MousePosition.X, MousePosition.Y))
                    {
                        //System.Diagnostics.Trace.WriteLine("click circle!!");
                        //circle[i].AccessibleName = circle[i].Name;
                        //circle[i].ToString(circle[i].Width, circle[i].Height, circle[i].Size);

                        //int new_count = 0;

                        //string width = circle[i].Width.ToString();
                        /*for (int j = (i + 1); j < circleCount; j++)
                        {
                            //System.Diagnostics.Trace.WriteLine("Second for loop and j is: " + j.ToString());
                            if (circle[j].HitTest(MousePosition.X, MousePosition.Y))
                            {
                                if (circle[i].Height > circle[j].Height)
                                {
                                    i = j;
                                    break;
                                }
                                //if (new_count > 0 )
                            }
                        }

                        //System.Diagnostics.Trace.WriteLine("e.X: " + e.X.ToString() + ",   e.Y: " + e.Y.ToString());
                        //circle[i].Enabled = true;
                        //circle[i].E
                        for (int k = 0; k < circleCount; k++)
                        {
                            if (k == i)
                                circle[k].Enabled = true;
                            else
                                circle[k].Enabled = false;
                        }

                        System.Diagnostics.Trace.WriteLine("Enable true now ");
                        int newX = circle[i].Left + e.X;
                        int newY = circle[i].Top + e.Y;

                        System.Diagnostics.Trace.WriteLine("newX: " + newX.ToString() + ",   newY: " + newY.ToString());

                        if ((newX >= 60 && newX <= 960) && (newY >= 50 && newY <= 850))
                        {
                            //System.Diagnostics.Trace.WriteLine("Congratulation!");

                            // Change the values to think move it x-axis 9, and y-axis 8
                            circle[i].Left += e.X- circle[i].Height / 2;
                            circle[i].Top += e.Y - circle[i].Height / 2;

                            //circle[i].Left = circle[i].Left 
                            System.Diagnostics.Trace.WriteLine("left: " + circle[i].Left.ToString() + ",    top: " + circle[i].Top.ToString());
                            double newDifferentiation = (double)(circle[i].Left + circle[i].Height / 2 - 60) / (double)90;
                            float newCriticality = (float)(850 - circle[i].Top + circle[i].Height/2) / (float)80;
                            float newEffectiveness = (float)circle[i].Height / 20;

                            //System.Diagnostics.Trace.WriteLine("circle height: " + circle[i].Height.ToString());

                            circle[i].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";

                            circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;
                        }
                        else if (newX < 60)
                        {
                            System.Diagnostics.Trace.WriteLine("X value less");
                            circle[i].Left = 60;
                            circle[i].Left -= circle[i].Height / 2;
                            //circle[i].Left += 60 - circle[i].Height / 2;
                            if ((newY >= 50 && newY <= 850))
                            {
                                circle[i].Top += e.Y - circle[i].Height / 2;
                            }
                            else if (newY < 50)
                            {
                                circle[i].Top = 50;
                                circle[i].Top -= circle[i].Height / 2;
                                //circle[i].Top += 50 - circle[i].Height / 2;
                            }
                            else
                            {
                                //System.Diagnostics.Trace.WriteLine("Y less");
                                circle[i].Top = 850;
                                circle[i].Top -= circle[i].Height / 2;
                                //System.Diagnostics.Trace.WriteLine("new circle: " + circle[i].Top.ToString());

                            }
                            float newDifferentiation = (float)(circle[i].Left + circle[i].Height / 2 - 60) / (float)90;
                            float newCriticality = (float)(850 - circle[i].Top - circle[i].Height / 2) / (float)80;
                            float newEffectiveness = (float)circle[i].Height / (float)20;

                            circle[i].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";

                            circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;
                        }
                        else if (newX > 960)
                        {
                            System.Diagnostics.Trace.WriteLine("X value more!");
                            circle[i].Left = 960;
                            circle[i].Left -= circle[i].Height / 2;
                            //circle[i].Left += 860 - circle[i].Height / 2;
                            if ((newY >= 50 && newY <= 850))
                            {
                                circle[i].Top += e.Y - circle[i].Height / 2;
                            }
                            else if (newY < 50)
                            {
                                circle[i].Top = 50;
                                circle[i].Top -= circle[i].Height / 2;
                                //circle[i].Top += 50 - circle[i].Height / 2;
                            }
                            else
                            {
                                circle[i].Top = 850;
                                circle[i].Top -= circle[i].Height / 2;
                                //circle[i].Top += 850 - circle[i].Height / 2;
                            }
                            float newDifferentiation = (float)(circle[i].Left + circle[i].Height / 2 - 60) / (float)90;
                            float newCriticality = (float)(850 - circle[i].Top + circle[i].Height / 2) / (float)80;
                            float newEffectiveness = (float)circle[i].Height / (float)20;

                            circle[i].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";

                            circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;
                        }

                        else if (newY > 850)
                        {
                            circle[i].Top = 850;
                            circle[i].Top -= circle[i].Height / 2;
                            //circle[i].Top += 860 - circle[i].Height / 2;
                            if ((newX >= 60 && newX <= 860))
                            {
                                circle[i].Left += e.X - circle[i].Height / 2;
                            }
                            else if (newX < 60)
                            {
                                circle[i].Left = 60;
                                circle[i].Left -= circle[i].Height / 2;
                            }
                            else
                            {
                                circle[i].Left = 860;
                                circle[i].Left -= circle[i].Height / 2;
                            }
                            float newDifferentiation = (float)(circle[i].Left + circle[i].Height / 2 - 60) / (float)90;
                            float newCriticality = (float)(850 - circle[i].Top - circle[i].Height / 2) / (float)80;
                            float newEffectiveness = (float)circle[i].Height / (float)20;

                            circle[i].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";

                            circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;
                        }

                        else if (newY < 50)
                        {
                            circle[i].Top = 50;
                            circle[i].Top -= circle[i].Height / 2;
                            //circle[i].Top += 860 - circle[i].Height / 2;
                            if ((newX >= 60 && newX <= 860))
                            {
                                circle[i].Left += e.X - circle[i].Height / 2;
                            }
                            else if (newX < 60)
                            {
                                circle[i].Left = 60;
                                circle[i].Left -= circle[i].Height / 2;
                            }
                            else
                            {
                                circle[i].Left = 960;
                                circle[i].Left -= circle[i].Height / 2;
                            }
                            float newDifferentiation = (float)(circle[i].Left + circle[i].Height / 2 - 60) / (float)90;
                            float newCriticality = (float)(850 - circle[i].Top - circle[i].Height / 2) / (float)80;
                            float newEffectiveness = (float)circle[i].Height / (float)20;

                            circle[i].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";

                            circle[i].AccessibleDescription = circle[i].Name + "\n" + circle[i].AccessibleName;
                        }
                    }
                }*/
            }
            mouseMove = false;
            for (int i = 0; i < circleCount; i++)
            {
                circle[i].Enabled = true;
            }
        }

        private void circle_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            mouseMove = false;
        }

        private void panelList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelList_Click(object sender, EventArgs e)
        {
            btnLoadChart_Click(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void objectivesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("CheckedChanged");
            //btnLoadChart_Click(sender, e);
            /*int rowCount = 0;
            int cirCount = 0;
            int objectivesCount = 0;

            System.Diagnostics.Trace.WriteLine("Load chart: " + labelCount.ToString());

            //labelDescription.ResetText();
            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                    labelInfo[i].ResetText();
            }

            for (int i = 0; i < circleCount; i++)
            {
                if (circle[i] != null)
                    circle[i].Hide();
            }
            circleCount = 0;
            labelCount = 0;

            //this.panelChart.

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = this.panelChart;

            if (mainForm == null)
            {
                this.Hide();
            }

            for (int i = 0; i < mainForm.CategoryCount; i++)
            {
                rowCount++;
                for (int j = 0; j < mainForm.Categories[i].BusinessObjectivesCount; j++)
                {
                    rowCount++;
                    if (objectivesCheckBox[objectivesCount].Checked)
                    {
                        for (int k = 0; k < mainForm.Categories[i].Objectives[j].InitiativesCount; k++)
                        {
                            name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                            criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                            differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                            effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                            int effectivenessBig = (int)(effectiveness * 20);
                            int newCriticality = (int)(criticality * 80);
                            int newDifferentiation = (int)(differentiation * 80);

                            circle[cirCount].Parent = canvas;

                            circle[cirCount].FillStyle = FillStyle.Solid;

                            circle[cirCount].FillColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));

                            circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                            circle[cirCount].AccessibleName = "(" + differentiation.ToString() + "," + criticality.ToString() + "," + effectiveness.ToString() + ")";

                            circle[cirCount].Visible = true;
                            circle[cirCount].Location = new System.Drawing.Point((60 + (int)(newDifferentiation) - (int)effectivenessBig / 2), (850 - (int)(newCriticality) - (int)(effectivenessBig / 2)));
                            circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);

                            circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseClick);
                            circle[cirCount].MouseDown += new MouseEventHandler(circle_MouseDown);
                            circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            circle[cirCount].MouseUp += new MouseEventHandler(circle_MouseUp);
                            //System.Diagnostics.Trace.WriteLine("Load chart after mouse click event: " + labelCount.ToString());
                            //circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            rowCount++;
                            circleCount++;
                            cirCount++;
                            System.Diagnostics.Trace.WriteLine("circle count: " + circleCount.ToString());

                        }
                    }
                    objectivesCount++;
                }
            }*/
        }

        private void BOMBubbleChartRedesign_CheckedChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("CheckedChanged");
            //btnLoadChart_Click(sender, e);
            int rowCount = 0;
            int cirCount = 0;
            int objectivesCount = 0;
            mouseDown = false;
            //System.Diagnostics.Trace.WriteLine("Load chart: " + labelCount.ToString());

            //labelDescription.ResetText();
            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                    labelInfo[i].ResetText();
            }

            for (int i = 0; i < circleCount; i++)
            {
                if (circle[i] != null)
                {
                    circle[i].Hide();
                }
            }
            circleCount = 0;
            labelCount = 0;

            //this.panelChart.

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = this.panelChart;

            for (int i = 0; i < mainForm.Categories.Count; i++)
            {
                rowCount++;
                for (int j = 0; j < mainForm.Categories[i].Objectives.Count; j++)
                {
                    rowCount++;
                    if (objectivesCheckBox[objectivesCount].Checked)
                    {
                        for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                        {
                            name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                            criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                            differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                            effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                            int effectivenessBig = (int)(effectiveness * 20);
                            int newCriticality = (int)(criticality * 80);
                            int newDifferentiation = (int)(differentiation * 80);

                            circle[cirCount].Parent = canvas;

                            circle[cirCount].FillStyle = FillStyle.Solid;

                            circle[cirCount].FillColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));

                            circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                            circle[cirCount].AccessibleName = "(" + differentiation.ToString() + "," + criticality.ToString() + "," + effectiveness.ToString() + ")";

                            circle[cirCount].Visible = true;
                            circle[cirCount].Location = new System.Drawing.Point((60 + (int)(newDifferentiation) - (int)effectivenessBig / 2), (850 - (int)(newCriticality) - (int)(effectivenessBig / 2)));
                            circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);

                            circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseClick);
                            circle[cirCount].MouseDown += new MouseEventHandler(circle_MouseDown);
                            circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            circle[cirCount].MouseUp += new MouseEventHandler(circle_MouseUp);
                            rowCount++;
                            circleCount++;
                            cirCount++;

                            System.Diagnostics.Trace.WriteLine("new circle count: " + circleCount.ToString());

                        }
                    }
                    objectivesCount++;
                }
            }
        }
    }
}
