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

namespace WikiURLCollector.ConsoleApp
{
    public class WikiUrlRegistry : Registry
    {
        public WikiUrlRegistry()
        {
            For<IUrlRepository>().Use<UrlRepository>();

            For<IUrlService>().Use<UrlService>();
            For<IUrlParsingService>().Use<UrlParsingService>();
            For<IPageDownloadingService>().Use<PageDownloadingService>().SelectConstructor(()=>new PageDownloadingService());
            For<IParallelUrlCollectingService>().Use<ParallelUrlCollectingService>();

            For<ParallelUrlCollector>().Use<ParallelUrlCollector>();
            For<WikiUrlDbContext>().Use<WikiUrlDbContext>().Singleton();
        }
    }
}
