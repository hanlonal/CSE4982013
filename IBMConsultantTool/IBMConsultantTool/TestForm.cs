﻿using System;
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
        public TestForm()
        {
            if (!Directory.Exists("Charts"))
            {
                Directory.CreateDirectory("Charts");
            }

            InitializeComponent();

            OnlineModeCheckbox.Checked = ClientDataControl.isOnline;

            ClientDataControl.Client = null;
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
            NewClientForm ncf = new NewClientForm();
            ncf.ShowDialog();
            ncf.StartPosition = FormStartPosition.CenterParent;
            if (ClientDataControl.Client != null)
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMainForm));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
            }
        }


        public static void ThreadProcMainForm()
        {
            Application.Run(new StartPage());
        }

        private void TrendAnalysisButton_Click(object sender, EventArgs e)
        {
            if (OnlineModeCheckbox.Checked)
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNTrendAnalysis));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
                return;
            }

            else
            {
                MessageBox.Show("Must be online for Trend Analysis mode", "Error");
            }
           
        }

        public void RUNTrendAnalysis()
        {
            Application.Run(new AnalyticsForm());

        }


        public static void ThreadProcCrossClientForm()
        {
            Application.Run(new CUPETool());
        }

        private void LoadConsultButton_Click(object sender, EventArgs e)
        {
            LoadClientForm lcf = new LoadClientForm();
            lcf.ShowDialog();
            if (ClientDataControl.Client != null)
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMainForm));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
            }
        }

        private void OnlineModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (OnlineModeCheckbox.Checked)
            {
                OnlineModeCheckbox.Text = "Connecting...";
                OnlineModeCheckbox.Update();
                OnlineModeCheckbox.Checked = ClientDataControl.LoadDatabase();
            }

            else
            {
                ClientDataControl.LoadFileSystem();
                OnlineModeCheckbox.Checked = false;
            }
            OnlineModeCheckbox.Text = "Online Mode";
        }

        private void TestForm_MouseEnter(object sender, EventArgs e)
        {
            TrendAnalysisButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
            LoadConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
            NewConsultButton.BorderStyle = System.Windows.Forms.BorderStyle.None;

        }

        private void checkoutClientsForOfflineModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnlineModeCheckbox.Checked)
            {
                new CheckoutClients().ShowDialog();
            }

            else
            {
                MessageBox.Show("Must be online to checkout clients for offline mode", "Error");
            }
        }

    }
   
}
