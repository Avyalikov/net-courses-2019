namespace TradeSimulator.Client.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StockOfClientInfo
    {
        public int Id { get; set; }

        public string TypeOfStocks { get; set; }

        public int quantityOfStocks { get; set; }

        public decimal PriceOfStock { get; set; }

        public int ClientId { get; set; }

        public int AccountId { get; set; }
    }
}
