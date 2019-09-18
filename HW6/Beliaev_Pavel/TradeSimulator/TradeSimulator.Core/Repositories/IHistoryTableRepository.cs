namespace TradeSimulator.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public interface IHistoryTableRepository
    {
        void Add(HistoryEntity entity);
        void SaveChanges();

        ICollection<HistoryEntity> GetHistory();
    }
}
