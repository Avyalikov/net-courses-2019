using System;
namespace trading_software
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual Client Seller { get; set; }
        public virtual Client Buyer { get; set; }
        public virtual Stock Stocks { get; set; }
        public int Amount { get; set; }
    }
}
