using Multithread.Core.Repositories;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using MultithreadConsoleApp.Repositories;
using StructureMap;

namespace MultithreadConsoleApp.Dependencies
{
    public class MultithreadRegistry : Registry
    {
        public MultithreadRegistry()
        {
            this.For<ILinkTableRepository>().Use<LinkRepository>();
            this.For<IHtmlParser>().Use<HtmlParser>();
            this.For<LinksDBContext>().Use<LinksDBContext>();
        }
    }
}
