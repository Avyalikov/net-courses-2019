namespace TradeSimulator.Core.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransactionInfo
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int QuantityOfStocks { get; set; }
        public string TypeOfStock { get; set; }
    }
}