using System.Linq;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Repositories
{
    class TraderTableRepository : ITraderTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public TraderTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TraderEntity entity)
        {
            this.dbContext.Traders.Add(entity);
        }

        public void AdditionBalance(int traderID, decimal amount)
        {
            var ItemToUpdate = this.dbContext.Traders.First(t => t.Id == traderID);
            ItemToUpdate.Balance += amount;
        }

        public bool Contains(TraderEntity entityToAdd)
        {
           return this.dbContext.Traders.Any(t =>
           t.Name == entityToAdd.Name
           && t.Surname == entityToAdd.Surname
           && t.PhoneNumber == entityToAdd.PhoneNumber);
        }

        public bool ContainsById(int entityId)
        {
            return this.dbContext.Traders.Any(t => t.Id == entityId);
        }

        public TraderEntity Get(int traderID)
        {
            var ItemToUpdate = this.dbContext.Traders.First(t => t.Id == traderID);
            return ItemToUpdate;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void SubstractBalance(int traderID, decimal amount)
        {
            var ItemToUpdate = this.dbContext.Traders.First(w => w.Id == traderID);
            ItemToUpdate.Balance -= amount;
        }
    }
}
