// <copyright file="ClientStockTableRepository.cs" company="SKorol">
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
    /// ClientStockTableRepository description
    /// </summary>
    public class ClientStockTableRepository : ITableRepository, ILinkedTableRepository
    {
        private readonly ExchangeContext db;
        public ClientStockTableRepository(ExchangeContext db)
        {
            this.db = db;
        }

        public void Add(object entity)
        {
            this.db.ClientStocks.Add((ClientStock)entity);
        }

        public bool ContainsByCompositeID(int entityId1, int entityId2)
        {
            return this.db.ClientStocks.Any(cs => cs.ClientID == entityId1 && cs.StockID == entityId2);
        }

        public IEnumerable<object> FindEntitiesByRequest(string arguments)
        {
            throw new NotImplementedException();
        }

        public object GetEntityByCompositeID(int entityId1, int entityId2)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
