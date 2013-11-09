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
    public partial class StartPage : Form
    {
        Client currentClient;

        public StartPage()
        {
            if (!Directory.Exists("Charts"))
            {
                Directory.CreateDirectory("Charts");
            }
            InitializeComponent();
            if (ClientDataControl.newClient)
            {
                NewClientForm form = new NewClientForm();
                form.Owner = this;
                form.ShowDialog();
            }

            else
            {
                LoadClientForm form = new LoadClientForm();
                form.Owner = this;
                form.ShowDialog();
            }
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            if (ClientDataControl.Client == null)
            {   System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcReset));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
            }
            //this.Controls.Add(form);
            clientNameLabel.Text = ClientDataControl.Client.Name;
            clientTypeLabel.Text = ClientDataControl.Client.BusinessType;
            dateStartedLabel.Text = ClientDataControl.Client.StartDate.ToString().Split(' ')[0];
            clientLocationLabel.Text = ClientDataControl.Client.Country.ToString();
            
        }

        public static void ThreadProcReset()
        {
            Application.Run(new TestForm());
        }
        private void BindLabels()
        {
            clientNameLabel.DataBindings.Add("Text", ClientDataControl.Client.Name, "Name");
            //clientLocationLabel.DataBindings.Add("Text", currentClient, "Location");
            clientTypeLabel.DataBindings.Add("Text", currentClient, "BusinessType");
            dateStartedLabel.DataBindings.Add("Text", currentClient, "StartDate");
        }

        public string ClientNameLabel
        {
            get { return clientNameLabel.Text; }
            set { clientNameLabel.Text = value; }
        }
        public Client CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value; BindLabels(); }
        }
        #region Run Bom Tool

        private void runBomButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNBOM));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNBOM()
        {
            Application.Run(new BOMTool());
        }
        #endregion

        #region Run CUPE Tool

        private void runCupeButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNCUPE));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNCUPE()
        {
            Application.Run(new CUPETool());
        }
        #endregion

        private void runITCapButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNITCAP));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }
        private void RUNITCAP()
        {
            Application.Run(new ITCapTool());
        }




    }
}
