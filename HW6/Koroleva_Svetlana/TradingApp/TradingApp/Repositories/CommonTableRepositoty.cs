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

    /// <summary>
    /// CommonTableRepositoty description
    /// </summary>
    public abstract class CommonTableRepositoty : ITableRepository
    {

        public readonly ExchangeContext db;
        public CommonTableRepositoty (ExchangeContext db)
        {
            this.db = db;
        
        }

        public void Add(object entity)
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

        public abstract object Single();
        public abstract Object OrderById(int type);

        public abstract object First();
        public abstract IEnumerable<object> FindEntitiesByRequest(params object[] arguments);
        public abstract IEnumerable<object> Where(params object[] arguments);
    }
}
