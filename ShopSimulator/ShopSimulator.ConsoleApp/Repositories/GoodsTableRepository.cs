using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.ConsoleApp.Repositories
{
    public class GoodsTableRepository : IGoodsTableRepository
    {
        private readonly ShopSimulatorDbContext dbContext;

        public GoodsTableRepository(ShopSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ProductEntity> FindProductsByRequest(BuyArguments buyArguments)
        {
            var retVal = new List<ProductEntity>();

            foreach (var item in buyArguments.ItemsToBuy)
            {
                var itemInDb = this.dbContext.Goods.First(f => f.Name == item.Name);
                retVal.Add(itemInDb);
            }

            return retVal;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void SubtractProduct(int productId, int subtractAmount)
        {
            var itemToUpdate = this.dbContext.Goods.First(f => f.Id == productId);
            itemToUpdate.Count -= subtractAmount;
        }
    }
}
