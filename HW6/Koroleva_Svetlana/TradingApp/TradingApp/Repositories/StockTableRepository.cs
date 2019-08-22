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
            return (TEntity)this.db.Stocks.OrderBy(c => c.StockID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(object entity)
        {
            Stock stock = (Stock)entity;

            return

               this.db.Stocks
                .Any(c => c.StockPrefix == stock.StockPrefix &&
                c.StockType == stock.StockType &&
                c.IssuerID == stock.IssuerID);
        }

        /*public override bool Contains(object entity)
        {
            Stock stock = (Stock)entity;

            return

                this.db.Stocks
                .Any(c => c.StockPrefix == stock.StockPrefix &&
                c.StockType == stock.StockType&&
                c.IssuerID==stock.IssuerID);
        }

        public override bool ContainsByPK(params object[] pk)
        {
            int primaryKey = (int)pk[0];
            return this.db.Stocks.Any(c => c.StockID == primaryKey);
        }

        public override int Count()
        {
            return this.db.Stocks.Count();
        }

        public override object Find(params object[] key)
        {
            return db.Stocks.Find(key);
        }

        public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
        {
            throw new NotImplementedException();
        }

      

        public override object GetElementAt(int position)
        {
            return this.db.Stocks.OrderBy(c => c.StockID).Skip(position - 1).Take(1).Single();
        }

        public override object OrderById(int type)
        {
            if (type == 0)
            {
                return this.db.Stocks.OrderBy(c => c.StockID);
            }
            return this.db.Stocks.OrderByDescending(c => c.StockID);
        }

     
        public override IEnumerable<object> Where(params object[] arguments)
        {
            //for StockID
            int stocktId = (int)arguments[0];
            return db.Stocks.Where(c => c.StockID == stocktId);
        }*/
    }
}
