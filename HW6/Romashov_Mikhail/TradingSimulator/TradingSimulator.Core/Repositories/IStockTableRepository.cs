using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IStockTableRepository
    {
        bool Contains(StockEntity entity);
        bool ContainsById(int entityId);
        StockEntity Get(int stockID);
        bool ContainsByName(string stockName);
        StockEntity GetByName(string stockName);
    }
}
