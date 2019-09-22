// <copyright file="CommonTableRepositoty.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace SharedContext.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using SharedContext.DAL;
    using System.Data.Entity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CommonRepositoty description
    /// </summary>
    public abstract class CommonRepositoty<TEntity> : ICommonRepository<TEntity> where TEntity : class
    {
        public  ExchangeContext db;
        DbSet<TEntity> dbSet;        

        public CommonRepositoty(ExchangeContext db)
        {
            this.db = db;
           this.dbSet = db.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).AsQueryable();
        }

        public void Add(TEntity entity)
        {
           this.dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Attach(entity);
            this.db.Entry(entity).State = EntityState.Modified; 
        }

        public void Delete(TEntity entity)
        {
           this.dbSet.Attach(entity); 
           this.dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
           return this.dbSet.AsNoTracking();
        }

      
             
    }
}
