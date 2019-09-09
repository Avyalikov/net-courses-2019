namespace Trading.TradesEmulator.Dto
{
    public class TransactionArguments
    {
        public int Quantity { get; set; }
        public int SharesId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
}