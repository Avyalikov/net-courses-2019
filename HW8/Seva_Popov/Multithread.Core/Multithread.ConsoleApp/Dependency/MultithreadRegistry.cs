using Multithread.ConsoleApp.Components;
using Multithread.ConsoleApp.Data;
using Multithread.ConsoleApp.Repositories;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.ConsoleApp.Dependency
{
    public class MultithreadRegistry : Registry
    {
        public MultithreadRegistry()
        {
            this.For<ILinksHistoryRepositoroes>().Use<LinksHistoryRepositoroes>();
            this.For<LinksHistoryServices>().Use<LinksHistoryServices>();
            this.For<MultithreadDbContext>().Use<MultithreadDbContext>();
            this.For<StartApp>().Use<StartApp>();
            this.For<ParserPages>().Use<ParserPages>();
        }
    }
}
