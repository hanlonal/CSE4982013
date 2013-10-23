using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBMConsultantTool
{
    internal sealed class ClientDataControl
    {
        private static readonly ClientDataControl instance = new ClientDataControl();
        private static DataManager db;
        private static bool isOnline;
        private static List<Person> participants = new List<Person>();
        private static List<CupeData> cupeAnswers = new List<CupeData>();
        private static List<NewCategory> bomCategories = new List<NewCategory>();

        private ClientDataControl() { }

        public static ClientDataControl Instance
        {
            get
            {
                return instance;
            }
        }

        public static bool LoadDatabase()
        {
            try
            {
                db = new DBManager();
                isOnline = true;
            }
            catch
            {
                db = new FileManager();
                isOnline = false;
                
            }


            return isOnline;
            ///categoryNames.Items.AddRange(db.GetCategoryNames());        
        }

        public static bool AddParticipant(Person person)
        {
            participants.Add(person);

            return true;
        }

        public static List<Person> GetParticipants()
        {
            return participants;
        }

        public static bool SetParticipants(List<Person> people)
        {
            participants = people;

            return true;

        }

        public static bool AddCategory(NewCategory cat)
        {
            bomCategories.Add(cat);
            return true;
        }

        public static bool ResetCategories()
        {
            bomCategories = new List<NewCategory>();
            return true;
        }
    }
}
