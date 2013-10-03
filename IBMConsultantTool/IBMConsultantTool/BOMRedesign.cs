using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace IBMConsultantTool
{
    public partial class BOMRedesign : Form
    {
        DBManager db;        
        List<Category> categories = new List<Category>();
        List<Color> colors = new List<Color>();
        private int categoryCount = 0;
        private Category lastFocused;
        
        public BOMRedesign()
        {
           // PopulateColorsList();
            InitializeComponent();

            db = new DBManager();

            categoryNames.Items.AddRange(db.GetCategoryNames());
        }

        private void categoryAddButton_Click(object sender, EventArgs e)
        {
            Category category = new Category(this, categoryNames.Text);
            categories.Add(category);
            categoryCount++;
            category.Click += new EventHandler(category_Click);
        }

        private void category_Click(object sender, EventArgs e)
        {
            Category cat = (Category)sender;
            lastFocused = cat;
            Console.WriteLine("clicked on category:" + cat.Name);
        }

        public int CategoryCount
        {
            get
            {
                return categoryCount;
            }
        }

        private void initiativeAddButton_Click(object sender, EventArgs e)
        {
            lastFocused.LastClicked.AddInitiative(initiativeNames.Text);
            INITIATIVE initiative;
            if (!db.GetInitiative(initiativeNames.Text, out initiative))
            {
                initiative = new INITIATIVE();
                initiative.NAME = initiativeNames.Text;
                BUSINESSOBJECTIVE objective;
                if(!db.GetObjective(objectiveNames.Text, out objective))
                {
                    objective = new BUSINESSOBJECTIVE();
                    objective.NAME = objectiveNames.Text;
                    CATEGORY category;
                    if(!db.GetCategory(categoryNames.Text, out category))
                    {
                        category = new CATEGORY();
                        category.NAME = categoryNames.Text;
                        db.AddCategory(category);
                    }

                    objective.CATEGORY = category;
                    db.AddObjective(objective);
                }

                initiative.BUSINESSOBJECTIVE = objective;
                db.AddInitiative(initiative);
            }

            db.SaveChanges();
        }

        private void objectiveAddButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(categoryCount.ToString());
            lastFocused.AddObjective(objectiveNames.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            FD.Title = "Select File to Add as an Attachment";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
            }

            var fromAddress = new MailAddress("cse498ibm@gmail.com", "Team IBM Capstone");
            var toAddress = new MailAddress("connorsname@gmail.com", "Survey Participant");
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
                attachment = new System.Net.Mail.Attachment(FD.FileName);
                message.Attachments.Add(attachment);
                smtp.Send(message);
            }

        }
        public Category LastClickedCategory
        {
            set
            {
                lastFocused = value;
            }
        }

        private void dataInputButton_Click(object sender, EventArgs e)
        {
            DataEntryForm form = new DataEntryForm(this);
            form.Show();
        }

        public List<Category> Categories
        {
            get
            {
                return categories;
            }
        }

        private void BomSurveyButton_Click(object sender, EventArgs e)
        {
            SurveyGenerator BomSurvGen = new SurveyGenerator();
            BomSurvGen.CreateBomSurvey(this.categories);
        }
        public Panel MainWorkspace
        {
            get
            {
                return mainWorkspace;
            }
        }

        private void categoryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedCategory();
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            ChangedCategory();
        }

        public void ChangedCategory()
        {
            CATEGORY category;

            objectiveNames.Items.Clear();
            objectiveNames.Text = "<Select Objective>";
            initiativeNames.Items.Clear();
            initiativeNames.Text = "";
            if (db.GetCategory(categoryNames.Text, out category))
            {
                objectiveNames.Items.AddRange((from ent in category.BUSINESSOBJECTIVE
                                               select ent.NAME.TrimEnd()).ToArray());
            }
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedObjective();
        }

        private void objectiveNames_LostFocus(object sender, EventArgs e)
        {
            ChangedObjective();
        }

        private void ChangedObjective()
        {
            BUSINESSOBJECTIVE objective;

            initiativeNames.Items.Clear();
            initiativeNames.Text = "<Select Initiative>";
            if (db.GetObjective(objectiveNames.Text, out objective))
            {
                initiativeNames.Items.AddRange((from ent in objective.INITIATIVE
                                                select ent.NAME.TrimEnd()).ToArray());
            }
        }

     /*   private void createPPTButton_Click(object sender, EventArgs e)
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



                //saveDialog.FileName = "untitled";
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveDialog.FileName);
                SaveFile.WriteLine(lines);
                SaveFile.Close();
            }
        }*/

        

    } // end of class
}
