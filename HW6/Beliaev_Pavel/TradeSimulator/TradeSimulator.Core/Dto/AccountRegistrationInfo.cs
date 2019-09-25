namespace TradeSimulator.Core.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public class AccountRegistrationInfo
    {
        public int ClientId { get; set; }

        public decimal Balance { get; set; }

        public string Zone { get; set; }

        public ICollection<StockOfClientEntity> Stocks { get; set; }
    }
}
