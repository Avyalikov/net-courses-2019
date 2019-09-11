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
            List<string> s = parserPages.GettingTheInitialListOfLinks();
            Parallel.ForEach(s, sb => { parserPages.ParsingThePageCsQuery("https://en.wikipedia.org" + sb); });
            Console.ReadKey();
        }
    }
}
