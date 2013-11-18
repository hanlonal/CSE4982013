using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace IBMConsultantTool
{
    public partial class CupeForm : Form
    {
        string line;
        LinkedList<Person> persons = new LinkedList<Person>();
        Person currentPerson;
        List<CupeQuestion> mainQuestions = new List<CupeQuestion>();
        private CupeQuestion currentQuestion;

        private float avgCurrentAll;
        private float avgFutureAll;

        public CupeForm()
        {
            InitializeComponent();
            personListBox.DrawItem +=
                new System.Windows.Forms.DrawItemEventHandler(personListBox_DrawItem);

        }

        private void CupeForm_Load(object sender, EventArgs e)
        {
            //CreateQuestions(); 
            //questionChart.Series.Remove(Series1);
           // questionChart.Legends[0].questionchar
          //  questionChart.Series[0].Points.AddY(5);
            //questionChart.Series[0].Points.AddY(3);
            //questionChart.Series[0].Points.AddY(2);
            //questionChart.Series[0].Points.AddY(4);
            //questionChart.Legends[0].CustomItems.Add(Color.Green, "IT Future");
            //questionChart.Legends[0].CustomItems.Add(Color.Brown, "IT Current");
            //questionChart.Legends[0].CustomItems.Add(Color.Lavender, "Busi Future");
            //questionChart.Legends[0].CustomItems.Add(Color.Olive, "Busi Future");

            
        }

        public void CreateQuestions(int number)
        {
            StringReader file = null;
            try
            {
                file = new StringReader(IBMConsultantTool.Properties.Resources.Questions);
                int i = 0;
                while ((line = file.ReadLine()) != null)
                {
                    CupeQuestion q = new CupeQuestion();
                   
                   // currentPerson.Questions.AddLast(q);
                    q.TextLabel.Location = new Point(10, (30 * i) + 5);
                    q.ID = i;
                    q.TextLabel.Text = line;
                    q.QuestionText = line;
                    q.CurrentBox.Location = new Point(250, 30 * i);
                    q.FutureBox.Location = new Point(310, 30 * i);
                    
                    q.CurrentBox.TextChanged += new EventHandler(CurrentBox_TextChanged);
                    q.FutureBox.TextChanged += new EventHandler(FutureBox_TextChanged);
                    q.TextLabel.Click +=new EventHandler(TextLabel_Click);
                    i++;
                    q.Owner = panel1;
                    mainQuestions.Add(q);
                    //Console.WriteLine("something");
                }

            }
            finally
            {
                if (file != null)
                    file.Close();
            }



            
                
        }

        public void ReloadChart()
        {
            int comm = 0;
            int util = 0;
            int part = 0;
            int enab = 0;

            foreach (CupeQuestion question in mainQuestions)
            {
                enab += question.TotalNumberOfCurrentEnabAnswersAll;
                part += question.TotalNumberOfCurrentPartAnswersAll;
                util += question.TotalNumberOfCurrentUtilAnswersAll;
                comm += question.TotalNumberOfCurrentCommAnswersAll;
            }
            cupeResponseChart.Series["cupeResponse"].Points[0].SetValueXY("oneee", util);
            cupeResponseChart.Series["cupeResponse"].Points[1].SetValueXY("twooo", comm);
            cupeResponseChart.Series["cupeResponse"].Points[2].SetValueXY("threee", part);
            cupeResponseChart.Series["cupeResponse"].Points[3].SetValueXY("fourr", enab);

            }


        public void TextLabel_Click(object sender, EventArgs e)
        {
            CustomLabel textlabel = (CustomLabel)sender;
            if(currentQuestion != null)
                currentQuestion.TextLabel.BackColor = Color.White;
            //Console.WriteLine(textlabel.Text);
            currentQuestion = textlabel.Owner;
            currentQuestion.TextLabel.BackColor = Color.GreenYellow;
            questionChart.Series["Series1"].Points.Clear();
            questionNameLabel.Text = "Question # : " + (currentQuestion.ID + 1).ToString();
            CalculateTotals();
            
        }


        public List<CupeQuestion> Questions
        {
            get
            {
                return mainQuestions;
            }
        }

        public void CurrentBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            currentPerson.Questions[box.QuestionID].CurrentValue = box.Text;
            // box.Owner.QuestionData.CurrentValue = box.Text;
            UpdateCurrentLabels();
            currentPerson.CalculateTotalCurrentCupeScore();
            UpdateCurrentPersonListBoxLabel();
        }

        public void FutureBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            currentPerson.Questions[box.QuestionID].FutureValue = box.Text;
            UpdateFutureLabels();
            currentPerson.CalculateTotalFutureCupeScore();
            UpdateCurrentPersonListBoxLabel();
        }

        private void UpdateCurrentPersonListBoxLabel()
        {
           // personListBox.Items.Insert(0, "hello");
            //personListBox.Items.RemoveAt(1);
            personListBox.Refresh();


            Color color = currentPerson.Color;
            Console.WriteLine(color.ToString());
            
        }

        private void personListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            Color color = Color.White;
            Graphics g = e.Graphics;
            

            //switch(((ListBox)sender).ValueMember.
            Console.WriteLine("getting called");

            g.FillRectangle(new SolidBrush(color), e.Bounds);

            // Print text
            g.DrawString(((ListBox)sender).Items[e.Index].ToString(),
        e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

           // e.DrawFocusRectangle();
            //personListBox.Refresh();
        }

        public void MakeGraphData()
        {
            questionChart.Series["Series1"].Points.AddY(avgCurrentAll);
            questionChart.Series["Series1"].Points.AddY(avgFutureAll);

            FillGraphLabels();
        }

        public void FillGraphLabels()
        {
            avgCurrentLabel.Text = avgCurrentAll.ToString();
            avgFutureLabel.Text = avgFutureAll.ToString();
            avgCurrentCommLabel.Text = currentQuestion.TotalNumberOfCurrentCommAnswersAll.ToString();
            avgCurrentUtilLabel.Text = currentQuestion.TotalNumberOfCurrentUtilAnswersAll.ToString();
            avgCurrentPartLabel.Text = currentQuestion.TotalNumberOfCurrentPartAnswersAll.ToString();
            avgCurrentEnabLabel.Text = currentQuestion.TotalNumberOfCurrentEnabAnswersAll.ToString();
            avgFutureCommLabel.Text = currentQuestion.TotalNumberOfFutureCommAnswersAll.ToString();
            avgFutureUtilLabel.Text = currentQuestion.TotalNumberOfFutureUtilAnswersAll.ToString();
            avgFuturePartLabel.Text = currentQuestion.TotalNumberOfFuturePartAnswersAll.ToString();
            avgFutureEnabLabel.Text = currentQuestion.TotalNumberOfFutureEnabAnswersAll.ToString();


        }

        public void CalculateTotals()
        {
            float totalFutureAll = 0;
            float totalCurrentAll = 0;
            int answerFromPerson = 0;
            currentQuestion.ClearCurrentTotals();
            currentQuestion.ClearFutureTotals();
            foreach (Person person in persons)
            {
                
               answerFromPerson = person.CalculateCurrentDataForQuestion(currentQuestion.ID);
               if (answerFromPerson == 1)
                   currentQuestion.TotalNumberOfCurrentCommAnswersAll++;
               if (answerFromPerson == 2)
                   currentQuestion.TotalNumberOfCurrentUtilAnswersAll++;
               if (answerFromPerson == 3)
                   currentQuestion.TotalNumberOfCurrentPartAnswersAll++;
               if (answerFromPerson == 4)
                   currentQuestion.TotalNumberOfCurrentEnabAnswersAll++;
               totalCurrentAll += answerFromPerson;

               answerFromPerson = person.CalculateFutureDataForQuestion(currentQuestion.ID);
               if (answerFromPerson == 1)
                   currentQuestion.TotalNumberOfFutureCommAnswersAll++;
               if (answerFromPerson == 2)
                   currentQuestion.TotalNumberOfFutureUtilAnswersAll++;
               if (answerFromPerson == 3)
                   currentQuestion.TotalNumberOfFuturePartAnswersAll++;
               if (answerFromPerson == 4)
                   currentQuestion.TotalNumberOfFutureEnabAnswersAll++;


               totalFutureAll += answerFromPerson;
            }

            avgFutureAll = totalFutureAll / persons.Count;
            avgCurrentAll = totalCurrentAll / persons.Count;
            foreach( CupeQuestion question in mainQuestions)
            {
                question.TotalCurrentAverageOfAllAnswers = avgCurrentAll;
                question.TotalFutureAverageOfAllAnswers = avgFutureAll;
            }
            MakeGraphData();
           // questionChart.Series[0].Points[0] = DataPointavgFutureIT;

        }

        public void UpdateCurrentLabels()
        {
            currentPerson.ClearCurrentValues();
            foreach (CupeQuestionData data in currentPerson.Questions)
            {
                if (data.CurrentValue == "a")
                    currentPerson.TotalCommodity++;                                   
                if (data.CurrentValue == "b")
                    currentPerson.TotalUtility++;
                if (data.CurrentValue == "c")
                    currentPerson.TotalPartner++;
                if (data.CurrentValue == "d")
                    currentPerson.TotalEnabler++;

                totalCommodityLabel.Text = currentPerson.TotalCommodity.ToString();
                totalEnablerLabel.Text = currentPerson.TotalEnabler.ToString();
                totalPartnerLabel.Text = currentPerson.TotalPartner.ToString();
                totalUtilityLabel.Text = currentPerson.TotalUtility.ToString();
            }
            
        }

        public void UpdateFutureLabels()
        {
            currentPerson.ClearFutureValues();
            foreach (CupeQuestionData data in currentPerson.Questions)
            {
                if (data.FutureValue == "a")
                    currentPerson.TotalFutureCommodity++;
                if (data.FutureValue == "b")
                    currentPerson.TotalFutureUtility++;
                if (data.FutureValue == "c")
                    currentPerson.TotalFuturePartner++;
                if (data.FutureValue == "d")
                    currentPerson.TotalFutureEnabler++;

                totalFutureCommodityLabel.Text = currentPerson.TotalFutureCommodity.ToString();
                totalFutureEnablerLabel.Text = currentPerson.TotalFutureEnabler.ToString();
                totalFuturePartnerLabel.Text = currentPerson.TotalFuturePartner.ToString();
                totalFutureUtilityLabel.Text = currentPerson.TotalFutureUtility.ToString();
            }

        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            Person person = new Person(persons.Count);
            persons.AddLast(person);
            currentPerson = person;
            person.Owner = this;
            personNameLabel.Text = "Person " + persons.Count.ToString();
            person.PopulateQuestionData();
            ResetQuestions();
            personListBox.Items.Add(person);
            
            personListBox.MouseDown +=new MouseEventHandler(personListBox_MouseDown);
            personListBox.SelectedValueChanged += new EventHandler(personListBox_SelectedValueChanged);
            
        }

        public void personListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = personListBox.IndexFromPoint(e.X, e.Y);
                if (index > -1 && index < personListBox.Items.Count)
                {
                    Console.WriteLine(index.ToString());
                }
            }
        }

        public void personListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox box = (ListBox)sender;
            int selectedindex = box.SelectedIndex;
           // Console.WriteLine(selectedindex.ToString());
            currentPerson = persons.ElementAt<Person>(selectedindex);
            ChangePerson();

        }

       public void CupeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                GetNext();
            }
            if (e.KeyCode == Keys.Right)
            {
                GetPrevious();
            }

        }

        public void ResetQuestions()
        {
            foreach (CupeQuestion q in mainQuestions)
            {
                q.CurrentBox.Text = "";
                q.FutureBox.Text = "";
            }
        }

        private void new20Question_Click(object sender, EventArgs e)
        {
            CreateQuestions(20);
        }

        private void previousPersonButton_Click(object sender, EventArgs e)
        {
            GetPrevious();
        }

        private void GetPrevious()
        {
            Person per = currentPerson;

            LinkedListNode<Person> p = persons.Find(per);
            if (p.Previous != null)
            {
                currentPerson = p.Previous.Value;
                ChangePerson();
            }
            else
                return;
        }

        private void ChangePerson()
        {
            personNameLabel.Text = "Person " + currentPerson.ID.ToString();
            personListBox.SelectedIndex = currentPerson.ID;
            foreach (CupeQuestion question in mainQuestions)
            {
                question.CurrentBox.Text = currentPerson.Questions[question.ID].CurrentValue;
                question.FutureBox.Text = currentPerson.Questions[question.ID].FutureValue;
                UpdateCurrentLabels();
                UpdateFutureLabels();
            }
        }

        private void nextPersonButton_Click(object sender, EventArgs e)
        {
            GetNext();
        }

        public void GetNext()
        {
            Person per = currentPerson;
            LinkedListNode<Person> p = persons.Find(per);
            if (p.Next != null)
            {
                currentPerson = p.Next.Value;
                
                ChangePerson();
            }
            else
                return;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                GetPrevious();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                GetNext();
                return true;
            }
            else return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReloadChart();
        }








        

    } // end of class
}
