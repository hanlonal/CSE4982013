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
using System.IO;

namespace IBMConsultantTool
{
    public partial class BOMTool : Form
    {
        TabPage clickedPage;
        List<Control> removableControls = new List<Control>();
        List<NewCategory> categories = new List<NewCategory>();

        string closeState = "close";

        public BOMTool()
        {
            InitializeComponent();
            detailInfoPanel.Controls.Add(seperatorLabel);
            seperatorLabel.Width = detailInfoPanel.Width;

            categoryNames.Items.AddRange(ClientDataControl.db.GetCategoryNames());
            ClientDataControl.db.BuildBOMForm(this);

            this.FormClosed += new FormClosedEventHandler(BOMTool_FormClosed);
        }

        private void BOMTool_FormClosed(object sender, FormClosedEventArgs e)
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

        public void ObjectiveClicked(NewObjective obj)
        {
            int heightBetween = 30;
            seperatorLabel.Text = "View Data: " + obj.ObjName;
            ClearDetailPanel();
            for (int i = 0; i < obj.Imperatives.Count; i++)
            {
                NewImperative init = obj.Imperatives[i];
                CreateDataLabels(55 + (i * heightBetween), init);
            }
        }

        private void ClearDetailPanel()
        {
            for (int i = detailInfoPanel.Controls.Count-1; i >= 0; i--)
            {
                if ((string)detailInfoPanel.Controls[i].Tag != "permanent")
                    detailInfoPanel.Controls.RemoveAt(i);

            }
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

        private void CreateDataLabels(int yValue, NewImperative init)
        {
            int nameXValue = 25;
            int effectivenessXValue = 351;
            int critXValue = 509;
            int diffXValue = 624;

            Label nameLabel = new Label();
            detailInfoPanel.Controls.Add(nameLabel);
            nameLabel.AutoEllipsis = true;
            nameLabel.Width = 270;
            //removableControls.Add(nameLabel);
            nameLabel.Text = init.Name;
            nameLabel.Location = new Point(nameXValue, yValue);

            //removableControls.Add(totalScoreLabel);
            
           // detailInfoPanel.Controls.Add(totalScoreLabel);
            TextBox effectbox = new TextBox();
           // effectbox.TextChanged +=new EventHandler(effectbox_TextChanged);
            effectbox.DataBindings.Add(new Binding("Text", init, "Effectiveness"));
            effectbox.Location = new Point(effectivenessXValue, yValue);
            //effectbox.TextChanged +=new EventHandler(effectbox_TextChanged);
            detailInfoPanel.Controls.Add(effectbox);
            TextBox critBox = new TextBox();
            critBox.DataBindings.Add(new Binding("Text", init, "Criticality"));            
            critBox.Location = new Point(critXValue, yValue);
            detailInfoPanel.Controls.Add(critBox);
            TextBox diffBox = new TextBox();
            diffBox.DataBindings.Add(new Binding("Text", init, "Differentiation"));
            diffBox.Location = new Point(diffXValue, yValue);
            detailInfoPanel.Controls.Add(diffBox);
        }


        private void effectbox_TextChanged(object sender, EventArgs e)
        {
            TextBox sendingTextBox = (TextBox)sender;
        }

        public TabControl CategoryWorkspace
        {
            get
            {
                return catWorkspace;
            }
        }

        private void newImperativeButton_Click(object sender, EventArgs e)
        {
            categories[catWorkspace.SelectedIndex].AddImperative(imperativeNames.Text);
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
            ClientDataControl.db.ChangedCategory(this);
        }

        private void categoryNames_LostFocus(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedCategory(this);
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedObjective(this);
        }

        private void objectiveNames_LostFocus(object sender, EventArgs e)
        {
            ClientDataControl.db.ChangedObjective(this);
        }

        private void AddImperativeButton_Click(object sender, EventArgs e)
        {
            string catName = categoryNames.Text.Trim();
            string busName = objectiveNames.Text.Trim();
            string iniName = imperativeNames.Text.Trim();

            ClientDataControl.db.AddImperativeToBOM(iniName, busName, catName, this);
        }

        private void SendEmailButton_Click(object sender, EventArgs e)
        {
            ClientDataControl.SendEmailButton_Click();
        }

        private void BomSurveyButton_Click(object sender, EventArgs e)
        {
            SurveyGenerator BomSurvGen = new SurveyGenerator();
            if(!BomSurvGen.CreateBomSurvey(this.Categories))
            {
                MessageBox.Show("Word encountered an error. Please retry");
            }
        }

        private void OpenSurvey_Clicked(object sender, EventArgs e)
        {
            var SurveyReader = new SurveyReader();

            ResetValues();
            SurveyReader.ReadSurvey( this.categories);
        }

        public void RemoveObjective(NewObjective obj)
        {
            foreach (NewImperative imp in obj.Imperatives)
            {
                ClientDataControl.db.RemoveBOM(imp.Name, ClientDataControl.Client.EntityObject);
            }
            obj.Controls.Clear();
            Controls.RemoveByKey(obj.ObjName);
            NewCategory cat = obj.owner;
            cat.objectives.Remove(obj);
            if (cat.Objectives.Count == 0)
            {
                Controls.Remove(cat);
                categories.Remove(cat);
                catWorkspace.TabPages.RemoveByKey(cat.name);
            }
            obj.Visible = false;
            ClientDataControl.db.SaveChanges();
        }

        public void RemoveImperative(NewImperative imp)
        {
            Controls.RemoveByKey(imp.Name);
            NewObjective obj = imp.owner;
            obj.Imperatives.Remove(imp);
            if (obj.Imperatives.Count == 0)
            {
                Controls.RemoveByKey(obj.ObjName);
                NewCategory cat = obj.owner;
                if (cat.Objectives.Count == 0)
                {
                    Controls.Remove(cat);
                    categories.Remove(cat);
                    catWorkspace.TabPages.RemoveByKey(cat.name);
                }
                obj.Visible = false;
            }

            ClientDataControl.db.RemoveBOM(imp.Name, ClientDataControl.Client.EntityObject);
            ClientDataControl.db.SaveChanges();
            Refresh();
        }

        public void ChooseColor(NewObjective obj)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = NewObjective.DefaultBackColor;
            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                obj.BackColor = MyDialog.Color;
            }
        }

