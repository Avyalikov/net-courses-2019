namespace TradingApp.Core.Models
{
    public class ShareEntity
    {
        public int Id { get; set; }
        public virtual StockEntity Stock { get; set; }
        public decimal Amount { get; set; }
        public virtual TraderEntity Owner { get; set; }
        public virtual ShareTypeEntity ShareType { get; set; }
    }
}
