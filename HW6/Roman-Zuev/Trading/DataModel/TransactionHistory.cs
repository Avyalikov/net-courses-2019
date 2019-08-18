using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    public class TransactionHistory
    {
        [Key]
        public int TransactionHistoryID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
        public Client Seller { get; set; }
        public Client Buyer { get; set; }
        [Required]
        public Shares SelledItem { get; set; }
        [Required, Range(0, 1000000)]
        public int Quantity { get; set; }
    }
}
