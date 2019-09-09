using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TradingSimulator.Core.Models
{
    public class TransactionHistoryEntity
    {
        public DateTime DateTimeBay { get; set; }
        [Key]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public int AmountShare { get; set; }
        public decimal Cost { get; set; }
    }
}
