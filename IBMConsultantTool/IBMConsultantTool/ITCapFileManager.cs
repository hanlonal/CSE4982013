using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IBMConsultantTool
{
   public class ITCapFileManager
    {
       List<string> subFolders = new List<string>();
       string rootfoldername = @"c:\Consultant Tool";
       ITCapTool mainForm;
       private int highestID = 0;

       private int defaultinList = 1;
       private int notdefault = 0;

       string[] originalFileWrite = new string[5];


       int idIndex = 0;
       int defaultIndex = 1;
       int startCapabilityIndex = 2;


       public ITCapFileManager(ITCapTool owner)
       {
           subFolders.Add("BOMTool");
           subFolders.Add("CUPETool");
           subFolders.Add("ITCAP");
           this.mainForm = owner;

           originalFileWrite[0] = highestID.ToString();
           originalFileWrite[1] = "InDefaultList";
       }


       public void CheckFileSystem()
       {
          
           foreach (string name in subFolders)
           {
               rootfoldername = @"c:\Consultant Tool";
               string pathString = System.IO.Path.Combine(rootfoldername, name);
               System.IO.Directory.CreateDirectory(pathString);
           }

           CreateListOfDomains();

                      
       }

       public bool AddDomainToSystem(string name)
       {
           if (!System.IO.File.Exists(@"c:\Consultant Tool\ITCap\Domains\" + name))
           {
               FileStream createdFile = System.IO.File.Create(@"c:\Consultant Tool\ITCap\Domains\" + name);
               createdFile.Close();
               originalFileWrite[defaultIndex] = defaultinList.ToString();
               IncreaseIDNumber();
               System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Domains\" + name, originalFileWrite);
             

               return true;
           }
           else
           {
               return false;
           }
       }

       private void CreateListOfDomains()
       {
           string[] filenames = Directory.GetFiles(@"c:\Consultant Tool\ITCap\Domains\");
           filenames.ToList<String>();
           
           foreach (string name in filenames)
           {
               Console.WriteLine("here");
               string domainName = System.IO.Path.GetFileName(name);
               string[] test = System.IO.File.ReadAllLines(name);
               Console.WriteLine(name);
               mainForm.CreateDomain(domainName, Convert.ToInt32(test[idIndex]));
           }
       }
       public int  GetHighestIDNumber()
       {
           string[] test = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           return Convert.ToInt32(test[idIndex]);
       }
       public void IncreaseIDNumber()
       {
           string[] fileContentsbyLine = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           int id = Convert.ToInt32(fileContentsbyLine[idIndex]);
           id++;
           highestID = id;
           originalFileWrite[idIndex] = highestID.ToString();
           fileContentsbyLine[idIndex] = id.ToString();
           System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Info.txt", fileContentsbyLine);
       }

    }// end class
}
