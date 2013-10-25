using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace IBMConsultantTool
{
    public partial class BOMTool : Form
    {

        public DataManager db;
        public object client;
        public bool isOnline;
        List<Control> removableControls = new List<Control>();
        List<NewCategory> categories = new List<NewCategory>();

        public BOMTool()
        {
            InitializeComponent();
            detailInfoPanel.Controls.Add(seperatorLabel);
            seperatorLabel.Width = detailInfoPanel.Width;
            try
            {
                db = new DBManager();
                isOnline = true;
            }
            catch
            {
                db = new FileManager();
                isOnline = false;
                MessageBox.Show("Could not reach database: Offline mode set", "Error");
            }

            categoryNames.Items.AddRange(db.GetCategoryNames());

            new ChooseBOMClient(this).ShowDialog();
            
        }

        public void ObjectiveClicked(NewObjective obj)
        {
            int heightBetween = 30;
            ClearDetailPanel();
            for (int i = 0; i < obj.Initiatives.Count; i++)
            {
                NewInitiative init = obj.Initiatives[i];
                CreateDataLabels(55 + (i * heightBetween), init);
            }
        }

        private void ClearDetailPanel()
        {

        }

        public NewCategory AddCategory(string name)
        {
            catWorkspace.TabPages.Add(name, name);
            catWorkspace.SelectedIndex = catWorkspace.TabCount - 1;
            NewCategory category = new NewCategory(this, catWorkspace.TabPages.Count - 1, name);
            categories.Add(category);

            //catWorkspace.TabPages[name].Controls.Add(category.);
            //catWorkspace.TabPages[name].BackColor = Color.DimGray; ;

            return category;
        }

        private void CreateDataLabels(int yValue, NewInitiative init)
        {
            int nameXValue = 25;
            int effectivenessXValue = 351;
            int critXValue = 509;
            int diffXValue = 624;
            int totalXValue = 791;

            Label nameLabel = new Label();
            detailInfoPanel.Controls.Add(nameLabel);
            nameLabel.AutoEllipsis = true;
            nameLabel.Width = 200;
            //removableControls.Add(nameLabel);
            nameLabel.Text = init.Name;
            nameLabel.Location = new Point(nameXValue, yValue);
            Label totalScoreLabel = new Label();
            //removableControls.Add(totalScoreLabel);
            
            detailInfoPanel.Controls.Add(totalScoreLabel);
            TextBox effectbox = new TextBox();
            
            effectbox.Location = new Point(effectivenessXValue, yValue);
            detailInfoPanel.Controls.Add(effectbox);
            TextBox critBox = new TextBox();
            critBox.Location = new Point(critXValue, yValue);
            detailInfoPanel.Controls.Add(critBox);
            TextBox diffBox = new TextBox();
            diffBox.Location = new Point(diffXValue, yValue);
            detailInfoPanel.Controls.Add(diffBox);
        }

        public TabControl CategoryWorkspace
        {
            get
            {
                return catWorkspace;
            }
        }

        private void newInitiativeButton_Click(object sender, EventArgs e)
        {
            categories[catWorkspace.SelectedIndex].AddInitiative(initiativeNames.Text);
        }

        public List<NewCategory> Categories
        {
            get
            {
                return categories;
            }
        }

        private void dataInputButton_Click(object sender, EventArgs e)
        {
            DataEntryForm form = new DataEntryForm(this);
            form.Show();
        }


        private void categoryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedCategory(this);
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            db.ChangedCategory(this);
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.ChangedObjective(this);
        }

        private void objectiveNames_LostFocus(object sender, EventArgs e)
        {
            db.ChangedObjective(this);
        }

        private void AddInitiativeButton_Click(object sender, EventArgs e)
        {
            string catName = categoryNames.Text.Trim();
            string busName = objectiveNames.Text.Trim();
            string iniName = initiativeNames.Text.Trim();

            db.AddInitiativeToBOM(iniName, busName, catName, this);
        }

        private void SendEmailButton_Click(object sender, EventArgs e)
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

        private void BomSurveyButton_Click(object sender, EventArgs e)
        {
            SurveyGenerator BomSurvGen = new SurveyGenerator();
            BomSurvGen.CreateBomSurvey(this.Categories);
        }

        private void OpenSurvey_Clicked(object sender, EventArgs e)
        {
            var SurveyReader = new SurveyReader();

            ResetValues();
            SurveyReader.ReadSurvey( this.categories);
        }

        public void ResetValues()
        {
            foreach (NewCategory cat in Categories)
            {
                foreach (NewObjective obj in cat.Objectives)
                {
                    foreach (NewInitiative ini in obj.Initiatives)
                    {
                        ini.Differentiation = 0;
                        ini.Criticality = 0;
                        ini.Effectiveness = 0;
                    }
                }
            }
        }
        private void participantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FD = new EditParticipants();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
        }

        private void BOMTool_Load(object sender, EventArgs e)
        {
            if (client == null)
            {
                this.Close();
            }
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            BOMBubbleChartRedesign chart = new BOMBubbleChartRedesign(this);
            chart.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cUPEToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void effectivenessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByEffectiveness();
                    }
                }
            }
        }

        private void criticalityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByCriticality();
                    }
                }
            }
        }

        private void differentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByDifferentiation();
                    }
                }
            }
        }


    } // end class
}
