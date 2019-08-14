using System;
using System.ComponentModel.DataAnnotations;
namespace trading_software
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        [Required]
        public virtual int SellerID { get; set; }
        [Required]
        public virtual int BuyerID { get; set; }
        [Required]
        public virtual int StockID { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
