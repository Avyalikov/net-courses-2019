using System;
using System.Collections.Generic;
using TradingApp.Core.Models;

namespace TradingApp.Core.Dto
{
    public class TransactionInfo
    { 
        public int SellerID { get; set; }
        public int BuyerID { get; set; }
        public int StockID { get; set; }
        public int StockAmount { get; set; }
        public DateTime dateTime { get; set; }
        public string SellerBalanceID { get; set; }
        public string BuyerBalanceID { get; set; }
    }
}