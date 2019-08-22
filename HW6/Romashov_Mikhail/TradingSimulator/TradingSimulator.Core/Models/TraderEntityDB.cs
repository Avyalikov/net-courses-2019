using System;
using System.Collections.Generic;

namespace TradingSimulator.Core.Models
{
    public class TraderEntityDB : TraderEntity
    {
        public virtual ICollection<StockToTraderEntity> StockToTraderEntity { get; set; }
    }
}