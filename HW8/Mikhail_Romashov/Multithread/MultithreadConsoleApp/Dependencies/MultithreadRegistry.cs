using Multithread.Core.Repositories;
using MultithreadConsoleApp.Repositories;
using StructureMap;
using System.Configuration;

namespace MultithreadConsoleApp.Dependencies
{
    public class MultithreadRegistry : Registry
    {
        public MultithreadRegistry()
        {
            this.For<ILinkTableRepository>().Use<LinkRepository>();
            this.For<LinksDBContext>().Use<LinksDBContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["linksConnectionString"].ConnectionString);
        }
    }
}
