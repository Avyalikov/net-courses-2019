using System;
using System.Collections.Generic;
using TradingApp.Core.Models;

namespace TradingApp.Core.Dto
{
    public class TransactionInfo
    { 
        public string SellerID { get; set; }
        public string BuyerID { get; set; }
        public string StockID { get; set; }
        public int StockAmount { get; set; }
        public DateTime dateTime { get; set; }
        public string SellerBalanceID { get; set; }
        public string BuyerBalanceID { get; set; }
    }
}