        public void ResetValues()
        {
            foreach (NewCategory cat in Categories)
            {
                foreach (NewObjective obj in cat.Objectives)
                {
                    foreach (NewImperative ini in obj.Imperatives)
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

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            BOMChartDynamically chart = new BOMChartDynamically(this);
            chart.Show();

            Bitmap bmp = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp.Save(Directory.GetCurrentDirectory() + @"/Charts/" + "Bubble Chart.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }



        private void cUPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = "CUPE";
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            foreach (NewCategory cat in categories)
            {
                foreach (NewObjective obj in cat.Objectives)
                {
                    foreach (NewImperative init in obj.Imperatives)
                    {
                        if (!ClientDataControl.db.UpdateBOM(ClientDataControl.Client.EntityObject, init))
                        {
                            MessageBox.Show("BOM \"" + init.Name + "\" could not be saved to database", "Error");
                            return;
                        }
                    }
                }
            }

            if (ClientDataControl.db.SaveChanges())
            {
                MessageBox.Show("Saved Changes Successfully", "Success");
                ClientDataControl.db.ClientCompletedBOM(ClientDataControl.Client.EntityObject);
            }

            else
            {
                MessageBox.Show("Failed to save changes", "Error");
            }
        }

        private void catWorkspace_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (TabPage item in this.catWorkspace.TabPages)
                {
                    Rectangle r = this.catWorkspace.GetTabRect(catWorkspace.TabPages.IndexOf(item));
                    if (r.X < e.Location.X && e.Location.X < r.X + r.Width && r.Y < e.Location.Y && e.Location.Y < r.Y + r.Height)
                    {
                        clickedPage = item;
                        foreach (NewCategory cat in categories)
                        {
                            if (cat.name == clickedPage.Text)
                            {
                                categories.Remove(cat);
                                break;
                            }
                        }                        
                        this.catWorkspace.TabPages.Remove(clickedPage);
                    }
                }

            }
        }

        private void bOMScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (categories.Count > 0)
            {
                foreach (NewCategory cat in categories)
                {
                    foreach (NewObjective obj in cat.Objectives)
                    {
                        obj.ColorByBOMScore();
                    }
                }
            }
        }

        private void ratingThresholdsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form thresholdsForm = new Form();
            thresholdsForm.Location = new Point(100, 100);
            thresholdsForm.Width = 400;
            thresholdsForm.Height = 350;
            Font font = new Font("Times New Roman", 14.0f);
            thresholdsForm.Font = font;
            TextBox lowerBoundBox = new TextBox();
            lowerBoundBox.Name = "LowerBox";
            TextBox upperBoundBox = new TextBox();
            upperBoundBox.Name = "UpperBox";
            thresholdsForm.Controls.Add(lowerBoundBox);
            thresholdsForm.Controls.Add(upperBoundBox);
            upperBoundBox.Text = NewImperative.criticalAmount.ToString();
            lowerBoundBox.Text = NewImperative.averageAmount.ToString();

            Label heading = new Label();
            heading.AutoSize = true;
            heading.Text = "Set the thresholds for viewing most critical metrics.";
            thresholdsForm.Controls.Add(heading);
            heading.Location = new Point(5, 20);
            upperBoundBox.Location = new Point(260, 100);
            upperBoundBox.Width = 50;
            lowerBoundBox.Width = 50;
            lowerBoundBox.Location = new Point(260, 200);
            Label criticalState = new Label();
            criticalState.Text = "Lower Bound: ";
            criticalState.AutoSize = true;
            criticalState.Location = new Point(50, 200);
            Label upperBound = new Label();
            upperBound.Text = "Upper Bound: ";
            upperBound.AutoSize = true;
            upperBound.Location = new Point(50, 100);
            thresholdsForm.Controls.Add(upperBound);
            thresholdsForm.Controls.Add(criticalState);

            Button okButton = new Button();
            Button cancelButton = new Button();
            thresholdsForm.Controls.Add(okButton);
            thresholdsForm.Controls.Add(cancelButton);
            okButton.Location = new Point(100, 280);
            okButton.Text = "OK";
            okButton.Height = 30;
            okButton.Click +=new EventHandler(okButton_Click);
            cancelButton.Height = 30;
            cancelButton.Location = new Point(200, 280);
            cancelButton.Text = "Cancel";
            cancelButton.Click +=new EventHandler(cancelButton_Click);
            thresholdsForm.Show();


        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Button butt = (Button)sender;
            Control[] text = butt.Parent.Controls.Find("UpperBox", true);
            TextBox box = (TextBox)text[0];

            Control[] text2 = butt.Parent.Controls.Find("LowerBox", true);
            TextBox box2 = (TextBox)text2[0];

            NewImperative.averageAmount = Convert.ToInt32(box.Text);
            NewImperative.criticalAmount = Convert.ToInt32(box2.Text);
            butt.Parent.Hide();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Button butt = (Button)sender;
            butt.Parent.Hide();
        }

        private void iTCapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = "ITCap";
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RUNITCap));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
            return;
        }

        private void RUNITCap()
        {
            Application.Run(new ITCapTool());
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new Form();
            form.Show();
        }






    } // end class
}
