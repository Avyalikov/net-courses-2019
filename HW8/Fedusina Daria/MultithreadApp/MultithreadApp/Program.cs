using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadApp.Dependencies;
using MultithreadApp.Core.Models;
using MultithreadApp.Core.Dto;

namespace MultithreadApp
{
    class Program
    {    

        static void Main(string[] args)
        {
            Logger.InitLogger(); //logger initialization
            Logger.Log.Info(DateTime.Now + " The application has started ");
            var container = new Container(new MultithreadAppRegistry());
            var pageService = container.GetInstance<PageService>();

            using (var dbContext = container.GetInstance<MultithreadAppDbContext>())
            {
                int linksInDbCount = dbContext.Links.Count();       // just calling dbContext here, for Db initialization

                PageRegistrationInfo pageInfo = new PageRegistrationInfo(); //dto
                ParseWebProcess parseWebProcess = new ParseWebProcess();   // main logic methods 

                (int, string) tempTuple = parseWebProcess.Info;
                pageInfo.num = tempTuple.Item1;
                pageInfo.url = tempTuple.Item2;
                pageInfo.pageService = pageService;

                var count = 0;
                try
                {   //one thread
                    string fileinfo = pageService.DownLoadPage(pageInfo.url);           //downloads a webpage to a file
                    List<string> listOfLinks = pageService.ExtractHtmlTags(fileinfo);  // extract all links on the webpage to a list
                    foreach(string link in listOfLinks)
                    {
                        pageInfo.url = link;
                        PageEntity entity = new PageEntity()                    // creating a PageEntity object to put in the data base
                        {
                            Link = link,
                            IterationId = count                                 // this parameter shows the layer of depth    
                        };
                        pageService.Add(entity);
                        pageInfo.url = link;
                        pageInfo.count = 0;

                    }
                    //multithreading
                    parseWebProcess.Go(pageInfo);                     //recursive method that parse webpages and put links in the database

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Logger.Log.Info(DateTime.Now + " process has failed for link pageInfo.url :" + e.Message) ;
                }
                
                
            }
            Console.WriteLine("Wait...");
            
            Console.ReadLine();
            Task.WaitAll();
            Console.WriteLine("Asyncronous task finished!");
        }

       
    }
}
