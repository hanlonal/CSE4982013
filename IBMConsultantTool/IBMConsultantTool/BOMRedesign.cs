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
        public DBManager db;
        public CLIENT client;
        public List<Category> categories = new List<Category>();
        List<Color> colors = new List<Color>();
        public int categoryCount = 0;
        private Category lastFocused;
        
        public BOMRedesign()
        {
           // PopulateColorsList();
            InitializeComponent();

            db = new DBManager();

            //new ChooseClient(this).ShowDialog();

            categoryNames.Items.AddRange(db.GetCategoryNames());
        }

        private void categoryAddButton_Click(object sender, EventArgs e)
        {
            Category category = new Category(this, categoryNames.Text);
            categories.Add(category);
            categoryCount++;
            category.Click += new EventHandler(category_Click);
        }

        public void category_Click(object sender, EventArgs e)
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
            string catName;
            string busName;
            string iniName = initiativeNames.Text.Trim();
            INITIATIVE initiative;
            if (!db.GetInitiative(iniName, out initiative))
            {
                initiative = new INITIATIVE();
                initiative.NAME = iniName;
                BUSINESSOBJECTIVE objective;
                busName = objectiveNames.Text.Trim();
                if(!db.GetObjective(busName, out objective))
                {
                    objective = new BUSINESSOBJECTIVE();
                    objective.NAME = objectiveNames.Text.Trim();
                    CATEGORY category;
                    catName = categoryNames.Text.Trim();
                    if(!db.GetCategory(catName, out category))
                    {
                        category = new CATEGORY();
                        category.NAME = catName;
                        if(!db.AddCategory(category))
                        {
                            MessageBox.Show("Failed to add Category to Database", "Error");
                            return;
                        }
                    }

                    objective.CATEGORY = category;
                    if (!db.AddObjective(objective))
                    {
                        MessageBox.Show("Failed to add Objective to Database", "Error");
                        return;
                    }
                }

                initiative.BUSINESSOBJECTIVE = objective;
                if (!db.AddInitiative(initiative))
                {
                    MessageBox.Show("Failed to add Initiative to Database", "Error");
                    return;
                }
            }

            BOM bom = new BOM();
            bom.CLIENT = client;
            bom.INITIATIVE = initiative;
            if (!db.AddBOM(bom))
            {
                MessageBox.Show("Failed to add Initiative to BOM", "Error");
                return;
            }
            if (!db.SaveChanges())
            {
                MessageBox.Show("Failed to save changes to database", "Error");
                db = new DBManager();
                return;
            }

            else
            {
                //Successfully added to database, update GUI
                catName = bom.INITIATIVE.BUSINESSOBJECTIVE.CATEGORY.NAME.TrimEnd();
                Category category = categories.Find(delegate(Category cat)
                {
                    return cat.Name == catName;
                });
                if (category == null)
                {
                    category = new Category(this, catName);
                    categories.Add(category);
                    categoryCount++;
                    category.Click += new EventHandler(category_Click);
                }

                busName = bom.INITIATIVE.BUSINESSOBJECTIVE.NAME.TrimEnd();
                BusinessObjective objective = category.Objectives.Find(delegate(BusinessObjective bus)
                {
                    return bus.Name == busName;
                });
                if (objective == null)
                {
                    objective = category.AddObjective(busName);
                }

                iniName = bom.INITIATIVE.NAME.TrimEnd();
                Initiative initiativeObj = objective.Initiatives.Find(delegate(Initiative ini)
                                                                   {
                                                                       return ini.Name == iniName;
                                                                   });
                if (initiativeObj == null)
                {
                    initiativeObj = objective.AddInitiative(iniName);
                }
                else
                {
                    MessageBox.Show("Initiative already exists in BOM", "Error");
                }
            }
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
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            string fileToOpen = FD.FileName;

            System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

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
            //DataEntryForm form = new DataEntryForm(this);
            //form.Show();
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
            if (db.GetCategory(categoryNames.Text.Trim(), out category))
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
            if (db.GetObjective(objectiveNames.Text.Trim(), out objective))
            {
                initiativeNames.Items.AddRange((from ent in objective.INITIATIVE
                                                select ent.NAME.TrimEnd()).ToArray());
            }
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            BOMInitiativeBubbleChart chart = new BOMInitiativeBubbleChart(this);
            chart.Show();
        }

        private void btnLoadRedesignChart_Click(object sender, EventArgs e)
        {
            BOMBubbleChartRedesign chart = new BOMBubbleChartRedesign(this);
            chart.Show();
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
