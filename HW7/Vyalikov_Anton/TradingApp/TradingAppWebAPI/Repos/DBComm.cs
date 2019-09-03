namespace TradingAppWebAPI
{
    using TradingApp.Core.Repos;
    using System;

    class DBComm : IDBComm
    {
        private readonly DBContext dBContext;

        public DBComm(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void SaveChanges()
        {
            dBContext.SaveChanges();
        }

        public void WithTransaction(Action func)
        {
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                dBContext.SaveChanges();

                try
                {
                    func();
                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception();
                }
            }
        }
    }
}
