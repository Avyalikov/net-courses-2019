namespace Trading.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Operation
    {
        [Key]
        public int OperationId { get; set; }
        
        public DateTime DebitDate { get; set; } // it's date for Customer

        public virtual Client Customer { get; set; }

        public DateTime ChargeDate { get; set; } // it's date for Seller

        public virtual Client Seller { get; set; }

        public virtual Share Share { get; set; }

        public virtual ShareType Type { get; set; }

        public decimal Cost { get; set; } // see ShareType.Cost (The cost will be fixed here at the time of purchase)

        public decimal Number { get; set; } // Number of shares for deal

        public decimal Total { get; set; } // Total = Cost * Number
    }
}
