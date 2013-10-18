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
    public partial class EditParticipants : Form
    {
        public EditParticipants()
        {
            InitializeComponent();



            try
            {
                if (ClientDataControl.GetParticipants().Count() != 0)
                {
                    participantsGrid.Rows.Add(ClientDataControl.GetParticipants().Count());
                }

                int i = 0;
                foreach (Person person in ClientDataControl.GetParticipants())
                {
                    participantsGrid.Rows[i].SetValues(person.Name,
                        person.Email,
                        person.Type,
                        true,
                        person.ID
                        );
                    i++;
                }

            }
            catch
            {

            }

            if (participantsGrid.Rows.Count == 1)
            {
                participantsGrid.Rows[participantsGrid.Rows.Count - 1].Cells[4].Value = 0;
            }
            else
            {
                participantsGrid.Rows[participantsGrid.Rows.Count - 1].Cells[4].Value =
                    Convert.ToInt32(participantsGrid.Rows[participantsGrid.Rows.Count - 2].Cells[4].Value) + 1;
            }
           
        }


        private void SaveParticipantButton_Click(object sender, EventArgs e)
        {
            List<Person> tempList = new List<Person>();
            foreach (DataGridViewRow row in participantsGrid.Rows)
            {
                try
                {
                    Person tempPerson = new Person();
                    tempPerson.Name = row.Cells[0].Value.ToString();

                    if (row.Cells[1].Value != null)
                    {
                        tempPerson.Email = row.Cells[1].Value.ToString();
                    }

                    tempPerson.ID = Convert.ToInt32(row.Cells[4].Value.ToString());

                    if (row.Cells[2].Value != null)
                    {
                        if (row.Cells[2].Value.ToString() == "IT")
                        {
                            tempPerson.Type = Person.EmployeeType.IT;
                        }
                        else if (row.Cells[2].Value.ToString() == "Business")
                        {
                            tempPerson.Type = Person.EmployeeType.IT;
                        }
                    }

                    tempList.Add(tempPerson);
                }
                catch
                {

                }

            }

            tempList.OrderBy(o => o.ID);

            ClientDataControl.SetParticipants(tempList);
        }

        private void participantsGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if(e.RowIndex != 0)
            {
                participantsGrid.Rows[e.RowIndex].Cells[4].Value = 
                    Convert.ToInt32(participantsGrid.Rows[e.RowIndex - 1].Cells[4].Value) + 1;
            }
            else
            {
                participantsGrid.Rows[e.RowIndex].Cells[4].Value = e.RowIndex;
            }
        }
    }
}
