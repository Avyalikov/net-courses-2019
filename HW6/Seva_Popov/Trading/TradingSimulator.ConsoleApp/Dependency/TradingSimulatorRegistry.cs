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
            this.For<TradingSimulatorDbContext>().Use<TradingSimulatorDbContext>();
            this.For<UserService>().Use<UserService>();
            this.For<SharesService>().Use<SharesService>();
        }

    }
}
