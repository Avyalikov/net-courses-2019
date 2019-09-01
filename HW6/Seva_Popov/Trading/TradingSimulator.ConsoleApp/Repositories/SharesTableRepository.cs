using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
{
    public class SharesTableRepository : ISharesTableRepository
    {
        private readonly TradingSimulatorDbContext dbContext;

        public SharesTableRepository(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(SharesEntity sharesEntity)
        {
            this.dbContext.Shares.Add(sharesEntity);
        }

        public bool Contains(SharesEntity sharesEntity)
        {
            return this.dbContext.Shares.Any(f =>
           f.Name == sharesEntity.Name
           && f.Price == sharesEntity.Price);
        }

        public bool ContainsById(int entityId)
        {
            return this.dbContext.Shares.Any(f =>
        f.Id == entityId);
        }

        public SharesEntity Get(int sharesId)
        {
            return this.dbContext.Shares.First(f => f.Id == sharesId);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
