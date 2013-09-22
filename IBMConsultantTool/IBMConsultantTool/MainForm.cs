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

        public CLIENT currentClient;

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
            if (currentClient == null)
            {
                MessageBox.Show("A BOM must be opened before adding Initiatives");
            }
            else
            {
                new AddInitiative(this).Show();
            }
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

        private void NewBOMButton_Click(object sender, EventArgs e)
        {
            new NewBOM(this).Show();
        }

        private void OpenBOMButton_Click(object sender, EventArgs e)
        {
            new OpenBOM(this).Show();
        }

        public int GetUniqueID(List<int> idList)
        {
            Random rnd = new Random();

            int id = rnd.Next();

            while (true)
            {
                var idQuery = from tmp in idList
                              where tmp == id
                              select tmp;

                if (idQuery.Count() == 0)
                {
                    break;
                }

                else
                {
                    id = rnd.Next();
                }
            }

            return id;
        }
    }
}
