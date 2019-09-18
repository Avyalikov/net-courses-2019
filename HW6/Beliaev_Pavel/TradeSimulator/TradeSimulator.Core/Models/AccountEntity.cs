namespace TradeSimulator.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AccountEntity
    {
        public int AccountId { get; set; }

        public int ClientId { get; set; }

        public decimal Balance { get; set; }

        public string Zone { get; set; }

        public ICollection<StockOfClientEntity> Stocks { get; set; }
    }
}
