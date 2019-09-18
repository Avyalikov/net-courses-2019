namespace TradeSimulator.Core.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StockOfClientInfo
    {
        public int ClientsAccountId { get; set; }

        public int StockId { get; set; }

        public string TypeOfStocks { get; set; }

        public int quantityOfStocks { get; set; }
    }
}
