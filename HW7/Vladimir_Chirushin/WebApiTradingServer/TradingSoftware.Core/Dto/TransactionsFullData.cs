namespace TradingSoftware.Core.Dto
{
    using System;

    public class TransactionsFullData
    {
        public int TransactionID { get; set; }

        public DateTime dateTime { get; set; }

        public string SellerName { get; set; }

        public string BuyerName { get; set; }

        public string ShareType { get; set; }

        public decimal SharePrice { get; set; }

        public int ShareAmount { get; set; }
    }
}
