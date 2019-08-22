using stockSimulator.Core.Models;

namespace stockSimulator.Core.Repositories
{
    public interface IStockTableRepository
    {
        void Add(StockEntity entity);
        void SaveChanges();
        bool Contains(StockEntity entityToAdd);
        StockEntity Get(int stockId);
        bool ContainsById(int stockId);
        void Update(StockEntity entityToUpdate);
        decimal GetCost(int stockId);
    }
}
