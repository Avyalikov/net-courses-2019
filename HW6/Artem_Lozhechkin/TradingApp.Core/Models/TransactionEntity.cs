namespace TradingApp.Core.Models
{
    using System;

    public class TransactionEntity
    {
        public int Id { get; set; }
        public ShareEntity Share { get; set; }
        public TraderEntity Buyer { get; set; }
        public TraderEntity Seller { get; set; }
        public decimal TransactionPayment { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
