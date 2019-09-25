namespace TradeSimulator.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HistoryEntity
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int QuantityOfStocks { get; set; }
        public string TypeOfStock { get; set; }
        public decimal FullPrice { get; set; }
    }
}
