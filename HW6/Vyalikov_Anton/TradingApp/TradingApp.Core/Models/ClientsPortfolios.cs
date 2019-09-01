namespace TradingApp.Core.Models
{
    public class ClientsPortfolios
    {
        public int ClientID { get; set; }
        public int ShareID { get; set; }
        public int? AmountOfShares { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Shares Shares { get; set; }

    }
}