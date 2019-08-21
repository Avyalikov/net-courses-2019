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
    using Trading.Core.DTO;

    /// <summary>
    /// ITableRepository description
    /// </summary>
    public class ClientTableRepository:  CommonTableRepositoty
    {
       
        public ClientTableRepository(ExchangeContext db) : base(db)
        {
        }

        public override bool Contains(object entity)
        {
            Client client  = (Client)entity;

            return 
                
                this.db.Clients
                .Any(c => c.FirstName == client.FirstName &&
                c.LastName == client.LastName &&
                c.Phone == client.Phone);
        }

        public override bool ContainsByPK(params object[] pk)
        {
            int primaryKey = (int)pk[0];
            return this.db.Clients.Any(c => c.ClientID == primaryKey);

        }

        public override int Count()
        {
            return this.db.Clients.Count();
        }

        public override object Find(params object[] key)
        {
            return db.Clients.Find(key);
                       
        }

        public override IEnumerable<object> FindEntitiesByRequestDTO( object arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override object First()
        {
            return this.db.Clients.First();
        }

        public override object GetElementAt(int position)
        {
            return this.db.Clients.OrderBy(c=>c.ClientID).Skip(position-1).Take(1).Single();
        }

        public override object OrderById(int type)
        {
            if (type == 0)
            {
                return this.db.Clients.OrderBy(c => c.ClientID);
            }
            return this.db.Clients.OrderByDescending(c => c.ClientID);
        }

        public override object Single()
        {
            return this.db.Clients.Single();
        }

        public override IEnumerable<object> Where(params object[] arguments)
        {
            //for ClientID
            int clientId = (int)arguments[0];
            return db.Clients.Where(c => c.ClientID == clientId);
        }
    }
}
