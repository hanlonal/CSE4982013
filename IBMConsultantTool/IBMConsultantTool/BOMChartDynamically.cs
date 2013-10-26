﻿using System;
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
    public partial class BOMChartDynamically : Form
    {
        private string name;
        private float criticality = 0;
        private float differentiation = 0;
        private float effectiveness = 0;
       
        private BOMTool mainForm;

        private Random random = new Random();
        
        private Panel panelChart = new Panel();
        private Panel panelList = new Panel();
        private LineShape lineX = new LineShape();
        private LineShape lineY = new LineShape();
        private Label diffLabel = new Label();
        private Label criticLabel = new Label();

        private PictureBox picBox = new PictureBox();
        private Button btnClose = new Button();
        private Button btnSave = new Button();
        private Button btnReset = new Button();
        private Button btnUndo = new Button();
        private Button btnUpdate = new Button();

        private int panelChartWidth = 550;
        private int panelChartHeight = 550;

        private float[] newDiff = new float[1000];
        private float[] newCrit = new float[1000];
        private int[] newEff = new int[1000];
        

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

        private int initiativesCount = 0;
        private int[] catArray = new int[1000];
        private int[] objArray = new int[1000];
        private int[] iniArray = new int[1000];
        private float[,] difArray = new float[1000, 100];
        private float[,] effArray = new float[1000, 100];
        private float[,] criArray = new float[1000, 100];

        private int current = 0;
        private int click = 0;

        private int[,] locX = new int[1000, 100];
        private int[,] locY = new int[1000, 100];
        private int[] count = new int[1000];
        private int[] clickArray = new int[1000];

        private string[,] access1 = new string[1000, 100];
        private string[,] access2 = new string[1000, 100];

        public BOMChartDynamically(BOMTool info)
        {
            /*if (this.WindowState == FormWindowState.Normal && (base.Height != Height || base.Width != Width))
            {
                Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
                if (Height > maxWindowTrackSize.Height)
                    Height = maxWindowTrackSize.Height;
                if (Width > maxWindowTrackSize.Width)
                    Width = maxWindowTrackSize.Width;
            }*/

            mainForm = info;
            MaxCount = Count();

            for (int i = 0; i < MaxCount; i++)
            {
                circle[i] = new OvalShape();
                labelInfo[i] = new Label();
                objectivesCheckBox[i] = new CheckBox();
                categoryLabel[i] = new Label();
                labelHide[i] = false;
                newDiff[i] = new float();
                newCrit[i] = new float();
                newEff[i] = new int();
                catArray[i] = new int();
                objArray[i] = new int();
                iniArray[i] = new int();
                
                count[i] = new int();
                clickArray[i] = new int();
                for (int j = 0; j < 100; j++)
                {
                    difArray[i, j] = new float();
                    effArray[i, j] = new float();
                    criArray[i, j] = new float();
                    locX[i, j] = new int();
                    locY[i, j] = new int();
                    //access[i, j] = new string("", j);
                }
            }

            panelChart.Parent = this;
            panelList.Parent = this;
            btnClose.Parent = this;
            btnReset.Parent = this;
            btnUndo.Parent = this;
            btnSave.Parent = this;
            btnUpdate.Parent = this;

            //this.Width = 800;
            //this.Height = 600;

            
            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = panelChart;

            lineX.Parent = canvas;
            lineY.Parent = canvas;
            diffLabel.Parent = panelChart;
            criticLabel.Parent = panelChart;

            panelChart.Width = 550;
            panelChart.Height = 550;

            panelList.Width = 200;
            panelList.Height = 400;

            panelList.AutoScroll = true;

            diffLabel.AutoSize = true;
            diffLabel.Text = "Differentiation";
            diffLabel.Font = new Font("Arial", 14, FontStyle.Bold);

            //criticLabel.AutoSize = true;
            //criticLabel.Text = "Criticality";
            //criticLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            //diffLabel.Font.Size = 14;
            //diffLabel.Font.Name = "Arial";
            //diffLabel.Font.Bold = true;

            diffLabel.Location = new Point(panelChart.Width / 2 - diffLabel.Width / 2, panelChart.Height - diffLabel.Height - 5);

            Size btnSize = new Size(200, 35);
            Size btnSmallSize = new Size(95, 35);

            btnClose.Size = btnSize;
            btnReset.Size = btnSmallSize;
            btnUndo.Size = btnSmallSize;
            btnSave.Size = btnSize;
            btnUpdate.Size = btnSize;

            btnClose.Text = "Close";
            btnReset.Text = "Reset";
            btnUndo.Text = "Undo";
            btnSave.Text = "Save Image";
            btnUpdate.Text = "Update";

            btnClose.TextAlign = ContentAlignment.MiddleCenter;

            lineX.X1 = 30;
            lineX.X2 = panelChart.Width - 20;
            lineX.Y1 = panelChart.Height - 30;
            lineX.Y2 = panelChart.Height - 30;

            lineY.X1 = 30;
            lineY.X2 = 30;
            lineY.Y1 = 20;
            lineY.Y2 = panelChart.Height - 30;

            this.SizeChanged += new EventHandler(BOMChartDynamically_SizeChanged);
            SizeChanged += new EventHandler(panelChart_SizeChanged);
            SizeChanged += new EventHandler(panelList_SizeChanged);

            panelChart.BackColor = Color.White;
            panelList.BackColor = Color.LightGray;

            panelChart.Location = new Point(5, 5);

            panelList.Location = new Point(570, 5);

            btnUndo.Location = new Point(570, panelList.Height + 10);
            btnReset.Location = new Point(570 + btnUndo.Width, panelList.Height + 10);
            btnUpdate.Location = new Point(570, btnUndo.Location.Y + 38);
            btnSave.Location = new Point(570, btnUpdate.Location.Y + 38);
            btnClose.Location = new Point(570, btnSave.Location.Y + 38);

            btnUndo.Click += new EventHandler(btnUndo_Click);
            btnReset.Click += new EventHandler(btnReset_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnClose.Click += new EventHandler(btnClose_Click);
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            panelChart.Paint += new PaintEventHandler(picBox_Paint);

            int rowCount = 0;
            objCount = 0;

            int cirCount = 0;
            int objectivesCount = 0;
            mouseDown = false;

            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                {
                    labelInfo[i].ResetText();
                    labelInfo[i] = new Label();
                }
            }

            for (int i = 0; i < circleCount; i++)
            {
                if (circle[i] != null)
                {
                    circle[i].Hide();
                    circle[i] = new OvalShape();
                }
            }

            circleCount = 0;
            labelCount = 0;

            int x = 5;
            int y = 5;

            if (mainForm == null)
            {
                this.Hide();
            }

            System.Diagnostics.Trace.WriteLine("lineX: " + lineX.X1.ToString() + lineX.X2.ToString() + lineX.Y1.ToString() + lineX.Y2.ToString() + "  lineY: " + lineY.ToString());

            for (int i = 0; i < mainForm.Categories.Count; i++)
            {
                rowCount++;
                categoryLabel[i].Parent = panelList;
                categoryLabel[i].Text = mainForm.Categories[i].name;
                categoryLabel[i].AutoSize = true;
                categoryLabel[i].Font = new Font("Arial", 12, FontStyle.Bold);
                categoryLabel[i].BackColor = Color.Orange;
                y += i * 24;
                categoryLabel[i].Location = new Point(x, y);
                for (int j = 0; j < mainForm.Categories[i].Objectives.Count; j++)
                {
                    rowCount++;
                    objectivesCheckBox[objCount].Parent = panelList;
                    //System.Diagnostics.Trace.WriteLine(mainForm.Categories[i].Objectives[j].Name);
                    objectivesCheckBox[objCount].Text = mainForm.Categories[i].Objectives[j].Name;
                    objectivesCheckBox[objCount].Name = objectivesCheckBox[j].Text;
                    y += 24;
                    objectivesCheckBox[objCount].Location = new Point((x + 10), y);
                    objectivesCheckBox[objCount].Font = new Font("Arial", 12);
                    objectivesCheckBox[objCount].AutoSize = true;
                    objectivesCheckBox[objCount].BackColor = Color.FromArgb(random.Next(225), random.Next(225), random.Next(225), random.Next(225));
                    objectivesCheckBox[objCount].Checked = true;
                    objectivesCheckBox[objCount].CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                    //if (objectivesCheckBox[objectivesCount].Checked)
                    //{
                        for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                        {
                            name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                            criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                            differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                            effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                            catArray[cirCount] = i;
                            objArray[cirCount] = j;
                            iniArray[cirCount] = k;

                            float effective = effectiveness * 20;
                            int effectivenessBig = (int)(effective);
                            newEff[cirCount] = effectivenessBig;
                            effArray[cirCount, 0] = effectiveness;

                            float critical = criticality * ((lineY.Y2 - lineY.Y1) / 10);
                            int newCriticality = (int)(critical);
                            newCrit[cirCount] = criticality;
                            criArray[cirCount, 0] = criticality;

                            float different = differentiation * ((lineX.X2 - lineX.X1) / 10);
                            int newDifferentiation = (int)(different);
                            newDiff[cirCount] = differentiation;
                            difArray[cirCount, 0] = differentiation;

                            System.Diagnostics.Trace.WriteLine("Diff: " + newDifferentiation.ToString() + "  Critic: " + newCriticality.ToString());

                            circle[cirCount].Parent = canvas;

                            circle[cirCount].FillStyle = FillStyle.Solid;
                            circle[cirCount].FillColor = objectivesCheckBox[objectivesCount].BackColor;

                            circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                            circle[cirCount].AccessibleName = "Differentiation: " + differentiation.ToString() + "\nCriticality: " + criticality.ToString() + "\nEffectiveness: " + effectiveness.ToString();

                            //circle[cirCount].AccessibleDescription = circle[cirCount].Name + "\n" + circle[cirCount].AccessibleName;

                            circle[cirCount].Visible = true;
                            circle[cirCount].Location = new System.Drawing.Point((30 + (int)(newDifferentiation) - (int)effectivenessBig / 2), (lineX.Y1 - (int)(newCriticality) - (int)(effectivenessBig / 2)));
                            circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);

                            locX[cirCount, 0] = circle[cirCount].Left;
                            locY[cirCount, 0] = circle[cirCount].Top;
                            count[cirCount] = 0;
                            access1[cirCount, 0] = circle[cirCount].Name;
                            access2[cirCount, 0] = circle[cirCount].AccessibleName;

                            System.Diagnostics.Trace.WriteLine("X: " + circle[cirCount].Location.X.ToString() + "  Y: " + circle[cirCount].Location.Y.ToString());

                            circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseClick);
                            circle[cirCount].MouseDown += new MouseEventHandler(circle_MouseDown);
                            circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            circle[cirCount].MouseUp += new MouseEventHandler(circle_MouseUp);

                            rowCount++;
                            circleCount++;
                            cirCount++;
                            initiativesCount++;

                        }
                    //}*/
                    objectivesCount++;
                    objCount++;
                }
                y += 10;
            }
            
            InitializeComponent();

            //this.panelChart.Paint += new PaintEventHandler(picBox_Paint);
            //GenerateText();
        }

        //private Label criticLabel = new Label();
        //private PictureBox picBox = new PictureBox();


        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("Painting!");
            string myText = "Criticality";

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 14, FontStyle.Bold, GraphicsUnit.Point);
            PointF pointF = new PointF(0, panelChart.Height / 2);
            StringFormat stringFormat = new StringFormat();
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));

            //stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            e.Graphics.DrawString(myText, font, Brushes.Black, pointF, stringFormat);

            /*StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            Bitmap img = new Bitmap(criticLabel.Height, criticLabel.Width);
            Graphics G = Graphics.FromImage(img);

            G.Clear(criticLabel.BackColor);

            SolidBrush brush = new SolidBrush(criticLabel.ForeColor);
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            G.DrawString(criticLabel.Text, criticLabel.Font, Brushes.Black, new Rectangle(0, 0, img.Width, img.Height), format);
            brush.Dispose();
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);

            criticLabel.Image = img;
            /*Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);*/

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

        private void BOMChartDynamically_SizeChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("In size change this.Width: " + this.Width.ToString() + "  this.Height: " + this.Height.ToString());
            Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
            if (Height > maxWindowTrackSize.Height)
                Height = maxWindowTrackSize.Height;
            if (Width > maxWindowTrackSize.Width)
                Width = maxWindowTrackSize.Width;
        }

        private void panelChart_SizeChanged(object sender, EventArgs e)
        {
            panelChart.Width = this.Width - 250;
            panelChart.Height = this.Height - 50;
            panelChart.Location = new Point(5, 5);

            lineX.X1 = 30;
            lineX.X2 = panelChart.Width - 20;
            lineX.Y1 = panelChart.Height - 30;
            lineX.Y2 = panelChart.Height - 30;

            lineY.X1 = 30;
            lineY.X2 = 30;
            lineY.Y1 = 20;
            lineY.Y2 = panelChart.Height - 30;

            diffLabel.Location = new Point(panelChart.Width / 2 - diffLabel.Width / 2, panelChart.Height - diffLabel.Height - 5);

            for (int cnt = 0; cnt < circleCount; cnt++)
            {
                float diff = newDiff[cnt];
                float crit = newCrit[cnt];
                float newValDiff = diff * ((lineX.X2 - lineX.X1) / 10);
                float newValCrit = crit * ((lineY.Y2 - lineY.Y1) / 10);
                int newX = (int)newValDiff;
                int newY = (int)newValCrit;
                circle[cnt].Location = new Point((30 + (int)(newValDiff) - (int)newEff[cnt] / 2), (lineX.Y2 - (int)(newValCrit) - (int)(newEff[cnt] / 2)));
                //labelInfo[cnt].Location = new Point();
            }

            panelChartHeight = panelChart.Height;
            panelChartWidth = panelChart.Width;
        }

        private void panelList_SizeChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("this.Width: " + this.DesktopBounds.Width.ToString() + "  this.Height: " + this.Height.ToString());
            
            panelList.Width = 200;
            panelList.Height = this.Height - 200;
            panelList.Location = new Point(this.Width - panelList.Width - 25, 5);
            btnUndo.Location = new Point(this.Width - 225, panelList.Height + 10);
            btnReset.Location = new Point(this.Width - 120, panelList.Height + 10);
            btnUpdate.Location = new Point(this.Width - 225, btnUndo.Location.Y + 38);
            btnSave.Location = new Point(this.Width - 225, btnUpdate.Location.Y + 38);
            btnClose.Location = new Point(this.Width - 225, btnSave.Location.Y + 38);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                {
                    labelInfo[i].ResetText();
                    labelInfo[i] = new Label();
                }
            }

            click -= 1;
            if (click >= 0)
            {
                int num = clickArray[click];
                count[num] -= 1;

                if (count[num] >= 0)
                {
                    circle[num].Left = locX[num, count[num]];
                    circle[num].Top = locY[num, count[num]];

                    circle[num].Name = access1[num, count[num]];
                    circle[num].AccessibleName = access2[num, count[num]];

                    System.Diagnostics.Trace.WriteLine("X: " + circle[current].Left.ToString()
                        + "  Y: " + circle[current].Top.ToString());
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                {
                    labelInfo[i].ResetText();
                    labelInfo[i] = new Label();
                }
            }

            for (int cnt = 0; cnt < circleCount; cnt++)
            {
                circle[cnt].Left = locX[cnt, 0];
                circle[cnt].Top = locY[cnt, 0];

                circle[cnt].Name = access1[cnt, 0];
                circle[cnt].AccessibleName = access2[cnt, 0];
            }
        }

        private void circle_MouseClick(object sender, MouseEventArgs e)
        {
            circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;

            if (labelInfo[currentCircle].Text == circle[currentCircle].AccessibleDescription)
            {
                labelInfo[currentCircle].ResetText();
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

                if (circle[currentCircle].Location.X > (panelChart.Width / 2))
                {
                    if (circle[currentCircle].Location.Y < (panelChart.Height / 2))
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X - circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);// + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X,
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);//circle[currentCircle].Height / 2);
                }
                else if (circle[currentCircle].Location.X < (30 + (lineX.X2 - lineX.X1)/2))
                {
                    if (circle[currentCircle].Location.Y < (panelChart.Height / 2))
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);
                }
                else
                {
                    //labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X + circle[currentCircle].Height, circle[currentCircle].Location.Y + circle[currentCircle].Height / 2);
                    if (circle[currentCircle].Location.Y < (panelChart.Height / 2))
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X - circle[currentCircle].Height / 2,
                            circle[currentCircle].Location.Y + circle[currentCircle].Height);//- labelInfo[currentCircle].Height);// + circle[currentCircle].Height);
                    else
                        labelInfo[currentCircle].Location = new Point(circle[currentCircle].Location.X,// + labelInfo[currentCircle].Width / 2,//circle[currentCircle].Height,
                            circle[currentCircle].Location.Y - labelInfo[currentCircle].Height);// - circle[currentCircle].Height / 2);

                }

                labelCount++;
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
                        for (int j = (i + 1); j < circleCount; j++)
                        {
                            if (circle[j].HitTest(MousePosition.X, MousePosition.Y))
                            {
                                if (circle[i].Height > circle[j].Height)
                                {
                                    i = j;
                                    break;
                                }
                            }
                        }

                        for (int k = 0; k < circleCount; k++)
                        {
                            if (k == i)
                                circle[k].Enabled = true;
                            else
                                circle[k].Enabled = false;
                        }
                        currentCircle = i;
                        current = i;
                        circle[i].BringToFront();
                        clickArray[click] = i;
                        click++;
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

                if ((newX >= lineX.X1 && newX <= lineX.X2) && (newY >= lineY.Y1 && newY <= lineY.Y2))
                {
                    circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;

                    System.Diagnostics.Trace.WriteLine("left: " + circle[currentCircle].Left.ToString() + ",    top: " + circle[currentCircle].Top.ToString());
                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    float newCriticality = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    float newEffectiveness = (float)circle[currentCircle].Height / 20;

                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();

                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
                else if (newX < lineX.X1)
                {
                    System.Diagnostics.Trace.WriteLine("X value less");
                    circle[currentCircle].Left = lineX.X1;
                    circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    //circle[i].Left += 60 - circle[i].Height / 2;
                    if ((newY >= lineY.Y1 && newY <= lineY.Y2))
                    {
                        circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;
                    }
                    else if (newY < lineY.Y1)
                    {
                        circle[currentCircle].Top = lineY.Y1;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 50 - circle[i].Height / 2;
                    }
                    else
                    {
                        //System.Diagnostics.Trace.WriteLine("Y less");
                        circle[currentCircle].Top = lineY.Y2;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //System.Diagnostics.Trace.WriteLine("new circle: " + circle[i].Top.ToString());

                    }
                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    float newCriticality = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    //float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    //float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    /*circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString() 
                        + "\nNew Effectiveness: " + newEffectiveness.ToString();*/
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
                else if (newX > lineX.X2)
                {
                    System.Diagnostics.Trace.WriteLine("X value more!");
                    circle[currentCircle].Left = lineX.X2;
                    circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    //circle[i].Left += 860 - circle[i].Height / 2;
                    if ((newY >= lineY.Y1 && newY <= lineY.Y2))
                    {
                        circle[currentCircle].Top += e.Y - circle[currentCircle].Height / 2;
                    }
                    else if (newY < lineY.Y1)
                    {
                        circle[currentCircle].Top = lineY.Y1;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 50 - circle[i].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Top = lineY.Y2;
                        circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                        //circle[i].Top += 850 - circle[i].Height / 2;
                    }
                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    float newCriticality = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    //float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    //float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString() + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }

                else if (newY > lineY.Y2)
                {
                    circle[currentCircle].Top = lineY.Y2;
                    circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                    //circle[i].Top += 860 - circle[i].Height / 2;
                    if ((newX >= lineX.X1 && newX <= lineX.X2))
                    {
                        circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    }
                    else if (newX < lineX.X1)
                    {
                        circle[currentCircle].Left = lineX.X1;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Left = lineX.X2;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }

                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    float newCriticality = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    //double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    //float newCriticality = (float)(panelChart.Height - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    //float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    //float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString()
                    //    + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }

                else if (newY < lineY.Y1)
                {
                    circle[currentCircle].Top = lineY.Y1;
                    circle[currentCircle].Top -= circle[currentCircle].Height / 2;
                    //circle[i].Top += 860 - circle[i].Height / 2;
                    if ((newX >= lineX.X1 && newX <= lineX.X2))
                    {
                        circle[currentCircle].Left += e.X - circle[currentCircle].Height / 2;
                    }
                    else if (newX < lineX.X1)
                    {
                        circle[currentCircle].Left = lineX.X1;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    else
                    {
                        circle[currentCircle].Left = lineX.X2;
                        circle[currentCircle].Left -= circle[currentCircle].Height / 2;
                    }
                    double newDifferentiation = (double)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (double)((lineX.X2 - lineX.X1) / 10);
                    float newCriticality = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);
                    //float newDifferentiation = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 60) / (float)90;
                    //float newCriticality = (float)(850 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)80;
                    float newEffectiveness = (float)circle[currentCircle].Height / (float)20;

                    //circle[currentCircle].AccessibleName = "(" + newDifferentiation.ToString() + "," + newCriticality.ToString() + "," + newEffectiveness.ToString() + ")";
                    //circle[currentCircle].AccessibleName = "New Differentiation: " + newDifferentiation.ToString() + "\nNew Criticality: " + newCriticality.ToString()
                    //    + "\nNew Effectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleName = "Differentiation: " + newDifferentiation.ToString() + "\nCriticality: "
                        + newCriticality.ToString() + "\nEffectiveness: " + newEffectiveness.ToString();
                    circle[currentCircle].AccessibleDescription = circle[currentCircle].Name + "\n" + circle[currentCircle].AccessibleName;
                }
            }
            mouseMove = false;
            for (int i = 0; i < circleCount; i++)
            {
                circle[i].Enabled = true;
            }
        }

        private void circle_MouseUp(object sender, MouseEventArgs e)
        {
            count[current] += 1;

            locX[current, count[current]] = circle[current].Left;
            locY[current, count[current]] = circle[current].Top;

            access1[current, count[current]] = circle[current].Name;
            access2[current, count[current]] = circle[current].AccessibleName;

            difArray[current, count[current]] = (float)(circle[currentCircle].Left + circle[currentCircle].Height / 2 - 30) / (float)((lineX.X2 - lineX.X1) / 10);
            criArray[current, count[current]] = (float)(lineY.Y2 - circle[currentCircle].Top - circle[currentCircle].Height / 2) / (float)((lineY.Y2 - lineY.Y1) / 10);

            mouseDown = false;
            mouseMove = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < circleCount; cnt++)
            {
                mainForm.Categories[catArray[cnt]].Objectives[objArray[cnt]].Initiatives[iniArray[cnt]].Criticality = criArray[cnt, count[cnt]];
                mainForm.Categories[catArray[cnt]].Objectives[objArray[cnt]].Initiatives[iniArray[cnt]].Differentiation = difArray[cnt, count[cnt]];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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
            this.Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            int rowCount = 0;

            int cirCount = 0;
            int objectivesCount = 0;
            mouseDown = false;

            for (int i = 0; i < circleCount; i++)
            {
                if (labelInfo[i] != null)
                {
                    labelInfo[i].ResetText();
                    labelInfo[i] = new Label();
                }
            }

            for (int i = 0; i < circleCount; i++)
            {
                if (circle[i] != null)
                {
                    circle[i].Hide();
                    circle[i] = new OvalShape();
                }
            }

            circleCount = 0;
            labelCount = 0;

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = panelChart;

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
                    if (objectivesCheckBox[objectivesCount].Checked)
                    {
                        for (int k = 0; k < mainForm.Categories[i].Objectives[j].Initiatives.Count; k++)
                        {
                            name = mainForm.Categories[i].Objectives[j].Initiatives[k].Name;
                            criticality = mainForm.Categories[i].Objectives[j].Initiatives[k].Criticality;
                            differentiation = mainForm.Categories[i].Objectives[j].Initiatives[k].Differentiation;
                            effectiveness = mainForm.Categories[i].Objectives[j].Initiatives[k].Effectiveness;

                            catArray[cirCount] = i;
                            objArray[cirCount] = j;
                            iniArray[cirCount] = k;

                            float effective = effectiveness * 20;
                            int effectivenessBig = (int)(effective);
                            newEff[cirCount] = effectivenessBig;
                            effArray[cirCount, 0] = effectiveness;

                            float critical = criticality * ((lineY.Y2 - lineY.Y1) / 10);
                            int newCriticality = (int)(critical);
                            newCrit[cirCount] = criticality;
                            criArray[cirCount, 0] = criticality;

                            float different = differentiation * ((lineX.X2 - lineX.X1) / 10);
                            int newDifferentiation = (int)(different);
                            newDiff[cirCount] = differentiation;
                            difArray[cirCount, 0] = differentiation;

                            circle[cirCount].Parent = canvas;

                            circle[cirCount].FillStyle = FillStyle.Solid;
                            circle[cirCount].FillColor = objectivesCheckBox[objectivesCount].BackColor;

                            circle[cirCount].Name = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + " " + name;

                            circle[cirCount].AccessibleName = "Differentiation: " + differentiation.ToString() + "\nCriticality: " + criticality.ToString() + "\nEffectiveness: " + effectiveness.ToString();

                            circle[cirCount].Visible = true;
                            circle[cirCount].Location = new System.Drawing.Point((30 + (int)(newDifferentiation) - (int)effectivenessBig / 2), (lineX.Y1 - (int)(newCriticality) - (int)(effectivenessBig / 2)));
                            circle[cirCount].Size = new System.Drawing.Size(effectivenessBig, effectivenessBig);

                            circle[cirCount].AccessibleDescription = circle[cirCount].Name + "\n" + circle[cirCount].AccessibleName;

                            locX[cirCount, 0] = circle[cirCount].Left;
                            locY[cirCount, 0] = circle[cirCount].Top;
                            count[cirCount] = 0;
                            access1[cirCount, 0] = circle[cirCount].Name;
                            access2[cirCount, 0] = circle[cirCount].AccessibleName;

                            circle[cirCount].MouseClick += new MouseEventHandler(circle_MouseClick);
                            circle[cirCount].MouseDown += new MouseEventHandler(circle_MouseDown);
                            circle[cirCount].MouseMove += new MouseEventHandler(circle_MouseMove);
                            circle[cirCount].MouseUp += new MouseEventHandler(circle_MouseUp);

                            rowCount++;
                            circleCount++;
                            cirCount++;

                        }
                    }
                    objectivesCount++;
                }
            }
        }
    }
}
