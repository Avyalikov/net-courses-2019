using Multithread.Core.Repositories;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using MultithreadConsoleApp.Repositories;
using StructureMap;
using System.Net.Http;

namespace MultithreadConsoleApp.Dependencies
{
    public class MultithreadRegistry : Registry
    {
        public MultithreadRegistry()
        {
            For<HttpClient>().Use<HttpClient>().SelectConstructor(() => new HttpClient());
            this.For<ILinkTableRepository>().Use<LinkRepository>();
            this.For<IHtmlParser>().Use<HtmlParser>();
            this.For<IHtmlReader>().Use<HtmlReader>();
            this.For<IDataBaseManager>().Use<DataBaseManager>();
            this.For<IFileSystemManager>().Use<FileSystemManager>();
            this.For<LinksDBContext>().Use<LinksDBContext>();
        }
    }
}
