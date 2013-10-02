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
        string filepath = "Resouces/Questions.txt";
        string line;
        LinkedList<Person> persons = new LinkedList<Person>();
        Person currentPerson;
        List<CupeQuestion> mainQuestions = new List<CupeQuestion>();
        private CupeQuestion currentQuestion;
        public CupeForm()
        {
            InitializeComponent();

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

        public void TextLabel_TextChanged(object sender, EventArgs e)
        {         

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
                    q.FutureBox.Location = new Point(140, 30 * i);
                    q.TextLabel.TextChanged += new EventHandler(TextLabel_TextChanged);
                    q.CurrentBox.Location = new Point(190, 30 * i);
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

        public void TextLabel_Click(object sender, EventArgs e)
        {
            CustomLabel textlabel = (CustomLabel)sender;
            if(currentQuestion != null)
                currentQuestion.TextLabel.BackColor = Color.White;
            //Console.WriteLine(textlabel.Text);
            currentQuestion = textlabel.Owner;
            currentQuestion.TextLabel.BackColor = Color.GreenYellow;
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
        }

        public void FutureBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            currentPerson.Questions[box.QuestionID].FutureValue = box.Text;
            UpdateFutureLabels();
        }

        public void MakeGraphData()
        {

        }

        public void CalculateTotals()
        {
            float totalFutureIT = 0;
            float totalCurrentIT = 0;
            
            foreach (Person person in persons)
            {
                if (person.Questions[currentQuestion.ID].FutureValue == "a")
                {
                    totalFutureIT += 1;

                }
                if (person.Questions[currentQuestion.ID].CurrentValue == "a")
                {
                    totalCurrentIT += 1;

                }
                if (person.Questions[currentQuestion.ID].FutureValue == "b")
                {
                    totalFutureIT += 2;

                }
                if (person.Questions[currentQuestion.ID].CurrentValue == "b")
                {
                    totalCurrentIT += 1;

                }
                if (person.Questions[currentQuestion.ID].FutureValue == "c")
                {
                    totalFutureIT += 3;

                }
                if (person.Questions[currentQuestion.ID].CurrentValue == "c")
                {
                    totalCurrentIT += 1;

                }
                if (person.Questions[currentQuestion.ID].FutureValue == "d")
                {
                    totalFutureIT += 4;

                }
                if (person.Questions[currentQuestion.ID].CurrentValue == "d")
                {
                    totalCurrentIT += 1;

                }
                
            }

            float avgFutureIT = totalFutureIT / persons.Count;
            float avgCurrentIT = totalCurrentIT / persons.Count;

           // questionChart.Series[0].Points[0] = DataPointavgFutureIT;
            questionChart.Series[1].Points[0].SetValueY(avgCurrentIT);
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
            Person person = new Person();
            persons.AddLast(person);
            currentPerson = person;
            person.Owner = this;
            person.Name =  "Person " + persons.Count.ToString();
            personNameLabel.Text = person.Name;
            person.PopulateQuestionData();
            ResetQuestions();
            personListBox.Items.Add(person);
            personListBox.SelectedValueChanged += new EventHandler(personListBox_SelectedValueChanged);
            
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
            personNameLabel.Text = currentPerson.Name;

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






        

    } // end of class
}
