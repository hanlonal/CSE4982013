using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBMConsultantTool
{
    public partial class DetailedBOMViewForm : Form
    {
        NewObjective owner;

        public DetailedBOMViewForm(NewObjective obj)
        {
            InitializeComponent();
            this.owner = obj;
            dataGridView1.RowHeadersVisible = false;
        }

        private void DetailedBOMViewForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = owner.Initiatives;
        }
    }
}
