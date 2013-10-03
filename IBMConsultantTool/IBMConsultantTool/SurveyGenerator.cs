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

        public void CreateBomSurvey(List<Category> BomCats)
        {
            //Find some stats regarding the Cats, Obj, and Imperatives for later reference.



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
            oDoc.PageSetup.LeftMargin = 10.0F;
            oDoc.PageSetup.RightMargin = 10.0F;

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
            oTable = oDoc.Tables.Add(wrdRng, 5, 6, ref oMissing,Word.WdAutoFitBehavior.wdAutoFitContent);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Spacing = 1;


            oTable.Cell(1, 1).Range.Text = "Category";
            oTable.Cell(1, 2).Range.Text = "Business Objectives";
            oTable.Cell(1, 3).Range.Text = "Business Imperatives";
            oTable.Cell(1, 4).Range.Text = "Effectiveness";
            oTable.Cell(1, 5).Range.Text = "Criticality";
            oTable.Cell(1, 6).Range.Text = "Competitive Differentiation";

            int r=2, c=1;
            //Add the categories, objectives, imperatives
            for (int i = 0; i < BomCats.Count; i++)
            {

                for (int j = 0; j < BomCats[i].BusinessObjectivesCount; j++)
                {
                    
                    for (int k = 0; k < BomCats[i].Objectives[j].InitiativesCount; k++)
                    {
                        oTable.Cell(r, 1).Range.Text = BomCats[i].Name;
                        oTable.Cell(r, 2).Range.Text = BomCats[i].Objectives[j].Name;
                        oTable.Cell(r, 3).Range.Text = BomCats[i].Objectives[j].Initiatives[k].Name;
                        r++;
                    }
                }

            }


            //Add forms for the effectiveness, criticality, and differentiation.
            for (r = 2; r <= 5; r++)
                for (c = 4; c <= 6; c++)
                {
                    oDoc.FormFields.Add(oTable.Cell(r, c).Range, Word.WdFieldType.wdFieldFormTextInput);
                }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Rows[1].Range.Font.Size = 8;

            //Word.Range range = oDoc.Range(0, 20);
            //oDoc.FormFields.Add(range, Word.WdFieldType.wdFieldFormTextInput);


            oDoc.Protect(Word.WdProtectionType.wdAllowOnlyFormFields, false, string.Empty, false, false);



            try
            {
                oDoc.SaveAs("hello", Word.WdSaveFormat.wdFormatDocument);
            }
            catch (Exception)
            {
                //just in case one is thrown for no reason
            }

        }
    }
}
