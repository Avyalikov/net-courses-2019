using StructureMap;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WikipediaParser.Repositories;
using WikipediaParser.Services;

namespace WikipediaParser
{
    public class WikipediaParserRegistry : Registry
    {
        public WikipediaParserRegistry()
        {
            this.For<LinksTableRepository>().Use<LinksTableRepository>().Transient();
            this.For<PageParsingService>().Use<PageParsingService>();
            this.For<DownloadingService>().Use<DownloadingService>();
            this.For<WikipediaParsingService>().Use<WikipediaParsingService>();
            this.For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());
            this.For<WikiParsingDbContext>().Use<WikiParsingDbContext>().Transient();
        }
    }
}
