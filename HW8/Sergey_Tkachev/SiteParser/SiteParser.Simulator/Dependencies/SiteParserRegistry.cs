namespace SiteParser.Simulator.Dependencies
{
    using SiteParser.Core.Repositories;
    using SiteParser.Core.Services;
    using SiteParser.Simulator.Repositories;
    using StructureMap;

    public class SiteParserRegistry : Registry
    {
        public SiteParserRegistry()
        {
            this.For<UrlCollectorService>().Use<UrlCollectorService>();
            this.For<DownloadPageService>().Use<DownloadPageService>();
            this.For<ParsePageService>().Use<ParsePageService>();
            this.For<SaveIntoDatabaseService>().Use<SaveIntoDatabaseService>();

            this.For<ISaver>().Use<SaverRepository>();
            this.For<IDownloader>().Use<DownloaderRepository>();
            this.For<ISimulator>().Use<Simulator>();
            this.For<ICleaner>().Use<CleanerRepository>();
        }
    }
}
