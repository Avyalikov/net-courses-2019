using System;

namespace TradingSimulator.Core.Models
{
    public class StockToTraderEntityDB : StockToTraderEntity
    {
        public virtual TraderEntityDB Traders { get; set; }
        public virtual StockEntityDB Stocks { get; set; }
    }
}