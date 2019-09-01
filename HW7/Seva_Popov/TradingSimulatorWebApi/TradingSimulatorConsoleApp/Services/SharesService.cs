using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp.Data;
using TradingSimulatorConsoleApp.Repositories;

namespace TradingSimulatorConsoleApp.Services
{
    public class SharesService
    {
        private readonly TradingSimulatorDbContext dbContext;

        public SharesService(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SharesEntity> Get()
        {
            SharesTableRepository sharesTableRepository = new SharesTableRepository(dbContext);
            return sharesTableRepository.Get();
        }
        public SharesEntity Add(SharesEntity shares)
        {
            SharesTableRepository sharesTableRepository = new SharesTableRepository(dbContext);
            sharesTableRepository.Add(shares);
            return shares;
        }

        public SharesEntity Delete(int id)
        {
            SharesTableRepository sharesTableRepository = new SharesTableRepository(dbContext);
            return sharesTableRepository.Delete(id);
        }

        public SharesEntity Put(SharesEntity shares)
        {
            SharesTableRepository sharesTableRepository = new SharesTableRepository(dbContext);
            return sharesTableRepository.Put(shares);
        }
    }
}
