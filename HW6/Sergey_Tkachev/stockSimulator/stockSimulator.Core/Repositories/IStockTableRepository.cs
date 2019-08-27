using stockSimulator.Core.Models;

namespace stockSimulator.Core.Repositories
{
    public interface IStockTableRepository
    {
        void Add(StockEntity entity);
        void SaveChanges();
        bool Contains(StockEntity entityToCheck);
        StockEntity Get(int stockId);
        bool ContainsById(int stockId);
        void Update(int stockId, StockEntity entityToUpdate);
        decimal GetCost(int stockId);
        string GetType(int stock_ID);
    }
}
