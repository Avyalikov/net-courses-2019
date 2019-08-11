using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.DataModel
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required]
        public virtual Trader Seller { get; set; }
        [Required]
        public virtual Trader Buyer {get; set;}
        [Required]
        public virtual Share Share { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PricePerShare { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
