namespace Traiding.ConsoleApp.Models
{
    using System;

    public class BlockedMoneyEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } // Debit time
        public virtual BalanceEntity ClientBalance { get; set; }
        public virtual OperationEntity Operation { get; set; }
        public virtual ClientEntity Customer { get; set; }
        public decimal Total { get; set; }
    }
}
