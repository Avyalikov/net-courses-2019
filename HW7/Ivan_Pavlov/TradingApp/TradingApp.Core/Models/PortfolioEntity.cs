namespace TradingApp.Core.Models
{
    public class PortfolioEntity
    {
        public int Id { get; set; }
        public int UserEntityId { get; set; }
        public int ShareId { get; set; }
        public int Amount { get; set; }

        public virtual ShareEntity Share { get; set; }
    }
}
