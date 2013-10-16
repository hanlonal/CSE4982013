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

            participantsGrid.Rows.Add(ClientDataControl.GetParticipants().Count());

            int i = 0;
            foreach (Person person in ClientDataControl.GetParticipants())
            {
                participantsGrid.Rows[i].SetValues(person.Name,
                    person.Email,
                    person.Type,
                    true
                    );
                i++;
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
                    tempPerson.Email = row.Cells[1].Value.ToString();

                    if (row.Cells[2].Value.ToString() == "IT")
                    {
                        tempPerson.Type = Person.EmployeeType.IT;
                    }
                    else if (row.Cells[2].Value.ToString() == "Business")
                    {
                        tempPerson.Type = Person.EmployeeType.IT;
                    }

                    tempList.Add(tempPerson);
                }
                catch
                {

                }

            }

            ClientDataControl.SetParticipants(tempList);
        }
    }
}
