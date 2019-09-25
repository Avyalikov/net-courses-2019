namespace TradingApp.Core.Models
{
    using System;

    public class TransactionStoryEntity
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int ShareId { get; set; }
        public int AmountOfShares { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TransactionCost { get; set; }

        public virtual ShareEntity Share { get; set; } 
    }
}
