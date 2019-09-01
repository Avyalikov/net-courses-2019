using System.Collections.Generic;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
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
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }

}
