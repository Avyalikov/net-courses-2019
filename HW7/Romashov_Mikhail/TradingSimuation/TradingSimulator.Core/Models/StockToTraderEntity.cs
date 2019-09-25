using System;

namespace TradingSimulator.Core.Models
{
    public class StockToTraderEntity
    {
        public int Id { get; set; }
        public int TraderId { get; set; }
        public int StockId { get; set; }
        public int StockCount { get; set; }
        public decimal PricePerItem { get; set; }
    }
}