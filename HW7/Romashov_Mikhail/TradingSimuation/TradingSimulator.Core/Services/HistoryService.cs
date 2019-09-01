using TradingSimulator.Core.Repositories;
using System.Collections.Generic;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Interfaces;

namespace TradingSimulator.Core.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryTableRepository historyTableRepository;
        public HistoryService(IHistoryTableRepository historyTableRepository)
        {
            this.historyTableRepository = historyTableRepository;
        }

        public IEnumerable<HistoryEntity> GetHistoryById(int traderId)
        {
            return historyTableRepository.GetHistoryById(traderId);
        }

        public IEnumerable<HistoryEntity> GetHistoryById(int traderId, int maxCount)
        {
            List<HistoryEntity> retVal = new List<HistoryEntity>();
            var history = historyTableRepository.GetHistoryById(traderId);
            foreach (var item in history)
            {
               
                if (retVal.Count == maxCount)
                    break;
                retVal.Add(item);
            }
            return retVal;
        }
    }
}

