using StructureMap;
using System;
using System.Collections.Generic;
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

            this.For<DownloadingService>().Use<DownloadingService>();
            this.For<PageParsingService>().Use<PageParsingService>();
            this.For<WikipediaParsingService>().Use<WikipediaParsingService>();

            this.For<WikiParsingDbContext>().Use<WikiParsingDbContext>().Transient();
        }
    }
}
