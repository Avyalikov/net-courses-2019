// <copyright file="IssuerTableRepository.cs" company="SKorol">
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
    /// IssuerTableRepository description
    /// </summary>
    public class IssuerTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity:Issuer
    {
        public IssuerTableRepository(ExchangeContext db) : base(db)
        {
        }
        
      
        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Issuers.OrderBy(c => c.IssuerID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            Issuer issuer = entity;

            return

                this.db.Issuers
                .Any(c => c.CompanyName == issuer.CompanyName &&
                c.Address == issuer.Address 
                );
        }
       
    }
}
