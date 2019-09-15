namespace WikipediaParser
{
    using StructureMap;
    using System.Net.Http;
    using WikipediaParser.Repositories;
    using WikipediaParser.Services;

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
