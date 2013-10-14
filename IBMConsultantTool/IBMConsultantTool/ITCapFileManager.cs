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
       
       

       private int defaultinList = 1;
       private int notdefault = 0;

       
       //string[] fileWriteCapability = new string[5];

       // file information for individual capabilities
       List<string> capabilityFileContents = new List<string>();
       private int capabilityIDIndex = 0;
       private int defaultInListIndexCap = 1;

       // file info for individual domains
       List<string> domainFileContents = new List<string>();
       int idDomainIndex = 0;
       int defaultInListIndexDomain = 1;
       int numberOfCapabilitiesOwnedIndex = 2;
       int startOfCapabilitiesOwnedIndex = 3;
 


       //file information for Info File
       List<string> infoFileContents = new List<string>();
       private int domainCount = 0;
       private int capabilityCount = 0;
       private int questionCount = 0;
       //indexes for lines in info file
       private int domainCountIndex = 0;
       private int capabilityCountIndex = 1;
       private int questionCountIndex = 2;//
       



       public ITCapFileManager(ITCapTool owner)
       {
           subFolders.Add("BOMTool");
           subFolders.Add("CUPETool");
           subFolders.Add("ITCAP");
           this.mainForm = owner;
           infoFileContents.Add(domainCount.ToString());
           infoFileContents.Add(capabilityCount.ToString());
           infoFileContents.Add(questionCount.ToString());
           //lines in info file to count data

           domainFileContents.Add(domainCount.ToString());
           domainFileContents.Add("In default List");
           domainFileContents.Add("0");


           
           //lines to get written to capability files
           capabilityFileContents.Add(capabilityCount.ToString());
           capabilityFileContents.Add("In default List");






       }


       public void CheckFileSystem()
       {
          
           foreach (string name in subFolders)
           {
              rootfoldername = @"c:\Consultant Tool";
              string pathString = System.IO.Path.Combine(rootfoldername, name);
              System.IO.Directory.CreateDirectory(pathString);
              string pathString2 = System.IO.Path.Combine(pathString, "Capabilities");
              System.IO.Directory.CreateDirectory(pathString);
              string pathString3 = System.IO.Path.Combine(pathString, "Questions");
              System.IO.Directory.CreateDirectory(pathString3);
           }

           CreateListOfDomains();
           //CreateListOfCapabilities();
           //CreateListOfCapabilities();

                      
       }

       public bool AddDomainToSystem(string name)
       {
           if (!System.IO.File.Exists(@"c:\Consultant Tool\ITCap\Domains\" + name))
           {
               FileStream createdFile = System.IO.File.Create(@"c:\Consultant Tool\ITCap\Domains\" + name);
               createdFile.Close();
               //originalFileWriteDomain[defaultIndex] = defaultinList.ToString();
               IncreaseIDNumberDomain();
               System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Domains\" + name, domainFileContents);
             

               return true;
           }
           else
           {
               return false;
           }
       }

       public bool AddCapabilityToSystem(string name, Domain dom)
       {
           if (!System.IO.File.Exists(@"c:\Consultant Tool\ITCap\Capabilities\" + name))
           {
               FileStream createdFile = System.IO.File.Create(@"c:\Consultant Tool\ITCap\Capabilities\" + name);
               createdFile.Close();

               IncreaseIDNumberCapability();
               System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Capabilities\" + name, capabilityFileContents);
               return true;
           }

           return true;
       }

       private void CreateListOfDomains()
       {
           string[] filenames = Directory.GetFiles(@"c:\Consultant Tool\ITCap\Domains\");
           filenames.ToList<String>();
           
           foreach (string name in filenames)
           {
               //Console.WriteLine("here");
               string domainName = System.IO.Path.GetFileName(name);
               string[] test = System.IO.File.ReadAllLines(name);
               Console.WriteLine(name);
               mainForm.CreateDomain(domainName, Convert.ToInt32(test[idDomainIndex]));
           }
       }
       /*private void CreateListOfCapabilities()
       {
           string[] filenames = Directory.GetFiles(@"c:\Consultant Tool\ITCap\Capabilities\");
           filenames.ToList<String>();

           foreach (string name in filenames)
           {
               string capabilityName = System.IO.Path.GetFileName(name);
               string[] contents = System.IO.File.ReadAllLines(name);
               mainForm.CreateCapability(capabilityName, Convert.ToInt32(contents[capabilityIDIndex]));
           }
       }*/

       public int  GetHighestIDNumberDomain()
       {
           string[] test = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           return Convert.ToInt32(test[idDomainIndex]);
       }
       public int GetHighestIDNumberCapability()
       {
           string[] test = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           infoFileContents[capabilityCountIndex] = test[capabilityCountIndex];
           return Convert.ToInt32(test[capabilityCountIndex]);
       }

       public void IncreaseIDNumberDomain()
       {
           string[] fileContentsbyLine = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           int id = Convert.ToInt32(fileContentsbyLine[idDomainIndex]);
           id++;
           domainCount = id;
           fileContentsbyLine[domainCountIndex] = domainCount.ToString();
           domainFileContents[idDomainIndex] = id.ToString();
           System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Info.txt", fileContentsbyLine);
       }
       
       
       public void IncreaseIDNumberCapability()
       {
           string[] fileContentsbyLine = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Info.txt");
           int id = Convert.ToInt32(fileContentsbyLine[capabilityCountIndex]);
           id++;
           capabilityCount = id;
           fileContentsbyLine[capabilityCountIndex] = capabilityCount.ToString();
           capabilityFileContents[capabilityIDIndex] = id.ToString();
           System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Info.txt", fileContentsbyLine);

           
       }

    }// end class
}
