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

        public void ReadSurvey(string file, List<NewCategory> BomCats)
        {
            //Start Word and open the word document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = false;

            oDoc = oWord.Documents.Open(file, Type.Missing, Type.Missing, Type.Missing, 
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //oWord.Activate();

            foreach ( Word.FormField form in oDoc.FormFields)
            {
                var value = form.Result;
            }

        }
    }
}
