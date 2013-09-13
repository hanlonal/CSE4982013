using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace IBMConsultantTool
{
    public partial class TestForm : Form
    {

        TextReader reader;
        string textLine;

        public TestForm()
        {
            InitializeComponent();

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            reader = new StreamReader("names.txt");


            while ((textLine = reader.ReadLine()) != null)
            {
                Debug.Print(textLine);
            }
        }


        private void NewConsultButton_MouseLeave(object sender, EventArgs e)
        {
            NewConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void NewConsultButton_MouseEnter(object sender, EventArgs e)
        {
            NewConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void TrendAnalysisButton_MouseEnter(object sender, EventArgs e)
        {
            TrendAnalysisButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void TrendAnalysisButton_MouseLeave(object sender, EventArgs e)
        {
            TrendAnalysisButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void NewConsultButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMainForm));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
            
        }


        //asdfjsdkldfjlksadjf
        public static void ThreadProcMainForm()
        {
            Application.Run(new MainForm());
        }

 


    }
   
}
