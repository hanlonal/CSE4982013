using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    internal sealed class ClientDataControl
    {       

        //why is everything in this static?????? AK

        private static readonly ClientDataControl instance = new ClientDataControl();
        public static DataManager db;
        public static bool isOnline = true;
        private static List<Person> participants = new List<Person>();
        private static List<CupeData> cupeAnswers = new List<CupeData>();
        private static List<NewCategory> bomCategories = new List<NewCategory>();
        public static List<CupeQuestionStringData> cupeQuestions = new List<CupeQuestionStringData>();
        private static Client client;
        public static List<ScoringEntity> itcapQuestionsForDiscussion = new List<ScoringEntity>();

        private static string emailAddress = "";
        private static string emailPassword = "";
        private static string emailDisplay = "";






        private ClientDataControl() {}

        public static ClientDataControl Instance
        {
            get
            {
                return instance;
            }
        }

        public static bool LoadDatabase()
        {
            if (db == null || !isOnline)
            {
                try
                {
                    //throw new Exception();
                    db = new DBManager();
                    isOnline = true;
                }
                catch (Exception e)
                {
                    db = new FileManager();
                    isOnline = false;
                    MessageBox.Show("Could not reach database\n\n" + e.Message + "\n\n" + "Offline mode set", "Error");
                }
            }

            return isOnline;
        }

        public static void LoadFileSystem()
        {
            db = new FileManager();
            isOnline = false;
        }

        public static void LoadParticipants()
        {
            db.LoadParticipants();
        }

        public static void LoadCUPEQuestions(CUPETool cupeForm)
        {
            cupeQuestions = db.GetCUPESForClient();

            if(cupeQuestions.Count == 0 && MessageBox.Show("No CUPE was found for this client. Would you like to make one?", "New CUPE", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                cupeQuestions = db.GetCUPEQuestionStringDataTwenty();
                int questionNumber = 1;
                foreach (CupeQuestionStringData cqsd in cupeQuestions)
                {
                    db.AddCUPE(cqsd.QuestionText, Client.EntityObject, questionNumber++);
                }
                if (!ClientDataControl.db.SaveChanges())
                {
                    MessageBox.Show("Failed to create new CUPE for client", "Error");
                }
            }
        }

        public static void LoadNewCUPE20(CUPETool cupeForm)
        {
            db.ClearCUPE(Client.EntityObject);
            cupeQuestions = db.GetCUPEQuestionStringDataTwenty();
            int questionNumber = 1;
            foreach (CupeQuestionStringData cqsd in cupeQuestions)
            {
                db.AddCUPE(cqsd.QuestionText, Client.EntityObject, questionNumber++);
            }
            if (!ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Failed to create new CUPE for client", "Error");
            }
        }

        public static void LoadNewCUPE10(CUPETool cupeForm)
        {
            db.ClearCUPE(Client.EntityObject);
            cupeQuestions = db.GetCUPEQuestionStringDataTen();
            int questionNumber = 1;
            foreach (CupeQuestionStringData cqsd in cupeQuestions)
            {
                db.AddCUPE(cqsd.QuestionText, Client.EntityObject, questionNumber++);
            }
            if (!ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Failed to create new CUPE for client", "Error");
            }
        }

        public static bool AddParticipant(Person person)
        {
            participants.Add(person);

            return true;
        }

        public static List<Person> GetParticipants()
        {
            return participants;
        }

        public static List<CupeData> GetCupeAnswers()
        {
            return cupeAnswers;
        }

        public static bool SetCupeAnswers(List<CupeData> data)
        {
            cupeAnswers = data;

            return true;
        }

        public static bool AddCupeAnswers(CupeData data)
        {
            cupeAnswers.Add(data);

            return true;
        }

        public static bool AddCupeQuestion(CupeQuestionStringData data)
        {
            cupeQuestions.Add(data);

            return true;
        }

        public static List<CupeQuestionStringData> GetCupeQuestions()
        {
            return cupeQuestions;
        }

        public static bool SetParticipants(List<Person> people)
        {
            participants = people;
            return true;
        }

        public static bool AddCategory(NewCategory cat)
        {
            bomCategories.Add(cat);
            return true;
        }

        public static bool ResetCategories()
        {
            bomCategories = new List<NewCategory>();
            return true;
        }

        public string EmailPassword
        {
            get { return emailPassword; }
            set { emailPassword = value; }
        }
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }


        public static void SendEmailButton_Click()
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            FD.Title = "Select File to Add as an Attachment";
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            string fileToOpen = FD.FileName;

            System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

            var fromAddress = new MailAddress(emailAddress, "IBM");
            var toAddress = new MailAddress("connorsname@gmail.com", "Survey Participant");
            string fromPassword = emailPassword;
            string subject = "IBM Survey Request";
            string body = "Please download attatchment, fill out the form, and return to sender. Thank you!\n\n\n\n\nTeam IBM";

            var smtp = new SmtpClient
            {
                Host = "na.relay.ibm.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            foreach (var person in GetParticipants())
            {
                if (person.Email != null)
                {
                    toAddress = new MailAddress(person.Email.ToString(), "Survey Participant");
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(FD.FileName);
                        message.Attachments.Add(attachment);
                        smtp.Send(message);
                    }
                }
            }

            MessageBox.Show("Email Succesfully Sent");
        }

        public static Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public static string[] GetRegionNames()
        {
            return db.GetRegionNames().ToArray();
        }

        public static string[] GetBusinessTypeNames()
        {
            return db.GetBusinessTypeNames().ToArray();
        }

        public static bool NewClient(Client selectedClient)
        {
            client = db.AddClient(selectedClient);

            return (client != null && db.SaveChanges());
        }

        public static bool LoadClient(string clientName)
        {
            client = db.LoadClient(clientName);

            return (client != null && db.SaveChanges());
        }

        public static List<CupeQuestionData> GetCupeQuestionData()
        {
            return db.GetCUPEQuestionData();
        }

        public static void SaveCUPE()
        {
            foreach (CupeQuestionStringData stringData in cupeQuestions)
            {
                stringData.OriginalQuestionText = db.UpdateCUPE(stringData);
            }

            db.SaveCUPEParticipants();
            if (db.SaveChanges())
            {
                MessageBox.Show("Changes Saved Successfully", "Success");
                ClientDataControl.db.ClientCompletedCUPE(client.EntityObject);
            }
        }

        public string EmailDisplay
        {
            get { return ClientDataControl.emailDisplay; }
            set { ClientDataControl.emailDisplay = value; }
        }

        public static void SaveParticipantsToDB()
        {
            db.SaveCUPEParticipants();
        }

        public static List<NewCategory> GetBomCategories()
        {
            return bomCategories;

        }
    }
}
