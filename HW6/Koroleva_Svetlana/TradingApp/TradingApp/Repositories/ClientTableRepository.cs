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

      

        public override IEnumerable<TEntity> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> FindEntitiesByRequestDTO(object DTOarguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> OrderById(object i)
        {
            throw new NotImplementedException();
        }

        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Clients.OrderBy(c => c.ClientID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(object entity)
        {
            Client client = (Client)entity;

            return

                this.db.Clients
                .Any(c => c.FirstName == client.FirstName&&
                c.LastName== client.LastName&&
                c.Phone==client.Phone
                );
        }


        /*
                public override bool ContainsByPK(params object[] pk)
                {
                    int primaryKey = (int)pk[0];
                    return this.db.Clients.Any(c => c.ClientID == primaryKey);

                }


                public override IEnumerable<object> FindEntitiesByRequestDTO( object arguments)
                {
                    throw new NotImplementedException();
                }

                public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
                {
                    throw new NotImplementedException();
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



                public override IEnumerable<object> Where(params object[] arguments)
                {
                    //for ClientID
                    int clientId = (int)arguments[0];
                    return db.Clients.Where(c => c.ClientID == clientId);
                }*/
    }
}
