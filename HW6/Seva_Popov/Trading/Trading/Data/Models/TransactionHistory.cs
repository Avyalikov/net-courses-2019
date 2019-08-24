using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trading.Data
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int ShareName { get; set; }
        public DateTime DateTimeBay { get; set; }
        public int AmountShare { get; set; }
        public decimal Cost { get; set; }
    }
}
