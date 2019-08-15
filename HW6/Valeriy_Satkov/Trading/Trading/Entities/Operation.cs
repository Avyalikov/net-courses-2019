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

        [Required]
        public DateTime DebitDate { get; set; } // it's date for Customer

        [Required, ForeignKey("ClientId")]
        public int Customer { get; set; }

        public DateTime ChargeDate { get; set; } // it's date for Seller

        [Required, ForeignKey("ClientId")]
        public int Seller { get; set; }

        [ForeignKey("ShareId")]
        public int Share { get; set; }

        public decimal Cost { get; set; } // see ShareType.Cost (The cost will be fixed here at the time of purchase)

        public decimal Number { get; set; } // Number of shares for deal

        public decimal Total { get; set; } // Total = Cost * Number
    }
}
