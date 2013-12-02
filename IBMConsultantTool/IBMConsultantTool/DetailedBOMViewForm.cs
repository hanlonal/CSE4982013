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
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            businessObjectiveName.Text = obj.ObjName;
            
            
        }


        private void DetailedBOMViewForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = owner.Imperatives;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["Name"].Width = 200;
            dataGridView1.Columns["TotalBOMScore"].HeaderText = "Weighed ECD Score \n ((11-EFF)*(CRIT*.5))/10 \n +DIFF*0.5";
            dataGridView1.Columns["TotalBOMScore"].Width = 190;
            dataGridView1.Columns["Effectiveness"].Width = 120;
            dataGridView1.Columns["Differentiation"].Width = 120;

            dataGridView1.Columns["TotalBOMScore"].DefaultCellStyle.BackColor = Color.LightYellow;
            dataGridView1.Columns["Differentiation"].DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView1.Columns["Effectiveness"].DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView1.Columns["Criticality"].DefaultCellStyle.BackColor = Color.AliceBlue;

            dataGridView1.Columns["TotalBOMScore"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["Differentiation"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["Effectiveness"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["Criticality"].SortMode = DataGridViewColumnSortMode.NotSortable;


        }
    }
}
