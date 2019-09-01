using System.Collections.Generic;

namespace TradingSimulator.Core.Interfaces
{
    public interface IHistoryService
    {
        IEnumerable<Models.HistoryEntity> GetHistoryById(int traderId);
        IEnumerable<Models.HistoryEntity> GetHistoryById(int traderId, int maxCount);
    }
}