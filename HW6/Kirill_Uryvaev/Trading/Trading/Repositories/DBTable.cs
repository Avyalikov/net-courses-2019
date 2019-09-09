using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;

namespace Trading.ConsoleApp.Repositories
{
    abstract class DBTable: IDBTable
    {
        private readonly TradingDBContext dbContext;
        public DBTable(TradingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        public void WithTransaction(Action function)
        {
            using (var dbContextTransaction = this.dbContext.Database.BeginTransaction())
            {
                this.dbContext.SaveChanges();

                try
                {
                    function();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    throw new Exception();
                }
            }
        }
    }
}
