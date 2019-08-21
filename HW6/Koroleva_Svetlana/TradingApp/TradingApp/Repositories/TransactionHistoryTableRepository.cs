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
    public class TransactionHistoryTableRepository:  CommonTableRepositoty
    {
       
        public TransactionHistoryTableRepository(ExchangeContext db) : base(db)
        {
        }

        public override bool Contains(object entity)
        {
            TransactionHistory transactionHistory  = (TransactionHistory)entity;

            return 
                
                this.db.TransactionHistories
                .Any(c => c.CustomerOrderID==transactionHistory.CustomerOrderID&&
                c.SalerOrderID==transactionHistory.SalerOrderID
               );
        }

        public override bool ContainsByPK(params object[] pk)
        {
            int primaryKey = (int)pk[0];
            return this.db.TransactionHistories.Any(c => c.TransactionHistoryID == primaryKey);

        }

        public override int Count()
        {
            return this.db.TransactionHistories.Count();
        }

        public override object Find(params object[] key)
        {
            return db.TransactionHistories.Find(key);
                       
        }

        public override object GetElementAt(int position)
        {
            return this.db.TransactionHistories.OrderBy(c=>c.TransactionHistoryID).Skip(position-1).Take(1).Single();
        }

      /*  public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
        {
            TransactionArguments args = (TransactionArguments)arguments;

            var Transactionhistory=this.db.TransactionHistories
                .Where(ph => ph.DateTimeBegin <= args.DateTimeLookUp && ph.DateTimeEnd >= args.DateTimeLookUp)
                .Select(p => p).Where(o => o.StockID ==args.StockId);

            return Transactionhistory;
          
        }*/

        

        public override object Single()
        {
            return this.db.TransactionHistories.Single();
        }

         public override object OrderById(int type)
        {
            if (type == 0)
            {
                return this.db.TransactionHistories.OrderBy(c => c.TransactionHistoryID);
            }
            return this.db.TransactionHistories.OrderByDescending(c => c.TransactionHistoryID);
        }

        public override object First()
        {
            return this.db.TransactionHistories.First();
        }

        public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Where(params object[] arguments)
        {
            //for OrderID
            int orderId = (int)arguments[0];
            int customerOrderId = (int)arguments[1];
            return db.TransactionHistories.Where(c => c.SalerOrderID == orderId&&c.CustomerOrderID==c.CustomerOrderID);
        }
    }

    /* public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
     {
         //for StockId
         object arg=arguments[0];
         var Transactionhistory = this.db.TransactionHistories
            .Select(p => p).Where(o => o.StockID == (int)arg);

         return Transactionhistory;

     }*/
}

