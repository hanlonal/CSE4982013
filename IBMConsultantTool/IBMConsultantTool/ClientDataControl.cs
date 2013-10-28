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
        private static readonly ClientDataControl instance = new ClientDataControl();
        private static DataManager db;
        private static bool isOnline;
        private static List<Person> participants = new List<Person>();
        private static List<CupeData> cupeAnswers = new List<CupeData>();
        private static List<NewCategory> bomCategories = new List<NewCategory>();
        public static List<CupeQuestionStringData> cupeQuestions = new List<CupeQuestionStringData>();

        private ClientDataControl() { }

        public static ClientDataControl Instance
        {
            get
            {
                return instance;
            }
        }

        public static bool LoadDatabase()
        {
            try
            {
                db = new DBManager();
                isOnline = true;
            }
            catch (Exception e)
            {
                db = new FileManager();
                isOnline = false;
                MessageBox.Show("Could not reach database\n\n" + e.Message + "\n\n" + "Offline mode set", "Error");
            }


            return isOnline;
            ///categoryNames.Items.AddRange(db.GetCategoryNames());        
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

            var fromAddress = new MailAddress("cse498ibm@gmail.com", "Team IBM Capstone");
            var toAddress = new MailAddress("connorsname@gmail.com", "Survey Participant");
            const string fromPassword = "CSE498-38734";
            const string subject = "IBM Survey Request";
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

    }
}
