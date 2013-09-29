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
            Person person = new Person();
            person.Name = "Adam";
            persons.AddLast(person);
            personNameLabel.Text = person.Name;
            currentPerson = person;
            
            CreateQuestions(); 
        }

        public void TextLabel_TextChanged(object sender, EventArgs e)
        {         

        }

        public void CreateQuestions()
        {
            StringReader file = null;
            try
            {
                file = new StringReader(IBMConsultantTool.Properties.Resources.Questions);
                int i = 0;
                while ((line = file.ReadLine()) != null)
                {
                    CupeQuestion q = new CupeQuestion();
                    mainQuestions.Add(q);
                    currentPerson.Questions.AddLast(q);
                    q.TextLabel.Location = new Point(10, (30 * i) + 5);
                    q.TextLabel.Text = line;
                    q.FutureBox.Location = new Point(140, 30 * i);
                    q.TextLabel.TextChanged += new EventHandler(TextLabel_TextChanged);
                    q.CurrentBox.Location = new Point(190, 30 * i);
                    q.CurrentBox.TextChanged += new EventHandler(CurrentBox_TextChanged);
                    q.FutureBox.TextChanged += new EventHandler(FutureBox_TextChanged);
                    i++;
                    q.Owner = panel1;
                    //Console.WriteLine("something");
                }

            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }

        public void CurrentBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            box.Owner.QuestionData.CurrentValue = box.Text;
        }

        public void FutureBox_TextChanged(object sender, EventArgs e)
        {
            CustomBox box = (CustomBox)sender;
            box.Owner.QuestionData.FutureValue= box.Text;
        }

        private void addPersonButton_Click(object sender, EventArgs e)
        {
            Person person = new Person();
            currentPerson = person;
            
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


    } // end of class
}
