using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IBMConsultantTool
{
    public partial class ITProviderRelationshipGraph : Form
    {
        // Location of Each Point
        Point zero = new Point(838 - 16, 428 - 16);
        Point zero_one = new Point(707 - 16, 420 - 16);
        Point zero_two = new Point(651 - 16, 412 - 16);
        Point zero_three = new Point(597 - 16, 404 - 16);
        Point zero_four = new Point(557 - 16, 395 - 16);
        Point zero_five = new Point(520 - 16, 386 - 16);
        Point zero_six = new Point(489 - 16, 377 - 16);
        Point zero_seven = new Point(707 - 16, 368 - 16);
        Point zero_eight = new Point(651 - 16, 359 - 16);
        Point zero_nine = new Point(408 - 16, 350 - 16);

        Point one = new Point(391 - 16, 341 - 16);
        Point one_one = new Point(372 - 16, 333 - 16);
        Point one_two = new Point(355 - 16, 325 - 16);
        Point one_three = new Point(338 - 16, 317 - 16);
        Point one_four = new Point(322 - 16, 308 - 16);
        Point one_five = new Point(309 - 16, 299 - 16);
        Point one_six = new Point(294 - 16, 290 - 16);
        Point one_seven = new Point(281 - 16, 281 - 16);
        Point one_eight = new Point(269 - 16, 272 - 16);
        Point one_nine = new Point(260 - 16, 263 - 16);

        Point two = new Point(249 - 16, 254 - 16);
        Point two_one = new Point(242 - 16, 246 - 16);
        Point two_two = new Point(233 - 16, 238 - 16);
        Point two_three = new Point(226 - 16, 230 - 16);
        Point two_four = new Point(218 - 16, 221 - 16);
        Point two_five = new Point(210 - 16, 212 - 16);
        Point two_six = new Point(203 - 16, 203 - 16);
        Point two_seven = new Point(198 - 16, 194 - 16);
        Point two_eight = new Point(191 - 16, 185 - 16);
        Point two_nine = new Point(184 - 16, 176 - 16);

        Point three = new Point(180 - 16, 167 - 16);
        Point three_one = new Point(176 - 16, 159 - 16);
        Point three_two = new Point(172 - 16, 151 - 16);
        Point three_three = new Point(168 - 16, 143 - 16);
        Point three_four = new Point(164 - 16, 134 - 16);
        Point three_five = new Point(161 - 16, 125 - 16);
        Point three_six = new Point(158 - 16, 116 - 16);
        Point three_seven = new Point(156 - 16, 107 - 16);
        Point three_eight = new Point(154 - 16, 98 - 16);
        Point three_nine = new Point(151 - 16, 89 - 16);

        Point four = new Point(150 - 16, 80 - 16);

        PictureBox pic1 = new PictureBox();
        PictureBox pic2 = new PictureBox();
        PictureBox pic3 = new PictureBox();
        PictureBox pic4 = new PictureBox();

        public ITProviderRelationshipGraph(double curBus, double futBus, double curIT, double futIT)
        {
            InitializeComponent();

            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();

            label1.Parent = this;
            label2.Parent = this;
            label3.Parent = this;
            label4.Parent = this;

            pic1.Parent = this;
            pic2.Parent = this;
            pic3.Parent = this;
            pic4.Parent = this;

            pic1.Size = new Size(32, 33);
            pic2.Size = new Size(32, 33);
            pic3.Size = new Size(32, 33);
            pic4.Size = new Size(32, 33);

            pic1.Image = Properties.Resources.markBusinessCurrent;
            pic2.Image = Properties.Resources.markBusinessFuture;
            pic3.Image = Properties.Resources.markITCurrent;
            pic4.Image = Properties.Resources.markITFuture;

            pic1.BackColor = Color.Transparent;
            pic2.BackColor = Color.Transparent;
            pic3.BackColor = Color.Transparent;
            pic4.BackColor = Color.Transparent;

            pic1.Visible = true;
            pic2.Visible = true;
            pic3.Visible = true;
            pic4.Visible = true;

            pic1.Enabled = true;
            pic2.Enabled = true;
            pic3.Enabled = true;
            pic4.Enabled = true;

            pic1.BringToFront();
            pic2.BringToFront();
            pic3.BringToFront();
            pic4.BringToFront();
            
            double busCurrent = 0;
            double busFuture = 0;
            double ITCurrent = 0;
            double ITFuture = 0;

            double temp = curBus;
            decimal tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 1);
            busCurrent = (double)tmp;

            temp = futBus;
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 1);
            busFuture = (double)tmp;

            temp = curIT;
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 1);
            ITCurrent = (double)tmp;

            temp = futIT;
            tmp = Convert.ToDecimal(temp);
            tmp = Math.Round(tmp, 1);
            ITFuture = (double)tmp;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label4.AutoSize = true;

            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;

            string specifier = String.Format("{0}{1}", "F", 1);
            label1.Text = "(" + busCurrent.ToString(specifier) + ")";
            label2.Text = "(" + busFuture.ToString(specifier) + ")";
            label3.Text = "(" + ITCurrent.ToString(specifier) + ")";
            label4.Text = "(" + ITFuture.ToString(specifier) + ")";

            label1.Location = new Point(850, 106);
            label2.Location = new Point(850, 153);
            label3.Location = new Point(850, 200);
            label4.Location = new Point(850, 245);

            label1.Font = new Font("Arial", 13);
            label2.Font = new Font("Arial", 13);
            label3.Font = new Font("Arial", 13);
            label4.Font = new Font("Arial", 13);

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;

            label1.BringToFront();
            label2.BringToFront();
            label3.BringToFront();
            label4.BringToFront();

            DrawStars(busCurrent, busFuture, ITCurrent, ITFuture);
        }

        public void DrawStars(double curB, double futB, double itCur, double itFu)
        {
            #region Business Current Value

            // Business Current Value
            if (curB == 0)
                this.pic1.Location = zero;
            if (curB == 0.1)
                this.pic1.Location = zero_one;
            if (curB == 0.2)
                this.pic1.Location = zero_two;
            if (curB == 0.3)
                this.pic1.Location = zero_three;
            if (curB == 0.4)
                this.pic1.Location = zero_four;
            if (curB == 0.5)
                this.pic1.Location = zero_five;
            if (curB == 0.6)
                this.pic1.Location = zero_six;
            if (curB == 0.7)
                this.pic1.Location = zero_seven;
            if (curB == 0.8)
                this.pic1.Location = zero_eight;
            if (curB == 0.9)
                this.pic1.Location = zero_nine;
            if (curB == 1.0)
                this.pic1.Location = one;
            if (curB == 1.1)
                this.pic1.Location = one_one;
            if (curB == 1.2)
                this.pic1.Location = one_two;
            if (curB == 1.3)
                this.pic1.Location = one_three;
            if (curB == 1.4)
                this.pic1.Location = one_four;
            if (curB == 1.5)
                this.pic1.Location = one_five;
            if (curB == 1.6)
                this.pic1.Location = one_six;
            if (curB == 1.7)
                this.pic1.Location = one_seven;
            if (curB == 1.8)
                this.pic1.Location = one_eight;
            if (curB == 1.9)
                this.pic1.Location = one_nine;
            if (curB == 2.0)
                this.pic1.Location = two;
            if (curB == 2.1)
                this.pic1.Location = two_one;
            if (curB == 2.2)
                this.pic1.Location = two_two;
            if (curB == 2.3)
                this.pic1.Location = two_three;
            if (curB == 2.4)
                this.pic1.Location = two_four;
            if (curB == 2.5)
                this.pic1.Location = two_five;
            if (curB == 2.6)
                this.pic1.Location = two_six;
            if (curB == 2.7)
                this.pic1.Location = two_seven;
            if (curB == 2.8)
                this.pic1.Location = two_eight;
            if (curB == 2.9)
                this.pic1.Location = two_nine;
            if (curB == 3.0)
                this.pic1.Location = three;
            if (curB == 3.1)
                this.pic1.Location = three_one;
            if (curB == 3.2)
                this.pic1.Location = three_two;
            if (curB == 3.3)
                this.pic1.Location = three_three;
            if (curB == 3.4)
                this.pic1.Location = three_four;
            if (curB == 3.5)
                this.pic1.Location = three_five;
            if (curB == 3.6)
                this.pic1.Location = three_six;
            if (curB == 3.7)
                this.pic1.Location = three_seven;
            if (curB == 3.8)
                this.pic1.Location = three_eight;
            if (curB == 3.9)
                this.pic1.Location = three_nine;
            if (curB == 4.0)
                this.pic1.Location = four;

            #endregion

            #region Business Future Value

            // Business Future Value
            if (futB == 0)
                this.pic2.Location = zero;
            if (futB == 0.1)
                this.pic2.Location = zero_one;
            if (futB == 0.2)
                this.pic2.Location = zero_two;
            if (futB == 0.3)
                this.pic2.Location = zero_three;
            if (futB == 0.4)
                this.pic2.Location = zero_four;
            if (futB == 0.5)
                this.pic2.Location = zero_five;
            if (futB == 0.6)
                this.pic2.Location = zero_six;
            if (futB == 0.7)
                this.pic2.Location = zero_seven;
            if (futB == 0.8)
                this.pic2.Location = zero_eight;
            if (futB == 0.9)
                this.pic2.Location = zero_nine;
            if (futB == 1.0)
                this.pic2.Location = one;
            if (futB == 1.1)
                this.pic2.Location = one_one;
            if (futB == 1.2)
                this.pic2.Location = one_two;
            if (futB == 1.3)
                this.pic2.Location = one_three;
            if (futB == 1.4)
                this.pic2.Location = one_four;
            if (futB == 1.5)
                this.pic2.Location = one_five;
            if (futB == 1.6)
                this.pic2.Location = one_six;
            if (futB == 1.7)
                this.pic2.Location = one_seven;
            if (futB == 1.8)
                this.pic2.Location = one_eight;
            if (futB == 1.9)
                this.pic2.Location = one_nine;
            if (futB == 2.0)
                this.pic2.Location = two;
            if (futB == 2.1)
                this.pic2.Location = two_one;
            if (futB == 2.2)
                this.pic2.Location = two_two;
            if (futB == 2.3)
                this.pic2.Location = two_three;
            if (futB == 2.4)
                this.pic2.Location = two_four;
            if (futB == 2.5)
                this.pic2.Location = two_five;
            if (futB == 2.6)
                this.pic2.Location = two_six;
            if (futB == 2.7)
                this.pic2.Location = two_seven;
            if (futB == 2.8)
                this.pic2.Location = two_eight;
            if (futB == 2.9)
                this.pic2.Location = two_nine;
            if (futB == 3.0)
                this.pic2.Location = three;
            if (futB == 3.1)
                this.pic2.Location = three_one;
            if (futB == 3.2)
                this.pic2.Location = three_two;
            if (futB == 3.3)
                this.pic2.Location = three_three;
            if (futB == 3.4)
                this.pic2.Location = three_four;
            if (futB == 3.5)
                this.pic2.Location = three_five;
            if (futB == 3.6)
                this.pic2.Location = three_six;
            if (futB == 3.7)
                this.pic2.Location = three_seven;
            if (futB == 3.8)
                this.pic2.Location = three_eight;
            if (futB == 3.9)
                this.pic2.Location = three_nine;
            if (futB == 4.0)
                this.pic2.Location = four;

            #endregion

            #region IT Current Value

            // IT Current Value
            if (itCur == 0)
                this.pic3.Location = zero;
            if (itCur == 0.1)
                this.pic3.Location = zero_one;
            if (itCur == 0.2)
                this.pic3.Location = zero_two;
            if (itCur == 0.3)
                this.pic3.Location = zero_three;
            if (itCur == 0.4)
                this.pic3.Location = zero_four;
            if (itCur == 0.5)
                this.pic3.Location = zero_five;
            if (itCur == 0.6)
                this.pic3.Location = zero_six;
            if (itCur == 0.7)
                this.pic3.Location = zero_seven;
            if (itCur == 0.8)
                this.pic3.Location = zero_eight;
            if (itCur == 0.9)
                this.pic3.Location = zero_nine;
            if (itCur == 1.0)
                this.pic3.Location = one;
            if (itCur == 1.1)
                this.pic3.Location = one_one;
            if (itCur == 1.2)
                this.pic3.Location = one_two;
            if (itCur == 1.3)
                this.pic3.Location = one_three;
            if (itCur == 1.4)
                this.pic3.Location = one_four;
            if (itCur == 1.5)
                this.pic3.Location = one_five;
            if (itCur == 1.6)
                this.pic3.Location = one_six;
            if (itCur == 1.7)
                this.pic3.Location = one_seven;
            if (itCur == 1.8)
                this.pic3.Location = one_eight;
            if (itCur == 1.9)
                this.pic3.Location = one_nine;
            if (itCur == 2.0)
                this.pic3.Location = two;
            if (itCur == 2.1)
                this.pic3.Location = two_one;
            if (itCur == 2.2)
                this.pic3.Location = two_two;
            if (itCur == 2.3)
                this.pic3.Location = two_three;
            if (itCur == 2.4)
                this.pic3.Location = two_four;
            if (itCur == 2.5)
                this.pic3.Location = two_five;
            if (itCur == 2.6)
                this.pic3.Location = two_six;
            if (itCur == 2.7)
                this.pic3.Location = two_seven;
            if (itCur == 2.8)
                this.pic3.Location = two_eight;
            if (itCur == 2.9)
                this.pic3.Location = two_nine;
            if (itCur == 3.0)
                this.pic3.Location = three;
            if (itCur == 3.1)
                this.pic3.Location = three_one;
            if (itCur == 3.2)
                this.pic3.Location = three_two;
            if (itCur == 3.3)
                this.pic3.Location = three_three;
            if (itCur == 3.4)
                this.pic3.Location = three_four;
            if (itCur == 3.5)
                this.pic3.Location = three_five;
            if (itCur == 3.6)
                this.pic3.Location = three_six;
            if (itCur == 3.7)
                this.pic3.Location = three_seven;
            if (itCur == 3.8)
                this.pic3.Location = three_eight;
            if (itCur == 3.9)
                this.pic3.Location = three_nine;
            if (itCur == 4.0)
                this.pic3.Location = four;

            #endregion

            #region IT Future Value

            // IT Future Value
            if (itFu == 0)
                this.pic4.Location = zero;
            if (itFu == 0.1)
                this.pic4.Location = zero_one;
            if (itFu == 0.2)
                this.pic4.Location = zero_two;
            if (itFu == 0.3)
                this.pic4.Location = zero_three;
            if (itFu == 0.4)
                this.pic4.Location = zero_four;
            if (itFu == 0.5)
                this.pic4.Location = zero_five;
            if (itFu == 0.6)
                this.pic4.Location = zero_six;
            if (itFu == 0.7)
                this.pic4.Location = zero_seven;
            if (itFu == 0.8)
                this.pic4.Location = zero_eight;
            if (itFu == 0.9)
                this.pic4.Location = zero_nine;
            if (itFu == 1.0)
                this.pic4.Location = one;
            if (itFu == 1.1)
                this.pic4.Location = one_one;
            if (itFu == 1.2)
                this.pic4.Location = one_two;
            if (itFu == 1.3)
                this.pic4.Location = one_three;
            if (itFu == 1.4)
                this.pic4.Location = one_four;
            if (itFu == 1.5)
                this.pic4.Location = one_five;
            if (itFu == 1.6)
                this.pic4.Location = one_six;
            if (itFu == 1.7)
                this.pic4.Location = one_seven;
            if (itFu == 1.8)
                this.pic4.Location = one_eight;
            if (itFu == 1.9)
                this.pic4.Location = one_nine;
            if (itFu == 2.0)
                this.pic4.Location = two;
            if (itFu == 2.1)
                this.pic4.Location = two_one;
            if (itFu == 2.2)
                this.pic4.Location = two_two;
            if (itFu == 2.3)
                this.pic4.Location = two_three;
            if (itFu == 2.4)
                this.pic4.Location = two_four;
            if (itFu == 2.5)
                this.pic4.Location = two_five;
            if (itFu == 2.6)
                this.pic4.Location = two_six;
            if (itFu == 2.7)
                this.pic4.Location = two_seven;
            if (itFu == 2.8)
                this.pic4.Location = two_eight;
            if (itFu == 2.9)
                this.pic4.Location = two_nine;
            if (itFu == 3.0)
                this.pic4.Location = three;
            if (itFu == 3.1)
                this.pic4.Location = three_one;
            if (itFu == 3.2)
                this.pic4.Location = three_two;
            if (itFu == 3.3)
                this.pic4.Location = three_three;
            if (itFu == 3.4)
                this.pic4.Location = three_four;
            if (itFu == 3.5)
                this.pic4.Location = three_five;
            if (itFu == 3.6)
                this.pic4.Location = three_six;
            if (itFu == 3.7)
                this.pic4.Location = three_seven;
            if (itFu == 3.8)
                this.pic4.Location = three_eight;
            if (itFu == 3.9)
                this.pic4.Location = three_nine;
            if (itFu == 4.0)
                this.pic4.Location = four;

            #endregion
        }
    }
}
