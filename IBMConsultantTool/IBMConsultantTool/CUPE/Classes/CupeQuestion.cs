using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IBMConsultantTool
{
   public class CupeQuestion
    {


      

       private string current;
       private string future;
       private CustomLabel textLabel;
       private int id;

       private CustomBox futureTextBox;
       private CustomBox currentTextBox;

       private string questionText;
       Panel owner = new Panel();

       private float totalCurrentAverageOfAllAnswers;
       private float totalFutureAverageOfAllAnswers;

       private int totalNumberOfFutureCommAnswersAll;

       public int TotalNumberOfFutureCommAnswersAll
       {
           get { return totalNumberOfFutureCommAnswersAll; }
           set { totalNumberOfFutureCommAnswersAll = value; }
       }

       private int totalNumberOfFutureUtilAnswersAll;

       public int TotalNumberOfFutureUtilAnswersAll
       {
           get { return totalNumberOfFutureUtilAnswersAll; }
           set { totalNumberOfFutureUtilAnswersAll = value; }
       }

       private int totalNumberOfFutureEnabAnswersAll;

       public int TotalNumberOfFutureEnabAnswersAll
       {
           get { return totalNumberOfFutureEnabAnswersAll; }
           set { totalNumberOfFutureEnabAnswersAll = value; }
       }

       private int totalNumberOfFuturePartAnswersAll;

       public int TotalNumberOfFuturePartAnswersAll
       {
           get { return totalNumberOfFuturePartAnswersAll; }
           set { totalNumberOfFuturePartAnswersAll = value; }
       }






       private int totalNumberOfCurrentCommAnswersAll;

       public int TotalNumberOfCurrentCommAnswersAll
       {
           get { return totalNumberOfCurrentCommAnswersAll; }
           set { totalNumberOfCurrentCommAnswersAll = value; }
       }
      
       
       private int totalNumberOfCurrentUtilAnswersAll;

       public int TotalNumberOfCurrentUtilAnswersAll
       {
           get { return totalNumberOfCurrentUtilAnswersAll; }
           set { totalNumberOfCurrentUtilAnswersAll = value; }
       }
      
       
       private int totalNumberOfCurrentPartAnswersAll;

       public int TotalNumberOfCurrentPartAnswersAll
       {
           get { return totalNumberOfCurrentPartAnswersAll; }
           set { totalNumberOfCurrentPartAnswersAll = value; }
       }
       
       
       private int totalNumberOfCurrentEnabAnswersAll;

       public int TotalNumberOfCurrentEnabAnswersAll
       {
           get { return totalNumberOfCurrentEnabAnswersAll; }
           set { totalNumberOfCurrentEnabAnswersAll = value; }
       }

       public void ClearCurrentTotals()
       {
           totalNumberOfCurrentCommAnswersAll = 0;
           totalNumberOfCurrentPartAnswersAll = 0;
           totalNumberOfCurrentEnabAnswersAll = 0;
           totalNumberOfCurrentUtilAnswersAll = 0;
       }
       public void ClearFutureTotals()
       {
           totalNumberOfFutureCommAnswersAll = 0;
           totalNumberOfFuturePartAnswersAll = 0;
           totalNumberOfFutureEnabAnswersAll = 0;
           totalNumberOfFutureUtilAnswersAll = 0;
       }

       public CupeQuestion()
       {
           currentTextBox = new CustomBox(this);
           futureTextBox = new CustomBox(this);
           
           textLabel = new CustomLabel(this);

           DefineCurrentBox();
           DefineFutureBox();
           
           DefineTextLabel();

           
       }




       public string Current
       {
           get
           {
               return current;
           }
           set
           {
               current = value;
               //Console.WriteLine(textLabel.Text + " texts reads" + current.ToString());
           }
       }
       public string Future { get { return future; } set { future = value;  } }
       public CustomLabel TextLabel { get { return textLabel; } }
       public TextBox FutureBox { get { return futureTextBox; } }
       public TextBox CurrentBox { get { return currentTextBox; } set { CurrentBox = value; } }
       public Panel Owner
       { 
           set 
           { 
               owner = value; 
               owner.Controls.Add(textLabel);
               owner.Controls.Add(currentTextBox);
               owner.Controls.Add(futureTextBox);
               
           } 
       }

       public string QuestionText
       {
           get
           {
               return questionText;
           }
           set
           {
               questionText = value;
           }
       }

       private void DefineCurrentBox()
       {
           currentTextBox.Width = 30;
           currentTextBox.Height = 30;

       }
       private void DefineFutureBox()
       {
           //q.Visible = true;
           futureTextBox.Width = 30;
           futureTextBox.Height = 30;   
       }

       private void DefineTextLabel()
       {
           textLabel.BackColor = Color.Red;
           textLabel.Width = 200;
           textLabel.Height = 20;
           textLabel.BackColor = Color.White;

       }

       public int ID
       {
           get
           {
               return id;
           }
           set
            {
                id = value;
                currentTextBox.QuestionID = value;
                futureTextBox.QuestionID = value;
                textLabel.QuestionID = value;
            }
       }

       public float TotalCurrentAverageOfAllAnswers
       {
           get
           {
               return totalCurrentAverageOfAllAnswers;
           }
           set
           {
               totalCurrentAverageOfAllAnswers = value;
           }

       
       }
       public float TotalFutureAverageOfAllAnswers
       {
           get
           {
               return TotalFutureAverageOfAllAnswers;
           }
           set
           {
               totalFutureAverageOfAllAnswers = value;
           }
       }


    } //end class
}
