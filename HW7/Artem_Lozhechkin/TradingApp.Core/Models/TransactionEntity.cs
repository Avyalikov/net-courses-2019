namespace TradingApp.Core.Models
{
    using System;

    public class TransactionEntity
    {
        public int Id { get; set; }
        public virtual ShareEntity Share { get; set; }
        public virtual TraderEntity Buyer { get; set; }
        public virtual TraderEntity Seller { get; set; }
        public decimal TransactionPayment { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
