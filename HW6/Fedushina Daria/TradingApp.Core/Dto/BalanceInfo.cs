using System.Collections;
using System.Collections.Generic;

namespace TradingApp.Core.Dto
{
    public class BalanceInfo
    {
        public BalanceInfo()
        {
        }
        public string UserID { get; set; }
        public decimal Balance { get; set; }
        public Dictionary<string,int> Stocks { get; set; }
        

    }
}