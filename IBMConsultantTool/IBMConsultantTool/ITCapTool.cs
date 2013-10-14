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
        int highestID;
        private Domain currentSelectedDomain;
        private Capability currentSelectedCapability;
        private ITCapQuestion currentSelectedQuestion;

        public ITCapTool()
        {
            InitializeComponent();
            fileManager = new ITCapFileManager(this);
        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {

            fileManager.CheckFileSystem();
            BuildGridView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileManager.AddDomainToSystem(textBox1.Text))
            {
                CreateDomain(textBox1.Text, fileManager.GetHighestIDNumberDomain());
            }
            else
            {
                Console.WriteLine("File already exists");
            }
        }

        public Domain CreateDomain(string name, int id)
        {
            Domain dom = new Domain();
            dom.Name = name;
            dom.ToolID = id.ToString();
            dom.Index = domains.Count;
            dom.Owner = this;
            domains.Add(dom);

            AddDomainToListBox(dom);

            return dom;
           /* List<string> capstoMake = fileManager.GetChildren(dom);
            if(capstoMake !=null)
                dom.BuildCapabilitiesList(capstoMake);*/
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
                    BuildRow(dom.Name, dom.ToolID, Color.DarkOrange);

                    BuildGridViewWithCapabilities(dom);

                }
            }

        }
        private void BuildRow(string name, string id, Color color)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = name;
            row.Cells[1].Value = id;
            row.DefaultCellStyle.BackColor = color;
            dataGridView1.Rows.Add(row);
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
                    BuildRow(cap.Name, cap.ToolID, Color.Yellow);
                    BuildGridViewWithQuestions(cap);
                }
            }
        }

        private void BuildGridViewWithQuestions(Capability cap)
        {

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
            string name =  row.Cells[0].Value.ToString();

            foreach (Domain dom in domains)
            {
                if (dom.Name == name)
                {
                    currentSelectedDomain = dom;
                    Console.WriteLine(currentSelectedDomain.ToString());
                    return;
                }
            }
            foreach (Capability cap in capabilities)
            {
                if (cap.Name == name)
                {
                    currentSelectedCapability = cap;
                    currentSelectedDomain = cap.Owner;
                    Console.WriteLine(currentSelectedCapability.ToString());
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fileManager.AddCapabilityToSystem(textBox2.Text, currentSelectedDomain))
            {
                CreateCapability(textBox2.Text, fileManager.GetHighestIDNumberCapability(), currentSelectedDomain);
            }
            else
            {
                Console.WriteLine("Capability already exists");
            }

           
        }

        public void CreateCapability(string name, int id, Domain dom)
        {
            Capability cap = new Capability();
            cap.Name = name;
            cap.ToolID = id.ToString();
            cap.Owner = dom;
            capabilities.Add(cap);
            AddCapabilityToListBox(cap);
            dom.AddCapabilitytoList(cap);
            dom.NumCapabilities++;
        }

        private void AddCapabilityToListBox(Capability cap)
        {
            listBox2.Items.Add(cap);
        }




    }
}
