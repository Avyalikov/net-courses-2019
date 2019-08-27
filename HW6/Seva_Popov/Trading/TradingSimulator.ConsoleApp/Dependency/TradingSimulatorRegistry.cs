using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.ConsoleApp.Repositories;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp.Dependency
{
    public class TradingSimulatorRegistry : Registry
    {
        public TradingSimulatorRegistry()
        {
            this.For<IUserTableRepository>().Use<UserTableRepository>();
            this.For<ISharesTableRepository>().Use<SharesTableRepository>();
            this.For<IAddingSharesToThUserServiceTableRepository>().Use<AddingSharesToThUserServiceTableRepository>();
            this.For<ITransactionRepositories>().Use<TransactionRepositories>();
            this.For<TradingSimulatorDbContext>().Use<TradingSimulatorDbContext>();
            this.For<UserService>().Use<UserService>();
            this.For<SharesService>().Use<SharesService>();
            this.For<AddingSharesToThUserService>().Use<AddingSharesToThUserService>();
            this.For<TransactionService>().Use<TransactionService>();
            this.For<ITradingStart>().Use<TradingStart>();
            this.For<TradingSimulation>().Use<TradingSimulation>();
        }
    }
}
