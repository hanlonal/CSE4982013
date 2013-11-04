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

            ClientDataControl.LoadDatabase();
        }

        private void NewConsultButton_MouseLeave(object sender, EventArgs e)
        {
            NewConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void NewConsultButton_MouseEnter(object sender, EventArgs e)
        {
            NewConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void LoadConsultButton_MouseLeave(object sender, EventArgs e)
        {
            LoadConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void LoadConsultButton_MouseEnter(object sender, EventArgs e)
        {
            LoadConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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


        public static void ThreadProcMainForm()
        {
            ClientDataControl.newClient = true;
            Application.Run(new StartPage());
        }

        private void TrendAnalysisButton_Click(object sender, EventArgs e)
        {
            /*System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcCrossClientForm));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;*/
            MessageBox.Show("Trend Analysis Feature Coming Soon!", "Coming Soon");
        }

        public static void ThreadProcCrossClientForm()
        {
            Application.Run(new CUPETool());
        }

        private void LoadConsultButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcLoad));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        public static void ThreadProcLoad()
        {
            ClientDataControl.newClient = false;
            Application.Run(new StartPage());
        }

    }
   
}
