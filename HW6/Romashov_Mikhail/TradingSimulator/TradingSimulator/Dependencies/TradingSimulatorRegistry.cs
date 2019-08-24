using StructureMap;
using TradingSimulator.Core.Services;
using TradingSimulator.Core.Repositories;
using System.Configuration;
using TradingSimulator.Repositories;

namespace TradingSimulator.Dependencies
{
    public class TradingSimulatorRegistry : Registry
    {
        public TradingSimulatorRegistry()
        {
            this.For<ITraderTableRepository>().Use<TraderTableRepository>();
            this.For<IStockTableRepository>().Use<StockTableRepository>();
            this.For<ITraderStockTableRepository>().Use<TraderStockTableRepository>();
            this.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            this.For<IBankruptRepository>().Use<BankruptRepository>();
            //this.For<SaleService>().Use<SaleService>();
            //this.For<StockService>().Use<StockService>();
            //this.For<TradersService>().Use<TradersService>();
            //this.For<TraderStocksService>().Use<TraderStocksService>();
            //this.For<BankruptService>().Use <BankruptService>();
            this.For<TradingSimulatorDBContext>().Use<TradingSimulatorDBContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingSimulatorConnectionString"].ConnectionString);
            this.For<TradeSimulation>().Use<TradeSimulation>();
        }
    }
}
