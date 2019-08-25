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
   public class ClientStockTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity:ClientStock
    {
        
        public ClientStockTableRepository(ExchangeContext db):base(db)
        {
            
        }

        public override bool ContainsDTO(TEntity entity)
        {
            ClientStock clientStock = entity;

            return

                this.db.ClientStocks
                .Any(c => c.ClientID == clientStock.ClientID &&
                c.StockID == clientStock.StockID);
        }

      

        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.ClientStocks.OrderBy(c => c.ClientID).Skip(position - 1).Take(1).Single();
          
        }

  
    }
}
