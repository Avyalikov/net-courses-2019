namespace TradingSimulator.DataBase.Model
{
    public class TransactionEntity
    {
        public int ID { get; set; }
        public virtual TraderEntity Seller { get; set; }
        public virtual TraderEntity Buyer { get; set; }
        public string ShareName { get; set; }
        public decimal SharePrice { get; set; }
        public int ShareQuantity { get; set; }
    }
}