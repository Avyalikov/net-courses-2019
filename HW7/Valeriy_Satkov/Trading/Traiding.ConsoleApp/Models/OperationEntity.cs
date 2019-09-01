namespace Traiding.ConsoleApp.Models
{
    using System;

    public class OperationEntity
    {
        public int Id { get; set; }

        public DateTime DebitDate { get; set; } // it's date from Customer blocked money

        public virtual ClientEntity Customer { get; set; }

        public DateTime ChargeDate { get; set; } // it's date of finish

        public virtual ClientEntity Seller { get; set; }

        public virtual ShareEntity Share { get; set; }

        public string ShareTypeName { get; set; } // see ShareTypeEntity.Name (The name will be fixed here at the time of purchase)

        public decimal Cost { get; set; } // see ShareTypeEntity.Cost (The cost will be fixed here at the time of purchase)

        public int Number { get; set; } // Number of shares for deal

        public decimal Total { get; set; } // Total = Cost * Number
    }
}
