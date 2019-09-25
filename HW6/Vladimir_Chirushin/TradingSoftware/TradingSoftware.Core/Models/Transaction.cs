namespace TradingSoftware.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual int SellerID { get; set; }
        public virtual int BuyerID { get; set; }
        public virtual int ShareID { get; set; }
        public int Amount { get; set; }
    }
}