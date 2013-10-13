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

       public ITCapFileManager(ITCapTool owner)
       {
           subFolders.Add("BOMTool");
           subFolders.Add("CUPETool");
           subFolders.Add("ITCAP");
           this.mainForm = owner;

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
               System.IO.File.Create((@"c:\Consultant Tool\ITCap\Domains\" + name));
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
               string domainName = System.IO.Path.GetFileName(name);
               string[] test = System.IO.File.ReadAllLines(name);
               Console.WriteLine(test[0]);
               mainForm.CreateDomain(domainName, test[0]);
           }
       }

    }// end class
}
