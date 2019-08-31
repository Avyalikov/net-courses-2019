namespace SharedContext.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using SharedContext.DAL;
    using Trading.Core.DTO;

    /// <summary>
    /// orderTableRepository description
    /// </summary>
    public class OrderRepository : CommonRepositoty<Order>, IOrderRepository
    {
        public OrderRepository(ExchangeContext db) : base(db)
        {
        }



    }
}
