using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IBMConsultantTool
{
    public partial class BOMRedesign : Form
    {
                
        List<Category> categoryPanels = new List<Category>();
        List<Color> colors = new List<Color>();
        private int categoryCount = 0;
        private Panel lastFocused;

        public BOMRedesign()
        {
            PopulateColorsList();
            InitializeComponent();
        }


        private void AddBox_Click(object sender, EventArgs e)
        {
           /* categoryCount++;
            Category category = new Category(this);
            mainWorkspace.Controls.Add(category);
            category.BackColor = colors[categoryCount-1];
            categoryPanels.Add(category);
            */
            categoryCount++;
            Panel dynamicPanel = new Panel();

            dynamicPanel.Location = new System.Drawing.Point(0, 0);

            dynamicPanel.Name = CategoryBox.Text;

            dynamicPanel.Size = new System.Drawing.Size(100, 100);

            dynamicPanel.BackColor = colors[categoryCount-1];
            mainWorkspace.Controls.Add(dynamicPanel);
            dynamicPanel.Visible = true;

            Label categoryLabel = new Label();
            categoryLabel.BorderStyle = BorderStyle.Fixed3D;
            categoryLabel.Location = new System.Drawing.Point(0, 0);
            categoryLabel.Text = CategoryBox.Text;
            
            dynamicPanel.Controls.Add(categoryLabel);
            dynamicPanel.Click +=new EventHandler(dynamicPanel_Click);
            dynamicPanel.MouseMove +=new MouseEventHandler(dynamicPanel_MouseMove);
            dynamicPanel.LostFocus +=new EventHandler(dynamicPanel_LostFocus);
            
            
             

        }

        private void dynamicPanel_Click(object sender, EventArgs e)
        {
            Panel a = (Panel)sender;
            foreach (Panel panel in categoryPanels)
            {
                if (panel != a)
                {
                    a.Focus();
                }
            }
            
            bool yes = a.Focused;
            Console.WriteLine(yes.ToString());
            a.Focus();
            bool yess =  a.Focused;
            Console.WriteLine(yess.ToString());
            a.Height = 200;
            a.Width = 200;

            
            Console.WriteLine(a.Location.ToString());
        }

        private void dynamicPanel_LostFocus(object sender, EventArgs e)
        {
            Panel a = (Panel)sender;
            a.Width = 100;
            a.Height = 100;
            lastFocused = a;
            Console.WriteLine(lastFocused.Name);

        }
        private void dynamicPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = (Panel)sender;
            if (e.Button == MouseButtons.Left)
            {
                panel.Left += e.X;
                panel.Top += e.Y;
                Console.WriteLine(panel.Name);
            }
        }

        private void PopulateColorsList()
        {
            colors.Add(Color.Red);
            colors.Add(Color.Blue);
            colors.Add(Color.Pink);
            colors.Add(Color.Green);
            colors.Add(Color.Lavender);
            colors.Add(Color.Salmon);
            colors.Add(Color.Ivory);


            
        }

        private void addData_Click(object sender, EventArgs e)
        {
            DataEntryForm dataForm = new DataEntryForm(this);
            dataForm.Show();
            dataForm.Location = new Point(100, 100);
        }
        #region Properties
        public int CategoryCount
        {
            get
            {
                return categoryCount;
            }
        }

        public Panel LastFocus
        {
            get
            {
                return lastFocused;
            }
        }

        #endregion

        private void addObjective_Click(object sender, EventArgs e)
        {
            Label newLabel = new Label();
            newLabel.Text = "Yay new objective";
            newLabel.Location = new System.Drawing.Point(0, 20);
            newLabel.Show();
            lastFocused.Controls.Add(newLabel);
        }

        private void addInitivative_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(SaveDialogThread));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start(); 
        }

        void SaveDialogThread()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "comma|*.csv";

            string lines = "";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //string linesstr ="";



                //saveDialog.FileName = "untitled";
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveDialog.FileName);
                SaveFile.WriteLine(lines);
                SaveFile.Close();
            }
        }


 




    }
}
