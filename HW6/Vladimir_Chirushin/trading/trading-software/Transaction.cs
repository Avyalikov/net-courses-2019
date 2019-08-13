using System;
using System.Collections.Generic;

namespace trading_software
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual List<Client> Seller { get; set; }
        public virtual List<Client> Buyer { get; set; }
        public virtual List<Stock> Stocks { get; set; }
        public int Amount { get; set; }
    }
}
