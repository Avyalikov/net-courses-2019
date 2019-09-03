using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System.Linq;

namespace stockSimulator.WevServer.Repositories
{
    class StockTableRepository : IStockTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        public StockTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(StockEntity entity)
        {
            this.dbContext.Stocks.Add(entity);
        }

        public bool Contains(StockEntity entityToCheck)
        {
            return this.dbContext.Stocks
               .Any(s => s.Name == entityToCheck.Name
               && s.Type == entityToCheck.Type
               && s.Cost == entityToCheck.Cost);
        }

        public bool ContainsById(int stockId)
        {
            return this.dbContext.Stocks
               .Any(s => s.ID == stockId);
        }

        public StockEntity Get(int stockId)
        {
            return this.dbContext.Stocks
                .Where(s => s.ID == stockId)
                .FirstOrDefault();
        }

        public decimal GetCost(int stockId)
        {
            return this.dbContext.Stocks
               .Where(s => s.ID == stockId)
               .Select(s => s.Cost)
               .FirstOrDefault();
        }

        public string GetType(int stockId)
        {
            return this.dbContext.Stocks
              .Where(s => s.ID == stockId)
              .Select(s => s.Type)
              .FirstOrDefault();
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Update(int stockId, StockEntity entityToUpdate)
        {
            var clientToUpdate = this.dbContext.Stocks.First(s => s.ID == stockId);
            clientToUpdate = entityToUpdate;
        }
    }
}
