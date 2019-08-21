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
    public class OrderTableRepository : CommonTableRepositoty
    {
        public OrderTableRepository(ExchangeContext db) : base(db)
        {
        }

        public override bool Contains(object entity)
        {
            Order order = (Order)entity;

            return

                this.db.Orders
                .Any(c => c.ClientID == order.ClientID &&
                c.StockID == order.StockID &&
                c.OrderType == order.OrderType);
        }

        public override bool ContainsByPK(params object[] pk)
        {
            int primaryKey = (int)pk[0];
            return this.db.Orders.Any(c => c.OrderID == primaryKey);
        }

        public override int Count()
        {
            return this.db.Orders.Count();
        }

        public override object Find(params object[] key)
        {
            return db.Orders.Find(key);
        }

        public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
        {
            throw new NotImplementedException();
        }

        public override object First()
        {
            return this.db.Orders.First();
        }

        public override object GetElementAt(int position)
        {
            return this.db.Orders.OrderBy(c => c.OrderID).Skip(position - 1).Take(1).Single();
        }

        public override object OrderById(int type)
        {
            if (type == 0)
            {
                return this.db.Orders.OrderBy(c => c.OrderID);
            }
            return this.db.Orders.OrderByDescending(c => c.OrderID);
        }

        public override object Single()
        {
            return this.db.Orders.Single();
        }


        public override IEnumerable<object> Where(params object[] arguments)
        {
            //for ClientID
            int clientId = (int)arguments[0];
            return db.Orders.Where(c => c.ClientID == clientId);
        }
    }
}
