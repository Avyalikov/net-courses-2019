using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;
using System.Linq;

namespace ShopSimulator.ConsoleApp.Repositories
{
    public class SupplierTableRepository : ISupplierTableRepository
    {
        private readonly ShopSimulatorDbContext dbContext;

        public SupplierTableRepository(ShopSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(SupplierEntity entity)
        {
            this.dbContext.Suppliers.Add(entity);
        }

        public bool Contains(SupplierEntity entity)
        {
            return this.dbContext.Suppliers.Any(f => 
            f.Name == entity.Name 
            && f.Surname == entity.Surname 
            && f.PhoneNumber == entity.PhoneNumber);
        }

        public bool ContainsById(int entityId)
        {
            return this.dbContext.Suppliers.Any(f =>
           f.Id == entityId);
        }

        public SupplierEntity Get(int supplierId)
        {
            return this.dbContext.Suppliers.First(f => f.Id == supplierId);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public T WithTransaction<T>(Func<T> function)
        {
            using (var dbContextTransaction = this.dbContext.Database.BeginTransaction())
            {
                this.dbContext.SaveChanges();

                try
                {
                    var result = function();

                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    throw new Exception();
                }
            }
        }

        public void WithTransaction(Action action)
        {
            using (var dbContextTransaction = this.dbContext.Database.BeginTransaction())
            {
                this.dbContext.SaveChanges();

                try
                {
                    action();

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
