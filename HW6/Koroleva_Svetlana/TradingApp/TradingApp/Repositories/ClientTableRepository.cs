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
    public class ClientTableRepository<TEntity>:  CommonTableRepositoty<TEntity> where TEntity: Client
    {
       
        public ClientTableRepository(ExchangeContext db) : base(db)
        {
            
        }

      


        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Clients.OrderBy(c => c.ClientID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            Client client = entity;

            return

                this.db.Clients
                .Any(c => c.FirstName == client.FirstName&&
                c.LastName== client.LastName&&
                c.Phone==client.Phone
                );
        }

    }
}
