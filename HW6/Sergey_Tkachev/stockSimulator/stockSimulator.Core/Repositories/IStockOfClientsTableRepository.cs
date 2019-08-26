using stockSimulator.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace stockSimulator.Core.Repositories
{
    public interface IStockOfClientsTableRepository
    {
        void Add(StockOfClientsEntity entity);
        void SaveChanges();
        bool Contains(StockOfClientsEntity entityToAdd);
        StockOfClientsEntity Get(int stockId);
        bool ContainsById(int stockId);
        void Update(StockOfClientsEntity entityToUpdate);
        int GetAmount(int client_id, int stockId);
        void UpdateAmount(int client_id, int stockId, int newStockAmount);
    }
}
