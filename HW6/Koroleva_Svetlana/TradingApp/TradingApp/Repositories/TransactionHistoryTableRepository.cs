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
    public class TransactionHistoryTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity : TransactionHistory
    {


        public TransactionHistoryTableRepository(ExchangeContext db) : base(db)
        {
        }

       

     
        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.TransactionHistories.OrderBy(c => c.TransactionHistoryID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            TransactionHistory transactionHistory = entity;

            return

                this.db.TransactionHistories
                .Any(c => c.CustomerOrderID == transactionHistory.CustomerOrderID &&
                c.SalerOrderID == transactionHistory.SalerOrderID
               );
        }

     
    }

}