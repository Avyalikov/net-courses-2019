using System.Collections;
using System.Collections.Generic;
using TradingApp.Core.Models;

namespace TradingApp.Core.Dto
{
    public class BalanceInfo 
    {
        public BalanceInfo()
        {
        }
        public int UserID { get; set; }
        public decimal Balance { get; set; }
        public int StockID { get; set; }
        public int StockAmount { get; set; }


    }
}