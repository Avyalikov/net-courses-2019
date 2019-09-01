using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace WebApiServer.Repositories
{
    class HistoryTableRepository : IHistoryTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public HistoryTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(HistoryEntity historyEntity)
        {
            this.dbContext.TradeHistory.Add(historyEntity);
        }

        public IEnumerable<HistoryEntity> GetHistoryById(int traderId)
        {
            return this.dbContext.TradeHistory.Where(t => t.SellerID == traderId || t.CustomerID == traderId);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }

}
