using MultithreadApp.Core.Dto;
using MultithreadApp.Core.Models;
using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using log4net;
using System.Threading.Tasks;

namespace MultithreadApp
{
    public class ParseWebProcess
    {
        public (int, string) Info
        {
            get
            {
                Console.WriteLine("Please, enter a link");
                string link = Console.ReadLine();
                bool isSuccess = false;
                int _num = new int();
                while (!isSuccess)                         // validation (till user puts a number into console)
                {
                    Console.WriteLine("Please, set the number of search depth");  
                    string UserInput = Console.ReadLine();
                    isSuccess = int.TryParse(UserInput, out int t);
                    _num = t;
                }
                return (_num, link);
            }
        }

        public async void Go(PageRegistrationInfo pageRegistrationInfo)  //    int count, PageService pageService
        {
            int count = pageRegistrationInfo.count;
            int num = pageRegistrationInfo.num;
            string url = pageRegistrationInfo.url;
            PageService pageService = pageRegistrationInfo.pageService;
            while(count < num)                                                              // when number of calls of Go method became more  than number of depth
            {
                try
                {
                    await Task.Run(() =>                            // initialize and run anonymous task 
                    {
                        List<PageEntity> listOfPages = new List<PageEntity>();
                        List<string> listOfLinks = new List<string>();
                        string fileinfo = String.Empty;
                        try
                        {
                            listOfLinks = pageService.GetNewBanchOfLinks(count);
                            count++;
                            //}

                            foreach (string linkOfSecondOrder in listOfLinks)
                            {
                                if (linkOfSecondOrder != "")
                                {
                                    PageEntity entity = new PageEntity()                    // creating a PageEntity object to put in the data base
                                    {
                                        Link = linkOfSecondOrder,
                                        IterationId = count                                 // this parameter shows the layer of depth    
                                    };
                                    pageService.Add(entity);
                                    PageRegistrationInfo tempInfo = new PageRegistrationInfo();
                                    tempInfo.num = num;
                                    tempInfo.count = count;
                                    tempInfo.url = linkOfSecondOrder;
                                    tempInfo.pageService = pageService;
                                    Console.WriteLine(linkOfSecondOrder + " " + count);
                                    Go(tempInfo);                                           // parsing each link from that webpage

                                }
                                else
                                {
                                    Logger.Log.Info(DateTime.Now + $"the link from {url} was empty");
                                }
                                Console.WriteLine(linkOfSecondOrder + " " + count);

                            };
                        }
                        catch
                        {
                            Logger.Log.Info(DateTime.Now + $" downloading on  {url} was failed");
                        }

                    });

                }
                catch
                {
                    Logger.Log.Info(DateTime.Now + $"Failed to run new task for  {url}");
                }

            }
        }
    }
}
