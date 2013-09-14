using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IBMConsultantTool
{
    public partial class MainForm : Form
    {
        public int numberOfColumns = 1;
        public SAMPLEEntities dbo;

        public MainForm()
        {
            InitializeComponent();

            dbo = new SAMPLEEntities();
        }

        private void BOMBubbleChartButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcBOMBubbleChart));
            t.Start();
            return;
        }
       
        public static void ThreadProcBOMBubbleChart()
        {
            Application.Run(new BOMBubbleChart());
        }

        /*private void CUPENOQUpdateButton_Click(object sender, EventArgs e)
        {
            int newNumberOfColumns = 1;
            try
            {
                newNumberOfColumns = Convert.ToInt32(NumberOfQuestionsTextBox.Text);
            }

            catch
            {
                return;
            }

            if (newNumberOfColumns < 1)
            {
                return;
            }

            CUPETable.ColumnCount = newNumberOfColumns + 1;

            while (numberOfColumns <= newNumberOfColumns)
            {
                CUPETable.Columns[numberOfColumns].HeaderText = "Q" + numberOfColumns.ToString();
                numberOfColumns++;
            }

            numberOfColumns = newNumberOfColumns;
        }*/

        private void BOMAddInitiativeButton_Click(object sender, EventArgs e)
        {
            new AddInitiative(this).Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void New_Click(object sender, EventArgs e)
        {

        }

        private void CUPE_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NumberOfQuestionsLabel_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenAnalytics_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCUPEAnalForm));
            t.Start();
            return;
        }

        public static void ThreadCUPEAnalForm()
        {
            Application.Run(new CUPEAnalytics());
        }

        private void BOMTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Thread newThread = new Thread(new ThreadStart(SaveDialogThread));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();  
            
        }

        void SaveDialogThread()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "comma|*.csv";

            string lines = "";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //string linesstr ="";

                for (int i = 0; i < BOMTable.RowCount; i++)
                {
                    for (int j = 0; j < BOMTable.Rows[i].Cells.Count; j++)
                    {
                        lines += (string)BOMTable.Rows[i].Cells[j].Value + ", ";
                    }
                }

                //saveDialog.FileName = "untitled";
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveDialog.FileName);
                SaveFile.WriteLine(lines);
                SaveFile.Close();
            }



        }

        private void MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SendBOMButton_Click(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress("cse498ibm@gmail.com", "Team IBM Capstone");
            var toAddress = new MailAddress(SendToEmail.Text, "Survey Participant");
            const string fromPassword = "CSE498-38734";
            const string subject = "IBM BOM Survey Request";
            const string body = "Please download attatchment, fill out the form, and submit. Thank you!\n\n\n\n\nTeam IBM";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("Test.txt");
                message.Attachments.Add(attachment);
                smtp.Send(message);
                SendToEmail.Text = "";
            }
        }


    }
}
