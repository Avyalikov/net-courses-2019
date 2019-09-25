using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Repositories;

namespace WikiURLCollector.ConsoleApp.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly WikiUrlDbContext dbContext;
        public BaseRepository(WikiUrlDbContext dbContext)
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
