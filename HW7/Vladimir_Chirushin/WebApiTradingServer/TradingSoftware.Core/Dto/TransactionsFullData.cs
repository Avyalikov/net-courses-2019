namespace TradingSoftware.Core.Dto
{
    public class TransactionsFullData
    {
        public string SellerName { get; set; }

        public string BuyerName { get; set; }

        public string ShareType { get; set; }

        public decimal SharePrice { get; set; }

        public int ShareAmount { get; set; }
    }
}
