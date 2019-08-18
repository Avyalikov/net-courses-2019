using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;

namespace ShopSimulator.ConsoleApp.Repositories
{
    public class SaleHistoryTableRepository : ISaleHistoryTableRepository
    {
        private readonly ShopSimulatorDbContext dbContext;

        public SaleHistoryTableRepository(ShopSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(SaleHistoryTableEntity productEntity)
        {
            this.dbContext.SaleHistory.Add(productEntity);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
