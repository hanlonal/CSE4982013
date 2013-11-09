using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Power = Microsoft.Office.Interop.PowerPoint;
using System.IO;
using Graph = Microsoft.Office.Interop.Graph;


namespace IBMConsultantTool
{
    class PowerPointGenerator
    {
        public PowerPointGenerator()
        {

        }

        public void CreatePowerPoint()
        {
            String strTemplate, strPic;
            strTemplate = Directory.GetCurrentDirectory() + "\\Resources\\blends.pot";
            strPic = Directory.GetCurrentDirectory() + "\\Resources\\Add record.png";

            Power.Application objApp;
            Power.Presentations objPresSet;
            Power._Presentation objPres;
            Power.Slides objSlides;
            Power._Slide objSlide;
            Power.TextRange objTextRng;
            Power.Shapes objShapes;
            Power.Shape objShape;

            //Power.SlideShowWindows objSSWs;
            //Power.SlideShowTransition objSST;
            //Power.SlideShowSettings objSSS;
            //Power.SlideRange objSldRng;
            //Graph.Chart objChart;

            //Create a new presentation based on a template.
            objApp = new Power.Application();
            objApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            objPresSet = objApp.Presentations;
            objPres = objPresSet.Open(strTemplate,
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoTrue,
                Microsoft.Office.Core.MsoTriState.msoTrue);
            objSlides = objPres.Slides;

            //Build Slide #1:
            //Add text to the slide, change the font and insert/position a 
            //picture on the first slide.
            //objSlide = objSlides.Add(1, Power.PpSlideLayout.ppLayoutTitleOnly);
            //objTextRng = objSlide.Shapes[1].TextFrame.TextRange;
            //objTextRng.Text = "IBM IT Consultation";
            //objTextRng.Font.Name = "Comic Sans MS";
            //objTextRng.Font.Size = 48;
            //objSlide.Shapes.AddPicture(strPic, Microsoft.Office.Core.MsoTriState.msoFalse,
            //    Microsoft.Office.Core.MsoTriState.msoTrue,
            //    150, 150, 500, 350);

            //Build Slide #2:
            //Add text to the slide title, format the text. Also add a chart to the
            //slide and change the chart type to a 3D pie chart.
            //objSlide = objSlides.Add(2, Power.PpSlideLayout.ppLayoutTitleOnly);
            //objTextRng = objSlide.Shapes[1].TextFrame.TextRange;
            //objTextRng.Text = "My Chart";
            //objTextRng.Font.Name = "Comic Sans MS";
            //objTextRng.Font.Size = 48;
            //objChart = (Graph.Chart)objSlide.Shapes.AddOLEObject(150, 150, 480, 320,
            //    "MSGraph.Chart.8", "", Microsoft.Office.Core.MsoTriState.msoFalse,
            //    "", 0, "",
            //    Microsoft.Office.Core.MsoTriState.msoFalse).OLEFormat.Object;
            //objChart.ChartType = Graph.XlChartType.xl3DPie;
            //objChart.Legend.Position = Graph.XlLegendPosition.xlLegendPositionBottom;
            //objChart.HasTitle = true;
            //objChart.ChartTitle.Text = "Here it is...";

            //Build Slide #3:
            //Change the background color of this slide only. Add a text effect to the slide
            //and apply various color schemes and shadows to the text effect.
            //objSlide = objSlides.Add(3, Power.PpSlideLayout.ppLayoutBlank);
            //objSlide.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
            //objShapes = objSlide.Shapes;
            //objShape = objShapes.AddTextEffect(Microsoft.Office.Core.MsoPresetTextEffect.msoTextEffect27,
            //  "The End", "Impact", 96, Microsoft.Office.Core.MsoTriState.msoFalse, 
            //  Microsoft.Office.Core.MsoTriState.msoFalse, 230, 200);


            var FD = new System.Windows.Forms.FolderBrowserDialog();
            if (FD.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var files = Directory.EnumerateFiles(FD.SelectedPath);

            foreach (var file in files)
            {

                if (file.Contains("~$") || !file.Contains(".jpg"))
                {
                    continue;
                }

                objSlide = objSlides.Add(1, Power.PpSlideLayout.ppLayoutTitleOnly);
                objTextRng = objSlide.Shapes[1].TextFrame.TextRange;
                objTextRng.Text = Path.GetFileNameWithoutExtension(file);
                objTextRng.Font.Name = "Comic Sans MS";
                objTextRng.Font.Size = 48;
                objSlide.Shapes.AddPicture(file, Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoTrue,
                    150, 150, 500, 350);



            }



        }



        public void ReplaceTemplatePowerpoint()
        {
            String strTemplate, strPic;
            strTemplate = Directory.GetCurrentDirectory() + "\\Resources\\ITPlanningTemplate.ppt";
            String strBubblePic = Directory.GetCurrentDirectory() + "\\Charts\\BubbleChart.jpg";
            String strITCUPECurr = Directory.GetCurrentDirectory() + "\\Charts\\IT Current CUPE Responses.jpg";
            String strITCUPEFut = Directory.GetCurrentDirectory() + "\\Charts\\IT Future CUPE Responses.jpg";
            String strBUSCUPECurr = Directory.GetCurrentDirectory() + "\\Charts\\Business Current CUPE Responses.jpg";
            String strBUSCUPEFut = Directory.GetCurrentDirectory() + "\\Charts\\Business Future CUPE Responses.jpg";
            String strITComparisonBar = Directory.GetCurrentDirectory() + "\\Charts\\IT StakeHolders CurrentFuture Comparison.jpg";
            String strBUSIComparisonBar = Directory.GetCurrentDirectory() + "\\Charts\\Business Leaders CurrentFuture Comparison.jpg";
            String strHeatMap = Directory.GetCurrentDirectory() + "\\Charts\\HeatMap.jpg";
            strPic = Directory.GetCurrentDirectory() + "\\Resources\\Add record.png";

            Power.Slides objSlides;
            Power._Slide objSlide;


            //Create a new presentation based on a template.
            Power.Application ppApp = new Power.Application();
            ppApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            Power.Presentations ppPresens = ppApp.Presentations; 
            Power.Presentation objPres = ppPresens.Open(strTemplate,
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoTrue,
                Microsoft.Office.Core.MsoTriState.msoTrue);
            objSlides = objPres.Slides;

            //Slide 10
            //Replace the bullets on Slide 10
            //
            objSlide = objSlides[10];

            Power.Shape objShape = objSlide.Shapes[4];
            String tempString = string.Empty;
            //TODO: change this to objectives
            foreach (string BomCat in ClientDataControl.db.GetCategoryNames())
            {
                tempString += BomCat + "\r";
            }
            objShape.TextFrame.TextRange.Text = tempString;
            ///


            //Slide 12
            //Replace the bubblechart picture on slide 12
            //
            objSlide = objSlides[12];

            objSlide.Shapes[2].Delete();


            objSlide.Shapes.AddPicture(strBubblePic, Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoTrue, 100, 100, 490, 275);
            //Change the text on slide 12

            objShape = objSlide.Shapes[3];

            tempString = string.Empty + "Business Imperatives:\v\v ";

            //TODO: Replace with initiatives
            foreach(String s in ClientDataControl.db.GetDomainNames())
            {
                tempString += s + "\t\t";
            }
            objShape.TextFrame.TextRange.Text = tempString;
            ///

            ///Slide 19
            ///
            objSlide = objSlides[19];
            objShape = objSlide.Shapes[3];
            objShape.Delete();
            objShape = objSlide.Shapes[3];
            objShape.Delete();
            objShape = objSlide.Shapes[3];
            objShape.Delete();
            objShape = objSlide.Shapes[3];
            objShape.Delete();
            objShape = objSlide.Shapes[4];
            objShape.Delete();
            objShape = objSlide.Shapes[4];
            objShape.Delete();

            objSlide.Shapes.AddPicture(strBUSCUPECurr, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 80, 120, 300, 200);
            objSlide.Shapes.AddPicture(strBUSCUPEFut, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 80, 320, 300, 200);
            objSlide.Shapes.AddPicture(strITCUPECurr, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 360, 120, 300, 200);
            objSlide.Shapes.AddPicture(strITCUPEFut, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 360, 320, 300, 200);

            ///
            ///Slide 20
            ///
            objSlide = objSlides[20];
            objShape = objSlide.Shapes[5];
            objShape.Delete();
            objShape = objSlide.Shapes[5];
            objShape.Delete();
            objShape = objSlide.Shapes[5];
            objShape.Delete();
            objShape = objSlide.Shapes[5];
            objShape.Delete();
            objShape = objSlide.Shapes[5];
            objShape.Delete();
            objShape = objSlide.Shapes[2];
            objShape.Delete();
            objShape = objSlide.Shapes[2];
            objShape.Delete();


            objSlide.Shapes.AddPicture(strBUSIComparisonBar, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 0, 100, 350, 380);
            objSlide.Shapes.AddPicture(strITComparisonBar, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 350, 100, 350, 380);

            ///

            ///
            ///Slide 26
            ///
            objSlide = objSlides[26];
            objShape = objSlide.Shapes[3];
            objShape.Delete();

            objSlide.Shapes.AddPicture(strHeatMap, Microsoft.Office.Core.MsoTriState.msoFalse,
        Microsoft.Office.Core.MsoTriState.msoTrue, 140, 120, 420, 330);


            ///


        }

    }
}
