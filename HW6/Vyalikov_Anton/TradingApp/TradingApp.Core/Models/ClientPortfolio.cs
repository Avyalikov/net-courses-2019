namespace TradingApp.Core.Models
{
    public class ClientPortfolio
    {
        public int ClientID { get; set; }
        public int ShareID { get; set; }
        public int? AmountOfShares { get; set; }
        public virtual Client Clients { get; set; }
        public virtual Share Shares { get; set; }

    }
}