namespace WikipediaParser
{
    using System;
    using WikipediaParser.Repositories;

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly WikiParsingDbContext dbContext = new WikiParsingDbContext();
        private LinksTableRepository repository;

        public ILinksTableRepository LinksTableRepository
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
