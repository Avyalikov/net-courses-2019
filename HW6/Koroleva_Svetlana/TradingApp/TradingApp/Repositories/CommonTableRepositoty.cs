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

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public bool Contains(TEntity entity)
        {
            return dbSet.Contains(entity);
        }

        public abstract bool ContainsDTO(Object entity);
       

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



        public TEntity First()
        {
            return dbSet.First();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public abstract TEntity GetElementAt(int position);
       /* {
            return this.dbSet.AsNoTracking().OrderBy(c=>((TEntity)c)).Equals()..Skip(position - 1).Take(1).Single();
        }*/

        public abstract IEnumerable<TEntity> OrderById(object i);
        public abstract IEnumerable<TEntity> FindEntitiesByRequest(params object[] arguments);
        public abstract IEnumerable<TEntity> FindEntitiesByRequestDTO(object DTOarguments);




        public void SaveChanges()
        {
            db.SaveChanges();
        }

        /*  public object Single()
          {
              return dbSet.Single();
          }
          */
        /*  public IEnumerable<TEntity> Where(params object[] arguments)
          {
              throw new NotImplementedException();
          }*/

        /* public void Add(object entity)
         {
             Type type =entity.GetType();
             db.Set(type).Add(entity);

         }

         public void AddRange(IEnumerable<object> entities)
         {
             Type type = entities.GetType();
             db.Set(type).AddRange(entities);
         }

         public void SaveChanges()
         {
             db.SaveChanges();
         }
         public abstract bool Contains(object entity);


         public abstract object Find(params object[] key);


         public abstract IEnumerable<object> FindEntitiesByRequestDTO( object arguments);


         public abstract bool ContainsByPK(params object[] PK);

         public abstract int Count();
         public abstract object GetElementAt(int position);

     /*    public  object Single(IEnumerable<Object> collection) {
             return collection.Single();
         }*/

        /* public abstract Object OrderById(int type);

         public  object First(IEnumerable<Object> collection) {
             return collection.First();
         }
         public abstract IEnumerable<object> FindEntitiesByRequest(params object[] arguments);
         public abstract IEnumerable<object> Where(params object[] arguments);
     }*/
    }
}
