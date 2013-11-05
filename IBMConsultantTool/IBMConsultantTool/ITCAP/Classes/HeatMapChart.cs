using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic.PowerPacks;

namespace IBMConsultantTool
{
    public partial class HeatMapChart : Form
    {
        private Panel panelChart = new Panel();
        private Panel panelList = new Panel();

        List<string> domains = new List<string>();
        List<string> capabilities = new List<string>();
        List<float> gap = new List<float>();
        List<int> capPerDom = new List<int>();
        List<bool> notAFocus = new List<bool>();
        int domCount = 0;
        int capCount = 0;
        float avgGap = 0;
        int gapNum = 0;

        LineShape main = new LineShape();

        public HeatMapChart(List<string> dom, List<string> cap, List<int> capNum, List<float> gap, 
            float avgGap, List<bool> notFocus, float totalGap, int numGap)
        {
            panelChart.Parent = this;
            panelList.Parent = this;

            panelChart.Size = new Size(580, 690);
            panelChart.Location = new Point(10, 10);
            panelChart.BackColor = Color.White;

            panelList.Size = new Size(180, 500);
            System.Diagnostics.Trace.WriteLine("position: " + (this.Width - panelList.Width - 10).ToString());
            panelList.Location = new Point(600, 10);
            panelList.AutoScroll = true;
            panelList.BackColor = Color.White;

            this.domCount = dom.Count;
            this.capCount = cap.Count;
            this.avgGap = avgGap;
            this.gapNum = numGap;

            for (int i = 0; i < domCount; i++)
            {
                this.domains.Add(dom[i]);
            }

            for (int j = 0; j < capCount; j++)
            {
                this.capabilities.Add(cap[j]);
                this.gap.Add(gap[j]);
            }

            for (int cnt = 0; cnt < capNum.Count; cnt++)
                this.capPerDom.Add(capNum[cnt]);
            for (int cnt = 0; cnt < notFocus.Count; cnt++)
                this.notAFocus.Add(notFocus[cnt]);

            this.Load += new EventHandler(HeatMapChart_Load);

            InitializeComponent();
        }

