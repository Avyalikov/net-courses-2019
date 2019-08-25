using StructureMap;
using TradingSimulator.Core.Services;
using TradingSimulator.Core.Repositories;
using System.Configuration;
using TradingSimulator.ConsoleApp.Repositories;
using TradingSimulator.ConsoleApp.Interfaces;
using TradingSimulator.ConsoleApp.Components;

namespace TradingSimulator.ConsoleApp.Dependencies
{
    public class TradingDataRegistry : Registry
    {
        public TradingDataRegistry()
        {
            this.For<ITraderTableRepository>().Use<TraderTableRepository>();
            this.For<IStockTableRepository>().Use<StockTableRepository>();
            this.For<ITraderStockTableRepository>().Use<TraderStockTableRepository>();
            this.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            this.For<IBankruptRepository>().Use<BankruptRepository>();
            this.For<ILogger>().Use<LoggerService>();
            this.For<IValidator>().Use<Validator>();
            this.For<TradingSimulatorDBContext>().Use<TradingSimulatorDBContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingSimulatorConnectionString"].ConnectionString);
        }
    }
}
