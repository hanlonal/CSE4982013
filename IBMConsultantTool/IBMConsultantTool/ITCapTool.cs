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

        public ITCapTool()
        {
            InitializeComponent();
            fileManager = new ITCapFileManager(this);
        }

        private void ITCapTool_Load(object sender, EventArgs e)
        {

            fileManager.CheckFileSystem();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileManager.AddDomainToSystem(richTextBox1.Text))
            {
                CreateDomain(richTextBox1.Text,(highestID +1 ).ToString());
            }
            else
            {
                Console.WriteLine("File already exists");
            }
        }

        public void CreateDomain(string name, string id)
        {
            Domain dom = new Domain();
            dom.Name = name;
            dom.ToolID = id;
            domains.Add(dom);
            AddDomainToListBox(dom);
        }

        private void AddDomainToListBox(Domain dom)
        {
            listBox1.Items.Add(dom);
        }



    }
}
