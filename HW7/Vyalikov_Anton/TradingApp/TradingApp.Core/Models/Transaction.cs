namespace TradingApp.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public virtual int ShareID { get; set; }
        public virtual int SellerID { get; set; }
        public virtual int BuyerID { get; set; }
        public int AmountOfShares { get; set; }
        public DateTime Date { get; set; }

    }
}