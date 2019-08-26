namespace TradingApp.Core.Models
{
    public class ShareEntity
    {
        public int Id { get; set; }
        public StockEntity Stock { get; set; }
        public decimal Amount { get; set; }
        public TraderEntity Owner { get; set; }
        public ShareTypeEntity ShareType { get; set; }
    }
}
