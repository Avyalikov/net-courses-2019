namespace Multithread.ConsoleApp.DependencyInjection
{
    using Multithread.ConsoleApp.Repositories;
    using Multithread.Core.Repositories;
    using Multithread.Core.Services;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectedLinksRegistry : Registry
    {
        public ConnectedLinksRegistry()
        {
            this.For<ILinkTableRepository>().Use<LinkTableRepository>();
            this.For<IFileManager>().Use<FileManager>();

            this.For<ParsingService>().Use<ParsingService>();
            this.For<ReportsService>().Use<ReportsService>();
            this.For<LoadService>().Use<LoadService>();

            this.For<ConnectedLinksDBContext>().Use<ConnectedLinksDBContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["connectedLinksConnectionString"].ConnectionString);
        }
    }
}
