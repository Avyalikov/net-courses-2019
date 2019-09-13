namespace Multithread.Console.DependencyInjection
{
    using Multithread.Console.Repo;
    using Multithread.Core.Repo;
    using Multithread.Core.Services;
    using StructureMap;

    public class AppRegistry :Registry
    {
        public AppRegistry()
        {
            this.For<ILinksRepo>().Use<LinksRepo>();
            this.For<IWebRepo>().Use<WebRepo>();
            this.For<LinksDbContext>().Use<LinksDbContext>();
            this.For<LinkProcessingService>().Use<LinkProcessingService>();
        }
    }
}
