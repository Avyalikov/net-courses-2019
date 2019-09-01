namespace TradingApp.Core.DTO
{
    public class ShareInfo
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal Amount { get; set; }
        public int OwnerId { get; set; }
        public int ShareTypeId { get; set; }
    }
}
