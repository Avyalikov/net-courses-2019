using Multithread.Core.Dto;
using Multithread.Core.Services;
using MultithreadConsoleApp.Components;
using System;

namespace MultithreadConsoleApp
{
    class Multithread
    {
        private readonly LinkService linkService;
        public Multithread(LinkService linkService)
        {
            this.linkService = linkService;
        }

        public void Run()
        {
            string url = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
            var result = HtmlReader.ReadHttp(url);
            IOFile.WriteToFile(result.Result, @"C:\multi\mummy.txt");
            var newResult = IOFile.ReadFromFile(@"C:\multi\mummy.txt");
            var collection = HtmlParser.FindLinksFromStr(newResult);

            int iteration = 1;
            foreach (var item in collection)
            {
                if (!linkService.ContainsByLink(item))
                {
                    LinkInfo entityToAdd = new LinkInfo()
                    {
                        Link = item,
                        Iteration = iteration
                    };

                    linkService.AddNewLink(entityToAdd);
                }
            }

            Console.WriteLine("The end. Check db!!");

        }
    }
}
