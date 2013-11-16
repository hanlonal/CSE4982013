using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Office;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace IBMConsultantTool
{
    class SurveyReader
    {

        public SurveyReader()
        {

        }

        public string TruncateLongString(string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        public string RemoveCharacters( string st)
        {
            string removeChars = " ?&^$#@!()+-,:;<>’\'-_*";

            string tempString = st;
            foreach (char p in removeChars)
            {
                tempString = tempString.Replace(p.ToString(), string.Empty);
            }
            return tempString;
        }

        public void ReadSurvey(List<NewCategory> BomCats)
        {
            var FD = new System.Windows.Forms.FolderBrowserDialog();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }


            var files = Directory.EnumerateFiles(FD.SelectedPath);
            var badFiles = 0;
            foreach (var file in files)
            {


                if (file.Contains("~$")) 
                {
                    badFiles++;
                    continue;
                }
            //Start Word and open the word document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = false;

            
                oDoc = oWord.Documents.Open(file, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //oWord.Activate();

                if (oDoc.Paragraphs.First.Range.Text != "Business Optimization Mapping Survey\r")
                {
                    continue;
                }

                //This is going to be ugly.
                foreach (Word.FormField form in oDoc.FormFields)
                {
                    //compare name of form to each category/obj/imperative that exists
                    //if theres a match set the correct value
                    foreach (NewCategory cat in BomCats)
                    {
                        foreach (NewObjective obj in cat.Objectives)
                        {
                            foreach (NewImperative ini in obj.Imperatives)
                            {
                                string BaseIniName = TruncateLongString(cat.Name, 5) +
                                TruncateLongString(obj.Name, 5) +
                                TruncateLongString(ini.Name, 5);
                                string temp = BaseIniName + "Diff";
                                if (temp == form.Name)
                                {
                                    ini.Differentiation += Convert.ToSingle(form.Result);
                                }
                                temp = BaseIniName + "Eff";
                                if (temp == form.Name)
                                {
                                    ini.Effectiveness += Convert.ToSingle(form.Result);
                                }
                                temp = BaseIniName + "Crit";
                                if (temp == form.Name)
                                {
                                    ini.Criticality += Convert.ToSingle(form.Result);
                                }
                            }
                        }
                    }
                }
                oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }


            foreach (NewCategory cat in BomCats)
            {
                foreach (NewObjective obj in cat.Objectives)
                {
                    foreach (NewImperative ini in obj.Imperatives)
                    {
                        var temp = Convert.ToSingle(files.Count()) - badFiles;
                        ini.Effectiveness = ini.Effectiveness / temp;
                        ini.Criticality = ini.Criticality / temp;
                        ini.Differentiation = ini.Differentiation / temp;
                    }
                }
            }

        }

        public void ReadSurveyCUPE(List<Person> people)
        {
            int count = 1;
            var FD = new System.Windows.Forms.FolderBrowserDialog();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var files = Directory.EnumerateFiles(FD.SelectedPath);
            var badFiles = 0;

            foreach (var file in files)
            {

                if (file.Contains("~$"))
                {
                    badFiles++;
                    continue;
                }
                //Start Word and open the word document.
                Word._Application oWord;
                Word._Document oDoc;
                oWord = new Word.Application();
                oWord.Visible = false;


                oDoc = oWord.Documents.Open(file, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //oWord.Activate();


                //Loop through the forms. If the person doesn't exist in the participant list then create a new person
                try
                {

                    //if (oDoc.Paragraphs.First.Range.Text != "IT Provider Relationship Survey\r")
                    {
                        continue;
                    }

                    //Find the person object the form is related to, otherwise create a new one
                    Person currentPerson = null;
                    foreach (Word.FormField form in oDoc.FormFields)
                    {
                        //Find the person and their type
                        if (form.Name == "Name")
                        {
                            currentPerson = new Person(count++);
                            if (form.Result.ToString() == "Business")
                            {
                                currentPerson.Type = Person.EmployeeType.Business;
                                currentPerson.CodeName = "Business" + count.ToString();
                            }
                            else
                            {
                                currentPerson.Type = Person.EmployeeType.IT;
                                currentPerson.CodeName = "IT" + count.ToString();
                            }
                            ClientDataControl.AddParticipant(currentPerson);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    int q = 1, c = 1;
                    foreach (Word.FormField form in oDoc.FormFields)
                    {
                        if (form.Name != "Name")
                        {
                            if (c == 1)
                            {
                                currentPerson.cupeDataHolder.CurrentAnswers.Add("Question " + q, form.Result.ToCharArray()[0]);
                                c = 2;
                            }
                            else if (c == 2)
                            {
                                currentPerson.cupeDataHolder.FutureAnswers.Add("Question " + q, form.Result.ToCharArray()[0]);
                                c = 1;
                                q++;
                            }
                        }
                    }
                    ClientDataControl.AddCupeAnswers(currentPerson.cupeDataHolder);
                    oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                }
                catch
                {
                    oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                }
            }
        }


        public void ReadSurveyITCap(List<ScoringEntity> questions)
        {
            var FD = new System.Windows.Forms.FolderBrowserDialog();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var files = Directory.EnumerateFiles(FD.SelectedPath);
            var badFiles = 0;

            foreach (var file in files)
            {

                if (file.Contains("~$"))
                {
                    badFiles++;
                    continue;
                }
                //Start Word and open the word document.
                Word._Application oWord;
                Word._Document oDoc;
                oWord = new Word.Application();
                oWord.Visible = false;


                oDoc = oWord.Documents.Open(file, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //oWord.Activate();


                try
                {

                    ScoringEntity currentQuestion = null;

                    if (oDoc.Paragraphs.First.Range.Text != "IT Capability Assessment Survey\r")
                    {
                        continue;
                    }
                    int q = 1, c = 1;
                    foreach (Word.FormField form in oDoc.FormFields)
                    {
                        if (form.Name != "Name")
                        {
                            foreach ( var f in questions)
                            {
                                var asdfasdfasf = RemoveCharacters(TruncateLongString(f.Name, 19));
                            }
                            try
                            {
                                currentQuestion = questions.Where(x => RemoveCharacters(TruncateLongString(x.Name, 19)) == form.Name).First();
                            }
                            catch
                            {
                                continue;
                            }

                            ITCapQuestion temp = (ITCapQuestion)currentQuestion;

                            if (c == 1)
                            {
                                temp.AddAsIsAnswer(Convert.ToSingle(form.Result.ToString()));
                                
                                c = 2;
                            }
                            else if (c == 2)
                            {
                                temp.AddToBeAnswer(Convert.ToSingle(form.Result.ToString()));
                                c = 3;
                                q++;
                            }
                            else if (c == 3)
                            {
                                temp.AddComment(form.Result.ToString());
                                c = 1;
                                q++;
                            }

                        }
                    }
                    oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                }
                catch
                {
                    oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                }
            }

        }

    }
}
