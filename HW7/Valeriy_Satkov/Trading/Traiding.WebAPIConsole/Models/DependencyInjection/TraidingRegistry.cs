namespace Traiding.WebAPIConsole.Models.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StructureMap;    
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;
    using Traiding.WebAPIConsole.Models.Repositories;

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

            this.For<ClientsService>().Use<ClientsService>();
            this.For<BalancesService>().Use<BalancesService>();
            this.For<SalesService>().Use<SalesService>();
            this.For<ReportsService>().Use<ReportsService>();
            this.For<SharesService>().Use<SharesService>();
            this.For<ShareTypesService>().Use<ShareTypesService>();

            //this.For<StockExchangeDBContext>().Use<StockExchangeDBContext>().Ctor<string>("connectionString")
            //    .Is(ConfigurationManager.ConnectionStrings["traidingConnectionString"].ConnectionString);
        }
    }    
}
