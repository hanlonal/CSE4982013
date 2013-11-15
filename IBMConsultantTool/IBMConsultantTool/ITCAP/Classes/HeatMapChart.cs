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
        List<string> gapType = new List<string>();
        int domCount = 0;
        int capCount = 0;
        float avgGap = 0;
        int gapNum = 0;

        List<string> nameOfDomain = new List<string>();
        List<string> nameOfCap = new List<string>();
        List<string> capabilityGapType = new List<string>();
        List<float> capabilityGap = new List<float>();

        Label[] domTitle = new Label[1000];
        Button[] capTitle = new Button[1000];
        RectangleShape[] rectangle = new RectangleShape[1000];

        LineShape main = new LineShape();
        Label title = new Label();
        ShapeContainer linePacks = new ShapeContainer();

        public HeatMapChart(List<string> dom, List<string> cap, List<int> capNum, List<float> gap, 
            float avgGap, List<bool> notFocus, float totalGap, int numGap, List<string> gapType)
        {
            panelChart.Parent = this;
            panelList.Parent = this;

            panelChart.Size = new Size(784, 523);
            panelChart.Location = new Point(0, 32);
            panelChart.BackColor = Color.White;

            panelList.Size = new Size(784, 30);
            panelList.Location = new Point(0, 0);
            panelList.AutoScroll = true;
            panelList.BackColor = Color.LightGray;

            this.domCount = dom.Count;
            this.capCount = cap.Count;
            this.avgGap = avgGap;
            this.gapNum = numGap;
            this.gapType = gapType;

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

            this.SizeChanged += new EventHandler(HeatMapChart_SizeChanged);
            SizeChanged += new EventHandler(panelList_SizeChanged);
            SizeChanged += new EventHandler(panelChart_SizeChanged);
            //this.Load += new EventHandler(HeatMapChart_Load);

            InitializeComponent();
        }

        private void HeatMapChart_SizeChanged(object sender, EventArgs e)
        {
            Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
            if (Height > maxWindowTrackSize.Height)
                Height = maxWindowTrackSize.Height;
            if (Width > maxWindowTrackSize.Width)
                Width = maxWindowTrackSize.Width;
            
        }

        private void panelList_SizeChanged(object sender, EventArgs e)
        {
            panelList.Height = 30;
            panelList.Width = this.Width - 16;
        }

        private void panelChart_SizeChanged(object sender, EventArgs e)
        {
            panelChart.Width = this.Width - 16;
            panelChart.Height = this.Height - 77;
            HeatMapChart_Load(sender, e);
        }

        private void HeatMapChart_Load(object sender, EventArgs e)
        {
            title.ResetText();
            title.Hide();
            linePacks.Hide();
            for (int i = 0; i < domCount; i++)
            {
                if (domTitle[i] != null)
                    domTitle[i].Hide();
                if (rectangle[i] != null)
                    rectangle[i].Hide();
                for (int j = 0; j < capCount; j++)
                {
                    if (capTitle[j] != null)
                        capTitle[j].Hide();
                }
            }
            Label legend = new Label();
            legend.Parent = panelList;
            legend.Text = "Legend: ";
            legend.AutoSize = true;
            legend.Font = new Font("Arial", 14, FontStyle.Bold);
            legend.Location = new Point(5, 5);

            Label major = new Label();
            major.Parent = panelList;
            major.Text = "Major Gap";
            major.AutoSize = true;
            major.Font = new Font("Arial", 12, FontStyle.Bold);

            Label middle = new Label();
            middle.Parent = panelList;
            middle.Text = "Gap";
            middle.AutoSize = true;
            middle.Font = new Font("Arial", 12, FontStyle.Bold);

            Label small = new Label();
            small.Parent = panelList;
            small.Text = "Small/No Gap";
            small.AutoSize = true;
            small.Font = new Font("Arial", 12, FontStyle.Bold);

            Label notFocus = new Label();
            notFocus.Parent = panelList;
            notFocus.Text = "Not Focus";
            notFocus.AutoSize = true;
            notFocus.Font = new Font("Arial", 12, FontStyle.Bold);

            ShapeContainer canvas = new ShapeContainer();
            canvas.Parent = panelList;

            RectangleShape recMajor = new RectangleShape();
            recMajor.Parent = canvas;
            recMajor.FillColor = Color.Red;
            recMajor.FillStyle = FillStyle.Solid;
            recMajor.Size = new Size(20, 20);
            recMajor.Location = new Point(legend.Right + 20, 5);
            major.Location = new Point(recMajor.Right + 5, recMajor.Top);

            RectangleShape recMid = new RectangleShape();
            recMid.Parent = canvas;
            recMid.FillColor = Color.Yellow;
            recMid.FillStyle = FillStyle.Solid;
            recMid.Size = new Size(20, 20);
            recMid.Location = new Point(major.Right + 20, 5);
            middle.Location = new Point(recMid.Right + 5, recMid.Top);

            RectangleShape recSmall = new RectangleShape();
            recSmall.Parent = canvas;
            recSmall.FillColor = Color.GreenYellow;
            recSmall.FillStyle = FillStyle.Solid;
            recSmall.Size = new Size(20, 20);
            recSmall.Location = new Point(middle.Right + 20, 5);
            small.Location = new Point(recSmall.Right + 5, recSmall.Top);

            RectangleShape recNo = new RectangleShape();
            recNo.Parent = canvas;
            recNo.FillColor = Color.White;
            recNo.FillStyle = FillStyle.Solid;
            recNo.Size = new Size(20, 20);
            recNo.Location = new Point(small.Right + 20, 5);
            notFocus.Location = new Point(recNo.Right + 5, recNo.Top);

            float[] gaps = new float[capabilities.Count];

            System.Diagnostics.Trace.WriteLine("size: " + notFocus.Height.ToString());

            title.Parent = panelChart;
            title.Text = "Capability Gap";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(panelChart.Width / 2 - title.Width / 2, 5);
            title.Show();
            
            linePacks.Parent = panelChart;

            main.Parent = linePacks;
            main.BorderWidth = 2;
            main.X1 = title.Left;
            main.X2 = title.Right;
            main.Y1 = title.Bottom + 5;
            main.Y2 = title.Bottom + 5;
            linePacks.Show();

            int counting = 0;
            int lastVal = 0;
            int locBoxY = 0;
            Size size = new Size(250, 20);
            
            for (int i = 0; i < domCount; i++)
            {
                domTitle[i] = new Label();
                domTitle[i].Parent = panelChart;
                domTitle[i].AutoSize = true;
                domTitle[i].Text = domains[i];
                domTitle[i].Font = new Font("Arial", 14, FontStyle.Bold);
                domTitle[i].BackColor = Color.LightGreen;

                ShapeContainer con = new ShapeContainer();
                con.Parent = panelChart;

                rectangle[i] = new RectangleShape();
                rectangle[i].Parent = con;

                if (lastVal != 0)
                    domTitle[i].Location = new Point(panelChart.Width / 2 - domTitle[i].Width / 2, 10 + capTitle[lastVal-1].Bottom);
                else
                    domTitle[i].Location = new Point(panelChart.Width / 2 - domTitle[i].Width / 2, title.Bottom + 20 + i * 10);
                
                // System.Diagnostics.Trace.WriteLine("per domain: " + capPerDom[i].ToString());
                counting = capPerDom[i];

                for (int j = 0; j < counting; j++)
                {
                    capTitle[j+lastVal] = new Button();
                    capTitle[j+lastVal].Parent = panelChart;
                    capTitle[j + lastVal].Width = panelChart.Width / 2 - 20;
                    capTitle[j + lastVal].Height = 25;
                    capTitle[j + lastVal].AutoSize = true;
                    capTitle[j+lastVal].Text = capabilities[j+lastVal];
                    capTitle[j+lastVal].Font = new Font("Arial", 12, FontStyle.Bold);
                    capTitle[j + lastVal].Name = capabilities[j + lastVal];

                    nameOfDomain.Add(domains[i]);
                    nameOfCap.Add(capabilities[j + lastVal]);
                    capabilityGapType.Add(gapType[j + lastVal]);
                    capabilityGap.Add(gap[j + lastVal]);

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

                    if (gapType[j + lastVal] == "High")
                    {
                        capTitle[j + lastVal].BackColor = Color.OrangeRed;
                    }

                    else if (gapType[j + lastVal] == "Middle")
                    {
                        capTitle[j + lastVal].BackColor = Color.Yellow;
                    }

                    else if (gapType[j + lastVal] == "Low")
                    {
                        capTitle[j + lastVal].BackColor = Color.GreenYellow;
                    }

                    else
                    {
                        capTitle[j + lastVal].BackColor = Color.White;
                    }

                    capTitle[j + lastVal].BringToFront();

                    capTitle[j + lastVal].Click += new EventHandler(HeatMapChart_Click);
                    //if (gap[j + lastVal] > avgGap)
                    /*if (gap[j + lastVal] >= redGap)
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
                    //heightBox += capTitle[j + lastVal].Height;*/
                }
                lastVal += counting;

                rectangle[i].Size = new Size(panelChart.Width - 10, capTitle[lastVal-1].Bottom + 15 - domTitle[i].Top);

                if (i == 0)
                    rectangle[i].Location = new Point(5, title.Bottom + 10);
                else
                    rectangle[i].Location = new Point(5, locBoxY);
                locBoxY = rectangle[i].Bottom;

                rectangle[i].Enabled = false;
                rectangle[i].FillStyle = FillStyle.Solid;
                rectangle[i].FillColor = Color.LightGreen;
                rectangle[i].FillGradientStyle = FillGradientStyle.Vertical;
                rectangle[i].FillGradientColor = Color.White;
                rectangle[i].SendToBack();

                panelChart.Refresh();
            }

            Bitmap bmp = new Bitmap(panelChart.Width, panelChart.Height);
            panelChart.DrawToBitmap(bmp, panelChart.Bounds);
            bmp.Save(Directory.GetCurrentDirectory() + @"/Charts/" + "HeatMap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void HeatMapChart_Click(object sender, EventArgs e)
        {
            int current = 0;
            for (int i = 0; i < domCount; i++)
            {
                for (int j = 0; j < capCount; j++)
                {
                    if (sender.Equals(capTitle[j]))
                    {
                        current = j;
                        break;
                    }
                }
            }
            string info = "Domain:  " + nameOfDomain[current] + "\nCapability:  " + nameOfCap[current] + "\nGap:  " 
                + capabilityGap[current].ToString() + "\nGapType:  " + capabilityGapType[current];
            MessageBox.Show(info);
        }
    }
}
