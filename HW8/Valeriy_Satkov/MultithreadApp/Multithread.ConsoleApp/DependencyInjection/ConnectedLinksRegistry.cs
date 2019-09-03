namespace Multithread.ConsoleApp.DependencyInjection
{
    using Multithread.ConsoleApp.Repositories;
    using Multithread.Core.Repositories;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectedLinksRegistry : Registry
    {
        public ConnectedLinksRegistry()
        {
            this.For<ILinkTableRepository>().Use<LinkTableRepository>();

            this.For<ConnectedLinksDBContext>().Use<ConnectedLinksDBContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["connectedLinksConnectionString"].ConnectionString);
        }
    }
}
