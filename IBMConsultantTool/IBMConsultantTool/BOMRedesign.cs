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
    public partial class BOMRedesign : Form
    {
        List<Panel> categoryPanels = new List<Panel>();
        List<Color> colors = new List<Color>();
        private int categoryCount = 0;

        public BOMRedesign()
        {
            PopulateColorsList();
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void CategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddBox_Click(object sender, EventArgs e)
        {

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

            categoryPanels.Add(dynamicPanel);

        }

        private void dynamicPanel_Click(object sender, EventArgs e)
        {
            Panel a = (Panel)sender;

            
            Console.WriteLine(a.Location.ToString());
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
 




    }
}
