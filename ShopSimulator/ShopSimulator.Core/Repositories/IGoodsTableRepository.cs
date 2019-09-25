using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using System.Collections.Generic;

namespace ShopSimulator.Core.Repositories
{
    public interface IGoodsTableRepository
    {
        IEnumerable<ProductEntity> FindProductsByRequest(BuyArguments buyArguments);

        void SubtractProduct(int productId, int subtractAmount);

        void SaveChanges();
    }
}
