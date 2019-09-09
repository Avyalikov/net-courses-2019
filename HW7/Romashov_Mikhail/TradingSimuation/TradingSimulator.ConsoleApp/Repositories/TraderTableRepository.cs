using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
{
    class TraderTableRepository : ITraderTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public TraderTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TraderEntityDB entity)
        {
            this.dbContext.Traders.Add(entity);
        }

        public void AdditionBalance(int traderID, decimal amount)
        {
            var ItemToUpdate = this.dbContext.Traders.First(t => t.Id == traderID);
            ItemToUpdate.Balance += amount;
        }

        public bool Contains(TraderEntityDB entityToAdd)
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

        public bool ContainsByName(string traderName)
        {
            return this.dbContext.Traders.Any(t => t.Name == traderName);
        }

        public int CountIds()
        {
            throw new System.NotImplementedException();
        }

        public TraderEntityDB GetById(int traderID)
        {
            var item = this.dbContext.Traders.First(t => t.Id == traderID);
            return item;
        }

        public TraderEntityDB GetByName(string traderName)
        {
            var item = this.dbContext.Traders.First(t => t.Name == traderName);
            return item;
        }

        public IEnumerable<TraderEntityDB> GetListOfTraders()
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetListTradersId()
        {
            List<int> listItems = new List<int>();
            foreach (var item in this.dbContext.Traders)
            {
                listItems.Add(item.Id);
            }

            return listItems;
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
