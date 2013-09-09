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
    public partial class MainForm : Form
    {
        public int numberOfColumns = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BOMBubbleChartButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcBOMBubbleChart));
            t.Start();
            return;
        }

        public static void ThreadProcBOMBubbleChart()
        {
            Application.Run(new BOMBubbleChart());
        }

        //private void CUPENOQUpdateButton_Click(object sender, EventArgs e)
        //{
        //    int newNumberOfColumns = 1;
        //    try
        //    {
        //        newNumberOfColumns = Convert.ToInt32(NumberOfQuestionsTextBox.Text);
        //    }

        //    catch
        //    {
        //        return;
        //    }

        //    if (newNumberOfColumns < 1)
        //    {
        //        return;
        //    }

        //    CUPETable.ColumnCount = newNumberOfColumns + 1;

        //    while (numberOfColumns <= newNumberOfColumns)
        //    {
        //        CUPETable.Columns[numberOfColumns].HeaderText = "Q" + numberOfColumns.ToString();
        //        numberOfColumns++;
        //    }

        //    numberOfColumns = newNumberOfColumns;
        //}

        private void BOMAddInitiativeButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcBOMAddInitiative));
            t.Start();
            return;
        }

        public static void ThreadProcBOMAddInitiative()
        {
            Application.Run(new AddInitiative());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void New_Click(object sender, EventArgs e)
        {

        }

        private void CUPE_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NumberOfQuestionsLabel_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenAnalytics_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCUPEAnalForm));
            t.Start();
            return;
        }

        public static void ThreadCUPEAnalForm()
        {
            Application.Run(new CUPEAnalytics());
        }


    }
}
