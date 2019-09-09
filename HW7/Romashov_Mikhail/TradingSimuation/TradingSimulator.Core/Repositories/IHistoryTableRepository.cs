using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IHistoryTableRepository
    {
        void Add(HistoryEntity historyEntity);
        System.Collections.Generic.IEnumerable<HistoryEntity> GetHistoryById(int traderId);
        void SaveChanges();
    }
}
