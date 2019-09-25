namespace Traiding.ConsoleApp.Models
{
    using System;

    public class BlockedSharesNumberEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual SharesNumberEntity ClientSharesNumber { get; set; }
        public virtual OperationEntity Operation { get; set; }
        public virtual ClientEntity Seller { get; set; }
        public virtual ShareEntity Share { get; set; }
        public string ShareTypeName { get; set; } // see ShareTypeEntity.Name (The name will be fixed here at the time of purchase)
        public decimal Cost { get; set; } // see ShareTypeEntity.Cost (The cost will be fixed here at the time of purchase)
        public int Number { get; set; } // Number of shares for deal
    }
}
