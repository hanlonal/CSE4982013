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
        public int numberOfCUPEColumns = 1;
        public SAMPLEEntities dbo;

        public MainForm()
        {
            InitializeComponent();
            dbo = new SAMPLEEntities();
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

        private void CUPENOQUpdateButton_Click(object sender, EventArgs e)
        {
            int newnumberOfCUPEColumns = 1;
            try
            {
                newnumberOfCUPEColumns = Convert.ToInt32(NumberOfQuestionsTextBox.Text);
            }

            catch
            {
                return;
            }

            if (newnumberOfCUPEColumns < 1)
            {
                return;
            }

            CUPETable.ColumnCount = newnumberOfCUPEColumns + 1;

            while (numberOfCUPEColumns <= newnumberOfCUPEColumns)
            {
                CUPETable.Columns[numberOfCUPEColumns].HeaderText = "Q" + numberOfCUPEColumns.ToString();
                numberOfCUPEColumns++;
            }

            numberOfCUPEColumns = newnumberOfCUPEColumns;
        }

        private void BOMAddInitiativeButton_Click(object sender, EventArgs e)
        {
            new AddInitiative(this).Show();
        }

        private void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "My Application",
            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbo.Dispose();
            }

            else
            {
                e.Cancel = true;
            }
        }
    }
}
