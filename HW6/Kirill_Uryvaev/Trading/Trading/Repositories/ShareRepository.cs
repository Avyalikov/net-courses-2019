using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.ConsoleApp.Repositories
{
    class ShareRepository : DBTable, IShareRepository
    {
        private readonly TradingDBContext dbContext;
        public ShareRepository(TradingDBContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ShareEntity share)
        {
            dbContext.Shares.Add(share);
        }

        public IEnumerable<ShareEntity> LoadAllShares()
        {
            return dbContext.Shares;
        }

        public ShareEntity LoadShareByID(int ID)
        {
            return dbContext.Shares.Where(x=>x.ShareID==ID).FirstOrDefault();
        }
    }
}
