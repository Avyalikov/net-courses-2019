using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IHistoryTableRepository
    {
        void Add(HistoryEntity historyEntity);
        void SaveChanges();
    }
}
