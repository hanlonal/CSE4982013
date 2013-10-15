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

    public partial class ITCapTool : Form
    {
        ITCapFileManager fileManager;
        List<Domain> domains = new List<Domain>();
        List<Capability> capabilities = new List<Capability>();
        List<ITCapQuestion> questions = new List<ITCapQuestion>();
        int highestID;
        private Domain currentSelectedDomain;
        private Capability currentSelectedCapability;
        private ITCapQuestion currentSelectedQuestion;

        public ITCapTool()
        {
            InitializeComponent();
           // fileManager = new ITCapFileManager(this);
        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {

            //fileManager.CheckFileSystem();
           // BuildGridView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Domain dom in domains)
            {
                if(dom.Name == textBox1.Text)
                {
                    Console.WriteLine("Domain Already exists");
                    return;
                }
            }
            
            CreateDomain(textBox1.Text);
            

        }

        public Domain CreateDomain(string name)
        {
            Domain dom = new Domain();
            dom.Name = name;
            domains.Add(dom);
            dom.ToolID = domains.Count.ToString();
            //dom.Index = domains.Count;

            dom.Owner = this;
            
            dom.IndexInDataGrid = FindIndexofDomain();
            AddToGrid(dom);
            AddDomainToListBox(dom);

            return dom;

        }

        public Capability CreateCapability(string name, Domain dom)
        {
            Capability cap = new Capability();
            dom.NumCapabilities++;
            cap.Name = name;
            cap.ToolID = dom.ToolID + "." + dom.NumCapabilities;
            cap.Owner = dom;
            cap.IndexInDataGrid = dom.IndexInDataGrid + dom.NumCapabilities + 1;
            AddToGrid(cap);
            capabilities.Add(cap);
           // AddCapabilityToListBox(cap);
            dom.AddCapabilitytoList(cap);
            
            return cap;
        }

        public void CreateQuestion(string name, Capability cap)
        {
            ITCapQuestion question = new ITCapQuestion();
            cap.NumQuestions++;
            question.QuestionText = name;
            question.ToolId = cap.ToolID + "." + cap.NumQuestions;
            question.Owner = cap;
            question.IndexInGrid = cap.IndexInDataGrid + cap.NumQuestions + 1;
            AddToGrid(question);
            questions.Add(question);
            cap.AddQuestionToList(question);

        }


        public string[] GetFileInfo(string name, string type)
        {
           return fileManager.GetFileInfo(name, type);
        }

        private void AddDomainToListBox(Domain dom)
        {
            listBox1.Items.Add(dom);
        }
        private void BuildGridView()
        {
            foreach (Domain dom in domains)
            {
                if (dom.IsDefault)
                {
                    //BuildRow(dom.Name, dom.ToolID, Color.DarkOrange);

                    BuildGridViewWithCapabilities(dom);

                }
            }

        }

        private int FindIndexofDomain()
        {
            int index = domains.Count + capabilities.Count + questions.Count;
            return index;
        }
        private void BuildRow(string name, string id, Color color, int index, string type)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = type;
            row.Cells[1].Value = name;
            row.Cells[2].Value = id;
            row.Cells[3].Value = CheckState.Checked;
            row.DefaultCellStyle.BackColor = color;
            if (index == dataGridView1.Rows.Count +1)
                dataGridView1.Rows.Insert(dataGridView1.Rows.Count -1, row);
            else
                dataGridView1.Rows.Insert(index -1, row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Show();
            ListBox view = new ListBox();
            form.Controls.Add(view);
            view.Visible = true;
            view.Location = new Point(10, 10);
            view.SelectedValueChanged += new EventHandler(view_SelectedValueChanged);
            foreach (Domain dom in domains)
            {
                view.Items.Add(dom);
                
            }
        }

        private void BuildGridViewWithCapabilities(Domain dom)
        {
            foreach (Capability cap in dom.Capabilities)
            {
                if (cap.IsDefault)
                {
                    //BuildRow(cap.Name, cap.ToolID, Color.Yellow);
                    BuildGridViewWithQuestions(cap);
                }
            }
        }

        public void AddToGrid(object sender)
        {
            if (sender.GetType() == typeof(Domain))
            {
                Domain dom = (Domain)sender;
                
                BuildRow(dom.Name, dom.ToolID, Color.DarkOrange, dom.IndexInDataGrid, "domain");
            }
            if (sender.GetType() == typeof(Capability))
            {
                Capability cap = (Capability)sender;
                BuildRow(cap.Name, cap.ToolID, Color.Yellow, cap.IndexInDataGrid, "capability");
                
            }
            if (sender.GetType() == typeof(ITCapQuestion))
            {
                ITCapQuestion question = (ITCapQuestion)sender;
                BuildRow(question.QuestionText, question.ToolId, Color.Green, question.IndexInGrid, "question");
            }
        }

        private void BuildGridViewWithQuestions(Capability cap)
        {
            foreach (ITCapQuestion question in cap.Questions)
            {
                if (question.IsDefault)
                {
                   // BuildRow(question.QuestionText, question.ToolId, Color.Green);
                }
            }
        }

        private void view_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox box = (ListBox)sender;
            currentSelectedDomain = (Domain)box.SelectedItem;
           // Console.WriteLine(currentSelected.ToString());
        }

        private void vieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            if ((string)row.Cells[0].Value == "domain")
            {

                foreach (Domain dom in domains)
                {
                    if (dom.Name == (string)row.Cells[1].Value)
                    {
                        currentSelectedDomain = dom;
                        currentSelectedDomain.IndexInDataGrid = rowIndex;
                        Console.WriteLine(currentSelectedDomain.IndexInDataGrid);
                        return;
                    }
                }
            }
            if ((string)row.Cells[0].Value == "capability")
            {
                foreach (Capability cap in capabilities)
                {
                    if (cap.Name == (string)row.Cells[1].Value)
                    {
                        currentSelectedDomain = cap.Owner;
                        currentSelectedCapability = cap;
                        currentSelectedCapability.IndexInDataGrid = rowIndex;
                        Console.WriteLine(currentSelectedCapability.IndexInDataGrid);
                        return;
                    }
                }
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Capability cap in capabilities)
            {
                if (cap.Name == textBox2.Text)
                {
                    Console.WriteLine("Capability Already exists");
                    return;
                }
            }

            CreateCapability(textBox2.Text, currentSelectedDomain);

           
        }





        private void AddCapabilityToListBox(Capability cap)
        {
            listBox2.Items.Add(cap);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ITCapQuestion question in questions)
            {
                if (question.QuestionText == textBox3.Text)
                {
                    Console.WriteLine("Question Already exists");
                    return;
                }
            }

            CreateQuestion(textBox3.Text, currentSelectedCapability);
        }

        private void changeDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridView1.EndEdit();
                if (Convert.ToBoolean(dataGridView1.CurrentCell.Value) == true)
                {
                    Console.WriteLine("changed to true");
                }
                else
                    Console.WriteLine("changed to false");
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }




    }
}
