namespace TradingApp.Core.Models
{
    public class StockEntity
    {
        public int Id { get; set; }
        public virtual CompanyEntity Company { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
