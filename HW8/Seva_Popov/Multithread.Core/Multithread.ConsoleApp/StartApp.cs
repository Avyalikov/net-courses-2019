using Multithread.ConsoleApp.Data;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.ConsoleApp
{
    public class StartApp
    {
        private readonly MultithreadDbContext dbContext;
        private readonly ILinksHistoryRepositoroes linksHistoryRepositoroes;
        private readonly LinksHistoryServices linksHistoryServices;

        public StartApp(
            MultithreadDbContext dbContext, 
            ILinksHistoryRepositoroes linksHistoryRepositoroes,
            LinksHistoryServices linksHistoryServices)
        {
            this.dbContext = dbContext;
            this.linksHistoryRepositoroes = linksHistoryRepositoroes;
            this.linksHistoryServices = linksHistoryServices;
        }

        public void Run()
        {

        }
    }
}
