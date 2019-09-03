namespace TradingApp.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ClientPortfolio
    {
        [Key, Column(Order = 0)]
        public int ClientID { get; set; }

        [Key, Column(Order = 1)]
        public int ShareID { get; set; }
        public int? AmountOfShares { get; set; }
    }
}