using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadApp.Dependencies;
using MultithreadApp.Core.Models;

namespace MultithreadApp
{
    class Program
    {
        static void Go(int num, List<PageEntity> Links, PageService pageService, int linksInDbCount)
        {
            num++;
            PageService _pageService = pageService;
            while (num < 5)
            {
                foreach (PageEntity link in Links)
                {
                    if (link.Link != "")
                    {
                        List<PageEntity> pagesList = new List<PageEntity>();
                        Console.WriteLine(link.Link + " " + link.IterationId);
                        try
                        {
                            Task.Factory.StartNew(() =>
                            {

                                string fileinfo = _pageService.DownLoadPage(link.Link);
                                List<string> listOfLinks = _pageService.ExtractHtmlTags(fileinfo);
                                foreach (string linkOfSecondOrder in listOfLinks)
                                {
                                    linksInDbCount++;
                                    PageEntity entity = new PageEntity()
                                    {
                                        Link = linkOfSecondOrder,
                                        IterationId = linksInDbCount
                                    };
                                    pageService.Add(entity);
                                    pagesList.Add(entity);
                                };
                                
                                Console.WriteLine($"Task #{num} is complite");
                                Go(num, pagesList, _pageService, linksInDbCount);

                            });
                        }
                        catch
                        {
                            throw new ArgumentException("Link can't be downloaded");
                        }

                        
                    }
                    else
                    {
                        Console.WriteLine("This link was null");
                    }
                }
            }
        }
                

        static void Main(string[] args)
        {
            var container = new Container(new MultithreadAppRegistry());

            var pageService = container.GetInstance<PageService>();
            using(var dbContext = container.GetInstance<MultithreadAppDbContext>())
            {
                int linksInDbCount = dbContext.Links.Count();
                List<PageEntity> Links = new List<PageEntity>();
                foreach(PageEntity page in dbContext.Links)
                {
                    Links.Add(page);
                }
                int num = 0;
                Go(num, Links, pageService, linksInDbCount);
   
                Console.WriteLine("End of Main");
                Console.ReadLine();
            }
            

        }
    }
}
