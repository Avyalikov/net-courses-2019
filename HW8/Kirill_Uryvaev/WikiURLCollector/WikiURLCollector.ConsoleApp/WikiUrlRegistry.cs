using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Repositories;
using WikiURLCollector.ConsoleApp.Repositories;
using System.Net.Http;

namespace WikiURLCollector.ConsoleApp
{
    public class WikiUrlRegistry : Registry
    {
        public WikiUrlRegistry()
        {
            For<IUrlRepository>().Use<UrlRepository>();

            For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());

            For<IUrlService>().Use<UrlService>();
            For<IUrlParsingService>().Use<UrlParsingService>();
            For<IPageService>().Use<PageService>();
            For<IParallelUrlCollectingService>().Use<ParallelUrlCollectingService>();

            For<ParallelUrlCollector>().Use<ParallelUrlCollector>();
            For<WikiUrlDbContext>().Use<WikiUrlDbContext>().Singleton();
        }
    }
}
