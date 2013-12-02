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

        string closeState = "close";

        bool browser = false;

        public StartPage()
        {
            InitializeComponent();

            string path = "C:\\Clients\\";
            if (!Directory.Exists(@"C:\\" + "Clients"))
                Directory.CreateDirectory(@"C:\\" + "Clients");
            if (!Directory.Exists(@"C:\\Clients\\" + ClientDataControl.Client.Name))
                Directory.CreateDirectory(path + ClientDataControl.Client.Name);
            if (!Directory.Exists(@"C:\\Clients\\" + ClientDataControl.Client.Name + "\\" + "Charts"))
                Directory.CreateDirectory(@"C:\\Clients\\" + ClientDataControl.Client.Name + "\\Charts");

            ClientDataControl.Client.FilePath = "C:\\Clients\\" + ClientDataControl.Client.Name + "\\Charts";
            textFilePathInfo.Text = ClientDataControl.Client.FilePath;

            this.FormClosed += new FormClosedEventHandler(StartPage_FormClosed);
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            //this.Controls.Add(form);
            clientNameLabel.Text = ClientDataControl.Client.Name;
            clientTypeLabel.Text = ClientDataControl.Client.BusinessType;
            dateStartedLabel.Text = ClientDataControl.Client.StartDate.ToString().Split(' ')[0];
            clientLocationLabel.Text = ClientDataControl.Client.Region.ToString();

        }

        private void StartPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (closeState == "close")
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNTEST));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
        }

        private void RUNTEST()
        {
            Application.Run(new TestForm());
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
            if (!browser)
            {
                if (MessageBox.Show("You did not choose the File Path to save charts from each tool.\n\nDo you want to use the default File Path for saving charts from each tool?", "File Path Choose", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    browser = true;
            }
            if(browser)
            {
                closeState = "BOM";
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNBOM));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
                return;
            }
        }

        private void RUNBOM()
        {
            Application.Run(new BOMTool());
        }
        #endregion

        #region Run CUPE Tool

        private void runCupeButton_Click(object sender, EventArgs e)
        {
            if (!browser)
            {
                if (MessageBox.Show("You did not choose the File Path to save charts from each tool.\n\nDo you want to use the default File Path for saving charts from each tool?", "File Path Choose", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    browser = true;
            }
            if(browser)
            {
                closeState = "CUPE";
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNCUPE));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
                return;
            }
        }

        private void RUNCUPE()
        {
            Application.Run(new CUPETool());
        }
        #endregion

        #region Run IT Capability Tool

        private void runITCapButton_Click(object sender, EventArgs e)
        {
            if (!browser)
            {
                if (MessageBox.Show("You did not choose the File Path to save charts from each tool.\n\nDo you want to use the default File Path for saving charts from each tool?", "File Path Choose", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    browser = true;
            }
            if(browser)
            {
                closeState = "ITCap";
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNITCAP));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                this.Close();
                return;
            }
        }
        private void RUNITCAP()
        {
            Application.Run(new ITCapTool());
        }

        #endregion

        private void agreeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            string path = "C:\\Clients\\";
            if (!Directory.Exists(@"C:\\" + "Clients"))
                Directory.CreateDirectory(@"C:\\" + "Clients");
            if (!Directory.Exists(@"C:\\Clients\\" + ClientDataControl.Client.Name))
                Directory.CreateDirectory(path + ClientDataControl.Client.Name);
            if (!Directory.Exists(@"C:\\Clients\\" + ClientDataControl.Client.Name + "\\" + "Charts"))
                Directory.CreateDirectory(@"C:\\Clients\\" + ClientDataControl.Client.Name + "\\" + "Charts");

            ClientDataControl.Client.FilePath = "C:\\Clients\\" + ClientDataControl.Client.Name + "\\Charts";
            textFilePathInfo.Text = ClientDataControl.Client.FilePath;

            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            if (userClickedOK == DialogResult.OK)
            {
                ClientDataControl.Client.FilePath = openFileDialog1.SelectedPath.ToString();
                System.Diagnostics.Trace.WriteLine("path: " + openFileDialog1.SelectedPath.ToString());

                textFilePathInfo.Text = ClientDataControl.Client.FilePath;

                browser = true;
            }
        }
    }
}
