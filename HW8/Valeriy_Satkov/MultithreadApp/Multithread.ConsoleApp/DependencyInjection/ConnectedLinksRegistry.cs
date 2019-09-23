namespace Multithread.ConsoleApp.DependencyInjection
{
    using Multithread.ConsoleApp.Repositories;
    using Multithread.Core.Repositories;
    using Multithread.Core.Services;
    using StructureMap;
    using System.Configuration;

    public class ConnectedLinksRegistry : Registry
    {
        public ConnectedLinksRegistry()
        {
            this.For<ILinkTableRepository>().Use<LinkTableRepository>();
            this.For<IFileManager>().Use<FileManager>();

            this.For<ParsingService>().Use<ParsingService>();
            this.For<ReportsService>().Use<ReportsService>();

            this.For<ConnectedLinksDBContext>().Use<ConnectedLinksDBContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["connectedLinksConnectionString"].ConnectionString);
        }
    }
}
