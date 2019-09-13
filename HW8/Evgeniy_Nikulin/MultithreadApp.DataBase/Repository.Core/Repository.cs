namespace MultithreadApp.DataBase.Repository.Core
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SiteDbContext db;
        public Repository(SiteDbContext db) =>
            this.db = db;

        public TEntity Get(int id) =>
            this.db.Set<TEntity>()
            .Find(id);

        public IEnumerable<TEntity> GetAll() =>
            this.db.Set<TEntity>()
            .ToList();

        public void Add(TEntity entity) =>
            this.db.Set<TEntity>()
            .Add(entity);

        public void Dispose() =>
            this.db.Dispose();
    }
}