// <copyright file="StockTableRepository.cs" company="SKorol">
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
    /// StockTableRepository description
    /// </summary>
    public class StockTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity:Stock
    {
        public StockTableRepository(ExchangeContext db) : base(db)
        {
        }


        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Stocks.OrderBy(c => c.StockID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            Stock stock = entity;

            return

               this.db.Stocks
                .Any(c => c.StockPrefix == stock.StockPrefix &&
                c.StockType == stock.StockType &&
                c.IssuerID == stock.IssuerID);
        }

       
    }
}
