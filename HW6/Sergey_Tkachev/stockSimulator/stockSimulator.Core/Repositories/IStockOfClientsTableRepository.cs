namespace stockSimulator.Core.Repositories
{
    using System.Linq;
    using stockSimulator.Core.Models;

    public interface IStockOfClientsTableRepository
    {
        void Add(StockOfClientsEntity entity);
        void SaveChanges();
        bool Contains(StockOfClientsEntity entityToCheck, out int entityId);
        StockOfClientsEntity Get(int entityId);
        bool ContainsById(int entityId);
        string Update(int entityId, StockOfClientsEntity newEntity);
        int GetAmount(int client_id, int stockId);
        void UpdateAmount(int client_id, int stockId, int newStockAmount);
        IQueryable<StockOfClientsEntity> GetStocksOfClient(int clientId);
    }
}
