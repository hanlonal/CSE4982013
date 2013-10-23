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

                //This is going to be ugly.
                foreach (Word.FormField form in oDoc.FormFields)
                {
                    //compare name of form to each category/obj/initiative that exists
                    //if theres a match set the correct value
                    foreach (NewCategory cat in BomCats)
                    {
                        foreach (NewObjective obj in cat.Objectives)
                        {
                            foreach (NewInitiative ini in obj.Initiatives)
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
                    foreach (NewInitiative ini in obj.Initiatives)
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

                var tempType = "strang";
                Person personTemp;
                foreach (Word.FormField form in oDoc.FormFields)
                {
                    //Find the person and their type
                    if(form.Name == "Name")
                    {
                        var nameForm = people.Where(x => x.Name == "Name");
                        


                    }

                    var value = form.Result;
                }
                oDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }

        }



    }
}
