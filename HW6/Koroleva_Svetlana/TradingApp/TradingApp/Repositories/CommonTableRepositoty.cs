// <copyright file="CommonTableRepositoty.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using TradingApp.DAL;
    using System.Data.Entity;

    /// <summary>
    /// CommonTableRepositoty description
    /// </summary>
    public abstract class CommonTableRepositoty<TEntity> : ITableRepository<TEntity> where TEntity : class
    {

        public readonly ExchangeContext db;
        DbSet<TEntity> dbSet;
        public CommonTableRepositoty(ExchangeContext db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();

        }


        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

   
        public bool Contains(TEntity entity)
        {
            return dbSet.Contains(entity);
        }

        public abstract bool ContainsDTO(TEntity entity);
       

        public bool ContainsByPK(params object[] pk)
        {
            if (FindByPK(pk) != null)
            {
                return true;
            }
            return false;
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public TEntity FindByPK(params object[] key)
        {

            return dbSet.Find(key);
        }


        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public abstract TEntity GetElementAt(int position);
      
        

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
