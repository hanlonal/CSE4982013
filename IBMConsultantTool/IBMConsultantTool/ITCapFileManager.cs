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
       string[] capabilityFileContents = new string[4096];
       private int capabilityIDIndex = 0;
       private int defaultInListIndexCap = 1;

       // file info for individual domains
       string[] domainFileContents = new string[4096];
       int idDomainIndex = 0;
       int defaultInListIndexDomain = 1;
       int numberOfCapabilitiesOwnedIndex = 2;
       int startOfCapabilitiesOwnedIndex = 3;

       private string CapabilityFolderPath = @"c:\Consultant Tool\ITCap\Capabilities\";

       //file information for Info File
       string[] infoFileContents = new string[4096];
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
               //lines to get written to main info file
           infoFileContents[domainCountIndex] = domainCount.ToString();
           infoFileContents[capabilityCountIndex] = capabilityCount.ToString();
           infoFileContents[questionCountIndex] = questionCount.ToString();


           //lines in info file to count data
           domainFileContents[idDomainIndex] = domainCount.ToString();
           domainFileContents[defaultInListIndexDomain] = "InDefault List";
           domainFileContents[numberOfCapabilitiesOwnedIndex] = "0";

                      
           //lines to get written to capability files
           capabilityFileContents[capabilityIDIndex] = capabilityCount.ToString();
           capabilityFileContents[defaultInListIndexCap] = "In default list";


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


           CreateListOfDomainsCapabilitiesAndQuestions();
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
               //open domain file to add capability
               string[] fileContents = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Domains\" +dom.Name);
               fileContents[startOfCapabilitiesOwnedIndex + dom.NumCapabilities] = name;
               fileContents[numberOfCapabilitiesOwnedIndex] = (dom.NumCapabilities +1).ToString();
               IncreaseIDNumberCapability();
               System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Domains\" + dom.Name, fileContents);
               System.IO.File.WriteAllLines(@"c:\Consultant Tool\ITCap\Capabilities\" + name, capabilityFileContents);
               return true;
           }

           return true;
       }

       private void CreateListOfDomainsCapabilitiesAndQuestions()
       {
           string[] filenames = Directory.GetFiles(@"c:\Consultant Tool\ITCap\Domains\");
           filenames.ToList<String>();
           
           foreach (string name in filenames)
           {
               //Console.WriteLine("here");
               string domainName = System.IO.Path.GetFileName(name);
               string[] test = System.IO.File.ReadAllLines(name);

              Domain dom = mainForm.CreateDomain(domainName, Convert.ToInt32(test[idDomainIndex]));
              for (int i = startOfCapabilitiesOwnedIndex; i < Convert.ToInt32(test[numberOfCapabilitiesOwnedIndex]) + startOfCapabilitiesOwnedIndex; i++)
              {
                  Console.WriteLine("ere");
                  string[] capFileInfo =  GetFileInfo(test[i], "cap");
                  mainForm.CreateCapability(test[i], Convert.ToInt32(capFileInfo[capabilityIDIndex]), dom);
              }
               
           }
       }


       public string[] GetFileInfo(string name, string type)
       {
           if (type == "cap")
           {
               string[] filecontents = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Capabilities\" + name);
               return filecontents;
           }

           return null;
           
       }

       public List<string> GetChildren(Domain dom)
       {
           string[] filecontents = System.IO.File.ReadAllLines(@"c:\Consultant Tool\ITCap\Domains\" + dom.Name);
           List<string> capsToMake = new List<string>();
          for(int i = startOfCapabilitiesOwnedIndex;i < Convert.ToInt32(filecontents[startOfCapabilitiesOwnedIndex + dom.NumCapabilities ]); i++)
          {
              capsToMake.Add(filecontents[i]);
          }
          return capsToMake;
       }



       /* private void CreateListOfCapabilities()
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

       public int GetHighestIDNumberDomain()
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
