using stockSimulator.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        IQueryable<StockOfClientsEntity> GetStocksOfClient(int clientId);
    }
}
