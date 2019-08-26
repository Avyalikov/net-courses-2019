using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IStockTableRepository
    {
        void Add(StockEntity entity);
        void SaveChanges();
        bool Contains(StockEntity entity);
        bool Contains(int entityId);
        StockEntity Get(int stockId);
    }
}