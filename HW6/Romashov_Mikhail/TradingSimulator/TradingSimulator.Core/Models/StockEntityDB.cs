using System.Collections.Generic;

namespace TradingSimulator.Core.Models
{
    public class StockEntityDB : StockEntity
    {
        public virtual ICollection<StockToTraderEntityDB> StockToTraderEntity { get; set; }
    }
}