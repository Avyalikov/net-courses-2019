using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IStockTableRepository
    {
        void Add(StockEntity entity);
        void SaveChanges();
        bool Contains(StockEntity entity);
        bool Contains(string entityId);
        StockEntity Get(string stockId);
    }
}