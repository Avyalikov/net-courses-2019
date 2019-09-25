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
    public class PriceHistoryTableRepository<TEntity>:  CommonTableRepositoty<TEntity> where TEntity:PriceHistory
    {
       
        public PriceHistoryTableRepository(ExchangeContext db) : base(db)
        {
        }

        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.PriceHistories.OrderBy(c => c.PriceHistoryID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            PriceHistory priceHistory  = entity;

             return 

                 this.db.PriceHistories
                 .Any(c => c.DateTimeBegin==priceHistory.DateTimeBegin&&
                 c.DateTimeEnd==priceHistory.DateTimeEnd&&
                 c.StockID==c.StockID);
        }

      
    }
}
