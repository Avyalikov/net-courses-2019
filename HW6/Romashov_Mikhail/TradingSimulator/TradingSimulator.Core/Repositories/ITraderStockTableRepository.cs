using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITraderStockTableRepository
    {
        void Add(StockToTraderEntity entityToAdd);
        void SaveChanges();
        bool Contains(StockToTraderEntity stockToTraderEntity);
        StockToTraderEntity FindStocksFromSeller(BuyArguments buyArguments);
        void SubtractStock(BuyArguments args);
        bool Contains(BuyArguments args);
    }
}
