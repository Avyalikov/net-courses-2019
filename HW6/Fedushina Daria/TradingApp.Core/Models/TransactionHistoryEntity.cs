using System;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Models
{
    public class TransactionHistoryEntity
    {
        public int TransactionID { get; set; }
        //public string SellerID { get; set; }
        public string SellerBalanceID { get; set; }
       // public string BuyerID { get; set; }
        public string BuyerBalanceID { get; set; }
        public int StockAmount { get; set; }
        public string StockName { get; set; }
        public decimal TransactionQuantity { get; set; }
        public DateTime TimeOfTransaction { get; set; }
    }
}