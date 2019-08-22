using System.Collections.Generic;

namespace TradingSimulator.Core.Models
{
    public class StockEntityDB : StockEntity
    {
        public virtual ICollection<StockToTraderEntity> StockToTraderEntity { get; set; }
    }
}