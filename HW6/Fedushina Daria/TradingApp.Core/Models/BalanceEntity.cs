using System;
using System.Collections;
using System.Collections.Generic;

namespace TradingApp.Core.Models
{
    public class BalanceEntity 
    {
        public string BalanceID { get; set; }
        public int UserID { get; set; }
        public decimal Balance { get; set; }
        public int StockID { get; set; }  
        public int StockAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}