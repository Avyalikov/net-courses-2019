using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;

namespace ShopSimulator.ConsoleApp.Repositories
{
    public class SoldGoodsTableRepository : ISoldGoodsTableRepository
    {
        private readonly ShopSimulatorDbContext dbContext;

        public SoldGoodsTableRepository(ShopSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(SoldGoodsTableEntity productEntity)
        {
            this.dbContext.SoldGoods.Add(productEntity);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
