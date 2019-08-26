using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;

namespace stockSimulator.Modulation.Repositories
{
    class StockTableRepository : IStockTableRepository
    {
        public void Add(StockEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(StockEntity entityToAdd)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsById(int stockId)
        {
            throw new System.NotImplementedException();
        }

        public StockEntity Get(int stockId)
        {
            throw new System.NotImplementedException();
        }

        public decimal GetCost(int stockId)
        {
            throw new System.NotImplementedException();
        }

        public string GetType(int stock_ID)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Update(StockEntity entityToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}