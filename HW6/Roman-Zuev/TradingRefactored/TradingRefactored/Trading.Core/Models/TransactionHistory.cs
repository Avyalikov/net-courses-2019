using System;

namespace Trading.Core.Models
{
    public class TransactionHistoryEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ClientEntity Seller { get; set; }
        public virtual ClientEntity Buyer { get; set; }
        public virtual SharesEntity SelledItem { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
