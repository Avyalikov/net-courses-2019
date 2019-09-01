using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Core.Models
{
    public class HistoryEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int SellerID { get; set; }
        public int CustomerID { get; set; }
        public int StockID { get; set; }
        public int StockCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
