using System;
using System.Collections;
using System.Collections.Generic;

namespace TradingApp.Core.Models
{
    public class BalanceEntity
    {
        public string BalanceID { get; set; }
        public string UserID { get; set; }
        public decimal Balance { get; set; }
        public Dictionary<string,int> Stocks { get; set; }  //the dictionary of pairs StockID-StockCount
        public DateTime CreatedAt { get; set; }
    }
}