﻿using System;
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
                        participantsGrid.Rows[i].SetValues(person.Name,
                            person.Email,
                            false,
                            true,
                            true,
                            person.ID
                            );
                    }
                    else if (person.Type == Person.EmployeeType.Business)
                    {
                        participantsGrid.Rows[i].SetValues(person.Name,
                            person.Email,
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
                            participantsGrid.Rows[i].SetValues(person.Name,
                                person.Email,
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
                participantsGrid.Rows[participantsGrid.Rows.Count - 1].Cells[5].Value = 0;
            }
            else
            {
                participantsGrid.Rows[participantsGrid.Rows.Count - 1].Cells[5].Value =
                    Convert.ToInt32(participantsGrid.Rows[participantsGrid.Rows.Count - 2].Cells[5].Value) + 1;
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

                    tempPerson.ID = Convert.ToInt32(row.Cells[5].Value.ToString());

                    if (Convert.ToBoolean( row.Cells[3].Value) == true)
                    {
                       tempPerson.Type = Person.EmployeeType.IT;
                    }
                   if ( Convert.ToBoolean( row.Cells[2].Value) == true )
                    {
                       tempPerson.Type = Person.EmployeeType.Business;
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
                participantsGrid.Rows[e.RowIndex].Cells[5].Value = 
                    Convert.ToInt32(participantsGrid.Rows[e.RowIndex - 1].Cells[5].Value) + 1;
            }
            else
            {
                participantsGrid.Rows[e.RowIndex].Cells[5].Value = e.RowIndex;
            }
        }
    }
}
