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
            this.For<ILinksTableRepository>().Use<LinksTableRepository>();
            this.For<IDatasourceManagementService>().Use<DatasourceManagementService>();
            this.For<IPageParsingService>().Use<PageParsingService>();
            this.For<IDownloadingService>().Use<DownloadingService>();
            this.For<IWikipediaParsingService>().Use<WikipediaParsingService>();

            this.For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());
            this.For<WikiParsingDbContext>().Use<WikiParsingDbContext>();
        }
    }
}
