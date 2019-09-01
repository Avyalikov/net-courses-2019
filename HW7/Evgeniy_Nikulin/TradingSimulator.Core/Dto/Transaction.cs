namespace TradingSimulator.Core.Dto
{
    public class Transaction
    {
        public Trader seller;
        public Trader buyer;
        public Share sellerShare;
        public Share buyerShare;

        public override string ToString() => $"{seller.name} {seller.surname} sold {sellerShare.quantity} shares of {sellerShare.name} to {buyer.name} {buyer.surname}";
    }
}