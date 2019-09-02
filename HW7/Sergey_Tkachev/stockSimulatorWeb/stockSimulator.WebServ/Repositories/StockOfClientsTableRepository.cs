using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System.Linq;

namespace stockSimulator.WebServ.Repositories
{
    class StockOfClientsTableRepository : IStockOfClientsTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        public StockOfClientsTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(StockOfClientsEntity entity)
        {
            this.dbContext.StockOfClients.Add(entity);
        }

        public bool Contains(StockOfClientsEntity entityToCheck, out int entityId)
        {
            var entry = this.dbContext.StockOfClients
              .FirstOrDefault(sc => sc.ClientID == entityToCheck.ClientID
              && sc.StockID == entityToCheck.StockID);
            if (entry != null)
            {
                entityId = entry.ID;
                return true;
            }
            entityId = 0;
            return false;
        }

        public bool ContainsById(int entityId)
        {
            return this.dbContext.StockOfClients
              .Any(sc => sc.ID == entityId);
        }

        public StockOfClientsEntity Get(int entityId)
        {
            return this.dbContext.StockOfClients
               .Where(sc => sc.ID == entityId)
               .FirstOrDefault();
        }

        public int GetAmount(int client_id, int stockId)
        {
            return this.dbContext.StockOfClients
               .Where(sc => sc.ClientID == client_id
               && sc.StockID == stockId)
               .Select(soc => soc.Amount)
               .FirstOrDefault();
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Update(int entityId, StockOfClientsEntity newEntity)
        {
            var entityToUpdate = this.dbContext.StockOfClients.First(sc => sc.ID == entityId);
            entityToUpdate = newEntity;
        }

        public void UpdateAmount(int client_id, int stockId, int newStockAmount)
        {
            var entityToUpdate = this.dbContext.StockOfClients
                .First(sc => sc.ClientID == client_id
               && sc.StockID == stockId);
            entityToUpdate.Amount = newStockAmount;
        }
    }
}
