using MultithreadApp.Console.DbInit;
using MultithreadApp.Console.Repository;
using MultithreadApp.Core.Repository;
using MultithreadApp.Core.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Console.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MultiAppDbContext db = new MultiAppDbContext();

        private IUrlRepository UrlRepository;
        
        public IUrlRepository Urls
        {
            get
            {
                if (UrlRepository == null)
                {
                    UrlRepository = new UrlRepository(db);
                }
                return UrlRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
