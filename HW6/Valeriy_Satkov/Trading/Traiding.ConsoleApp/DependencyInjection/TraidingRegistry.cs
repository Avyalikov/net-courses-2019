namespace Traiding.ConsoleApp.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StructureMap;
    using Traiding.ConsoleApp.Repositories;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    public class TraidingRegistry : Registry
    {
        public TraidingRegistry()
        {
            this.For<IClientTableRepository>().Use<ClientTableRepository>();
            this.For<IBalanceTableRepository>().Use<BalanceTableRepository>();
            this.For<ISharesNumberTableRepository>().Use<SharesNumberTableRepository>();

            this.For<IShareTypeTableRepository>().Use<ShareTypeTableRepository>();
            this.For<IShareTableRepository>().Use<ShareTableRepository>();

            this.For<IOperationTableRepository>().Use<OperationTableRepository>();
            this.For<IBlockedMoneyTableRepository>().Use<BlockedMoneyTableRepository>();
            this.For<IBlockedSharesNumberTableRepository>().Use<BlockedSharesNumberTableRepository>();

            this.For<StockExchangeDBContext>().Use<StockExchangeDBContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["traidingConnectionString"].ConnectionString);
        }
    }    
}
