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

    class SurveyGenerator
    {
        public SurveyGenerator()
        {

        }

        public string TruncateLongString( string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        public void CreateBomSurvey(List<NewCategory> BomCats)
        {
            //Find some stats regarding the Cats, Obj, and Imperatives for later reference.
            var totalRows = 1;
            //Add the categories, objectives, imperatives
            for (int i = 0; i < BomCats.Count; i++)
            {
                for (int j = 0; j < BomCats[i].Objectives.Count; j++)
                {
                    for (int k = 0; k < BomCats[i].Objectives[j].Initiatives.Count; k++)
                    {
                        totalRows++;
                    }
                }
            }

            //Creating the document
            //
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            oWord.Activate();
            System.Threading.Thread.Sleep(3000);

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Business Optimization Mapping Survey";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert a paragraph at the end of the document.
            //Word.Paragraph oPara2;
            //object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            //oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
            //oPara2.Range.Text = "Heading 2";
            //oPara2.Format.SpaceAfter = 6;
            //oPara2.Range.InsertParagraphAfter();

            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, totalRows, 6, ref oMissing,Word.WdAutoFitBehavior.wdAutoFitContent);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Spacing = 1;


            oTable.Cell(1, 1).Range.Text = "Category";
            oTable.Cell(1, 2).Range.Text = "Business Objectives";
            oTable.Cell(1, 3).Range.Text = "Business Imperatives";
            oTable.Cell(1, 4).Range.Text = "Effectiveness";
            oTable.Cell(1, 5).Range.Text = "Criticality";
            oTable.Cell(1, 6).Range.Text = "Competitive Differentiation";


            System.Threading.Thread.Sleep(3000);

            //Create an array for the names for the formfields
            string[] FormNames = new string [(totalRows - 1 ) * 3];

            //Current Row and Current FormName position
            int r = 2, c = 0;
            //Add the categories, objectives, imperatives
            for (int i = 0; i < BomCats.Count; i++)
            {

                for (int j = 0; j < BomCats[i].Objectives.Count; j++)
                {
                    
                    for (int k = 0; k < BomCats[i].Objectives[j].Initiatives.Count; k++)
                    {
                        oTable.Cell(r, 1).Range.Font.Size = 7;
                        oTable.Cell(r, 2).Range.Font.Size = 7;
                        oTable.Cell(r, 3).Range.Font.Size = 7;

                        oTable.Cell(r, 1).Range.Text = BomCats[i].name;
                        oTable.Cell(r, 2).Range.Text = BomCats[i].Objectives[j].Name;
                        oTable.Cell(r, 3).Range.Text = BomCats[i].Objectives[j].Initiatives[k].Name;

                        //Add the form fields for each row
                        Word.Range cell2Range = oTable.Cell(r, 4).Range;
                        cell2Range.Collapse(ref oMissing);
                        oDoc.FormFields.Add(cell2Range, Word.WdFieldType.wdFieldFormTextInput);

                        cell2Range = oTable.Cell(r, 5).Range;
                        cell2Range.Collapse(ref oMissing);
                        oDoc.FormFields.Add(cell2Range, Word.WdFieldType.wdFieldFormTextInput);

                        cell2Range = oTable.Cell(r, 6).Range;
                        cell2Range.Collapse(ref oMissing);
                        oDoc.FormFields.Add(cell2Range, Word.WdFieldType.wdFieldFormTextInput);


                        string BaseFormName = TruncateLongString(BomCats[i].Name, 5) + 
                            TruncateLongString(BomCats[i].Objectives[j].Name, 5) +
                            TruncateLongString(BomCats[i].Objectives[j].Initiatives[k].Name, 5);

                        FormNames[c] = BaseFormName + "Eff";
                        c++;

                        FormNames[c] = BaseFormName + "Crit";
                        c++;

                        FormNames[c] = BaseFormName + "Diff";
                        c++;

                        r++;
                    }
                }

            }


            //Add forms for the effectiveness, criticality, and differentiation.
            //for (r = 2; r <= totalRows; r++)
            //    for (c = 4; c <= 6; c++)
            //    {
            //        oDoc.FormFields.Add(oTable.Cell(r, c).Range, Word.WdFieldType.wdFieldFormTextInput);
            //    }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Rows[1].Range.Font.Size = 10;
            
            
            //Word.Range range = oDoc.Range(0, 20);
            //oDoc.FormFields.Add(range, Word.WdFieldType.wdFieldFormTextInput);



            oDoc.Protect(Word.WdProtectionType.wdAllowOnlyFormFields, false, string.Empty, false, false);
            c = 0;
            foreach(Word.FormField form in oDoc.FormFields)
            {
                //form.Name = FormNames[c].ToString();
                string removeChars = " ?&^$#@!()+-,:;<>’\'-_*";

                string name = FormNames[c];
                foreach (char p in removeChars)
                {
                    name = name.Replace(p.ToString(), string.Empty);
                }
                
                

                form.Name = name;
                c++;
            }

            try
            {
                oDoc.SaveAs("BomSurvey", Word.WdSaveFormat.wdFormatDocument);
            }
            catch (Exception)
            {
                //just in case one is thrown for no reason
            }

        }

        public void CreateCupeSurvey(List<Person> people, List<string> questions)
        {
            //Find some stats regarding the Cats, Obj, and Imperatives for later reference.
            var totalRows = questions.Count + 1;

            //Creating the document
            //
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            oWord.Activate();
            System.Threading.Thread.Sleep(3000);

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "CUPE Survey";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();


            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, totalRows, 3, ref oMissing, Word.WdAutoFitBehavior.wdAutoFitContent);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Spacing = 1;


            oTable.Cell(1, 1).Range.Text = "Question";
            oTable.Cell(1, 2).Range.Text = "Current Value";
            oTable.Cell(1, 3).Range.Text = "Future Value";



            System.Threading.Thread.Sleep(3000);

            //Create an array for the questions for the formfields
            string[] FormNames = new string[(totalRows - 1) * 2];

            //Current Row and Current FormName position
            int r = 2, c = 0;
            //Add the questions
            foreach ( string question in questions)
            {
                oTable.Cell(r, 1).Range.Text = question;
                FormNames[c] = question;
                c++;
                FormNames[c] = question; ;

                Word.Range cell2Range = oTable.Cell(r, 2).Range;
                cell2Range.Collapse(ref oMissing);
                oDoc.FormFields.Add(cell2Range, Word.WdFieldType.wdFieldFormTextInput);

                cell2Range = oTable.Cell(r, 3).Range;
                cell2Range.Collapse(ref oMissing);
                oDoc.FormFields.Add(cell2Range, Word.WdFieldType.wdFieldFormTextInput);

                r++;
                c++;
            }


            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Rows[1].Range.Font.Size = 10;




            oDoc.Protect(Word.WdProtectionType.wdAllowOnlyFormFields, false, string.Empty, false, false);
            c = 0;
            foreach (Word.FormField form in oDoc.FormFields)
            {
                //form.Name = FormNames[c].ToString();
                string removeChars = " ?&^$#@!()+-,:;<>’\'-_*";

                string name = FormNames[c];
                foreach (char p in removeChars)
                {
                    name = name.Replace(p.ToString(), string.Empty);
                }



                form.Name = name;
                c++;
            }

            try
            {
                oDoc.SaveAs("CupeSurvey", Word.WdSaveFormat.wdFormatDocument);
            }
            catch (Exception)
            {
                //just in case one is thrown for no reason
            }

        }









    }
}
