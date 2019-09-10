namespace Traiding.WebAPIConsole.Models.Repositories
{
    using System.Collections.Generic;
    using System.Linq;    
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    class BalanceTableRepository : IBalanceTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public BalanceTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(BalanceEntity entity)
        {
            this.dBContext.Balances.Add(entity);
        }

        public void ChangeAmount(int entityId, decimal newAmount)
        {
            var balance = this.dBContext.Balances.First(b => b.Id == entityId); // it will fall here if we can't find
            balance.Amount = newAmount;
        }

        public bool Contains(BalanceEntity entity)
        {
            return this.dBContext.Balances.Any(b => b.Client.Id == entity.Client.Id);
        }

        public bool ContainsById(int entityId)
        {
            return this.dBContext.Balances.Any(b => b.Id == entityId);
        }

        public BalanceEntity Get(int entityId)
        {
            return this.dBContext.Balances.First(b => b.Id == entityId); // it will fall here if we can't find
        }

        public IEnumerable<BalanceEntity> GetNegativeBalances()
        {
            return this.dBContext.Balances.Where(b => b.Amount < 0);
        }

        public IEnumerable<BalanceEntity> GetZeroBalances()
        {
            return this.dBContext.Balances.Where(b => b.Amount == 0);
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public BalanceEntity SearchBalanceByClientId(int clientId)
        {
            return this.dBContext.Balances.FirstOrDefault(b => b.Client.Id == clientId); // it will not fall here if we can't find
        }
    }
}
