using Multithread.ConsoleApp.Components;
using Multithread.ConsoleApp.Data;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Multithread.ConsoleApp
{
    public class StartApp
    {
        private readonly MultithreadDbContext dbContext;
        private readonly ILinksHistoryRepositoroes linksHistoryRepositoroes;
        private readonly LinksHistoryServices linksHistoryServices;
        private readonly ParserPages parserPages;

        public StartApp(
            MultithreadDbContext dbContext, 
            ILinksHistoryRepositoroes linksHistoryRepositoroes,
            LinksHistoryServices linksHistoryServices,
            ParserPages parserPages)
        {
            this.dbContext = dbContext;
            this.linksHistoryRepositoroes = linksHistoryRepositoroes;
            this.linksHistoryServices = linksHistoryServices;
            this.parserPages = parserPages;
        }

        public void Run()
        {
            //List<string> linrsList = new List<string>();
            //linrsList.Add("https://en.wikipedia.org/wiki/Main_Page");

            //ParserPages parserPages = new ParserPages(dbContext, linksHistoryRepositoroes, linksHistoryServices);
            //parserPages.ParsingThePageCsQuery(linrsList);

            List<string> s = parserPages.CsQuery();
            Parallel.ForEach(s, sb => { parserPages.ParsingThePageCsQuery("https://en.wikipedia.org" + sb); });



            Console.WriteLine("Я усе");
            Console.ReadKey();


            //Task task = new Task(parserPages.ParsingThePageCsQuery, "https://en.wikipedia.org" + s[1]);

            //foreach (var item in s)
            {
                //parserPages.ParsingThePageCsQuery("https://en.wikipedia.org" + item);
                ////Task task = new Task(parserPages.ParsingThePageCsQuery, item)
                ////task.Start();
                //Task task1 = new Task(new Action<string>(parserPages.ParsingThePageCsQuery), "httpdsf");
                //task1.Start();
            }
        }

        ////public void AsuncMeto(int x)
        ////{
        ////    for (int i = 0; i < x; i++)
        ////    {
        ////        parserPages.ParsingThePageCsQuery("https://en.wikipedia.org" + x);
        ////    }
        ////}
        //Parallel.ForEach(colors, color =>
        //    {
        //        Console.WriteLine("{0}, Thread Id= {1}", color, Thread.CurrentThread.ManagedThreadId);
        //        Thread.Sleep(10);
        //    }
        //    );
        //    Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", sw.Elapsed.TotalSeconds);
        //    Console.Read();
    }
}
