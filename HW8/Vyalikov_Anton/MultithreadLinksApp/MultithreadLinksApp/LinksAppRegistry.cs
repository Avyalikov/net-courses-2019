namespace MultithreadLinksApp
{
    using MultithreadLinksApp.Core.Interfaces;
    using MultithreadLinksApp.Core.Services;
    using MultithreadLinksApp.Core.Repos;
    using MultithreadLinksApp.Repos;
    using StructureMap;
    using System.Net.Http;

    class LinksAppRegistry : Registry
    {
        public LinksAppRegistry()
        {
            For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());

            For<AppDBContext>().Use<AppDBContext>().Singleton();
            For<URLCollector>().Use<URLCollector>();

            For<IPageService>().Use<PageService>();
            For<IURLCollectingService>().Use<URLCollectingService>();
            For<IURLParsingService>().Use<URLParsingService>();
            For<IURLService>().Use<URLService>();
            For<IURLRepository>().Use<URLRepository>();
        }
    }
}
