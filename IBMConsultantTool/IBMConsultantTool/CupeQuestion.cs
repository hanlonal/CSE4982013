﻿using System;
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
       private Label textLabel;
       private int id;

       private CustomBox futureTextBox;
       private CustomBox currentTextBox;

       private string questionText;
       Panel owner = new Panel();


       public CupeQuestion()
       {
           futureTextBox = new CustomBox(this);
           currentTextBox = new CustomBox(this);
           textLabel = new Label();

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
               Console.WriteLine(textLabel.Text + " texts reads" + current.ToString());
           }
       }
       public string Future { get { return future; } set { future = value; Console.WriteLine(textLabel.Text + " texts reads" + future.ToString()); } }
       public Label TextLabel { get { return textLabel; } }
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

       private void DefineCurrentBox()
       {
           currentTextBox.Width = 20;
           currentTextBox.Height = 20;

       }
       private void DefineFutureBox()
       {
           //q.Visible = true;
           futureTextBox.Width = 20;
           futureTextBox.Height = 20;   
       }

       private void DefineTextLabel()
       {
           textLabel.BackColor = Color.Red;
           textLabel.Width = 70;
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
            }
       }


    } //end class
}