using ShopSimulator.Core.Models;

namespace ShopSimulator.Core.Repositories
{
    public interface ISoldGoodsTableRepository
    {
        void SaveChanges();
        void Add(SoldGoodsTableEntity productEntity);
    }
}
