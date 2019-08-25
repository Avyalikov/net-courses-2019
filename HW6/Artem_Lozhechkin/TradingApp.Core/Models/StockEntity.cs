namespace TradingApp.Core.Models
{
    public class StockEntity
    {
        public int Id { get; set; }
        public CompanyEntity Company { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
