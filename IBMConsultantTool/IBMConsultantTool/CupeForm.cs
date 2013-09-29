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

        public CupeForm()
        {
            InitializeComponent();

        }

        private void CupeForm_Load(object sender, EventArgs e)
        {
            //CreateQuestions(); 
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
                    q.FutureBox.Location = new Point(140, 30 * i);
                    q.TextLabel.TextChanged += new EventHandler(TextLabel_TextChanged);
                    q.CurrentBox.Location = new Point(190, 30 * i);
                    q.CurrentBox.TextChanged += new EventHandler(CurrentBox_TextChanged);
                    q.FutureBox.TextChanged += new EventHandler(FutureBox_TextChanged);
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
        }

        public void FutureBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            currentPerson.Questions[box.QuestionID].FutureValue = box.Text;
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
            }
        }

        private void nextPersonButton_Click(object sender, EventArgs e)
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






    } // end of class
}
