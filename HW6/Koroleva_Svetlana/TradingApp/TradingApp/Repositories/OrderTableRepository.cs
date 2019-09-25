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
    /// orderTableRepository description
    /// </summary>
    public class OrderTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity : Order
    {
        public OrderTableRepository(ExchangeContext db) : base(db)
        {
        }


        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Orders.OrderBy(c => c.OrderID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(TEntity entity)
        {
            Order order = entity;

            return

                this.db.Orders
                .Any(c => c.ClientID == order.ClientID &&
                c.StockID == order.StockID&&
                c.OrderType==order.OrderType
                );
        }


    }
}
