using ShopSimulator.Core.Models;

namespace ShopSimulator.Core.Repositories
{
    public interface ISaleHistoryTableRepository
    {
        void SaveChanges();

        void Add(SaleHistoryTableEntity productEntity);
    }
}
