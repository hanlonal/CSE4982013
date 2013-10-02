using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace IBMConsultantTool
{
   public class CupeQuestion
    {


      

       private string current;
       private string future;
       private string text;
       private CustomLabel textLabel;
       private int id;

       private CustomBox futureTextBox;
       private CustomBox currentTextBox;

       private string questionText;
       Panel owner = new Panel();

       private float totalCurrentAverageOfAllAnswers;
       private float totalFutureAverageOfAllAnswers;
       private float totalAverageofAllITAnswers;
       private float totalAverageofAllBusiAnswers;


       public CupeQuestion()
       {
           futureTextBox = new CustomBox(this);
           currentTextBox = new CustomBox(this);
           textLabel = new CustomLabel(this);

           DefineFutureBox();
           DefineCurrentBox();
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
       public string Future { get { return future; } set { future = value; Console.WriteLine(textLabel.Text + " texts reads" + future.ToString()); } }
       public CustomLabel TextLabel { get { return textLabel; } }
       public TextBox FutureBox { get { return futureTextBox; } }
       public TextBox CurrentBox { get { return currentTextBox; } set { CurrentBox = value; } }
       public Panel Owner
       { 
           set 
           { 
               owner = value; 
               owner.Controls.Add(textLabel);
               owner.Controls.Add(futureTextBox);
               owner.Controls.Add(currentTextBox);
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
