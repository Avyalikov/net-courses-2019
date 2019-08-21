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
    public class PriceHistoryTableRepository:  CommonTableRepositoty
    {
       
        public PriceHistoryTableRepository(ExchangeContext db) : base(db)
        {
        }

        public override bool Contains(object entity)
        {
            PriceHistory priceHistory  = (PriceHistory)entity;

            return 
                
                this.db.PriceHistories
                .Any(c => c.DateTimeBegin==priceHistory.DateTimeBegin&&
                c.DateTimeEnd==priceHistory.DateTimeEnd&&
                c.StockID==c.StockID);
        }

        public override bool ContainsByPK(params object[] pk)
        {
            int primaryKey = (int)pk[0];
            return this.db.PriceHistories.Any(c => c.PriceHistoryID == primaryKey);

        }

        public override int Count()
        {
            return this.db.PriceHistories.Count();
        }

        public override object Find(params object[] key)
        {
            return db.PriceHistories.Find(key);
                       
        }

        public override object GetElementAt(int position)
        {
            return this.db.PriceHistories.OrderBy(c=>c.PriceHistoryID).Skip(position-1).Take(1).Single();
        }

        public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
        {
            PriceArguments args = (PriceArguments)arguments;

            var pricehistory=this.db.PriceHistories
                .Where(ph => ph.DateTimeBegin <= args.DateTimeLookUp && ph.DateTimeEnd >= args.DateTimeLookUp)
                .Select(p => p).Where(o => o.StockID ==args.StockId);

            return pricehistory;
          
        }

        

        public override object Single()
        {
            return this.db.PriceHistories.Single();
        }

         public override object OrderById(int type)
        {
            if (type == 0)
            {
                return this.db.PriceHistories.OrderBy(c => c.PriceHistoryID);
            }
            return this.db.PriceHistories.OrderByDescending(c => c.PriceHistoryID);
        }

        public override object First()
        {
            return this.db.PriceHistories.First();
        }

        public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
        {
            //for StockId
            object arg=arguments[0];
            var pricehistory = this.db.PriceHistories
               .Select(p => p).Where(o => o.StockID == (int)arg);

            return pricehistory;

        }
    }
}
