using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace trading_software
{
    public class BlockOfShares
    {
        [Required]
        [Key, Column(Order = 0)]
        public virtual int ClientID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public virtual int StockID { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
