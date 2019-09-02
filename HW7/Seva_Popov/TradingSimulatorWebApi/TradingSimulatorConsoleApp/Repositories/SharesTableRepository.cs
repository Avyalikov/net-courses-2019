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
            try
            {
                return dbContext.Shares.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SharesEntity Add(SharesEntity shares)
        {
            try
            {
                dbContext.Shares.Add(shares);
                dbContext.SaveChanges();
                return shares;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SharesEntity Delete(int id)
        {
            try
            {
                SharesEntity shares = dbContext.Shares.FirstOrDefault(x => x.Id == id);
                dbContext.Shares.Remove(shares);
                dbContext.SaveChanges();
                return shares;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            } 
        }

        public SharesEntity Put(SharesEntity shares)
        {
            try
            {
                dbContext.Update(shares);
                dbContext.SaveChanges();
                return shares;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
