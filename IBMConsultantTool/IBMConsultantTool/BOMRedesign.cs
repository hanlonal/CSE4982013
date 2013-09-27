﻿using System;
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
        SAMPLEEntities dbo = new SAMPLEEntities();
                
        List<Category> categories = new List<Category>();
        List<Color> colors = new List<Color>();
        private int categoryCount = 0;
        private Category lastFocused;
        public BOMRedesign()
        {
           // PopulateColorsList();
            InitializeComponent();
            categoryNames.Items.AddRange((from cat in dbo.CATEGORY
                                          select cat.NAME.TrimEnd()).ToArray());
        }

        private void categoryAddButton_Click(object sender, EventArgs e)
        {
            if (categoryNames.Text == "")
            {
                MessageBox.Show("Choose a Category first");
            }

            else
            {
                Category category = new Category(this, categoryNames.Text);
                categories.Add(category);
                categoryCount++;
                category.Click += new EventHandler(category_Click);
            }
        }

        private void category_Click(object sender, EventArgs e)
        {
            Category cat = (Category)sender;
            lastFocused = cat;
            Console.WriteLine("clicked on category:" + cat.Name);
        }

        public int CategoryCount
        {
            get
            {
                return categoryCount;
            }
        }

        private void initiativeAddButton_Click(object sender, EventArgs e)
        {
            lastFocused.LastClicked.AddInitiative(initiativeNames.Text);
        }

        private void objectiveAddButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(categoryCount.ToString());
            lastFocused.AddObjective(objectiveNames.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Category cat in categories)
            {
                Console.WriteLine(cat.Name);
            }
        }
        public Category LastClickedCategory
        {
            set
            {
                lastFocused = value;
            }
        }

        private void dataInputButton_Click(object sender, EventArgs e)
        {
            DataEntryForm form = new DataEntryForm(this);
            form.Show();
        }

        public List<Category> Categories
        {
            get
            {
                return categories;
            }
        }

        private void categoryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectiveNames.Text = "<Select Objective>";
            objectiveNames.Items.Clear();
            objectiveNames.Items.AddRange((from bus in dbo.BUSINESSOBJECTIVE
                                           where bus.CATEGORY.NAME.TrimEnd() == categoryNames.Text
                                           select bus.NAME.TrimEnd()).ToArray());

            initiativeNames.Text = "";
            initiativeNames.Items.Clear();
        }

        private void objectiveNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            initiativeNames.Text = "<Select Initiative>";
            initiativeNames.Items.Clear();
            initiativeNames.Items.AddRange((from ini in dbo.INITIATIVE
                                            where ini.BUSINESSOBJECTIVE.NAME.TrimEnd() == objectiveNames.Text
                                            select ini.NAME.TrimEnd()).ToArray());

        }

     /*   private void createPPTButton_Click(object sender, EventArgs e)
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
        }*/

        

    } // end of class
}
