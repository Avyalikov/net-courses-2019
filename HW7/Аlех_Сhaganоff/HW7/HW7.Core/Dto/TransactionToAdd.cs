using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Dto
{
    public class TransactionToAdd
    {
        [Required]
        public int SellerId { get; set; }
        [Required]
        public int BuyerId { get; set; }
        [Required]
        public int ShareId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
