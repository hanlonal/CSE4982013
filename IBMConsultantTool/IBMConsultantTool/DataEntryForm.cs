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
    public partial class DataEntryForm : Form
    {
        //private int categoryCount = 2;
        BOMRedesign mainForm;

        public DataEntryForm(BOMRedesign chart)
        {
            mainForm = chart;
            InitializeComponent();
        }

        private void DataEntryForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < mainForm.CategoryCount; i++)
            {
                dataGrid.Rows.Add(1);
                dataGrid.Rows[i].Cells[0].Value = "Initiative" + i.ToString();
            }
        }


    }
}
