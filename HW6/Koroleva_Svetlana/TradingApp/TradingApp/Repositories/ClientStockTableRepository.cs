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

        public override bool ContainsDTO(object entity)
        {
            ClientStock clientStock = (ClientStock)entity;

            return

                this.db.ClientStocks
                .Any(c => c.ClientID == clientStock.ClientID &&
                c.StockID == clientStock.StockID);
        }

        public override IEnumerable<TEntity> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> FindEntitiesByRequestDTO(object DTOarguments)
        {
            throw new NotImplementedException();
        }

        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.ClientStocks.OrderBy(c => c.ClientID).Skip(position - 1).Take(1).Single();
          
        }

        public override IEnumerable<TEntity> OrderById(object i)
        {
            throw new NotImplementedException();
        }
        /*public override bool Contains(object entity)
{
   ClientStock ClientStock = (ClientStock)entity;

   return

       this.db.ClientStocks
       .Any(c => c.ClientID == ClientStock.ClientID &&
       c.StockID== ClientStock.StockID );
}

public override bool ContainsByPK(params object[] pk)
{
   int primaryKeyPart1 = (int)pk[0];
   int primaryKeyPart2 = (int)pk[1];
   return this.db.ClientStocks.Any(c => c.ClientID == primaryKeyPart1&& c.StockID==primaryKeyPart2);
}

public override int Count()
{
   return this.db.ClientStocks.Count();
}

public override object Find(params object[] key)
{
   return db.ClientStocks.Find(key);
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
   return this.db.ClientStocks.OrderBy(c => c.ClientID).ThenBy(c=>c.StockID).Skip(position - 1).Take(1).Single();
}

public override object OrderById(int type)
{
   if (type == 0)
   {
       return this.db.ClientStocks.OrderBy(c => c.ClientID).ThenBy(c=>c.StockID);
   }
   return this.db.ClientStocks.OrderByDescending(c => c.ClientID).ThenByDescending(c => c.StockID);
}


public override IEnumerable<object> Where(params object[] arguments)
{
   //for ClientID
   int clientId = (int)arguments[0];
   return db.ClientStocks.Where(c=>c.ClientID==clientId);
}*/
    }
}
