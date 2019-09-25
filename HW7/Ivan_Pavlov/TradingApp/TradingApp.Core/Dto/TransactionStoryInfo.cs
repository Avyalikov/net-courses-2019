namespace TradingApp.Core.Dto
{
    using System;
    using TradingApp.Core.Models;

    public class TransactionStoryInfo
    {
        public int sellerId { get; set; }
        public int customerId { get; set; }
        public int shareId { get; set; }
        public int AmountOfShares { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TransactionCost { get; set; }
        public virtual ShareEntity Share { get; set; }
    }
}
