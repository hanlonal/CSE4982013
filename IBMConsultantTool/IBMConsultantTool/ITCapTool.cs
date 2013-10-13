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
        int highestID;
        private Domain currentSelected;

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
                CreateDomain(textBox1.Text, fileManager.GetHighestIDNumber());
            }
            else
            {
                Console.WriteLine("File already exists");
            }
        }

        public void CreateDomain(string name, int id)
        {
            Domain dom = new Domain();
            dom.Name = name;
            dom.ToolID = id.ToString();
            domains.Add(dom);
            AddDomainToListBox(dom);
            //fileManager.IncreaseIDNumber();
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
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = dom.Name;
                    row.Cells[1].Value = dom.ToolID;
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    dataGridView1.Rows.Add(row);
                }
            }

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

        private void view_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox box = (ListBox)sender;
            currentSelected = (Domain)box.SelectedItem;
            Console.WriteLine(currentSelected);
        }

        private void vieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            
        }



    }
}
