namespace MultithreadLinksApp.Repos
{
    using MultithreadLinksApp.Core.Repos;
    using System;

    class Repository : IRepository
    {
        private readonly AppDBContext appDBContext;
        public Repository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public void SaveChanges()
        {
            appDBContext.SaveChanges();
        }

        public void WithTransaction(Action action)
        {
            using (var dBContext = this.appDBContext.Database.BeginTransaction())
            {
                try
                {
                    action();
                    dBContext.Commit();
                    this.appDBContext.SaveChanges();
                }

                catch (Exception)
                {
                    dBContext.Rollback();
                    throw new Exception();
                }
            }
        }
    }
}
