using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace stockSimulator.Modulation.Repositories
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
            if(entry != null)
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

        public IQueryable<StockOfClientsEntity> GetStocksOfClient(int clientId)
        {
            var retListOfStocksOfClient = this.dbContext.StockOfClients
                .Where(sc => sc.ClientID == clientId)
                .Include(sc => sc.Stock)
                .Include(sc => sc.Client);

            return retListOfStocksOfClient;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public string Update(int entityId, StockOfClientsEntity newEntity)
        {
            var stockOfCloentToUpdate = this.dbContext.StockOfClients.FirstOrDefault(c => c.ID == entityId);
            if (stockOfCloentToUpdate != null)
            {
                stockOfCloentToUpdate.Amount = newEntity.Amount;
                SaveChanges();
                return "Stock of Client data was updated.";
            }
            return "Stock of Client data wasn't found.";
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