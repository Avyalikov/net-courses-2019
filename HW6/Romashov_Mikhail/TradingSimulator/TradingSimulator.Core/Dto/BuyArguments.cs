using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Core.Dto
{
    public class BuyArguments
    {
        public int SellerID { get; set; }
        public int CustomerID { get; set; }
        public int StockID { get; set; }
        public int StockCount { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
