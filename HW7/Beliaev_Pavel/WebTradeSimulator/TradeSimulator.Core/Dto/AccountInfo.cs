namespace TradeSimulator.Core.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public class AccountInfo
    {
        public int AccountId { get; set; }

        public int ClientId { get; set; }

        public decimal Balance { get; set; }

        public string Zone { get; set; }
    }
}
