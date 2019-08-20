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
    public class ClientTableRepository: ITableRepository
    {
        private readonly ExchangeContext db;
        public TableRepository(ExchangeContext db)
        {
            this.db = db;
        }
       

        public void ITableRepository.Add(Client entity)
        {
            this.db.Clients.Add(entity);
        }

       public bool ITableRepository.ContainsByID(int entityId)
        {
            return this.db.Clients.Any(c=>c.ClientID==entityId);
        }

        bool ITableRepository.ContainsByCompositeID(int entityId1, int entityId2)
        {
            throw new NotImplementedException();
        }

        object ITableRepository.GetEntityByID(int entityId)
        {
            throw new NotImplementedException();
        }

        object ITableRepository.GetEntityByCompositeID(int entityId1, int entityId2)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> ITableRepository.FindEntitiesByRequest(string arguments)
        {
            throw new NotImplementedException();
        }

        void ITableRepository.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
