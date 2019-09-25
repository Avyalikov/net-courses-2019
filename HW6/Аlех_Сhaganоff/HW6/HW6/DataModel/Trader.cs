using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.DataModel
{
    public class Trader
    {
        public int TraderId { get; set; }
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public virtual List<Portfolio> Portfolio { get; set; }
    }
}