        private void HeatMapChart_Load(object sender, EventArgs e)
        {
            Label legend = new Label();
            legend.Parent = panelList;
            legend.Text = "Legend";
            legend.AutoSize = true;
            legend.Font = new Font("Arial", 14, FontStyle.Bold);
            legend.Location = new Point(panelList.Width / 2 - legend.Width / 2, 5);

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = panelList;

            RectangleShape recMajor = new RectangleShape();
            recMajor.Parent = canvas;
            recMajor.FillColor = Color.Red;
            recMajor.FillStyle = FillStyle.Solid;
            recMajor.Size = new Size(20, 20);
            recMajor.Location = new Point(10, legend.Bottom + 30);

            RectangleShape recMid = new RectangleShape();
            recMid.Parent = canvas;
            recMid.FillColor = Color.Yellow;
            recMid.FillStyle = FillStyle.Solid;
            recMid.Size = new Size(20, 20);
            recMid.Location = new Point(10, recMajor.Location.Y + recMajor.Height + 20);

            RectangleShape recSmall = new RectangleShape();
            recSmall.Parent = canvas;
            recSmall.FillColor = Color.GreenYellow;
            recSmall.FillStyle = FillStyle.Solid;
            recSmall.Size = new Size(20, 20);
            recSmall.Location = new Point(10, recMid.Location.Y + recMid.Height + 20);

            RectangleShape recNo = new RectangleShape();
            recNo.Parent = canvas;
            recNo.FillColor = Color.White;
            recNo.FillStyle = FillStyle.Solid;
            recNo.Size = new Size(20, 20);
            recNo.Location = new Point(10, recSmall.Location.Y + recSmall.Height + 20);

            Label major = new Label();
            major.Parent = panelList;
            major.Text = "Major Gap";
            major.AutoSize = true;
            major.Font = new Font("Arial", 12, FontStyle.Bold);
            //recMajor.Size = new Size(major.Height, major.Height);
            major.Location = new Point(recMajor.Right + 10, recMajor.Top);

            Label middle = new Label();
            middle.Parent = panelList;
            middle.Text = "Gap";
            middle.AutoSize = true;
            middle.Font = new Font("Arial", 12, FontStyle.Bold);
            //recMid.Size = new Size(middle.Height, middle.Height);
            middle.Location = new Point(recMid.Right + 10, recMid.Top);

            Label small = new Label();
            small.Parent = panelList;
            small.Text = "Small/No Gap";
            small.AutoSize = true;
            small.Font = new Font("Arial", 12, FontStyle.Bold);
            //recSmall.Size = new Size(small.Height, small.Height);
            small.Location = new Point(recSmall.Right + 10, recSmall.Top);

            Label notFocus = new Label();
            notFocus.Parent = panelList;
            notFocus.Text = "Not Focus";
            notFocus.AutoSize = true;
            notFocus.Font = new Font("Arial", 12, FontStyle.Bold);
            //recNo.Size = new Size(notFocus.Height, notFocus.Height);
            notFocus.Location = new Point(recNo.Right + 10, recNo.Top);

            float[] gaps = new float[capabilities.Count];

            

            System.Diagnostics.Trace.WriteLine("size: " + notFocus.Height.ToString());

            

            Label title = new Label();
            title.Parent = panelChart;
            title.Text = "Capability Gap";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(panelChart.Width / 2 - title.Width / 2, 5);

            ShapeContainer linePacks = new ShapeContainer();
            linePacks.Parent = panelChart;

            main.Parent = linePacks;
            main.BorderWidth = 2;
            main.X1 = title.Left;
            main.X2 = title.Right;
            main.Y1 = title.Bottom + 5;
            main.Y2 = title.Bottom + 5;

            Label[] domTitle = new Label[domCount];
            Button[] capTitle = new Button[capCount];
            
            int counting = 0;
            int lastVal = 0;
            //int heightBox = 0;
            int locBoxY = 0;
            Size size = new Size(250, 20);
            int count = 0;

            float redGap = 0;
            float yellowGap = 0;
            float greenGap = 0;

            for (int j = 0; j < capabilities.Count; j++)
            {
                if (!notAFocus[j])
                {
                    gaps[count] = gap[j];
                    count++;
                }
            }

            Array.Sort<float>(gaps);
            count = 0;
            foreach (var g in gaps)
            {
                Console.WriteLine(g);
                gaps[count] = g;
                count++;
            }

            int section = 0;
            if (count == 1)
                redGap = gaps[0];
            else if (count == 2)
            {
                redGap = gaps[1];
                yellowGap = gaps[0];
            }
            else if (count == 3)
            {
                redGap = gaps[2];
                yellowGap = gaps[1];
                greenGap = gaps[0];
            }
            else
            {
                section = count / 3;
                if ((count % 3) == 0)
                    yellowGap = gaps[count - section * 2];
                else
                {
                    yellowGap = gaps[section];
                    section = count / 3 + 1;
                }

                redGap = gaps[count - section];
                greenGap = gaps[0];

            }

            System.Diagnostics.Trace.WriteLine("red: " + redGap.ToString() + ", yellow: " + yellowGap.ToString() + ", green: " + greenGap.ToString());

            for (int i = 0; i < domCount; i++)
            {
                //heightBox = 0;
                domTitle[i] = new Label();
                domTitle[i].Parent = panelChart;
                domTitle[i].AutoSize = true;
                domTitle[i].Text = domains[i];
                domTitle[i].Font = new Font("Arial", 14, FontStyle.Bold);
                ShapeContainer con = new ShapeContainer();
                con.Parent = panelChart;
                RectangleShape rectangle = new RectangleShape();
                rectangle.Parent = con;
                if (lastVal != 0)
                    domTitle[i].Location = new Point(panelChart.Width / 2 - domTitle[i].Width / 2, 10 + capTitle[lastVal-1].Bottom);
                else
                    domTitle[i].Location = new Point(panelChart.Width / 2 - domTitle[i].Width / 2, title.Bottom + 20 + i * 10);
                System.Diagnostics.Trace.WriteLine("per domain: " + capPerDom[i].ToString());
                counting = capPerDom[i];
                /*if (i == 0)
                    heightBox += domTitle[i].Height;
                else
                    heightBox += domTitle[i].Height + 10;*/
                for (int j = 0; j < counting; j++)
                {
                    capTitle[j+lastVal] = new Button();
                    capTitle[j+lastVal].Parent = panelChart;
                    capTitle[j + lastVal].Width = 250;
                    capTitle[j+lastVal].AutoSize = true;
                    capTitle[j+lastVal].Text = capabilities[j+lastVal];
                    capTitle[j+lastVal].Font = new Font("Arial", 12, FontStyle.Bold);
                    //System.Diagnostics.Trace.WriteLine("cap loop: " + j.ToString());
                    if ((j % 2) == 1)
                        capTitle[j+lastVal].Location = new Point(panelChart.Width - capTitle[j+lastVal].Width - 10, capTitle[j + lastVal - 1].Location.Y);
                    else
                    {
                        if (j != 0)
                            capTitle[j+lastVal].Location = new Point(10, capTitle[j + lastVal - 1].Bottom + 10);
                        else
                            capTitle[j+lastVal].Location = new Point(10, domTitle[i].Bottom + 10);
                    }

                    System.Diagnostics.Trace.WriteLine("gap: " + gap[j + lastVal].ToString());
                    //if (gap[j + lastVal] > avgGap)
                    if (gap[j + lastVal] >= redGap)
                    {
                        //System.Diagnostics.Trace.WriteLine("red: ");
                        capTitle[j + lastVal].BackColor = Color.OrangeRed;
                    }
                    //else if (gap[j + lastVal] == 0)
                    else if (gap[j + lastVal] < redGap && gap[j + lastVal] >= yellowGap)
                    {
                        //System.Diagnostics.Trace.WriteLine("yellow: ");
                        capTitle[j + lastVal].BackColor = Color.Yellow;
                    }
                    else if (gap[j + lastVal] < yellowGap && gap[j + lastVal] >= greenGap)
                    {
                        //System.Diagnostics.Trace.WriteLine("green: " + yellowGap.ToString() + ", gap: " + gap[j+lastVal].ToString());
                        capTitle[j + lastVal].BackColor = Color.GreenYellow;
                    }
                    else
                        capTitle[j + lastVal].BackColor = Color.White;
                    //heightBox += capTitle[j + lastVal].Height;
                }
                lastVal += counting;
                //System.Diagnostics.Trace.WriteLine("height: " + (capTitle[lastVal - 1].Bottom + 20 - domTitle[i].Top).ToString());
                rectangle.Size = new Size(panelChart.Width - 10, capTitle[lastVal-1].Bottom + 20 - domTitle[i].Top);

                if (i == 0)
                    rectangle.Location = new Point(5, title.Bottom + 10);
                else
                    rectangle.Location = new Point(5, locBoxY);
                locBoxY = rectangle.Bottom;
                //rectangle.BringToFront();
                //System.Diagnostics.Trace.WriteLine("X: " + rectangle.Location.X.ToString() + ",  Y: " + rectangle.Location.Y.ToString());
                rectangle.Enabled = false;
                rectangle.SendToBack();
                //System.Diagnostics.Trace.WriteLine("Done");
            }

            Bitmap bmp = new Bitmap(panelChart.Width, panelChart.Height);
            //new Bitmap(this.panelChart.Width,this.panelChart.Height);
            panelChart.DrawToBitmap(bmp, panelChart.Bounds);
            bmp.Save(Directory.GetCurrentDirectory() + @"/Charts/" + "HeatMap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //bmp.Save(Application.StartupPath + "\\HeatMap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void formChart_Paint(object sender, PaintEventArgs e)
        {
            Rectangle myRec = new Rectangle(new Point(5, 5), new Size(20, 20));

            //myRec.
            /*RectangleF myRectangleF = new RectangleF(30F, 30F, 30F, 30F);

            Rectangle roundedRectangle = Rectangle.Round(myRectangleF);

            Pen redPen = new Pen(Color.Red, 4);
            e.Graphics.DrawRectangle(redPen, roundedRectangle);

            Rectangle truncatedRectangle = Rectangle.Truncate(myRectangleF);

            Pen whitePen = new Pen(Color.White, 4);
            e.Graphics.DrawRectangle(whitePen, truncatedRectangle);

            redPen.Dispose();
            whitePen.Dispose();*/
        }
    }
}
