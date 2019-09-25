using stockSimulator.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace stockSimulator.Core.Repositories
{
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
        IEnumerable<StockOfClientsEntity> GetStocksOfClient(int clientId);
        string Remove(int entityId, StockOfClientsEntity entityToRemove);
    }
}
