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

                    if(person.Type == Person.EmployeeType.IT)
                    {
                        participantsGrid.Rows[i].SetValues("",
                            false,
                            true,
                            true,
                            person.ID
                            );
                    }
                    else if (person.Type == Person.EmployeeType.Business)
                    {
                        participantsGrid.Rows[i].SetValues("",
                            true,
                            false,
                            true,
                            person.ID
                            );
                    }
                    else
                    {
                        if (person.Type == Person.EmployeeType.IT)
                        {
                            participantsGrid.Rows[i].SetValues("",
                                false,
                                false,
                                true,
                                person.ID
                                );
                        }
                    }
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
            int count = 1;
            List<Person> tempList = new List<Person>();
            foreach (DataGridViewRow row in participantsGrid.Rows)
            {
                if(Convert.ToBoolean( row.Cells[2].Value) && Convert.ToBoolean( row.Cells[1].Value))
                {
                    MessageBox.Show("Participants must be either Business or IT", "Error");
                    return;
                }
                else if (!Convert.ToBoolean(row.Cells[2].Value) && !Convert.ToBoolean(row.Cells[1].Value))
                {
                    continue;
                }
                try
                {
                    //Create the new person
                    Person tempPerson = new Person(count++);

                    if (row.Cells[0].Value != null)
                    {
                        tempPerson.Email = row.Cells[0].Value.ToString();
                    }

                    if (Convert.ToBoolean( row.Cells[2].Value) == true)
                    {
                       tempPerson.Type = Person.EmployeeType.IT;
                       tempPerson.CodeName = "Business" + count.ToString();
                    }
                    if ( Convert.ToBoolean( row.Cells[1].Value) == true )
                    {
                       tempPerson.Type = Person.EmployeeType.Business;
                       tempPerson.CodeName = "IT" + count.ToString();
                    }
                   //See if there are answers for the current person

                   CupeData data = null;
                   try
                   {
                       data = ClientDataControl.GetCupeAnswers().Where(x => x.ParticipantId == tempPerson.ID).Single();
                   }
                   catch
                   {

                   }
                   if (data != null)
                   {
                       tempPerson.cupeDataHolder = data;
                   }
                   //If not create new cupedata object
                   else
                   {
                       tempPerson.cupeDataHolder = new CupeData(tempPerson.ID);
                   }

                   tempList.Add(tempPerson);
                }
                catch
                {

                }

            }

            tempList.OrderBy(o => o.ID);

            ClientDataControl.SetParticipants(tempList);
            this.Close();
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
