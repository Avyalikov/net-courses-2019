// <copyright file="ITableRepository.cs" company="SKorol">
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
    /// ITableRepository description
    /// </summary>
    public class ClientTableRepository: ITableRepository, IOnePKTableRepository
    {
        private readonly ExchangeContext db;
        public ClientTableRepository(ExchangeContext db)
        {
            this.db = db;
        }

        public void Add(object entity)
        {
            this.db.Clients.Add((Client)entity);
        }

        public bool ContainsByID(int entityId)
        {
            return this.db.Clients.Any(c=>c.ClientID==entityId);
        }

        public IEnumerable<object> FindEntitiesByRequest(string arguments)
        {
            throw new NotImplementedException();
        }

        public object GetEntityByID(int entityId)
        {
            return this.db.Clients.Where(c=>c.ClientID==entityId).Single();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
