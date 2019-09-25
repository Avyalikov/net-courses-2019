namespace ReferenceCollectorApp.Dependencies
{
    using ReferenceCollectorApp.Context;
    using ReferenceCollectorApp.Repositories;
    using ReferenceCollectorApp.Services;
    using StructureMap;
    using ReferenceCollector.Core.Repositories;
    using ReferenceCollector.Core.Services;
    using System.Net.Http;

    public class ReferenceCollectorRegistry : Registry
    {
        public ReferenceCollectorRegistry()
        {
            this.For<IReferenceTable>().Use<ReferencesTable>();
            this.For<ReferenceCollectorDbContext>().Use<ReferenceCollectorDbContext>();
            this.For<IIoDevice>().Use<ConsoleIoDevice>();
            this.For<IFileSystemService>().Use<FileSystemService>();
            this.For<IPageDownloadService>().Use<PageDownloadService>();
            this.For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());
            this.For<IFileSystemRepository>().Use<FileSystemRepository>();
            this.For<IWikiReferencesParsingService>().Use<WikiReferencesParsingService>();
            this.For<IReferencesDbService>().Use<ReferencesDbService>();
            this.For<IWikiReferenceCollector>().Use<WikiReferencesCollector>();
        }
    }
}
