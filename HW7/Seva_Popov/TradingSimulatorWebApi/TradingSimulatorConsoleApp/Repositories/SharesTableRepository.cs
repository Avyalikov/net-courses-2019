using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulatorConsoleApp.Data;

namespace TradingSimulatorConsoleApp.Repositories
{
    public class SharesTableRepository : ISharesTableRepository
    {
        private readonly TradingSimulatorDbContext dbContext;

        public SharesTableRepository(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SharesEntity> Get()
        {
            return dbContext.Shares.ToList();
        }

        public SharesEntity Add(SharesEntity shares)
        {
            dbContext.Shares.Add(shares);
            dbContext.SaveChanges();
            return shares;
        }

        public SharesEntity Delete(int id)
        {
            SharesEntity shares = dbContext.Shares.FirstOrDefault(x => x.Id == id);
            dbContext.Shares.Remove(shares);
            dbContext.SaveChanges();
            return shares;
        }

        public SharesEntity Put(SharesEntity shares)
        {
            dbContext.Update(shares);
            dbContext.SaveChanges();
            return shares;
        }
    }
}
