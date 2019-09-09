using System;
using WikipediaParser.Repositories;

namespace WikipediaParser
{
    public class UnitOfWork : IDisposable
    {
        private readonly WikiParsingDbContext dbContext = new WikiParsingDbContext();
        private LinksTableRepository repository;

        public LinksTableRepository LinksTableRepository
        {
            get
            {
                if (repository is null)
                    repository = new LinksTableRepository(dbContext);
                return repository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
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
